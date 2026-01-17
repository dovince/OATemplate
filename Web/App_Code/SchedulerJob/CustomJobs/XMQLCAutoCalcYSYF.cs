using FSDZ.Logger;
using NPOI.POIFS.Properties;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using ZWL.Common;
using ZWL.DBUtility;

namespace SchedulerJob.CustomJobs
{
    public class XMQLCAutoCalcYSYF : ISchedulerJob
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
                        RunType = RunType.Anytime
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
                var dt = DateTime.Now; ;
                var data = DbHelperSQL.GetDataTable(SQLWhere);
                var list = DataTableHelper.ConvertTo_1<ZWL.BLL.ERPXMJBXX>(data);
                var dList = DataTableHelper.ConvertTo_1<ZWL.BLL.ERPNWorkToDo>(data);
                if (list != null && list.Any())
                {
                    var tlist = new ZWL.BLL.ERPXMJBXXExtend().GetModelList("");
                    var kModel = new ZWL.BLL.ERPKeyValue();

                    for (int i = 0; i < list.Count; i++)
                    {
                        var item = list[i];
                        //如果不在库里，则判断是否为指定日期之后的项目，是则添加到库里
                        if (!tlist.Any(e => e.XMBH == item.XMBH))
                        {
                            var nModel = new ZWL.BLL.ERPXMJBXXExtend
                            {
                                XMBH = item.XMBH,
                                XMName = item.XMName,
                                JFHTJSJE = item.XMJF,
                                XMFZR = item.XMFZR,
                                YWLXR = item.XMFZR,
                                SFWG = "否",
                                SFYJF = "否",
                                SFYJWHZ = "否",
                                BM = item.XMBM,
                                DJR = item.XMFZR
                            };
                            nModel.ID = nModel.Add();
                            Logger.Log(LogType.Info, this.GetType().Name + " Add XMBH:" + item.XMBH);
                            if (nModel.ID > 0)
                            {
                                tlist.Add(nModel);
                            }
                        }
                        if (tlist.Any(r => r.XMBH == item.XMBH))//对库里的项目更新应收应付
                        {
                            #region MyRegion
                            var selectedXMItem = tlist.FirstOrDefault(r => r.XMBH == item.XMBH);
                            var ysfBaseDate = selectedXMItem.YSYFBaseDate;
                            var result = Util.GetXMYingShouYingFu(item.XMBH, ysfBaseDate);
                            selectedXMItem.YSJE = result.yingSJE;
                            selectedXMItem.YFJE = result.yingFJE;
                            selectedXMItem.YSGCK = result.yiSJE;
                            selectedXMItem.YFLWF = result.yiFJE;
                            if (selectedXMItem.DWMC.IsNullOrEmpty())
                            {
                                selectedXMItem.DWMC = item.WTDWName;
                            }
                            if (selectedXMItem.PZR.IsNullOrEmpty())
                            {
                                selectedXMItem.PZR = Util.GetXMPZR(item.XMBH);
                            }
                            if (selectedXMItem.ZL.IsNullOrEmpty())
                            {
                                selectedXMItem.ZL = Util.GetXMZL(item.XMBH);
                            }
                            if (selectedXMItem.ZQZSQ.IsNullOrEmpty())
                            {
                                selectedXMItem.ZQZSQ = Util.GetXMZQZSQ(item.XMBH);
                            }
                            if (selectedXMItem.ZQFSSJ.IsNullOrEmpty())
                            {
                                selectedXMItem.ZQFSSJ = Util.GetXMZQFSSJ(item.XMBH);
                            }
                            if (selectedXMItem.YFDW.IsNullOrEmpty())
                            {
                                var hlist = Util.GetXMRelativeEffetiveHeTongList(item.XMBH);
                                if (hlist != null && hlist.Any(r => r.HTLB == "收款"))
                                {
                                    selectedXMItem.YFDW = hlist.FirstOrDefault(r => r.HTLB == "收款").YFDW;
                                }
                            }
                            #endregion
                            selectedXMItem.Update();
                            Logger.Log(LogType.Info, this.GetType().Name + " Run XMBH:" + i + "," + tlist.IndexOf(selectedXMItem) + "," + item.XMBH);
                        }
                    }
                    Logger.Log(LogType.Info, this.GetType().Name + " Run ERPXMJBXXExtend Completed:" + DateTime.Now.ToString());
                    Logger.Log(LogType.Info, this.GetType().Name + " Run ERPXMQLCReport Start:" + DateTime.Now.ToString());
                    tlist = new ZWL.BLL.ERPXMJBXXExtend().GetModelList("");
                    if (tlist != null && tlist.Any())
                    {
                        var lotid = Guid.NewGuid().ToString();
                        var flow = new ZWL.BLL.Flow()
                        {
                            DataTable = "ERPXMQLCReport",
                            CreatedTime = DateTime.Now,
                            TKey = "LotID",
                            NewValue = "LotID",
                            Operation = (int)ZWL.BLL.FlowOperation.Add,
                            UserName = "admin",
                            LotID = lotid,
                            ParentID = lotid,
                            RecordID = lotid,
                        };
                        flow.ID = flow.Add();
                        var orderby = 1;
                        foreach (var item in tlist.GroupBy(e => e.XMBH))
                        {
                            Logger.Log(LogType.Info, " Run ERPXMQLCReport XMBH:" + item.Key);
                            var fitem = item.FirstOrDefault();
                            var xitem = list.FirstOrDefault(e => e.XMBH == item.Key);
                            if (xitem == null) continue;
                            var currentId = Guid.NewGuid().ToString();
                            var hSqlWhere = @"select h.* from ERPHeTong h join ERPNWorkToDo d on h.NWorkToDoID=d.ID
                            where StateNow not in ('已被驳回','不通过') 
                            and JieDianID in (select n.ID from  ERPNWorkFlowNode n LEFT JOIN ERPNWorkFlow f on n.WorkFlowID=f.ID 
	                                                            where f.FormID=d.FormID and NodeSerils>2) and XMID='{0}'".FormatWith(fitem.XMBH);
                            var hlist = Conv<ZWL.BLL.ERPHeTong>.GetList(hSqlWhere);
                            var hslist = hlist.Where(e => e.HTLB == "收款").CastToList();
                            var hflist = hlist.Where(e => e.HTLB == "付款").CastToList();
                            var htjiesuansql = @"select s.* from ERPHTJieSuan s join ERPNWorkToDo d on s.NWorkToDoID=d.ID 
                                where StateNow in ('正常结束')
                                and HTBH in (select HTID from ERPHeTong h	join ERPNWorkToDo d on h.NWorkToDoID=D.ID
                                where StateNow in ('正在办理','正常结束') and XMID in ('{0}'))
                                UNION
                                select s.* from ERPHTJieSuan s where NWorkToDoID is NULL and beiyong1 in ('{0}')".FormatWith(fitem.XMBH);
                            var htjslist = Conv<ZWL.BLL.ERPHTJieSuan>.GetList(htjiesuansql);
                            var costdetailsql = @"select i.* from ERPCostDetailPostItems i left join ERPProjectCost c on i.RecordId=c.ID where Item='工程出包费' and XMBH in ('{0}') ".FormatWith(fitem.XMBH);
                            var costdetaillist = Conv<ZWL.BLL.ERPCostDetailPostItems>.GetList(costdetailsql);
                            var workloadsql = @"SELECT * FROM ERPCostDetailPostItemsWorkload d where ParentID in (
                                SELECT ID FROM ERPCostDetailPostItems where RecordID in (
                                select ID from ERPProjectCost where XMBH in ('{0}')))".FormatWith(fitem.XMBH);
                            var workloadlist = Conv<ZWL.BLL.ERPCostDetailPostItemsWorkload>.GetList(workloadsql);
                            var shoukuansql = @"select s.* from ERPHeTongShouKuan s join ERPNWorkToDo d on s.NWorkToDoID=d.ID 
                                where StateNow in ('正在办理','正在办理，已开票','正常结束')
                                and HTBH in (select HTID from ERPHeTong h	join ERPNWorkToDo d on h.NWorkToDoID=D.ID
                                where StateNow in ('正在办理','正常结束') and XMID in ('{0}'))".FormatWith(fitem.XMBH);
                            var shoukuanlist = Conv<ZWL.BLL.ERPHeTongShouKuan>.GetList(shoukuansql);
                            var daozhangsql = @"select s.* from ERPHeTongDaoZhang s join ERPNWorkToDo d on s.NWorkToDoID=d.ID 
                                where StateNow in ('正在办理','正在办理，已开票','正常结束')
                                and HTBH in (select HTID from ERPHeTong h	join ERPNWorkToDo d on h.NWorkToDoID=D.ID
                                where StateNow in ('正在办理','正常结束') and XMID in ('{0}'))".FormatWith(fitem.XMBH);
                            var daozhanglist = Conv<ZWL.BLL.ERPHeTongDaoZhang>.GetList(daozhangsql);
                            var maxRow = GetHTMaxRows(hslist, htjslist, shoukuanlist, hflist);
                            for (int i = 0; i < maxRow; i++)
                            {
                                var report = new ZWL.BLL.ERPXMQLCReport
                                {
                                    LotID = lotid,
                                    XMID = item.Key,
                                    OrderBy = orderby,
                                };
                                if (i == 0)
                                {
                                    #region new ZWL.BLL.ERPXMQLCReport
                                    report.ID = currentId;
                                    report.XMBH = item.Key;
                                    report.XMName = fitem.XMName;
                                    report.DWMC = fitem.YFDW;
                                    report.ZYLB = xitem.ZYLB;
                                    report.SSBM = xitem.XMBM;
                                    report.XMFZR = xitem.XMFZR;
                                    report.PZR = fitem.PZR;
                                    report.YWLXR = fitem.YWLXR;
                                    report.JFDWXZ = xitem.JFDWXZ;
                                    report.JFDWMC = xitem.WTDWName;
                                    report.JFProvince = PublicMethod.ResolvedAddress(xitem.XMAdress, "province");
                                    report.JFCity = PublicMethod.ResolvedAddress(xitem.XMAdress, "city");
                                    report.JFDistrict = PublicMethod.ResolvedAddress(xitem.XMAdress, "district");
                                    report.ZL = fitem.ZL;
                                    report.XMBeginTime = TimeParser.GetFormatDateString(xitem.XMBeginTime);
                                    report.XMEndTime = TimeParser.GetFormatDateString(xitem.XMEndTime);
                                    report.SFWG = fitem.SFWG;
                                    report.GWSJ = TimeParser.GetFormatDateString(fitem.GWSJ);
                                    report.LYQK = fitem.LYQK;
                                    report.SFYJF = fitem.SFYJF;
                                    report.ZQZSQ = fitem.ZQZSQ;
                                    report.ZQFSSJ = fitem.ZQFSSJ;
                                    report.CSCS = fitem.CSCS;
                                    #endregion
                                }
                                else
                                {
                                    report.ID = Guid.NewGuid().ToString();
                                    report.ParentID = currentId;
                                }
                                var htindex = GetSKHTRows(hslist, htjslist, shoukuanlist);
                                var htjsindex = GetHTJSRows(hslist, htjslist, shoukuanlist);
                                var htskindex = GetHTSKRows(hslist, htjslist, shoukuanlist);
                                if (htindex.Contains(i))
                                {
                                    var htidx = htindex.IndexOf(i);
                                    var hitem = hslist.ElementAt(htidx);
                                    report.HTBH = hitem.HTID;
                                    report.HTBGQK = GetHTBGLY(hitem.NWorkToDoID);
                                    report.HTJE = hitem.HTJE;
                                }
                                if (htjsindex.Contains(i))
                                {
                                    var htidx = htjsindex.IndexOf(i);
                                    var hitem = htjslist.ElementAt(htidx);
                                    report.TCJSJE = hitem.TCJSJE;
                                    report.JSSJ = TimeParser.GetFormatDateString(hitem.JSTime);
                                    report.JFQRJSJE = hitem.JSJE;
                                    report.HJJSJE = htjslist.Sum(e => e.JSJE);
                                }
                                if (htskindex.Contains(i))
                                {
                                    var htidx = htskindex.IndexOf(i);
                                    var hitem = shoukuanlist.ElementAt(htidx);
                                    report.KPSJ = TimeParser.GetFormatDateString(hitem.SQTime);
                                    report.FPBH = hitem.FPBH;
                                    report.FPJE = hitem.KaiPiaoJE;
                                    var dzlist = daozhanglist.Where(e => e.NWorkToDoID == hitem.NWorkToDoID);
                                    if (dzlist != null && dzlist.Any())
                                    {
                                        report.HJSKJE = dzlist.Sum(r => r.DaoZhangJE);
                                    }
                                    report.WSKJE = report.HTJE - report.HJSKJE;
                                    report.WSKZYGZ = report.FPJE - report.HJSKJE;
                                    report.WSKWGZJE = report.HTJE - shoukuanlist.Sum(e => e.KaiPiaoJE);
                                }
                                if (hflist.Any() && i < hflist.Count)
                                {
                                    var hitem = hflist.ElementAt(i);
                                    report.FBDWMC = hitem.YFDW;
                                    report.FBProvince = PublicMethod.ResolvedAddress(hitem.Adress, "province");
                                    report.FBCity = PublicMethod.ResolvedAddress(hitem.Adress, "city");
                                    report.FBDistrict = PublicMethod.ResolvedAddress(hitem.Adress, "district");
                                    report.FBKSTime = TimeParser.GetFormatDateString(hitem.KSTime);
                                    report.FBJZTime = TimeParser.GetFormatDateString(hitem.JZTime);
                                    report.FBHTBH = hitem.HTID;
                                    report.FBHTJE = hitem.HTJE;
                                    if (costdetaillist.Any() && !hitem.YFDW.IsNullOrEmpty() && workloadlist.Any(e => e.Supplier.Contains(hitem.YFDW)))
                                    {
                                        report.FBHTHJFKJE = workloadlist.Where(e => e.Supplier.Contains(hitem.YFDW)).Sum(e => e.Amount);
                                    }
                                    report.FBHTWFKJE = hitem.HTJE - (report.FBHTHJFKJE ?? 0);
                                }
                                report.Add();
                                orderby++;
                            }
                        }
                    }
                    Logger.Log(LogType.Info, this.GetType().Name + " Run ERPXMQLCReport Completed:" + DateTime.Now.ToString());
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }
        }
        protected string GetHTBGLY(int nwid)
        {
            var todoModelExt = new ZWL.BLL.ERPNWorkToDoExtend();
            todoModelExt.GetModelByNWorkId(nwid);
            if (todoModelExt != null && todoModelExt.ID > 0)
            {
                var parser = new ZWL.Common.ParseHtml();
                parser.GetAttListFormHTMLinput(todoModelExt.FormContent);
                return parser.getValue("变更理由");
            }
            return "";
        }
        private int GetHTMaxRows(List<ZWL.BLL.ERPHeTong> hslist, List<ZWL.BLL.ERPHTJieSuan> htjslist, List<ZWL.BLL.ERPHeTongShouKuan> shoukuanlist, List<ZWL.BLL.ERPHeTong> hflist)
        {
            int maxRows = 0;
            if (hslist != null && hslist.Any())
            {
                for (int i = 0; i < hslist.Count; i++)
                {
                    maxRows += 1;
                    var item = hslist[i];
                    var fhtjslist = htjslist.Where(e => e.HTBH == item.HTID).CastToList();
                    var fshoukuanlist = shoukuanlist.Where(e => e.HTBH == item.HTID).CastToList();
                    if (fhtjslist.Count > 1 || fshoukuanlist.Count > 1)
                    {
                        if (fhtjslist.Count > fshoukuanlist.Count)
                            maxRows += fhtjslist.Count - 1;
                        else
                            maxRows += fshoukuanlist.Count - 1;
                    }
                }
            }

            if (hflist != null && hflist.Any() && hflist.Count > maxRows)
                maxRows = hflist.Count;
            if (maxRows == 0) maxRows = 1;
            return maxRows;
        }
        private List<int> GetSKHTRows(List<ZWL.BLL.ERPHeTong> hslist, List<ZWL.BLL.ERPHTJieSuan> htjslist, List<ZWL.BLL.ERPHeTongShouKuan> shoukuanlist)
        {
            var result = new List<int>();
            var preSubCount = 1;
            for (int i = 0; i < hslist.Count; i++)
            {
                var hitem = hslist.ElementAt(i);
                if (hitem == null) continue;
                if (i == 0) result.Add(i);
                else
                {
                    result.Add(result.ElementAt(i - 1) + preSubCount);
                }
                var fhtjslist = htjslist.Where(e => e.HTBH == hitem.HTID).CastToList();
                var fshoukuanlist = shoukuanlist.Where(e => e.HTBH == hitem.HTID).CastToList();
                if (fhtjslist.Any() && fhtjslist.Count > 1 || fshoukuanlist.Any() && fshoukuanlist.Count > 1)
                {
                    if (fhtjslist.Count > fshoukuanlist.Count)
                        preSubCount = fhtjslist.Count;
                    else
                        preSubCount = fshoukuanlist.Count;
                }
                else { preSubCount = 1; }
            }
            return result;
        }
        private List<int> GetHTJSRows(List<ZWL.BLL.ERPHeTong> hslist, List<ZWL.BLL.ERPHTJieSuan> htjslist, List<ZWL.BLL.ERPHeTongShouKuan> shoukuanlist)
        {
            var result = new List<int>();
            var htindex = GetSKHTRows(hslist, htjslist, shoukuanlist);
            for (int i = 0; i < htindex.Count; i++)
            {
                var hitem = hslist.ElementAt(i);
                var sindex = htindex.ElementAt(i);
                var fhtjslist = htjslist.Where(e => e.HTBH == hitem.HTID).CastToList();
                for (int j = 0; j < fhtjslist.Count; j++)
                {
                    result.Add(sindex + j);
                }
            }
            return result;
        }
        private List<int> GetHTSKRows(List<ZWL.BLL.ERPHeTong> hslist, List<ZWL.BLL.ERPHTJieSuan> htjslist, List<ZWL.BLL.ERPHeTongShouKuan> shoukuanlist)
        {
            var result = new List<int>();
            var htindex = GetSKHTRows(hslist, htjslist, shoukuanlist);
            for (int i = 0; i < htindex.Count; i++)
            {
                var hitem = hslist.ElementAt(i);
                var sindex = htindex.ElementAt(i);
                var fshoukuanlist = shoukuanlist.Where(e => e.HTBH == hitem.HTID).CastToList();
                for (int j = 0; j < fshoukuanlist.Count; j++)
                {
                    result.Add(sindex + j);
                }
            }
            return result;
        }
        private string SQLWhere
        {
            get
            {
                return @"select x.*,FormID
      ,WorkFlowID
      ,UserName
      ,TimeStr
      ,FuJianList
      ,JieDianID
      ,JieDianName
      ,ShenPiUserList
      ,OKUserList
      ,StateNow
      ,SUBSTRING(d.BeiYong1,0,CHARINDEX('@',d.BeiYong1)) Number
	  ,SUBSTRING(d.BeiYong1,CHARINDEX('@',d.BeiYong1)+1,LEN(d.BeiYong1)) Name
      ,0.00 YiSJE, 0.00 YingSJE,0.00 YiFJE,0.00 YingFJE 
    from ERPXMJBXX x join ERPNWorkToDo d on x.NWorkID=d.ID
        where StateNow not in ('不通过') and JieDianID in (select ID from ERPNWorkFlowNode where WorkFlowID in(select ID from ERPNWorkFlow where FormID=d.FormID) and CAST(NodeSerils as int)>2)  
    and (XMBH in (select XMBH from ERPXMJBXXExtend) or 
		(
		                        case when XMBM in ('第一工程处','第三工程处','第四工程处') and DJTime>'2023-08-31' then 1 
				                         when XMBM in ('第二工程处') and DJTime>'2023-06-30' then 1 
				                         when XMBM in ('区调中心') and DJTime>'2023-06-30' then 1 
				                         when XMBM in ('水环中心','地质调查所','工程施工部') and DJTime>'2023-05-31' then 1
				                         when XMBM in ('云浮分院') and DJTime>'2023-05-31' then 1				
		                        else 0 
		                        end
		                        )=1
		or DJTime>'2023-05-31' or CONVERT(varchar(100), TimeStr, 23)='2013-08-03') ";
            }
        }
    }

}
