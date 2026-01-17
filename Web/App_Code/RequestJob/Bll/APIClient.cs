using Aspose.Words.Lists;
using Newtonsoft.Json;
using System.Data;
using System.Web;
using ZWL.Common;

/// <summary>
/// APIClient 的摘要说明
/// </summary>
namespace RequestJob
{
    //[Authorize]
    public class APIClient : Base, IRequestJob
    {
        public APIClient()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        //[HttpMethod(Action = HttpVerb.POST)]
        public JsonResult GetXMBaseInfo(HttpRequest Request)
        {
            var msg = "";
            var cpage = PublicMethod.GetInto(Request["currentPage"]);
            cpage = cpage == 0 ? 1 : cpage;
            var psize = PublicMethod.GetInto(Request["pageSize"]);
            psize = psize == 0 ? 20 : psize;
            var keyword = Request["keyword"];
            var beginTime = Request["beginTime"];
            var endTime = Request["endTime"];
            var sqlWhere = "";
            if (!keyword.IsNullOrEmpty())
            {
                sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere)
                    + @"(XMBH like '%{0}%' or XMName like '%{0}%' or WTDWName like '%{0}%' or CJDWName like '%{0}%')".FormatWith(keyword);
            }
            if (!beginTime.IsNullOrEmpty() && !endTime.IsNullOrEmpty())
            {
                sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere)
                    + @"( 
                                                (XMBeginTime BETWEEN '{0}' AND '{1}')
                                                OR (XMEndTime BETWEEN '{0}' AND '{1}')
                                                OR (XMBeginTime <= '{0}' AND XMEndTime >= '{1}')
                                            )".FormatWith(beginTime, endTime);
            }
            var sql = @"SELECT x.XMBH,x.XMQQBH,x.HTBH,x.XMName,x.XMState,x.XMAdress,x.WTDWName,
                            x.WTDWLXR,x.WTDWLXDH,x.HZDWName,x.HZDWLXR,x.HZDWLXDH,
                            (SELECT top 1 (
                                case when HTLB='付款' THEN JFDW
                                WHEN HTLB='收款' and ZYLB<>'租赁' THEN YFDW
                                WHEN HTLB='收款' and ZYLB='租赁' THEN JFDW
                                ELSE YFDW END
                                ) CDDWName from ERPHeTong h LEFT JOIN ERPNWorkToDo d on h.NWorkToDoID=d.ID
                                where h.XMID=x.XMBH and StateNow not in ('已被驳回','不通过')
                            ) CJDWName,
                            x.WTFS,x.ZYLB,x.HYLB,x.XMZJLY,x.XMJF,CONVERT(varchar(100), x.XMBeginTime, 23) XMBeginTime,
                            CONVERT(varchar(100), x.XMEndTime, 23) XMEndTime,x.XMBM,x.XMFZR,
                            x.ZKS,x.ZKJC,x.PGDJ,x.PGMJ,x.KCDJ,x.BXCLDJ,x.DCMJ,x.TC,x.KT,x.ZT,x.ZYLBMain,CONVERT(varchar(100), x.DJTime, 20) DJTime,
                            x.SHState,x.XMReport,x.SHTime,x.LNG,x.LAT,x.HTState,x.XMYWLXR,x.JFDWXZ,x.WGTime,x.ISWG,
                            x.LYQK,x.SFYJF,x.QPJE,x.YQQPJE,x.SFYJWHZ,x.CSCS 
                            from ERPXMJBXX x LEFT JOIN ERPNWorkToDo d on x.NWorkID=d.ID
                            where StateNow not in ('已被驳回','不通过')";
            var mainSql = "select * from ({0}) t ".FormatWith(sql);
            if (!sqlWhere.IsNullOrEmpty())
            {
                mainSql += " where {0}".FormatWith(sqlWhere);
            }
            DataTable dt = null;
            var pager = new Pager(mainSql, cpage, psize, "DJTime desc");
            if (pager.ExecuteToDataTable())
            {
                dt = (DataTable)pager.Result;
            }
            var result = new
            {
                pagination = new
                {
                    pageIndex = pager.CurrentPage,
                    pageSize = pager.PageSize,
                    total = pager.Rows
                },
                list = dt,
            };
            return JsonResult(true, "", result);
        }
    }
}