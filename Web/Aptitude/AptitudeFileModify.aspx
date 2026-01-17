<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AptitudeFileModify.aspx.cs" Inherits="Aptitude_AptitudeFileModify" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>
        <%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" type="text/css" rel="stylesheet" />
    <link href="../Style/Style1.css" rel="stylesheet" />
    <link href="../CSS/cxcalendar.css" rel="stylesheet" />
    <link href="../CSS/default/easyui.css" rel="stylesheet" />
    <link href="../CSS/icon.css" rel="stylesheet" />
    <script type="text/javascript" src="../JS/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../CSS/calendar.js"></script>
    <script src="../JS/jquery.easyui.min.js"></script>
    <script src="../JS/easyui-lang-zh_CN.js"></script>
    <script>
        function openselectDepartment(op) {
            var wName;
            var RadNum = Math.random();
            wName = window.showModalDialog('../Main/SelectCondition.aspx?TableName=AptitudeFile&LieName=Department&Radstr=' + RadNum, '', 'dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');
            if (wName == null) { }
            else {
                $("#txtDepartment").val(wName);
            }
        }
    </script>
    <style type="text/css">
        p {
            text-indent: 2em;
            line-height: 0.5;
        }

        h4 {
            background: url(../images/JBQK.gif);
        }

        #Text1 {
            width: 350px;
        }

        #Txtxinxibianhao {
        }

        #Text2 {
            width: 175px;
        }

        #Select1 {
            width: 175px;
        }

        #Select2 {
            width: 175px;
        }

        #Select3 {
            width: 175px;
        }

        .style57 {
            width: 180px;
            height: 30px;
        }
        .auto-style2 {
            width: 477px;
            height: 30px;
        }

        .auto-style3 {
            text-align: right;
            font-style: normal;
            font-variant: normal;
            font-weight: normal;
            font-size: 14px;
            line-height: normal;
            font-family: 微软雅黑;
            height: 30px;
            background-color: #f6f6f6;
            width: 169px;
        }

        .auto-style8 {
            width: 342px;
            height: 30px;
        }

        .auto-style9 {
            text-align: right;
            font-style: normal;
            font-variant: normal;
            font-weight: normal;
            font-size: 14px;
            line-height: normal;
            font-family: 微软雅黑;
            height: 30px;
            background-color: #f6f6f6;
            width: 228px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="PrintHide" style="width: 100%" border="0" cellpadding="2" cellspacing="1">
                <tr>
                    <td valign="middle" style="border-bottom: #006633 1px ; height: 30px;">&nbsp;<img src="../images/BanKuaiJianTou.gif" />
                        <a class="hei" href="../Main/MyDesk.aspx">桌面</a>&nbsp;>>&nbsp;资质管理&nbsp;&gt;&gt;&nbsp;添加资质
                    </td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px ; height: 30px;">
                        <asp:HiddenField ID="HiddenField_ID" runat="server" />
                        <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/images/Button/Submit.jpg" OnClick="btnSubmit_Click" Style="height: 19px" />
                        &nbsp;&nbsp;
                    <img src="../images/Button/JianGe.jpg" />
                        <img class="HerCss" onclick="javascript:window.history.go(-1)" src="../images/Button/BtnExit.jpg" />&nbsp;
                    </td>
                </tr>
            </table>
            <table style="width: 100%" bgcolor="#999999" border="0" cellpadding="3" cellspacing="1">
                <tr>
                    <td class="TitleStyle" colspan="4">
                        <strong>
                            <asp:TextBox ID="txtWorkName" runat="server" Width="51px" Visible="False"></asp:TextBox>
                            添加资质</strong>

                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">资质单位：<a style="color: #FF0000; font-size: 18px;">*</a>&nbsp;&nbsp;
                    </td>
                    <td style="background-color: #ffffff; text-align: left;" class="auto-style8">
                        <asp:TextBox ID="txtDepartment" runat="server" Width="480px"></asp:TextBox>
                        <img class="HerCss" onclick="openselectDepartment(this);" src="../images/Button/search.gif" />
                    </td>

                </tr>
                <tr>
                    <td class="auto-style9">资质名称：&nbsp;&nbsp;<a style="color: #FF0000; font-size: 18px;">*</a>
                    </td>


                    <td style="background-color: #ffffff; text-align: left;" class="auto-style2">
                        <asp:TextBox ID="txtAptitudeName" runat="server" Width="480px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style9">启用：&nbsp;&nbsp;<a style="color: #FF0000; font-size: 18px;">*</a>
                    </td>


                    <td style="background-color: #ffffff; text-align: left;" class="auto-style2">
                        <asp:RadioButtonList runat="server" ID="rblActive" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="启用" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="关闭"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
