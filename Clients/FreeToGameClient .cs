using GameServiceAPI.DTOs;
using GameServiceAPI.Interfaces;

namespace GameServiceAPI.Clients
{
    public class FreeToGameClient : IFreeToGameClient
    {
        private readonly HttpClient _http;

        public FreeToGameClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<GameBasic>> GetGamesAsync() =>
            await _http.GetFromJsonAsync<List<GameBasic>>("/api/games")
                ?? new List<GameBasic>();

        public Task<GameDetail> GetGameDetailAsync(int id) =>
            _http.GetFromJsonAsync<GameDetail>($"/api/game?id={id}")!;
    }
}
