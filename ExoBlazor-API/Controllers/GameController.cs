using ExoBlazor_API.Mappers;
using ExoBlazor_API.Models;
using ExoBlazor_API.Models.DTOs;
using ExoBlazor_API.Repositories;
using ExoBlazor_API.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExoBlazor_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly GameRepository _gameRepository;

        public GameController(GameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [Authorize("UserRequired")]
        [HttpPost("Add")]
        public IActionResult Insert([FromBody]GameDTO game)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (_gameRepository.Insert(game.ToModel()))
            {
                return Ok("Game added.");
            }
            return BadRequest("An error occurred during insertion.");
        }

        [HttpGet("Games")]
        public IActionResult GetAllGames()
        {
            return Ok(_gameRepository.GetAllGames());
        }

        [HttpGet("Game/{gameId}")]
        public IActionResult GetGame([FromRoute]int gameId)
        {
            Game? game = _gameRepository.GetGameById(gameId);
            
            if (game is null) return NotFound();

            return Ok(game);
        }

        [Authorize("AdminRequired")]
        [HttpPut("Edit/{gameId}")]
        public IActionResult UpdateGame([FromRoute]int gameId, [FromBody] GameDTO game)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (_gameRepository.UpdateGame(gameId, game.ToModel()))
            {
                return Ok("Game updated");
            }
            return BadRequest("An error occurred during update.");
        }

        [Authorize("AdminRequired")]
        [HttpDelete("Delete/{gameId}")]
        public IActionResult DeleteGame([FromRoute] int gameId)
        {
            if (_gameRepository.DeleteGame(gameId))
            {
                return Ok("Game deleted.");
            }
            return BadRequest("An error occurred during removal.");
        }
    }
}
