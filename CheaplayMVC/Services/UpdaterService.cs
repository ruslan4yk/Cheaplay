using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using CheaplayMVC.Models;
using CheaplayMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace CheaplayMVC.Services
{
    public class UpdaterService : BackgroundService
    {
        private int _countChanges = 1;
        readonly CheaplayContext _contextDB;
        readonly List<Game> _games = new List<Game>();

        public UpdaterService(DbContext contextDB)
        {
            _contextDB = (CheaplayContext)contextDB;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int[] gamesId = _contextDB.Games.Select(g => g.IdSharkAPI).ToArray();
            Game game;
            SharkApiHelper.Game sharkGame;
            foreach (int id in gamesId)
            {
                sharkGame = await SharkApiHelper.SharkApi.ConvertJsonToGame(id);
                game = new Game
                {
                    Title = sharkGame.Title,
                    Price = sharkGame.Price,
                    StoreId = sharkGame.StoreId,
                    Discount = sharkGame.Discount,
                    Image = sharkGame.Image,
                    IsOnSale = sharkGame.IsOnSale,
                    IdSharkAPI = sharkGame.IdSharkAPI,
                    LastUpdate = sharkGame.LastUpdate
                };
                _games.Add(game);
            }
            await CheckAndSaveUpdates(gamesId);
        }

        // Порівнює нові дані та дані з бази.
        // Якщо є зміни, оновлює базу та записує в табличку змін оновллення
        private async Task CheckAndSaveUpdates(int[] gamesId)
        {
            Game oldGame, newGame;
            foreach (int id in gamesId)
            {
                oldGame = _contextDB.Games.FirstOrDefault(g => g.IdSharkAPI == id);
                newGame = _games.FirstOrDefault(g => g.IdSharkAPI == id);
                if (newGame.Discount != oldGame.Discount)
                {
                    Console.WriteLine("Updates:" + _countChanges++);
                    oldGame.Discount = newGame.Discount;
                    oldGame.StoreId = newGame.StoreId;
                    oldGame.IsOnSale = newGame.Discount > 0;

                    DiscountUpdate update = new DiscountUpdate()
                    {
                        Discount = newGame.Discount,
                        StoreId = newGame.StoreId,
                        GameId = oldGame.Id,
                        UpdateTime = DateTime.Now
                    };
                    _contextDB.Updates.Add(update);
                }
                oldGame.LastUpdate = DateTime.Now;
                await _contextDB.SaveChangesAsync();
            }
        }
    }
}
