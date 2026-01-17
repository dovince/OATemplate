<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectCostView.aspx.cs" Inherits="Financial_ProjectCostView" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>
        <%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" rel="stylesheet" />
    <link href="../Style/Style1.css" rel="stylesheet" />
    <link href="../CSS/cxcalendar.css" rel="stylesheet" />
    <link href="../CSS/default/easyui.css" rel="stylesheet" />
    <link href="../CSS/icon.css" rel="stylesheet" />
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
    <script type="text/javascript" src="../JS/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../CSS/calendar.js"></script>
    <script type="text/javascript" src="../JS/common.js?v=20240613"></script>
    <script src="../JS/jquery.easyui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".input_cxcalendar").each(function () {
                //debugger
                var a = new Calendar({
                    targetCls: $(this),
                    type: 'yyyy-mm-dd',
                    wday: 2
                }, function (val) {
                    //console.log(val);
                });
            });
        });
        function openbumenDialog() {
            var RadNum = Math.random();
            $("#bumen")[0].src += '&Radstr=' + RadNum;
            $('#win_bumen').css("visibility", "visible");
            $('#win_bumen').window('open');
        }
        function openselectDialog() {
            var RadNum = Math.random();
            $("#select")[0].src += '&Radstr=' + RadNum;
            $("#win_select").css("visibility", "visible");
            $('#win_select').window('open');
        }
        var usertypeid = "";
        function openuserDialog(utype) {
            //防止缓存之前的页面
            var RadNum = Math.random();
            $("#user")[0].src += '&Radstr=' + RadNum;
            $('#win_user').css("visibility", "visible");
            $('#win_user').window('open');
            if (utype == "fzr") {
                usertypeid = "txtXMFZR";
            }
            //else if (utype == "spr") {
            //    usertypeid = "TextBox5";
            //}
        }
        function PrintTable() {
            document.getElementById("PrintHide").style.visibility = "hidden"
            print();
            document.getElementById("PrintHide").style.visibility = "visible"
        }
        function selectUser(imgidstr) {
            var wName;
            var RadNum = Math.random();
            wName = window.showModalDialog('../Main/SelectUser.aspx?Radstr=' + RadNum, '', 'dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');
            if (wName == null || wName == "")
            { }
            else {
                imgidstr.value = wName;
            }
        }

        function selectBuMen(imgidstr) {
            var wName;
            var RadNum = Math.random();
            wName = window.showModalDialog('../Main/SelectDanWei.aspx?Radstr=' + RadNum, '', 'dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');
            if (wName == null || wName == "")
            { }
            else {
                imgidstr.value = wName;
            }
        }
        function selectyinzhang(imgidstr) {
            var wName;
            var RadNum = Math.random();
            wName = window.showModalDialog('../Main/SelectYinZhang.aspx?Radstr=' + RadNum, '', 'dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');
            if (wName == null || wName == "")
            { }
            else {
                imgidstr.src = "http://" + window.location.host + "<%=System.Configuration.ConfigurationManager.AppSettings["OARoot"] %>/UploadFile/" + wName;
            }
        }
        //        $(document).ready(function(){
        //            $('td:contains("项目编号")').addClass('highlight');
        //        });
        function selectShouXie(imgidstr)//手写
        {
            var wName;
            var RadNum = Math.random();
            wName = window.showModalDialog('../Main/InsertQianMing.aspx?Radstr=' + RadNum, '', 'dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');
            if (wName == null || wName == "")
            { }
            else {
                imgidstr.src = "http://" + window.location.host + "<%=System.Configuration.ConfigurationManager.AppSettings["OARoot"] %>/UploadFile/" + wName;
            }
        }

    </script>
    <style>
        h1, h2, h3 {
            font: bold 36px/1 "\5fae\8f6f\96c5\9ed1";
        }

        h2 {
            font-size: 20px;
        }

        h3 {
            font-size: 16px;
        }

        fieldset {
            margin: 1em 0;
        }

            fieldset legend {
                font: bold 14px/2 "\5fae\8f6f\96c5\9ed1";
            }

        a {
            color: #06f;
            text-decoration: none;
        }

            a:hover {
                color: #00f;
            }

        .wrap {
            width: 600px;
            margin: 0 auto;
            padding: 20px 40px;
            border: 2px solid #999;
            border-radius: 8px;
            background: #fff;
            box-shadow: 0 0 10px rgba(0,0,0,0.5);
        }
    </style>
    <style type="text/css">
        #Checkbox2 {
            width: 62px;
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

        .style22 {
            width: 117px;
            text-align: right;
            height: 25px;
        }

        .style24 {
            height: 25px;
        }

        .highlight {
            font-weight: bold;
            color: Red;
        }

        .style25 {
            height: 25px;
        }

        .style26 {
            width: 107px;
            text-align: right;
            height: 80px;
        }

        .style27 {
            height: 65px;
            width: 800px;
        }

        .style36 {
            height: 25px;
            width: 117px;
        }

        .style51 {
            text-align: center;
            height: 25px;
        }

        .style52 {
            height: 25px;
            width: 107px;
        }

        .style53 {
            width: 107px;
            text-align: right;
            height: 25px;
        }

        .style57 {
            height: 25px;
            width: 420px;
        }

        .style58 {
            width: 117px;
        }

        .style59 {
            width: 381px;
        }

        .auto-style5 {
            height: 25px;
        }

        .auto-style7 {
            font-size: large;
        }
    </style>
</head>
<body onload="Load_Do();">
    <form id="form1" runat="server">
        <div>
            <table class="tb_normal" style="width: 100%">
                <tr>
                    <td valign="middle" style="border-bottom: #006633 1px ; height: 30px;">&nbsp;<img src="../images/BanKuaiJianTou.gif" />
                        <a class="hei" href="../Main/MyDesk.aspx">桌面</a>&nbsp;&gt;&gt;&nbsp;财务管理&nbsp;&gt;&gt; 项目成本核算信息查看</td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px ; height: 30px;">
                        <asp:HiddenField ID="XMJBXXID" runat="server" />
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/Button/Submit.jpg"
                            OnClick="ImageButton1_Click" Style="height: 19px" Visible="False" />
                        <img src="../images/Button/JianGe.jpg" />&nbsp;<asp:ImageButton ID="ImageButton2" ImageUrl="~/images/Button/BtnExit.jpg" runat="server" OnClick="ImageButton2_Click" />
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td height="3px" colspan="2" style="background-color: #ffffff"></td>
                </tr>
            </table>
            <table class="tb_normal" style="width: 100%">
                <tr>
                    <td class="TitleStyle" colspan="6">
                        <strong><span class="auto-style7">项目成本核算信息</span></strong></td>
                </tr>
                <tr>
                    <td class="MainStyle">项目编号：</td>
                    <td class="auto-style5" style="background-color: #ffffff">
                        <asp:TextBox ID="txtXMID" runat="server" Width="320px"
                            onkeypress="event.returnValue=false;" Enabled="False" AutoPostBack="True" ToolTip="如果找不到项目编号，请先在项目管理中登记项目基本信息" Wrap="False" ReadOnly="True"></asp:TextBox>
                        <br />
                    </td>
                    <td class="MainStyle">项目名称：</td>
                    <td class="style24" style="background-color: #ffffff">
                        <asp:TextBox ID="txtXMName" runat="server" Width="320px" Enabled="False" Height="59px" TextMode="MultiLine"></asp:TextBox>

                    </td>
                    <td class="MainStyle">合同编号：</td>
                    <td class="style24" style="background-color: #ffffff">
                        <asp:TextBox ID="txtHTBH" runat="server" Width="200px" ToolTip="如果没有合同编号可以不填，在合同签订评审时会自动关联" Wrap="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="MainStyle">专业类别：</td>
                    <td class="auto-style5" style="background-color: #ffffff">
                        <asp:TextBox ID="txtZYLB" runat="server" Width="320px"></asp:TextBox>

                    </td>
                    <td class="MainStyle">承接部门：</td>
                    <td class="style24" style="background-color: #ffffff">
                        <asp:TextBox ID="txtXMbumen" runat="server" Width="320px"></asp:TextBox>

                    </td>
                    <td class="MainStyle">项目负责人：</td>
                    <td class="style24" style="background-color: #ffffff">
                        <asp:TextBox ID="txtXMFZR" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="MainStyle">项目周期：</td>
                    <td class="auto-style5" style="background-color: #ffffff">
                        <asp:TextBox ID="txtXMstarttime" runat="server" Width="120px" class="input_cxcalendar"></asp:TextBox>
                        &nbsp; 至&nbsp;
                    <asp:TextBox ID="txtXMendtime" runat="server" Width="120px" class="input_cxcalendar"></asp:TextBox>
                    </td>
                    <td class="MainStyle">项目状态：</td>
                    <td class="style24" style="background-color: #ffffff">
                        <asp:TextBox ID="txtXMState" runat="server" Width="320px"></asp:TextBox>
                    </td>
                    <td class="MainStyle">&nbsp;</td>
                    <td class="style24" style="background-color: #ffffff">&nbsp;</td>
                </tr>
                <tr>
                    <td class="MainStyle">合同金额：</td>
                    <td class="auto-style5" style="background-color: #ffffff">
                        <asp:TextBox ID="txtXMJingFei" onkeypress="if((event.keyCode<48||event.keyCode>57)&&event.keyCode!=45&&event.keyCode!=46)event.returnValue=false;"
                            onpaste="return false;" Style="ime-mode: disabled;" runat="server" Width="320px"></asp:TextBox>&nbsp;元
                    </td>
                    <td class="MainStyle">结算金额：</td>
                    <td class="style24" style="background-color: #ffffff">
                        <asp:TextBox ID="txt结算金额" onkeypress="if((event.keyCode<48||event.keyCode>57)&&event.keyCode!=45&&event.keyCode!=46)event.returnValue=false;"
                            onpaste="return false;" runat="server" Width="250px"></asp:TextBox>
                        &nbsp;元</td>
                    <td class="MainStyle">成本支出合计：</td>
                    <td class="style24" style="background-color: #ffffff">
                        <asp:TextBox ID="txt成本支出合计" Enabled="false" runat="server" Width="200px"></asp:TextBox>
                        &nbsp;元</td>
                </tr>
                <%-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
                <tr>
                    <td class="TitleStyle" colspan="6">
                        <strong><span class="auto-style7">项目成本预算明细</span></strong></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <iframe id="budgetFrame" style="margin-top: -70px; width: 100%; height: 300px;"></iframe>
                    </td>

                </tr>
                <%-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
                <tr>
                    <td class="TitleStyle" colspan="6">
                        <strong><span class="auto-style7">项目成本支出明细</span></strong></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <iframe id="infoFrame" style="margin-top: -70px; width: 100%; height: 400px;"></iframe>
                    </td>

                </tr>
            </table>
        </div>
        <script>
            //批量设置字段的可写与保密属性
           <%=PiLiangSet %>
</script>
        <script>
            function Load_Do() {
                //            setTimeout("Load_Do()", 0);
                //		        var content = document.getElementById("Label1").innerHTML
                //document.getElementById("TextBox3").value = content;
            }
            function btnyujitoubiaotime_onclick() {

            }

        </script>
    </form>
    <div id="win_bumen" class="easyui-window" data-options="title:'选择部门',iconCls:'icon-search',closed:true,closable:false,minimizable:false,maximizable:false,collapsible:false,resizable:true" style="width: 350px; height: 400px; visibility: hidden; padding: 5px;">
        <iframe id="bumen" scrolling="yes" frameborder="0" src="../Main/SelectDanWei.aspx?TableName=ERPUser&LieName=UserName" style="width: 100%; height: 100%;"></iframe>
    </div>
    <div id="win_user" class="easyui-window" data-options="title:'选择用户',iconCls:'icon-search',closed:true,closable:false,minimizable:false,maximizable:false,collapsible:false,resizable:true" style="width: 350px; height: 400px; visibility: hidden; padding: 5px;">
        <iframe id="user" scrolling="yes" frameborder="0" src="../Main/SelectUser.aspx?TableName=ERPUser&LieName=UserName" style="width: 100%; height: 100%;"></iframe>
    </div>
    <div id="win_select" class="easyui-window" data-options="title:'选择项目',iconCls:'icon-search',closed:true,closable:false,minimizable:false,maximizable:false,collapsible:false,resizable:true" style="width: 400px; height: 450px; visibility: hidden; padding: 5px;">
        <iframe id="select" scrolling="yes" frameborder="0" src="../BusinessManage/CommonSelect.aspx?TypeStr=XMBH" style="width: 100%; height: 100%;"></iframe>
    </div>
</body>
</html>




