using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public DateTime Created { get; set; }

        public User User { get; set; }

        public Game Game { get; set; }

        public override string ToString()
        {
            return String.Format("gameid={0}&userid={1}&maxprice={2}", GameId, UserId, decimal.ToDouble(MaxPrice));
        }

        public Subscription ToSubscription(string urlValue)
        {
            var splitUrl = urlValue.Split('&');
            var sub = new Subscription {
                GameId = Int32.Parse(splitUrl[0].Split('=')[1]),
                UserId = Int32.Parse(splitUrl[1].Split('=')[1]),
                MaxPrice = Decimal.Parse(splitUrl[2].Split('=')[1])
            };
            return sub;
        }

        public void Update(Subscription sub)
        {
            MaxPrice = sub.MaxPrice;
            IsActive = sub.IsActive;
            Created = IsActive ? sub.Created : Created;
        }
    }
}
