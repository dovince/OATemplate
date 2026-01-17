using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FSDZ.Logger;

namespace SchedulerJob
{
    public class Scheduler
    {
        private static readonly ZWL.BLL.ERPKeyValue thislock = new ZWL.BLL.ERPKeyValue();
        private SchedulerConfiguration configuration = null;

        private Thread runThread;

        private Queue<ISchedulerJob> jobQueue;

        private object quelock = new object();
        public Scheduler(SchedulerConfiguration config)
        {
            configuration = config;
            jobQueue = new Queue<ISchedulerJob>();
            this.runThread = new Thread(() =>
            {
                try
                {
                    this.DealJob();
                }
                catch (Exception ex)
                {
                    Logger.Log(LogType.Exception, ex.Message);
                }
            });
            //this.runThread.IsBackground = true;
            this.runThread.Start();
        }
        public void Start()
        {
            while (true)
            {
                try
                {
                    var dt = DateTime.Now;
                    if (!Util.IsDebug)
                        foreach (ISchedulerJob item in configuration.Jobs)
                        {
                            if (CheckSchedulerJobExec(item, dt))
                            {
                                item.Delimit.LastExecTime = dt;
                                this.UpdateSchedulerJobExecLogTime(item, dt);
                                Logger.SetUser(item.Delimit.OrderBy, item.GetType().Name);
                                Logger.Log(LogType.Info, dt.ToString());
                                this.SaveJob(item);
                            }
                        }

                }
                catch (Exception e)
                {
                    Logger.Log(LogType.Exception, e.Message);
                }
                finally
                {
                    Thread.Sleep(configuration.SleepInterval);
                }
            }
        }
        public void Stop()
        {
            if (runThread != null)
                runThread.Abort();
        }
        public void SendJob(ISchedulerJob job)
        {
            SaveJob(job);
        }
        public void ResetJob(ISchedulerJob job)
        {
            lock (this.quelock)
            {
                if (configuration.Jobs != null && configuration.Jobs.Count > 0)
                {
                    foreach (ISchedulerJob item in configuration.Jobs)
                    {
                        if (item.GetType().Name != job.GetType().Name) continue;
                        item.Delimit.LastExecTime = MinDefaultDateTime;
                        break;
                    }
                }
            }
        }
        private void DealJob()
        {
            while (true)
            {
                try
                {
                    ISchedulerJob job = null;
                    lock (this.quelock)
                    {
                        if (this.jobQueue.Count > 0)
                        {
                            job = this.jobQueue.Dequeue();
                        }
                    }
                    if (job != null)
                    {
                        var t2 = new Thread(() => job.Run());
                        t2.IsBackground = true;
                        // 启动线程  
                        t2.Start();
                    }
                }
                catch (Exception e)
                {
                    Logger.Log(LogType.Exception, e.Message);
                }
                finally
                {
                    Thread.Sleep(1);
                }
            }
        }
        private void SaveJob(ISchedulerJob job)
        {
            lock (this.quelock)
            {
                this.jobQueue.Enqueue(job);
            }
        }

        private bool CheckSchedulerJobExec(ISchedulerJob job, DateTime dt)
        {
            var result = false;
            var item = job.Delimit;
            if (item == null)
            {
                Activator.CreateInstance(job.Delimit.GetType());
            }
            if (item.RunType == RunType.Worktime)
            {
                if (!(dt.Hour >= 8 && dt.Hour <= 18))//非工作时间8至18时
                {
                    return false;
                }
            }
            if (item.RunType == RunType.Workday)
            {
                if (!ZWL.Common.PublicMethod.IsWorkDay(dt))//非工作日
                {
                    return false;
                }
            }
            if (item.RunType == RunType.Workdaytime)
            {
                if (!(ZWL.Common.PublicMethod.IsWorkDay(dt) && dt.Hour >= 8 && dt.Hour <= 18))//非（工作日且工作时间8至18时）
                {
                    return false;
                }
            }
            if (item.LastExecTime == MinDefaultDateTime)
            {
                var dbLogTime = GetSchedulerJobExecLogTime(job);
                if (dbLogTime == MinDefaultDateTime)
                    return true;
                else
                    item.LastExecTime = dbLogTime;
            }
            switch (item.IntervalType)
            {
                case IntervalType.Second:
                    result = item.LastExecTime.AddSeconds(item.Interval) <= dt;
                    break;
                case IntervalType.Minute:
                    result = item.LastExecTime.Date.AddHours(item.LastExecTime.Hour).AddMinutes(item.LastExecTime.Minute).AddMinutes(item.Interval) <= dt;
                    break;
                case IntervalType.Hour:
                    result = item.LastExecTime.Date.AddHours(item.LastExecTime.Hour).AddHours(item.Interval) <= dt;
                    break;
                case IntervalType.Day:
                    result = item.LastExecTime.Date.AddDays(item.Interval) <= dt;
                    break;
                case IntervalType.Week:
                    result = item.LastExecTime.Year != dt.Year || WeekOfYear(item.LastExecTime) + item.Interval <= WeekOfYear(dt);
                    break;
                case IntervalType.Month:
                    result = item.LastExecTime.Year != dt.Year || item.LastExecTime.AddMonths(item.Interval).Month <= dt.Month;
                    break;
                case IntervalType.Year:
                    result = item.LastExecTime.Year + item.Interval <= dt.Year;
                    break;
            }
            return result;
        }
        private bool UpdateSchedulerJobExecLogTime(ISchedulerJob job, DateTime dt)
        {
            var result = false;
            var className = job.GetType().Name;
            var list = GetSchedulerJobExecLogTimeList(job);
            if (list.Any())
            {
                lock (thislock)
                {
                    var fModel = list.FirstOrDefault();
                    fModel.Value1 = dt.ToString();
                    result = fModel.Update();
                }
            }
            else
            {
                lock (thislock)
                {
                    var kModel = new ZWL.BLL.ERPKeyValue
                    {
                        Category = "SchedulerJobExecLogTime",
                        Key1 = className,
                        Value1 = dt.ToString()
                    };
                    result = kModel.Add() > 0;
                }
            }
            return result;
        }
        private DateTime GetSchedulerJobExecLogTime(ISchedulerJob job)
        {
            var result = MinDefaultDateTime;
            var className = job.GetType().Name;
            var list = GetSchedulerJobExecLogTimeList(job);
            if (list.Any())
            {
                var dateTime = MinDefaultDateTime;
                if (DateTime.TryParse(list.FirstOrDefault().Value1, out dateTime))
                    result = dateTime;
            }
            return result;
        }
        private int WeekOfYear(DateTime dt)
        {
            string firstdayofyear = dt.Year.ToString() + "-01-01";
            DateTime firstday = Convert.ToDateTime(firstdayofyear);
            int weekday = (int)firstday.DayOfWeek;
            int weeknum = (dt.DayOfYear + weekday - 2) / 7 + 1;
            return weeknum;
        }
        private DateTime MinDefaultDateTime
        {
            get
            {
                return new DateTime(0001, 01, 01);
            }
        }
        private IList<ZWL.BLL.ERPKeyValue> GetSchedulerJobExecLogTimeList(ISchedulerJob job)
        {
            IList<ZWL.BLL.ERPKeyValue> list = null;
            lock (thislock)
            {
                var className = job.GetType().Name;
                list = thislock.GetModelList(string.Format("Category='SchedulerJobExecLogTime' and Key1='{0}'", className));
            }
            return list;
        }
    }
}
