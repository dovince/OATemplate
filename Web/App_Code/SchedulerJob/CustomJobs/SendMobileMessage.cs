using FSDZ.Logger;
using RequestJob;
using System;
using System.Configuration;
using System.Linq;
using ZWL.Common;

namespace SchedulerJob.CustomJobs
{
    public class SendMobileMessage //: ISchedulerJob
    {
        #region MyRegion
        private Definition _definition;
        public Definition Delimit
        {
            get
            {
                if (_definition == null)
                {
                    _definition = new Definition
                    {
                        IntervalType = IntervalType.Second,
                        RunType = RunType.Trigger
                    };
                }
                return _definition;
            }
            set
            {
                _definition = value;
            }
        }
        #endregion

        public void Run()
        {
            try
            {
                var dt = DateTime.Now;
                var sqlWhere = @"SELECT * FROM AliyunMsgLog where SendStatus='PENDING' and CONVERT(varchar(100), SendDate, 112)= CONVERT(varchar(100), GETDATE(), 112)";
                var list = Conv<ZWL.BLL.AliyunMsgLog>.GetList(sqlWhere);
                if (list != null && list.Any())
                {
                    foreach (var item in list)
                    {

                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }
        }
    }
}
