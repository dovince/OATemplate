using System;
using System.IO;
using ZWL.Common;
using System.Web.UI.WebControls;
using System.Collections.Generic;

public partial class Services_WebUploader : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            if (FileUploadInput.HasFile)
            {
                var fileSize = PublicMethod.GetByteUnit(FileUploadInput.PostedFile.ContentLength);
                var fileName = FileUploadInput.FileName;
                var nowName = PublicMethod.UploadFileIntoDir2(FileUploadInput, DateTime.Now.Ticks.ToString() + Path.GetExtension(FileUploadInput.PostedFile.FileName));
                var list = new List<object>();
                var o = new
                {
                    nowName = nowName,
                    oldName = fileName,
                    showHtml = string.Format(upload_list_tr_edit_block.InnerHtml, fileName, fileSize, string.IsNullOrEmpty(nowName) ? 0 : 100, string.IsNullOrEmpty(nowName) ? "失败" : "成功", nowName)
                };
                list.Add(o);
                ClientScript.RegisterClientScriptBlock(GetType(), "callback", string.Format("callback({0})", new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(list)), true);
            }
        }
        else
        {
            var workId = PublicMethod.GetInt(PublicMethod.GetDecryptParam("ID"));
            if (!FileUploadInput.HasFile && workId > 0)
            {
                var list = new List<object>();
                var model = new ZWL.BLL.ERPNWorkToDo();
                model.GetModel(workId);
                if (!string.IsNullOrEmpty(model.FuJianList))
                {
                    var downUrl = "../DsoFramer/DownLoadFile.aspx?f=../UpLoadFile/{0}&n={1}";
                    var readUrl = "../FlexPaperFlash/SWFShow.aspx?f={0}&n={1}";
                    foreach (var item in model.FuJianList.Split('|'))
                    {
                        if (string.IsNullOrEmpty(item)) continue;
                        var savefile = new ZWL.BLL.ERPSaveFileName();
                        savefile = savefile.GetModelByNowName(item);
                        if (savefile != null)
                        {
                            var dUrl = string.Format(downUrl, savefile.NowName, savefile.OldName);
                            var rUrl = string.Format(readUrl, savefile.NowName, savefile.OldName);
                            var o = new
                            {
                                nowName = savefile.NowName,
                                oldName = savefile.OldName,
                                showHtml = string.Format(upload_list_tr_view_block.InnerHtml, savefile.OldName, PublicMethod.GetFileSize(item), rUrl, dUrl,savefile.NowName)
                            };
                            list.Add(o);
                        }
                    }
                    ClientScript.RegisterClientScriptBlock(GetType(), "callback", string.Format("callback({0})", new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(list)), true);
                }
            }
        }
    }
    /// <summary>
    /// 根据FileUpload控件名获取上传文件(大小)类型
    /// </summary>
    /// <param name="upload">FileUpload控件名</param>
    /// <returns>上传文件(大小)类型</returns>
    public string GetFileSize(FileUpload upload)
    {
        return PublicMethod.GetByteUnit(upload.PostedFile.ContentLength);
    }
}