using CheaplayMVC.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheaplayMVC.Controllers
{
    public class GameController : Controller
    {
        private readonly GameRepository _gameRepository = new GameRepository();
        public IActionResult Allgames()
        {
            var allGames = _gameRepository.GetAll();
            return View(allGames);
        }

        public IActionResult Popular()
        {
            var allGames = _gameRepository.GetPopular();
            return View(allGames);
        }

        public IActionResult Search()
        {
            var allGames = _gameRepository.GetAll();
            return View(allGames);
        }

        [HttpPost]
        public IActionResult Search(string gameTitle)
        {
            var allGames = _gameRepository.GetByTitle(gameTitle);
            return View(allGames);
        }
    }
}
