using CheaplayMVC.Data;
using CheaplayMVC.Models;
using Microsoft.Extensions.Hosting;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CheaplayMVC.Services
{
    class SubcsriptionCheckService : BackgroundService
    {
        readonly CheaplayContext _contextDB;

        public SubcsriptionCheckService(DbContext contextDB)
        {
            _contextDB = (CheaplayContext)contextDB;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var activeSubs = _contextDB.Subscriptions.Where(s => s.IsActive).Include(s=>s.Game).Include(s=>s.User);
            List<Subscription> emailQueue = new List<Subscription>();

            foreach (var sub in activeSubs)
            {
                if (sub.Game.IsOnSale)
                {
                    emailQueue.Add(sub);
                }
            }

            if (emailQueue.Count > 0)
            {
                foreach (var subcsription in emailQueue)
                {
                    await SendMail(new EmailObject(subcsription));
                    subcsription.IsActive = false;
                    _contextDB.SaveChanges();
                }
            }

        }

        private async Task SendMail(EmailObject email)
        {
            var apiKey = Environment.GetEnvironmentVariable("SendGridSecondApiKey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("ruslanq37@gmail.com", "Cheaplay");
            var subject = "Congratulations!";
            var to = new EmailAddress(email.UserEmail, email.UserName);
            var plainTextContent = email.GameTitle;
            var htmlContent = "<pre>" + email.GetContent() + "</pre>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);
        }
    }
}
