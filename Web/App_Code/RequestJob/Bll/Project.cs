using Aspose.Words;
using Aspose.Words.Tables;
using FSDZ.Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Web.UI.WebControls;
using Utility;
using ZWL.Common;
using ZWL.DBUtility;

namespace RequestJob
{
    /// <summary>
    /// Project 的摘要说明
    /// </summary>
    [AuthSession]
    public class Project : Base, IRequestJob
    {

        public JsonResult GetProjectList(HttpRequest Request)
        {
            string query = Request["q"];
            if (query.Contains(",") && query.Length > 1)
            {
                query = query.Substring(1, query.Length - 1);
            }
            string strsql = "select top 20 ID as value,ProjectName as text from PROJECT where ProjectName like '%" + query + "%'";
            DataSet ds = ZWL.DBUtility.DbHelperSQL.GetDataSet(strsql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                var reault = DataTableHelper.ConvertTo<Combobox>(dt);
                return JsonResult(true, reault);
            }
            else
            {
                strsql = "select top 20 ID as value,ProjectName as text from PROJECT ";
                ds = ZWL.DBUtility.DbHelperSQL.GetDataSet(strsql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    List<Combobox> reault = DataTableHelper.ConvertTo<Combobox>(dt);
                    return JsonResult(true, reault);
                }
            }
            return JsonResult(false, "");
        }
        public JsonResult GetContractList(HttpRequest Request)
        {
            int page = Convert.ToInt32(Request["page"] ?? "1");
            int rows = Convert.ToInt32(Request["rows"] ?? "10");
            string sort = Request["sort"] ?? "id";
            string order = Request["order"] ?? "asc";

            var sqlWhere = "";
            var htbh = Request["HTBH"];
            var htname = Request["HTName"];
            if (!htbh.IsNullOrEmpty())
            {
                sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere) + " HTBH like '%{0}%'".FormatWith(htbh);
            }
            if (!htname.IsNullOrEmpty())
            {
                sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere) + " HTName like '%{0}%'".FormatWith(htname);
            }
            // 构建安全排序SQL
            string orderBy = BuildSafeOrderBy(sort, order);
            if (orderBy.IsNullOrEmpty())
            {
                orderBy = " ID DESC ";
            }
            else
            {
                orderBy = orderBy.Replace("ORDER BY", "");
            }

            var dt = new DataTable();
            string sql = string.Format(@"select * from (SELECT h.*,
                                            CONVERT(VARCHAR(20), KSTime, 23) KSDate,
                                            CONVERT(VARCHAR(20), JZTime, 23) JZDate,
                                            REPLACE((case when (JFDW like '%广东省地质局佛山地质调查中心%' or JFDW like '%广东省佛山地质局%' or JFDW like '%广东佛山地质工程勘察院%')  then JFDW ELSE YFDW end ),'（广东省佛山地质灾害应急抢险技术中心）','') WoFangDanWei,
                                            (case when (JFDW like '%广东省地质局佛山地质调查中心%' or JFDW like '%广东省佛山地质局%' or JFDW like '%广东佛山地质工程勘察院%')  then YFDW ELSE JFDW end ) DuiFangDanWei,
                                            ISNULL(x.XMFZR, d.UserName) XMFZR,ISNULL(u.JiaTingDianHua, l.JiaTingDianHua) JiaTingDianHua,
                                            d.StateNow,d.UserName,d.TimeStr
                                            from ERPHeTong h join ERPNWorkToDo d on h.NWorkToDoID=d.ID
                                            LEFT JOIN ERPXMJBXX x on h.XMID=x.XMBH
                                            LEFT JOIN ERPUser u on x.XMFZR=u.UserName
                                            LEFT JOIN ERPUser l on l.UserName=d.UserName
                                            where StateNow not in ('已被驳回','不通过')) t");
            if (!sqlWhere.IsNullOrEmpty())
            {
                sql += " where " + sqlWhere;
            }
            var pager = new Pager(sql, page, rows, orderBy);
            if (pager.ExecuteToDataTable())
            {
                dt = (DataTable)pager.Result;
            }
            var json = "{\"total\":" + pager.Rows + ",\"rows\":" + JsonConvert.SerializeObject(dt) + "}";
            return JsonResult(true, "", json);
        }

        public JsonResult SubmitDataIntegBizSys(HttpRequest Request)
        {
            var baseinfo = Request["baseinfo"];
            var list = Request["list"];
            var url = Util.IntegBizSysAPIBaseUrl + "/api/oauth/CurrentUser?systemCode=&_t=" + DateTime.Now.Ticks;

            var sqlWhere = @"select top 1 * from Token where GETDATE()<ExpiresTime and EnabledMark=1 and UserName='{0}' and Type='Sync' order by ID desc ".FormatWith(PublicMethod.GetUserName());
            var temp = Conv<ZWL.BLL.Token>.GetModel(sqlWhere);
            if (temp == null || temp.ExpiresTime.Value < DateTime.Now)
            {
                return JsonResult(false, "token过期，重新登录。");
            }
            var headers = new Dictionary<string, string>();
            headers.Add("Authorization", temp.TokenValue);
            var res = RequestHelper.HttpGet<CurrentUser>(url, null, headers);
            // 解析为JObject
            var baseinfoObj = JObject.Parse(baseinfo);

            // 过滤非"f_"开头的属性
            var filteredBaseinfo = new JObject();
            foreach (var prop in baseinfoObj.Properties())
            {
                if (prop.Name.StartsWith("f_"))
                {
                    var val = prop.Value;
                    if (prop.Name == "f_data_year")
                    {
                        val = new DateTime(PublicMethod.GetInto(val), 1, 1);
                    }
                    filteredBaseinfo.Add(prop.Name, val);
                }
            }

            filteredBaseinfo.Add("id", ""); // 示例值
            filteredBaseinfo.Add("flowId", ""); // 示例值
            filteredBaseinfo.Add("f_organize_id", JArray.Parse(JsonConvert.SerializeObject(res.data.userinfo.organizeIdList)));
            filteredBaseinfo.Add("f_organize_name", res.data.userinfo.organizeIdList[2].ToString());
            filteredBaseinfo.Add("f_creator_user_id", res.data.userinfo.userId);
            filteredBaseinfo.Add("f_creator_time", DateTime.Now);

            // 解析为JArray
            JArray listArray = JArray.Parse(list);

            // 遍历每个对象处理
            foreach (JObject item in listArray)
            {
                // 过滤非"f_"开头的属性
                var filteredItem = new JObject();
                foreach (var prop in item.Properties())
                {
                    if (prop.Name.StartsWith("f_"))
                    {
                        filteredItem.Add(prop.Name, prop.Value);
                    }
                }

                // 清空原对象并替换为过滤后的属性
                item.RemoveAll();
                foreach (var prop in filteredItem.Properties())
                {
                    item.Add(prop.Name, prop.Value);
                }

                // 新增需要的属性（示例：f_sync_time、f_status）

                item.Add("jnpfId", PublicMethod.GenerateGuid().ToLower());
                item.Add("f_organize_id", 11); // 同步时间
                item.Add("f_organize_before", 18); // 同步状态
                                                   // 可根据需求继续添加其他属性，如f_batch_id等
            }


            filteredBaseinfo.Add("tableField033730", listArray); // 示例值
            // 转换回JSON字符串（处理后结果）
            //string processedBaseinfo = filteredBaseinfo.ToString(Formatting.None);

            var data = new { id = "", data = JsonConvert.SerializeObject(filteredBaseinfo) };

            var aurl = Util.IntegBizSysAPIBaseUrl + "/api/visualdev/OnlineDev/681376809291350277";
            var result = HttpHelper.PostAsJson(aurl, data, headers);
            var ress = JsonConvert.DeserializeObject<SyncDataIntegBizSysResult>(result);
            if (ress.code != 200)
            {
                return JsonResult(false, ress.msg);
            }

            return JsonResult(true, "");
        }
        public JsonResult GetDDLSheng(HttpRequest Request)
        {
            string DDLShi = Request["PN"];
            string re = string.Empty;
            List<Combobox> reault = new List<Combobox>();

            string shiareaid = ZWL.DBUtility.DbHelperSQL.GetSHSL("SELECT AreaID FROM ERPArea WHERE AreaName = '" + DDLShi + "'");
            if (DDLShi == "initProvince")
            {
                shiareaid = "0";
            }
            if (!string.IsNullOrEmpty(shiareaid))
            {
                string sql = "SELECT AreaName FROM ERPArea WHERE ParentID = " + shiareaid;
                DataTable table = ZWL.DBUtility.DbHelperSQL.GetDataTable(sql);

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        Combobox cb = new Combobox();
                        cb.text = dr["AreaName"].ToString();
                        reault.Add(cb);
                    }
                }
            }
            return JsonResult(true, reault);
        }
        public JsonResult GetDDLXian(HttpRequest Request)
        {
            return GetDDLSheng(Request);
        }

        public JsonResult CheckXMQQName(HttpRequest Request)
        {
            string result;
            var xmName = Request["name"];
            xmName = HttpUtility.UrlDecode(xmName);
            var r = DbHelperSQL.GetSHSLInt1(string.Format(@"select count(*) from [ERPXMQQDJ] where [XMName] like '%{0}%'", xmName.Trim()));
            if (r > 0)
            {
                result = "true";
            }
            else
            {
                var dt = DbHelperSQL.GetDataTable("select XMName from [ERPXMQQDJ] order by XMBH desc");
                foreach (DataRow dr in dt.Rows)
                {
                    var des = dr["XMName"].ToString();
                    var temt = PublicMethod.GetSimilWidth(xmName, des);
                    if (temt > 0.6)//
                    {
                        result = "" + des;
                    }
                }
                result = "false";
            }
            return JsonResult(true, "", result);
        }
        public JsonResult CheckXMCJJCPGName(HttpRequest Request)
        {
            string result;
            var xmName = Request["name"];
            xmName = HttpUtility.UrlDecode(xmName);
            var r = DbHelperSQL.GetSHSLInt1(@"select count(*) from ERPXMCJJCPG p join ERPNWorkToDo d on p.NWorkID=d.ID 
                    where d.StateNow not in ('已被驳回','不通过') and XMName like '%{0}%'".FormatWith(xmName.Trim()));
            if (r > 0)
            {
                result = "true";
            }
            else
            {
                var dt = DbHelperSQL.GetDataTable(@"select XMName from ERPXMCJJCPG p join ERPNWorkToDo d on p.NWorkID=d.ID where d.StateNow not in ('已被驳回','不通过') order by XMBH desc");
                foreach (DataRow dr in dt.Rows)
                {
                    var des = dr["XMName"].ToString();
                    var temt = PublicMethod.GetSimilWidth(xmName, des);
                    if (temt > 0.6)//
                    {
                        result = "" + des;
                    }
                }
                result = "false";
            }
            return JsonResult(true, "", result);
        }

        public JsonResult CheckXMJBXXName(HttpRequest Request)
        {
            string result;
            var xmName = Request["name"];
            xmName = HttpUtility.UrlDecode(xmName);
            var r = DbHelperSQL.GetSHSLInt1(string.Format(@"select count(*) from [ERPXMJBXX] where [XMName] = '{0}'", xmName.Trim()));
            if (r > 0)
            {
                result = "true";
            }
            else
            {
                result = "false";
            }
            return JsonResult(true, "", result);
        }

        public JsonResult ProjectCostFilter(HttpRequest Request)
        {
            var keyword = HttpUtility.UrlDecode(Request["keyword"]);
            var itemname = HttpUtility.UrlDecode(Request["Item"]);
            var sqlwhere = PublicMethod.GetLimitDataSqlWhere("CostDetailPostList");
            var sql = @"select top 10 p.ID
                      ,x.XMName
                      ,x.XMBH
                      ,x.DJTime
                      ,p.HTBH
                      ,x.XMState
                      ,x.ZYLB
                      ,x.XMBM
                      ,x.XMFZR
                      ,x.XMBeginTime
                      ,x.XMEndTime
                      ,(case when p.HTJE is not null and p.HTJE>0 then p.HTJE ELSE (case when (p.XMJF is null or p.XMJF<0) then x.XMJF ELSE p.XMJF end) end)  Amount --合同金额
                      ,isnull(((SELECT sum(KaiPiaoJE) KaiPiaoJE from ERPHeTongShouKuan h join ERPNWorkToDo d on h.NWorkToDoID=d.ID and StateNow not in ('已被驳回','不通过')
		where h.HTBH=p.HTBH)),0)  SettleAmt --结算金额
                      ,isnull(
							    (
								    CASE WHEN p.HTBH is null or p.HTBH='' THEN 
									    (
											    select sum(ISNULL(DaoZhangJE,0)) from (
												    select HTBH,DaoZhangJE from ERPHeTongDaoZhang a join ERPNWorkToDo d on a.NWorkToDoID=d.ID where d.StateNow not in ('已被驳回','不通过')
												    UNION ALL
												    select HTBH,Amount DaoZhangJE from ERPHeTongYuShouKuan y where Flag<>0 and ConnectID is null
												    ) t  where HTBH in (

												    select HTID from ERPHeTong a join ERPNWorkToDo d on a.NWorkToDoID=d.ID where d.StateNow not in ('已被驳回','不通过') and HTLB='收款'
												    and XMID=p.XMBH
												    )
									    )
									    ELSE 
									    (
											    select sum(ISNULL(DaoZhangJE,0)) from (
											    select DaoZhangJE from ERPHeTongDaoZhang a join ERPNWorkToDo d on a.NWorkToDoID=d.ID where d.StateNow not in ('已被驳回','不通过') and a.HTBH=p.HTBH
											    UNION ALL
											    select Amount DaoZhangJE from ERPHeTongYuShouKuan y where Flag<>0 and ConnectID is null and HTBH=p.HTBH
											    ) h
									    )
								    END
                        )												
						,0) ReceivedAmt  --收款金额
                      ,(select  ISNULL(sum({1}), 0) from ERPCostDetail d WHERE d.ParentId=p.ID) CostedAmt  --本类别已支出
                      ,({2}) BudgetAmt
                      ,(SELECT CostSum from (
                                    select  ParentId,sum(工资及津贴 +节日补贴+养老统筹 +福利费 +劳动保护费 +住房公积金 +住房补贴 +材料费 +工程出包费 +固定资产+办公费 +差旅费 +水电费 +物业管理费 +交通运输费用 +邮电费用 +维修费用 +会议费 +培训费 +业务招待费 +劳务费 +租赁费 +税金及附加 +安全生产费用 +工会经费+ 印刷费 +其它费用) CostSum from ERPCostDetail group by ParentId
                                    ) a where ParentId=p.ID) CostedSum --已累计支出金额
                        from ERPProjectCost p JOIN ERPXMJBXX x on p.XMBH=x.XMBH
                        left join (select * from ERPHTJieSuan where ID in (
																		select ID from (
																		select HTBH,max(ID) ID from (
																		select h.* from ERPHTJieSuan h join ERPNWorkToDo d on h.NWorkToDoID=d.ID and StateNow not in ('已被驳回','不通过')
																		UNION
																		select * from ERPHTJieSuan h where NWorkToDoID is null
																		) t where HTBH is not null and HTBH<>'' GROUP BY HTBH
																		) t
																		)) s 
                        on p.XMBH=s.beiyong1 and p.HTBH=s.HTBH 
                where x.XMName like '%{0}%' or x.XMBH like '%{0}%' or p.HTBH like '%{0}%'".FormatWith(keyword, itemname,
                (BudgetItems.Contains(itemname) ? @"(select top 1 ISNULL({0}, 0) from ERPBudgetDetail b join ERPProjectCost c on b.ParentId=c.ID where c.ID=p.ID ORDER BY Version DESC)".FormatWith(itemname) : "0")
                );
            var dt = DbHelperSQL.GetDataTable(sql);
            var data = ConvertDataTableToList(dt);
            return JsonResult(true, "", data);
        }

        public JsonResult ProjectCostItemBudget(HttpRequest Request)
        {
            var recordid = PublicMethod.GetInt(HttpUtility.UrlDecode(Request["RecordId"]));
            var item = HttpUtility.UrlDecode(Request["Item"]);
            var submitamt = PublicMethod.GetDecimal(HttpUtility.UrlDecode(Request["SubmitAmt"]));
            decimal costbudget = 0;//单项预算
            decimal singleCostSums = 0;//单项支出
            decimal costSums = 0;//总支出
            decimal htje = 0;//合同金额
            var sql = @"select top 1 ISNULL({0}, 0) from ERPBudgetDetail b join ERPProjectCost c 
                        on b.ParentId=c.ID where c.ID={1} ORDER BY Version DESC".FormatWith(item, recordid);
            if (BudgetItems.Contains(item))
            {
                var budget = DbHelperSQL.GetSingle(sql);
                if (budget != null)
                {
                    costbudget = PublicMethod.GetDecimal(budget.ToString());
                }
            }
            var ssql = @"select  ISNULL(sum({1}), 0) from ERPCostDetail d join ERPProjectCost c on d.ParentId=c.ID and c.ID={0}".FormatWith(recordid, item);
            var scostsums = DbHelperSQL.GetSingle(ssql);
            if (scostsums != null)
            {
                singleCostSums = PublicMethod.GetDecimal(scostsums.ToString());
            }
            var projectCost = new ZWL.BLL.ERPProjectCost();
            var ptablesql = projectCost.GetFinancialSql("pc.ID={0}".FormatWith(recordid)).ToString();
            var psql = @"select isnull(sum(HTJE),0) HTJE,isnull(sum(HTJE2),0) HTJE2,isnull(sum(结算金额),0) 结算金额,isnull(sum(开票金额),0) 开票金额,isnull(sum(收款金额),0) 收款金额,isnull(sum(CostSums),0) CostSums 
                        from ({0}) as newtable".FormatWith(ptablesql);
            var row = DbHelperSQL.GetDataRow(psql);
            if (row != null)
            {
                costSums = PublicMethod.GetDecimal((row["CostSums"] == null || row["CostSums"] is System.DBNull) ? "0" : row["CostSums"].ToString());
            }
            var hsql = @"select top 1 p.ID
                      ,XMName
                      ,XMBH
                      ,DJTime
                      ,p.HTBH
                      ,XMState
                      ,ZYLB
                      ,XMBM
                      ,XMFZR
                      ,XMBeginTime
                      ,XMEndTime
                      ,(case when s.JSJE is not null then s.JSJE when p.JSJE>0 then p.JSJE when HTJE>0 then HTJE else XMJF end)  HTJE
                      ,isnull((select sum(ISNULL(h.DaoZhangJE,0)) from ERPHeTongDaoZhang h join ERPNWorkToDo d on h.NWorkToDoID=d.ID where StateNow not in ('已被驳回','不通过') and h.HTBH=p.HTBH),0) JSJE
                      ,CostMoneySUM
                      ,XMJF from ERPProjectCost p 
                        left join ((select isnull(sum(JSJE),0.00) JSJE,beiyong1,HTBH from ERPHTJieSuan where beiyong1 is not null group by beiyong1,HTBH)) s 
                        on p.XMBH=s.beiyong1 and p.HTBH=s.HTBH 
                where p.ID={0}".FormatWith(recordid);
            var hModel = Conv<ZWL.BLL.ERPProjectCost>.GetModel(hsql);
            if (hModel != null)
            {
                //htje = hModel.HTJE;
                htje = DataHelper.GetProjectCostJieSuanAmt(hModel.ID);
            }
            var ItemScale = costbudget > 0 ? PublicMethod.GetDecimal(((submitamt + singleCostSums) * 100 / costbudget).ToString()) : 0;
            var CostScale = costSums > 0 && htje > 0 ? PublicMethod.GetDecimal((costSums * 100 / htje).ToString()) : 0;
            return JsonResult(true, "", new { TotalAmt = costSums + submitamt, CostedAmt = singleCostSums, BudgetAmt = costbudget, ItemScale = ItemScale, CostScale = CostScale });
        }
        public JsonResult SupplierFilter(HttpRequest Request)
        {
            var keyword = HttpUtility.UrlDecode(Request["keyword"]);
            var list = Conv<ZWL.BLL.ERPSubcontractTeam>.GetList("select top 10 * from ERPSubcontractTeam where IsDeleted=0 and FBDWMC like '%{0}%'".FormatWith(keyword));
            return JsonResult(true, "", list);
        }
        public JsonResult PrintProjectCostDetailSubmitReport(HttpRequest Request)
        {
            var id = PublicMethod.GetInt(Request["id"]);
            var result = CanPrint(id);
            if (!result)
            {
                return JsonResult(false, "此报销单未达打印要求，请确认！");
            }
            var filename = string.Empty;
            var info = new ZWL.BLL.ERPCostDetailPost();
            info.GetModel(PublicMethod.GetInt(id));
            if (info.ID > 0)
            {
                if (!new List<string> { "已提交", "已完成" }.Contains(info.State))
                {
                    SubmitData(PublicMethod.GetInt(id));
                }
                info.GetModel(PublicMethod.GetInt(id));
                var newVer = false;
                var tempfilename = "项目费用成本报销统计表v1.doc";
                var dt2 = GetSummaryDataTable(info.ID);
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    newVer = true;
                    tempfilename = "项目费用成本报销统计表v2.doc";
                }
                var tfilename = "ProjectCostDetailSubmitReport_{0}.html".FormatWith(DateTime.Now.ToString("yyyyMMddHHmmss"));
                var tempfilepath = Path.Combine(HostingEnvironment.MapPath("~/ReportFile"), tempfilename);
                var tagetfilepath = Path.Combine(Path.Combine(HostingEnvironment.MapPath("~/UploadFile"), "DocumentPreview"), tfilename);

                var dt = DataTableHelper.ConvertTo<ZWL.BLL.ERPCostDetailPost>(new List<ZWL.BLL.ERPCostDetailPost> { info });
                var dic = DataTableToDicList(dt).FirstOrDefault();
                var selecteddic = dic.FirstOrDefault(r => r.Key == "DJR");
                dic.Remove(selecteddic.Key);
                var aspose = new AsposeWordHelper();
                aspose.OpenTempelte(tempfilepath);
                aspose.ExecuteField(dic.Keys.ToArray(), dic.Values.ToArray());

                decimal amt = 0;
                var dataTable = DataTableHelper.ConvertTo<ZWL.BLL.ERPCostDetailPostItems>(info.SubItems);
                var itemsdt = dataTable.Clone();
                itemsdt.Columns.Remove("CostScale");
                var costScale = new DataColumn();
                costScale.DataType = typeof(string);
                costScale.ColumnName = "CostScale"; // 可以与原列名相同或不同
                itemsdt.Columns.Add(costScale);
                var list = info.SubItems.GroupBy(r => r.RecordId);
                if (list != null && list.Any())
                {
                    foreach (var xitem in list)
                    {
                        for (int i = 0; i < xitem.Count(); i++)
                        {
                            var item = xitem.ElementAt(i);
                            var subdt = DataTableHelper.ConvertTo<ZWL.BLL.ERPCostDetailPostItems>(new List<ZWL.BLL.ERPCostDetailPostItems> { item });
                            if (i > 0)
                            {
                                var row = subdt.Rows[0];
                                row["XMName"] = DBNull.Value;
                                row["HTBH"] = DBNull.Value;
                                row["Amount"] = DBNull.Value;
                                row["SettleAmt"] = DBNull.Value;
                                row["ReceivedAmt"] = DBNull.Value;
                                if (newVer)
                                    row["TotalAmt"] = DBNull.Value;
                            }
                            DataRow newRow = itemsdt.NewRow();
                            for (int x = 0; x < subdt.Columns.Count - 1; x++) // 跳过最后一个字段
                            {
                                var col = subdt.Columns[x].ColumnName;
                                if (col == "CostScale")
                                {
                                    newRow[col] = (!item.CostScale.HasValue || item.CostScale.HasValue && item.CostScale == 0) ? "N/A" : PublicMethod.FormatMoney(item.CostScale); // 手动转换
                                }
                                else
                                {
                                    newRow[col] = subdt.Rows[0][col];
                                }
                            }
                            //newRow[itemsdt.Columns.Count - 1] = (!item.CostScale.HasValue || item.CostScale.HasValue && item.CostScale == 0) ? "N/A" : PublicMethod.FormatMoney(item.CostScale); // 手动转换
                            itemsdt.Rows.Add(newRow);

                            //itemsdt.Rows.Add(subdt.Rows[0].ItemArray);
                            amt += item.SubmitAmt;
                        }
                    }
                }
                itemsdt.Columns.Add(new DataColumn("RowNumber", typeof(int)));
                ResetRowID(itemsdt);
                var amtitem = itemsdt.NewRow();
                amtitem["XMName"] = "合计";
                amtitem["SubmitAmt"] = amt;
                itemsdt.Rows.Add(amtitem);
                itemsdt.AcceptChanges();
                itemsdt.TableName = "childrenDataTable2";
                aspose.WriteTable(itemsdt);

                if (newVer)
                {

                    // 2. 获取动态列的汇总数据 (dt2)
                    // 假设这个 DataTable 是您动态生成的，列数不固定
                    dt2 = ProcessReportTable(dt2);
                    var doc = aspose.Doc;
                    // 3. 定位到模板中的书签位置
                    if (doc.Range.Bookmarks["DynamicSummaryTable"] != null)
                    {
                        var builder = aspose.GetDocumentBuilder();
                        builder.MoveToBookmark("DynamicSummaryTable");
                        // --- 表格样式设置准备 ---
                        // --- 开始动态构建表格 ---
                        var table = builder.StartTable();
                        // 设置全局字体样式（可选）
                        builder.Font.Size = 8;
                        builder.Font.Name = "宋体";
                        builder.RowFormat.Height = ConvertUtil.PixelToPoint(25);
                        // A. 构建表头 (根据 dt2 的列名)
                        foreach (DataColumn col in dt2.Columns)
                        {
                            builder.InsertCell();
                            // 设置样式 (可选)
                            builder.CellFormat.Borders.LineStyle = Aspose.Words.LineStyle.Single;
                            builder.CellFormat.Width = 80; // 您可以根据页面宽度动态计算每列宽度
                            builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
                            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                            builder.Write(col.ColumnName);
                        }
                        builder.EndRow();

                        builder.Font.Size = 8;
                        // B. 构建数据行
                        foreach (DataRow row in dt2.Rows)
                        {
                            builder.RowFormat.Height = ConvertUtil.PixelToPoint(25);
                            for (int i = 0; i < row.ItemArray.Length; i++)
                            {
                                var item = row.ItemArray[i];
                                builder.InsertCell();
                                builder.CellFormat.Borders.LineStyle = Aspose.Words.LineStyle.Single;
                                builder.ParagraphFormat.Alignment = (i == 0) ? ParagraphAlignment.Center : ParagraphAlignment.Right;
                                builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
                                builder.Write((i == 0) ? item.ToString() : PublicMethod.FormatMoney(PublicMethod.GetDecimal(item)));
                            }
                            builder.EndRow();
                        }

                        builder.EndTable();
                        // --- 表格构建结束 ---

                        // ==================
                        // [需求1] 边框特殊处理：移除外边框，保留内边框
                        // ==================
                        // 先清除现有边框设置（可选，为了保险）
                        // table.ClearBorders(); 

                        // 重新设置内部边框为实线
                        table.SetBorder(BorderType.Horizontal, Aspose.Words.LineStyle.Single, 1.0, Color.Black, true);
                        table.SetBorder(BorderType.Vertical, Aspose.Words.LineStyle.Single, 1.0, Color.Black, true);

                        // 显式将外围边框设置为“无”
                        table.SetBorder(BorderType.Left, Aspose.Words.LineStyle.None, 0, Color.Empty, true);
                        table.SetBorder(BorderType.Right, Aspose.Words.LineStyle.None, 0, Color.Empty, true);
                        table.SetBorder(BorderType.Top, Aspose.Words.LineStyle.None, 0, Color.Empty, true);
                        table.SetBorder(BorderType.Bottom, Aspose.Words.LineStyle.None, 0, Color.Empty, true);

                        // 清理书签内容（如果书签本身占了位置）
                        doc.Range.Bookmarks["DynamicSummaryTable"].Text = "";
                    }

                }
                aspose.Save(tagetfilepath, Aspose.Words.SaveFormat.HtmlFixed);
                aspose.Save(tagetfilepath.Substring(0, tagetfilepath.LastIndexOf('.') + 1) + Aspose.Words.SaveFormat.Pdf.ToString(), Aspose.Words.SaveFormat.Pdf);
                //var baseUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf(Request.RawUrl)) + Request.ApplicationPath;
                //var url = "{0}/CommonSelect/PrintHelper.aspx?filename={1}".FormatWith(baseUrl, HttpUtility.UrlEncode(Path.GetFileNameWithoutExtension(tfilename)));
                //var dosomething = "addTab_parent('{0}','{1}');"
                //    .FormatWith(url, PublicMethod.ShortenText(Path.GetFileNameWithoutExtension(tfilename), 10));
                //MessageBox.ResponseScript(this, dosomething);
                filename = Path.GetFileNameWithoutExtension(tfilename);
            }
            return JsonResult(true, "", filename);
        }
        // 模拟 dt2 数据结构构建
        private DataTable GetSummaryDataTable(int pid)
        {
            DataTable dt = new DataTable("SummaryDT"); // 必须与模板中的 TableStart 名称一致
            var oldsql = @"select ItemName 类别,工资及津贴,工程出包费,材料费,租赁费,劳务费,安全生产费用,办公费,维修费用,交通运输费用,差旅费,邮电费用,其它费用,水电费,会议费,印刷费,节日补贴,养老统筹,福利费,劳动保护费,住房公积金,住房补贴,固定资产,物业管理费,培训费,业务招待费,工会经费,Total 合计 from ERPCostDetailPostSum where ParentId={0}".FormatWith(pid);
            dt = DbHelperSQL.GetDataTable(oldsql);
            //var oldList = Conv<ZWL.BLL.ERPCostDetailPostSum>.GetList(oldsql);
            //// 定义列，名称必须与模板中的域代码一致
            //dt.Columns.Add("类别");    // 行标题：支出合计、预算、比例
            //dt.Columns.Add("工资及津贴");      // 工资及津贴
            //dt.Columns.Add("材料费");   // 材料费
            //dt.Columns.Add("工程出包费");    // 工程出包费
            //dt.Columns.Add("差旅费");     // 差旅费
            //dt.Columns.Add("交通运输费");  // 交通运输费
            //dt.Columns.Add("劳务费");      // 劳务费
            //dt.Columns.Add("租赁费");       // 租赁费
            //dt.Columns.Add("印刷费");      // 印刷费
            //dt.Columns.Add("其它费用");      // 其它费用
            //dt.Columns.Add("其它费用1");      // 其它费用
            //dt.Columns.Add("合计");      // 合计

            //// 添加 "支出合计" 行
            //dt.Rows.Add("支出合计", "6,869,048.70", "87,840.00", "338,621.00", "2,380,681.70", "712,100.00", "523,488.00", "2,640,360.00", "60,000.00", "51,750.00", "74,208.00", "74,208.00");

            //// 添加 "预算" 行
            //dt.Rows.Add("预算", "2,764,400.20", "103,500.00", "340,900.00", "2,564,400.00", "825,600.00", "588,000.00", "2,874,000.00", "60,000.00", "68,000.00", "75,600.00", "75,600.00");

            //// 添加 "支出比例" 行
            //dt.Rows.Add("支出比例(%)", "91.59", "84.87", "99.33", "92.84", "86.25", "89.03", "91.87", "100.00", "76.10", "98.16", "98.16");

            return dt;
        }
        public static DataTable ProcessReportTable(DataTable sourceTable)
        {
            // 1. 筛选出需要的行：只需要 "支出比例(%)" 这一行。
            //    我们使用 LINQ to DataSet 来查找该行。
            DataRow proportionRow = sourceTable.AsEnumerable()
                .FirstOrDefault(row => row.Field<string>("类别") == "支出比例(%)");

            if (proportionRow == null)
            {
                Console.WriteLine("未找到 '支出比例(%)' 行。");
                return null; // 或者返回一个空的 DataTable
            }

            // 2. 确定需要保留的列。
            //    遍历原始 DataTable 的所有列，检查 "支出合计" 和 "预算" 行在该列的值是否都为 0。

            // 找到 "支出合计" 和 "预算" 行，以便后续检查列值。
            DataRow totalRow = sourceTable.AsEnumerable()
                .FirstOrDefault(row => row.Field<string>("类别") == "支出合计");
            DataRow budgetRow = sourceTable.AsEnumerable()
                .FirstOrDefault(row => row.Field<string>("类别") == "预算");

            List<string> columnsToKeep = new List<string>();

            // 总是保留关键识别列
            columnsToKeep.Add("类别");

            // 遍历所有可能的费用列
            foreach (DataColumn col in sourceTable.Columns)
            {
                // 排除我们已经确定要保留的 ID、Name 等元数据列，只检查具体的费用列
                if (columnsToKeep.Contains(col.ColumnName))
                {
                    continue;
                }

                // 尝试获取该列在“支出合计”和“预算”行的值
                // 假设这些费用列的数据类型是 Decimal 或 Double，这里使用通用的 object
                object totalValueObj = totalRow[col.ColumnName];
                object budgetValueObj = budgetRow[col.ColumnName];

                decimal totalValue = 0;
                decimal budgetValue = 0;

                // 安全地转换值为 Decimal
                if (totalValueObj != DBNull.Value && totalValueObj != null)
                {
                    decimal.TryParse(totalValueObj.ToString(), out totalValue);
                }
                if (budgetValueObj != DBNull.Value && budgetValueObj != null)
                {
                    decimal.TryParse(budgetValueObj.ToString(), out budgetValue);
                }

                // 如果两个值不同时为 0，则保留此列
                if (totalValue != 0 || budgetValue != 0)
                {
                    columnsToKeep.Add(col.ColumnName);
                }
            }

            // 3. 创建一个新的 DataTable 结构（只有需要保留的列）
            DataTable resultTable = new DataTable("FilteredCostProportion");
            foreach (string colName in columnsToKeep)
            {
                // 使用原始列的类型来创建新列
                resultTable.Columns.Add(colName, sourceTable.Columns[colName].DataType);
            }

            // 4. 将筛选出的 "支出比例(%)" 行的数据复制到新的 DataTable 中
            DataRow newRowTotal = resultTable.NewRow();
            DataRow newRowBud = resultTable.NewRow();
            DataRow newRowPer = resultTable.NewRow();
            foreach (string colName in columnsToKeep)
            {
                newRowTotal[colName] = totalRow[colName];
                newRowBud[colName] = budgetRow[colName];
                newRowPer[colName] = proportionRow[colName];
            }
            resultTable.Rows.Add(newRowTotal);
            resultTable.Rows.Add(newRowBud);
            resultTable.Rows.Add(newRowPer);

            return resultTable;
        }
        public JsonResult PreviewProjectQuanLiuChengDetailReport(HttpRequest Request)
        {
            var formdata = Request["formData"];
            var item = GetListFromDataByName(formdata, "Item").FirstOrDefault();
            var file = GetListFromDataByName(formdata, "file").FirstOrDefault();
            if (file == null) file = Request["file"];
            var sqlWhere = PreviewProjectQuanLiuChengDetailReportSqlWhere(Request);
            var filename = string.Empty;
            if (item == "明细")
            {
                var strSql = @"SELECT  * FROM ERPXMQLCReport 
                            where LotID=(select top 1 LotID from Flow where DataTable='ERPXMQLCReport' ORDER BY ID DESC) 
                            and XMID in (select t.XMBH from ERPXMJBXXExtend t join ERPXMJBXX x on t.XMBH=x.XMBH join ERPNWorkToDo d on x.NWorkID=d.ID {0})  
                            ORDER BY OrderBy";
                strSql = strSql.FormatWith(sqlWhere);
                var dt = DbHelperSQL.GetDataTable(strSql);
                var strmodelname = "项目全流程情况统计表";
                //如果模板文件存在
                var strmodelfilepath = HostingEnvironment.MapPath("~/ReportFile/") + strmodelname + ".xlsx";
                string sourcefile = strmodelfilepath;
                string datestring = DateTime.Now.ToString("yyyyMMddHHmmsss");
                string destfile = HostingEnvironment.MapPath("~/ReportFile/") + strmodelname + datestring + ".xlsx";
                string destfilefullname = strmodelname + datestring + ".xlsx";
                try
                {
                    XMLifeCircleoutput(dt, sourcefile, destfile);
                    filename = Path.GetFileNameWithoutExtension(destfile);
                }
                catch (Exception ex)
                {
                    string strex = ex.Message.ToString();
                    Logger.SetMode(LogMode.Web);
                    Logger.Log(LogType.Exception, ex.Message);
                }
                if (!destfile.IsNullOrEmpty() && file == "htm")
                {
                    var uploadrootdir = PublicMethod.UploadFileFolderTruePath;
                    filename = Path.GetFileNameWithoutExtension(destfile);
                    using (var workBook = new Aspose.Cells.Workbook(destfile))
                    {
                        workBook.Save(Path.Combine(Path.Combine(uploadrootdir, "DocumentPreview"), filename), Aspose.Cells.SaveFormat.Html);
                    }
                }

            }
            else if (item == "列表")
            {
                var dic = new Dictionary<string, string>();
                dic.Add("row", "序号");
                dic.Add("Number", "项目编号");
                dic.Add("Name", "项目名称");
                dic.Add("XMJF", "项目金额");
                dic.Add("YiSJE", "已收金额");
                dic.Add("YingSJE", "应收金额");
                dic.Add("YiFJE", "已付金额");
                dic.Add("YingFJE", "应付金额");
                dic.Add("XMYWLXR", "业务联系人");
                dic.Add("ISWG", "是否完工");
                dic.Add("SFYJF", "是否有纠纷");
                dic.Add("TimeStr", "登记日期");
                dic.Add("XMBM", "承接部门");
                dic.Add("UserName", "登记人");
                var sql = ChargemanDataSqlFormat.FormatWith(sqlWhere);
                var pager = new Pager(sql, 1, 1);
                var tempfilename = DataToExcel.GridViewToExcelOrderByNameListReturnFileName(DbHelperSQL.GetDataSet(pager.SQL), dic, "项目应收应付汇总excel报表", ".xlsx");
                var rootpath = Path.Combine(PublicMethod.UploadFileFolderTruePath, "DocumentPreview");
                string destfile = Path.Combine(rootpath, tempfilename);
                filename = Path.GetFileNameWithoutExtension(destfile);
                if (!destfile.IsNullOrEmpty() && file == "htm")
                {
                    var uploadrootdir = PublicMethod.UploadFileFolderTruePath;
                    using (var workBook = new Aspose.Cells.Workbook(destfile))
                    {
                        workBook.Save(Path.Combine(Path.Combine(uploadrootdir, "DocumentPreview"), filename), Aspose.Cells.SaveFormat.Html);
                    }
                }
            }

            return JsonResult(true, "", filename);
        }
        public JsonResult ProjectArchiveList(HttpRequest Request)
        {
            var code = false;
            var msg = string.Empty;
            if (ValidateInput(Request, ref msg))
            {
                var sqlWhere = @"select g.*,XMQQBH
      ,HTBH
      ,XMState
      ,XMAdress
      ,WTDWName
      ,WTDWLXR
      ,WTDWLXDH
      ,HZDWName
      ,HZDWLXR
      ,HZDWLXDH
      ,WTFS
      ,ZYLB
      ,HYLB
      ,XMZJLY
      ,XMJF
      ,XMBeginTime
      ,XMEndTime
      ,XMBM
      ,ZKS
      ,ZKJC
      ,PGDJ
      ,PGMJ
      ,KCDJ
      ,BXCLDJ
      ,DCMJ
      ,TC
      ,KT
      ,ZT
      ,ZYLBMain
      ,SHState
      ,XMReport
      ,SHTime
      ,LNG
      ,LAT
      ,AUTOSYSPROJECTID
      ,HTState
      ,XMYWLXR
      ,JFDWXZ
      ,WGTime
      ,ISWG
      ,LYQK
      ,SFYJF
      ,QPJE
      ,YQQPJE
      ,SFYJWHZ
      ,CSCS from ERPXMZLGuiDang g JOIN ERPXMJBXX x on g.XMBH=x.XMBH where g.ID in (
select MAX(g.ID) NWorkID from ERPXMZLGuiDang g JOIN ERPNWorkToDo d on g.NWorkID=d.ID
where StateNow not in ('已被驳回','不通过') and DAH is not null and DAH<>'' {0}
GROUP BY DAH,XMBH
)";
                var cpage = 1; var psize = 20;
                var pSizeList = new List<int> { 20, 50 };
                var cpagearg = PublicMethod.GetInto(Request["pageIndex"]);
                var psizearg = PublicMethod.GetInto(Request["pageSize"]);
                var keyWord = Request["keyWord"];
                if (cpagearg > 0)
                {
                    cpage = cpagearg;
                }
                if (psizearg > 0 && pSizeList.Contains(psizearg))
                {
                    psize = psizearg;
                }
                var sqlWhereFormat = string.Empty;
                if (!keyWord.IsNullOrEmpty())
                    sqlWhereFormat = "and (DAH like '%{0}%' or g.XMName='%{0}%' or XMBH like '%{0}%' or ReportBH like '%{0}%')".FormatWith(PublicMethod.UrlDecode(keyWord));
                sqlWhere = sqlWhere.FormatWith(sqlWhereFormat);
                var pager = new Pager(sqlWhere, cpage, psize, "DJTime");
                if (pager.ExecuteToDataTable())
                {
                    var dt = (DataTable)pager.Result;
                    var list = DataTableHelper.ConvertTo<ERPXMZLGuiDangArchive>(dt);
                    if (list != null && list.Any())
                    {
                        var xmlist = DataTableHelper.ConvertTo<ZWL.BLL.ERPXMJBXX>(dt);
                        foreach (var item in list)
                        {
                            item.XMItem = xmlist.FirstOrDefault(r => r.XMBH == item.XMBH);
                        }
                    }
                    var data = new XMZLGuiDangArchive
                    {
                        List = list,
                        Pagination = new Pagination
                        {
                            PageIndex = cpage,
                            PageSize = psize,
                            PageSum = pager.TotalPage,
                            Total = pager.Rows
                        }
                    };
                    return JsonResult(true, data);
                }
            }
            return JsonResult(code, msg, "");
        }
        public JsonResult ProjectArchiveXlsxToHtml(HttpRequest Request)
        {
            var filename = Request["filename"];
            var tempfilepath = Path.Combine(PublicMethod.UploadFileFolderTruePath, filename);
            var tfilename = DateTime.Now.Ticks.ToString() + ".html";
            var tagetfilepath = Path.Combine(Path.Combine(PublicMethod.UploadFileFolderTruePath, "DocumentPreview"), tfilename);
            if (!File.Exists(tagetfilepath))
            {
                using (var workBook = new Aspose.Cells.Workbook(tempfilepath))
                {
                    // 创建HtmlSaveOptions对象
                    var options = new Aspose.Cells.HtmlSaveOptions();

                    // 设置PresenationPreference选项以获得更优雅的布局
                    options.PresentationPreference = true;

                    // 遍历所有工作表
                    foreach (Aspose.Cells.Worksheet sheet in workBook.Worksheets)
                    {
                        // 获取工作表的页面设置
                        Aspose.Cells.PageSetup pageSetup = sheet.PageSetup;

                        // 设置页面布局为横向
                        pageSetup.Orientation = Aspose.Cells.PageOrientationType.Landscape;
                    }

                    workBook.Save(tagetfilepath, options);
                }
            }
            return JsonResult(true, "", tfilename);
        }
        [HttpMethod(Action = HttpVerb.POST)]
        public JsonResult ProjectArchiveToTempDocToHtml(HttpRequest Request, string json)
        {
            var paras = JObject.Parse(json);
            var id = PublicMethod.GetInto(paras["ID"]);
            return PrintProjectArchiveDetailReport(id, false);
        }
        public JsonResult PrintProjectArchiveDetailReport(HttpRequest Request)
        {
            var id = PublicMethod.GetInt(PublicMethod.GetDecryptParam("id"));
            var result = CanPrintProjectArchived(id);
            if (!result)
            {
                return JsonResult(false, "此表单尚在审核中，未赋档案号无法打印，请确认！");
            }

            return PrintProjectArchiveDetailReport(id);
        }
        private JsonResult PrintProjectArchiveDetailReport(int id, bool rename = true)
        {
            var filename = string.Empty;
            var infoModel = new ZWL.BLL.ERPXMZLGuiDang();
            infoModel.GetModelByNWorkID(id);
            if (infoModel.ID > 0)
            {
                var todoModel = new ZWL.BLL.ERPNWorkToDo();
                todoModel.GetModel(id);
                var newname = DateTime.Now.Ticks.ToString();
                if (!rename)
                {
                    if (!todoModel.FuJianList.IsNullOrEmpty())
                    {
                        var flist = todoModel.FuJianList.Split('|');
                        if (flist.Length > 0)
                            newname = Path.GetFileNameWithoutExtension(flist[0]);
                    }
                }
                var pdffilename = "{0}.pdf".FormatWith(newname);
                var htmlfilename = "{0}.html".FormatWith(newname);
                var tempfilepath = Path.Combine(PublicMethod.UploadFileFolderTruePath, "638471320442416050.doc");
                var pdftagetpath = Path.Combine(Path.Combine(PublicMethod.UploadFileFolderTruePath, "DocumentPreview"), pdffilename);
                var htmltagetpath = Path.Combine(Path.Combine(PublicMethod.UploadFileFolderTruePath, "DocumentPreview"), htmlfilename);

                var zjzAmt = 0;
                var dzwjAmt = 0;
                var summarysql = @"SELECT 
                                        s.ID,CAST(Key3 as int) OrderBy, v.Key2 LeiBie, DaiHao, ZhiAmt, 
                                        ZhiHeHao, DianAmt, DianPanHao, QiZhiYeMa, Comment, NWorkID
                                        FROM ERPXMZLGuiDangSummary s 
                                        LEFT JOIN ERPKeyValue v on s.LeiBie=v.Key1 
                                        where NWorkID={0}
                                        ORDER BY CAST(Key3 as int)
                                        ".FormatWith(id);
                var summarydt = DbHelperSQL.GetDataTable(summarysql);
                if (summarydt != null && summarydt.Rows.Count > 0)
                {
                    zjzAmt = summarydt.AsEnumerable().Sum(x => x.Field<int>("ZhiAmt"));
                    dzwjAmt = summarydt.AsEnumerable().Sum(x => x.Field<int>("DianAmt"));
                }
                summarydt.TableName = "Summary";

                var dt = DataTableHelper.ConvertTo<ZWL.BLL.ERPXMZLGuiDang>(new List<ZWL.BLL.ERPXMZLGuiDang> { infoModel });
                var dic = DataTableToDicList(dt).FirstOrDefault();
                var userModel = new ZWL.BLL.ERPUser();
                userModel = userModel.GetModel("UserName='{0}'".FormatWith(infoModel.JBR));
                dic.Add("DJBM", userModel.Department);
                dic.Add("ZJZAmt", zjzAmt);
                dic.Add("DZWJAmt", dzwjAmt);

                var aspose = new AsposeWordHelper();
                aspose.OpenTempelte(tempfilepath);
                aspose.ExecuteField(dic.Keys.ToArray(), dic.Values.ToArray());
                //处理审批意见 start
                if (todoModel.StateNow == "正常结束")
                {
                    var todoLog = new ZWL.BLL.ERPNWorkToDoLog();
                    var todoLogList = todoLog.GetModelList("ParentId='{0}' and Operation='Approved' and Action='Agree'".FormatWith(id));
                    if (todoLogList.Any())
                    {
                        var selectItem = todoLogList.FirstOrDefault(x => x.ID == todoLogList.Max(r => r.ID));
                        aspose.ExecuteField("ZiLiaoShiYiJian", GetYiJianDivContent(selectItem.Description));
                        aspose.ExecuteField("ZiLiaoShiDate", TimeParser.GetFormatDateString(selectItem.TimeStamp));
                        aspose.ExecuteField("YSR", selectItem.UserName);
                        var imgVal = BitmapImageToByteArray(Path.Combine(HostingEnvironment.MapPath("~/UploadFile"), selectItem.YinZhangPath));
                        aspose.AddImageByStream("ZiLiaoShiSign", new MemoryStream(Convert.FromBase64String(imgVal)), 55, 30);
                    }
                }
                else
                {
                    aspose.ExecuteField("ZiLiaoShiYiJian", "");
                    aspose.ExecuteField("ZiLiaoShiDate", "");
                    aspose.ExecuteField("ZiLiaoShiSign", "");
                    aspose.ExecuteField("YSR", "");
                }
                //处理审批意见 end
                aspose.WriteTable(summarydt);

                var subitemSQL = @"select [ID],[DAH],RIGHT('000' + CAST(XuHao AS nvarchar(20)), 3) as [XuHao],[DAName],[DanWei],[Count],[BZ],[NWorkID],[LeiBie],[MiJi],[ZTXS],ROW_NUMBER() over(order by ID) RowNumber FROM [ERPXMZLGuiDangDetail]  where ID in (
                                        select t.ID from ERPXMZLGuiDangDetail t JOIN ERPNWorkToDo d on t.NWorkID=d.ID
                                        LEFT JOIN ERPXMZLGuiDang g on t.DAH=t.DAH and g.NWorkID=t.NWorkID
                                        where StateNow not in ('已被驳回','不通过') and g.NWorkID={0}
                                    )".FormatWith(id);
                var itemsdt = DbHelperSQL.GetDataTable(subitemSQL);
                itemsdt.TableName = "Detail";
                aspose.WriteTable(itemsdt);
                // 创建HtmlFixedSaveOptions实例
                var saveOptions = new Aspose.Words.Saving.HtmlFixedSaveOptions();

                // 设置CSS样式，这里将边框颜色改为红色
                saveOptions.ShowPageBorder = false;

                aspose.Doc.Save(htmltagetpath, saveOptions);
                //aspose.Save(htmltagetpath, Aspose.Words.SaveFormat.HtmlFixed);
                aspose.Save(pdftagetpath, Aspose.Words.SaveFormat.Pdf);
                filename = Path.GetFileNameWithoutExtension(newname);
            }
            return JsonResult(true, "", filename);
        }
        #region privite function
        private string BuildSafeOrderBy(string sort, string order)
        {
            // 验证字段名合法性
            //string[] allowedFields = { "id", "name", "price" };
            string[] sortFields = sort.Split(',');
            string[] orderFields = order.Split(',');

            List<string> orderClauses = new List<string>();

            for (int i = 0; i < sortFields.Length; i++)
            {
                string field = sortFields[i];
                string dir = (i < orderFields.Length) ? orderFields[i] : "asc";
                if (field.IsNullOrEmpty() || dir.IsNullOrEmpty()) continue;
                // 检查字段是否在允许列表中
                //if (Array.IndexOf(allowedFields, field) >= 0)
                //{
                orderClauses.Add(string.Format("[{0}] {1}", field,
                    dir.ToLower() == "desc" ? "DESC" : "ASC"));
                //}
            }

            return orderClauses.Count > 0 ?
                "ORDER BY " + string.Join(", ", orderClauses) : "";
        }
        private string GetYiJianDivContent(string input)
        {
            var content = string.Empty;
            if (!input.IsNullOrEmpty())
            {
                string pattern = "<div class=\"[^\"]*\">(.*?)</div>";

                var match = Regex.Match(input, pattern);

                if (match.Success)
                {
                    // 提取匹配的分组内容
                    content = match.Groups[1].Value;
                }
            }
            return content;
        }
        private string PreviewProjectQuanLiuChengDetailReportSqlWhere(HttpRequest Request)
        {
            var sqlWhere = string.Empty;
            var formdata = Request["formData"];
            var item = GetListFromDataByName(formdata, "Item").FirstOrDefault();
            var startDate = GetListFromDataByName(formdata, "TextBox_Start").FirstOrDefault();
            var endDate = GetListFromDataByName(formdata, "TextBox_End").FirstOrDefault();
            var xmName = GetListFromDataByName(formdata, "XMName").FirstOrDefault();
            var username = GetListFromDataByName(formdata, "UserName").FirstOrDefault();
            var dept = GetListFromDataByName(formdata, "Department").FirstOrDefault();
            #region sqlWhere
            if (!string.IsNullOrEmpty(xmName))
            {
                sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere) + " (x.XMBH like '%{0}%' or x.XMName like '%{0}%')".FormatWith(xmName);
            }
            if (!string.IsNullOrEmpty(username))
            {
                sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere) + " UserName like '%" + username + "%'";
            }
            if (!string.IsNullOrEmpty(dept))
            {
                var sbbm = GetSqlInString(GetBuMen(dept));
                sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere) + " XMBM in ({0})".FormatWith(sbbm);
            }

            if (!string.IsNullOrEmpty(startDate))
            {
                sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere) + " TimeStr >= '" + DateTime.Parse(startDate).Date + "'";
            }

            if (!string.IsNullOrEmpty(endDate))
            {
                sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere) + " TimeStr < '" + DateTime.Parse(endDate).Date.AddDays(1) + "'";
            }

            var strjiaose = PublicMethod.GetSessionValue("JiaoSe");
            if (strjiaose.Contains("单位领导") || strjiaose.Contains("总工程师办公室") ||
                    strjiaose.Contains("经营管理科") || strjiaose.Contains("中心办公室") ||
                    strjiaose.Contains("资料室") || strjiaose.Contains("超级管理员"))
            {

            }
            else
            {
                var limitSql = PublicMethod.GetLimitDataSqlWhere(54, "xmzhgl");
                limitSql = limitSql.Replace("ID in", "NWorkID in");
                sqlWhere += PublicMethod.GetSqlAndByWhere(sqlWhere, limitSql) + limitSql;
            }
            sqlWhere += PublicMethod.GetSqlKeywordAnd(sqlWhere) + " (StateNow='正在办理' or StateNow='正常结束') ";
            if (!sqlWhere.IsNullOrEmpty())
            {
                sqlWhere = PublicMethod.GetSqlKeywordAnd(sqlWhere) + sqlWhere;
            }
            #endregion
            return sqlWhere;
        }
        private bool CanPrint(object id)
        {
            var result = false;
            var info = new ZWL.BLL.ERPCostDetailPost();
            info.GetModel(PublicMethod.GetInto(id));
            if (info.ID > 0)
            {
                var list = new List<string> { "暂存", "已撤回", "已提交", "已完成" };
                if (PublicMethod.StrIFIn("|CostDetailPostListP|", PublicMethod.GetQuanXian()) && list.Contains(info.State))
                {
                    result = true;
                }
            }
            return result;
        }
        private bool CanPrintProjectArchived(object id)
        {
            var info = new ZWL.BLL.ERPNWorkToDo();
            info.GetModel(PublicMethod.GetInto(id));
            if (info.ID > 0 && info.StateNow == "正常结束")
            {
                return true;
            }
            return false;
        }
        private bool ValidateInput(HttpRequest Request, ref string msg)
        {
            var result = true;
            var token = Request["token"];
            if (token.IsNullOrEmpty())
            {
                msg = "拒绝访问。";
                return false;
            }
            var kval = new ZWL.BLL.ERPKeyValue();
            kval = kval.GetModel("Category='ProjectArchiveToken'");
            if (kval == null || kval.Value1.IsNullOrEmpty())
            {
                msg = "拒绝访问。";
                return false;
            }
            var klist = kval.Value1.Split(',');
            if (!klist.Contains(token))
            {
                msg = "未授权。";
                return false;
            }

            return result;
        }

        /// <summary>
        /// DataTable转DicList.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<Dictionary<string, object>> DataTableToDicList(DataTable dt)
        {
            var result = new List<Dictionary<string, object>>();
            if (dt.AsEnumerable().Count() > 0)
                return dt.AsEnumerable().Select(
                        row => dt.Columns.Cast<DataColumn>().ToDictionary(
                        column => column.ColumnName,
                        column => row[column])).ToList();
            else
            {
                var dic = new Dictionary<string, object>();
                foreach (DataColumn item in dt.Columns)
                {
                    dic.Add(item.ColumnName, DBNull.Value);
                }
                result.Add(dic);
            }
            return result;
        }

        /// <summary>
        /// 动态表单时间格式转换.
        /// </summary>
        /// <param name="diclist"></param>
        /// <returns></returns>
        private List<Dictionary<string, object>> DateConver(List<Dictionary<string, object>> diclist)
        {
            foreach (var item in diclist)
            {
                foreach (var dic in item.Keys)
                {
                    if (item[dic] is DateTime)
                    {
                        item[dic] = item[dic].ToString() + " ";
                    }
                }
            }

            return diclist;
        }
        /// <summary>
        /// 根据图片的路径解析成图片资源
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string BitmapImageToByteArray(string filePath)
        {
            var pic = "";
            try
            {
                if (File.Exists(filePath))
                {
                    byte[] byteArray = null;
                    byteArray = File.ReadAllBytes(filePath);
                    pic = Convert.ToBase64String(byteArray);
                }
            }
            catch { }
            return pic;
        }
        private void ResetRowID(DataTable dt)
        {
            int rowNumber = 1;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    row["RowNumber"] = rowNumber;
                    rowNumber++;
                }
            }
        }
        protected bool SubmitData(int id)
        {
            var result = false;
            var info = new ZWL.BLL.ERPCostDetailPost();
            info.GetModel(PublicMethod.GetInt(id));
            if (info.ID > 0)
            {
                var list = info.SubItems;
                foreach (var item in list)
                {
                    var pcost = new ZWL.BLL.ERPProjectCost();
                    pcost.GetModel(item.RecordId);
                    var maxqijian = 0;
                    var dlist = new ZWL.BLL.ERPCostDetail().GetListModelByParentId(pcost.ID);
                    if (dlist != null && dlist.Any())
                    {
                        foreach (var ditem in dlist)
                        {
                            var tnum = PublicMethod.GetInto(ditem.期间);
                            maxqijian = tnum > maxqijian ? tnum : maxqijian;
                        }
                    }
                    var dInfo = new ZWL.BLL.ERPCostDetail()
                    {
                        XMBH = pcost.XMBH,
                        HTBH = pcost.HTBH,
                        期间 = (maxqijian + 1).ToString(),
                        beiyong1 = item.Description,
                        beiyong2 = TimeParser.GetFormatTimeString(DateTime.Now),
                    };
                    dInfo = (ZWL.BLL.ERPCostDetail)PublicMethod.SetModelPropertyValueByName(dInfo, item.Item, item.SubmitAmt);
                    dInfo.ParentId = pcost.ID;
                    dInfo.ID = dInfo.Add();
                    FlowLog.AddLog(dInfo);
                    var dshots = FlowLog.EditShot(item);
                    item.RelativeId = dInfo.ID;
                    item.Update();
                    FlowLog.EditLog(dshots, item);
                }
                var ishots = FlowLog.EditShot(info);
                info.State = "已提交";
                result = info.Update();
                FlowLog.EditLog(ishots, info);
            }
            return result;
        }
        private List<string> BudgetItems
        {
            get
            {
                var sql = @"select name from syscolumns 
                    where id=(select max(id) from sysobjects where xtype='u' and name='ERPBudgetDetail')
                    and PATINDEX('%[a-z]%', LOWER(name)) <= 0";
                return DbHelperSQL.GetSingleCulumnList<string>(sql);
            }
        }
        private class Combobox
        {
            public string value { get; set; }

            public string text { get; set; }
        }

        private List<string> GetListFromDataByName(HttpRequest request, string name)
        {
            var list = new List<string>();
            var formdata = request["formdata"];
            if (!formdata.IsNullOrEmpty())
            {
                list = GetListFromDataByName(formdata, name);
            }
            return list;
        }
        private List<string> GetListFromDataByName(string formdata, string name)
        {
            var list = new List<string>();
            if (!formdata.IsNullOrEmpty())
            {
                foreach (var item in formdata.Split('&'))
                {
                    if (item.Contains("="))
                    {
                        var l = item.Split('=');
                        var n = l[0];
                        var v = l[1];
                        if (n == name)
                        {
                            list.Add(PublicMethod.UrlDecode(v));
                        }
                    }
                }
            }
            return list;
        }
        private string GetValueByName(HttpRequest request, string name)
        {
            var list = GetListFromDataByName(request, name);
            if (list.Any())
            {
                return list.FirstOrDefault();
            }
            return string.Empty;
        }
        public void XMLifeCircleoutput(DataTable dt, string sourcefile, string destfile)
        {
            //模板文件
            var TempletFileName = sourcefile;//√
                                             //导出文件
            var ReportFileName = destfile;
            var file = new FileStream(TempletFileName, FileMode.Open, FileAccess.Read);
            var wbook = new XSSFWorkbook(file);
            if (dt != null && dt.Rows.Count > 0)
            {
                Logger.SetMode(LogMode.Web);
                try
                {
                    var startIndex = 4;
                    ICellStyle style = wbook.CreateCellStyle();
                    style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin; // 细边框
                    var sheet = wbook.GetSheetAt(0);
                    var orderby = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var xmitem = dt.Rows[i];
                        var crow = sheet.CreateRow(startIndex);
                        var parentid = xmitem["ParentID"].ToString();
                        if (parentid.IsNullOrEmpty())
                        {
                            crow.CreateCell(xh).SetCellValue(orderby);//序号
                            orderby++;
                        }
                        crow.CreateCell(xmlx).SetCellValue(xmitem["ZYLB"].ToString());//项目类型
                        crow.CreateCell(xmmc).SetCellValue(xmitem["XMName"].ToString());//项目名称
                        crow.CreateCell(dwmc).SetCellValue(xmitem["DWMC"].ToString());//单位名称 承接部门 
                        crow.CreateCell(ssbm).SetCellValue(xmitem["SSBM"].ToString());//所属部门 承接部门
                        crow.CreateCell(xmfzr).SetCellValue(xmitem["XMFZR"].ToString());//项目负责人 承接部门
                        crow.CreateCell(xmpzr).SetCellValue(xmitem["PZR"].ToString());//批准人 承接部门
                        crow.CreateCell(ywlxr).SetCellValue(xmitem["YWLXR"].ToString());//业务联系人 承接部门
                        crow.CreateCell(dwxz).SetCellValue(xmitem["JFDWXZ"].ToString());//单位性质 甲方概况
                        crow.CreateCell(qkdwmc).SetCellValue(xmitem["JFDWMC"].ToString());//单位名称 甲方概况
                        crow.CreateCell(sssf).SetCellValue(xmitem["JFProvince"].ToString());//所属省份 甲方概况
                        crow.CreateCell(sss).SetCellValue(xmitem["JFCity"].ToString());//所属市 甲方概况
                        crow.CreateCell(ssqx).SetCellValue(xmitem["JFDistrict"].ToString());//所属区/县 甲方概况

                        crow.CreateCell(kssj).SetCellValue(xmitem["XMBeginTime"].ToString());//开始时间
                        crow.CreateCell(jssj).SetCellValue(xmitem["XMEndTime"].ToString());//结束时间
                        crow.CreateCell(sfwg).SetCellValue(xmitem["SFWG"].ToString());//是否完工
                        crow.CreateCell(wgsj).SetCellValue(xmitem["GWSJ"].ToString());//完工时间
                        crow.CreateCell(lyqk).SetCellValue(xmitem["LYQK"].ToString());//履约情况
                        crow.CreateCell(sfyjf).SetCellValue(xmitem["SFYJF"].ToString());//是否有纠纷

                        if (PublicMethod.GetDouble(xmitem["QPJE"]) > 0)
                            crow.CreateCell(qpje).SetCellValue(PublicMethod.GetDouble(xmitem["QPJE"]));//期票金额
                        if (PublicMethod.GetDouble(xmitem["QZYQQPJE"]) > 0)
                            crow.CreateCell(qzyqje).SetCellValue(PublicMethod.GetDouble(xmitem["QZYQQPJE"]));//逾期期票金额
                        crow.CreateCell(sfyjwkz).SetCellValue(xmitem["SFYJWHZ"].ToString());//是否预计为坏账


                        crow.CreateCell(cscs).SetCellValue(xmitem["CSCS"].ToString());//催收措施

                        crow.CreateCell(htbh).SetCellValue(xmitem["HTBH"].ToString());//合同编号
                        if (PublicMethod.GetDouble(xmitem["HTJE"]) > 0)
                            crow.CreateCell(htje).SetCellValue(PublicMethod.GetDouble(xmitem["HTJE"]));//合同金额

                        crow.CreateCell(htbgqk).SetCellValue(xmitem["HTBGQK"].ToString());//合同变更情况

                        if (PublicMethod.GetDouble(xmitem["TCJSJE"]) > 0)
                            crow.CreateCell(wfxjftcjse).SetCellValue(PublicMethod.GetDouble(xmitem["TCJSJE"]));//我方向甲方提出结算额
                        crow.CreateCell(jssj2).SetCellValue(xmitem["JSSJ"].ToString());//结算时间

                        if (PublicMethod.GetDouble(xmitem["JFQRJSJE"]) > 0)
                            crow.CreateCell(jfqrjse).SetCellValue(PublicMethod.GetDouble(xmitem["JFQRJSJE"]));//甲方确认结算额
                        if (PublicMethod.GetDouble(xmitem["HJJSJE"]) > 0)
                            crow.CreateCell(hjjsje).SetCellValue(PublicMethod.GetDouble(xmitem["HJJSJE"]));//合计结算金额

                        crow.CreateCell(kpsj).SetCellValue(xmitem["KPSJ"].ToString());//开票时间

                        if (!xmitem["FPJE"].ToString().IsNullOrEmpty())
                            crow.CreateCell(kpje).SetCellValue(PublicMethod.GetDouble(xmitem["FPJE"]));//发票金额
                        crow.CreateCell(fpbh).SetCellValue(xmitem["FPBH"].ToString());//发票编号

                        if (PublicMethod.GetDouble(xmitem["HJSKJE"]) > 0)
                            crow.CreateCell(hjskje).SetCellValue(PublicMethod.GetDouble(xmitem["HJSKJE"]));//合计收款金额
                        if (PublicMethod.GetDouble(xmitem["WSKJE"]) > 0)
                            crow.CreateCell(wskje).SetCellValue(PublicMethod.GetDouble(xmitem["WSKJE"]));//未收款金额
                        if (PublicMethod.GetDouble(xmitem["WSKZYGZ"]) > 0)
                            crow.CreateCell(wskzygz).SetCellValue(PublicMethod.GetDouble(xmitem["WSKZYGZ"]));//未收款中已挂账
                        if (PublicMethod.GetDouble(xmitem["WSKWGZJE"]) > 0)
                            crow.CreateCell(wskwgz).SetCellValue(PublicMethod.GetDouble(xmitem["WSKWGZJE"]));//未收款未挂账

                        crow.CreateCell(zl).SetCellValue(xmitem["ZL"].ToString());//账龄
                        crow.CreateCell(zqfssj).SetCellValue(xmitem["ZQFSSJ"].ToString());//债权发生时间
                        crow.CreateCell(zqzsq).SetCellValue(xmitem["ZQZSQ"].ToString());//债权追索期

                        crow.CreateCell(fbdwmc).SetCellValue(xmitem["FBDWMC"].ToString());
                        crow.CreateCell(fbsssf).SetCellValue(xmitem["FBProvince"].ToString());//所属省份
                        crow.CreateCell(fbsss).SetCellValue(xmitem["FBCity"].ToString());//所属市 
                        crow.CreateCell(fbssqx).SetCellValue(xmitem["FBDistrict"].ToString());//所属区/县 
                        crow.CreateCell(fbkssj).SetCellValue(xmitem["FBKSTime"].ToString());
                        crow.CreateCell(fbjssj).SetCellValue(xmitem["FBJZTime"].ToString());
                        crow.CreateCell(fbhtbh).SetCellValue(xmitem["FBHTBH"].ToString());
                        if (!xmitem["FBHTJE"].ToString().IsNullOrEmpty())
                            crow.CreateCell(fbhtje).SetCellValue(PublicMethod.GetDouble(xmitem["FBHTJE"]));

                        if (!xmitem["FBHTHJFKJE"].ToString().IsNullOrEmpty())
                            crow.CreateCell(hjfkje).SetCellValue(PublicMethod.GetDouble(xmitem["FBHTHJFKJE"]));
                        if (!xmitem["FBHTWFKJE"].ToString().IsNullOrEmpty())
                            crow.CreateCell(wfkje).SetCellValue(PublicMethod.GetDouble(xmitem["FBHTWFKJE"]));

                        if (parentid.IsNullOrEmpty())
                        {
                            var trow = sheet.GetRow(startIndex);
                            for (int k = 1; k < 51; k++)
                            {
                                var tcell = trow.GetCell(k);
                                if (tcell == null) tcell = trow.CreateCell(k);
                                tcell.CellStyle = style;
                            }
                        }
                        startIndex += 1;
                        Logger.Log(LogType.Exception, xmitem["XMBH"].ToString() + "," + xmitem["XMName"].ToString());
                    }
                }
                catch (Exception e)
                {
                    Logger.Log(LogType.Exception, e.Message);
                }
            }
            using (FileStream filess = File.OpenWrite(ReportFileName))
            {
                wbook.Write(filess);
            }
        }

        private string GetBuMen(string bumen)
        {
            var sbbm = new StringBuilder();
            if (!string.IsNullOrEmpty(bumen))
            {
                foreach (var item in bumen.Split(','))
                {
                    if (item.IsNullOrEmpty()) continue;
                    sbbm.Append(item);
                    if (item == "第一工程处")
                    {
                        sbbm.Append(",第三工程处,第四工程处");
                    }
                    if (item == "水环中心")
                    {
                        sbbm.Append(",地质调查所,工程施工部");
                    }
                }
            }
            return sbbm.ToString();
        }
        private string GetSqlInString(string bumen)
        {
            var sbbm = new StringBuilder();
            if (!bumen.IsNullOrEmpty())
            {
                foreach (var item in bumen.Split(','))
                {
                    if (item.IsNullOrEmpty()) continue;
                    sbbm.AppendFormat("'{0}',", item);
                }
            }
            return sbbm.ToString().TrimEnd(',');
        }
        public List<Dictionary<string, object>> ConvertDataTableToList(DataTable dt)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var dict = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        dict[col.ColumnName] = row[col];
                    }
                    list.Add(dict);
                }
            }
            return list;
        }
        #region column index
        private int xh = 1;//序号
        private int xmlx = 2;//项目类型
        private int xmmc = 3;//项目名称
        private int dwmc = 4;//单位名称
        private int ssbm = 5;//所属部门
        private int xmfzr = 6;//项目负责人
        private int xmpzr = 7;//批准人
        private int ywlxr = 8;//业务联系人

        private int dwxz = 9;//单位性质（欠款单位性质）
        private int qkdwmc = 10;//单位名称（欠款单位名称）
        private int sssf = 11;//所属省份（欠款单位所属省份）
        private int sss = 12;//所属市（欠款单位所属市）
        private int ssqx = 13;//所属区/县（欠款单位所属区/县）
        private int zl = 14;//账龄

        private int kssj = 15;//开始时间
        private int jssj = 16;//结束时间
        private int htbh = 17;//合同编号
        private int sfwg = 18;//是否完工
        private int wgsj = 19;//完工时间
        private int lyqk = 20;//履约情况
        private int htbgqk = 21;//合同变更情况
        private int sfyjf = 22;//是否有纠纷
        private int htje = 23;//合同金额 项目概况
        private int wfxjftcjse = 24;//我方向甲方提出结算额 项目概况
        private int jssj2 = 25;//结算时间 项目概况 项目概况
        private int jfqrjse = 26;//甲方确认结算额 项目概况
        private int hjjsje = 27;//合计结算金额 项目概况

        private int kpsj = 28;//开票时间 项目收入情况
        private int kpje = 29;//发票金额 项目收入情况
        private int fpbh = 30;//发票编号 项目收入情况
        private int hjskje = 31;//合计收款金额 项目收入情况
        private int wskje = 32;//未收款金额 项目收入情况
        private int wskzygz = 33;//未收款中已挂账 项目收入情况
        private int qpje = 34;//期票金额 项目收入情况
        private int qzyqje = 35;//其中：逾期期票金额 项目收入情况
        private int wskwgz = 36;//未收款未挂账 项目收入情况
        private int sfyjwkz = 37;//是否预计为坏账 项目收入情况
        private int zqfssj = 38;//债权发生时间 项目收入情况
        private int zqzsq = 39;//债权追索期 项目收入情况
        private int cscs = 40;//催收措施 项目收入情况

        private int fbdwmc = 41;//单位名称 分包方概况
        private int fbsssf = 42;//所属省份 分包方概况
        private int fbsss = 43;//所属市 分包方概况
        private int fbssqx = 44;//所属区/县 分包方概况
        private int fbkssj = 45;//开始时间 分包方概况
        private int fbjssj = 46;//结束时间 分包方概况
        private int fbhtbh = 47;//合同编号 分包方概况
        private int fbhtje = 48;//合同金额 分包方概况
        private int hjfkje = 49;//合计付款金额 项目支付情况
        private int wfkje = 50;//未付款金额 项目支付情况 
        public class cdclass
        {
            public decimal submitAmt;
            public decimal fukuanAmt;
            public decimal circleAmt;
            public decimal unfukuanAmt;
        }
        #endregion
        private string ChargemanDataSqlFormat
        {
            get
            {
                return @"select YSGCK YiSJE,YSJE YingSJE,YFLWF YiFJE,YFJE YingFJE,x.*,FormID
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
	  ,SUBSTRING(d.BeiYong1,CHARINDEX('@',d.BeiYong1)+1,LEN(d.BeiYong1)) Name from ERPXMJBXXExtend t join ERPXMJBXX x on t.XMBH=x.XMBH
join ERPNWorkToDo d on x.NWorkID=d.ID
where StateNow not in ('不通过') {0}";
            }
        }
        private class ERPXMZLGuiDangArchive : ZWL.BLL.ERPXMZLGuiDang
        {
            public ZWL.BLL.ERPXMJBXX XMItem { get; set; }
        }
        private class XMZLGuiDangArchive
        {
            public List<ERPXMZLGuiDangArchive> List { get; set; }
            public Pagination Pagination { get; set; }

        }
        private class Pagination
        {
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int PageSum { get; set; }
            public int Total { get; set; }
        }
        private class CurrentUser
        {
            public int code { get; set; }
            public UserData data { get; set; }
        }
        private class UserData
        {
            public userinfo userinfo { get; set; }
        }
        private class userinfo
        {
            public ArrayList organizeIdList { get; set; }
            public string organizeName { get; set; }
            public string organizeId { get; set; }
            public string departmentName { get; set; }
            public string departmentId { get; set; }
            public string userAccount { get; set; }
            public string userName { get; set; }
            public string userId { get; set; }
        }
        private class SyncDataIntegBizSysResult
        {
            public int code { get; set; }
            public string msg { get; set; }
        }
        #endregion
    }
}