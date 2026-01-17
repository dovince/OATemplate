using System;
using System.Collections;
using System.Data;
using ZWL.Common;
using System.Linq;
using System.Collections.Generic;

public partial class UserControls_RebuildForm : System.Web.UI.UserControl
{
    public string BaseInfoBodyHtml = string.Empty;
    public string PiLiangSet = string.Empty;
    public bool ShowTransform = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadInputData();
    }
    private void LoadInputData()
    {
        var workId = PublicMethod.CheckInt(PublicMethod.GetDecryptParam("ID"));
        var Model = new ZWL.BLL.ERPNWorkToDo();
        Model.GetModel(workId);
        if (Model.ID > 0)
        {
            var formModel = new ZWL.BLL.ERPNForm();
            formModel.GetModel(Model.FormID.Value);
            lblFormTitle.Text = formModel.FormName;
            BaseInfoBodyHtml = GetBaseInfoBodyHtml(Model);
            Label_FormContent.Text = Model.FormContent;
            var signimgid = string.Empty;
            if (!string.IsNullOrEmpty(Request.AppRelativeCurrentExecutionFilePath) && Request.AppRelativeCurrentExecutionFilePath.ToLower().Contains("dowork"))
                PiLiangSet = Util.FormInputCanWriteSet(Model.ID, ref signimgid);
            ShowTransform = Util.ShowTransformSetting(Model);
        }
    }

    private string GetBaseInfoBodyHtml(ZWL.BLL.ERPNWorkToDo WorkToDo)
    {
        var result = string.Empty;
        var parse = new ParseHtml();
        var ds = parse.GetDataSetFormHTML(WorkToDo.FormContent);
        ds = Util.GetRebuildFormExtend(ds, parse.GetAttList(), WorkToDo.ID);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            var dt = ds.Tables[0];
            var list = dt.Rows;
            var subTableFlag = dt.AsEnumerable().Any(r => PublicMethod.IsContainNumber(r["Name"].ToString()));
            if (subTableFlag)
            {
                var listIds = new List<int>();
                var headers = new Dictionary<int, string>();
                for (int i = 0; i < list.Count; i++)
                {
                    var item = list[i];
                    var id = PublicMethod.GetInt(item["ID"].ToString());
                    var name = item["Name"].ToString();
                    if (PublicMethod.IsContainNumber(name))
                    {
                        listIds.Add(id);
                        var text = name.Replace(PublicMethod.GetNumeric(name), "");
                        if (!headers.Any(r => r.Value == text))
                            headers.Add(headers.Count + 1, text);
                    }
                }
                var dt1 = dt.Clone();
                var dt2 = dt.Clone();
                var dt3 = dt.Clone();
                var min = listIds.Min();
                var max = listIds.Max();
                for (int i = 0; i < list.Count; i++)
                {
                    var item = list[i];
                    var id = PublicMethod.GetInt(item["ID"].ToString());
                    if (id < min)
                    {
                        dt1.ImportRow(item);
                        if (dt1.Rows.Count == min - 1)
                        {
                            result += HandleDataToHtml(dt1.Rows);
                        }
                    }
                    else if (listIds.Contains(id))
                    {
                        dt2.ImportRow(item);
                        if (dt2.Rows.Count == headers.Count)
                        {
                            result += HandleDataToHtml(dt2.Rows);
                            dt2 = dt.Clone();
                        }
                    }
                    else if (id > max)
                    {
                        dt3.ImportRow(item);
                    }
                }
                result += HandleDataToHtml(dt3.Rows);
            }
            else
            {
                result = HandleDataToHtml(list);
            }

        }
        return result;
    }
    private string HandleDataToHtml(DataRowCollection list)
    {
        var result = new System.Text.StringBuilder();
        if (list != null && list.Count > 0)
        {
            var first = new Queue();
            var second = new Queue();
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                var name = item["Name"].ToString();
                var val = item["Value"].ToString();
                var cols = item["Cols"].ToString();
                if (cols == "1")
                    second.Enqueue(item);
                if (cols == "2")
                    first.Enqueue(item);
                if (first.Count == 2)
                {
                    var f = (DataRow)first.Dequeue();
                    var s = (DataRow)first.Dequeue();
                    result.AppendFormat(TowColsFormat, f["Name"], f["Value"], s["Name"], s["Value"]);
                    if (second.Count > 0)
                    {
                        for (int j = 0; j < second.Count; j++)
                        {
                            var d = (DataRow)second.Dequeue();
                            result.AppendFormat(OneColsFormat, d["Name"], d["Value"]);
                        }
                    }
                }
                if (first.Count == 1 && (i == list.Count - 1))
                {
                    var d = (DataRow)first.Dequeue();
                    result.AppendFormat(OneColsFormat, d["Name"], d["Value"]);
                }
                if (second.Count > 0)
                {
                    for (int j = 0; j < second.Count; j++)
                    {
                        var d = (DataRow)second.Dequeue();
                        result.AppendFormat(OneColsFormat, d["Name"], d["Value"]);
                    }
                }
            }
        }
        return result.ToString();
    }
    private readonly string TowColsFormat = @"<tr>
                                    <td class='td_normal_title' width='15%'>{0}</td>
                                    <td width='35%'><div>{1}</div></td>
                                    <td class='td_normal_title' width='15%'>{2}</td>
                                    <td width='35%'><div>{3}</div></td>
                                </tr>";
    private readonly string OneColsFormat = @"<tr>
                                                        <td class='td_normal_title' width='15%'>{0}</td>
                                                        <td colspan='3' width='83.0%'>
                                                            <div>{1}</div>
                                                        </td>
                                                    </tr>";
}