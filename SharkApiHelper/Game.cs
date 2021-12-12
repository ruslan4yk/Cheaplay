namespace SharkApiHelper
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int StoreId { get; set; }
        public double Discount { get; set; }
        public string Image { get; set; }
        public bool IsOnSale { get; set; }
        public int IdSharkAPI { get; set; }
        public System.DateTime LastUpdate { get; set; }

    }
}