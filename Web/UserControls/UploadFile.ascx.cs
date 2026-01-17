using System;
using System.Collections.Generic;
using System.Web.UI;
using ZWL.Common;

public partial class UserControls_UploadFile : UserControl
{
    #region property
    private string _attachments = string.Empty;
    private bool _canDel = true;
    private bool _canView = true;
    private bool _canEdit = true;
    protected List<ZWL.BLL.ERPSaveFileName> _list = new List<ZWL.BLL.ERPSaveFileName>();
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
    #endregion
    public void Init(string attachments)
    {
        _attachments = attachments;
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
        }
        else
        {
            ViewState["Result"] = PublicMethod.Get("HiddenField_Attachments");
            if (!string.IsNullOrEmpty(Result))
            {
                _attachments = Result;
                SetResultList();
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
            if (model != null) _list.Add(model);
        }
    }
}