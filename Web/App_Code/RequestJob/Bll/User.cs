using Aspose.Cells;
using FSDZ.Logger;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using ViewModels;
using ZWL.Common;
using ZWL.DBUtility;

namespace RequestJob
{
    /// <summary>
    /// User 的摘要说明
    /// </summary>
    [AuthSession]
    public class User : Base, IRequestJob
    {
        //[Authorize(Type = AuthorizeType.Token)]
        [HttpMethod(Action = HttpVerb.POST)]
        public JsonResult UserInfo(HttpRequest Request)
        {
            try
            {
                var msg = "";
                var user = new ZWL.BLL.ERPUser();
                user.UserLogin("admin", "1", "1", ref msg);
                //var checkuser = user.UserLogin(username, userpwd, "1", ref msg);
                var t = JWTUtil.GenerateJwt("admin", DateTime.Now, DateTime.Now.AddHours(1));
                var l = JWTUtil.ValidateToken(t);
                return JsonResult(true, "", new { t = t, l = l });
            }
            catch (Exception e)
            {
                return JsonResult(false, e.Message);
            }
        }
        [HttpMethod(Action = HttpVerb.POST)]
        public JsonResult GetConfigOauth(HttpRequest Request)
        {
            try
            {
                var username = Request["username"];
                if (username.IsNullOrEmpty())
                {
                    return JsonResult(false, "请输入账号");
                }
                var url = Util.IntegBizSysAPIBaseUrl + "/api/oauth/getConfig/{0}?_t={1}".FormatWith(username, DateTime.Now.Ticks);
                var msg = "";

                var res = RequestHelper.HttpGet<IntegBizSysOauthConfig>(url);
                if (res.code == 200)
                {
                    return JsonResult(true, res.msg, res.data);
                }
                return JsonResult(false, "请求出错了");
            }
            catch (Exception e)
            {
                return JsonResult(false, e.Message);
            }
        }
        [HttpMethod(Action = HttpVerb.POST)]
        public JsonResult ImageCodeOauth(HttpRequest Request)
        {
            try
            {
                var codeLength = Request["codeLength"];
                var timestamp = Request["timestamp"];
                if (codeLength.IsNullOrEmpty() || timestamp.IsNullOrEmpty())
                {
                    return JsonResult(false, "请求参数出错");
                }
                var url = Util.IntegBizSysAPIBaseUrl + "/api/oauth/ImageCode/{0}/{1}".FormatWith(codeLength, timestamp);

                var res = RequestHelper.GetImageBase64(url);
                if (!res.IsNullOrEmpty())
                {
                    return JsonResult(true, "", res);
                }
                return JsonResult(false, "请求出错了");
            }
            catch (Exception e)
            {
                return JsonResult(false, e.Message);
            }
        }
        [HttpMethod(Action = HttpVerb.POST)]
        public JsonResult LoginIntegBizSys(HttpRequest Request)
        {
            try
            {
                var accountVal = Request["account"];
                var encryptPassword = Request["password"];
                var codeVal = Request["code"];
                var timeStamp = Request["timestamp"];
                var formData = new
                {
                    account = accountVal,
                    password = encryptPassword,
                    code = codeVal,
                    origin = "password",
                    timestamp = timeStamp,
                    grant_type = "password"
                };
                var url = Util.IntegBizSysAPIBaseUrl + "/api/oauth/Login";
                var res = RequestHelper.HttpPost<LoginIntegBizSysResult>(url, formData);
                if (res.code == 200)
                {
                    var token = res.data.token;
                    var result = SaveIntegBizSysToken(Request, token);
                    return result;
                }
                return JsonResult(false, res);
            }
            catch (Exception e)
            {
                return JsonResult(false, e.Message);
            }
        }
        /// <summary>
        /// 检查IntegBizSysToken是否有效
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpMethod(Action = HttpVerb.POST)]
        public JsonResult SyncIntegBizSysToken(HttpRequest Request)
        {
            try
            {
                var msg = "";
                var sqlWhere = @"select top 1 * from Token where GETDATE()<ExpiresTime and EnabledMark=1 and UserName='{0}' and Type='Sync' order by ID desc ".FormatWith(PublicMethod.GetUserName());
                var temp = Conv<ZWL.BLL.Token>.GetModel(sqlWhere);
                if (temp == null)
                {
                    return JsonResult(false, "无有效token。");
                }
                return JsonResult(true, msg);
            }
            catch (Exception e)
            {
                return JsonResult(false, e.Message);
            }
        }
        [HttpMethod(Action = HttpVerb.POST)]
        public JsonResult SaveIntegBizSysToken(HttpRequest Request)
        {
            try
            {
                var token = Request["token"];
                return SaveIntegBizSysToken(Request, token);
            }
            catch (Exception e)
            {
                return JsonResult(false, e.Message);
            }
        }
        [HttpMethod(Action = HttpVerb.POST)]
        public JsonResult Login(HttpRequest Request, string json)
        {
            try
            {
                var info = JsonConvert.DeserializeObject<LoginInput>(json);
                var msg = "";
                var result = false;
                object data = new { };
                var username = info.UserName;
                var userpwd = info.PassWord;
                if (!username.IsNullOrEmpty() && !userpwd.IsNullOrEmpty())
                {
                    var user = new ZWL.BLL.ERPUser();
                    result = user.UserLogin(username, userpwd, "1", ref msg);
                    if (result)
                    {
                        var token = JWTUtil.GenerateJwt(username);
                        var timestamp = Request.RequestContext.HttpContext.Timestamp;
                        var tinfo = new ZWL.BLL.Token()
                        {
                            CreatedTime = timestamp,
                            DeviceId = Request.Headers.Get("DeviceId"),
                            UserName = username,
                            Type = "access",//access_token（访问令牌）、refresh_token（刷新令牌）
                            TokenValue = token,
                            ExpiresTime = timestamp.AddDays(1).Date,
                        };
                        tinfo.ID = tinfo.Add();
                        data = new { token = token };
                    }
                }
                return JsonResult(result, msg, data);
            }
            catch (Exception e)
            {
                return JsonResult(false, e.Message);
            }
        }
        public string GenerateToken(string otoken)
        {
            var msg = "";
            var token = string.Empty;
            var info = new ZWL.BLL.Token();
            info.GetModel(otoken);
            if (info.ID > 0)
            {
                if (info.ExpiresTime < DateTime.Now)
                {
                    var user = new ZWL.BLL.ERPUser();
                    var uinfo = user.GetModel("UserName='{0}'".FormatWith(info.UserName));
                    if (user.UserLogin(uinfo.UserName, PublicMethod.GetMd5(ZWL.Common.DEncrypt.DESEncrypt.Decrypt(uinfo.UserPwd)), "1", ref msg))
                    {
                        token = JWTUtil.GenerateJwt(uinfo.UserName);
                    }
                }
                else
                {
                    token = otoken;
                }
            }
            return token;
        }
        public string GenerateToken(string username, string userpwd)
        {
            var msg = "";
            var token = string.Empty;
            var user = new ZWL.BLL.ERPUser();
            if (user.UserLogin(username, userpwd, "1", ref msg))
            {
                token = JWTUtil.GenerateJwt(username);
            }
            return token;
        }
        public JsonResult PrintKaoQinDetailReport(HttpRequest Request)
        {
            try
            {
                var exportformat = Request["format"];
                if (exportformat.IsNullOrEmpty())
                    exportformat = "html";
                var msg = string.Empty;
                var check = CanPrintKaoQinDetail(Request, ref msg);
                if (!check)
                {
                    return JsonResult(false, msg);
                }
                object result = null;
                var filename = string.Empty;
                var deptInput = Request["dept"].ToString().Trim();
                if (!deptInput.IsNullOrEmpty() && deptInput.Contains("总工程师办公室"))
                {
                    deptInput += ",资料室";
                }
                var deptlist = deptInput.Split(',').ToList();
                var yearItem = PublicMethod.GetInto(Request["yearItem"]);
                var monthItem = PublicMethod.GetInto(Request["monthItem"]);
                var title = "{0}{1}年{2}月份考勤表".FormatWith(deptlist.FirstOrDefault(), yearItem, monthItem);
                var newfilenameroot = Request.RequestContext.HttpContext.Timestamp.Ticks.ToString();
                var newfilename = newfilenameroot + "." + SaveFormat.Xlsx.ToString().ToLower();
                var newpdfname = newfilenameroot + "." + SaveFormat.Html.ToString().ToLower();
                var newTempName = DateTime.Now.Ticks + "." + SaveFormat.Xlsx.ToString().ToLower();
                var uploadrootdir = HostingEnvironment.MapPath("~/ReportFile");
                var tempPath = Path.Combine(uploadrootdir, "职工考勤表模板.xlsx");
                var newTempPath = Path.Combine(Path.Combine(PublicMethod.UploadFileFolderTruePath, "DocumentPreview"), newTempName);
                var targPath = Path.Combine(Path.Combine(PublicMethod.UploadFileFolderTruePath, "DocumentPreview"), newfilename);
                if (File.Exists(tempPath))
                {
                    File.Copy(tempPath, newTempPath, true);
                    //创建一个工作簿对象
                    //IWorkbook workbook = null;
                    var file = new FileStream(newTempPath, FileMode.Open, FileAccess.Read);
                    var workbook = new XSSFWorkbook(file);
                    //获取第一个工作表
                    ISheet sheet = workbook.GetSheetAt(0);
                    var sqlWhere = "select * from ({0}) t where BuMen<>'协解人员' and (Department in ({1})) ORDER BY DirID,Orderby,DisplayID"
                        .FormatWith(PrintKaoQinDetailUserDeptSQL, PublicMethod.GetSqlInWhere(deptInput));
                    var dt = DbHelperSQL.GetDataTable(sqlWhere);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        var firstDate = new DateTime(yearItem, monthItem, 1);
                        var lastDate = firstDate.AddMonths(1).AddDays(-1).Date;
                        var workDates = PublicMethod.GetWorkDays(firstDate, lastDate);
                        var csql = @"select c.* from ERPChuChai c join ERPNWorkToDo d on c.NWorkID=d.ID 
				                    where StateNow='正常结束' 
                                        and ( 
                                                (ChuChaiStart BETWEEN '{0}' AND '{1}')
                                                OR (ChuChaiEnd BETWEEN '{0}' AND '{1}')
                                                OR (ChuChaiStart <= '{0}' AND ChuChaiEnd >= '{1}')
                                            )
				                    and EXISTS (select 1 from ({2}) u where (c.SQR=u.UserName or TongXingRenYuan like '%'+u.TrueName+'%') and (Department in ({3})))"
                                    .FormatWith(TimeParser.GetFormatDateString(firstDate), TimeParser.GetFormatDateString(lastDate), PrintKaoQinDetailUserDeptSQL, PublicMethod.GetSqlInWhere(deptInput));
                        var clist = Conv<ZWL.BLL.ERPChuChai>.GetList(csql);
                        var qsql = @"select c.* from ERPQingJia c join ERPNWorkToDo d on c.NWorkID=d.ID 
				                    where StateNow='正常结束' and 														
															( 
																(QJSJStart BETWEEN '{0}' AND '{1}')
																OR (QJSJEnd BETWEEN '{0}' AND '{1}')
																OR (QJSJStart <= '{0}' AND QJSJEnd >= '{1}')
															)
				                    and EXISTS (select 1 from ({2}) u where c.QJR=u.UserName and (Department in ({3})))"
                                    .FormatWith(TimeParser.GetFormatDateString(firstDate), TimeParser.GetFormatDateString(lastDate), PrintKaoQinDetailUserDeptSQL, PublicMethod.GetSqlInWhere(deptInput));
                        var qlist = Conv<ZWL.BLL.ERPQingJia>.GetList(qsql);
                        var zsql = @"SELECT l.* FROM VacationLeave l join ERPUser u on l.UserName=u.TrueName 
                                        where (u.Department in ({2}) or l.Department in ({2}))
                                        and ( 
				                                        (StartDate BETWEEN '{0}' AND '{1}')
				                                        OR (EndDate BETWEEN '{0}' AND '{1}')
				                                        OR (StartDate <= '{0}' AND EndDate >= '{1}')
		                                        )".FormatWith(TimeParser.GetFormatDateString(firstDate), TimeParser.GetFormatDateString(lastDate), PublicMethod.GetSqlInWhere(deptInput));
                        var zlist = Conv<ZWL.BLL.VacationLeave>.GetList(zsql);
                        var glist = dt.AsEnumerable().GroupBy(x => new { ZWXZ = x.Field<string>("ZWXZ"), Orderby = x.Field<int>("Orderby"), }).OrderBy(x => x.Key.Orderby);
                        for (int i = 0; i < glist.Count(); i++)
                        {
                            if (!ValidateSheetIndex(workbook, i))
                                sheet.CopySheet(glist.ElementAt(i).Key.ZWXZ, true);
                            else
                                workbook.SetSheetName(i, glist.ElementAt(i).Key.ZWXZ);
                        }
                        foreach (var item in glist)
                        {
                            var rowIndex = 2;
                            var gkey = item.Key.ZWXZ;
                            var csheet = workbook.GetSheet(gkey);
                            var sheettitle = "{0}{3}{1}年{2}月份考勤表".FormatWith(deptlist.FirstOrDefault(), yearItem, monthItem, gkey);
                            //设置标题
                            var titleCell = csheet.GetRow(0).GetCell(0);
                            titleCell.SetCellValue(sheettitle);
                            for (int i = 0; i < item.Count(); i++)//遍历行
                            {
                                var sitem = item.ElementAt(i);
                                var cellStartIndex = 1;
                                var username = sitem["UserName"].ToString();
                                var truename = sitem["TrueName"].ToString();
                                var dept = sitem["Department"].ToString();
                                if (item.Count() > 8 && i >= 8)
                                {
                                    //在当前行索引处插入一行，保留之后的行
                                    csheet.ShiftRows(rowIndex, csheet.LastRowNum, 2, true, false);
                                    //创建新的行对象
                                    var nrow = csheet.CreateRow(rowIndex);
                                    nrow.RowStyle = csheet.GetRow(rowIndex - 2).RowStyle;
                                    nrow.Height = csheet.GetRow(rowIndex - 2).Height;
                                    var nrow1 = csheet.CreateRow(rowIndex + 1);
                                    nrow1.RowStyle = csheet.GetRow(rowIndex - 1).RowStyle;
                                    nrow1.Height = csheet.GetRow(rowIndex - 1).Height;

                                    var templateMergedRegion = new CellRangeAddress(rowIndex, rowIndex + 1, 0, 0);
                                    csheet.AddMergedRegion(templateMergedRegion);
                                    var fCell = nrow.GetCell(0);
                                    if (fCell == null) fCell = nrow.CreateCell(0);
                                    fCell.CellStyle = csheet.GetRow(rowIndex - 2).GetCell(0).CellStyle;

                                    var mIndex = 1;
                                    for (int j = 0; j < 31; j++)
                                    {
                                        templateMergedRegion = new CellRangeAddress(rowIndex, rowIndex + 1, mIndex, mIndex + 1);
                                        csheet.AddMergedRegion(templateMergedRegion);
                                        var prerow = csheet.GetRow(rowIndex - 2);
                                        var prerow1 = csheet.GetRow(rowIndex - 1);

                                        var tempCell = nrow.GetCell(mIndex);
                                        if (tempCell == null) tempCell = nrow.CreateCell(mIndex);
                                        tempCell.CellStyle = prerow.GetCell(mIndex).CellStyle;


                                        var tempCell1 = nrow1.GetCell(mIndex + 1);
                                        if (tempCell1 == null) tempCell1 = nrow1.CreateCell(mIndex + 1);
                                        tempCell1.CellStyle = prerow1.GetCell(mIndex + 1).CellStyle;

                                        mIndex += 2;
                                    }

                                    // 2. 获取模板中的合并单元格信息
                                    //var templateMergedRegion = csheet.GetMergedRegion(rowIndex - 2); // 假设第一个合并单元格
                                    // 复制合并单元格

                                }
                                var crow = csheet.GetRow(rowIndex);
                                var cCell = crow.GetCell(0);
                                if (cCell == null) cCell = crow.CreateCell(0);
                                cCell.SetCellValue(truename.IsNullOrEmpty() ? username : truename);
                                for (int j = 1; j <= lastDate.Day; j++)//遍历每人的日期列
                                {
                                    var pdate = new DateTime(yearItem, monthItem, j);
                                    var text = string.Empty;
                                    var isWorkDate = workDates.Contains(pdate);
                                    if (isWorkDate)
                                        text = "1";
                                    else
                                    {
                                        text = "=";
                                        if (zlist != null && zlist.Any(x => x.UserName == truename && x.Department == dept && x.StartDate <= pdate && x.EndDate >= pdate))
                                        {
                                            text = "Z";
                                        }
                                    }
                                    if (clist != null && clist.Any(x => (x.SQR == username || !x.TongXingRenYuan.IsNullOrEmpty() && x.TongXingRenYuan.Contains(username)) && x.ChuChaiStart <= pdate && x.ChuChaiEnd >= pdate))
                                    {
                                        text = "△";
                                    }
                                    if (qlist != null && qlist.Any(x => x.QJR == username && x.QJSJStart <= pdate && x.QJSJEnd >= pdate))
                                    {
                                        var tempQList = qlist.Where(x => x.QJR == username && x.QJSJStart <= pdate && x.QJSJEnd >= pdate);
                                        var tempText = GetQingJiaFlag(tempQList, isWorkDate);
                                        if (!tempText.IsNullOrEmpty())
                                        {
                                            text = tempText;
                                        }
                                    }
                                    var sCell = crow.GetCell(cellStartIndex);
                                    if (sCell == null)
                                        sCell = crow.CreateCell(cellStartIndex);
                                    sCell.SetCellValue(text);
                                    cellStartIndex += 2;
                                }
                                rowIndex += 2;
                            }
                        }
                        workbook.SetActiveSheet(0);
                    }
                    using (FileStream filess = File.OpenWrite(targPath))
                    {
                        workbook.Write(filess);
                    }
                    using (var workBook = new Workbook(targPath))
                    {
                        var designer = new WorkbookDesigner(workBook);
                        designer.Process();
                        workBook.Save(targPath.Replace(newfilename, newpdfname), SaveFormat.Html);
                    }
                    File.Delete(newTempPath);
                }
                result = new { filename = title, fileId = newfilename, htmlId = newpdfname };
                return JsonResult(true, "", result);
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return JsonResult(false, e.Message);
            }
        }
        [HttpMethod(Action = HttpVerb.POST)]
        public JsonResult ValidateBaoCan(HttpRequest Request)
        {
            try
            {
                var msg = "";
                var result = false;
                object data = null;
                var ulist = new List<string>();
                var sdate = Request["sdate"].ToString();
                var edate = Request["edate"].ToString();
                var cname = PublicMethod.GetUserName();
                var names = cname + "," + Util.ReplaceSymbolsWithComma(Request["names"].ToString());
                var namelist = names.Split(',');
                for (int i = 0; i < namelist.Length; i++)
                {
                    var uname = namelist[i];
                    if (uname.IsNullOrEmpty() || uname == "无") continue;
                    var sqlWhere = string.Format("IsCancel='否' and BCRQ>='{0}' and BCRQ<='{1}' and UserName='{2}' ", sdate, edate, uname);
                    var blist = Conv<ZWL.BLL.ERPBaoCan>.GetListBySQLWhere(sqlWhere);
                    if (blist != null && blist.Any())
                    {
                        if (!ulist.Contains(uname))
                            ulist.Add(uname);
                    }
                }
                result = ulist.Count > 0;
                data = string.Join(",", ulist);
                return JsonResult(result, msg, data);
            }
            catch (Exception e)
            {
                return JsonResult(false, e.Message);
            }
        }
        [HttpMethod(Action = HttpVerb.POST)]
        public JsonResult YongCanYiChangInfo(HttpRequest Request)
        {
            try
            {
                var msg = "";
                var timestr = Request["date"];
                var users = Request["users"];
                if (timestr.IsNullOrEmpty())
                    return JsonResult(false, "请选择一个日期");
                var sqlformat = @"SELECT * from (

select b.ID,b.LotID,Number,Name,a.UserName,ZhiWu,b.Sex,RecordDate,XingQi,ShiJianDuan,CanShi,ShiJianDian,
KaoQinRecord,Memo,IsChuChai,BCRQ,IsCancel,u.Department,'' as sfbc,'' as sfyc,u.UserName as xm,'' as rq,
'' as xq,'' as ycsd,'' iserror,m.BackInfo,m.DirID,u.DisplayID,ISNULL(CanShi, ShiJianDian) CanShiText,
ISNULL(Name, a.UserName) NameText,
(case when a.UserName is null then '未报餐就餐' else '报餐未就餐' end) ExcpText
                        from FanTangJiuCanRecord b
                        join Flow f on f.LotID=b.LotID and Operation=1 and KaoQinRecord != '-'
                        FULL OUTER JOIN (select * from ERPBaoCan where IsCancel='否') a ON a.UserName = b.Name and a.BCRQ = b.RecordDate and a.ShiJianDian = b.CanShi
                        join ERPUser u on(u.UserName = a.UserName or u.UserName = b.Name)
                        LEFT JOIN ERPBuMen m on u.Department=m.BuMenName
                        where  (RecordDate >= '{0}' or BCRQ >= '{0}')  and  (RecordDate < '{1}' or BCRQ < '{1}') 
                        and  (a.UserName is null or Name is null) and (a.UserName not in ('蔡晓帆','谢廷忠') or Name not in ('蔡晓帆','谢廷忠')) 

) t ";
                var selectDate = TimeParser.GetFormatDate(timestr);
                var sqlWhere = string.Format(sqlformat, selectDate, selectDate.Value.Date.AddDays(1));
                if (!users.IsNullOrEmpty())
                {
                    sqlWhere += " where NameText in ({0})".FormatWith(PublicMethod.GetSplitInSQL(Util.ReplaceSymbolsWithComma(users), ","));
                }
                var dt = DbHelperSQL.GetDataTable(sqlWhere);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return JsonResult(false, "所选日期[{0}]无异常数据".FormatWith(selectDate));
                }
                var mlist = new List<KeyValuePair<string, string>>();
                var list = dt.AsEnumerable().GroupBy(x => x.Field<string>("NameText"));
                foreach (var item in list.OrderBy(x => x.Key))
                {
                    var text = "";
                    var tlist = item.ToList();
                    var glist = tlist.GroupBy(x => x.Field<string>("ExcpText"));
                    if (tlist.Count == 2)
                    {
                        if (glist.Count() == 1)
                            text = "早、午餐" + glist.FirstOrDefault().Key;
                        else
                        {
                            foreach (var g in glist)
                            {
                                text += g.FirstOrDefault()["CanShiText"] + g.Key + "、";
                            }
                            text = text.TrimEnd('、');
                        }
                    }
                    else
                    {
                        text += item.FirstOrDefault()["CanShiText"].ToString() + item.FirstOrDefault()["ExcpText"];
                    }
                    mlist.Add(new KeyValuePair<string, string>(item.Key, text));
                }
                return JsonResult(true, "", new { Users = string.Join(",", list.Select(x => x.Key).OrderBy(x => x)), MList = mlist });
            }
            catch (Exception e)
            {
                return JsonResult(false, e.Message);
            }
        }
        [HttpMethod(Action = HttpVerb.POST)]
        public JsonResult ChuChaiYongCanYiChangInfo(HttpRequest Request)
        {
            try
            {
                var msg = "";
                var sdatetext = Request["sdate"];
                var edatetext = Request["edate"];
                var users = Request["users"];
                if (sdatetext.IsNullOrEmpty() || edatetext.IsNullOrEmpty())
                    return JsonResult(false, "请选择日期范围");
                var sqlformat = @"SELECT * from (

                        select d.ID,Name,Dept Department,RecordDate,XingQi,ZaoCan,WuCan,WanCan,IsChuChai,ChuChaiDiDian,ChuChaiNWorkID,Received,Comment 
                            from FanTangJiuCanRecordReport d 
                            where LotID in (
                            SELECT LotID from Flow where DataTable='FanTangJiuCanRecord' and Operation=1
                            ) and Name<>'无'  and  RecordDate >= '{0}' and  RecordDate < '{1}' and  IsChuChai='是' and Received='是' 
                        and (ZaoCan<>'-' or WuCan<>'-') and  IsChuChai='是' and  Received='是' and Name not in ('蔡晓帆','谢廷忠')
                            
                        ) t ";
                var sdate = TimeParser.GetFormatDate(sdatetext);
                var edate = TimeParser.GetFormatDate(edatetext);
                var sqlWhere = string.Format(sqlformat, sdate, edate.Value.Date.AddDays(1));
                if (!users.IsNullOrEmpty())
                {
                    sqlWhere += " where Name in ({0})".FormatWith(PublicMethod.GetSplitInSQL(Util.ReplaceSymbolsWithComma(users), ","));
                }
                var dt = DbHelperSQL.GetDataTable(sqlWhere);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return JsonResult(false, "所选日期[{0}、{1}]无异常数据".FormatWith(sdate, edate));
                }
                var mlist = new List<object>();
                var list = dt.AsEnumerable().GroupBy(x => x.Field<string>("Name"));
                var dlist = dt.AsEnumerable();
                foreach (var item in dlist.OrderBy(x => x.Field<DateTime>("RecordDate")))
                {
                    var text = "";
                    var date = TimeParser.GetFormatDateString(item["RecordDate"].ToString());
                    var name = item["Name"].ToString();
                    var zanc = item["ZaoCan"].ToString();
                    var wuc = item["WuCan"].ToString();
                    if (!zanc.IsNullOrEmpty() && zanc != "-")
                    {
                        text += "早、";
                    }
                    if (!wuc.IsNullOrEmpty() && wuc != "-")
                    {
                        text += "午、";
                    }
                    text = text.TrimEnd('、');

                    mlist.Add(new { Name = name, Date = date, Text = text });
                }
                return JsonResult(true, "", new { Users = string.Join(",", list.Select(x => x.Key).OrderBy(x => x)), MList = mlist });
            }
            catch (Exception e)
            {
                return JsonResult(false, e.Message);
            }
        }
        private bool ValidateSheetIndex(IWorkbook wbook, int index)
        {
            var result = true;
            int num = wbook.NumberOfSheets - 1;
            if (index < 0 || index > num)
            {
                return false;
            }
            return result;
        }
        public JsonResult CanAlterChuChaiModel(HttpRequest Request)
        {
            var id = PublicMethod.GetInt(Request["id"]);
            var msg = string.Empty;
            var result = CanAlterChuChaiModel(id, ref msg);
            return JsonResult(result, msg);
        }
        private bool CanAlterChuChaiModel(int workId, ref string msg)
        {
            var result = true;
            var work = new ZWL.BLL.ERPNWorkToDo();
            work.GetModel(workId);
            if (!PublicMethod.GetJiaoSe().Contains("超级管理员"))
            {
                if (work.UserName != PublicMethod.GetUserName())
                {
                    msg = "只有登记人本人才能变更工作申请.";
                    return false;
                }
                else
                {
                    if (work.WorkName.Contains("变更"))
                    {
                        if (work.StateNow != "已被驳回" && work.StateNow != "正在办理")
                        {
                            msg = "此审批流程工作状态为[" + work.StateNow + "],只有状态为[正在办理、已被驳回]的工作才可修改．";
                            return false;
                        }
                    }
                    else
                    {
                        if (work.StateNow != "正常结束")
                        {
                            msg = "此审批流程工作状态为[" + work.StateNow + "],只有状态为[正常结束]的工作才可变更．";
                            return false;
                        }
                        if (!work.WorkName.Contains("出差审批表"))
                        {
                            var workname = work.WorkName.Split("--")[1].Split("(")[0];
                            msg = "此审批流程为[" + workname + "],只有流程为[出差审批表]的工作才可变更．";
                            return false;
                        }
                        var list = Conv<ZWL.BLL.ERPNWorkToDo>.GetList("select * from ERPNWorkToDo where StateNow in ('正在办理','正常结束') and WorkName like '%出差变更审批流程%' and BeiYong2='{0}'".FormatWith(workId));
                        if (list.Count > 0)
                        {
                            var firstinfo = list.FirstOrDefault();
                            msg = "此审批流程于[{0}]已登记过出差变更,其审批状态为[{1}],只能变更一次，请无重复操作．".FormatWith(firstinfo.TimeStr, firstinfo.StateNow);
                            return false;
                        }
                    }
                }

            }

            return result;
        }
        private bool CanPrintKaoQinDetail(HttpRequest Request, ref string msg)
        {
            var result = true;
            var exportformat = Request["format"];
            if (exportformat == "excel")
            {
                if (!PublicMethod.StrIFIn("|kqmxmbexportE|", PublicMethod.GetQuanXian()))
                {
                    msg = "当前账号没有导出excel权限。";
                    return false;
                }
            }
            var deptInput = Request["dept"].ToString().Trim();
            if (deptInput.IsNullOrEmpty())
            {
                msg = "请选择部门名称。";
                return false;
            }
            else
            {
                if (deptInput != PublicMethod.GetDepartment() && !PublicMethod.StrIFIn("|kqmxmbexportW|", PublicMethod.GetQuanXian()))
                {
                    msg = "当前用户没有查看其他部门数据的权限，选中的部门:{0}".FormatWith(deptInput);
                    return false;
                }
                var list = Conv<ZWL.BLL.ERPUser>.GetList("select Department from ({0}) t group by Department".FormatWith(PrintKaoQinDetailUserDeptSQL));
                var deptlist = deptInput.Split(",").ToList();
                if (!list.Any(x => deptlist.Contains(x.Department) && deptInput != "机关"))
                {
                    msg = "选择的部门不存在，请重新选择。选中的部门:{0}".FormatWith(deptInput);
                    return false;
                }
            }
            return result;
        }
        private string GetQingJiaFlag(IEnumerable<ZWL.BLL.ERPQingJia> list, bool isWorkDate)
        {
            var text = string.Empty;
            if (list != null && list.Any())
            {
                var glist = list.GroupBy(x => x.QJLX);
                foreach (var item in glist)
                {
                    #region MyRegion
                    if (item.Key == "病假")
                    {
                        text += "+" + "/";
                    }
                    else if (item.Key == "事假")
                    {
                        text += "⊙" + "/";
                    }
                    else if (item.Key == "探亲假")
                    {
                        text += "◎" + "/";
                    }
                    else if (item.Key == "育儿假")
                    {
                        text += "Y" + "/";
                    }
                    else if (item.Key == "婚假" || item.Key == "丧假")
                    {
                        text += "※" + "/";
                    }
                    else if (item.Key == "产假")
                    {
                        text += "∽" + "/";
                    }
                    else if (item.Key == "年休假")
                    {
                        if (isWorkDate)
                        {
                            text += "工";
                            if (item.Count() == 1)
                            {
                                var qjdays = item.Sum(x => x.QJTS);
                                float integerPart = (float)Math.Floor(qjdays); // 整数部分
                                float decimalPart = qjdays - integerPart; // 小数部分
                                if (decimalPart > 0 && qjdays < 1)
                                {
                                    text += "半" + "/";
                                }
                            }
                            text += "/";
                        }
                    }
                    else if (item.Key.Contains("独生子女"))
                    {
                        text += "D" + "/";
                    }
                    else if (item.Key.Contains("补休假"))
                    {
                        text += "＠" + "/";
                    }
                    else if (item.Key == "陪产假")
                    {
                        text += "*" + "/";
                    }
                    else if (item.Key == "哺乳假")
                    {
                        text += "∞" + "/";
                    }
                    else if (item.Key == "看护假")
                    {
                        text += "#" + "/";
                    }
                    else
                    {
                        if (item.Key.EndsWith("假"))
                            text += "休" + "/";
                    }
                    #endregion
                }
            }
            return text.TrimEnd('/');
        }
        private JsonResult SaveIntegBizSysToken(HttpRequest Request, string token)
        {
            var msg = "";
            if (token.IsNullOrEmpty())
            {
                return JsonResult(false, "登录失败。");
            }
            if (!JwtTokenHelper.IsValidTokenFormat(token))
            {
                return JsonResult(false, "非法token。");
            }
            var username = PublicMethod.GetUserName();
            if (username.IsNullOrEmpty() || username == "NoLogin")
            {
                return JsonResult(false, "请重新登录OA系统或刷新。");
            }
            var currentId = 0;
            var temp = Conv<ZWL.BLL.Token>.GetModel("select top 1 * from Token where TokenValue='{0}'".FormatWith(token));
            if (temp == null)
            {
                var timestamp = Request.RequestContext.HttpContext.Timestamp;
                var exp = JwtTokenHelper.GetTokenExpirationTime(token);
                var tmodel = new ZWL.BLL.Token()
                {
                    CreatedTime = timestamp,
                    EnabledMark = 1,
                    UserName = PublicMethod.GetUserName(),
                    TokenValue = token,
                    ExpiresTime = exp.HasValue ? exp.Value.ToLocalTime() : DateTime.Now,
                    DeviceId = Request.UserAgent,
                    Type = "Sync"
                };
                tmodel.ID = tmodel.Add();
                currentId = tmodel.ID;
                var logModel = new ZWL.BLL.TokenLogs()
                {
                    TokenID = tmodel.ID,
                    AccessedTime = timestamp,
                    Action = "Saved",
                    IPAddress = Request.UserHostAddress,
                };
                logModel.ID = logModel.Add();

            }
            var list = Conv<ZWL.BLL.Token>.GetListBySQLWhere("UserName='{0}' and EnabledMark=1 and ID not in ({1})".FormatWith(username, currentId));
            if (list != null && list.Any())
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var item = list[i];
                    item.EnabledMark = 0;
                    item.Update();
                }
            }
            return JsonResult(true, msg);
        }
        private string PrintKaoQinDetailUserDeptSQL
        {
            get
            {
                var sql = @"select COALESCE(Name,UserName) UserName,COALESCE(TrueName,UserName) TrueName,COALESCE(BuMen,Department) BuMen,COALESCE(Department,b.BuMen) Department,ISNULL(ZWXZ, '劳务派遣人员') ZWXZ,ISNULL(Orderby, 3) Orderby,ISNULL(DisplayID, ISNULL(SortCode, 9999)) DisplayID,DirID  
                            from (
                                select e.*,cast(ISNULL(b.BackInfo, 9999) as int) DirID from ERPUser e LEFT JOIN ERPBuMen b on e.Department=b.BuMenName 
                                where IfLogin='是' and (Department not like '%离职' and UserName not like '%临时' and UserName not like '%管理员' and UserName<>'sj') 
                                and Department not in ('开发部','第三工程处','第四工程处')
                            ) u 
                            FULL join (
                            select XingMing Name,BuMen,'合同工' ZWXZ, 2 Orderby,SortCode from ERPMingCeHeTongZhiZhiGong where (DeleteMark is null or DeleteMark<>1)
                            UNION
                            select XingMing Name,BuMen,'劳务派遣人员' ZWXZ, 3 Orderby,SortCode from ERPMingCeLaoWuPaiQian where (DeleteMark is null or DeleteMark<>1)
                            UNION
                            select XingMing Name,BuMen,'返聘人员' ZWXZ, 4 Orderby,SortCode from ERPMingCeFanPin where (DeleteMark is null or DeleteMark<>1)
                            UNION
                            select XingMing Name,BuMen,'职工' ZWXZ, 1 Orderby,SortCode from ERPMingCeZaiBianZhiGong where (DeleteMark is null or DeleteMark<>1)
                            ) b on u.UserName=b.Name";
                return sql;
            }
        }
        private class LoginIntegBizSysResult
        {
            public int code { get; set; }
            public string msg { get; set; }
            public sdata data { get; set; }
            public class sdata
            {
                public string token { get; set; }
                public string theme { get; set; }
                public string wl_qrcode { get; set; }
            }
        }
        private class IntegBizSysOauthConfig
        {
            public int code { get; set; }
            public string msg { get; set; }
            public sdata data { get; set; }
            public class sdata
            {
                public int enableVerificationCode { get; set; }
                public int verificationCodeNumber { get; set; }
            }
        }
    }
}