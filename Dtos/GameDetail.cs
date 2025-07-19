using System.Text.Json.Serialization;

namespace GameServiceAPI.DTOs
{
    public class GameDetail
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("game_url")]
        public string GameUrl { get; set; } = string.Empty;

        [JsonPropertyName("genre")]
        public string Genre { get; set; } = string.Empty;

        [JsonPropertyName("platform")]
        public string Platform { get; set; } = string.Empty;

        [JsonPropertyName("minimum_system_requirements")]
        public MinimumSystemRequirements MinimumSystemRequirements { get; set; }
            = new MinimumSystemRequirements();

    }
}
