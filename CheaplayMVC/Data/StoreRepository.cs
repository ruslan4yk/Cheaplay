using CheaplayMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CheaplayMVC.Data
{
    public class StoreRepository : IGenericRepository<Store>
    {
        readonly DbContext _context;
        readonly DbSet<Store> _stores;

        public StoreRepository()
        {
            _context = new CheaplayContext(new DbContextOptions<CheaplayContext>());
            _stores = _context.Set<Store>();
        }
        public void Create(Store item)
        {
            _stores.Add(item);
            _context.SaveChanges();
        }

        public Store GetById(int id)
        {
            return _stores.FirstOrDefault(u => u.Id == id);
        }

        public List<Store> GetAll()
        {
            return _stores.ToList();
        }

        public void Remove(Store item)
        {
            _stores.Remove(item);
            _context.SaveChanges();
        }

        public void Update(Store item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
