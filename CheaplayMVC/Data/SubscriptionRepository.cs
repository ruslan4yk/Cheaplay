using CheaplayMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CheaplayMVC.Data
{
    public class SubscriptionRepository : IGenericRepository<Subscription>
    {
        readonly DbContext _context;
        readonly DbSet<Subscription> _subscriptions;

        public SubscriptionRepository()
        {
            _context = new CheaplayContext(new DbContextOptions<CheaplayContext>());
            _subscriptions = _context.Set<Subscription>();
        }
        public void Create(Subscription item)
        {
            _subscriptions.Add(item);
            _context.SaveChanges();
        }
        public Subscription GetById(int id)
        {
            return _subscriptions.FirstOrDefault(g => g.Id == id);
        }
        public List<Subscription> GetByUserId(int id)
        {
            return _subscriptions.Where(g => g.UserId == id).Include(g=>g.Game).Include(g=>g.Game.Store).ToList();
        }
        public List<Subscription> GetByGameId(int id)
        {
            return _subscriptions.Where(g => g.GameId == id).Include(g => g.Game).ToList();
        }
        public Subscription GetConcreteSubscr(int gameId, int userId)
        {
            return _subscriptions.FirstOrDefault(s => s.GameId == gameId && s.UserId == userId);
        }
        public List<Subscription> GetAll()
        {
            return _subscriptions.Include(s=> s.Game).ToList();
        }
        public void Remove(Subscription item)
        {
            _subscriptions.Remove(item);
            _context.SaveChanges();
        }
        public void Update(Subscription item)
        {
            Subscription subscription = GetConcreteSubscr(item.GameId, item.UserId);
            subscription.Update(item);
            _context.SaveChanges();
        }
    }
}
