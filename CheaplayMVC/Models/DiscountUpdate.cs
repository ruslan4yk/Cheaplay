using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheaplayMVC.Models
{
    public class DiscountUpdate
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int StoreId { get; set; }
        public double Discount { get; set; }
        public DateTime UpdateTime { get; set; }
        public Game Game { get; set; }
    }
}
