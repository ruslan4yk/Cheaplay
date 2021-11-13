using CheaplayMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CheaplayMVC.Data
{
    public class UserRepository : IGenericRepository<User>
    {
        readonly DbContext _context;
        readonly DbSet<User> _users;

        public UserRepository()
        {
            _context = new CheaplayContext(new DbContextOptions<CheaplayContext>());
            _users = _context.Set<User>();
        }
        public void Create(User item)
        {
            _users.Add(item);
            _context.SaveChanges();
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }
        public User GetByLogin(string login)
        {
            return _users.FirstOrDefault(u => u.Login.Equals(login));
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            var user = await _users.FirstOrDefaultAsync(u => u.Login.Equals(login));
            return user;
        }

        public User GetByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email.Equals(email));
        }

        public async Task<User> GetUserAsync(string login, string pass)
        {
            var user = await _users.FirstOrDefaultAsync(u => u.Login == login && u.Password == pass);
            return user;
        }
        public List<User> GetAll()
        {
            return _users.ToList();
        }

        public void Remove(User item)
        {
            _users.Remove(item);
            _context.SaveChanges();
        }

        public void Update(User item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
