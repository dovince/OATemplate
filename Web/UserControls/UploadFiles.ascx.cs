using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using ZWL.Common;

public partial class UserControls_UploadFiles : UserControl
{
    #region property
    private string _attachments = string.Empty;
    private string _callBackFunc = string.Empty;
    private bool _canDel = true;
    private bool _canView = true;
    private bool _canEdit = true;
    protected List<FileViewModel> _list = new List<FileViewModel>();
    public string Result
    {
        get
        {
            if (ViewState["Result"] != null)
                return ViewState["Result"].ToString();
            else
                return string.Empty;
        }
    }
    public bool CanDel
    {
        get
        {
            if (ViewState["CanDel"] != null)
                return Convert.ToBoolean(ViewState["CanDel"].ToString());
            else
                return _canDel;
        }
        set
        {
            value = _canDel;
            ViewState["CanDel"] = _canDel.ToString().ToLower();
        }
    }
    public bool CanView
    {
        get
        {
            if (ViewState["CanView"] != null)
                return Convert.ToBoolean(ViewState["CanView"].ToString());
            else
                return _canView;
        }
        set
        {
            value = _canView;
            ViewState["CanView"] = _canView.ToString().ToLower();
        }
    }
    public bool CanEdit
    {
        get
        {
            if (ViewState["CanEdit"] != null)
                return Convert.ToBoolean(ViewState["CanEdit"].ToString());
            else
                return _canEdit;
        }
        set
        {
            value = _canEdit;
            ViewState["CanEdit"] = _canEdit.ToString().ToLower();
        }
    }
    public string Attachments
    {
        get
        {
            return _attachments;
        }
        set
        {
            _attachments = value;
        }
    }
    public string CallBackFunc
    {
        get
        {
            if (ViewState["CallBackFunc"] != null)
                return ViewState["CallBackFunc"].ToString();
            else
                return _callBackFunc;
        }
        set
        {
            value = _callBackFunc;
            ViewState["CallBackFunc"] = _callBackFunc.ToString().ToLower();
        }
    }
    #endregion
    public void Init(string attachments)
    {
        _attachments = attachments;
    }
    public void InitFunc(string func)
    {
        _callBackFunc = func;
    }
    public void Init(bool canDel,bool canView,bool canEdit)
    {
        _canDel = canDel;
        _canView = canView;
        _canEdit = canEdit;
    }
    public void Init(string attachments,bool canDel,bool canView,bool canEdit)
    {
        _attachments = attachments;
        _canDel = canDel;
        _canView = canView;
        _canEdit = canEdit;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetResultList();
            FilesViewHtml();
        }
        else
        {
            ViewState["Result"] = PublicMethod.Get("HiddenField_Attachments");
            if (!string.IsNullOrEmpty(Result))
            {
                _attachments = Result;
                SetResultList();
                FilesViewHtml();
            }
        }
    }
    private void SetResultList()
    {
        var atts = _attachments.Split('|');
        foreach (var item in atts)
        {
            if (string.IsNullOrEmpty(item)) continue;
            var model = new ZWL.BLL.ERPSaveFileName();
            model = model.GetModelByNowName(item);
            if (model != null)
            {
                var tModel = Util.ConverToTEntity<FileViewModel>(model);
                tModel.FileSize = PublicMethod.GetFileSize(item);
                _list.Add(tModel);
            }
        }
    }
    private void FilesViewHtml()
    {
        var downUrl = "../DsoFramer/DownLoadFile.aspx?f=../UpLoadFile/{0}&n={1}";
        var readUrl = "../FlexPaperFlash/SWFShow.aspx?f={0}&n={1}";
        var html = string.Empty;
        var filePath = HttpContext.Current.Server.MapPath("~/Html/UploadFileViewRow.html");
        var htmlStr = File.ReadAllText(filePath, System.Text.Encoding.UTF8);
        foreach (var item in _attachments.Split('|'))
        {
            if (string.IsNullOrEmpty(item)) continue;
            var savefile = new ZWL.BLL.ERPSaveFileName();
            savefile = savefile.GetModelByNowName(item);
            if (savefile != null)
            {
                var dUrl = string.Format(downUrl, savefile.NowName, savefile.OldName);
                var rUrl = string.Format(readUrl, savefile.NowName, savefile.OldName);

                html += string.Format(htmlStr, savefile.OldName, PublicMethod.GetFileSize(item), rUrl, dUrl, savefile.NowName);
            }
        }
        att_xtable_attprojectMain.InnerHtml = html;
    }
    protected class FileViewModel : ZWL.BLL.ERPSaveFileName
    {
        public string FileSize { get; set; }
    }
}