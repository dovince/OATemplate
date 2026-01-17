using FSDZ.Logger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ZWL.Common;
using ZWL.DBUtility;

namespace SchedulerJob.CustomJobs
{
    public class ProjectReportedNotice //: ISchedulerJob
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
                        IntervalType = IntervalType.Week,
                        RunType = RunType.Workdaytime
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
                var rootsqlwhere = @"select {0} 
	                                from ERPXMJBXX x join ERPNWorkToDo d on x.NWorkID=d.ID	
	                                where StateNow in ('正在办理')	
	                                and JieDianID in (select n.ID from  ERPNWorkFlowNode n LEFT JOIN ERPNWorkFlow f on n.WorkFlowID=f.ID 
	                                where f.FormID=54 and NodeSerils>2)
	                                and YEAR(TimeStr)>2021 and DATEDIFF(day,TimeStr,GETDATE())>30";
                var xsqlwhere = rootsqlwhere.FormatWith(@"XMBH,ZYLB,NWorkID,UserName
                                ,(case when ZYLB in ('实验测试','水资源储量核实','规划编制','地学信息','地质科技') then 1 else 0 end) SJSC
                                ,(case when ZYLB in ('实验测试','矿山地质环境保护与治理恢复方案编制','地灾防治规划','规划编制','地学信息','地质科技') then 1 else 0 end) KGAQ");
                var csqlwhere = @"select *,SUBSTRING(BeiYong1,0,CHARINDEX('@',BeiYong1)) Number from ERPNWorkToDo where FormID in (56,75,62) and StateNow not in ('已被驳回','不通过')
	                                and SUBSTRING(BeiYong1,0,CHARINDEX('@',BeiYong1)) in ({0})".FormatWith(rootsqlwhere.FormatWith("XMBH"));
                var xlist = DbHelperSQL.GetDataTable(xsqlwhere);
                if (xlist != null && xlist.Rows.Count > 0)
                {
                    var dlist = Conv<ZWL.BLL.ERPNWorkToDo>.GetList(csqlwhere);
                    var glist = xlist.AsEnumerable().GroupBy(e => e.Field<string>("UserName"));
                    foreach (var gitem in glist)
                    {
                        var list = new List<string>();
                        foreach (var item in gitem)
                        {
                            var xmbh = item["XMBH"].ToString();
                            var sjsc = PublicMethod.GetInto(item["SJSC"]);
                            var kgaq = PublicMethod.GetInto(item["KGAQ"]);
                            if (sjsc <= 0)
                            {
                                sjsc = dlist.Any(e => (e.FormID == 56 || e.FormID == 75) && e.Number == xmbh) ? 1 : 0;
                            }
                            if (kgaq <= 0)
                            {
                                kgaq = dlist.Any(e => e.FormID == 62 && e.Number == xmbh) ? 1 : 0;
                            }
                            if (sjsc * kgaq <= 0)
                                list.Add(xmbh);
                        }
                        if (list.Any())
                        {
                            var projectno = string.Join(",", list);
                            if (projectno.Length > 35)
                            {
                                projectno = PublicMethod.LongToShortStr(projectno, 35);
                                var lindex = projectno.LastIndexOf(",");
                                if (lindex != projectno.Length - 1)
                                {
                                    projectno = projectno.Substring(0, lindex) + "等";
                                }
                            }
                            //您有${worknum}个${workname}超过${days}天${flowstate}！请及时${handlecontent}。(详细:${details})
                            var json = new
                            {
                                worknum = list.Count(),
                                workname = "项目",
                                days = 60,
                                flowstate = "未提交设计审查或安全报告",
                                handlecontent = "补充相关流程。",
                                details = projectno
                            };
                            Mobile.SendSMS("佛山地质局", gitem.Key, string.Format("ProjectReportRequiredNotice_{0}", JsonHelper.Convert2Json(json).ToBase64String()));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }
        }
        private DateTime MinDefaultDateTime
        {
            get
            {
                return new DateTime(0001, 01, 01);
            }
        }
    }
}
