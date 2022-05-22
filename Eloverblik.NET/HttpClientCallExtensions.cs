using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Eloverblik.NET
{
    internal static class HttpClientCallExtensions
    {
        public static async Task<HttpResponseMessage> Call(this HttpClient httpClient, string url, HttpMethod method,
            HttpContent body = null, IReadOnlyDictionary<string, IEnumerable<string>> queryParameters = null,
            IReadOnlyDictionary<string, string> headers = null)
        {
            var queryParametersString = "";
            if (queryParameters != null)
            {
                var queryList = new List<string>();
                foreach (var parameter in queryParameters)
                {
                    queryList.Add($"{string.Join("&", parameter.Value.Select(o => $"{parameter.Key}={o}"))}");
                }
                queryParametersString = "?" + string.Join("&", queryList);
            }

            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri(url + queryParametersString),
                Method = method,
            };

            // Add content
            if (body != null)
            {
                httpRequest.Content = body;
            }

            // Add headers
            if (headers != null)
                foreach (var header in headers)
                {
                    httpRequest.Headers.Add(header.Key, header.Value);
                }

            // Send request
            var response = await httpClient.SendAsync(httpRequest);

            return response;
        }

        public static Task<HttpResponseMessage> Call(this HttpClient httpClient, string baseUrl, string relativeRoute,
            HttpMethod method, HttpContent body = null,
            IReadOnlyDictionary<string, IEnumerable<string>> queryParameters = null,
            IReadOnlyDictionary<string, string> headers = null)
        {
            return Call(httpClient, $"{baseUrl}/{relativeRoute}", method, body, queryParameters, headers);
        }

        /// <summary>
        /// Serialize a generic object to JSON, and return it as HttpContent
        /// </summary>
        public static HttpContent ConvertToJsonContent(object content)
        {
            var requestContent = content == null ? (HttpContent)null :
                new StringContent(JsonSerializer.Serialize(content),
                Encoding.UTF8, "application/json");
            return requestContent;
        }
    }

    public static class HttpResponseSerializationExtensions
    {
        /// <summary>
        /// Parse the response as json into the generic object
        /// </summary>
        public static async Task<T> ParseJson<T>(this HttpResponseMessage response,
            JsonSerializerOptions options = null)
        {
            if (options == null)
                options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Converters = { new JsonStringEnumConverter() }
                };

            var content = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<T>(content, options);
            return responseObject;
        }
    }
}
