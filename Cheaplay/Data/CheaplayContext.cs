using Cheaplay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Cheaplay.Data
{
    public class CheaplayContext : DbContext
    {
        public CheaplayContext(DbContextOptions<CheaplayContext> options): base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-TUQ6E5D; Database=CheaplayDB; Trusted_Connection=True; MultipleActiveResultSets=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().Property(g => g.Price).HasColumnType("decimal");
            modelBuilder.Entity<Subscription>().Property(g => g.MaxPrice).HasColumnType("decimal");

            /*modelBuilder.Entity<User>(u =>
            {
                u.HasKey(m => m.Id)
                 .HasName("PK_Tenants");

                u.Property(m => m.Id)
                 .UseSqlServerIdentityColumn();
            });

            modelBuilder.Entity<User>().HasData(
    new { FirstName = "User1Name", SecondName = "User1SName", Login = "User1Login", Password = "User1Pass", Birthday = "01-01-2000", Email = "user1@gmail.com" },
    new { FirstName = "User2Name", SecondName = "User2SName", Login = "User2Login", Password = "User2Pass", Birthday = "02-01-2000", Email = "user2@gmail.com" },
    new { FirstName = "User3Name", SecondName = "User3SName", Login = "User3Login", Password = "User3Pass", Birthday = "03-01-2000", Email = "user3@gmail.com" },
    new { FirstName = "User4Name", SecondName = "User4SName", Login = "User4Login", Password = "User4Pass", Birthday = "04-01-2000", Email = "user4@gmail.com" },
    new { FirstName = "User5Name", SecondName = "User5SName", Login = "User5Login", Password = "User5Pass", Birthday = "05-01-2000", Email = "user5@gmail.com" }
    );*/
        }
    }
}
