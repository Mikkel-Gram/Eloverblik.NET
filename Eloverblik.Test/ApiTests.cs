using Eloverblik.NET;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eloverblik.Test
{
    [TestClass]
    public class ApiTests
    {
        private string _refreshToken;
        private EloverblikApi _api;

        [TestInitialize]
        public void Init()
        {
            _refreshToken = Environment.GetEnvironmentVariable("EloverblikRefreshToken");
            _api = new EloverblikApi(_refreshToken);
        }

        [TestMethod]
        [Ignore]
        public async Task Should_get_token()
        {
            var token = await _api.GetToken();
        }

        [TestMethod]
        [Ignore]
        public async Task Should_get_metering_points()
        {
            var meteringPoints = await _api.GetMeteringPoints(true);
            var meteringPoint = meteringPoints.First();
        }

        [TestMethod]
        [Ignore]
        public async Task Should_get_meter_readings()
        {
            var readings = await _api.GetMeterReadings(new List<string>() { "571313161160152568" },
                new DateTime(2019, 01, 01), new DateTime(2020, 01, 01));
        }

        [TestMethod]
        [Ignore]
        public async Task Should_get_meter_timeseries()
        {
            var timeseries = await _api.GetMeteringPointTimeSeries(new List<string>() { "571313161160152568" },
                new DateTime(2020, 01, 01), new DateTime(2020, 01, 10));
        }
    }
}