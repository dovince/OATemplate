using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulerJob
{
    public class SchedulerAgent
    {
        private static System.Threading.Thread schedulerThread = null;
        private static Scheduler scheduler = null;
        public static void Start()
        {
            var config = new SchedulerConfiguration(1000 * 1);//设置时间，此处为1秒钟
            scheduler = new Scheduler(config);
            var myThreadStart = new System.Threading.ThreadStart(scheduler.Start);
            schedulerThread = new System.Threading.Thread(myThreadStart);
            schedulerThread.Start();
        }
        public static void Stop()
        {
            if (null != schedulerThread)
            {
                schedulerThread.Abort();
            }
            if (scheduler != null)
            {
                scheduler.Stop();
            }
        }
        public static void SendJob(ISchedulerJob job)
        {
            if (scheduler != null)
            {
                scheduler.SendJob(job);
            }
        }
        public static void ResetJob(ISchedulerJob job)
        {
            if (scheduler != null)
            {
                scheduler.ResetJob(job);
            }
        }
    }
}
