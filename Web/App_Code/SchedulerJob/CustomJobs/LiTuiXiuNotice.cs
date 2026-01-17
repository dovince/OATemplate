using FSDZ.Logger;
using RequestJob;
using System;
using System.Configuration;
using System.Linq;
using ZWL.Common;

namespace SchedulerJob.CustomJobs
{
    public class LiTuiXiuNotice : ISchedulerJob
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
                var sqlWhere = @"select * from ERPMingCeZaiBianZhiGong 
                                    where XingBie='男' and DATEDIFF(year,ChuShengNianYue,GETDATE())=60 
                                    and (ChuShengNianYue<GETDATE() and DATEDIFF(DAY,ChuShengNianYue,GETDATE())<=30)
                                    UNION all
                                    select * from ERPMingCeZaiBianZhiGong 
                                    where XingBie='女' and ( DATEDIFF(year,ChuShengNianYue,GETDATE())=50 and GeRenShenFen='工人')
                                    and (ChuShengNianYue<GETDATE() and DATEDIFF(DAY,ChuShengNianYue,GETDATE())<=30)
                                    UNION all
                                    select * from ERPMingCeZaiBianZhiGong 
                                    where XingBie='女' and ( DATEDIFF(year,ChuShengNianYue,GETDATE())=55 and GeRenShenFen<>'工人')
                                    and (ChuShengNianYue<GETDATE() and DATEDIFF(DAY,ChuShengNianYue,GETDATE())<=30)";
                var list = Conv<ZWL.BLL.ERPMingCeZaiBianZhiGong>.GetList(sqlWhere);
                if (list != null && list.Any())
                {
                    var templateCode = ConfigurationManager.AppSettings["TemplateCode_BanLiTuiXiuNotice"].ToString();
                    var ulist = Conv<ZWL.BLL.ERPUser>.GetListBySQLWhere("JiaoSe like '%人事科%' and IfLogin='是' ORDER BY DisplayID");
                    foreach (var item in list)
                    {
                        var checksms = Conv<ZWL.BLL.AliyunMsgLog>.GetListBySQLWhere("Template='{0}' and MsgContent like '%{1}%' and DATEDIFF(DAY,SendDate,GETDATE())<=30".FormatWith(templateCode, item.XingMing));
                        if (checksms == null || !checksms.Any())
                        {
                            //TemplateCode_BanLiTuiXiuNotice//
                            //在编职工[${ username}]，性别${sex}，于${ time}将年满${ nianling}周岁，达到法定退休年龄，请提前做好办理退休准备。
                            var ningling = DateTime.Now.Year - item.ChuShengNianYue.Value.Year;
                            var smstext = new
                            {
                                username = item.XingMing,
                                sex = item.XingBie,
                                time = TimeParser.GetFormatDateString(item.ChuShengNianYue.Value.AddYears(ningling)),
                                nianling = ningling
                            };
                            Mobile.SendSMS("佛山地质局", PublicMethod.WorkWeiTuoUserList(string.Join(",", ulist.Select(r => r.UserName))),
                                @"TemplateCode_BanLiTuiXiuNotice_{0}".FormatWith(JsonHelper.Convert2Json(smstext).ToBase64String()));
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
