using BingoBongo.DAL.DTOs;
using BingoBongo.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace BingoBongo.Web.Controllers
{
    [ApiController]
    [Route("/game")]
    public class GameStateController : ControllerBase
    {
        private readonly ILogger<GameStateController> _logger;
        private readonly IGameStateService _gameStateService;

        public GameStateController(ILogger<GameStateController> logger, IGameStateService gameStateService)
        {
            _logger = logger;
            _gameStateService = gameStateService;
        }

        [HttpGet]
        public ContentResult Index() {
            var fileContents = System.IO.File.ReadAllText("./Html/Index.html");
            return new ContentResult
            {
                Content = fileContents,
                ContentType = "text/html"
            };
        }

        [HttpGet]
        [Route("gamestate")]
        public async Task<GameStateDto> GameState(int userId)
        {
            return await _gameStateService.GetGameState(userId);
        }

        [HttpPost]
        [Route("roll")]
        public async Task<GameStateDto> Roll([FromBody] int gameStateId)
        {
            return await _gameStateService.Roll(gameStateId);
        }

        [HttpPost]
        [Route("restart")]
        public async Task<GameStateDto> Restart([FromBody] int gameStateId)
        {
            return await _gameStateService.RestartGame(gameStateId);
        }
    }
}