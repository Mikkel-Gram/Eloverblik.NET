using System.Text.Json.Serialization;

namespace Eloverblik.NET.Models
{
    internal class GetTokenResponse
    {
        [JsonPropertyName("result")]
        public string Token { get; set; }
    }
}
