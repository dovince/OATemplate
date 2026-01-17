using FSDZ.Logger;
using RequestJob;
using System;
using System.Linq;
using ZWL.Common;

namespace SchedulerJob.CustomJobs
{
    public class BirthdayNotice : ISchedulerJob
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
                        IntervalType = IntervalType.Day,
                        RunType = RunType.Worktime
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
                var sqlWhere = @"SFZSerils is not null and SFZSerils<>'' and CONVERT(INT,substring(SFZSerils,11,2))=DATENAME(MONTH, GETDATE())  and CONVERT(INT,substring(SFZSerils,13,2))=DATENAME(DAY, GETDATE())";
                var list = Conv<ZWL.BLL.ERPUser>.GetListBySQLWhere(sqlWhere);
                if (list != null && list.Any())
                {
                    foreach (var item in list)
                    {
                        var json = new
                        {
                            name = item.UserName,
                            dwmc = "佛山地质调查中心",
                        };
                        Mobile.SendSMS("佛山地质调查中心", item.UserName, "BirthdayNotice_{0}".FormatWith(JsonHelper.Convert2Json(json).ToBase64String()));
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
