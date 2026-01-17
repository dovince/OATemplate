using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ZWL.Common;

public partial class NWorkFlow_NWorkFlowState : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadProcessStateInput(Id);
        }
    }

    private void LoadProcessStateInput(int id)
    {
        var Model = new ZWL.BLL.ERPNWorkToDo();
        Model.GetModel(id);
        this.lblWorkName.Text = Regex.Split(Model.WorkName, "--", RegexOptions.IgnoreCase)[1];
        this.lblUserName.Text = Model.UserName.ToString();
        this.lblTimeStr.Text = Model.TimeStr.ToString();
        //this.lblFuJianList.Text = PublicMethod.GetWenJian(Model.FuJianList.ToString(), "../UpLoadFile/");
        this.lblJieDianName.Text = Model.JieDianName.ToString();
        this.lblShenPiUserList.Text = Model.ShenPiUserList.ToString();
        this.lblOKUserList.Text = Model.OKUserList.ToString();
        this.lblStateNow.Text = Model.StateNow.ToString();
        this.lblLateTime.Text = Model.LateTime.ToString();
        //var list = Model.ShenPiUserList.Split(',');
        //if (list != null && list.Length > 0)
        //{
        //    var temp = string.Empty;
        //    var list1 = new List<string>();
        //    var tList = Model.OKUserList.Split(',');
        //    if (tList != null && tList.Length > 0)
        //    {
        //        foreach (var item in tList)
        //        {
        //            list1.Add(item);
        //        }
        //    }
        //    foreach (var item in list)
        //    {
        //        if (!list1.Contains(item))
        //            temp += item + ",";
        //    }
        //    if (!string.IsNullOrEmpty(temp))
        //        this.lblNotOKUserList.Text = temp.TrimEnd(',');
        //}
    }
}