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
            _refreshToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0b2tlblR5cGUiOiJDdXN0b21lckFQSV9SZWZyZXNoIiwidG9rZW5pZCI6IjdlZGU4NTUyLWJhODMtNDhkMi1iYzliLWZjMmUwZjA4MTlkOSIsIndlYkFwcCI6WyJDdXN0b21lckFwaSIsIkN1c3RvbWVyQXBwQXBpIl0sImp0aSI6IjdlZGU4NTUyLWJhODMtNDhkMi1iYzliLWZjMmUwZjA4MTlkOSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiUElEOjkyMDgtMjAwMi0yLTkwMDEzMjQyMjQwMCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2dpdmVubmFtZSI6IlBzZXVkb255bSIsImxvZ2luVHlwZSI6IktleUNhcmQiLCJwaWQiOiI5MjA4LTIwMDItMi05MDAxMzI0MjI0MDAiLCJ0eXAiOiJQT0NFUyIsInVzZXJJZCI6IjE3OTcxMSIsImV4cCI6MTY4MzIyOTg5MywiaXNzIjoiRW5lcmdpbmV0IiwidG9rZW5OYW1lIjoiQVdTIHRva2VuIiwiYXVkIjoiRW5lcmdpbmV0In0.OajerUsSqwCh9ZPSdNey9N7Uo6txJ4RMd3EVKKCZUR8";
            _api = new EloverblikApi(_refreshToken);
        }

        [TestMethod]
        public async Task Should_get_token()
        {
            var token = await _api.GetToken();
        }

        [TestMethod]
        public async Task Should_get_metering_points()
        {
            var meteringPoints = await _api.GetMeteringPoints(true);
            var meteringPoint = meteringPoints.First();
        }

        [TestMethod]
        public async Task Should_get_meter_readings()
        {
            await _api.GetMeterReadings(new List<string>() { "571313161160152568" },
                new DateTime(2019, 01, 01), new DateTime(2020, 01, 01));
        }

        [TestMethod]
        public async Task Should_get_meter_timeseries()
        {
            await _api.GetMeteringPointTimeSeries(new List<string>() { "571313161160152568" },
                new DateTime(2020, 01, 01), new DateTime(2020, 01, 3));
        }
    }
}