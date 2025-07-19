namespace GameServiceAPI.Dtos
{
    public class RecommendedGameDto
    {
        /// <summary>Nome do jogo.</summary>
        public string Title { get; set; } = default!;

        /// <summary>Link para abrir o jogo na FreeToGame.</summary>
        public string GameUrl { get; set; } = default!;
    }
}
