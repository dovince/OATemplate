using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using ZWL.Common;

public partial class CommonSelect_FlowForward : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
            var text = fdUsageContent.Text;
            var PiShiStr = "<font color=#0000FF>" + UserName + "&nbsp;&nbsp;" + DateTime.Now.ToString() + "&nbsp;&nbsp;</font><BR><div class=\"showShenPiYiJianFormat\">" +
                "已阅" + "</div>" + "" + "<hr>";
            var shenpiuser = ShenPiUser.Text.Trim();
            shenpiuser = Util.ReplaceSymbolsWithComma(shenpiuser);
            var ulist = todoModel.ShenPiUserList.Split(',').ToList();
            var tlist = shenpiuser.Split(",").ToList();
            var slist = new List<string>();
            foreach (var item in tlist)
            {
                if (!ulist.Contains(item))
                    slist.Add(item);
            }
            todoModel.ShenPiUserList = PublicMethod.WorkWeiTuoUserList(todoModel.ShenPiUserList + "," + string.Join(",", slist));
            todoModel.OKUserList += "," + UserName;
            todoModel.ShenPiYiJian = PiShiStr + todoModel.ShenPiYiJian;
            todoModel.Update();

            var wfuser = new ZWL.BLL.ERPNWorkFlowToDoUser();
            wfuser.ToDoID = todoModel.ID;
            wfuser.NodeID = todoModel.JieDianID.Value;
            wfuser.WorkFlowID = int.Parse(todoModel.WorkFlowID.ToString());
            wfuser.ShenPiUserList = PublicMethod.WorkWeiTuoUserList(shenpiuser.Trim());

            if (wfuser.Exists(wfuser.ToDoID, wfuser.NodeID))
            {
                wfuser.UpdateUser();
            }
            else
            {
                wfuser.Add();
            }

            WorkLogHelper.WriteWorkFlowLog(todoModel.ID, ZWL.BLL.Opration.Approved.ToString(), ZWL.BLL.Action.Agree.ToString(), "");
            var logModel = new ZWL.BLL.ERPNWorkToDoLog()
            {
                Name = todoModel.WorkName,
                UniqueID = PublicMethod.GenerateGuid(),
                ParentID = todoModel.ID,
                RecordID = todoModel.JieDianID,
                Operation = ZWL.BLL.Opration.Forward.ToString(),
                Action = ZWL.BLL.Action.Agree.ToString(),
                StateNow = todoModel.StateNow,
                ShenPiUserList = todoModel.ShenPiUserList,
                OKUserList = todoModel.OKUserList,
                TimeStamp = Timestamp,
                UserName = UserName,
                Description = text + "【转发至：{0}】".FormatWith(shenpiuser),
            };
            logModel.ID = logModel.Add();
            todoModel.GetModel(Id);
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
        var shenpiuser = ShenPiUser.Text.Trim();
        if (shenpiuser.IsNullOrEmpty())
        {
            msg = "请选择接收人。";
            return false;
        }
        else
        {
            shenpiuser = Util.ReplaceSymbolsWithComma(shenpiuser);
            var list = shenpiuser.Split(',');
            if (list == null || list.Length <= 0)
            {
                msg = "请选择接收人。";
                return false;
            }
            else
            {
                var ulist = new List<string>();
                for (int i = 0; i < list.Length; i++)
                {
                    var item = list[i];
                    if (item.IsNullOrEmpty()) continue;
                    ulist.Add(item.Trim());
                }
                var umodel = new ZWL.BLL.ERPUser();
                var userlist = umodel.GetListModel("UserName in ({0})".FormatWith(PublicMethod.GetSplitInSQL(shenpiuser, ",")));
                if (userlist == null || !userlist.Any())
                {
                    msg = "请选择接收人。";
                    return false;
                }
                foreach (var item in ulist)
                {
                    if (!userlist.Any(x => x.UserName == item))
                    {
                        msg = "【{0}】不存在。".FormatWith(item);
                        return false;
                    }
                }
            }
        }
        return result;
    }
    #region MyRegion

    #endregion
}