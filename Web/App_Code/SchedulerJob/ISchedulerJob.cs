using System;

namespace SchedulerJob
{
    /// <summary>
    ///ISchedulerJob 的摘要说明
    /// </summary>
    public interface ISchedulerJob
    {
        Definition Delimit { get; set; }
        void Run();
    }
    public enum IntervalType
    {
        Year = 1,
        Month = 2,
        Week = 3,
        Day = 4,
        Hour = 5,
        Minute = 6,
        Second = 7,
    }
    public enum RunType
    {
        Worktime = 1,
        Workday = 2,
        Workdaytime = 3,
        Anytime = 4,
        Trigger = 5,
    }
    public class Definition
    {
        public DateTime LastExecTime
        {
            get;
            set;
        }
        public RunType RunType { get; set; }
        public IntervalType IntervalType { get; set; }
        public int Interval { get; set; }
        public int OrderBy { get; set; }
        public Definition()
        {
            this.RunType = RunType.Worktime;
            this.IntervalType = IntervalType.Year;
            this.Interval = 1;
        }
    }
}
