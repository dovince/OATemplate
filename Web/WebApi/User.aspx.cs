using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebApi_User : System.Web.UI.Page
{
    //正确的时候返回的字符串
    private const string Success = "ok";
    protected void Page_Load(object sender, EventArgs e)
    {
        var method = Request["api_method"].ToLower();
        var result = "Not Define " + method;
        try
        {
            switch (method)
            {
                case "login":
                    result = Login();
                    break;
            }
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }

        Response.Write(result);
    }

    protected string Login()
    {
        var username = Request["username"];
        var pwd = Request["pwd"];
        var user = new ZWL.BLL.ERPUser();
        var result = user.NewUserLogin(username, pwd);
        if (result == "ok")
        {
            return Success;
        }
        else
        {
            return result;
        }
    }

}