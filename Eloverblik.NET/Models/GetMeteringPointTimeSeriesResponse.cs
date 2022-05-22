using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;

namespace Eloverblik.NET.Models
{
    internal class GetMeteringPointTimeSeriesResponse
    {
        [JsonPropertyName("result")]
        public List<Result2> Result { get; set; }
    }

    public class MarketEvaluationPoint
    {
        [JsonPropertyName("mRID")]
        public MRID MRID { get; set; }
    }

    public class MRID
    {
        [JsonPropertyName("codingScheme")]
        public string CodingScheme { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class MyEnergyDataMarketDocument
    {
        [JsonPropertyName("mRID")]
        public string MRID { get; set; }

        [JsonPropertyName("createdDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [JsonPropertyName("sender_MarketParticipant.name")]
        public string SenderMarketParticipantName { get; set; }

        [JsonPropertyName("sender_MarketParticipant.mRID")]
        public SenderMarketParticipantMRID SenderMarketParticipantMRID { get; set; }

        [JsonPropertyName("period.timeInterval")]
        public PeriodTimeInterval PeriodTimeInterval { get; set; }

        [JsonPropertyName("TimeSeries")]
        public List<TimeSeries> TimeSeries { get; set; }
    }

    public class Period
    {
        [JsonPropertyName("resolution")]
        public string Resolution { get; set; }

        [JsonPropertyName("timeInterval")]
        public TimeInterval TimeInterval { get; set; }

        [JsonPropertyName("Point")]
        public List<Point> Point { get; set; }
    }

    public class PeriodTimeInterval
    {
        [JsonPropertyName("start")]
        public DateTime Start { get; set; }

        [JsonPropertyName("end")]
        public DateTime End { get; set; }
    }

    public class Point
    {
        [JsonPropertyName("position")]
        public string Position { get; set; }

        [JsonPropertyName("out_Quantity.quantity")]
        public string OutQuantityQuantity { get; set; }

        [JsonPropertyName("out_Quantity.quality")]
        public string OutQuantityQuality { get; set; }
    }

    public class Result2
    {
        [JsonPropertyName("MyEnergyData_MarketDocument")]
        public MyEnergyDataMarketDocument MyEnergyDataMarketDocument { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("errorCode")]
        public string ErrorCode { get; set; }

        [JsonPropertyName("errorText")]
        public string ErrorText { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("stackTrace")]
        public object StackTrace { get; set; }
    }

    public class SenderMarketParticipantMRID
    {
        [JsonPropertyName("codingScheme")]
        public object CodingScheme { get; set; }

        [JsonPropertyName("name")]
        public object Name { get; set; }
    }

    public class TimeInterval
    {
        [JsonPropertyName("start")]
        public DateTime Start { get; set; }

        [JsonPropertyName("end")]
        public DateTime End { get; set; }
    }

    public class TimeSeries
    {
        [JsonPropertyName("mRID")]
        public string MRID { get; set; }

        [JsonPropertyName("businessType")]
        public string BusinessType { get; set; }

        [JsonPropertyName("curveType")]
        public string CurveType { get; set; }

        [JsonPropertyName("measurement_Unit.name")]
        public string MeasurementUnitName { get; set; }

        [JsonPropertyName("MarketEvaluationPoint")]
        public MarketEvaluationPoint MarketEvaluationPoint { get; set; }

        [JsonPropertyName("Period")]
        public List<Period> Period { get; set; }
    }
}
