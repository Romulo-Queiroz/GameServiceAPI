using GameServiceAPI.DTOs;

namespace GameServiceAPI.Interfaces
{
    public interface IFreeToGameClient
    {
        Task<List<GameBasic>> GetGamesAsync();
        Task<GameDetail> GetGameDetailAsync(int id);
    }
}
