using GameServiceAPI.DTOs;
using GameServiceAPI.Interfaces;

namespace GameServiceAPI.Services
{
    public class GamesService
    {
        private readonly IFreeToGameClient _api;

        public GamesService(IFreeToGameClient api)
        {
            _api = api;
        }
        public async Task<GameDetail?> GetRecommendedGameAsync(
            IEnumerable<string> genres,
            string platform,
            int userMemoryGb)
        {
            var basics = await _api.GetGamesAsync();

            var details = (await Task.WhenAll(
                basics.Select(b => _api.GetGameDetailAsync(b.Id))
            ))
            .Where(d => d is not null)
            .Cast<GameDetail>();

            var filtered = details
              .Where(d =>
                genres.Any(g =>
                  string.Equals(d.Genre, g, StringComparison.OrdinalIgnoreCase)
                )

                && (
                   platform.Equals("all", StringComparison.OrdinalIgnoreCase)

                   || (platform.Equals("pc", StringComparison.OrdinalIgnoreCase)
                       && d.Platform.IndexOf("windows", StringComparison.OrdinalIgnoreCase) >= 0)

                   || (platform.Equals("browser", StringComparison.OrdinalIgnoreCase)
                       && d.Platform.IndexOf("browser", StringComparison.OrdinalIgnoreCase) >= 0)
                )

                && d.MinimumSystemRequirements?.Memory is string mem
                && TryParseMemoryGb(mem, out var req)
                && req <= userMemoryGb
              )
              .ToList();

            var platforms = details.Select(d => d.Platform).Distinct();

            if (!filtered.Any())
                return null;

            var rnd = new Random();
            return filtered[rnd.Next(filtered.Count)];
        }



        private bool TryParseMemoryGb(string memory, out int gb)
        {
            var m = System.Text.RegularExpressions.Regex.Match(memory, @"(\d+)");
            if (m.Success)
            {
                gb = int.Parse(m.Groups[1].Value);
                return true;
            }
            gb = 0;
            return false;
        }
    }
}
