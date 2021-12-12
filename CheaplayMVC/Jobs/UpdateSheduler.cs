using Quartz;
using Quartz.Impl;
using System;

namespace CheaplayMVC.Jobs
{
    public class UpdateSheduler
    {
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail jobGameUpdater = JobBuilder.Create<GameUpdater>().Build();
            IJobDetail jobEmailSender = JobBuilder.Create<EmailSender>().Build();

            ITrigger triggerGameUpdater = TriggerBuilder.Create()
                .WithIdentity("triggerGameUpdater", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(4)
                    .RepeatForever()
                    )
                .Build();

            var startEmailSenderAt = DateTime.Now.AddHours(2);

            ITrigger triggerEmailSender = TriggerBuilder.Create()
                .WithIdentity("triggerEmailSender", "group1")
                .StartAt(startEmailSenderAt)
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(4)
                    .RepeatForever()
                    )
                .Build();

            await scheduler.ScheduleJob(jobGameUpdater, triggerGameUpdater);
            await scheduler.ScheduleJob(jobEmailSender, triggerEmailSender);
        }
    }
}
