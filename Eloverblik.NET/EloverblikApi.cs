using Eloverblik.NET.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;

namespace Eloverblik.NET
{
    public class EloverblikApi
    {
        private readonly string _refreshToken;
        private string _token;
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;
        private const string dateFormat = "yyyy-MM-dd";

        public EloverblikApi(string refreshToken, HttpClient httpClient = null,
            string baseUrl = "https://api.eloverblik.dk/customerapi")
        {
            _refreshToken = refreshToken;
            _baseUrl = baseUrl;
            _httpClient = httpClient ?? new HttpClient();
        }

        public async Task<List<(string, List<(DateTime, double)>)>> GetMeteringPointTimeSeries(IEnumerable<string> meterIds, DateTime dateFrom, DateTime dateTo)
        {
            var aggregation = Aggregation.Hour;
            var apiRoute = $"api/meterdata/gettimeseries/{dateFrom.ToString(dateFormat)}/" +
                $"{dateTo.ToString(dateFormat)}/{aggregation}";
            var headers = await EnsureAuthorizationAndGetHeaders();

            var request = new GetMeterDataRequest()
            {
                MeteringPoints = new GetMeterDataRequest.MeteringPoint()
                {
                    MeterIds = meterIds,
                }
            };

            var response = await _httpClient.Call(_baseUrl, apiRoute, HttpMethod.Post,
                headers: headers, body: ConvertToJsonContent(request));
            
            var body = await response.ParseJson<GetMeteringPointTimeSeriesResponse>();

            // Convert response to output
            var output = new List<(string, List<(DateTime, double)>)>();

            foreach (var result in body.Result)
            {
                var timeSeries = new List<(DateTime, double)>();

                if (!result.Success)
                    output.Add((result.Id, timeSeries));

                var date = dateFrom.Date;
                foreach (var series in result.MyEnergyDataMarketDocument.TimeSeries)
                {
                    foreach (var period in series.Period)
                    {
                        foreach (var point in period.Point)
                        {
                            var value = double.Parse(point.OutQuantityQuantity, CultureInfo.InvariantCulture);
                            var time = date.Add(TimeSpan.FromHours(int.Parse(point.Position, CultureInfo.InvariantCulture) -1));
                            timeSeries.Add((time, value));
                        }
                    }
                    date = date.AddDays(1);
                }
                output.Add((result.Id, timeSeries));
            }
            return output;
        }

        public async Task<IEnumerable<Reading>> GetMeterReadings(IEnumerable<string> meterIds, DateTime dateFrom, DateTime dateTo)
        {
            var apiRoute = $"api/meterdata/getmeterreadings/{dateFrom.ToString(dateFormat)}/" +
                $"{dateTo.ToString(dateFormat)}";
            var headers = await EnsureAuthorizationAndGetHeaders();

            var request = new GetMeterDataRequest()
            {
                MeteringPoints = new GetMeterDataRequest.MeteringPoint()
                {
                    MeterIds = meterIds,
                }
            };

            var response = await _httpClient.Call(_baseUrl, apiRoute, HttpMethod.Post,
                headers: headers, body: ConvertToJsonContent(request));
            
            var body = await response.ParseJson<GetMeterReadingsResponse>();
            return body.Result.First().MeteringPointReading.Readings;
        }

        public async Task<IEnumerable<MeteringPointInfo>> GetMeteringPoints(bool includeAll = false)
        {
            var apiRoute = "/api/meteringpoints/meteringpoints";
            var headers = await EnsureAuthorizationAndGetHeaders();
            var parameters = new Dictionary<string, IEnumerable<string>>() { 
                { "includeAll", new List<string>() { includeAll.ToString().ToLower()} } 
            };

            var response = await _httpClient.Call(_baseUrl, apiRoute, HttpMethod.Get, headers: headers,
                queryParameters: parameters);

            var meteringPoints = await response.ParseJson<GetMeteringPointsResponse>();

            return meteringPoints.Result;
        }

        public async Task<string> GetToken()
        {
            var apiRoute = "api/token";
            var response = await _httpClient.Call(_baseUrl, apiRoute, HttpMethod.Get,
                headers: GetHeaders(_refreshToken));

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Exception thrown when calling api to get token. " +
                    $"Got response {response.StatusCode} and message {await response.Content.ReadAsStringAsync()}");

            var token = await response.ParseJson<GetTokenResponse>();
            _token = token.Token;
            return token.Token;
        }

        private async Task<Dictionary<string, string>> EnsureAuthorizationAndGetHeaders()
        {
            if (!string.IsNullOrEmpty(_token))
                return GetHeaders(_token);

            await GetToken();
            return GetHeaders(_token);
        }

        private static Dictionary<string, string> GetHeaders(string token)
        {
            var headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {token}");
            return headers;
        }

        private static HttpContent ConvertToJsonContent(object content)
        {
            var requestContent = content == null ? (HttpContent)null :
                new StringContent(JsonSerializer.Serialize(content),
                Encoding.UTF8, "application/json");
            return requestContent;
        }
    }
}
