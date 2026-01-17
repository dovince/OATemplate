<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectBudgetView.aspx.cs" Inherits="Financial_ProjectBudgetView" %>

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
    <script src="../JS/jquery.easyui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //$("input[id $=_备注]").hide();
            $("span[id ^= Label_]").click(function () {
                if ($(this).next().is(':hidden')) {
                    $(this).next().show();
                } else {
                    $(this).next().hide();
                }
            });
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
            $('#win_bumen').window({
                onBeforeClose: function () {
                    var returnVal = "";
                    for (var i = 0; i < window.length; i++) {
                        if (window[i].frameElement.id == "bumen") {
                            //根据弹出窗内部的ifame的id来定位
                            returnVal = window[i].returnValue;
                        }
                    }
                    $('#txtXMbumen')[0].value = returnVal;
                }
            });
            $('#win_select').window({
                onBeforeClose: function () {
                    var returnVal = "";
                    for (var i = 0; i < window.length; i++) {
                        if (window[i].frameElement.id == "select") {
                            //根据弹出窗内部的ifame的id来定位
                            returnVal = window[i].returnValue;
                            document.getElementById('txt合同编号').value = returnVal;
                            //document.getElementById('btnaddsomething').click();
                        }
                    }
                    $('#txt合同编号')[0].value = returnVal;
                    //document.getElementById('btnaddsomething').click();
                }
            });
            $('#win_user').window({
                onBeforeClose: function () {
                    var returnVal = "";
                    for (var i = 0; i < window.length; i++) {
                        if (window[i].frameElement.id == "user") {
                            //根据弹出窗内部的ifame的id来定位
                            returnVal = window[i].returnValue;
                        }
                    }
                    //$('#txtdengjiren')[0].value = returnVal;
                    if (usertypeid != "") {
                        var idstr = "#" + usertypeid;
                        $(idstr)[0].value = returnVal;
                    }
                }
            });

            $(".BudgetControl").bind("keyup", function () {
                var p = $(this);
                var costname = p.attr("id").replace("txt", "");
                var val = p.val();
                var projectno = $("#txtXMID").val();
                var htbh = $("#txtHTID").val();
                $.ajax({
                    cache: false,
                    type: "get",
                    data: { flag: "CheckBudgetControl", costname: escape(costname), val: val, projectno: projectno, htbh: htbh },
                    dataType: "json",
                    url: "../Main/GetJsonResultHandler.ashx",
                    success: function (data) {
                        if (data != null && !data.Code) {
                            alert(data.Message);
                        }
                    }
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
            else if (utype == "spr") {
                usertypeid = "TextBox5";
            }
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
            if (wName == null || wName == "") { }
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
            width: 623px;
        }

        .auto-style7 {
            font-size: large;
        }

        .auto-style110 {
            height: 25px;
        }

        .auto-style114 {
            text-align: left;
            font: 18px "仿宋";
            background-color: #f6f6f6;
            height: 30px;
            width: 141px;
        }

        .auto-style115 {
            text-align: left;
            font: 18px "仿宋";
            background-color: #f6f6f6;
            height: 30px;
            width: 156px;
        }

        .TitleSpan {
            text-align: left;
            font: 18px "仿宋";
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="tb_normal" style="width: 100%">
                <tr>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px;">&nbsp;<img src="../images/BanKuaiJianTou.gif" />
                        <a class="hei" href="../Main/MyDesk.aspx">桌面</a>&nbsp;>>&nbsp;财务管理&nbsp;&gt;&gt; 项目预算信息</td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px;">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <%--<asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/images/Button/Submit.jpg"
                            OnClick="btnSubmit_Click" Style="height: 19px" />--%>
                        <%--<img src="../images/Button/JianGe.jpg" />&nbsp;--%>
                        <img class="HerCss" onclick="javascript:window.history.go(-1)" src="../images/Button/BtnExit.jpg" />&nbsp;
                    </td>
                </tr>
                <tr>
                    <td height="3px" colspan="2" style="background-color: #ffffff"></td>
                </tr>
            </table>
            <table class="tb_normal" style="width: 100%">
                <tr>
                    <td class="TitleStyle" colspan="4">
                        <strong><span class="auto-style7">项目信息</span></strong></td>
                </tr>
                <tr>
                    <td class="MainStyle">项目名称：</td>
                    <td class="style24" style="background-color: #ffffff">
                        <asp:TextBox ID="txtXMName" runat="server" Width="400px" Enabled="False" Height="35px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td class="auto-style115">合同编号：</td>
                    <td class="auto-style5" style="background-color: #ffffff">
                        <asp:TextBox ID="txtHTID" runat="server" Width="200px" onkeypress="event.returnValue=false;" Enabled="False" AutoPostBack="True" Wrap="False" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="MainStyle">项目编号：</td>
                    <td class="style24" style="background-color: #ffffff">
                        <asp:TextBox ID="txtXMID" runat="server" Width="200px" onkeypress="event.returnValue=false;" Enabled="False" AutoPostBack="True" Wrap="False" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td class="auto-style115">专业类别：</td>
                    <td class="auto-style5" style="background-color: #ffffff">
                        <asp:TextBox ID="txtZYTYPE" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="MainStyle">经办部门：</td>
                    <td style="background-color: #ffffff">
                        <asp:TextBox ID="txtDepartment" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td class="MainStyle">经营方式：</td>
                    <td style="background-color: #ffffff">
                        <asp:TextBox ID="txtBizpat" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="MainStyle">合同金额：</td>
                    <td style="background-color: #ffffff">
                        <asp:TextBox ID="txtContractAmt" runat="server" Enabled="false" Style="text-align: right"></asp:TextBox>元
                    </td>
                    <td class="auto-style115">摘要：</td>
                    <td class="auto-style5" style="background-color: #ffffff">
                        <asp:TextBox ID="txtComment" runat="server" Width="300px" TextMode="MultiLine" Height="40px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TitleStyle" colspan="4">
                        <strong><span class="auto-style7">成本预算信息</span></strong></td>
                </tr>
                <tr>
                    <td class="auto-style115">1.工资及津贴：</td>
                    <td class="auto-style5" style="background-color: #ffffff">
                        <asp:TextBox ID="txt工资及津贴" runat="server" Width="150px" Enabled="false" CssClass="BudgetControl" Style="text-align: right; ime-mode: disabled;" onkeypress="if((event.keyCode<48||event.keyCode>57)&&event.keyCode!=45&&event.keyCode!=46)event.returnValue=false;" ToolTip=""></asp:TextBox>
                        &nbsp;<strong>元</strong>&nbsp; 
                                <span class="TitleSpan">预算比例:</span><asp:Label ID="lbl工资及津贴" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style115">2.工程出包费：</td>
                    <td class="auto-style5" style="background-color: #ffffff">
                        <asp:TextBox ID="txt工程出包费" runat="server" Width="150px" Enabled="false" CssClass="BudgetControl" Style="text-align: right; ime-mode: disabled;" onkeypress="if((event.keyCode<48||event.keyCode>57)&&event.keyCode!=45&&event.keyCode!=46)event.returnValue=false;" ToolTip=""></asp:TextBox>
                        &nbsp;<strong>元</strong>&nbsp;
                                <span class="TitleSpan">预算比例:</span><asp:Label ID="lbl工程出包费" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style114">3.材料费：</td>
                    <td class="auto-style110" style="background-color: #ffffff" colspan="">
                        <asp:TextBox ID="txt材料费" runat="server" CssClass="BudgetControl" Enabled="false" Width="150px" Style="text-align: right; ime-mode: disabled;" onkeypress="if((event.keyCode<48||event.keyCode>57)&&event.keyCode!=45&&event.keyCode!=46)event.returnValue=false;" ToolTip=""></asp:TextBox>
                        &nbsp;<strong>元 </strong>
                        <span class="TitleSpan">预算比例:</span><asp:Label ID="lbl材料费" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style114">4.租赁费：</td>
                    <td class="auto-style110" style="background-color: #ffffff">
                        <asp:TextBox ID="txt租赁费" runat="server" CssClass="BudgetControl" Enabled="false" Width="150px" Style="text-align: right; ime-mode: disabled;" onkeypress="if((event.keyCode<48||event.keyCode>57)&&event.keyCode!=45&&event.keyCode!=46)event.returnValue=false;" ToolTip=""></asp:TextBox>
                        &nbsp;<strong>元</strong>&nbsp; 
                                <span class="TitleSpan">预算比例:</span><asp:Label ID="lbl租赁费" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style115">5.劳务费：</td>
                    <td class="auto-style5" style="background-color: #ffffff">
                        <asp:TextBox ID="txt劳务费" runat="server" CssClass="BudgetControl" Enabled="false" Width="150px" Style="text-align: right; ime-mode: disabled;" onkeypress="if((event.keyCode<48||event.keyCode>57)&&event.keyCode!=45&&event.keyCode!=46)event.returnValue=false;" ToolTip=""></asp:TextBox>
                        &nbsp;<strong>元</strong>&nbsp; 
                                <span class="TitleSpan">预算比例:</span><asp:Label ID="lbl劳务费" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style114">6.安全生产费用:</td>
                    <td class="auto-style110" style="background-color: #ffffff">
                        <asp:TextBox ID="txt安全生产费用" runat="server" CssClass="BudgetControl" Enabled="false" Width="150px" Style="text-align: right; ime-mode: disabled;" onkeypress="if((event.keyCode<48||event.keyCode>57)&&event.keyCode!=45&&event.keyCode!=46)event.returnValue=false;" ToolTip=""></asp:TextBox>
                        &nbsp;<strong>元 </strong>&nbsp;
                                <span class="TitleSpan">预算比例:</span><asp:Label ID="lbl安全生产费用" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style115">7.办公费：</td>
                    <td class="auto-style5" style="background-color: #ffffff">
                        <asp:TextBox ID="txt办公费" runat="server" Width="150px" CssClass="BudgetControl" Enabled="false" Style="text-align: right; ime-mode: disabled;" onkeypress="if((event.keyCode<48||event.keyCode>57)&&event.keyCode!=45&&event.keyCode!=46)event.returnValue=false;" ToolTip=""></asp:TextBox>
                        &nbsp;<strong>元</strong>&nbsp;
                                <span class="TitleSpan">预算比例:</span><asp:Label ID="lbl办公费" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style115">8.维修费用：</td>
                    <td class="auto-style5" style="background-color: #ffffff">
                        <asp:TextBox ID="txt维修费用" runat="server" Width="150px" CssClass="BudgetControl" Enabled="false" Style="text-align: right; ime-mode: disabled;" onkeypress="if((event.keyCode<48||event.keyCode>57)&&event.keyCode!=45&&event.keyCode!=46)event.returnValue=false;" ToolTip=""></asp:TextBox>
                        &nbsp;<strong>元</strong>&nbsp;
                                <span class="TitleSpan">预算比例:</span><asp:Label ID="lbl维修费用" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style115">9.交通运输费用：</td>
                    <td class="auto-style5" style="background-color: #ffffff">
                        <asp:TextBox ID="txt交通运输费用" runat="server" Width="150px" CssClass="BudgetControl" Enabled="false" Style="text-align: right; ime-mode: disabled;" onkeypress="if((event.keyCode<48||event.keyCode>57)&&event.keyCode!=45&&event.keyCode!=46)event.returnValue=false;" ToolTip=""></asp:TextBox>
                        &nbsp;<strong>元 </strong>&nbsp;
                                <span class="TitleSpan">预算比例:</span><asp:Label ID="lbl交通运输费用" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style114">10.差旅费：</td>
                    <td class="auto-style110" style="background-color: #ffffff">
                        <asp:TextBox ID="txt差旅费" runat="server" Width="150px" CssClass="BudgetControl" Enabled="false" Style="text-align: right; ime-mode: disabled;" onkeypress="if((event.keyCode<48||event.keyCode>57)&&event.keyCode!=45&&event.keyCode!=46)event.returnValue=false;" ToolTip=""></asp:TextBox>
                        &nbsp;<strong>元 </strong>&nbsp;
                                <span class="TitleSpan">预算比例:</span><asp:Label ID="lbl差旅费" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style114">11.邮电费用：</td>
                    <td class="auto-style110" style="background-color: #ffffff">
                        <asp:TextBox ID="txt邮电费用" runat="server" Width="150px" CssClass="BudgetControl" Style="text-align: right; ime-mode: disabled;" onkeypress="if((event.keyCode<48||event.keyCode>57)&&event.keyCode!=45&&event.keyCode!=46)event.returnValue=false;" ToolTip=""></asp:TextBox>
                        &nbsp;<strong>元 </strong>&nbsp;
                        <span class="TitleSpan">预算比例:</span><asp:Label ID="lbl邮电费用" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style114">12.水电费：</td>
                    <td class="auto-style110" style="background-color: #ffffff">
                        <asp:TextBox ID="txt水电费" runat="server" Width="150px" CssClass="BudgetControl" Style="text-align: right; ime-mode: disabled;" onkeypress="if((event.keyCode<48||event.keyCode>57)&&event.keyCode!=45&&event.keyCode!=46)event.returnValue=false;" ToolTip=""></asp:TextBox>
                        &nbsp;<strong>元 </strong>&nbsp;
                        <span class="TitleSpan">预算比例:</span><asp:Label ID="lbl水电费" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style114">13.会议费：</td>
                    <td class="auto-style110" style="background-color: #ffffff">
                        <asp:TextBox ID="txt会议费" runat="server" Width="150px" CssClass="BudgetControl" Style="text-align: right; ime-mode: disabled;" onkeypress="if((event.keyCode<48||event.keyCode>57)&&event.keyCode!=45&&event.keyCode!=46)event.returnValue=false;" ToolTip=""></asp:TextBox>
                        &nbsp;<strong>元 </strong>&nbsp;
                        <span class="TitleSpan">预算比例:</span><asp:Label ID="lbl会议费" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style114">14.印刷费：</td>
                    <td class="auto-style110" style="background-color: #ffffff">
                        <asp:TextBox ID="txt印刷费" runat="server" Width="150px" CssClass="BudgetControl" Style="text-align: right; ime-mode: disabled;" onkeypress="if((event.keyCode<48||event.keyCode>57)&&event.keyCode!=45&&event.keyCode!=46)event.returnValue=false;" ToolTip=""></asp:TextBox>
                        &nbsp;<strong>元 </strong>&nbsp;
                        <span class="TitleSpan">预算比例:</span><asp:Label ID="lbl印刷费" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style114">15.其它费用：</td>
                    <td class="auto-style110" style="background-color: #ffffff" colspan="3">
                        <asp:TextBox ID="txt其它费用" runat="server" Width="150px" CssClass="BudgetControl" Style="text-align: right; ime-mode: disabled;" onkeypress="if((event.keyCode<48||event.keyCode>57)&&event.keyCode!=45&&event.keyCode!=46)event.returnValue=false;" ToolTip=""></asp:TextBox>
                        &nbsp;<strong>元 </strong>&nbsp;
                        <span class="TitleSpan">预算比例:</span><asp:Label ID="lbl其它费用" runat="server"></asp:Label>
                    </td>
                </tr>

            </table>
        </div>
        <script type="text/javascript">
            function Load_Do() {

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
        <iframe id="select" scrolling="yes" frameborder="0" src="CommonSelect.aspx?TypeStr=GCKJS" style="width: 100%; height: 100%;"></iframe>
    </div>
</body>
</html>

