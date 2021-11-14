using Quartz;
using System.Threading.Tasks;
using CheaplayMVC.Services;
using CheaplayMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace CheaplayMVC.Jobs
{
    public class GameUpdater : IJob
    {
        readonly CheaplayContext _contextDB = new CheaplayContext(new DbContextOptions<CheaplayContext>());
        public async Task Execute(IJobExecutionContext context)
        {
            await new UpdaterService(_contextDB).StartAsync(new System.Threading.CancellationToken());
        }        
    }
}
