<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NWorkToDoView.aspx.cs" Inherits="NWorkFlow_NWorkToDoView" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" type="text/css" rel="stylesheet">
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
    <script type="text/javascript" src="../JS/calendar.js"></script>

    <script type="text/javascript">
        function PrintTable() {
            document.getElementById("PrintHide").style.visibility = "hidden"
            print();
            document.getElementById("PrintHide").style.visibility = "visible"
        }

        function selectUser(imgidstr) {
            var wName;
            var RadNum = Math.random();
            wName = window.showModalDialog('../Main/SelectUser.aspx?Radstr=' + RadNum, '', 'dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');
            if (wName == null || wName == "") { }
            else {
                imgidstr.value = wName;
            }
        }

        function selectBuMen(imgidstr) {
            var wName;
            var RadNum = Math.random();
            wName = window.showModalDialog('../Main/SelectDanWei.aspx?Radstr=' + RadNum, '', 'dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');
            if (wName == null || wName == "") { }
            else {
                imgidstr.value = wName;
            }
        }


        function selectyinzhang(imgidstr) {
            var wName;
            var RadNum = Math.random();
            wName = window.showModalDialog('../Main/SelectYinZhang.aspx?Radstr=' + RadNum, '', 'dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');
            if (wName == null || wName == "") { }
            else {
                imgidstr.src = "http://" + window.location.host +"<%=System.Configuration.ConfigurationManager.AppSettings["OARoot"] %>/UploadFile/" + wName;
            }
        }
        function selectShouXie(imgidstr) {
            var wName;
            var RadNum = Math.random();
            wName = window.showModalDialog('../Main/InsertQianMing.aspx?Radstr=' + RadNum, '', 'dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');
            if (wName == null || wName == "") { }
            else {
                imgidstr.src = "http://" + window.location.host +"<%=System.Configuration.ConfigurationManager.AppSettings["OARoot"] %>/UploadFile/" + wName;
            }
        }
    </script>
    <style type="text/css">
        #lblFormContent input[id^='Text'],select {
            height: 22px;
            width: 99%;
            border:0 !important;
        }
        #lblFormContent input[id^='Date'] {
            height: 22px;
            width: 120px;
            border:0 !important;
        }
        #lblFormContent input[id^='Date'] {
            height: 22px;
            width: 120px;
            border:0 !important;
        }
        .selectTdClass {
            background-color: #edf5fa !important
        }

        #lblFormContent table.noBorderTable td, table.noBorderTable th, table.noBorderTable caption {
            border: 1px dashed #ddd !important
        }

        #lblFormContent table {
            margin-bottom: 10px;
            border-collapse: collapse;
            display: table;
        }

        #lblFormContent td, th {
            background: white;
            padding: 5px 10px;
            border: 1px solid #DDD;
        }

        #lblFormContent caption {
            border: 1px dashed #DDD;
            border-bottom: 0;
            padding: 3px;
            text-align: center;
        }

        #lblFormContent th {
            border-top: 2px solid #BBB;
            background: #F7F7F7;
        }

        #lblFormContent td p {
            margin: 0;
            padding: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="PrintHide" style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px;">&nbsp;<img src="../images/BanKuaiJianTou.gif" />
                        <a class="hei" href="../Main/MyDesk.aspx">桌面</a>&nbsp;>>&nbsp;工作管理&nbsp;>>&nbsp;查看信息
                    </td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px;">&nbsp;<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../images/Button/BtnPrint.jpg"
                        OnClick="ImageButton1_Click" />
                        <img src="../images/Button/JianGe.jpg" />&nbsp;
                    <img class="HerCss" onclick="javascript:window.history.go(-1)" src="../images/Button/BtnExit.jpg" />&nbsp;
                    </td>
                </tr>
                <tr>
                    <td height="3px" colspan="2" style="background-color: #ffffff"></td>
                </tr>
            </table>
            <table style="width: 100%" class="tb_normal">
                <tr>
                    <td style="width: 170px; height: 25px; background-color: #f6f6f6" align="right">工作名称：
                    </td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:Label ID="lblWorkName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px; height: 25px; background-color: #f6f6f6" align="right">发起人：
                    </td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:Label ID="lblUserName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px; height: 25px; background-color: #f6f6f6" align="right">发起时间：
                    </td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:Label ID="lblTimeStr" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px; height: 25px; background-color: #f6f6f6" align="right">表单内容：
                    </td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:Label ID="lblFormContent" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px; height: 25px; background-color: #f6f6f6" align="right">附件文件：
                    </td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:Label ID="lblFuJianList" runat="server"></asp:Label>
                    </td>
                </tr>
                <%if (!string.IsNullOrEmpty(showProcessing))%>
                <%{ %>
                <tr>
                    <td align="right" style="width: 170px; height: 25px; background-color: #f6f6f6">审批记录：</td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <div id="recordss">
                            <div class="stitle zhu" onclick="showDoLog()" style="cursor: pointer">
                                <asp:Label runat="server" ID="lblDoTitle"></asp:Label>
                            </div>
                            <div id="showrecord0" style="display: block;">
                                <table border="0" class="tabled2" style="border-collapse: collapse;" width="100%" cellspacing="0" cellpadding="0">
                                    <thead>
                                        <tr>
                                            <td>序号</td>
                                            <td>节点名称</td>
                                            <td>审批人</td>
                                            <td>审批状态</td>
                                            <td>审批时间</td>
                                            <td>审批意见</td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%=showProcessing %>
                                    </tbody>
                                </table>
                            </div>
                            <div class="blank10"></div>
                        </div>
                    </td>
                </tr>
                <%} %>
                <tr>
                    <td style="width: 170px; height: 25px; background-color: #f6f6f6" align="right">签注审批：
                    </td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:Label ID="lblShenPiYiJian" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px; height: 25px; background-color: #f6f6f6" align="right">当前节点名称：
                    </td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:Label ID="lblJieDianName" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 170px; height: 25px; background-color: #f6f6f6" align="right">当前审批用户：
                    </td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:Label ID="lblShenPiUserList" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px; height: 25px; background-color: #f6f6f6" align="right">当前已审批用户：
                    </td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:Label ID="lblOKUserList" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px; height: 25px; background-color: #f6f6f6" align="right">当前状态：
                    </td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:Label ID="lblStateNow" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px; height: 25px; background-color: #f6f6f6" align="right">超时时间：
                    </td>
                    <td style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:Label ID="lblLateTime" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <script>
		<%=PiLiangSet %>
        </script>
    </form>
</body>
</html>
