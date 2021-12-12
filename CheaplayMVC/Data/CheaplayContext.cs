using CheaplayMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CheaplayMVC.Data
{
    public class CheaplayContext : DbContext
    {
        public CheaplayContext(DbContextOptions<CheaplayContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<DiscountUpdate> Updates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-TUQ6E5D; Database=CheaplayDB; Trusted_Connection=True; MultipleActiveResultSets=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().Property(g => g.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Subscription>().Property(g => g.MaxPrice).HasColumnType("decimal");
            modelBuilder.Entity<DiscountUpdate>().Property(g => g.Id).ValueGeneratedOnAdd();        
        }
    }
}
