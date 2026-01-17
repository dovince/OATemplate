using FSDZ.Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using ZWL.Common;

public partial class CommonSelect_YongCanYiChangAlert : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //lblFormTitle.Text = "收文分享";
            //TemplateExcelFilePath.NavigateUrl = "";
            //TemplateExcelFilePath.Text = "";//饭堂就餐数据导入

            //var username = "陈东旭,陈蓉蓉,陈晓坚,陈忠权,閤承松,何皓锋,黄红波,黄建霞,黄伟华,黄政涛,姜灵芝,姜伟,蒋秀珍,黎惠东,李亮,李亚军,梁志海,林家敏,罗勇,罗志明,骆碧婷,马威,谭新强,王首一,冼有才,肖荣军,谢静思,许栋山,叶永全,张丽红,张宗胜,张祖铭,钟诚,钟美红,安宁,樊仕华,黄冠滔,黎曙光,沈琳,许静,张大希,章瑞林";
            //var username = "吴达铭";
            //var ll = new { time = "2025-06-30" };
            //Mobile.SendSMS("佛山地质局", username,
            //@"TemplateCode_BanCanYongCanYiChangNotice_{0}".FormatWith(JsonHelper.Convert2Json(ll).ToBase64String()));
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        var msg = string.Empty;
        if (Validate(ref msg))
        {
            var sdate = SDate.Text.Trim();
            var edate = EDate.Text.Trim();
            //dynamic savedUsers = JsonConvert.DeserializeObject(SavedUsers.Text);
            //var t = @"[{""Name"":""吴达铭"",""Date"":""2025-09-02"",""Text"":""午""}]";
            JArray savedUsers = JsonConvert.DeserializeObject<JArray>(SavedUsers.Text);

            //var savedUsers = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("吴达铭", "早、午餐报餐未就餐") };

            //var users = Util.ReplaceSymbolsWithComma(ReceivedUser.Text.Trim());
            //var ulist = PublicMethod.GetSplitCollection(users);
            var deadline = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 5, 17, 30, 0);
            if (!Deadline.IsNullOrEmpty())
            {
                var dt = DateTime.Now;
                var deaddate = DateTime.Now;
                if (DateTime.TryParse(Deadline, out deaddate))
                {
                    if (dt.Year == deaddate.Year && dt.Month == deaddate.Month)
                    {
                        deadline = deaddate;
                    }
                }
            }
            foreach (var item in savedUsers)
            {
                //通知：${ name}，${ time} ，您出差期间在饭堂就餐（${ canshi}餐），若未领取生活补贴，
                //请于本月${ date}日下班前登录OA修改（计划领取补贴视为已领取）；逾期未改，系统默认已领取并扣除餐费。
                var data = new
                {
                    name = item["Name"].ToString(),
                    time = TimeParser.GetFormatDateString(item["Date"].ToString()),
                    canshi = item["Text"].ToString(),
                    date = deadline.Day
                };
                Mobile.SendSMS("佛山地质局", item["Name"].ToString(),
                @"TemplateCode_ChuChaiYongCanYiChangNotice_{0}".FormatWith(JsonHelper.Convert2Json(data).ToBase64String()));
            }

            WriteLog("出差期间领取生活补贴提醒({0}-{1})".FormatWith(sdate, edate));
            var sb = new StringBuilder();
            sb.AppendLine(MessageBox.CheckEasyUI);
            sb.AppendLine(MessageBox.ShowText);
            sb.AppendFormat("alert('{0}');CCC();window.parent.frameElement.src = window.parent.frameElement.src;", "操作成功！");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "scriptKey", sb.ToString(), true);
        }
        else
        {
            MessageBox.Show(this, msg);
        }

    }
    private bool Validate(ref string msg)
    {
        var result = true;
        var sdate = SDate.Text.Trim();
        var edate = EDate.Text.Trim();
        if (sdate.IsNullOrEmpty())
        {
            msg = "请选择一个有效日期";
            return false;
        }
        else
        {
            if (TimeParser.GetFormatDate(sdate) > DateTime.Now)
            {
                msg = "所选日期不能大于当前日期({0})".FormatWith(TimeParser.GetFormatDateString(DateTime.Now));
                return false;
            }
        }
        if (edate.IsNullOrEmpty())
        {
            msg = "请选择一个有效日期";
            return false;
        }
        else
        {
            if (TimeParser.GetFormatDate(edate) > DateTime.Now)
            {
                msg = "所选日期不能大于当前日期({0})".FormatWith(TimeParser.GetFormatDateString(DateTime.Now));
                return false;
            }
        }

        var users = Util.ReplaceSymbolsWithComma(ReceivedUser.Text.Trim());
        var ulist = PublicMethod.GetSplitCollection(users);
        try
        {
            var sqlWhere = " IfLogin='是' and  UserName in ({0})".FormatWith(PublicMethod.GetSplitInSQL(users, ","));
            var tlist = Conv<ZWL.BLL.ERPUser>.GetListBySQLWhere(sqlWhere);
            if (tlist == null || !tlist.Any() || tlist.Count != ulist.Count)
            {
                var ouser = users;
                if (tlist.Count != ulist.Count)
                {
                    var l = tlist.Select(e => e.UserName);
                    var elist = ulist.Except(l);
                    msg = "以下人员不存在，检查后再提交。（{0}）".FormatWith(string.Join(",", elist));
                    return false;
                }
            }
        }
        catch (Exception e)
        {
            Logger.Log(e);
        }
        return result;
    }
    private bool InsertToDB(DataTable dt, string nowname)
    {
        var result = false;
        return result;
    }
    #region MyRegion
    private string SQLFormat
    {
        get
        {
            return @"SELECT (case when Name is null then UserName else Name end) Name from (
SELECT DISTINCT Name,UserName from (

select b.[ID],b.[LotID],[Number],[Name],a.[UserName],[ZhiWu],b.[Sex],[RecordDate],[XingQi],[ShiJianDuan],[CanShi],[ShiJianDian],[KaoQinRecord],[Memo],[IsChuChai],[BCRQ],[IsCancel],u.[Department],'' as sfbc,'' as sfyc,u.[UserName] as xm,'' as rq,'' as xq,'' as ycsd,'' iserror,m.BackInfo,m.DirID,u.DisplayID
                        from FanTangJiuCanRecord b
                        join [Flow] f on f.LotID=b.LotID and Operation=1 and [KaoQinRecord] != '-'
                        FULL OUTER JOIN (select * from ERPBaoCan where IsCancel='否') a ON a.UserName = b.Name and a.BCRQ = b.[RecordDate] and a.ShiJianDian = b.CanShi
                        join ERPUser u on(u.[UserName] = a.[UserName] or u.[UserName] = b.[Name])
                        LEFT JOIN ERPBuMen m on u.Department=m.BuMenName
                        where  (RecordDate >= '{0}' or BCRQ >= '{0}')  and  (RecordDate < '{1}' or BCRQ < '{1}') 
                        and  (a.UserName is null or Name is null) and (a.UserName not in ('蔡晓帆','谢廷忠') or Name not in ('蔡晓帆','谢廷忠')) 

) t ) b";
        }
    }

    private string _deadline = "";
    private string Deadline
    {
        get
        {
            if (_deadline.IsNullOrEmpty())
            {
                var kModel = new ZWL.BLL.ERPKeyValue();
                kModel = kModel.GetModel("Category='YongCanModifyDeadline'");
                if (kModel != null && !kModel.Value1.IsNullOrEmpty())
                {
                    _deadline = kModel.Value1;
                }

            }
            return _deadline;
        }
    }
    #endregion
}