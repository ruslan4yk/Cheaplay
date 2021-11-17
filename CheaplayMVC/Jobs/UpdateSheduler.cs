using Quartz;
using Quartz.Impl;

namespace CheaplayMVC.Jobs
{
    public class UpdateSheduler
    {
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<GameUpdater>().Build();

            ITrigger trigger = TriggerBuilder.Create()  
                .WithIdentity("updatingTrigger", "group1")     
                .StartNow()                            
                .WithSimpleSchedule(x => x            
                    .WithIntervalInHours(4)          
                    .RepeatForever()
                    )               
                .Build();                               

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
