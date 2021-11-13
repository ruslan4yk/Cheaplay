
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CheaplayMVC.Models
{
    public class Game
    {
        public string Title { get; set; }

        public decimal Price { get; set; }

        public int StoreId { get; set; }

        public int Id { get; set; }

        public double Discount { get; set; }

        public string Image { get; set; }

        public bool IsOnSale { get; set; }

        [Index(IsUnique = true)]
        public int IdSharkAPI { get; set; }

        public DateTime LastUpdate { get; set; }

        public int NumberSubscribes { get; set; }

        public Store Store { get; set; }
    }

}
