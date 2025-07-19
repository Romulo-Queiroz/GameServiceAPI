using System.Text.Json.Serialization;

namespace GameServiceAPI.DTOs
{
    public class MinimumSystemRequirements
    {
        [JsonPropertyName("os")]
        public string OS { get; set; } = string.Empty;

        [JsonPropertyName("processor")]
        public string Processor { get; set; } = string.Empty;

        [JsonPropertyName("memory")]
        public string Memory { get; set; } = string.Empty;

        [JsonPropertyName("graphics")]
        public string Graphics { get; set; } = string.Empty;

        [JsonPropertyName("storage")]
        public string Storage { get; set; } = string.Empty;
    }
}
