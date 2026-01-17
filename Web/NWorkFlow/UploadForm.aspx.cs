using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZWL.Common;
using ZWL.DBUtility;

public partial class NWorkFlow_UploadForm : System.Web.UI.Page
{
    public List<ZWL.BLL.ERPSaveFileName> ZWFiles = new List<ZWL.BLL.ERPSaveFileName>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var workId = PublicMethod.CheckInt(PublicMethod.GetDecryptParam("ID"));
            var model = new ZWL.BLL.ERPNWorkToDo();
            model.GetModel(workId);
            if (model != null && model.FormID == 49 && !string.IsNullOrEmpty(model.BeiYong2))
            {
                HiddenField_ZhengWenList.Value = model.BeiYong2;
                foreach (var item in model.BeiYong2.Split('|'))
                {
                    if (string.IsNullOrEmpty(item)) continue;
                    var sfile = new ZWL.BLL.ERPSaveFileName();
                    sfile = sfile.GetModelByNowName(item);
                    if (sfile != null)
                    {
                        ZWFiles.Add(sfile);
                    }
                }

                var zw = GetWenJianName(this.HiddenField_ZhengWenList.Value);
                HiddenField_ZhengWenListName.Value = zw;
            }
        }
    }

    protected void ImageButton21_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        string FileNameStr = UploadFileIntoDir(this.FileUpload2, DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName));
        if (this.HiddenField_ZhengWenList.Value.Trim() == "")
        {
            this.HiddenField_ZhengWenList.Value = FileNameStr;
        }
        else
        {
            this.HiddenField_ZhengWenList.Value += "|" + FileNameStr;
        }
        foreach (var item in HiddenField_ZhengWenList.Value.Split('|'))
        {
            if (string.IsNullOrEmpty(item)) continue;
            var sfile = new ZWL.BLL.ERPSaveFileName();
            sfile = sfile.GetModelByNowName(item);
            if (sfile != null)
            {
                ZWFiles.Add(sfile);
            }
        }

        updatezw(FileNameStr, "添加");

        var zw = GetWenJianName(this.HiddenField_ZhengWenList.Value);
        HiddenField_ZhengWenListName.Value = zw;
    }

    protected void ImageButton31_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            foreach (var item in HiddenField_ZhengWenList.Value.Split('|'))
            {
                if (string.IsNullOrEmpty(item)) continue;
                var sfile = new ZWL.BLL.ERPSaveFileName();
                sfile = sfile.GetModelByNowName(item);
                if (sfile != null)
                {
                    ZWFiles.Add(sfile);
                }
            }

            updatezw(HiddenField_Deletefile.Value, "删除");
            HiddenField_Deletefile.Value = "";
            var zw = GetWenJianName(this.HiddenField_ZhengWenList.Value);
            HiddenField_ZhengWenListName.Value = zw;
        }
        catch
        { }
    }
    public void updatezw(string filename, string control)
    {
        var UserName = PublicMethod.GetUserName();
        var sfile = new ZWL.BLL.ERPSaveFileName();
        sfile = sfile.GetModelByNowName(filename);
        var downloadlink = "../DsoFramer/DownLoadFile.aspx?f=../UpLoadFile/" + sfile.NowName + "&n=" + sfile.OldName;
        var addzwfileLink = "" + control + "了正文：<a href=\"" + downloadlink + "\" target=\"_blank\">" + sfile.OldName + "</a> ";
        //var PiShiStr = "<font color=\"#0000FF\">" + UserName + "&nbsp;&nbsp;" + DateTime.Now.ToString() + "&nbsp;&nbsp;</font><BR><div class=\"showShenPiYiJianFormat\">修改记录：</div>" + addzwfileLink + "<hr>";
        var PiShiStr = "<font color=\"#0000FF\">" + UserName + "&nbsp;&nbsp;" + DateTime.Now.ToString() + "&nbsp;&nbsp;</font><BR><div class=\"showShenPiYiJianFormat\">修改记录：</div>" + addzwfileLink + "<hr>";

        var workId = PublicMethod.CheckInt(PublicMethod.GetDecryptParam("ID"));
        ZWL.BLL.ERPFaWenDJ FaWen = new ZWL.BLL.ERPFaWenDJ();
        FaWen.NWorkGetModel(Convert.ToInt32(workId));
        FaWen.ZhengWen = this.HiddenField_ZhengWenList.Value;
        FaWen.Update();
        var model = new ZWL.BLL.ERPNWorkToDo();
        model.GetModel(workId);
        model.BeiYong2 = this.HiddenField_ZhengWenList.Value;
        model.ShenPiYiJian = PiShiStr + model.ShenPiYiJian;
        model.Update();
        WorkLogHelper.WriteWorkLog(model.ID, "修改正文", "修改正文", PiShiStr, "");
    }

    public static string UploadFileIntoDir(FileUpload MyFile, string DirName)
    {
        if (IfOkFile(DirName) == true)
        {
            string ReturnStr = string.Empty;
            if (MyFile.FileContent.Length > 0)
            {
                MyFile.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("../UploadFile/"), DirName));
                //将原文件名与现在文件名写入ERPSaveFileName表中
                string NowName = DirName;
                
                string OldName = Path.GetFileNameWithoutExtension(MyFile.FileName) + "(" + ZWL.Common.PublicMethod.GetSessionValue("UserName") + "改)" + Path.GetExtension(MyFile.FileName);
                string SqlTempStr = "insert into ERPSaveFileName(NowName,OldName) values ('" + NowName + "','" + OldName + "')";
                DbHelperSQL.ExecuteSQL(SqlTempStr);
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
            int PointPosition = DirName.LastIndexOf(".");
            string FileExd = DirName.Substring(PointPosition, DirName.Length - PointPosition).Replace(".", "");
            string JKL = DbHelperSQL.GetSHSL("select FileType from ERPSystemSetting where FileType like '%|" + FileExd + "|%'");
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

    //得到正文附件文件名
    public static string GetWenJianName(string WenJianList)
    {
        if (!string.IsNullOrEmpty(WenJianList))
        {
            string[] MyRange = WenJianList.Split('|');
            string MyReturn = string.Empty;
            for (int i = 0; i < MyRange.Length; i++)
            {
                if (MyRange[i].ToString().Trim().Length > 0)
                {
                    if (MyReturn.Trim().Length > 0)
                    {
                        string OldNameStr = ZWL.DBUtility.DbHelperSQL.GetSHSL("select OldName from ERPSaveFileName where NowName='" + MyRange[i].ToString() + "'");
                        if (OldNameStr.Trim().Length <= 0)
                        {
                            OldNameStr = MyRange[i].ToString();
                        }
                        MyReturn = MyReturn + "," + OldNameStr;

                    }
                    else
                    {
                        string OldNameStr = ZWL.DBUtility.DbHelperSQL.GetSHSL("select OldName from ERPSaveFileName where NowName='" + MyRange[i].ToString() + "'");
                        if (OldNameStr.Trim().Length <= 0)
                        {
                            OldNameStr = MyRange[i].ToString();
                        }
                        MyReturn = OldNameStr;

                    }
                }
            }
            if (MyReturn.ToString().Trim().Length <= 0)
            {
                MyReturn = MyReturn + "无文件！";
            }
            return MyReturn;
        }
        return "无文件！";
    }
}