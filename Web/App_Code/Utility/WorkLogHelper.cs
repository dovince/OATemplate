using Aspose.Words;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using ZWL.Common;
using ZWL.DBUtility;

/// <summary>
/// WorkLogHelper 的摘要说明
/// </summary>
public class WorkLogHelper
{
    public static bool WriteWorkLog(int workid, string name, string operation, string action, string yinzhangpath)
    {
        var result = true;
        var work = new ZWL.BLL.ERPNWorkToDo();
        var username = PublicMethod.GetUserName();
        work.GetModel(workid);
        var shenpiyijian = PublicMethod.GetLastShenPiyijian(work.ShenPiYiJian);
        var recordId = work.JieDianID.Value;
        if (name == "WorkFlow" && (operation == ZWL.BLL.Opration.Submit.ToString() || operation == ZWL.BLL.Opration.Modified.ToString())
            && username == work.UserName)
        {
            var node = new ZWL.BLL.ERPNWorkFlowNode();
            node.GetModel("WorkFlowID=" + work.WorkFlowID + " and NodeAddr='开始'");
            if (node.ID > 0)
                recordId = node.ID;
        }
        if (yinzhangpath == "")
        {
            yinzhangpath = DbHelperSQL.GetSHSL("SELECT TOP 1 [ImgPath] FROM [ERPYinZhang] where [UserName]='" + username + "'");
        }
        //var count =
        //    DbHelperSQL.GetSHSL(
        //        "SELECT COUNT([ID]) FROM [ERPNWorkToDoLog] " +
        //        "where [ParentID]='" + workid + "' and [RecordID]='" + recordId + "' and [Action]='" + action + "' and [Operation]='" + operation + "' and [UserName]='" + username + "'");
        //if (count == "0")
        //{
        var desc = string.Empty;
        if (shenpiyijian != null && operation.ToLower() != "submit" && shenpiyijian.UserName == username)
        {
            desc = shenpiyijian.Comment;
        }
        var model = new ZWL.BLL.ERPNWorkToDoLog()
        {
            Name = name,
            UniqueID = PublicMethod.GenerateGuid(),
            ParentID = workid,
            RecordID = recordId,
            Operation = operation,
            Action = action,
            StateNow = work.StateNow,
            ShenPiUserList = work.ShenPiUserList,
            OKUserList = work.OKUserList,
            TimeStamp = DateTime.Now,
            UserName = username,
            Description = desc,
            YinZhangPath = yinzhangpath
        };
        result = model.Add() > 0;
        return result;
        //}
        //else
        //{
        //    return true;
        //}
    }
    public static bool WriteWorkFlowLog(int workid, string operation, string state)
    {
        return WriteWorkLog(workid, "WorkFlow", operation, state, "");
    }
    public static bool WriteWorkFlowLog(int workid, string operation, string state, string yinzhangpath)
    {
        return WriteWorkLog(workid, "WorkFlow", operation, state, yinzhangpath);
    }
    public static bool WriteWorkLog(int workid, int recordId, string name, string username, string operation, string action, string stateNow, string shenPiUserList, string oKUserList, DateTime time, string yinzhangpath)
    {
        var result = true;
        if (yinzhangpath == "" && username != "")
        {
            yinzhangpath = DbHelperSQL.GetSHSL("SELECT TOP 1 [ImgPath] FROM [ERPYinZhang] where [UserName]='" + username + "'");
        }
        //var count =
        //DbHelperSQL.GetSHSL(
        //"SELECT COUNT([ID]) FROM [ERPNWorkToDoLog] " +
        //"where [ParentID]='" + workid + "' and [RecordID]='" + recordId + "' and [Action]='" + action + "' and [Operation]='" + operation + "' and [UserName]='" + username + "'");
        //if (count == "0")
        //{
        var model = new ZWL.BLL.ERPNWorkToDoLog()
        {
            Name = name,
            UniqueID = PublicMethod.GenerateGuid(),
            ParentID = workid,
            RecordID = recordId,
            Operation = operation,
            Action = action,
            StateNow = stateNow,
            ShenPiUserList = shenPiUserList,
            OKUserList = oKUserList,
            TimeStamp = time,
            UserName = username,
            Description = "",
            YinZhangPath = yinzhangpath
        };
        result = model.Add() > 0;
        return result;
        //}
        //else
        //{
        //return true;
        //}
    }
    private static List<int> SortingAscList
    {
        get
        {
            return new List<int> { 48 };
        }
    }
    public static List<ProcessingViewModel> GetProcessingList(int id)
    {
        var source = new List<ProcessingViewModel>();
        var workLog = new ZWL.BLL.ERPNWorkToDoLog();
        var work = new ZWL.BLL.ERPNWorkToDo();
        work.GetModel(id);
        var list = workLog.GetModelList("Name='WorkFlow' and ParentID=" + id);
        var yList = PublicMethod.GetShenPiYiJianList(work.ShenPiYiJian);
        if (list.Count >= yList.Count && work.FormID != 49)
        {
            var i = 1;
            var node = new ZWL.BLL.ERPNWorkFlowNode();
            var rlist = list.OrderByDescending(r => r.TimeStamp);
            if (SortingAscList.Contains(work.FormID.Value))
            {
                rlist = list.OrderBy(r => r.TimeStamp);
            }
            foreach (var item in rlist)
            {
                node.GetModel(PublicMethod.GetInt(item.RecordID));
                source.Add(new ProcessingViewModel
                {
                    ID = i,
                    NodeName = node.NodeName,
                    Operation = item.Operation,
                    Action = item.Action,
                    Description = item.Description,
                    Timestamp = TimeParser.GetFormatTimeString(item.TimeStamp),
                    UserName = item.UserName,
                });
                i++;
            }
        }
        else
        {
            //yList.Insert(yList.Count, new ShenPiyijian { ID = yList.Count + 1, UserName = work.UserName, TimeStamp = TimeParser.GetFormatTimeString(work.TimeStr) });
            var node = new ZWL.BLL.ERPNWorkFlowNode();
            var i = 1;
            var rlist = yList.OrderByDescending(r => TimeParser.GetFormatTimeString(r.TimeStamp));
            if (SortingAscList.Contains(work.FormID.Value))
            {
                rlist = yList.OrderBy(r => TimeParser.GetFormatTimeString(r.TimeStamp));
            }
            foreach (var item in rlist)
            {
                var action = "";
                if (i == 1)
                    action = GetAction(work.StateNow);
                else
                    //if (i != yList.Count)
                    action = "Agree";
                node.GetModel(item.NodeID);
                source.Add(new ProcessingViewModel
                {
                    ID = i,
                    NodeName = item.NodeID > 0 ? node.NodeName : item.NodeName,
                    //Operation = i == yList.Count ? "Submit" : "Approved",
                    Operation = "Approved",
                    Action = action,
                    Description = item.Comment,
                    Timestamp = item.TimeStamp,
                    UserName = item.UserName,
                });
                i++;
            }
        }
        if (!source.Any(e => e.UserName == work.UserName && e.Operation == ZWL.BLL.Opration.Submit.ToString()))
        {
            var info = new ProcessingViewModel
            {
                ID = 1,
                NodeName = "申请",
                Operation = ZWL.BLL.Opration.Submit.ToString(),
                Action = "Approved",
                Timestamp = TimeParser.GetFormatTimeString(work.TimeStr),
                UserName = work.UserName,
            };
            if (SortingAscList.Contains(work.FormID.Value))
            {
                source.Insert(0, info);
            }
            else
            {
                source.Add(info);
            }
        }
        return source;
    }
    public static string GetProcessingHtml(int id)
    {
        return GetProcessingHtml(id, true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">Work To Do ID</param>
    /// <param name="isClosed">是否暂时关闭,返回空串</param>
    /// <returns></returns>
    public static string GetProcessingHtml(int id, bool isClosed)
    {
        if (isClosed)
            return "";
        var html = string.Empty;
        var list = GetProcessingList(id);
        var htmlFormat = @"<tr>
                                <td>{0}</td>
                                <td>{1}</td>
                                <td>{4}</td>
                                <td style='{6}'>{2}</td>
                                <td>{5}</td>
                                <td style='width:35%'>
                                    <div align='left' style='max-width: 98%;text-overflow: ellipsis;overflow-x: hidden;overflow-y: scroll; max-height: 60px;' title='{3}'>{3}</div>
                                </td>
                            </tr>";
        var todoModel = new ZWL.BLL.ERPNWorkToDo();
        todoModel.GetModel(id);
        var i = list.Count;
        if (SortingAscList.Contains(todoModel.FormID.Value))
        {
            i = 1;
        }
        foreach (var item in list)
        {
            var desc = item.Description;
            if (desc != null && !desc.IsNullOrEmpty())
            {
                var htmldoc = new HtmlAgilityPack.HtmlDocument();
                htmldoc.LoadHtml(desc);
                if (desc.Contains("UploadFile"))
                {
                    var slist = Regex.Split(desc, "<br>", RegexOptions.IgnoreCase);
                    if (slist != null && slist.Count() > 1)
                    {
                        var fitem = slist[0];
                        var sitem = slist[1];
                        htmldoc.LoadHtml(fitem);
                        desc = htmldoc.DocumentNode.InnerText + sitem;
                    }
                    else
                        desc = htmldoc.DocumentNode.InnerText;
                }
                else
                    desc = htmldoc.DocumentNode.InnerText;
            }
            var op = EnumHelper.ToEnum<ZWL.BLL.Opration>(item.Operation);
            switch (op)
            {
                case ZWL.BLL.Opration.Submit:
                    html += string.Format(htmlFormat, i, item.NodeName,
                        EnumHelper.GetDescription(EnumHelper.ToEnum<ZWL.BLL.Opration>(item.Operation)),
                        desc, item.UserName, item.Timestamp, "");
                    break;
                case ZWL.BLL.Opration.Modified:
                    html += string.Format(htmlFormat, i, item.NodeName,
                        EnumHelper.GetDescription(EnumHelper.ToEnum<ZWL.BLL.Opration>(item.Operation)),
                        desc, item.UserName, item.Timestamp, "");
                    break;
                case ZWL.BLL.Opration.Approved:
                case ZWL.BLL.Opration.Forward:
                    if (!string.IsNullOrEmpty(item.Action))
                    {
                        var color = "color:yellow";
                        var st = EnumHelper.ToEnum<ZWL.BLL.Action>(item.Action);
                        switch (st)
                        {
                            case ZWL.BLL.Action.Agree:
                                color = "color:green";
                                if (op == ZWL.BLL.Opration.Forward)
                                    color = "color:YellowGreen";
                                break;
                            case ZWL.BLL.Action.Return:
                                color = "color:blue";
                                break;
                            case ZWL.BLL.Action.Reject:
                                color = "color:red";
                                break;
                        }
                        //20250214 江庆梅要求办公室分发的操作改名为分发，备注内容去掉
                        if(item.NodeName == "办公室分发" && st == ZWL.BLL.Action.Agree)
                        {
                            html += string.Format(htmlFormat, i, item.NodeName, "分发", "",
                                item.UserName, TimeParser.GetFormatTimeString(item.Timestamp), color);
                            break;
                        }
                        html += string.Format(htmlFormat, i, item.NodeName,
                EnumHelper.GetDescription(EnumHelper.ToEnum<ZWL.BLL.Opration>(item.Operation))
                + EnumHelper.GetDescription(EnumHelper.ToEnum<ZWL.BLL.Action>(item.Action)),
                desc, item.UserName, TimeParser.GetFormatTimeString(item.Timestamp), color);
                    }
                    break;
                default:

                    break;
            }
            if (SortingAscList.Contains(todoModel.FormID.Value))
                i++;
            else
                i--;
        }
        return html;
    }
    public static string GetProcessingPrintWorkHtml(int id, bool isClosed)
    {
        if (isClosed)
            return "";
        var html = "<table style='width:100%;margin: 0;text-align:center;'>";
        var list = GetProcessingList(id);
        var htmlFormat = @"<tr style='height:45px;'>
                                <td>{0}</td>
                                <td style='width:60%'>
                                    <div align='left' style='max-width: 98%;text-overflow: ellipsis;overflow-x: hidden;overflow-y: auto; max-height: 60px;'>{1}</div>
                                </td>
                                <td>{2}</td>
                                <td><img class='HerCss' src='{3}'/></td>
                            </tr>";
        var todoModel = new ZWL.BLL.ERPNWorkToDo();
        todoModel.GetModel(id);
        var i = list.Count;
        if (SortingAscList.Contains(todoModel.FormID.Value))
        {
            i = 1;
        }
        foreach (var item in list)
        {
            var desc = item.Description;
            if (desc != null && !desc.IsNullOrEmpty())
            {
                var htmldoc = new HtmlAgilityPack.HtmlDocument();
                htmldoc.LoadHtml(desc);
                if (desc.Contains("UploadFile"))
                {
                    var slist = Regex.Split(desc, "<br>", RegexOptions.IgnoreCase);
                    if (slist != null && slist.Count() > 1)
                    {
                        var fitem = slist[0];
                        var sitem = slist[1];
                        htmldoc.LoadHtml(fitem);
                        desc = htmldoc.DocumentNode.InnerText + sitem;
                    }
                    else
                        desc = htmldoc.DocumentNode.InnerText;
                }
                else
                    desc = htmldoc.DocumentNode.InnerText;
            }
            var op = EnumHelper.ToEnum<ZWL.BLL.Opration>(item.Operation);
            switch (op)
            {
                //case ZWL.BLL.Opration.Submit:
                //    html += string.Format(htmlFormat, i, item.NodeName,
                //        EnumHelper.GetDescription(EnumHelper.ToEnum<ZWL.BLL.Opration>(item.Operation)),
                //        desc, item.UserName, item.Timestamp, "");
                //    break;
                //case ZWL.BLL.Opration.Modified:
                //    html += string.Format(htmlFormat, i, item.NodeName,
                //        EnumHelper.GetDescription(EnumHelper.ToEnum<ZWL.BLL.Opration>(item.Operation)),
                //        desc, item.UserName, item.Timestamp, "");
                //    break;
                case ZWL.BLL.Opration.Approved:
                case ZWL.BLL.Opration.Forward:
                    if (!string.IsNullOrEmpty(item.Action))
                    {
                        var color = "color:yellow";
                        var st = EnumHelper.ToEnum<ZWL.BLL.Action>(item.Action);
                        switch (st)
                        {
                            case ZWL.BLL.Action.Agree:
                                color = "color:green";
                                if (op == ZWL.BLL.Opration.Forward)
                                    color = "color:YellowGreen";
                                break;
                            case ZWL.BLL.Action.Return:
                                color = "color:blue";
                                break;
                            case ZWL.BLL.Action.Reject:
                                color = "color:red";
                                break;
                        }
                        //20250214 江庆梅要求办公室分发的操作改名为分发，备注内容去掉
                        //if (item.NodeName == "办公室分发" && st == ZWL.BLL.Action.Agree)
                        //{
                        //    html += string.Format(htmlFormat, i, item.NodeName, "分发", "",
                        //        item.UserName, TimeParser.GetFormatTimeString(item.Timestamp), color);
                        //    break;
                        //}
                        ZWL.BLL.ERPYinZhang yinzhang = new ZWL.BLL.ERPYinZhang();
                        var yzlist = yinzhang.GetListModel(" YinZhangLeiBie='私人印章' and UserName='" + item.UserName + "' order by ID desc");
                        if (yzlist.Any())
                        {
                            html += string.Format(htmlFormat, item.NodeName, desc, Convert.ToDateTime(item.Timestamp).ToString("yyyy-MM-dd"), "../UploadFile/" + yzlist[0].ImgPath);
                        }
                        else
                        {
                            html += string.Format(htmlFormat, item.NodeName, desc, Convert.ToDateTime(item.Timestamp).ToString("yyyy-MM-dd"), item.UserName);
                        }
                    }
                    break;
                default:

                    break;
            }
            if (SortingAscList.Contains(todoModel.FormID.Value))
                i++;
            else
                i--;
        }
        html += "</table>";
        return html;
    }
    private static string GetAction(string stateNow)
    {
        var result = string.Empty;
        if (stateNow == "正在办理"
            || stateNow == "正常结束")
            result = "Agree";
        if (stateNow == "已被驳回")
            result = "Return";
        if (stateNow == "不通过")
            result = "Reject";
        return result;
    }
    protected static DateTime GetLateTime(int? nodeid)
    {
        var hours = DbHelperSQL.GetSHSLInt("select top 1 JieShuHours from ERPNWorkFlowNode where ID=" + nodeid);
        return DateTime.Now.AddHours(double.Parse(hours));
    }
    protected static int GetFirstNodeID(int workflowid)
    {
        return DbHelperSQL.GetSHSLInt1("select ID from ERPNWorkFlowNode where WorkFlowID=" + workflowid + " and NodeAddr='开始'");
    }
}