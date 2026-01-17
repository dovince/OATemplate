using O2S.Components.PDFRender4NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using ZWL.Common;
using PdfiumViewer;

public partial class FlexPaperFlash_SWFShow : System.Web.UI.Page
{
    public Dictionary<int, string> SwfFile = new Dictionary<int, string>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDefaultInput();
        }
    }
    private void LoadDefaultInput()
    {
        if (IsValidatedFile())
        {
            try
            {
                var PDFFilePath = System.Configuration.ConfigurationManager.AppSettings["PDFFilePath"];
                var SWFFilePath = System.Configuration.ConfigurationManager.AppSettings["SWFFilePath"];

                if (!Directory.Exists(HttpContext.Current.Server.MapPath(PDFFilePath)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(PDFFilePath));
                }
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(SWFFilePath)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(SWFFilePath));
                }

                //根据文件类型转换
                var f = Request["f"].ToLower();
                var ext = Path.GetExtension(f).ToLower();
                var filename = PublicMethod.MakeValidFileName1(Path.GetFileNameWithoutExtension(f.Trim()));
                var base64tag = "base64,";
                if (!f.IsNullOrEmpty() && f.StartsWith(base64tag))
                {
                    f = f.Replace(" ", "+");
                    f = PublicMethod.Base64Decode(f.Remove(0, f.IndexOf(base64tag) + base64tag.Length));
                }
                if (!f.IsNullOrEmpty() && (f.StartsWith("../") || f.StartsWith("../UploadFile/")))
                {
                    f = Path.GetFileName(f);
                    if (f.StartsWith("../UploadFile/"))
                        f = f.Replace("../UploadFile/", "");
                    if (f.StartsWith("../../UploadFile/"))
                        f = f.Replace("../../UploadFile/", "");
                    if (f.StartsWith("../"))
                        f = f.Replace("../", "");
                }
                var n = PublicMethod.UrlDecode(Request["n"]);
                if (!n.IsNullOrEmpty() && n.StartsWith(base64tag))
                {
                    n = n.Replace(" ", "+");
                    n = PublicMethod.Base64Decode(n.Remove(0, n.IndexOf(base64tag) + base64tag.Length));
                }
                if (string.IsNullOrEmpty(Path.GetExtension(n)))
                {
                    n += Path.GetExtension(f);
                }
                var filepath = string.Empty;
                if (Directory.Exists(PublicMethod.UploadFileFolderTruePath))
                {
                    var flist = Directory.GetFiles(PublicMethod.UploadFileFolderTruePath, f, SearchOption.AllDirectories);
                    if (flist != null && flist.Length > 0)
                    {
                        filepath = flist[0];
                    }
                }
                if (filepath.IsNullOrEmpty())
                {
                    var rootpath = Directory.GetParent(PublicMethod.UploadFileFolderTruePath);
                    if (rootpath != null)
                    {
                        var reportpath = Path.Combine(rootpath.Parent.FullName, "ReportFile");
                        if (Directory.Exists(reportpath))
                        {
                            var flist = Directory.GetFiles(reportpath, f, SearchOption.AllDirectories);
                            if (flist != null && flist.Length > 0)
                            {
                                filepath = flist[0];
                            }
                        }
                    }
                }
                if (filepath.IsNullOrEmpty())
                {
                    var reportpath = @"C:\inetpub\wwwroot\UploadFile\";
                    if (Directory.Exists(reportpath))
                    {
                        var flist = Directory.GetFiles(reportpath, f, SearchOption.AllDirectories);
                        if (flist != null && flist.Length > 0)
                        {
                            filepath = flist[0];
                        }
                    }
                }
                if (File.Exists(filepath) && (PublicMethod.AllowedOfficeType.Contains(ext) || ext == ".pdf"))//office file,pdf file
                {
                    var targetPath = HttpContext.Current.Server.MapPath(PDFFilePath + filename + ".pdf");
                    if (!File.Exists(targetPath))
                    {
                        if (ext.Contains(".doc"))
                        {
                            AsposeHelper.Word2Pdf(filepath, targetPath);
                        }
                        else if (ext.Contains(".xls"))
                        {
                            AsposeHelper.Excel2Pdf(filepath, targetPath);
                        }
                        else if (ext.Contains(".ppt"))
                        {
                            AsposeHelper.ppt2Pdf(filepath, targetPath);
                        }
                        else
                        {
                            File.Copy(filepath, targetPath, true);
                        }
                    }

                    var length = 0;
                    using (var document = PdfDocument.Load(targetPath))
                    {
                        length = document.PageCount;
                    }
                    for (int i = 0; i < length; i++)
                    {
                        SwfFile.Add(i + 1, filename);
                    }
                }
                else if (PublicMethod.AllowedPicType.Contains(ext)) //图片
                {
                    SwfFile.Add(1, "../UploadFile/" + f);
                }
                else
                {
                    if (ext == ".txt" || ext == ".html" || ext == ".htm")
                    {
                        var text = ToHtml(File.ReadAllText(filepath, Encoding.GetEncoding("gb2312")));
                        SwfFile.Add(0, text);
                    }
                }

                if (Request.QueryString["TelFileID"] != null && !string.IsNullOrEmpty(Request.QueryString["TelFileID"].ToString()))
                {
                    ZWL.DBUtility.DbHelperSQL.ExecuteSQL("update ERPTelFileRecipient set State = '已查看', LookDate = '" + DateTime.Now + "' where TelFileID=" + Request.QueryString["TelFileID"].ToString() + " and name='" + ZWL.Common.PublicMethod.GetUserName() + "'");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }
        else
        {
            MessageBox.Show(this, "不支持此文件的预览，请下载到本地再打开。");
        }
    }
    private bool IsValidatedFile()
    {
        var result = true;
        var file = Request["f"].ToLower();
        var ext = Path.GetExtension(file).ToLower();
        if (!PublicMethod.AllowedFileType.Contains(ext))
        {
            return false;
        }
        return result;
    }
    public string ToHtml(string text)
    {
        var sb = new StringBuilder();
        var sr = new StringReader(text);
        var str = sr.ReadLine();
        while (str != null)
        {
            str = str.TrimEnd();
            str.Replace("  ", " &nbsp;");
            if (str.Length > 80)
            {
                sb.AppendLine(string.Format("<p>{0}</p>", str));
            }
            else if (str.Length > 0)
            {
                sb.AppendLine(string.Format("{0}</br>", str));
            }
            str = sr.ReadLine();
        }
        return sb.ToString();
    }
}
