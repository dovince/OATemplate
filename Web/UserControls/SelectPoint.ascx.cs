using System;
using System.Web.Script.Serialization;
using System.Web.UI;
using ZWL.Common;

public partial class UserControls_SelectPoint : UserControl
{
    #region property
    private decimal? _lng = 0;
    private decimal? _lat = 0;
    private bool _isSelected = false;
    private string _action = string.Empty;
    public string Result
    {
        get
        {
            var result = "";
            if (IsSelected)
            {
               result = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(new PointResult { lng = _lng.Value, lat = _lat.Value });
            }
            return result;
        }
    }
    public string Action
    {
        get
        {
            return _action;
        }
        set
        {
            _action = value;
        }
    }
    public bool IsSelected
    {
        get
        {
            return _isSelected;
        }
        set
        {
            value = _isSelected;
        }
    }
    public decimal? lng
    {
        get
        {
            return _lng;
        }
        set
        {
            _lng = value;
        }
    }
    public decimal? lat
    {
        get
        {
            return _lat;
        }
        set
        {
            _lat = value;
        }
    }
    #endregion
    public void Init(decimal? l,decimal? a)
    {
        if(l.HasValue && a.HasValue)
        {
            _lng = l;
            _lat = a;
            _isSelected = true;
        }
    }
    public void Show(string attachments)
    {

    }
    public PointResult GetPointByAddress(string address)
    {
        PointResult result = null;
        var url = PublicMethod.BaiduGeocodingUrl + address;
        var responseText = PublicMethod.GetRemoteResultByURL(url);
        var model = new JavaScriptSerializer().Deserialize<WebLocationResult>(responseText);
        if (model != null)
        {
            result = model.result.location;
        }
        return result;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
        else
        {
            var point = PublicMethod.Get("HiddenPoint");
            if (!string.IsNullOrEmpty(point))
            {
                var json = new JavaScriptSerializer().Deserialize<PointResult>(point);
                _lng = json.lng;
                _lat = json.lat;
                _isSelected = true;
            }
        }
    }
}