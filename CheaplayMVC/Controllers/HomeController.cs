using CheaplayMVC.Data;
using CheaplayMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace CheaplayMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GameRepository _gameRepository = new GameRepository();
        private readonly UserRepository _userRepository = new UserRepository();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var games = _gameRepository.GetRandomGames(5);
            return View(games);
        }

        // Method Check() - user should check info about subscription.
        [Authorize]
        public IActionResult Check(string button)
        {
            Game game = _gameRepository.GetById(Int32.Parse(button));
            User user = _userRepository.GetByLogin(HttpContext.User.Identity.Name);
            var newSub = new Subscription
            {
                Created = DateTime.Now,
                IsActive = true,
                MaxPrice = 0,
                UserId = user.Id,
                GameId = game.Id,
                User = user,
                Game = game
            };
            return View(newSub);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
