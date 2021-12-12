using CheaplayMVC.Data;
using Microsoft.AspNetCore.Mvc;
using SharkApiHelper;
using System;
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

        
        public IActionResult AddNewGame()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GameAddResult(string gameId)
        {
            if (gameId != null)
            {
                int intId = Int32.Parse(gameId);
                var newGame = await SharkApi.ConvertJsonToGame(intId);
                _gameRepository.Create(new Models.Game
                {
                    Title = newGame.Title,
                    Price = newGame.Price,
                    StoreId = newGame.StoreId,
                    Discount = newGame.Discount,
                    Image = newGame.Image,
                    IsOnSale = newGame.IsOnSale,
                    IdSharkAPI = newGame.IdSharkAPI,
                    LastUpdate = newGame.LastUpdate,
                    NumberSubscribes = 0
                });
            }
            return View();
        }
    }
}
