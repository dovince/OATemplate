<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileSmsAdd.aspx.cs" Inherits="Mobile_MobileSmsAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head runat="server">
    <title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" rel="stylesheet" />
    <link href="../Style/Style1.css" rel="stylesheet" />
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
    <script type="text/javascript">
        function PrintTable() {
            document.getElementById("PrintHide").style.visibility = "hidden"
            print();
            document.getElementById("PrintHide").style.visibility = "visible"
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
            font-size: small;
            background-color: #FF3300;
        }

        .auto-style2 {
            font-size: small;
        }
    </style>
</head>
<body class="lui_form_body">
    <form id="form1" runat="server">
        <div>
            <table class="tb_normal" style="width: 100%">
                <tr>
                    <td valign="middle" style="border-bottom: #006633 1px ; height: 30px;">&nbsp;<img src="../images/BanKuaiJianTou.gif" />
                        <a class="hei" href="../Main/MyDesk.aspx">桌面</a>&nbsp;>>&nbsp;手机短信&nbsp;>>&nbsp;新信息
                    </td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px ; height: 30px;">&nbsp; &nbsp;<img src="../images/Button/JianGe.jpg" />&nbsp;
                    <img class="HerCss" onclick="javascript:window.location.href='../Mobile/MobileSms.aspx'" src="../images/Button/BtnExit.jpg" />&nbsp;</td>
                </tr>
            </table>
            <table class="tb_normal" style="width: 100%">

                <tr>
                    <td align="right" colspan="2" style="height: 25px; background-color: #f6f6f6; text-align: center">
                        <strong><span class="auto-style2">内部短信群发</span></strong></td>
                </tr>

                <tr>
                    <td align="right" style="width: 170px; background-color: #f6f6f6; height: 25px;">接收用户：</td>
                    <td style="background-color: #ffffff; height: 25px; padding-left: 5px;">
                        <asp:TextBox ID="TextBox1" runat="server" Width="350px"></asp:TextBox>
                        <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectUser.aspx?TableName=ERPUser&LieName=UserName&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox1').value=wName;}"
                            src="../images/Button/search.gif" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                            ErrorMessage="*该项不可以为空" Display="Dynamic" ValidationGroup="Neibu"></asp:RequiredFieldValidator>&nbsp;
                    <span style="color: darkgray">* 请选择用户名，用于内部人员短信</span></td>
                </tr>
                <tr>
                    <td align="right" style="width: 170px; height: 25px; background-color: #f6f6f6">信息内容：</td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:TextBox ID="TextBox2" runat="server" Width="350px" Height="100px" TextMode="MultiLine"></asp:TextBox>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right" style="width: 170px; height: 25px; background-color: #f6f6f6"></td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/Button/Submit.jpg"
                            OnClick="ImageButton1_Click" ValidationGroup="Neibu" /></td>
                </tr>
                <tr>
                    <td align="right" style="width: 170px; height: 25px; background-color: #f6f6f6">备注：</td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <span class="auto-style1"><strong>短信限制70个字以内，如果接手用户没有收到短信，请与系统管理员联系。</strong></span></td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-left: 5px; height: 25px; background-color: #ffffff">&nbsp;</td>
                </tr>
                <%-- <tr>
            <td align="right" style="height: 25px; background-color: #f6f6f6; text-align: center;" colspan="2">
                <strong><span style="font-size: 10pt">外部短信群发</span></strong></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; background-color: #f6f6f6; height: 25px;" >
                接收用户：</td>
            <td style="background-color: #ffffff; height: 25px; padding-left:5px;" >
                <asp:TextBox ID="TextBox3" runat="server" Height="90px" TextMode="MultiLine" Width="350px"></asp:TextBox>
                <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectTXL.aspx?TableName=ERPUser&LieName=UserName&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox3').value=wName;}"
                    src="../images/Button/search.gif" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox3"
                    Display="Dynamic" ErrorMessage="*该项不可以为空" ValidationGroup="WaiBu"></asp:RequiredFieldValidator>&nbsp;
                <span style="color: darkgray">* 请输入手机号码列表，用 "," 分隔。</span></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #f6f6f6">
                信息内容：</td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:TextBox ID="TextBox4" runat="server" Height="50px" TextMode="MultiLine" Width="350px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="width: 170px; height: 25px; background-color: #f6f6f6">
            </td>
            <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/Button/Submit.jpg"
                        OnClick="ImageButton2_Click" ValidationGroup="WaiBu" /></td>
        </tr>--%>
            </table>
        </div>
    </form>
</body>
</html>
