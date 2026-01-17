<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintWork.aspx.cs" Inherits="NWorkFlow_PrintWork" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" rel="stylesheet" />
    <script type="text/javascript" src="../JS/jquery-1.11.2.min.js"></script>
    <script type="text/javascript">
        function Load_Do() {
            //    for(var i=1;i<window.document.form1.elements.length;i++)
            //    {                
            //     
            //      var e = form1.elements[i];
            //      //获取当前元素的Name值(name)
            //      var namestr=e.name;
            //      //alert(namestr);
            //      e.readOnly = "true"; //设置所有文本框不可输入
            //      //e.className="PrintCSS";

            //    }
        }

        function selectUser(imgidstr) {

        }

        function selectBuMen(imgidstr) {

        }


        function selectyinzhang(imgidstr) {

        }
        function selectShouXie(imgidstr) {

        }

        function PrintTable() {
            document.getElementById("PrintHide").style.visibility = "hidden"
            print();
            document.getElementById("PrintHide").style.visibility = "visible"

        }

        function printpage() {
            var oldStr = window.document.body.innerHTML;
            var start = "<!--startprint-->";
            var end = "<!--endprint-->";
            var newStr = oldStr.substring(oldStr.indexOf(start) + 17);
            newStr = newStr.substring(0, newStr.indexOf(end));
            window.document.body.innerHTML = newStr;
            window.print();
            window.document.body.innerHTML = oldStr;
        }
    </script>
    <style>
        #Label3 input[id^='Text'], select {
            height: 22px;
            border: 0 !important;
        }

        #Label3 input[id^='Date'] {
            height: 22px;
            width: 120px;
            border: 0 !important;
        }

        #Label3 textarea {
            border: 0 !important;
        }

        .selectTdClass {
            background-color: #edf5fa !important
        }

        #Label3 table.noBorderTable td, table.noBorderTable th, table.noBorderTable caption {
            border: 1px dashed #ddd !important
        }

        #Label3 table {
            margin-bottom: 10px;
            border-collapse: collapse;
            display: table;
        }

        #Label3 td, th {
            background: white;
            padding: 5px 0px;
            border: 1px solid #DDD;
        }

        #Label3 caption {
            border: 1px dashed #DDD;
            border-bottom: 0;
            padding: 3px;
            text-align: center;
        }

        #Label3 th {
            border-top: 2px solid #BBB;
            background: #F7F7F7;
        }

        #Label3 td p {
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
                        <a class="hei" href="../Main/MyDesk.aspx">桌面</a>&nbsp;>>&nbsp;审批流程&nbsp;>>&nbsp;打印工作表单
                    </td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px;">
                        <img id="IMG1" class="HerCss" onclick="printpage()" src="../images/Button/BtnPrint.jpg" />&nbsp;
                    <img src="../images/Button/JianGe.jpg" />&nbsp;
                    <img class="HerCss" onclick="javascript:window.history.go(-1)" src="../images/Button/BtnExit.jpg" />&nbsp;</td>
                </tr>
            </table>

            <table style="width: 100%" border="0" cellpadding="2" cellspacing="1">
                <tr>
                    <td colspan="2" style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <!--startprint-->
                        <asp:Label ID="Label3" runat="server"></asp:Label>
                        <!--endprint-->
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
