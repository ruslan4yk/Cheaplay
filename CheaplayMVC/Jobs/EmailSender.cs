using CheaplayMVC.Data;
using CheaplayMVC.Services;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System.Threading.Tasks;

namespace CheaplayMVC.Jobs
{
    public class EmailSender : IJob
    {
        readonly CheaplayContext _contextDB = new CheaplayContext(new DbContextOptions<CheaplayContext>());
        public async Task Execute(IJobExecutionContext context)
        {
            await new SubcsriptionCheckService(_contextDB).StartAsync(new System.Threading.CancellationToken());
        }
    }
}
