using GameServiceAPI.Dtos;
using GameServiceAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GamesService _svc;
        public GamesController(GamesService svc) => _svc = svc;

        /// <summary>
        ///     Retorna um jogo recomendado aleatoriamente que atenda aos filtros de gênero, plataforma e memória.
        /// </summary>
        /// <param name="genres">
        ///     Lista de gêneros de jogos (p.ex. Action, Adventure). Deve ter pelo menos 1 item.
        /// </param>
        /// <param name="platform">
        ///     Plataforma desejada: “pc”, “browser” ou “all” (padrão: “all”).
        /// </param>
        /// <param name="memory">
        ///     Quantidade de memória RAM disponível, em GB. Valor inteiro positivo.
        /// </param>
        /// <response code="200">Retorna um jogo recomendado (title + url).</response>
        /// <response code="400">Quando faltar gênero ou memory ≤ 0.</response>
        /// <response code="404">Quando não houver nenhum jogo que satisfaça os filtros.</response>
        [HttpGet("compatible")]
        [ProducesResponseType(typeof(RecommendedGameDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCompatible(
            [FromQuery] List<string> genres,
            [FromQuery] string platform = "all",
            [FromQuery] int memory = 0)
        {
            if (genres == null || !genres.Any())
                return BadRequest("Informe ao menos um gênero de jogo.");

            if (memory <= 0)
                return BadRequest("O parâmetro 'memory' deve ser um inteiro positivo.");

            var game = await _svc.GetRecommendedGameAsync(genres, platform, memory);
            if (game is null)
                return NotFound("Nenhum jogo encontrado com esses filtros. Tente alterar os critérios.");

            var dto = new RecommendedGameDto
            {
                Title = game.Title,
                GameUrl = game.GameUrl,
                Thumbnail = game.Thumbnail,
                ShortDescription = game.ShortDescription
            };
            return Ok(dto);
        }

    }
}
