namespace GameServiceAPI.Dtos
{
    public class RecommendedGameDto
    {
        /// <summary>Nome do jogo.</summary>
        public string Title { get; set; } = default!;

        /// <summary>Link para abrir o jogo na FreeToGame.</summary>
        public string GameUrl { get; set; } = default!;
        /// <summary>URL da imagem de capa do jogo.</summary>
        public string Thumbnail { get; set; } = default!;

        /// <summary>Descrição curta do jogo.</summary>
        public string ShortDescription { get; set; } = default!;
    }
}
