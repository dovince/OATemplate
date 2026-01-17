<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostDetailView.aspx.cs" Inherits="Financial_CostDetailView" %>

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
    <script type="text/javascript" src="../JS/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../CSS/calendar.js"></script>
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
            if (wName == null || wName == "")
            { }
            else
            {
                imgidstr.value = wName;
            }
        }

        function selectBuMen(imgidstr) {
            var wName;
            var RadNum = Math.random();
            wName = window.showModalDialog('../Main/SelectDanWei.aspx?Radstr=' + RadNum, '', 'dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');
            if (wName == null || wName == "")
            { }
            else
            {
                imgidstr.value = wName;
            }
        }
        function selectyinzhang(imgidstr) {
            var wName;
            var RadNum = Math.random();
            wName = window.showModalDialog('../Main/SelectYinZhang.aspx?Radstr=' + RadNum, '', 'dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');
            if (wName == null || wName == "")
            { }
            else
            {
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
        else
        {
            imgidstr.src = "http://" + window.location.host + "<%=System.Configuration.ConfigurationManager.AppSettings["OARoot"] %>/UploadFile/" + wName;
        }
    }

    </script>
     <style>
        h1, h2, h3
        {
            font: bold 36px/1 "\5fae\8f6f\96c5\9ed1";
        }

        h2
        {
            font-size: 20px;
        }

        h3
        {
            font-size: 16px;
        }

        fieldset
        {
            margin: 1em 0;
        }

            fieldset legend
            {
                font: bold 14px/2 "\5fae\8f6f\96c5\9ed1";
            }

        a
        {
            color: #06f;
            text-decoration: none;
        }

            a:hover
            {
                color: #00f;
            }

        .wrap
        {
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
        #Checkbox2
        {
            width: 62px;
        }
        #Text1
        {
            width: 350px;
        }
        #Txtxinxibianhao
        {
        }
        #Text2
        {
            width: 175px;
        }
        #Select1
        {
            width: 175px;
        }
        #Select2
        {
            width: 175px;
        }
        #Select3
        {
            width: 175px;
        }
        .style22
        {
            width: 117px;
            text-align: right;
            height: 25px;
        }
        .style24
        {
            height: 25px;
        }
        .highlight
        {
            font-weight: bold;
            color: Red;
        }
        .style25
        {
            height: 25px;
        }
        .style26
        {
            width: 107px;
            text-align: right;
            height: 80px;
        }
        .style27
        {
            height: 65px;
            width: 800px;
        }
        .style36
        {
            height: 25px;
            width: 117px;
        }
        .style51
        {
            text-align: center;
            height: 25px;
        }
        .style52
        {
            height: 25px;
            width: 107px;
        }
        .style53
        {
            width: 107px;
            text-align: right;
            height: 25px;
        }
        .style57
        {
            height: 25px;
            width: 420px;
        }
        .style58
        {
            width: 117px;
        }
        .style59
        {
            width: 381px;
        }
        .MainStyle
        {
                       text-align: left;

            font:18px "仿宋";
            background-color:#D6E2F3;
            height:30px;
        }
        .auto-style5
        {
            height: 25px;
            width: 393px;
        }
        .auto-style7 {
            font-size: large;
        }
        .auto-style90 {
            height: 32px;
        }
        .auto-style91 {
            text-align: right;
            font: 18px "仿宋";
            background-color: #f6f6f6;
            height: 32px;
        }
         .auto-style97 {
            text-align: left;
            font: 18px "仿宋";
            background-color: #f6f6f6;
            height: 30px;
            width: 148px;
        }
        .auto-style105 {
            text-align: left;
            font: 18px "仿宋";
            background-color: #f6f6f6;
            height: 30px;
            width: 160px;
        }
        .auto-style106 {
            height: 25px;
            width: 373px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="PrintHide" style="width: 100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="middle" style="border-bottom: #006633 1px ; height: 30px;">
                    &nbsp;<img src="../images/BanKuaiJianTou.gif" />
                    <a class="hei" href="../Main/MyDesk.aspx">桌面</a>&nbsp;>>&nbsp;财务管理&nbsp;&gt;&gt; 项目支出信息查看</td>
                <td align="right" valign="middle" style="border-bottom: #006633 1px ; height: 30px;">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                   
                    <img src="../images/Button/JianGe.jpg" />&nbsp;
                    <asp:ImageButton ID="ImageButton_goback" ImageUrl="~/images/Button/BtnExit.jpg" runat="server" OnClick="ImageButton_goback_Click" />
                       
                
                </td>
            </tr>
            <tr>
                <td height="3px" colspan="2" style="background-color: #ffffff">
                </td>
            </tr>
        </table>
        <table style="width: 100%" bgcolor="#999999" border="0" cellpadding="2" cellspacing="1">
            <tr>
                <td class="TitleStyle" colspan="7">
                    <strong><span class="auto-style7">项目信息</span></strong></td>
            </tr>
             <tr>
                <td class="auto-style91" colspan="1">
                    支出期数：</td>
                 <td class="auto-style90" style="background-color: #ffffff" colspan="6" >
                     <strong>第 </strong> <asp:TextBox ID="txt期数" runat="server" Width="25px"></asp:TextBox><strong>&nbsp;期</strong></td>
            </tr>
             <tr>
                <td class="MainStyle" >
                    项目编号：</td>
                <td class="auto-style5" style="background-color: #ffffff">
                              
                            <asp:TextBox ID="txtXMBH" runat="server"></asp:TextBox>
                              
                            </td>
                <td class="auto-style105" >
                    项目名称：</td>
                <td class="style24" style="background-color: #ffffff" colspan="4">
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                <asp:TextBox ID="txtXMName" runat="server" Width="823px" Enabled="False" Height="35px" TextMode="MultiLine"></asp:TextBox>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                                        


                    </td>
            </tr>
               <tr>
                <td class="TitleStyle" colspan="7">
                    <strong><span class="auto-style7">成本支出信息</span></strong></td>
            </tr>
              <tr>
                <td class="auto-style97" >
                    1.工资及津贴：</td>
                <td class="auto-style5" style="background-color: #ffffff">
                        <asp:TextBox ID="txt工资及津贴" runat="server" Width="150px" ToolTip="以上季度合同登记总额为标准分配"></asp:TextBox>
                        &nbsp;<strong>元&nbsp; 备注：<asp:TextBox ID="txt工资及津贴_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
                  <td class="auto-style105" >
                      2.节日补贴：</td>
                <td class="auto-style106" style="background-color: #ffffff">
                        <asp:TextBox ID="txt节日补贴" runat="server" Width="141px" ToolTip="以上季度合同登记总额为标准分配"></asp:TextBox>
                        &nbsp;<strong>元 备注：<asp:TextBox ID="txt节日补贴_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>

                <td class="auto-style105">
                    3.养老统筹：</td>
                <td class="style24" style="background-color: #ffffff">
                        <asp:TextBox ID="txt养老统筹" runat="server" Width="140px"></asp:TextBox>

                    &nbsp;<strong>元 备注：<asp:TextBox ID="txt养老统筹_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
            </tr>
             <tr>
                <td class="auto-style97" >
                    4.福利费：</td>
                <td class="auto-style5" style="background-color: #ffffff">
                        <asp:TextBox ID="txt福利费" runat="server" Width="150px"></asp:TextBox>

                    &nbsp;<strong>元&nbsp; 备注：<asp:TextBox ID="txt福利费_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
                <td class="auto-style105" >
                    5.劳动保护费：</td>
                <td class="auto-style106" style="background-color: #ffffff">
                        <asp:TextBox ID="txt劳动保护费" runat="server" Width="140px"></asp:TextBox>

                    &nbsp;<strong>元 备注：<asp:TextBox ID="txt劳动保护费_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
                <td class="auto-style105">
                    6.住房公积金：</td>
                <td class="style24" style="background-color: #ffffff">
                        <asp:TextBox ID="txt住房公积金" runat="server" Width="140px"></asp:TextBox>

                        <strong>&nbsp;元 备注：<asp:TextBox ID="txt住房公积金_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
            </tr>
            <tr>
                <td class="auto-style97" >
                    7.8.住房补贴：</td>
                <td class="auto-style5" style="background-color: #ffffff">
                    <asp:TextBox ID="txt住房补贴" runat="server" Width="150px"></asp:TextBox>
                &nbsp;<strong>元&nbsp; 备注：<asp:TextBox ID="txt住房补贴_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
                <td class="auto-style105" >
                    9.材料费：</td>
                <td class="auto-style106" style="background-color: #ffffff">
                    <asp:TextBox ID="txt材料费" runat="server" Width="140px"></asp:TextBox>
                &nbsp;<strong>元 备注：<asp:TextBox ID="txt材料费_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
                <td class="auto-style105">
                    10.工程出包费：</td>
                <td class="style24" style="background-color: #ffffff">
                    <asp:TextBox ID="txt工程出包费" runat="server" Width="140px"></asp:TextBox>
                &nbsp;<strong>元 备注：<asp:TextBox ID="txt工程出包费_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
            </tr>
            <tr>
                <td class="MainStyle" >
                    11.固定资产：</td>
                <td class="auto-style5" style="background-color: #ffffff">
                    <asp:TextBox ID="txt固定资产" runat="server" Width="150px"></asp:TextBox>
                &nbsp;<strong>元&nbsp; 备注：<asp:TextBox ID="txt固定资产_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
                <td class="auto-style105" >
                    12.办公费：</td>
                <td class="auto-style106" style="background-color: #ffffff">
                    <asp:TextBox ID="txt办公费" runat="server" Width="140px"></asp:TextBox>
                &nbsp;<strong>元 备注：<asp:TextBox ID="txt办公费_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
                <td class="auto-style105">
                    13.差旅费：</td>
                <td class="style24" style="background-color: #ffffff">
                    <asp:TextBox ID="txt差旅费" runat="server" Width="140px"></asp:TextBox>
                &nbsp;<strong>元 备注：<asp:TextBox ID="txt差旅费_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
            </tr>
            <tr>
                <td class="MainStyle" >
                    14.水电费：</td>
                <td class="auto-style5" style="background-color: #ffffff">
                    <asp:TextBox ID="txt水电费" runat="server" Width="150px"></asp:TextBox>
                &nbsp;<strong>元&nbsp; 备注：<asp:TextBox ID="txt水电费_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
                <td class="auto-style105" >
                    15.物业管理费：</td>
                <td class="auto-style106" style="background-color: #ffffff">
                    <asp:TextBox ID="txt物业管理费" runat="server" Width="140px"></asp:TextBox>
                &nbsp;<strong>元 备注：<asp:TextBox ID="txt物业管理费_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
                <td class="auto-style105">
                    16.交通运输费用：</td>
                <td class="style24" style="background-color: #ffffff">
                    <asp:TextBox ID="txt交通运输费用" runat="server" Width="140px"></asp:TextBox>
                &nbsp;<strong>元 备注：<asp:TextBox ID="txt交通运输费用_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
            </tr>
            <tr>
                <td class="MainStyle" >
                    17.邮电费用：</td>
                <td class="auto-style5" style="background-color: #ffffff">
                    <asp:TextBox ID="txt邮电费用" runat="server" Width="150px"></asp:TextBox>
                &nbsp;<strong>元&nbsp; 备注：<asp:TextBox ID="txt邮电费用_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
                  <td class="auto-style105" >
                    18.维修费用：</td>
                <td class="auto-style106" style="background-color: #ffffff">
                    <asp:TextBox ID="txt维修费用" runat="server" Width="140px"></asp:TextBox>
                &nbsp;<strong>元 备注：<asp:TextBox ID="txt维修费用_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
                  <td class="auto-style105" >
                    19.会议费：</td>
                <td class="style24" style="background-color: #ffffff">
                    <asp:TextBox ID="txt会议费" runat="server" Width="140px"></asp:TextBox>
                &nbsp;<strong>元 备注：<asp:TextBox ID="txt会议费_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
            </tr>
              <tr>
                <td class="MainStyle" >
                    20.培训费：</td>
                <td class="auto-style5" style="background-color: #ffffff">
                    <asp:TextBox ID="txt培训费" runat="server" Width="150px"></asp:TextBox>
                &nbsp;<strong>元&nbsp; 备注：<asp:TextBox ID="txt培训费_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
                   <td class="auto-style105" >
                       21.业务招待费：</td>
                <td class="auto-style106" style="background-color: #ffffff">
                    <asp:TextBox ID="txt业务招待费" runat="server" Width="140px"></asp:TextBox>
                &nbsp;<strong>元 备注：<asp:TextBox ID="txt业务招待费_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>

           
                   <td class="auto-style105" >
                       22.劳务费：</td>
                <td class="style24" style="background-color: #ffffff">
                    <asp:TextBox ID="txt劳务费" runat="server" Width="140px"></asp:TextBox>
                &nbsp;<strong>元 备注：<asp:TextBox ID="txt劳务费_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
            </tr>
            <tr>
                <td class="MainStyle" >
                    23.租赁费：</td>
                <td class="auto-style5" style="background-color: #ffffff">
                    <asp:TextBox ID="txt租赁费" runat="server" Width="150px"></asp:TextBox>
                &nbsp;<strong>元&nbsp; 备注：<asp:TextBox ID="txt租赁费_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
                <td class="auto-style105" >
                    24.税金及附加：</td>
                <td class="auto-style106" style="background-color: #ffffff">
                    <asp:TextBox ID="txt税金及附加" runat="server" Width="140px"></asp:TextBox>
                &nbsp;<strong>元 备注：<asp:TextBox ID="txt税金及附加_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
                <td class="auto-style105">
                    25.安全生产费用:</td>
                <td class="style24" style="background-color: #ffffff">
                    <asp:TextBox ID="txt安全生产费用" runat="server" Width="140px"></asp:TextBox>
                &nbsp;<strong>元 备注：<asp:TextBox ID="txt安全生产费用_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
            </tr>
            <tr>
                <td class="MainStyle" >
                    26.工会经费：</td>
                <td class="auto-style5" style="background-color: #ffffff">
                    <asp:TextBox ID="txt工会经费" runat="server" Width="150px"></asp:TextBox>
                &nbsp;<strong>元&nbsp; 备注：<asp:TextBox ID="txt工会经费_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
                <td class="auto-style105" >
                    27.其他费用：</td>
                <td class="auto-style106" style="background-color: #ffffff">
                    <asp:TextBox ID="txt其他费用" runat="server" Width="140px"></asp:TextBox>
                &nbsp;<strong>元 备注：<asp:TextBox ID="txt其他费用_备注"  Width="140px" runat="server"></asp:TextBox>
                        </strong></td>
                <td style="background-color:#D6E2F3;" colspan="2">
                    &nbsp;</td>
               
            </tr>
           <%-- <tr>
                <td class="MainStyle" >
                    附件文件：&nbsp;&nbsp;
                </td>
                <td class="style25" style="background-color: #ffffff" colspan="5">
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                    </asp:CheckBoxList>
                    &nbsp;<asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False"
                        ImageAlign="AbsMiddle" ImageUrl="../images/Button/DelFile.jpg" OnClick="ImageButton3_Click" />
                    &nbsp; &nbsp;&nbsp;
                    <asp:ImageButton ID="ImageButton5" runat="server" CausesValidation="False" ImageAlign="AbsMiddle"
                        ImageUrl="~/images/Button/ReadFile.gif" OnClick="ImageButton5_Click" ToolTip="如果无法阅读或编辑文件，请在主页面点击【关于】下载安装相关插件" />
                    &nbsp; &nbsp;&nbsp;
                    <asp:ImageButton ID="ImageButton6" runat="server" CausesValidation="False" ImageAlign="AbsMiddle"
                        ImageUrl="~/images/Button/EditFile.gif" OnClick="ImageButton6_Click" ToolTip="如果无法阅读或编辑文件，请在主页面点击【关于】下载安装相关插件" />
                </td>
            </tr>--%>
           <%-- <tr>
                <td class="MainStyle" >
                    上传附件：&nbsp;&nbsp;
                </td>
                <td class="style25" style="background-color: #ffffff" colspan="5">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="350px" />
                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" ImageAlign="AbsMiddle"
                        ImageUrl="../images/Button/UpLoad.jpg" OnClick="ImageButton2_Click" />
                </td>
            </tr>
            <tr>
                <td class="TitleStyle" colspan="6">
                    <strong>流程审批附加属性</strong>
                </td>
            </tr>
            <tr>
                <td class="MainStyle" >
                    下一节点选择：&nbsp;&nbsp;
                </td>
                <td class="style25" style="background-color: #ffffff" colspan="5">
                    <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"
                        Width="350px">
                    </asp:DropDownList>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" Text="根据条件自动决定下一节点" />
                </td>
            </tr>
            <tr>
                <td class="MainStyle" >
                    评审模式：&nbsp;&nbsp;
                </td>
                <td class="style25" style="background-color: #ffffff" colspan="5">
                    <asp:TextBox ID="TextBox1" runat="server" BorderStyle="None" BorderWidth="0px" ReadOnly="True"
                        Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="MainStyle" >
                    审批人选择模式：&nbsp;&nbsp;
                </td>
                <td class="style25" style="background-color: #ffffff" colspan="5">
                    <asp:TextBox ID="TextBox2" runat="server" BorderStyle="None" BorderWidth="0px" ReadOnly="True"
                        Width="350px"></asp:TextBox>
                </td>
            </tr>--%>
            <%--<tr>
                <td class="MainStyle" >
                    审批人选择：<a style="color: #FF0000;font-size:18px;">*</a>
                </td>
                <td class="style25" style="background-color: #ffffff" colspan="5">
                    <asp:TextBox ID="TextBox5" runat="server" onkeydown="javascript:return false;" Width="349px"></asp:TextBox>
                        <img class="HerCss" id="Img3" onclick="openuserDialog('spr')"
                            src="../images/Button/search.gif" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox5"
                        Display="Dynamic" ErrorMessage="*必须指定审批人"></asp:RequiredFieldValidator>
                    <asp:CheckBox ID="CHKSMS" runat="server" Checked="True" />
                    <img src="../images/TreeImages/@sms.gif" />内部邮件<asp:CheckBox ID="CHKMOB" Checked="True" runat="server" /><img
                        src="../images/TreeImages/mobile_sms.gif" />短信息
                <span style="color: #FF0000">（勾选后向审批人发送手机短信提醒）</span></td>
            </tr>--%>
        </table>
        <%--</table>--%>
    </div>
    <script>
		
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
    <div id="win_bumen" class="easyui-window" data-options="title:'选择部门',iconCls:'icon-search',closed:true,closable:false,minimizable:false,maximizable:false,collapsible:false,resizable:true" style="width: 350px; height: 400px;visibility: hidden; padding: 5px;">
         <iframe id="bumen" scrolling="yes" frameborder="0" src="../Main/SelectDanWei.aspx?TableName=ERPUser&LieName=UserName" style="width: 100%; height: 100%;"></iframe>
    </div>
     <div id="win_user" class="easyui-window" data-options="title:'选择用户',iconCls:'icon-search',closed:true,closable:false,minimizable:false,maximizable:false,collapsible:false,resizable:true" style="width: 350px; height: 400px;visibility: hidden; padding: 5px;">
         <iframe id="user" scrolling="yes" frameborder="0" src="../Main/SelectUser.aspx?TableName=ERPUser&LieName=UserName" style="width: 100%; height: 100%;"></iframe>
    </div>
     <div id="win_select" class="easyui-window" data-options="title:'选择项目',iconCls:'icon-search',closed:true,closable:false,minimizable:false,maximizable:false,collapsible:false,resizable:true" style="width: 400px; height: 450px;visibility: hidden; padding: 5px;">
         <iframe id="select" scrolling="yes" frameborder="0" src="CommonSelect.aspx?TypeStr=GCKJS" style="width: 100%; height: 100%;"></iframe>
    </div>
</body>
</html>
