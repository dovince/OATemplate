using HtmlAgilityPack;
using Newtonsoft.Json;
using RequestJob;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;
using ViewModels;
using ZWL.Common;
using ZWL.DBUtility;

/// <summary>
/// Common 的摘要说明
/// </summary>
public class Util
{
    public static readonly string OperatingManagement = "";
    public static readonly string SuperAdmin = "超级管理员";
    public static readonly string TheLeader = "队领导";
    public static readonly string FinanceDepart = "财务科";
    public static readonly string HeadOffice = "综合办公室";
    public static readonly string EngineerOffice = "总工程师办公室";
    public static readonly string DataDepart = "资料室";
    public static readonly string PersonnelDepart = "人事科";
    public static List<ConnectedUser> ConnectedUsers = new List<ConnectedUser>();
    public Util()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 获取文本编辑器的数据，并自动上传远程图片
    /// </summary>
    /// <param name="uc">文本编辑器数据</param>
    /// <returns></returns>
    public string GetText(string str)
    {
        string mycontext = Regex.Replace(str, @"src[^>]*[^/].(?:jpg|bmp|gif|png|jpeg|JPG|BMP|GIF|JPEG)(?:\""|\')", new MatchEvaluator(SaveYuanFile));

        return mycontext;
    }



    private string SaveYuanFile(Match m)
    {
        string imgurl = "";
        string matchstr = m.Value;//str[i].ToString();
        string tempimgurl = "";
        tempimgurl = matchstr.Substring(5);
        tempimgurl = tempimgurl.Substring(0, tempimgurl.IndexOf("\""));

        Regex re = new Regex(@"^http://*");
        if (re.Match(tempimgurl).Success)
        {
            matchstr = matchstr.Substring(5);
            matchstr = matchstr.Substring(0, matchstr.IndexOf("\""));

            //Response.Write(matchstr + "<br>");

            //远程文件保存路径
            string Folders = ConfigurationManager.AppSettings["yuanimg"].ToString();
            string fullname = matchstr;

            string huozui = fullname.Substring(fullname.LastIndexOf("."));
            string filename = Util.GetFileName();
            string path = Folders + filename + huozui;
            //Folders+fullname.Substring(fullname.LastIndexOf("\\") + 1);

            if (System.IO.File.Exists(System.Web.HttpContext.Current.Request.MapPath(path)))
                System.IO.File.Delete(System.Web.HttpContext.Current.Request.MapPath(path));
            GetHttpFile(matchstr, System.Web.HttpContext.Current.Request.MapPath(path));
            imgurl = "src=\"" + path.Replace("~/", "") + "\"";
        }
        else
        {
            imgurl = matchstr;
        }


        return imgurl;
    }


    string sException = null;
    private bool GetHttpFile(string sUrl, string sSavePath)
    {
        bool bRslt = false;
        WebResponse oWebRps = null;
        WebRequest oWebRqst = WebRequest.Create(sUrl);
        oWebRqst.Timeout = 100000;
        try
        {
            oWebRps = oWebRqst.GetResponse();
        }
        catch (WebException e)
        {
            sException = e.Message.ToString();
        }
        catch (Exception e)
        {
            sException = e.ToString();
        }
        finally
        {
            if (oWebRps != null)
            {
                BinaryReader oBnyRd = new BinaryReader(oWebRps.GetResponseStream(), System.Text.Encoding.GetEncoding("GB2312"));
                int iLen = Convert.ToInt32(oWebRps.ContentLength);
                FileStream oFileStream;
                try
                {
                    if (File.Exists(System.Web.HttpContext.Current.Request.MapPath("RecievedData.tmp")))
                    {
                        oFileStream = File.OpenWrite(sSavePath);
                    }
                    else
                    {
                        oFileStream = File.Create(sSavePath);
                    }
                    oFileStream.SetLength((Int64)iLen);
                    oFileStream.Write(oBnyRd.ReadBytes(iLen), 0, iLen);
                    oFileStream.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    oBnyRd.Close();
                    oWebRps.Close();

                }
                bRslt = true;

            }
        }
        return bRslt;

    }

    /// <summary>
    /// 文件上传
    /// </summary>
    /// <param name="fileupload">文件上传实例</param>
    /// <returns>保存的文件名称</returns>
    public static string UpLoadFile(FileUpload fileupload, string Folders)
    {
        //string Folders = "~/admin/eWebEditor/UpLoadFile/";
        string fullname = fileupload.PostedFile.FileName;
        if ((fullname == null) || (fullname.Equals("")))
            return "";
        string huozui = fullname.Substring(fullname.LastIndexOf("."));
        string filename = GetFileName();
        string p1 = Folders + filename + huozui;
        //Folders + fullname.Substring(fullname.LastIndexOf("\\") + 1); 
        string path = System.Web.HttpContext.Current.Server.MapPath(p1);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);
        fileupload.PostedFile.SaveAs(path);
        return p1;
    }
    public static string GetFileName()
    {
        //System.Threading.Thread.Sleep(1000);
        string str1 = System.DateTime.Now.Year.ToString() + "-";

        if ((System.DateTime.Now.Month).ToString().Length < 2)
        {
            str1 += "0" + System.DateTime.Now.Month.ToString() + "-";
        }
        else
        {
            str1 += System.DateTime.Now.Month.ToString() + "-";
        }

        if ((System.DateTime.Now.Day).ToString().Length < 2)
        {
            str1 += "0" + System.DateTime.Now.Day.ToString() + "-";
        }
        else
        {
            str1 += System.DateTime.Now.Day.ToString() + "-";
        }

        if ((System.DateTime.Now.Hour).ToString().Length < 2)
        {
            str1 += "0" + System.DateTime.Now.Hour.ToString() + "-";
        }
        else
        {
            str1 += System.DateTime.Now.Hour.ToString() + "-";
        }

        if ((System.DateTime.Now.Minute).ToString().Length < 2)
        {
            str1 += "0" + System.DateTime.Now.Minute.ToString() + "-";
        }
        else
        {
            str1 += System.DateTime.Now.Minute.ToString() + "-";
        }

        if ((System.DateTime.Now.Second).ToString().Length < 2)
        {
            str1 += "0" + System.DateTime.Now.Second.ToString();
        }
        else
        {
            str1 += System.DateTime.Now.Second.ToString();
        }

        return str1;
    }

    //上传文件
    public static string UploadFileIntoDir(FileUpload MyFile, string DirName, string RootDir)
    {
        if (IfOkFile(DirName) == true)
        {
            string ReturnStr = string.Empty;
            if (MyFile.FileContent.Length > 0)
            {
                MyFile.SaveAs(System.Web.HttpContext.Current.Request.MapPath(RootDir + "UploadFile/") + DirName);
                //将原文件名与现在文件名写入ERPSaveFileName表中
                string NowName = DirName;
                string OldName = MyFile.FileName;
                string SqlTempStr = "insert into ERPSaveFileName(NowName,OldName) values ('" + NowName + "','" + OldName + "')";
                ZWL.DBUtility.DbHelperSQL.ExecuteSQL(SqlTempStr);
                return DirName;
            }
            else
            {
                return ReturnStr;
            }
        }
        else
        {
            if (MyFile.FileName.Length > 0)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('不允许上传此类型文件！');</script>");
                return "";
            }
            else
            {
                return "";
            }
        }
    }

    //判断文件是否在允许的范围内
    public static bool IfOkFile(string DirName)
    {
        bool ReturnIF = true;
        try
        {
            string FileExd = DirName.Split('.')[1].ToString();
            string JKL = ZWL.DBUtility.DbHelperSQL.GetSHSL("select FileType from ERPSystemSetting where FileType like '%|" + FileExd + "|%'");
            if (JKL.Length < 1)
            {
                ReturnIF = false;
            }
        }
        catch
        {
            ReturnIF = false;
        }
        return ReturnIF;
    }
    /// <summary>
    /// 根据所已勾选的的aptitude file判断单据工作是否归还
    /// </summary>
    /// <param name="aptid"></param>
    /// <returns></returns>
    public static ZWL.Common.AptitudeState GetAptitudeStateById(int aptid)
    {
        var result = ZWL.Common.AptitudeState.Returned;
        var model = new ZWL.BLL.AptitudeFileState();
        var list = model.GetListModel(" [AptID] =" + aptid);
        if (!list.Any(r => r.State == (int)ZWL.Common.AptitudeState.Returned))
        {
            result = ZWL.Common.AptitudeState.Using;
        }
        return result;
    }
    /// <summary>
    /// 根据aptitude本身字段字段state判断aptitudefile是否归还
    /// </summary>
    /// <param name="aptid"></param>
    /// <returns></returns>
    public static ZWL.Common.AptitudeState GetAptitudeFileStateByAptId(int aptid)
    {
        var result = ZWL.Common.AptitudeState.Returned;
        var model = new ZWL.BLL.AptitudeFileState();
        model.GetModel(aptid);
        result = EnumHelper.ToEnum<ZWL.Common.AptitudeState>(model.State);
        return result;
    }

    public static string GetLabelTextByAptType(int type)
    {
        var result = string.Empty;
        switch (type)
        {
            case (int)ZWL.Common.AptitudeType.Original:
                result = "正本";
                break;
            case (int)ZWL.Common.AptitudeType.Carbon:
                result = "副本";
                break;
            case (int)ZWL.Common.AptitudeType.OriginalCopy:
                result = "原件";
                break;
            case (int)ZWL.Common.AptitudeType.CarbonCopy:
                result = "复印件";
                break;
        }
        return result;
    }

    public static string GetAptitudeFileList()
    {
        var result = string.Empty;
        var path = HttpContext.Current.Server.MapPath("../Aptitude/AptitudeFileList.htm");
        using (var t = new System.IO.StreamReader(path))
        {
            result = t.ReadToEnd();
        }
        var tableStr = string.Empty;
        var groupList = new List<ZWL.BLL.AptitudeFile>();
        var source = new ZWL.BLL.AptitudeFile().GetAllActiveList();
        foreach (var item in source)
        {
            if (!groupList.Any(r => r.Department.Contains(item.Department)))
                groupList.Add(item);
        }
        var j = 1;
        foreach (var item in groupList)
        {
            var i = 0;
            foreach (var it in source)
            {
                if (it.Department != item.Department) continue;
                tableStr += "<tr class='AptitudeRow' data-id='" + it.ID + "'>";
                if (i == 0)
                {
                    var count = source.Count(r => r.Department == item.Department);
                    tableStr += string.Format(@"<td rowspan='{0}' vAlign='middle' style='width:80px;text-align:center;' class='auto-style15 groupName'>
                                        {1}
                                    </td>", count, item.Department);
                }
                tableStr += string.Format(@"<td style='text-align:center;' class='auto-style14 orderNo'>{0}</td>
                                                <td class='auto-style13 aptitudeName'>{1}</td>", j, it.AptitudeName);

                var firstTd = "<td align='center' class='auto-style12'>";
                var secondTd = "<td align='center' class='auto-style12'>";
                var ds = new ZWL.BLL.AptitudeFileState().GetListModel("[AptFileID]=" + it.ID);
                foreach (var im in ds)
                {
                    var isUsing = GetAptitudeFileStateByAptId(im.ID) == ZWL.Common.AptitudeState.Using;
                    var str = @"<label for='{4}_{0}' class='lblAptitudeCheckbox' title='{1}{2}'>
                                    <input type='checkbox' name='{4}_{0}' value='{4}_{0}' class='lblCheckBox' {3} id='{4}_{0}' title='{1}{2}'>
                                    <span class='lblText'>{2}</span>
                                </label>";
                    var lblText = GetLabelTextByAptType(im.Type);
                    switch (im.Type)
                    {
                        case (int)ZWL.Common.AptitudeType.Original:
                            firstTd += string.Format(str, im.Type, it.AptitudeName, lblText, "", it.ID);
                            break;
                        case (int)ZWL.Common.AptitudeType.Carbon:
                            firstTd += string.Format(str, im.Type, it.AptitudeName, lblText, "", it.ID);
                            break;
                        case (int)ZWL.Common.AptitudeType.OriginalCopy:
                            secondTd += string.Format(str, im.Type, it.AptitudeName, lblText, isUsing ? "data-enable=0" : "", it.ID);
                            break;
                        case (int)ZWL.Common.AptitudeType.CarbonCopy:
                            secondTd += string.Format(str, im.Type, it.AptitudeName, lblText, "", it.ID);
                            break;
                    }

                }
                firstTd += "</td>";
                secondTd += "</td>";

                tableStr += firstTd + secondTd;
                tableStr += "</tr>";
                i++;
                j++;
            }
        }
        return result.Replace("{0}", tableStr);
    }
    public static bool IsDebug
    {
        get
        {
            var result = true;
#if DEBUG
#else
            result = false;
#endif
            return result;
        }
    }

    public static string ReplaceSymbolsWithComma(string input)
    {
        var result = "";
        if (!input.IsNullOrEmpty())
        {
            // 定义正则表达式，匹配逗号、点、句号、分号、空格等  
            // 注意：这里使用了字符类[]来匹配多个字符，并用|（或）来组合多个匹配项  
            // 但是，在字符类中，不需要使用|，可以直接列出所有字符  
            // \s 匹配任何空白字符，包括空格、制表符、换页符等  
            // 所以，我们可以简单地使用 [\s，。；;,.]+ 来匹配所有指定的字符和空格  
            string pattern = @"[\s，、。；;,.]+";

            // 使用Regex.Replace方法替换匹配的字符为英文逗号  
            // 注意：由于我们要替换为单个逗号，但匹配可能包含多个字符，  
            // 所以替换时只使用一个逗号  
            result = Regex.Replace(input, pattern, ",");

            // 去除结果字符串中可能出现的连续逗号  
            result = Regex.Replace(result, @",+", ",");
        }

        return result;
    }
    public static string GetTitleTextByAptType(int type)
    {
        var result = string.Empty;
        switch (type)
        {
            case (int)ZWL.Common.AptitudeType.Original:
                result = "正本原件";
                break;
            case (int)ZWL.Common.AptitudeType.Carbon:
                result = "副本原件";
                break;
            case (int)ZWL.Common.AptitudeType.OriginalCopy:
                result = "正本复印件";
                break;
            case (int)ZWL.Common.AptitudeType.CarbonCopy:
                result = "副本复印件";
                break;
        }
        return result;
    }
    public static void BindJiaoSeTree(TreeNodeCollection treeNode, string quanxian)
    {
        BindJiaoSeTree(treeNode, 0, quanxian);
    }
    public static void BindJiaoSeTree(TreeNodeCollection treeNode, int did, string quanxian)
    {
        var treeModel = new ZWL.BLL.ERPTreeList();
        var list = treeModel.GetListModel("ParentID=" + did);
        var i = 0;
        var blackSpace = @"&nbsp;&nbsp;&nbsp;&nbsp;";
        foreach (var item in list)
        {
            var text = string.Empty;
            //var source = treeModel.GetListModel("DisplayID=" + item.ID);
            if (!string.IsNullOrEmpty(item.QuanXianList))
            {

                var checkedStr = PublicMethod.StrIFIn("|" + item.ValueStr + "|", quanxian) ? "checked" : "";
                text = string.Format(@"{2}{3}<input id='quanxian_{0}' {5} type='checkbox' name='quanxian_{0}' value='{1}' class='quanxiancheck' >{4}",
                                        item.ValueStr, (string.IsNullOrEmpty(checkedStr) ? 0 : 1), item.TextStr, blackSpace, "查看", checkedStr);
                i++;
                foreach (var it in item.QuanXianList.Split('|'))//E_导出|W_显示全部数据|V_显示本部门数据|F_显示金额
                {
                    if (string.IsNullOrEmpty(it)) continue;
                    var code = it.Split('_')[0];
                    var name = it.Split('_')[1];
                    var value = item.ValueStr + code;
                    checkedStr = PublicMethod.StrIFIn("|" + value + "|", quanxian) ? "checked" : "";

                    text += string.Format(@"{2}<input id='quanxian_{0}' {4} type='checkbox' name='quanxian_{0}' value='{1}' class='quanxiancheck' >{3}",
                                                value, (string.IsNullOrEmpty(checkedStr) ? 0 : 1), blackSpace, name, checkedStr);
                    i++;
                }

            }
            else
            {
                text = string.Format(@"{0}", item.TextStr);
            }
            var oNode = new TreeNode()
            {
                Text = text,
                Value = item.ValueStr,
                ImageUrl = item.ImageUrlStr,
                ToolTip = item.TextStr,
                SelectAction = TreeNodeSelectAction.Expand,
            };
            BindJiaoSeTree(oNode.ChildNodes, item.ID, quanxian);
            treeNode.Add(oNode);
            i++;
        }
    }
    public static string GetJiaoSeTreeQuanxian(System.Collections.Specialized.NameValueCollection form)
    {
        var result = string.Empty;
        var treeModel = new ZWL.BLL.ERPTreeList();
        var list = treeModel.GetListModel("NavigateUrlStr!='' and NavigateUrlStr is not null order by PaiXuStr asc,ParentID asc,ID asc");
        if (list.Any())
        {
            foreach (var item in list)
            {
                var key = "quanxian_" + item.ValueStr;
                if (form.AllKeys.Any(r => r == key))
                {
                    var checkbox = form.Get(key);
                    if (checkbox == "1")
                        result = result + "|" + item.ValueStr + "|";
                }
                if (!item.QuanXianList.IsNullOrEmpty())
                {
                    var subSource = item.QuanXianList.Split('|');
                    if (subSource != null && subSource.Length > 0)
                    {
                        foreach (var sub in subSource.ToList())
                        {
                            var sKey = "quanxian_" + item.ValueStr + sub.Split('_')[0];
                            if (form.AllKeys.Any(r => r == sKey))
                            {
                                var sCheck = form.Get(sKey);
                                if (sCheck == "1")
                                    result = result + "|" + item.ValueStr + sub.Split('_')[0] + "|";
                            }
                        }
                    }
                }
            }
        }

        return result;
    }
    public static List<ZWL.BLL.ERPKeyValue> GetContractWithoutProject()
    {
        var list = GetContractSubjectType().Where(r => r.Key2 == "QT");
        return list.ToList();
    }
    public static List<ZWL.BLL.ERPKeyValue> GetContractMainSubject()
    {
        var result = new List<ZWL.BLL.ERPKeyValue>();
        var list = GetContractSubjectType();
        foreach (var item in list.OrderBy(r => r.Key1))
        {
            if (!result.Any(r => r.Category == item.Category && r.Key1 == item.Key1 && r.Key2 == item.Key2))
                result.Add(item);
        }
        return result;
    }
    public static List<ZWL.BLL.ERPKeyValue> GetContractSubjectType()
    {
        var kv = new ZWL.BLL.ERPKeyValue();
        var list = kv.GetModelList("Category='ContractSubjectType'");
        return list.ToList();
    }
    public static List<ZWL.BLL.ERPKeyValue> GetCompanyList()
    {
        var kv = new ZWL.BLL.ERPKeyValue();
        var list = kv.GetModelList("Category='CompanyType'").OrderBy(r => r.Key3);
        return list.ToList();
    }
    public static string GetWaterCodeByTableName(string strtablename, string strzy)
    {
        return GetWaterCodeByTableName(strtablename, strzy, 0);
    }

    public static string GetWaterCodeByTableName(string strtablename, string strzy, int len)
    {
        string strwatercode = "";
        string strbianhaoname = "";
        string stryear = DateTime.Now.Year.ToString();
        string strSQL = " SELECT ";
        switch (strtablename)
        {
            case "ERPHeTong"://合同
                strbianhaoname = "HTID";
                break;
            case "ERPHeTongShare"://合同
                strbianhaoname = "No";
                break;
            case "ERPXMJBXX"://项目编号
                strbianhaoname = "XMBH";
                break;
            default:
                break;
        }
        strSQL += strbianhaoname;
        strSQL += " FROM " + strtablename;
        strSQL += " where " + strbianhaoname + " like '%-%' and " + strbianhaoname + " like '%" + strzy + "%' and " + strbianhaoname + " like '%" + stryear + "%'";
        var dt = DbHelperSQL.GetDataTable(strSQL);
        //遍历取到当年中最大的流水账号
        int nmaxwatercode = 0;
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strbh = dt.Rows[i][0].ToString();
                var preStr = strzy;
                if (preStr.Contains("[[]")) preStr = preStr.Replace("[[]", "[");
                if (!string.IsNullOrEmpty(strbh) && strbh.Contains(stryear) && strbh.Contains("-") && strbh.Contains(preStr) && !strbh.Contains("_"))
                {
                    var flowNo = string.Empty;
                    var source = strbh.Split('-');
                    if (source != null && source.Length > 0)
                    {
                        for (int j = source.Length - 1; j >= 0; j--)
                        {
                            if (!string.IsNullOrEmpty(source[j]))
                            {
                                flowNo = source[j];
                                if (!string.IsNullOrEmpty(flowNo)) break;
                            }
                        }
                    }
                    int ncode = 0;
                    if (strtablename.Equals("ERPXMJBXX"))
                    {
                        ncode = int.Parse(flowNo.Replace("X", "").Replace(stryear, ""));
                    }
                    else
                    {
                        ncode = int.Parse(flowNo.Replace("X", ""));
                    }
                    if (ncode >= nmaxwatercode)
                    {
                        nmaxwatercode = ncode;
                    }
                }
            }
        }
        strwatercode = (nmaxwatercode + 1).ToString();
        if (strwatercode.Length < 4)//不足4位
        {
            if (len == 0)
            {
                if (strwatercode.Length == 1)
                {
                    strwatercode = "00" + strwatercode;
                }
                if (strwatercode.Length == 2)
                {
                    strwatercode = "0" + strwatercode;
                }
            }
            else
            {
                strwatercode = strwatercode.PadLeft(len, '0');
            }
        }
        return strwatercode;
    }

    /// <summary>
    /// 用更新后的项目类替换合成后的新表单中的内容
    /// </summary>
    /// <param name="hetong"></param>
    /// <param name="strFormContent"></param>
    /// <returns>返回更新后的表单</returns>
    public static string updateNewFormContent(ZWL.BLL.ERPXMJBXX XMJBXX, string strFormContent)
    {
        DateTime defaultime = new DateTime();
        PublicMethod.GetDefaultTime(out defaultime);
        //替换原有表单content中的值
        strFormContent = strFormContent.Replace("用户自定义控件-项目编号", XMJBXX.XMBH);
        strFormContent = strFormContent.Replace("用户自定义控件-项目前期信息编号", XMJBXX.XMQQBH);
        strFormContent = strFormContent.Replace("用户自定义控件-项目名称", XMJBXX.XMName);
        strFormContent = strFormContent.Replace("用户自定义控件-合同编号", XMJBXX.HTBH);
        strFormContent = strFormContent.Replace("用户自定义控件-项目状态", XMJBXX.XMState);
        strFormContent = strFormContent.Replace("用户自定义控件-项目地址", XMJBXX.XMAdress);
        strFormContent = strFormContent.Replace("用户自定义控件-委托单位名称", XMJBXX.WTDWName);
        strFormContent = strFormContent.Replace("用户自定义控件-委托联系人", XMJBXX.WTDWLXR);
        strFormContent = strFormContent.Replace("用户自定义控件-委托联系电话", XMJBXX.WTDWLXDH);
        strFormContent = strFormContent.Replace("用户自定义控件-委托方式", XMJBXX.WTFS);
        strFormContent = strFormContent.Replace("用户自定义控件-专业类别", XMJBXX.ZYLB);
        strFormContent = strFormContent.Replace("用户自定义控件-行业类别", XMJBXX.HYLB);
        strFormContent = strFormContent.Replace("用户自定义控件-项目经费", XMJBXX.XMJF.ToString());
        strFormContent = strFormContent.Replace("用户自定义-项目资金来源", XMJBXX.XMZJLY);
        if (XMJBXX.XMBeginTime.Equals(defaultime))
        {
            strFormContent = strFormContent.Replace("用户自定义控件-项目周期起", "");
        }
        else
        {
            strFormContent = strFormContent.Replace("用户自定义控件-项目周期起", XMJBXX.XMBeginTime.ToShortDateString());
        }
        if (XMJBXX.XMEndTime.Equals(defaultime))
        {
            strFormContent = strFormContent.Replace("用户自定义控件-项目周期止", "");
        }
        else
        {
            strFormContent = strFormContent.Replace("用户自定义控件-项目周期止", XMJBXX.XMEndTime.ToShortDateString());
        }
        strFormContent = strFormContent.Replace("用户自定义控件-合作单位名称", XMJBXX.HZDWName);
        strFormContent = strFormContent.Replace("用户自定义控件-合作联系人", XMJBXX.HZDWLXR);
        strFormContent = strFormContent.Replace("用户自定义控件-合作联系电话", XMJBXX.HZDWLXDH);
        strFormContent = strFormContent.Replace("用户自定义控件-项目实施部门", XMJBXX.XMBM);
        strFormContent = strFormContent.Replace("用户自定义控件-项目负责人", XMJBXX.XMFZR);
        return strFormContent;
    }

    public static string GetShenPiUserList(ZWL.BLL.ERPNWorkFlowNode node)
    {
        return GetShenPiUserList("", "", 0, node);
    }
    public static string GetShenPiUserList(string SPStr, string DefultStr, int wid)
    {
        return GetShenPiUserList(SPStr, DefultStr, wid, null);
    }
    public static string GetShenPiUserList(string SPStr, string DefultStr, int wid, ZWL.BLL.ERPNWorkFlowNode node)
    {
        var username = string.Empty;
        if (node != null && (SPStr != "" || DefultStr != ""))
        {
            SPStr = node.SPType;
            DefultStr = node.SPDefaultList;
        }
        if (SPStr == "审批时自由指定")
        {
            username = "";
        }
        else if (SPStr == "从默认审批人中选择")
        {
            username = DefultStr;
        }
        else if (SPStr == "从默认审批人中再选择")
        {
            username = DefultStr;
        }

        else if (SPStr == "从默认审批部门中选择")
        {
            var sql = "select UserName from ERPUser where IfLogin<>'否' ";
            var SqlWhere = "";
            string[] DefultList = DefultStr.Split(',');
            for (int i = 0; i < DefultList.Length; i++)
            {
                if (SqlWhere.Trim().Length > 0)
                {
                    SqlWhere = SqlWhere + " or  " + " ','+Department+',' like '%," + DefultList[i].ToString() + ",%' ";
                }
                else
                {
                    SqlWhere = " ','+Department+',' like '%," + DefultList[i].ToString() + ",%' ";
                }
            }
            if (!string.IsNullOrEmpty(SqlWhere))
            {
                sql += string.Format(" and ({0})", SqlWhere);
            }

            username = DbHelperSQL.GetStringList(sql).Replace("|", ",");
        }
        else if (SPStr == "从默认审批角色中选择")
        {
            var sql = "select UserName from ERPUser where IfLogin<>'否' ";
            string SqlWhere = "";
            string[] DefultList = DefultStr.Split(',');
            for (int i = 0; i < DefultList.Length; i++)
            {
                if (SqlWhere.Trim().Length > 0)
                {
                    SqlWhere = SqlWhere + " or  " + " ','+JiaoSe+',' like '%," + DefultList[i].ToString() + ",%' ";
                }
                else
                {
                    SqlWhere = " ','+JiaoSe+',' like '%," + DefultList[i].ToString() + ",%' ";
                }
            }
            if (!string.IsNullOrEmpty(SqlWhere))
            {
                sql += string.Format(" and ({0})", SqlWhere);
            }
            username = DbHelperSQL.GetStringList(sql).Replace("|", ",");
        }
        else if (SPStr == "自动选择流程发起人")
        {
            username = PublicMethod.GetUserName();
            if (wid > 0)
            {
                var wModel = new ZWL.BLL.ERPNWorkToDo();
                wModel.GetModel(wid);
                username = wModel.UserName;
            }
        }
        else if (SPStr == "自动选择本部门主管")
        {
            if (wid > 0)
            {
                var wModel = new ZWL.BLL.ERPNWorkToDo();
                wModel.GetModel(wid);
                var sql = string.Format(@"select ChargeMan from ERPBuMen where BuMenName =(
                            select top 1 Department from ERPUser where UserName='{0}')", wModel.UserName);
                username = DbHelperSQL.GetSHSL(sql);
            }
            else
            {
                username = DbHelperSQL.GetSHSL("select top 1 ChargeMan from ERPBuMen where BuMenName='" + PublicMethod.GetDepartment() + "'");
            }
        }
        else if (SPStr == "自动选择上级部门主管")
        {
            if (wid > 0)
            {
                var wModel = new ZWL.BLL.ERPNWorkToDo();
                wModel.GetModel(wid);
                var sql = string.Format(@"select top 1 ChargeMan from ERPBuMen where ID=(select top 1 DirID from ERPBuMen where BuMenName=(select top 1 Department from ERPUser where UserName='{0}'))", wModel.UserName);
                username = DbHelperSQL.GetSHSL(sql);
            }
            else
                username = DbHelperSQL.GetSHSL("select top 1 ChargeMan from ERPBuMen where ID=(select top 1 DirID from ERPBuMen where BuMenName='" + PublicMethod.GetDepartment() + "')");
        }
        else if (SPStr == "根据条件自动选择审批人")
        {
            if (wid > 0)
            {
                var wModel = new ZWL.BLL.ERPNWorkToDo();
                wModel.GetModel(wid);
                var nodeList = GetNextNode(wModel.JieDianID);
                if (nodeList != null && nodeList.Any(r => r.ConditionSet.Contains("SPDefaultListFilterSQL")))
                {
                    var selectedItem = nodeList.FirstOrDefault(r => r.ConditionSet.Contains("SPDefaultListFilterSQL"));
                    var list = selectedItem.ConditionSet.Split('|').ToList();
                    foreach (var item in list)
                    {
                        if (item.Contains("SPDefaultListFilterSQL"))
                        {
                            var sql = item.Split('_').ToList().LastOrDefault();
                            username = DbHelperSQL.GetSHSL(string.Format(sql, wid));
                            break;
                        }
                    }
                }
            }
            if (node != null)
            {
                if (node.ConditionSet.Contains("SPDefaultListFilterSQL"))
                {
                    var list = node.ConditionSet.Split('|').ToList();
                    foreach (var item in list)
                    {
                        if (item.Contains("SPDefaultListFilterSQL"))
                        {
                            var sql = item.Split('_').ToList().LastOrDefault();
                            username = DbHelperSQL.GetSHSL(string.Format(sql, wid));
                            break;
                        }
                    }
                }
            }
        }

        return PublicMethod.WorkWeiTuoUserList(username);
    }
    public static string GenerateRandomCode(int length)
    {
        Random rand = new Random();
        StringBuilder code = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            code.Append(rand.Next(0, 10).ToString());
        }
        return code.ToString();
    }
    public static Carrier IdentifyCarrier(string phone)
    {
        if (Regex.IsMatch(phone, @"^1[3-9]\d{9}$")) // 基础格式验证
        {
            //^1[3456789]\d{9}
            Regex mobileRegex = new Regex(@"^1[3456789]\d{9}$", RegexOptions.Compiled);

            if (mobileRegex.IsMatch(phone)) return Carrier.ChinaMobile;
        }
        return Carrier.Unknown;
    }
    public static List<ZWL.BLL.ERPNWorkFlowNode> GetNextNode(int? currentNodeId)
    {
        var result = new List<ZWL.BLL.ERPNWorkFlowNode>();
        var node = new ZWL.BLL.ERPNWorkFlowNode();
        node.GetModel(currentNodeId.Value);
        if (!string.IsNullOrEmpty(node.NextNode))
        {
            var list = node.NextNode.Split(',').ToList();
            if (list.Any())
            {
                var workFlowId = node.WorkFlowID.Value;
                foreach (var item in list)
                {
                    var no = new ZWL.BLL.ERPNWorkFlowNode();
                    no.GetModel("WorkFlowID=" + workFlowId + " and NodeSerils='" + item + "'");
                    result.Add(no);
                }
            }
        }
        else
        {
            if (node.NodeAddr.Contains("结束"))
            {
                node.NodeName = node.NodeAddr;
                result.Add(node);
            }
        }
        return result;
    }
    public static List<TKeyValuePair<string, string>> FormInputCanWriteSet(int workId)
    {
        var list = new List<TKeyValuePair<string, string>>();
        var tModel = new ZWL.BLL.ERPNWorkToDo();
        tModel.GetModel(workId);
        var currentNode = tModel.CurrentNode();
        var fModel = tModel.CurrentForm();
        if (currentNode.PSType == "全部通过可向下流转")
        {
            var selectedImgIndex = 0;
            if (!string.IsNullOrEmpty(currentNode.CanWriteSet))
            {
                var cwlist = currentNode.CanWriteSet.Split('|');
                if (fModel.ItemsList.Length > 0)
                {
                    var imgList = cwlist.ToList().Where(e => e.Contains("img"));
                    var firstImg = imgList.FirstOrDefault().Split('_')[0];
                    var firstindex = PublicMethod.GetInt(PublicMethod.GetNumeric(firstImg));
                    if (firstindex > 0)
                    {
                        var preStr = firstImg.Replace(firstindex.ToString(), "");
                        var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                        htmlDoc.LoadHtml(tModel.FormContent);
                        var namelist = new List<string> { "id", "src" };
                        var inputnodelist = htmlDoc.DocumentNode.SelectNodes("//img");
                        if (inputnodelist != null)
                            foreach (HtmlNode inputnode in inputnodelist)
                            {
                                var attlist = inputnode.Attributes.Where(e => namelist.Contains(e.Name.ToLower()));
                                HtmlAttribute currentAttr = null;//是否存在此id属性，并且是以特定字符串开头
                                foreach (HtmlAttribute att in attlist)
                                {
                                    if (att.Name.ToLower() == "id" && att.Value.Contains(preStr))
                                    {
                                        currentAttr = att;
                                        break;
                                    }
                                }
                                if (currentAttr != null)
                                {
                                    foreach (var item in attlist)
                                    {
                                        if (item.Name.ToLower() == "src" && item.Value.Contains("InsertYinZhang"))//src属性是否被填充，即已签名
                                        {
                                            selectedImgIndex = PublicMethod.GetInt(currentAttr.Value.Replace(preStr, ""));
                                            var selectedImg = preStr + selectedImgIndex;
                                            var tselected = cwlist.FirstOrDefault(e => e.Contains(selectedImg));
                                            if (tselected == null) continue;
                                            var tlist = tselected.Split('_');
                                            if (tlist == null || tlist.Length <= 0) continue;
                                            var selectedImgAlt = tlist[1];
                                            list.Add(new TKeyValuePair<string, string>() { Key = selectedImg, Value = selectedImgAlt });

                                            var textList = cwlist.ToList().Where(e => e.Contains("Text"));
                                            if (textList.Any())
                                            {
                                                var firstTextList = textList.FirstOrDefault().Split('_');
                                                var firstText = firstTextList[0];
                                                var firstTextAlt = firstTextList[1];
                                                firstText = firstText.Replace(PublicMethod.GetNumeric(firstText), "");
                                                var selectedText = firstText + selectedImgIndex;
                                                list.Add(new TKeyValuePair<string, string>() { Key = selectedText, Value = firstTextAlt });
                                            }

                                            var dateList = cwlist.ToList().Where(e => e.Contains("Date"));
                                            if (textList.Any())
                                            {
                                                var firstDateList = dateList.FirstOrDefault().Split('_');
                                                var firstDate = firstDateList[0];
                                                var firstDateAlt = firstDateList[1];
                                                firstDate = firstDate.Replace(PublicMethod.GetNumeric(firstDate), "");
                                                var selectedDate = firstDate + selectedImgIndex;
                                                list.Add(new TKeyValuePair<string, string>() { Key = selectedDate, Value = firstDateAlt });
                                            }

                                            break;
                                        }
                                    }
                                }
                                if (selectedImgIndex > 0)
                                {
                                    break;
                                }
                            }
                    }
                }
                if (list.Count <= 0)
                {
                    var imgList = cwlist.ToList().Where(e => e.Contains("img"));
                    foreach (var item in imgList)
                    {
                        list.Add(new TKeyValuePair<string, string>() { Key = item.Split('_')[0], Value = item.Split('_')[1] });
                    }

                }
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(currentNode.CanWriteSet))
            {
                var inputList = currentNode.CanWriteSet.Split('|');
                var item = inputList.GetEnumerator();
                while (item.MoveNext())
                {
                    if (item.Current.ToString().Contains("_"))
                    {
                        var pair = item.Current.ToString().Split('_');
                        list.Add(new TKeyValuePair<string, string> { Key = pair[0], Value = pair[1] });
                    }
                }
            }
        }
        return list;
    }

    public static string FormInputCanWriteSet(int workId, ref string yinzhangimgid)
    {
        var tModel = new ZWL.BLL.ERPNWorkToDo();
        tModel.GetModel(workId);
        var tModelExt = tModel.CurrentWorkToDoExtend();
        var strformcontent = tModelExt.FormContent;
        var fModel = tModel.CurrentForm();
        var ItemsList = fModel.ItemsList;
        var nModel = tModel.CurrentNode();
        var CanWriteStr = nModel.CanWriteSet;
        var SecretStr = nModel.SecretSet;
        var sbuilder = new StringBuilder();
        var formatStr = "if(document.getElementById('{0}')!=null) document.getElementById('{0}').{1};";
        var shenpiStr = "if(document.getElementById('{0}')!=null&&document.getElementById('{0}').value=='')document.getElementById('{0}').{1};";
        var FormItemList = ItemsList.Split('|');
        var currenttime = DateTime.Now.ToShortDateString();
        var strformidtongyi = "63,64,68,73,74,56,57";//formid为63,64,68,73,74的表先不自动添加同意
        var sql = "select * from ERPNWorkFlowNode where WorkFlowID in (select ID from ERPNWorkFlow where FormID in (" + strformidtongyi + ")) and CanWriteSet<>''";
        var NotInList = Conv<ZWL.BLL.ERPNWorkFlowNode>.GetList(sql);
        var canWriteInputs = FormInputCanWriteSet(workId);
        for (int ItemNum = 0; ItemNum < FormItemList.Length; ItemNum++)
        {
            var item = FormItemList[ItemNum].ToString().Trim();
            if (string.IsNullOrEmpty(item)) continue;

            var inputId = item.Split('_')[0];
            if (strformcontent.IndexOf(inputId) < 0)
            {
                continue;//如果表单中没找到该控件，跳过该控件的设置（这种情况是更新了表单，审批旧表单引起的）
            }
            if (!PublicMethod.StrIFIn(inputId, CanWriteStr) || !canWriteInputs.Any(e => item.Contains(e.Key)))//不属于可写字段
            {
                sbuilder.AppendFormat(formatStr, inputId, "disabled=true");
            }
            else
            {
                sbuilder.AppendFormat(formatStr, inputId, "disabled=false");
                //可写，如果不是开始节点
                if (item.ToLower().Contains("text") && !NotInList.Any(r => r.CanWriteSet.Contains(item)))//审批意见
                {
                    if (item.Contains("是否"))
                    {
                        sbuilder.AppendFormat(formatStr, inputId, "value='是'");//设置默认审批意见为同意
                    }
                    else
                    {
                        //结算金额不加同意
                        if (item.Contains("结算金额"))
                        {
                            sbuilder.AppendFormat(formatStr, inputId, "readOnly=false");
                        }
                        else if (tModel.FormID == 49)
                        {
                            if (!item.Contains("审核") && !item.Contains("核稿") && !item.Contains("会稿") && !item.Contains("会稿"))
                            {
                                continue;
                            }
                            else
                            {
                                sbuilder.AppendFormat(shenpiStr, inputId, "value='同意'");
                            }
                        }
                        else
                        {
                            sbuilder.AppendFormat(shenpiStr, inputId, "value='同意'");
                        }
                    }
                }
                if (item.ToLower().Contains("date"))//审批时间
                {
                    sbuilder.AppendFormat(formatStr, inputId, "value= '" + currenttime + "'");
                    if (!FormItemList[ItemNum].Contains("可写"))
                    {
                        sbuilder.AppendFormat(formatStr, inputId, "disabled=true");
                    }
                }
                if (item.ToLower().Contains("img"))//签字
                {
                    yinzhangimgid += (string.IsNullOrEmpty(yinzhangimgid) ? "" : ",") + inputId.ToString();
                }
            }
            if (PublicMethod.StrIFIn(item, SecretStr) == true)//属于保密字段
            {
                sbuilder.AppendFormat(formatStr, inputId, "style.visibility=\"hidden\"");
            }
            else
            {
                sbuilder.AppendFormat(formatStr, inputId, "style.visibility=\"visible\"");
            }
        }

        return sbuilder.ToString();
    }
    public static bool CheCkIfOk(string TongGuoStr, string ShenPiList, string TiaoJianStr)
    {
        if (TiaoJianStr == "一人通过可向下流转")
        {
            return true;
        }
        else
        {
            //判断审批人列表是否全部在通过人列表中
            string[] ShenPiArry = ShenPiList.Split(',');
            for (int i = 0; i < ShenPiArry.Length; i++)
            {
                if (PublicMethod.StrIFIn("," + ShenPiArry[i] + ",", "," + TongGuoStr + ",") == false)
                {
                    //检测到任何一个审批人不在已经通过列表中，则返回false
                    return false;
                }
            }
            return true;
        }
    }
    public static bool ShowTransformSetting(int workid)
    {
        var model = new ZWL.BLL.ERPNWorkToDo();
        model.GetModel(workid);
        return ShowTransformSetting(model);
    }

    public static bool ShowTransformSetting(ZWL.BLL.ERPNWorkToDo model)
    {
        var ShowTransform = true;
        if (model == null || model.ID <= 0) return ShowTransform;
        var list = new List<int> { 39, 42, 43, 46, 48, 49, 58, 60, 73, 86, 88, 90, 112, 113, 115, 116, 117 };
        PublicMethod.XiangMuFormIDsint.ForEach(r => { list.Add(r); });
        if (list.Contains(model.FormID.Value))
        {
            ShowTransform = false;
        }
        return ShowTransform;
    }
    public static bool SendEmail(string content, string title, string fujian, string toUser, string beiyong1, int? formId, int? workFlowId)
    {
        return SendEmail(content, title, fujian, toUser, beiyong1, formId, workFlowId, 0);
    }
    public static bool SendEmail(string content, string title, ZWL.BLL.ERPNWorkToDo m)
    {
        if (m != null)
            return SendEmail(title, title, m.FuJianList, m.ShenPiUserList, m.BeiYong1, m.FormID, m.WorkFlowID, m.ID);
        return false;
    }
    public static string GetFirstSplitString(string splitstr, string defaultstr)
    {
        var result = string.Empty;
        if (splitstr == null) splitstr = result;
        var list = splitstr.Split(',');
        foreach (var item in list)
        {
            if (string.IsNullOrEmpty(item)) continue;
            result = item;
            break;
        }
        if (string.IsNullOrEmpty(splitstr)) result = defaultstr;
        return result;
    }

    public static bool SendEmail(string content, string title, string fujian, string toUser, string beiyong1, int? formId, int? workFlowId, int worktodoid)
    {
        var result = true;
        var MyMail = new ZWL.BLL.ERPLanEmail();
        MyMail.EmailTitle = title;
        MyMail.EmailContent = content;
        MyMail.EmailState = "未读";
        MyMail.FromUser = "系统消息";
        MyMail.FuJian = fujian;
        MyMail.TimeStr = DateTime.Now;
        MyMail.ToUser = toUser;
        MyMail.FormID = formId.HasValue ? formId.Value : 0;
        MyMail.WorkFlowID = workFlowId.HasValue ? workFlowId.Value : 0;
        MyMail.BeiYong1 = beiyong1;
        MyMail.WorkToDoID = worktodoid;
        result = MyMail.Add() > 0;
        return result;
    }

    public static DataSet GetRebuildFormExtend(DataSet ds, Hashtable myHastlist, int workid)
    {
        var WorkToDo = new ZWL.BLL.ERPNWorkToDo();
        WorkToDo.GetModel(workid);
        if (WorkToDo.FormID == 46)
        {
            if (myHastlist != null && myHastlist.Count > 0 && myHastlist.ContainsKey("付款单位"))
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    var table = ds.Tables[0];
                    var dModel = new ZWL.BLL.ERPHeTong().GetReceiptState(WorkToDo.Number);
                    var dr = table.NewRow();
                    dr["ID"] = (table.Rows.Count + 1).ToString();
                    dr["Name"] = "合同金额";
                    dr["Value"] = PublicMethod.FormatMoney(dModel.HTJE);
                    dr["Cols"] = "1";
                    var index = PublicMethod.FindRowIndex(table, "name", "付款单位");
                    table.Rows.InsertAt(dr, index + 1);
                    var dr1 = table.NewRow();
                    dr1["ID"] = (table.Rows.Count + 1).ToString();
                    dr1["Name"] = "到账金额";
                    dr1["Value"] = PublicMethod.FormatMoney(dModel.DaoZhangJE);
                    dr1["Cols"] = "1";
                    var index1 = PublicMethod.FindRowIndex(table, "name", "合同金额");
                    table.Rows.InsertAt(dr1, index1 + 1);
                    var dr2 = table.NewRow();
                    dr2["ID"] = (table.Rows.Count + 1).ToString();
                    dr2["Name"] = "未收账款";
                    dr2["Value"] = PublicMethod.FormatMoney(dModel.KaiPiaoJE);
                    dr2["Cols"] = "1";
                    var index2 = PublicMethod.FindRowIndex(table, "name", "到账金额");
                    table.Rows.InsertAt(dr2, index2 + 1);
                    table.AcceptChanges();
                }
            }
        }
        if (WorkToDo.FormID == 78 && WorkToDo.JieDianID == 261)
        {
            if (ds != null && ds.Tables.Count > 0)
            {
                var table = ds.Tables[0];
                foreach (DataRow dr in table.Rows)
                {
                    if (dr["Name"].ToString() == "交通工具" && dr["Value"].ToString() == "飞机")
                    {
                        dr["Name"] = "";
                        dr["Value"] = "";
                    }
                }
            }
        }
        if (WorkToDo.FormID == 42)
        {
            if (ds != null && ds.Tables.Count > 0)
            {
                var table = ds.Tables[0];
                for (int i = table.Rows.Count - 1; i >= 0; i--)
                {
                    var dr = table.Rows[i];
                    var name = dr["Name"].ToString();
                    var val = dr["Value"].ToString();
                    if (!name.Contains("中标") && string.IsNullOrEmpty(val))
                    {
                        table.Rows.RemoveAt(i);
                    }
                }
            }
        }
        if (WorkToDo.FormID == 43)
        {
            if (ds != null && ds.Tables.Count > 0)
            {
                var table = ds.Tables[0];
                for (int i = table.Rows.Count - 1; i >= 0; i--)
                {
                    var dr = table.Rows[i];
                    var name = dr["Name"].ToString();
                    var val = dr["Value"].ToString();
                    if (name.Contains("计划") && string.IsNullOrEmpty(val))
                    {
                        table.Rows.RemoveAt(i);
                    }
                }
            }
        }
        if (WorkToDo.FormID == 44)
        {
            if (ds != null && ds.Tables.Count > 0)
            {
                var table = ds.Tables[0];
                for (int i = table.Rows.Count - 1; i >= 0; i--)
                {
                    var dr = table.Rows[i];
                    var name = dr["Name"].ToString();
                    if (Regex.IsMatch(name, @"[0-9]+$"))
                    {
                        dr["Name"] = Regex.Split(name, "[0-9]+$", RegexOptions.IgnoreCase)[0];
                    }
                }
            }
        }
        if (WorkToDo.FormID == 110 || WorkToDo.FormID == 111)
        {
            if (ds != null && ds.Tables.Count > 0)
            {
                var table = ds.Tables[0];
                for (int i = table.Rows.Count - 1; i >= 0; i--)
                {
                    var dr = table.Rows[i];
                    var name = dr["Name"].ToString();
                    var val = dr["Value"].ToString();
                    if (string.IsNullOrEmpty(val))
                    {
                        table.Rows.RemoveAt(i);
                    }
                }
            }
        }
        if (WorkToDo.FormID == 99)
        {
            if (ds != null && ds.Tables.Count > 0)
            {
                var table = ds.Tables[0];
                for (int i = table.Rows.Count - 1; i >= 0; i--)
                {
                    var dr = table.Rows[i];
                    var name = dr["Name"].ToString();
                    if (!string.IsNullOrEmpty(name) && name == "车辆信息")
                    {
                        table.Rows.RemoveAt(i);
                    }
                }
            }
        }
        if (WorkToDo.FormID == 49)
        {
            if (ds != null && ds.Tables.Count > 0)
            {
                var list = new List<string> { "发文稿表头", "事由", "主送单位", "抄送", "主办单位", "拟稿人", "紧急程度", "正文", "附件" };
                var table = ds.Tables[0];
                for (int i = table.Rows.Count - 1; i >= 0; i--)
                {
                    var dr = table.Rows[i];
                    var name = dr["Name"].ToString();
                    if (!string.IsNullOrEmpty(name) && name == "车辆信息")
                    {
                        table.Rows.RemoveAt(i);
                    }
                }
            }
        }
        if (ds != null && ds.Tables.Count > 0)
        {
            var table = ds.Tables[0];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                table.Rows[i]["ID"] = (i + 1);
            }
        }
        return ds;
    }
    public static string GetCombineHTBH(string orightbh, string currhtbh)
    {
        var result = string.Empty;
        if (orightbh == null) orightbh = string.Empty;
        if (!orightbh.Contains(currhtbh))
        {
            var list = orightbh.Split(',');
            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(item)) continue;
                var hModel = new ZWL.BLL.ERPHeTong();
                hModel.GetstrModel(item);
                if (!string.IsNullOrEmpty(hModel.HTID))
                {
                    result += item + ",";
                }
            }
            result += currhtbh;
        }
        else
        {
            result = orightbh;
        }
        return result;
    }
    /// <summary>
    /// 得到圆饼图的数据
    /// </summary>
    /// <param name="sqltable"></param>
    /// <returns></returns>
    public static List<PieInfo> GetPieInfoList(string sqltable)
    {
        string sql_count = "select a.ZYType as name,isnull(count(1),0) as value from (" + sqltable + ") a GROUP BY a.ZYType";
        string sql_money = "select a.ZYType as name,CAST(isnull(sum(price),0) as money) as value from (" + sqltable + ") a GROUP BY a.ZYType";
        List<PieInfo> pieInfosList = new List<PieInfo>();
        PieInfoMingXi pieInfoMingXi = new PieInfoMingXi();
        List<PieInfoMingXi> pieInfoMingXi_count = pieInfoMingXi.GetModelList(sql_count);
        List<PieInfoMingXi> pieInfoMingXi_Money = pieInfoMingXi.GetModelList(sql_money);
        int counttotal = 0;
        foreach (var item in pieInfoMingXi_count)
        {
            int num = 0;
            if (!string.IsNullOrEmpty(item.value))
            {
                int.TryParse(item.value, out num);
                counttotal += num;
            }
        }
        pieInfosList.Add(new PieInfo() { type = "count", total = counttotal.ToString(), PieInfoMingXiList = pieInfoMingXi_count });

        decimal moneytotal = 0.00M;
        foreach (var item in pieInfoMingXi_Money)
        {
            decimal money = 0.00M;
            if (!string.IsNullOrEmpty(item.value))
            {
                decimal.TryParse(item.value, out money);
                moneytotal += money;
            }
        }
        pieInfosList.Add(new PieInfo() { type = "money", total = String.Format("{0:###,###,##0.00}", moneytotal), PieInfoMingXiList = pieInfoMingXi_Money });
        return pieInfosList;
    }
    public static StatisticsInfo GetXMCount(string elementID, string categroy, string sql)
    {
        StatisticsInfo info = new StatisticsInfo();
        int count = ZWL.DBUtility.DbHelperSQL.GetDataRowsCount(sql);
        info.elementID = elementID;
        info.categroy = categroy;
        info.value = count.ToString();
        info.pieInfos = GetPieInfoList(sql);
        return info;
    }
    public static List<TKeyValuePair<string, int>> GetXMBanliQingkuangForZonggongban(int xmworkid)
    {
        var subSql1 = @"select count(1) from ERPNWorkToDo t where StateNow='正常结束' and t.FormID in ({0}) and t.BeiYong1 like  SUBSTRING(d.BeiYong1,0,CHARINDEX('@',d.BeiYong1))+'%'";

        var sql = string.Format(@"select 'SC' [Key], ({0}) [Value] from ERPNWorkToDo d  where ID={4}
                                    UNION
                                    select 'AQ' [Key], ({1}) [Value] from ERPNWorkToDo d  where ID={4}
                                    UNION
                                    select 'ZR' [Key], ({2}) [Value] from ERPNWorkToDo d  where ID={4}
                                    UNION
                                    select 'GZ' [Key], ({3}) [Value] from ERPNWorkToDo d  where ID={4}",
                                    string.Format(subSql1, "56,75"),
                                    string.Format(subSql1, "62"),
                                    string.Format(subSql1, "108"),
                                    string.Format(subSql1, "109"),
                                    xmworkid);
        var cmodel = Conv<TKeyValuePair<string, int>>.GetList(sql);
        return cmodel;
    }
    public static string GetXMUtilityReviewer(int workid)
    {
        return GetXMUtilityReviewer(workid, null);
    }
    public static string GetXMUtilityReviewer(int workid, int? nextnodeid)
    {
        var result = string.Empty;
        var kModel = new ZWL.BLL.ERPKeyValue();
        kModel = kModel.GetModel("Category='XMUtilityReviewerForm'");
        if (kModel != null && kModel.ID > 0)
        {
            var forms = kModel.Value1.Split(',').Select<string, int>(x => Convert.ToInt32(x)).ToList();
            var list = kModel.GetModelList("Category='XMUtilityReviewerByZYLB'");
            if (list != null && list.Any())
            {
                var firstitem = list.FirstOrDefault();
                var doModel = new ZWL.BLL.ERPNWorkToDo();
                doModel.GetModel(workid);
                if (forms.Contains(doModel.FormID.Value))
                {
                    var node = new ZWL.BLL.ERPNWorkFlowNode();
                    if (nextnodeid.HasValue)
                    {
                        node.GetModel(nextnodeid.Value);
                    }
                    else
                    {
                        var cnode = doModel.CurrentNode();
                        if (!cnode.NextNode.IsNullOrEmpty())
                        {
                            var nextnode = PublicMethod.GetInt(cnode.NextNode.Split(",")[0]);
                            var nodes = node.GetListModel("WorkFlowID={0} and NodeSerils='{1}'".FormatWith(doModel.WorkFlowID, nextnode));
                            if (nodes != null && nodes.Any())
                                node = nodes.FirstOrDefault();
                        }
                    }
                    var zylb = string.Empty;
                    if (node != null && node.ID > 0 && (node.NodeName.Contains("工") || node.NodeName.Contains("成果")))
                    {
                        var xModel = new ZWL.BLL.ERPXMJBXX();
                        if (doModel.FormID.Value == 93)
                        {
                            var gModel = Conv<ZWL.BLL.ERPGaizhang>.GetModel("select * from ERPGaizhang where NWorkID=" + workid);
                            if (gModel != null && !gModel.code.IsNullOrEmpty())
                            {
                                xModel = xModel.GetModelByXMBH(gModel.code);
                            }
                        }
                        else
                        {
                            xModel = xModel.GetModelByXMBH(doModel.Number);
                        }
                        if (xModel != null && xModel.ID > 0)
                        {
                            zylb = xModel.ZYLB;
                            if (node.NodeName.Contains("成果") && zylb == "岩土工程勘察" && node.NodeAddr == "结束")
                            {
                                var sqlWhere1 = @"select (case when GCCLarea>0 or GCCLlength>0 then '测量' ELSE '' end) GCCL,
	                                    (case when JKJCarea>0 or JKJCdepth>0 or JKSJlength>0 or JKSJdepth>0 or JKSJarea>0 then '基坑监测' ELSE '' end) JKJC,
	                                    (case when GXTC>0 then '管线探测'  ELSE '' end) GXTC,
	                                    (case when TRDJC>0 then '土壤氡浓度测试'  ELSE '' end) TRDJC 
	                                    from ERPXMChengGuo c join ERPXMJBXX x on c.XMBH=x.XMBH where NWorkToDoID={0}".FormatWith(doModel.ID);
                                var checkCG = DbHelperSQL.GetDataRow(sqlWhere1);
                                if (checkCG != null)
                                {
                                    if (!checkCG["GCCL"].ToString().IsNullOrEmpty())
                                    {
                                        zylb = checkCG["GCCL"].ToString();
                                    }
                                    else if (!checkCG["JKJC"].ToString().IsNullOrEmpty())
                                    {
                                        zylb = checkCG["JKJC"].ToString();
                                    }
                                    else if (!checkCG["GXTC"].ToString().IsNullOrEmpty())
                                    {
                                        zylb = checkCG["GXTC"].ToString();
                                    }
                                    else if (!checkCG["TRDJC"].ToString().IsNullOrEmpty())
                                    {
                                        zylb = checkCG["TRDJC"].ToString();
                                    }
                                }
                                else
                                {
                                    var paser = new ZWL.Common.ParseHtml();
                                    paser.GetAttListFormHTMLall(doModel.FormContent);
                                    var gccl = !paser.getValue("工程测量长度").IsNullOrEmpty() || !paser.getValue("工程测量面积").IsNullOrEmpty();
                                    if (gccl)
                                    {
                                        zylb = "测量";
                                    }
                                    var JKJC = !paser.getValue("基坑监测面积").IsNullOrEmpty() || !paser.getValue("基坑监测深度").IsNullOrEmpty()
                                        || !paser.getValue("基坑设计周长").IsNullOrEmpty() || !paser.getValue("基坑设计深度").IsNullOrEmpty() || !paser.getValue("基坑设计面积").IsNullOrEmpty();
                                    if (JKJC)
                                    {
                                        zylb = "基坑监测";
                                    }
                                    var GXTC = !paser.getValue("管线探测").IsNullOrEmpty();
                                    if (GXTC)
                                    {
                                        zylb = "管线探测";
                                    }
                                    var TRDJC = !paser.getValue("土壤氡检测").IsNullOrEmpty();
                                    if (TRDJC)
                                    {
                                        zylb = "土壤氡浓度测试";
                                    }
                                }
                            }
                        }
                    }
                    if (!zylb.IsNullOrEmpty())
                    {
                        ZWL.BLL.ERPKeyValue selectedItem = null;
                        foreach (var item in list)
                        {
                            if (item.Value1.IsNullOrEmpty()) continue;
                            var tlist = item.Value1.Split(',').ToList();
                            if (tlist.Contains(zylb))
                            {
                                selectedItem = item;
                                break;
                            }
                        }
                        if (selectedItem != null)
                        {
                            result = selectedItem.Value2 + "," + selectedItem.Value3;
                        }
                        else
                        {
                            selectedItem = list.FirstOrDefault();
                            result = selectedItem.Value2;
                        }
                    }
                }
            }
        }
        return result;
    }
    public static List<SysColumnInfo> GetSysColumnInfos(string tableName)
    {
        var sql = @"SELECT a.colorder [Order],a.name Name,b.name as Type,b.length as Length,g.value AS [Desc]
                    FROM syscolumns a left join systypes b on a.xtype=b.xusertype inner join sysobjects d on a.id=d.id and d.xtype='U' and d.name<>'dtproperties'left join sys.extended_properties g 
                    on a.id=g.major_id AND a.colid = g.minor_id 
                    WHERE d.name ='{0}' 
                    order by a.id,a.colorder".FormatWith(tableName);
        var list = Conv<SysColumnInfo>.GetList(sql);
        return list;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="category">CommonNianLingCeng/LiTuiXiuNianLingCeng</param>
    /// <param name="birthDate"></param>
    /// <returns></returns>
    public static string GetNianLingCeng(string category, DateTime birthDate)
    {
        var nianlingceng = string.Empty;
        if (birthDate != null && !category.IsNullOrEmpty())
        {
            var nianling = CalculateAgeCorrect(birthDate, DateTime.Now);
            var kmodel = new ZWL.BLL.ERPKeyValue();
            var klist = kmodel.GetModelList("Category='" + category + "'");
            foreach (var item in klist)
            {
                var type = (ZWL.BLL.Expression)Enum.Parse(typeof(ZWL.BLL.Expression), item.Key1);
                switch (type)
                {
                    case ZWL.BLL.Expression.LE:
                        if (nianling <= PublicMethod.GetInt(item.Value1))
                            nianlingceng = item.Key2;
                        break;
                    case ZWL.BLL.Expression.LT:
                        if (nianling < PublicMethod.GetInt(item.Value1))
                            nianlingceng = item.Key2;
                        break;
                    case ZWL.BLL.Expression.GEL:
                        if (nianling >= PublicMethod.GetInt(item.Value1) && nianling < PublicMethod.GetInt(item.Value2))
                            nianlingceng = item.Key2;
                        break;
                    case ZWL.BLL.Expression.GT:
                        if (nianling > PublicMethod.GetInt(item.Value1))
                            nianlingceng = item.Key2;
                        break;
                    case ZWL.BLL.Expression.GE:
                        if (nianling >= PublicMethod.GetInt(item.Value1))
                            nianlingceng = item.Key2;
                        break;
                }
            }
        }
        return nianlingceng;
    }
    public static int CalculateAgeCorrect(DateTime birthDate, DateTime now)
    {
        int age = now.Year - birthDate.Year;
        if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
            age--;
        return age;
    }
    public static ysfclass GetXMYingShouYingFu(string xmbh, DateTime? baseDate)
    {
        return GetXMYingShouYingFu(xmbh, baseDate, null);
    }
    public static ysfclass GetXMYingShouYingFu(string xmbh, DateTime? baseDate, DateTime? endDate)
    {
        var result = new ysfclass();
        var xmbhsql = "'{0}'".FormatWith(xmbh);
        var shetongsql = @"select h.* from ERPHeTong h	join ERPNWorkToDo d on h.NWorkToDoID=D.ID
                            where StateNow in ('正在办理','正常结束') and XMID in ({0}) and HTLB = '收款' ".FormatWith(xmbhsql);
        if (baseDate.HasValue)
        {
            shetongsql += @" and QDTime>'{0}' ".FormatWith(baseDate.Value);
        }
        if (endDate.HasValue)
        {
            shetongsql += @" and QDTime<'{0}' ".FormatWith(endDate.Value);
        }
        var shetonglist = Conv<ZWL.BLL.ERPHeTong>.GetList(shetongsql);
        var fhetongsql = @"select h.* from ERPHeTong h	join ERPNWorkToDo d on h.NWorkToDoID=D.ID
                            where StateNow in ('正在办理','正常结束') and XMID in ({0}) and HTLB = '付款' ".FormatWith(xmbhsql);
        if (baseDate.HasValue)
        {
            fhetongsql += @" and QDTime>'{0}' ".FormatWith(baseDate.Value);
        }
        if (endDate.HasValue)
        {
            fhetongsql += @" and QDTime<'{0}' ".FormatWith(endDate.Value);
        }
        var fhetonglist = Conv<ZWL.BLL.ERPHeTong>.GetList(fhetongsql);
        var workloadSqlWhere = string.Empty;
        var costdetailsql = @"select i.*,XMName,XMBH,HTBH,ZYLB from ERPCostDetailPostItems i left join ERPProjectCost c on i.RecordId=c.ID 
                where RelativeId is not null and Item='工程出包费' and (DeleteMark is null or DeleteMark<>1) and XMBH in ({0}) ".FormatWith(xmbhsql);
        var costdetailsqlWhere = string.Empty;
        if (baseDate.HasValue)
        {
            costdetailsqlWhere += @" and DJTime>'{0}' ".FormatWith(baseDate.Value);
        }
        if (endDate.HasValue)
        {
            costdetailsqlWhere += @" and DJTime<'{0}' ".FormatWith(endDate.Value);
        }
        workloadSqlWhere += @" and ParentId in(
                                select ID from ERPCostDetailPost where State in ('已提交','已完成') and (DeleteMark is null or DeleteMark<>1) {0}
                                )".FormatWith(costdetailsqlWhere);

        if (!workloadSqlWhere.IsNullOrEmpty())
        {
            costdetailsql += workloadSqlWhere;
        }
        decimal costsum = 0;
        var costdetaillist = DbHelperSQL.GetDataTable(costdetailsql);//工程出包费
        if (costdetaillist != null && costdetaillist.Rows.Count > 0)
        {
            for (int j = 0; j < costdetaillist.Rows.Count; j++)
            {
                var witem = costdetaillist.Rows[j];
                costsum += PublicMethod.GetDecimal(witem["SubmitAmt"]);
            }
        }
        var daozhangsql = @"select s.* from ERPHeTongDaoZhang s join ERPNWorkToDo d on s.NWorkToDoID=d.ID 
                                where StateNow in ('正在办理','正在办理，已开票','正常结束')
                                and HTBH in (select HTID from ERPHeTong h	join ERPNWorkToDo d on h.NWorkToDoID=D.ID
                                where HTLB='收款' and StateNow in ('正在办理','正常结束') and XMID in ({0}))".FormatWith(xmbhsql);
        if (baseDate.HasValue)
        {
            daozhangsql += @" and DaoZhangTime>'{0}' ".FormatWith(baseDate.Value);
        }
        if (endDate.HasValue)
        {
            daozhangsql += @" and DaoZhangTime<'{0}' ".FormatWith(endDate.Value);
        }
        var daozhanglist = Conv<ZWL.BLL.ERPHeTongDaoZhang>.GetList(daozhangsql);

        var shoukuansql = @"select s.* from ERPHeTongShouKuan s join ERPNWorkToDo d on s.NWorkToDoID=d.ID 
                                where StateNow in ('正在办理','正在办理，已开票','正常结束')
                                and HTBH in (select HTID from ERPHeTong h	join ERPNWorkToDo d on h.NWorkToDoID=D.ID
                                where HTLB='收款' and StateNow in ('正在办理','正常结束') and XMID in ({0}))".FormatWith(xmbhsql);
        if (baseDate.HasValue)
        {
            shoukuansql += @" and SQTime>'{0}' ".FormatWith(baseDate.Value);
        }
        if (endDate.HasValue)
        {
            shoukuansql += @" and SQTime<'{0}' ".FormatWith(endDate.Value);
        }
        var shoukuanlist = Conv<ZWL.BLL.ERPHeTongShouKuan>.GetList(shoukuansql);

        var fshoukuanlist = shoukuanlist.Where(r => shetonglist.Any(u => u.HTID == r.HTBH)).ToList();
        decimal daozhangsum = 0;
        if (daozhanglist != null && daozhanglist.Any())
        {
            foreach (var item in daozhanglist)
            {
                daozhangsum += item.DaoZhangJE;
            }
        }
        decimal kaipiaosum = 0;
        if (shoukuanlist != null && shoukuanlist.Any())
        {
            foreach (var item in shoukuanlist)
            {
                kaipiaosum += item.KaiPiaoJE;
            }
        }
        decimal jiesuansum = 0;
        var htjiesuansql = @"select s.* from ERPHTJieSuan s join ERPNWorkToDo d on s.NWorkToDoID=d.ID 
                                where StateNow in ('正常结束')
                                and HTBH in (select HTID from ERPHeTong h	join ERPNWorkToDo d on h.NWorkToDoID=D.ID
                                where StateNow in ('正在办理','正常结束') and XMID in ({0}))
                                UNION
                                select s.* from ERPHTJieSuan s where NWorkToDoID is NULL and beiyong1 in ({0})".FormatWith(xmbhsql);
        var htjslist = Conv<ZWL.BLL.ERPHTJieSuan>.GetList(htjiesuansql);
        if (htjslist != null && htjslist.Any())
        {
            foreach (var item in htjslist.GroupBy(r => r.HTBH))
            {
                if (item.Count() > 1)
                {
                    var mxid = item.Max(r => r.ID);
                    var selectitem = item.FirstOrDefault(r => r.ID == mxid);
                    if (selectitem != null)
                    {
                        jiesuansum += selectitem.JSJE.Value;
                    }
                }
                else
                    jiesuansum += item.FirstOrDefault().JSJE.Value;
            }
        }
        else
        {
            var xModel = new ZWL.BLL.ERPXMJBXX();
            xModel = xModel.GetModelByXMBH(xmbh);
            if (xModel != null)
            {
                jiesuansum = xModel.XMJF;
            }
        }
        result.yiSJE = daozhangsum;
        result.yiFJE = costsum;
        result.yingSJE = shetonglist.Sum(p => p.HTJE) - daozhangsum;
        result.yingFJE = fhetonglist.Sum(p => p.HTJE) - costsum;
        result.jieSJE = jiesuansum;
        if (baseDate.HasValue)
        {
            var tempModel = new ZWL.BLL.ERPXMJBXXExtend();
            tempModel = tempModel.GetModelBySqlWhere("XMBH='{0}'".FormatWith(xmbh));
            if (tempModel != null)
            {
                if (tempModel.YSGCKBase.HasValue)
                    result.yiSJE += tempModel.YSGCKBase.Value;
                if (tempModel.YFLWFBase.HasValue)
                    result.yiFJE += tempModel.YFLWFBase.Value;
                if (tempModel.YSJEBase.HasValue)
                    result.yingSJE += tempModel.YSJEBase.Value;
                if (tempModel.YFJEBase.HasValue)
                    result.yingFJE += tempModel.YFJEBase.Value;
            }
        }
        return result;
    }
    public static string GetXMPZR(string xmbh)
    {
        var shetongsql = @"select h.* from ERPHeTong h	join ERPNWorkToDo d on h.NWorkToDoID=D.ID
                            where StateNow in ('正在办理','正常结束') and HTLB = '收款' and XMID in ('{0}') ".FormatWith(xmbh);
        var shetonglist = Conv<ZWL.BLL.ERPHeTong>.GetList(shetongsql);
        if (!shetonglist.Any())
        {
            return "";
        }
        var res = "";

        var maxshoukuan = shetonglist.OrderByDescending(p => p.HTJE).FirstOrDefault();
        if (maxshoukuan.HTJE < 50 * 10000)
        {
            res = DbHelperSQL.GetSHSL("SELECT [UserName] FROM [ERPNWorkToDoLog] where [ParentID]='" + maxshoukuan.NWorkToDoID + "' and [RecordID]='129'");
        }
        else if (maxshoukuan.HTJE >= 50 * 10000 && maxshoukuan.HTJE < 300 * 10000)
        {
            res = DbHelperSQL.GetSHSL("SELECT top 1 [UserName] FROM [ERPNWorkToDoLog] where [ParentID]='" + maxshoukuan.NWorkToDoID + "' and [RecordID] in ('131', '272')");
        }
        else
        {
            res = DbHelperSQL.GetSHSL("SELECT [UserName] FROM [ERPNWorkToDoLog] where [ParentID]='" + maxshoukuan.NWorkToDoID + "' and [RecordID]='416'");
        }
        if (res == "")
        {
            var workToDo = new ZWL.BLL.ERPNWorkToDo();
            workToDo.GetModel(maxshoukuan.NWorkToDoID);
            if (workToDo.ID > 0)
            {
                var yList = PublicMethod.GetShenPiYiJianList(workToDo.ShenPiYiJian);
                yList.Reverse();
                if (yList.Count > 0)
                {
                    if (maxshoukuan.HTJE < 50 * 10000)
                    {
                        res = yList[0].UserName;
                    }
                    else if (maxshoukuan.HTJE >= 50 * 10000 && maxshoukuan.HTJE < 300 * 10000)
                    {
                        if (yList.Count > 2)
                        {
                            res = yList[2].UserName;
                        }
                        else
                        {
                            res = "";
                        }
                    }
                    else
                    {

                        if (yList.Count > 3)
                        {
                            res = yList[3].UserName;
                        }
                        else
                        {
                            res = "";
                        }
                    }
                }
            }
        }
        return res;
    }
    public static string GetXMZL(string xmbh)
    {
        var htjiesuansql = @"select s.* from ERPHTJieSuan s join ERPNWorkToDo d on s.NWorkToDoID=d.ID 
                                where StateNow in ('正常结束')
                                and HTBH in (select HTID from ERPHeTong h	join ERPNWorkToDo d on h.NWorkToDoID=D.ID
                                where StateNow in ('正在办理','正常结束') and XMID in ('{0}'))
                                UNION
                                select s.* from ERPHTJieSuan s where NWorkToDoID is NULL and beiyong1 in ('{0}')".FormatWith(xmbh);
        var htjslist = Conv<ZWL.BLL.ERPHTJieSuan>.GetList(htjiesuansql);
        if (htjslist.Where(p => p.JSTime != null).Any())
        {
            var maxtime = htjslist.Where(p => p.JSTime != null).Max(p => Convert.ToDateTime(p.JSTime));
            if (maxtime.AddYears(1) > DateTime.Now)
            {
                return "<1年";
            }
            else if (maxtime.AddYears(1) <= DateTime.Now && maxtime.AddYears(3) >= DateTime.Now)
            {
                return "1-3年";
            }
            else if (maxtime.AddYears(3) <= DateTime.Now && maxtime.AddYears(5) >= DateTime.Now)
            {
                return "3-5年";
            }
            else if (maxtime.AddYears(5) < DateTime.Now)
            {
                return ">5年";
            }
        }
        return "";
    }
    public static string GetXMZQFSSJ(string xmbh)
    {
        var htjiesuansql = @"select s.* from ERPHTJieSuan s join ERPNWorkToDo d on s.NWorkToDoID=d.ID 
                                where StateNow in ('正常结束')
                                and HTBH in (select HTID from ERPHeTong h	join ERPNWorkToDo d on h.NWorkToDoID=D.ID
                                where StateNow in ('正在办理','正常结束') and XMID in ('{0}'))
                                UNION
                                select s.* from ERPHTJieSuan s where NWorkToDoID is NULL and beiyong1 in ('{0}')".FormatWith(xmbh);
        var htjslist = Conv<ZWL.BLL.ERPHTJieSuan>.GetList(htjiesuansql);
        if (htjslist.Where(p => p.JSTime != null).Any())
        {
            var maxtime = htjslist.Where(p => p.JSTime != null).Max(p => Convert.ToDateTime(p.JSTime));
            return maxtime.ToString("yyyy-MM-dd");
        }
        return "";
    }
    public static string GetXMZQZSQ(string xmbh)
    {
        var htjiesuansql = @"select s.* from ERPHTJieSuan s join ERPNWorkToDo d on s.NWorkToDoID=d.ID 
                                where StateNow in ('正常结束')
                                and HTBH in (select HTID from ERPHeTong h	join ERPNWorkToDo d on h.NWorkToDoID=D.ID
                                where StateNow in ('正在办理','正常结束') and XMID in ('{0}'))
                                UNION
                                select s.* from ERPHTJieSuan s where NWorkToDoID is NULL and beiyong1 in ('{0}')".FormatWith(xmbh);
        var htjslist = Conv<ZWL.BLL.ERPHTJieSuan>.GetList(htjiesuansql);
        if (htjslist.Where(p => p.JSTime != null).Any())
        {
            var maxtime = htjslist.Where(p => p.JSTime != null).Max(p => Convert.ToDateTime(p.JSTime));
            var dist = maxtime.AddYears(3) - DateTime.Now;
            return dist.Days.ToString() + "天";
        }
        return "";
    }
    public static List<ZWL.BLL.ERPHeTong> GetXMRelativeEffetiveHeTongList(string xmbh)
    {
        var sqlWhere = @"select h.* from ERPHeTong h join ERPNWorkToDo d on h.NWorkToDoID=d.ID
                            where StateNow not in ('已被驳回','不通过') and 
                            JieDianID in (select ID from ERPNWorkFlowNode where WorkFlowID in(select ID from ERPNWorkFlow where FormID=d.FormID) and CAST(NodeSerils as int)>2)
                            and XMID='{0}'".FormatWith(xmbh);
        var htlist = Conv<ZWL.BLL.ERPHeTong>.GetList(sqlWhere);
        return htlist;
    }
    public static void FillEntity<T>(ZWL.BLL.ModelBase info, string proName, object value)
    {
        Type t = typeof(T);
        PropertyInfo[] t_propinfos = t.GetProperties();
        foreach (var item in t_propinfos)
        {
            if (item.Name == proName && item.CanWrite)
            {
                item.SetValue(info, DataTableHelper.ChangeType(value, item.PropertyType), null);
                break;
            }
        }
    }
    public static void FillEntity<T>(ZWL.BLL.ModelBase info, DataRow row)
    {
        Type t = typeof(T);
        PropertyInfo[] t_propinfos = t.GetProperties();
        foreach (var item in t_propinfos)
        {
            //if (item.Name == proName && item.CanWrite)
            //{
            //    item.SetValue(info, DataTableHelper.ChangeType(value, item.PropertyType), null);
            //    break;
            //}
        }
    }
    public static T ConverToTEntity<T>(object value)
    {
        Type t = typeof(T);
        PropertyInfo[] t_propinfos = t.GetProperties();
        T obj = (T)t.Assembly.CreateInstance(t.FullName);
        if (value != null)
        {
            Type val_t = value.GetType();

            PropertyInfo[] val_t_propinfos = val_t.GetProperties();

            foreach (PropertyInfo vp in val_t_propinfos)
            {
                PropertyInfo tp = t_propinfos.FirstOrDefault(m => m.Name == vp.Name);
                if (tp != null && tp.CanWrite)
                {
                    object val = vp.GetValue(value, null);
                    tp.SetValue(obj, DataTableHelper.ChangeType(val, tp.PropertyType), null);
                }
            }
        }
        return obj;
    }
    public static T MapRequestToModel<T>(HttpRequest Request)
    {
        Type t = typeof(T);
        PropertyInfo[] t_propinfos = t.GetProperties();
        T obj = (T)t.Assembly.CreateInstance(t.FullName);
        if (Request != null)
        {
            PropertyInfo[] val_t_propinfos = t.GetProperties();

            foreach (PropertyInfo vp in val_t_propinfos)
            {
                PropertyInfo tp = t_propinfos.FirstOrDefault(m => m.Name == vp.Name);
                if (tp != null && tp.CanWrite)
                {
                    object val = Request[tp.Name];
                    if (val != null)
                        tp.SetValue(obj, DataTableHelper.ChangeType(val, tp.PropertyType), null);
                }
            }
        }
        return obj;
    }

    public static string GetInputByRequestInputStream(Stream stream)
    {
        var re = "";
        if (stream != null && stream.Length > 0)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                re = reader.ReadToEnd();
            }
        }
        return re;
    }

    public static string IntegBizSysAPIBaseUrl
    {
        get
        {
            var url = ConfigurationManager.AppSettings.Get("IntegBizSysAPIBaseUrl");
            return url;
        }
    }
}
public static class Extension
{
    private static ArrayList jobs = new ArrayList();
    public static T GetModel<T>()
    {
        return (T)Activator.CreateInstance(typeof(T));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static JsonResult Accept(this IHttpHandler obj, HttpContext context)
    {
        var result = new JsonResult(false, "请求缺少必要参数。");
        var action = context.Request["Action"];
        if (action == null) action = context.Request["action"];
        if (action == null) action = context.Request["f"];
        var form = string.Empty;
        if (context.Request.RequestType == "POST" && context.Request.ContentType.StartsWith("application/json")
            && !context.Request.ContentType.StartsWith("multipart/form-data"))
        {
            form = Util.GetInputByRequestInputStream(context.Request.InputStream);
            if (!form.IsNullOrEmpty())
            {
                var json = JsonConvert.DeserializeObject<dynamic>(form);
                if (json["Action"] != null)
                    action = json["Action"].ToString();
                if (action == null && json["action"] != null)
                    action = json["action"].ToString();
                if (action == null && json["f"] != null)
                    action = json["f"].ToString();
            }
        }
        #region MyRegion
        /*var username = PublicMethod.GetCookie("AdminName", "DTcms");
        if (username.IsNullOrEmpty() && context.Session != null && context.Session["UserName"] != null)
        {
            username = context.Session["UserName"].ToString();
        }
        var log = new ZWL.BLL.ERPRiZhi
        {
            UserName = username,
            DoSomething = "调用Services接口服务:{0}".FormatWith(action),
            IpStr = context.Request.UserHostAddress.ToString(),
            TimeStr = context.Timestamp
        };
        log.ID = log.Add(); */
        #endregion
        if (!string.IsNullOrEmpty(action))
        {
            MethodInfo selectedMethod = null;
            object selectedType = null;
            foreach (var item in Jobs)
            {
                var type = item.GetType();
                if (type == null) continue;
                var method = type.GetMethod(action);
                if (method != null)
                {
                    selectedMethod = method;
                    selectedType = item;
                    break;
                }
            }
            if (selectedMethod != null)
            {
                try
                {
                    var attrs = selectedMethod.GetCustomAttributes(typeof(AuthorizeAttribute), true);
                    if (attrs == null || attrs.Length <= 0)
                    {
                        attrs = selectedMethod.DeclaringType.GetCustomAttributes(typeof(AuthorizeAttribute), true);
                    }
                    if (attrs != null && attrs.Length > 0)
                    {
                        if (!((AuthorizeAttribute)attrs[0]).IsAuthorized(context))
                        {
                            return new JsonResult(false, "验证失败");
                        }
                    }
                    var hmattr = selectedMethod.GetCustomAttributes(typeof(HttpMethodAttribute), true);
                    if (hmattr == null || hmattr.Length <= 0)
                    {
                        hmattr = selectedMethod.DeclaringType.GetCustomAttributes(typeof(HttpMethodAttribute), true);
                    }
                    if (hmattr != null && hmattr.Length > 0)
                    {
                        if (!((HttpMethodAttribute)hmattr[0]).IsAuthorized(context))
                        {
                            return new JsonResult(false, "验证失败");
                        }
                    }
                    var seattr = selectedMethod.GetCustomAttributes(typeof(AuthSessionAttribute), true);
                    if (seattr == null || seattr.Length <= 0)
                    {
                        seattr = selectedMethod.DeclaringType.GetCustomAttributes(typeof(AuthSessionAttribute), true);
                    }
                    if (seattr != null && seattr.Length > 0)
                    {
                        if (!((AuthSessionAttribute)seattr[0]).IsAuthorized(context))
                        {
                            return new JsonResult(false, "验证失败");
                        }
                    }
                    var parameters = selectedMethod.GetParameters();
                    object[] args = new object[parameters.Length];
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        if (i == 0)
                        {
                            args[0] = context.Request;
                        }
                        else
                        {
                            args[i] = form;
                        }
                    }
                    result = (JsonResult)selectedMethod.Invoke(selectedType, args);
                }
                catch (Exception e)
                {
                    return new JsonResult(false, e.Message);
                }
            }
        }
        return result;
    }
    private static ArrayList Jobs
    {
        get
        {
            if (jobs.Count > 0) return jobs;
            var types = Assembly.GetCallingAssembly().GetTypes().Where(e => e.GetInterfaces().Contains(typeof(IRequestJob)));
            if (types != null && types.Any())
            {
                foreach (var item in types)
                {
                    if (item.IsInterface) continue;
                    var checkFlag = false;
                    for (int i = 0; i < jobs.Count; i++)
                    {
                        var sitem = jobs[i];
                        if (sitem.GetType().Name == item.Name)
                        {
                            checkFlag = true;
                            break;
                        }
                    }
                    if (!checkFlag)
                    {
                        var job = (IRequestJob)Activator.CreateInstance(item);
                        if (job != null)
                            jobs.Add(job);
                    }
                }
            }
            return jobs;
        }
    }
}
