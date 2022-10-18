using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;

namespace Eloverblik.NET.Models
{
    internal class GetMeterReadingsResponse
    {
        [JsonPropertyName("result")]
        public List<Result> Result { get; set; }
    }

    public class Reading
    {
        [JsonPropertyName("readingDate")]
        public DateTime ReadingDate { get; set; }

        [JsonPropertyName("registrationDate")]
        public DateTime RegistrationDate { get; set; }

        [JsonPropertyName("meterNumber")]
        public string MeterNumber { get; set; }

        [JsonPropertyName("meterReading")]
        public string MeterReading { get; set; }

        [JsonPropertyName("measurementUnit")]
        public string MeasurementUnit { get; set; }
    }

    internal class Result
    {
        [JsonPropertyName("result")]
        public MeteringPointReading MeteringPointReading { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("errorCode")]
        public int ErrorCode { get; set; }

        [JsonPropertyName("errorText")]
        public string ErrorText { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("stackTrace")]
        public object StackTrace { get; set; }
    }

    internal class MeteringPointReading
    {
        [JsonPropertyName("meteringPointId")]
        public string MeteringPointId { get; set; }

        [JsonPropertyName("readings")]
        public List<Reading> Readings { get; set; }
    }
}
