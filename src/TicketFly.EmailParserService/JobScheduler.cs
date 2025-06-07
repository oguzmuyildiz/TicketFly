using Quartz.Impl;
using Quartz;
using TicketFly.EmailParserService.Jobs;
using TicketFly.EmailParserService.Models;

namespace TicketFly.EmailParserService
{
    public class JobScheduler
    {
        private ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler;
        private readonly string Name;

        private Dictionary<string, TriggerKey> _triggers;

        public JobScheduler()
        {
            Name = "JobTrigger";
            InitJobTrigger();
        }

        public async void InitJobTrigger()
        {
            _triggers = new Dictionary<string, TriggerKey>(StringComparer.OrdinalIgnoreCase);
            _schedulerFactory = new StdSchedulerFactory();
            _scheduler = await _schedulerFactory.GetScheduler();
            await _scheduler.Start();
        }
        
        private void RemoveCurrentTasks(ScheduledJob sea)
        {
            var toRemove = new Dictionary<string, TriggerKey>(_triggers).Where(x=>x.Key == sea.OrganizationId.ToString());
            
            foreach (var triggerPair in toRemove)
            {
                try
                {
                    _scheduler.UnscheduleJob(triggerPair.Value);
                    _triggers.Remove(triggerPair.Key);
                }
                catch (Exception ex)
                {
                }
            }
        }

        private IJobDetail JobFactory()
        {
            var group = "readmail";
            var name = Guid.NewGuid().ToString();

            return JobBuilder.Create<ReadEmailJob>()
                .WithIdentity(name, group)
                .Build();
        }

        public async void ScheduleReadEmailJob(ScheduledJob sea)
        {
            RemoveCurrentTasks(sea);
            var trigger = TriggerBuilder.Create()
                .WithIdentity(sea.Id.ToString(), sea.OrganizationId.ToString())
                .WithCronSchedule(sea.Expression)
                .Build();

            var key = trigger.Key;
            //not sure if this is correct, but it seems like we are using the OrganizationId as a key
            _triggers.Add(sea.OrganizationId.ToString(), key);

            await _scheduler.ScheduleJob(JobFactory(), trigger);
        }
    }
}
