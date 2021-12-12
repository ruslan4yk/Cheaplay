using System;
using CheaplayMVC.Data;

namespace CheaplayMVC.Models
{
    public class EmailObject
    {
        private readonly StoreRepository _storeRepository = new StoreRepository();

        public string GameTitle { get; set; }
        public string GameDiscount { get; set; }
        public string GamePrice { get; set; }
        public string GameDiscountPrice { get; set; }
        public string GameStore { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Savings { get; set; }

        public const string Subject = "Cheaplay discount informer";

        public EmailObject(Subscription subscription)
        {
            GameTitle = subscription.Game.Title;
            GameDiscount = subscription.Game.Discount.ToString();
            GamePrice = subscription.Game.Price.ToString();
            GameDiscountPrice = Math.Round((Decimal.ToDouble(subscription.Game.Price) *
                (100 - subscription.Game.Discount) / 100), 2).ToString();
            GameStore = _storeRepository.GetById(subscription.Game.StoreId).Name;
            UserName = subscription.User.FirstName;
            UserEmail = subscription.User.Email;
            Savings = Math.Round((Decimal.ToDouble(subscription.Game.Price) *
                subscription.Game.Discount) / 100, 2).ToString();
        }

        public string GetContent()
        {
            return String.Format("Dear {0},\n\n" +
                "We are glad to inform you that it's finally appeared discount on your subscribed game {1}!\n" +
                "Regular prire: {2}$\n" +
                "Discount: {3}%\n" +
                "Discount price: {4}$\n" +
                "Your savings: {5}$\n\n" +
                "Store: {6}\n\n" +
                "Thank you for choosing our service! See you next time!", 
                UserName, GameTitle, GamePrice, GameDiscount, GameDiscountPrice, Savings, GameStore);
        }
    }
}
