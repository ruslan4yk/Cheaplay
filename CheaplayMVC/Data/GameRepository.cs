using CheaplayMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CheaplayMVC.Data
{
    public class GameRepository : IGenericRepository<Game>
    {
        readonly DbContext _context;
        readonly DbSet<Game> _games;

        public GameRepository()
        {
            _context = new CheaplayContext(new DbContextOptions<CheaplayContext>());
            _games = _context.Set<Game>();
        }
        public void Create(Game item)
        {
            _games.Add(item);
            _context.SaveChanges();
        }
        public Game GetById(int id)
        {
            return _games.FirstOrDefault(g => g.Id == id);
        }
        public Game GetBySharkApiId(int id)
        {
            return _games.FirstOrDefault(g => g.IdSharkAPI == id);
        }
        public List<Game> GetByTitle(string title)
        {
            if(title == null)
            {
                title = "";
            }
            return _games.Where(g => g.Title.ToLower().Contains(title.ToLower())).ToList();
        }
        public List<Game> GetAll()
        {
            return _games.ToList();
        }

        public List<Game> GetPopular()
        {
            return _games.OrderByDescending(g => g.NumberSubscribes).Take(10).ToList();
        }
        public List<Game> GetAllOnSale()
        {
            return _games.Where(g => g.IsOnSale).ToList();
        }
        public List<Game> GetRandomGames(int count)
        {
            var allGames = _games.ToList();
            List<Game> resultGames = new List<Game>();
            Random r = new Random();
            int gameId;
            for (int i = 0; i < count; i++)
            {
                gameId = r.Next(1, allGames.Count);
                resultGames.Add(allGames[gameId]);
            }
            return resultGames;
        }
        public void Remove(Game item)
        {
            _games.Remove(item);
            _context.SaveChanges();
        }

        public void Update(Game item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void AddSubscr(int gameId)
        {
            Game game = GetById(gameId);
            game.NumberSubscribes++;
            _context.SaveChanges();
        }
    }
}
