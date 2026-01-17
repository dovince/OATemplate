using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace SchedulerJob
{
    public class SchedulerConfiguration
    {
        private int sleepInterval;
        private ArrayList jobs = new ArrayList();
        public int SleepInterval
        {
            get { return sleepInterval; }
        }
        public ArrayList Jobs
        {
            get
            {
                var types = Assembly.GetAssembly(typeof(ISchedulerJob)).GetTypes().Where(e => e.GetInterfaces().Contains(typeof(ISchedulerJob)));
                if (types != null && types.Any())
                {
                    foreach (var item in types)
                    {
                        if (item.IsInterface) continue;
                        var checkFlag = false;
                        for (int i = 0; i < jobs.Count; i++)
                        {
                            var sitem = jobs[i];
                            if (sitem.GetType().Name == item.Name)
                            {
                                checkFlag = true;
                                break;
                            }
                        }
                        if (!checkFlag)
                        {
                            var job = (ISchedulerJob)Activator.CreateInstance(item);
                            if (job.Delimit.RunType != RunType.Trigger)
                                jobs.Add(job);
                        }
                    }
                }
                return jobs;
            }
        }
        public SchedulerConfiguration(int newSleepInterval)
        {
            sleepInterval = newSleepInterval;
        }
    }
}
