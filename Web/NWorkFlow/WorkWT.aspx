<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkWT.aspx.cs" Inherits="NWorkFlow_WorkWT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head runat="server">
    <title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" rel="stylesheet" />
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
    <script type="text/javascript">
        function PrintTable() {
            document.getElementById("PrintHide").style.visibility = "hidden"
            print();
            document.getElementById("PrintHide").style.visibility = "visible"
        }

    </script>
</head>
<body class="lui_form_body">
    <form id="form1" runat="server">
        <div>
            <table class="tb_normal" style="width: 100%">   
                <tr>
                    <td valign="middle" style="border-bottom: #006633 1px ; height: 30px;">&nbsp;<img src="../images/BanKuaiJianTou.gif" />
                        <a class="hei" href="../Main/MyDesk.aspx">桌面</a>&nbsp;>>&nbsp;审批流程 &gt;&gt; 设置工作委托
                    </td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px ; height: 30px;">&nbsp;<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/Button/Submit.jpg"
                        OnClick="ImageButton1_Click" />
                        <img src="../images/Button/JianGe.jpg" />&nbsp;
                    <img class="HerCss" onclick="javascript:window.history.go(-1)" src="../images/Button/BtnExit.jpg" />&nbsp;</td>
                </tr>
            </table>
            <table class="tb_normal" style="width: 100%">   
                <tr>
                    <td style="width: 170px; height: 25px; background-color: #f6f6f6" align="right">当前用户：
                    </td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:Label ID="Label1" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td align="right" style="width: 170px; height: 25px; background-color: #f6f6f6">受委托用户：</td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:TextBox ID="TextBox1" runat="server" Width="151px"></asp:TextBox>
                        <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectUser.aspx?TableName=ERPUser&LieName=UserName&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox1').value=wName;}"
                            src="../images/Button/search.gif" />&nbsp; <span style="color: #FF0000; font-weight: bold;">*如果不再需要委托办理，请清空受委托人，然后重新提交保存即可！</span></td>
                </tr>
                <tr>
                    <td align="right" style="width: 170px; height: 25px; background-color: #f6f6f6">委托说明：</td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <span style="color: #FF0000; font-weight: bold;">*委托人代理审批该用户的所有工作！</span></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
