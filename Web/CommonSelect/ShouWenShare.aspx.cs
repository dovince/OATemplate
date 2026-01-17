using FSDZ.Logger;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.Common;

public partial class CommonSelect_ShouWenShare : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var shouModel = new ZWL.BLL.ERPShouWenBanWen();
            shouModel.NWorkGetModel(Id);
            lblTitle.Text = shouModel.TitleStr;
            //lblFormTitle.Text = "收文分享";
            //TemplateExcelFilePath.NavigateUrl = "";
            //TemplateExcelFilePath.Text = "";//饭堂就餐数据导入
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        var msg = string.Empty;
        if (Validate(ref msg))
        {
            var todoModel = new ZWL.BLL.ERPNWorkToDo();
            todoModel.GetModel(Id);
            var shouModel = new ZWL.BLL.ERPShouWenBanWen();
            shouModel.NWorkGetModel(Id);
            var lotid = PublicMethod.GenerateGuid();
            var shareModel = new ZWL.BLL.ERPShouWenBanWenShare()
            {
                LotId = lotid,
                DocId = shouModel.ID,
                TitleStr = shouModel.TitleStr,
                UserName = PublicMethod.GetUserName(),
                Department = PublicMethod.GetDepartment(),
                CreatedTime = Timestamp,
            };
            shareModel.ID = shareModel.Add();
            var users = Util.ReplaceSymbolsWithComma(ReceivedUser.Text.Trim());
            var ulist = PublicMethod.GetSplitCollection(users);
            foreach (var item in ulist)
            {
                var shareRModel = new ZWL.BLL.ERPShouWenBanWenShareRecipient()
                {
                    DocId = shouModel.ID,
                    Receiver = item,
                    State = "未查看"
                };
                shareRModel.ID = shareRModel.Add();
            }
            var msgContent = "您有新的工作需要办理！(" + todoModel.WorkName + ")";
            var CHKSMS = new CheckBox() { Checked = true };
            var CHKMOB = new CheckBox() { Checked = true };
            SendMainAndSms.SendMessage(CHKSMS, CHKMOB, msgContent, users, todoModel.FormID.Value, todoModel.WorkFlowID.Value, todoModel.BeiYong1, todoModel.ID);

            WriteLog("用户添加转发收文信息({0})".FormatWith(lotid));
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

    #endregion
}