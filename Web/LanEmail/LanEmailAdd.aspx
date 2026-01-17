<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LanEmailAdd.aspx.cs" Inherits="LanEmail_LanEmailAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" rel="stylesheet" />
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
    <script src="../UEditor/editor_config.js" type="text/javascript"></script>
    <script src="../UEditor/editor_all.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../UEditor/themes/default/ueditor.css" />
    <%--<script language="javascript" type="text/javascript">
        function PrintTable() {
            document.getElementById("PrintHide").style.visibility = "hidden"
            print();
            document.getElementById("PrintHide").style.visibility = "visible"
        }
    </script>--%>
</head>
<body class="lui_form_body">
    <form id="form1" runat="server">
        <div>
            <table class="tb_normal" style="width: 100%">
                <tr>
                    <td valign="middle" style="border-bottom: #006633 1px ; height: 30px;">&nbsp;<img src="../images/BanKuaiJianTou.gif" />
                        <a class="hei" href="../Main/MyDesk.aspx">桌面</a>&nbsp;>>&nbsp;内部邮件&nbsp;>>&nbsp;撰写新邮件
                    </td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px ; height: 30px;">&nbsp;<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/Button/Submit.jpg"
                        OnClick="ImageButton1_Click" />
                        &nbsp;&nbsp;
                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/Button/SubmitCaoGao.jpg"
                        OnClick="ImageButton4_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    
                    </td>
                </tr>
            </table>
            <table class="tb_normal" style="width: 100%">
                <tr>
                    <td align="right" style="width: 170px; background-color: #f6f6f6; height: 25px;">邮件主题：
                    </td>
                    <td style="background-color: #ffffff; height: 25px; padding-left: 5px;">
                        <asp:TextBox ID="TextBox1" runat="server" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                            ErrorMessage="*该项不可以为空"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 170px; height: 25px; background-color: #f6f6f6">接收人：
                    </td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:TextBox ID="TextBox2" runat="server" Width="350px"></asp:TextBox>
                        <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectUser.aspx?TableName=ERPUser&LieName=UserName&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox2').value=wName;}"
                            src="../images/Button/search.gif" />
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 170px; height: 25px; background-color: #f6f6f6">附件：
                    </td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal"
                            RepeatColumns="4">
                        </asp:CheckBoxList>
                        &nbsp;<asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False"
                            ImageAlign="AbsMiddle" ImageUrl="../images/Button/DelFile.jpg" OnClick="ImageButton3_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 170px; height: 25px; background-color: #f6f6f6">上传附件：
                    </td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="350px" />
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageAlign="AbsMiddle" ImageUrl="../images/Button/UpLoad.jpg"
                            OnClick="ImageButton2_Click" CausesValidation="False" />
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 170px; height: 25px; background-color: #f6f6f6">邮件内容：
                    </td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:TextBox ID="TxtContent" runat="server" Width="100%" Rows="10" TextMode="MultiLine"></asp:TextBox>
                        <%--<script type="text/javascript">
                        var editor = new baidu.editor.ui.Editor({ id: 'editor', minFrameHeight: 300 }); editor.render("TxtContent");
                    </script>--%>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
<script type="text/javascript">
    var editor = new baidu.editor.ui.Editor({ id: 'editor', minFrameHeight: 300 }); editor.render("TxtContent");
</script>
</html>
