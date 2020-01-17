using ISWM.WEB.AutoScheduler.ISWM_Job;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.AutoScheduler.ISWM_Scheduled_Task
{
    public class JobScheduler
    {
        public static void Start()
        {
            try
            {                
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
                scheduler.Start();

                //####Add Truck Current location####

                var GCPTrackScheduler = ConfigurationManager.AppSettings["TTM_Schedul"];

                IJobDetail job = JobBuilder.Create<Job_AddTruckCurrentLocation>().Build();
                ITrigger trigger = TriggerBuilder.Create()                   
                    .WithIdentity("trigger1", "group1")
                    .WithCronSchedule(GCPTrackScheduler, cron => { cron.InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")); })
                    .ForJob(job)
                    .Build();

                scheduler.ScheduleJob(job, trigger);

                //####Add Truck Current location####

                var RFIDTrackScheduler = ConfigurationManager.AppSettings["RPM_Schedul"];

                IJobDetail job1 = JobBuilder.Create<Job_AddRFIDPunchingData>().Build();
                ITrigger trigger1= TriggerBuilder.Create()
                    .WithIdentity("trigger2", "group2")
                    .WithCronSchedule(RFIDTrackScheduler, cron => { cron.InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")); })
                    .ForJob(job1)
                    .Build();

                scheduler.ScheduleJob(job1, trigger1);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

        }
    }
}
