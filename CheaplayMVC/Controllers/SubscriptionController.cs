using CheaplayMVC.Data;
using CheaplayMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CheaplayMVC.Controllers
{
    [Authorize]
    public class SubscriptionController : Controller
    {
        private readonly SubscriptionRepository _subscrRepository = new SubscriptionRepository();
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly GameRepository _gameRepository = new GameRepository();
        public IActionResult All()
        {
            User user = _userRepository.GetByLogin(HttpContext.User.Identity.Name);
            var subsrcs = _subscrRepository.GetByUserId(user.Id);
            return View(subsrcs);
        }

        public IActionResult Add(string buttonAdd)
        {
            var sub = new Subscription().ToSubscription(buttonAdd);
            sub.IsActive = true;
            sub.Created = DateTime.Now;
            Subscription existedSub = _subscrRepository.GetConcreteSubscr(sub.GameId, sub.UserId);
            if (existedSub == null)
            {
                _gameRepository.AddSubscr(sub.GameId);
                _subscrRepository.Create(sub);
            }
            else
            {
                _subscrRepository.Update(sub);
            }
            return View(sub);
        }

        public IActionResult Disactivate(string subscription)
        {
            var sub = new Subscription().ToSubscription(subscription);            
            sub.IsActive = false;
            _subscrRepository.Update(sub);
            return View();
        }        
    }
}
