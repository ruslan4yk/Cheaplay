using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheaplayMVC.Models
{
    public class Subscription
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int GameId { get; set; }

        public decimal MaxPrice { get; set; }

        public bool IsActive { get; set; }

        public User User { get; set; }

        public Game Game { get; set; }
    }
}
