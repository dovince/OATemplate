<%@ WebHandler Language="C#" Class="WebUploader" %>
using System;
using System.Web;
using ZWL.Common;
using System.Web.Script.Serialization;
using System.Linq;
using System.Web.SessionState;
using System.IO;
using System.Collections.Generic;

public class WebUploader : Base, IHttpHandler, IRequiresSessionState
{
    public delegate JsonResult MethodDelegate(HttpRequest Request);
    private static MethodDelegate method;
    public void ProcessRequest(HttpContext context)
    {
        var result = string.Empty;
        var f = context.Request["f"];
        if (!string.IsNullOrEmpty(f))
        {
            switch (f)
            {
                case "FileUpload":
                    method = new MethodDelegate(FileUpload);
                    break;
                case "UploadFile":
                    method = new MethodDelegate(UploadFile);
                    break;
            }
            if (method != null)
                result = new JavaScriptSerializer().Serialize(method(context.Request));

        }

        context.Response.ContentType = "text/html";
        context.Response.Write(result);
    }
    public JsonResult FileUpload(HttpRequest Request)
    {
        try
        {
            var fileInput = HttpContext.Current.Request.Files["fileInput"];
            var fileName = fileInput.FileName;
            if (fileName.Contains("\\"))
            {
                fileName = fileName.Split('\\').ToList().LastOrDefault();
            }

            if (PublicMethod.IfOkFile(fileName))
            {
                var FileNameStr = PublicMethod.UploadFileIntoDir(fileInput, DateTime.Now.Ticks.ToString() + Path.GetExtension(fileName));
                var fileSize = PublicMethod.GetByteUnit(fileInput.ContentLength);
                var sf = new FileViewModel() { NowName = FileNameStr, OldName = fileName, FileSize = fileSize };
                return JsonResult(true, "上传成功", sf);
            }
            else
            {
                return JsonResult(false, "不允许上传此类型文件！");
            }
        }
        catch
        {
            return JsonResult(false, "上传失败");
        }
    }
    public JsonResult UploadFile(HttpRequest Request)
    {
        var fileInput = HttpContext.Current.Request.Files["fileInput"];
        if (fileInput.ContentLength > 0)
        {
            var fileSize = PublicMethod.GetByteUnit(fileInput.ContentLength);
            var fileName = fileInput.FileName;
            if (fileName.Contains("\\"))
            {
                fileName = fileName.Split('\\').ToList().LastOrDefault();
            }
            var nowName = PublicMethod.UploadFileIntoDir(fileInput, DateTime.Now.Ticks.ToString() + Path.GetExtension(fileInput.FileName));
            var filePath = HttpContext.Current.Server.MapPath("~/Html/UploadFileEditRow.html");
            var htmlStr = File.ReadAllText(filePath, System.Text.Encoding.UTF8);
            var downUrl = string.Format("../DsoFramer/DownLoadFile.aspx?f=../UpLoadFile/{0}&n={1}", nowName, fileName);
            var readUrl = string.Format("../FlexPaperFlash/SWFShow.aspx?f={0}&n={1}", nowName, fileName);
            var list = new List<object>();
            var o = new
            {
                nowName = nowName,
                oldName = fileName,
                showHtml = string.Format(htmlStr, fileName, fileSize, readUrl, downUrl, nowName)
            };
            list.Add(o);
            return JsonResult(true, "上传成功", list);
        }
        return JsonResult(false, "上传失败", "");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    private class FileViewModel : ZWL.BLL.ERPSaveFileName
    {
        public string FileSize { get; set; }
    }
}