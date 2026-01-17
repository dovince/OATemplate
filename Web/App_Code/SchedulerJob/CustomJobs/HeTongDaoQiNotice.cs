using FSDZ.Logger;
using RequestJob;
using System;
using System.Configuration;
using System.Linq;
using ZWL.Common;

namespace SchedulerJob.CustomJobs
{
    public class HeTongDaoQiNotice : ISchedulerJob
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
                var sqlWhere = @"select * from ERPMingCeHeTongZhiZhiGong 
                                where (DeleteMark is null or DeleteMark=0) 
                                and (HeTongDaoQiShiJian<GETDATE() and DATEDIFF(DAY,HeTongDaoQiShiJian,GETDATE())<=30)";
                var list = Conv<ZWL.BLL.ERPMingCeHeTongZhiZhiGong>.GetList(sqlWhere);
                if (list != null && list.Any())
                {
                    var templateCode = ConfigurationManager.AppSettings["TemplateCode_LaoWuHeTongDaoQiNotice"].ToString();
                    var ulist = Conv<ZWL.BLL.ERPUser>.GetListBySQLWhere("JiaoSe like '%人事科%' and IfLogin='是' ORDER BY DisplayID");
                    var glist = list.GroupBy(r => r.HeTongDaoQiShiJian.Value.Date);
                    foreach (var item in glist)
                    {
                        var checksms = Conv<ZWL.BLL.AliyunMsgLog>.GetListBySQLWhere("Template='{0}' and MsgContent like '%{1}%' and DATEDIFF(DAY,SendDate,GETDATE())<=30".FormatWith(templateCode, item.FirstOrDefault().XingMing));
                        if (checksms == null || !checksms.Any())
                        {
                            //TemplateCode_LaoWuHeTongDaoQiNotice//合同制职工[{0}]的劳务合同即将在1个月后[{1}]到期，请做好相关准备。
                            var smstext = new
                            {
                                username = string.Join(",", item.Select(r => r.XingMing)),
                                time = TimeParser.GetFormatDateString(item.Key)
                            };
                            Mobile.SendSMS("佛山地质局", PublicMethod.WorkWeiTuoUserList(string.Join(",", ulist.Select(r => r.UserName))),
                                @"TemplateCode_LaoWuHeTongDaoQiNotice_{0}".FormatWith(JsonHelper.Convert2Json(smstext).ToBase64String()));
                        }
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
