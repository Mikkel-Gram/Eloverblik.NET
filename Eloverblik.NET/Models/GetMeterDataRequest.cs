using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Eloverblik.NET.Models
{
    internal class GetMeterDataRequest
    {
        [JsonPropertyName("meteringPoints")]
        public MeteringPoint MeteringPoints { get; set; }

        internal class MeteringPoint{
            [JsonPropertyName("meteringPoint")]
            public IEnumerable<string> MeterIds { get; set; }
        }
    }

}
