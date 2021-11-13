using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cheaplay.Models;
using Cheaplay.Data;

namespace Cheaplay.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly GameRepository _gameRepository = new GameRepository();
        [HttpGet]
        public ActionResult<List<Game>> Get() => _gameRepository.GetRandomGames(5);
        
        [HttpGet("search")]
        public ActionResult<List<Game>> SearchGame([FromQuery] string title) => _gameRepository.GetByTitle(title);
        
    }
}
