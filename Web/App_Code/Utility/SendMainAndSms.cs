using System;
using System.Linq;
using System.Web.UI.WebControls;

/// <summary>
/// SendMainAndSms 的摘要说明
/// </summary>
public class SendMainAndSms
{
    public SendMainAndSms()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 发送内部短信和手机短信
    /// </summary>
    /// <param name="MailChk">内部短信选择框</param>
    /// <param name="SmsChk">手机短信选择框</param>
    /// <param name="ContentStr">发送消息内容</param>
    /// <param name="ToUserList">接收人列表</param>
    public static void SendMessage(CheckBox MailChk, CheckBox SmsChk, string ContentStr, string ToUserList)
    {

        if (SmsChk.Checked == true)
        {
            //发送手机信息
            Mobile.SendSMS("系统消息", ToUserList, ContentStr);
        }

        string[] UserListStr = ToUserList.Split(',');
        for (int i = 0; i < UserListStr.Length; i++)
        {
            if (MailChk.Checked == true)
            {
                //发送内部信息
                ZWL.BLL.ERPLanEmail MyMail = new ZWL.BLL.ERPLanEmail();
                MyMail.EmailContent = ContentStr;
                MyMail.EmailState = "未读";
                MyMail.EmailTitle = ContentStr;
                MyMail.FromUser = "系统消息";
                MyMail.FuJian = "";
                MyMail.TimeStr = DateTime.Now;
                MyMail.ToUser = UserListStr[i].ToString();
                MyMail.Add();
            }
        }
    }
    public static void SendMessage(CheckBox MailChk, CheckBox SmsChk, string ContentStr, ZWL.BLL.ERPNWorkToDo m)
    {
        SendMessage(MailChk, SmsChk, ContentStr, m.ShenPiUserList, m.FormID.Value, m.WorkFlowID.Value, m.BeiYong1, m.ID);
    }
    //发送内部邮件，传递FormID、WorkFlowID、BeiYong1,nworktodoid
    public static void SendMessage(CheckBox MailChk, CheckBox SmsChk, string ContentStr, string ToUserList, int FormID, int WorkFlowID, string BeiYong1, int nworktodoid)
    {
        if (SmsChk.Checked == true)
        {
            Mobile.SendSMS("系统消息", ToUserList, ContentStr);
        }

        string[] UserListStr = ToUserList.Split(',');
        for (int i = 0; i < UserListStr.Length; i++)
        {
            if (MailChk.Checked == true)
            {
                //发送内部信息
                ZWL.BLL.ERPLanEmail MyMail = new ZWL.BLL.ERPLanEmail();
                MyMail.EmailContent = ContentStr;
                MyMail.EmailState = "未读";
                MyMail.EmailTitle = ContentStr;
                MyMail.FromUser = "系统消息";
                MyMail.FuJian = "";
                MyMail.TimeStr = DateTime.Now;
                MyMail.ToUser = UserListStr[i].ToString();
                MyMail.FormID = FormID;
                MyMail.WorkFlowID = WorkFlowID;
                MyMail.BeiYong1 = BeiYong1;
                MyMail.WorkToDoID = nworktodoid;
                MyMail.Add();
            }
        }
    }
    //发送内部邮件，传递FormID、WorkFlowID、BeiYong1
    public static void SendMessage1(CheckBox MailChk, CheckBox SmsChk, string ContentStr, string ToUserList, int FormID, int WorkFlowID, string BeiYong1)
    {

        if (SmsChk.Checked == true)
        {
            //发送手机信息
            Mobile.SendSMS("系统消息", ToUserList, ContentStr);
        }

        string[] UserListStr = ToUserList.Split(',');
        for (int i = 0; i < UserListStr.Length; i++)
        {
            if (MailChk.Checked == true)
            {
                //发送内部信息
                ZWL.BLL.ERPLanEmail MyMail = new ZWL.BLL.ERPLanEmail();
                MyMail.EmailContent = ContentStr;
                MyMail.EmailState = "未读";
                MyMail.EmailTitle = ContentStr;
                MyMail.FromUser = "系统消息";
                MyMail.FuJian = "";
                MyMail.TimeStr = DateTime.Now;
                MyMail.ToUser = UserListStr[i].ToString();
                MyMail.FormID = FormID;
                MyMail.WorkFlowID = WorkFlowID;
                MyMail.BeiYong1 = BeiYong1;
                MyMail.Add();
            }
        }
    }
    //合同签订评审通过后给指定人发送内部邮件
    public static void SendMessage2(string ContentStr, string ToUserList, int FormID, int WorkFlowID, string BeiYong1)
    {

        string[] UserListStr = ToUserList.Split(',');
        for (int i = 0; i < UserListStr.Length; i++)
        {
            //发送内部信息
            ZWL.BLL.ERPLanEmail MyMail = new ZWL.BLL.ERPLanEmail();
            MyMail.EmailContent = ContentStr;
            MyMail.EmailState = "未读";
            MyMail.EmailTitle = ContentStr;
            MyMail.FromUser = "系统消息";
            MyMail.FuJian = "";
            MyMail.TimeStr = DateTime.Now;
            MyMail.ToUser = UserListStr[i].ToString();
            MyMail.FormID = FormID;
            MyMail.WorkFlowID = WorkFlowID;
            MyMail.BeiYong1 = BeiYong1;
            MyMail.Add();

        }
    }

    public static void SendMobileMessage(string ContentStr, string ToUserList)
    {

        //发送手机信息
        Mobile.SendSMS("系统消息", ToUserList, ContentStr);

    }

    //发送内部邮件，传递FormID、WorkFlowID、BeiYong1
    public static void SendMessage(bool IsMail, bool IsSms, string ContentStr, string ToUserList, int FormID, int WorkFlowID, string BeiYong1)
    {
        if (IsSms == true)
        {
            //发送手机信息
            Mobile.SendSMS("系统消息", ToUserList, ContentStr);
        }
        if (IsMail == true)
        {
            string[] UserListStr = ToUserList.Split(',');
            for (int i = 0; i < UserListStr.Length; i++)
            {
                //发送内部信息
                ZWL.BLL.ERPLanEmail MyMail = new ZWL.BLL.ERPLanEmail();
                MyMail.EmailContent = ContentStr;
                MyMail.EmailState = "未读";
                MyMail.EmailTitle = ContentStr;
                MyMail.FromUser = "系统消息";
                MyMail.FuJian = "";
                MyMail.TimeStr = DateTime.Now;
                MyMail.ToUser = UserListStr[i].ToString();
                MyMail.FormID = FormID;
                MyMail.WorkFlowID = WorkFlowID;
                MyMail.BeiYong1 = BeiYong1;
                MyMail.Add();
            }
        }
    }

}
