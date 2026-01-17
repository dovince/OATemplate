using HtmlAgilityPack;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.Common;

public partial class CommonSelect_ChuChaiAlterModel : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var todoModel = new ZWL.BLL.ERPNWorkToDo();
            todoModel.GetModel(Id);
            var formContent = todoModel.FormContent;
            //if (todoModel.WorkName.Contains("变更"))
            //{
            //    var tempModel = new ZWL.BLL.ERPNWorkToDo(); 
            //    tempModel.GetModel(PublicMethod.GetInto(todoModel.BeiYong2));
            //    formContent = tempModel.FormContent;
            //}
            lblFormContent.Text = formContent;
            var t = RenameShenPiHtmlInput(formContent);
        }
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        var msg = string.Empty;
        if (Validate(ref msg))
        {
            var todoModel = new ZWL.BLL.ERPNWorkToDo();
            todoModel.GetModel(Id);
            WorkFlowId = todoModel.WorkFlowID.Value;
            var desc = Desc.Text;
            var doc = new HtmlDocument();
            doc.LoadHtml(RenameShenPiHtmlInput(GetHtml(todoModel.FormContent)));
            var html = @"<tr height='30'><td colspan='6' align='center'><b>变更信息说明</b></td></tr>";
            var html1 = @"<tr><td colspan='6' style='padding-left: 10px;'>{0}</td></tr>".FormatWith(desc.Replace(" ", "&nbsp;").Replace("\r\n", "<br>"));
            var tbodynode = doc.DocumentNode.SelectSingleNode("/div/table/tbody");
            if (tbodynode == null) tbodynode = doc.DocumentNode.SelectSingleNode("/div/div/table/tbody");
            if (tbodynode != null && tbodynode.ChildNodes.Any())
            {
                var newnode = HtmlNode.CreateNode(html);
                var newnode1 = HtmlNode.CreateNode(html1);
                tbodynode.InsertAfter(newnode, tbodynode.LastChild);
                tbodynode.InsertAfter(newnode1, tbodynode.LastChild);

                var ndoc = new HtmlDocument();
                ndoc.LoadHtml(todoModel.CurrentForm().ContentStr);
                var ntbodynode = ndoc.DocumentNode.SelectSingleNode("/div/table/tbody");
                if (ntbodynode == null) ntbodynode = ndoc.DocumentNode.SelectSingleNode("/div/div/table/tbody");
                for (int i = 0; i <= ntbodynode.ChildNodes.Count - 1; i++)
                {
                    var citem = ntbodynode.ChildNodes[i];//非基本信息行，即审批意见行
                    if (!citem.InnerHtml.IsNullOrEmpty() && citem.InnerHtml.Contains("selectyinzhang"))
                    {
                        tbodynode.InsertAfter(citem, tbodynode.LastChild);
                    }
                }

                var workName = GetWorkName(todoModel.WorkFlowID.Value).Replace("出差审批表", "出差变更审批流程").Replace("部门负责人出差", "出差变更审批流程");
                var infoModel = Common.ConverToTEntity<ZWL.BLL.ERPNWorkToDo>(todoModel);
                infoModel.ID = 0;
                infoModel.WorkName = workName;
                infoModel.TimeStr = Timestamp;
                infoModel.FormContent = doc.DocumentNode.OuterHtml;
                var secnode = GetSecondWorkFlowNode(todoModel.WorkFlowID.Value);
                infoModel.JieDianID = secnode.ID;
                infoModel.JieDianName = secnode.NodeName;
                infoModel.StateNow = "正在办理";
                infoModel.ShenPiUserList = PublicMethod.WorkWeiTuoUserList(GetShenPiUserList(secnode.SPType, secnode.SPDefaultList));
                infoModel.OKUserList = "默认";
                infoModel.LateTime = GetLateTime(infoModel.JieDianID);
                if (Action == "Edit")
                {
                    infoModel.ID = Id;
                    infoModel.BeiYong2 = todoModel.BeiYong2.ToString();
                    infoModel.Update();
                }
                else
                {
                    infoModel.BeiYong2 = todoModel.ID.ToString();
                    infoModel.ID = infoModel.Add();
                }
                WriteLog("用户修改新工作信息(" + infoModel.WorkName + ")");

                SendMainAndSms.SendMessage1(new CheckBox { Checked = true }, new CheckBox { Checked = true }, "您有新的工作需要办理！(" + infoModel.WorkName + ")", infoModel.ShenPiUserList, infoModel.FormID.Value, infoModel.WorkFlowID.Value, infoModel.BeiYong1);
                ClientScript.RegisterStartupScript(this.GetType(), "message", "alert('出差变更申请提交成功！');CCC();window.frameElement.src = window.frameElement.src;", true);

                //MessageBox.ShowAndRedirect(this, "出差变更申请提交成功！", "ChuChaiSPList.aspx?FormID=" + infoModel.FormID + "&WorkFlowID=" + infoModel.WorkFlowID);
            }

        }
        else
            MessageBox.Show(this, msg);
    }
    protected bool Validate(ref string msg)
    {
        var pass = true;
        if (Desc.Text.Trim().IsNullOrEmpty())
        {
            msg = "请输入出差变更信息说明。";
            pass = false;
        }
        return pass;
    }
    private string GetHtml(string html)
    {
        if (!string.IsNullOrEmpty(html) && html.Contains("Label_FormContent"))
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            for (int i = 0; i < doc.DocumentNode.ChildNodes.Count; i++)
            {
                var item = doc.DocumentNode.ChildNodes[i];
                if (item.Name == "span")
                {
                    return GetHtml(item.InnerHtml);
                }
            }
        }
        return html;
    }
    /// <summary>
    /// RenameShenPiHtmlInput避免重复添加审批栏导致input的id同名
    /// </summary>
    /// <param name="html"></param>
    /// <returns></returns>
    private string RenameShenPiHtmlInput(string html)
    {
        // 创建一个HtmlDocument对象 
        var doc = new HtmlDocument();
        // 加载html字符串
        doc.LoadHtml(GetHtml(html));
        // 遍历所有table节点 
        foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table"))
        {
            // 遍历table节点下的所有tr节点 
            var trlist = table.SelectNodes(".//tr").Where(r => !r.InnerHtml.IsNullOrEmpty() && r.InnerHtml.Contains("selectyinzhang"));
            if (trlist != null && trlist.Any())
            {
                var i = 0;
                foreach (var tr in trlist)
                {
                    var tdlist = tr.SelectNodes(".//td");
                    // 遍历td节点下的所有input、textarea、img节点 
                    if (tdlist == null || !tdlist.Any()) continue;
                    foreach (var td in tdlist)
                    {
                        var htmlInputs = td.SelectNodes(".//input|.//textarea|.//img");
                        if (htmlInputs == null || !htmlInputs.Any()) continue;
                        foreach (var node in htmlInputs)
                        {
                            var nodeseriel = PublicMethod.GeneratorNodeCode(i + 1);
                            // 获取节点的id和name属性 
                            HtmlAttribute id = node.Attributes["id"];
                            HtmlAttribute name = node.Attributes["name"];
                            var ids = string.Empty;
                            // 如果id属性存在，就修改它的值为"new_id"
                            if (id != null)
                            {
                                ids = PublicMethod.RemoveNumber(id.Value) + nodeseriel;
                                id.Value = ids;
                            }

                            // 如果name属性存在，就修改它的值为"new_name"
                            if (name != null)
                                name.Value = ids;

                            if (node.Name == "img")
                            {
                                HtmlAttribute onclick = node.Attributes["onclick"];
                                if (onclick != null)
                                    onclick.Value = "selectyinzhang({0});".FormatWith(ids);
                            }
                        }
                        i++;
                    }
                }
            }
        }
        return doc.DocumentNode.OuterHtml;
    }
}