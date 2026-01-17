<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERPCaiDanAdd.aspx.cs" Inherits="RequireCreate_ERPCaiDanAdd" %>
<%@ Register Src="~/UserControls/UploadFiles.ascx" TagPrefix="uc1" TagName="UploadFiles" %>

<!DOCTYPE html>
<head>
    <title>
        <%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" type="text/css" rel="STYLESHEET">
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
    <link href="../CSS/cxcalendar.css" rel="stylesheet" />
    <link href="../CSS/default/easyui.css" rel="stylesheet" />
    <link href="../CSS/common/upload.css" rel="stylesheet" />
    <script type="text/javascript" src="../JS/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../CSS/calendar.js"></script>
    <script src="../JS/jquery.easyui.min.js"></script>
    <script type="text/javascript">
        var g_RCount = 0;
        $(document).ready(function () {
            

            InitTimeSelect();

            $('#win_bumen').window({
                onBeforeClose: function () {
                    var returnVal = "";
                    for (var i = 0; i < window.length; i++) {
                        if (window[i].frameElement.id == "bumen") {
                            //根据弹出窗内部的ifame的id来定位
                            returnVal = window[i].returnValue;
                        }
                    }
                    if (bumentypeid != "") {
                        var idstr = "#" + bumentypeid;
                        $(idstr).val(returnVal);
                    }
                    else {
                        $('#txtTBBM')[0].value = returnVal;
                    }
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
                    if (usertypeid != "") {
                        var idstr = "#" + usertypeid;
                        $(idstr).val(returnVal);
                    }

                    //$('#win_user').window('refresh');
                }
            });
            
        });
        function InitTimeSelect() {
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
            $(".input_timecxcalendar").each(function () {
                //debugger
                var a = new Calendar({
                    targetCls: $(this),
                    type: 'yyyy-mm-dd HH:MM',
                    wday: 2
                }, function (val) {
                    //console.log(val);
                });
            });
        }
        function PrintTable() {
            document.getElementById("PrintHide").style.visibility = "hidden"
            print();
            document.getElementById("PrintHide").style.visibility = "visible"
            //123    
        }

        var bumentypeid = "";
        function openbumenDialog(btype) {
            var RadNum = Math.random();
            $("#bumen")[0].src = '../Main/SelectDanWei.aspx?TableName=ERPUser&LieName=UserName&Radstr=' + RadNum;
            $('#win_bumen').css("visibility", "visible");
            $('#win_bumen').window('open');

            bumentypeid = btype;
        }
        var usertypeid = "";
        function openuserDialog(utype) {
            //防止缓存之前的页面
            var RadNum = Math.random();
            $("#user")[0].src = '../Main/SelectUser.aspx?TableName=ERPUser&LieName=UserName&Radstr=' + RadNum;
            $('#win_user').css("visibility", "visible");
            $('#win_user').window('open').window('resize', { top: $("body").scrollTop() + 50 });

            usertypeid = utype;
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
        
    function replaceAll(str, source, target) {
        str = str.replace(source, target);
        while (str.indexOf(source) > -1) {
            str = str.replace(source, target);
        }
        return str;
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

        .style38 {
        }

        .style29 {
            width: 107px;
            text-align: right;
            height: 7px;
        }

        .style44 {
            height: 25px;
        }

        .style45 {
            height: 52px;
            width: 550px;
        }

        .style49 {
        }

        .style50 {
            height: 23px;
        }

        .style53 {
            width: 107px;
            text-align: right;
            height: 25px;
        }

        .MainStyle {
            text-align: right;
            font: 14px "微软雅黑";
            width: 140px;
            height: 30px;
        }

        .TitleStyle {
            text-align: center;
            font: 16px "宋体";
            height: 35px;
        }

        .auto-style1 {
            width: 473px;
        }

        .auto-style2 {
            height: 25px;
            width: 100px;
        }
        .com_input{
            width:100px;
        }
    </style>
</head>
<body onload="javascript:document.getElementById('TextBox5').readOnly=true;">
    <form id="form1" runat="server">
    <div style="display: none;">
        
    </div>
    <div>
        <table id="PrintHide" style="width: 100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="middle" style="border-bottom: #006633 1px dashed; height: 30px;">
                    &nbsp;<img src="../images/BanKuaiJianTou.gif" />
                    <a class="hei" href="../Main/MyDesk.aspx">桌面</a>&nbsp;>>&nbsp;菜单管理
                </td>
                <td align="right" valign="middle" style="border-bottom: #006633 1px dashed; height: 30px;">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:HiddenField ID="HiddenField_formcontent" runat="server" />
                    <asp:HiddenField ID="HiddenField_xmqqbh" runat="server" />
                    <asp:HiddenField ID="HiddenField_WenJianList" runat="server" />
                    <asp:HiddenField ID="HiddenField_CTBody" runat="server" />
                    <asp:HiddenField ID="HiddenField_InitHtml" runat="server" />
                    <asp:HiddenField ID="HiddenField_UserName" runat="server" />
                    <asp:HiddenField ID="HiddenField_Department" runat="server" />
                    <asp:HiddenField ID="rowCount" runat="server" Value="0" />
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/Button/Submit.jpg"
                        OnClick="ImageButton1_Click" />
                    <img src="../images/Button/JianGe.jpg" />&nbsp;
                    <img class="HerCss" onclick="javascript:window.history.go(-1)" src="../images/Button/BtnExit.jpg" />&nbsp;
                </td>
            </tr>
            <tr>
                <td height="3px" colspan="2" style="background-color: #ffffff">
                </td>
            </tr>
        </table>
        <table style="width: 100%" bgcolor="#ffffff" border="0" cellpadding="2" cellspacing="0">
            <tr>
                <td>
                    <table class="tb_normal" style="width: 100%" bgcolor="#999999" border="0" cellpadding="2" cellspacing="1">
                        <tr>
                            <td class="TitleStyle" colspan="4">
                                <strong>
                                <asp:TextBox ID="txtWorkName" runat="server" Width="53px" Visible="False"></asp:TextBox>
                                菜单管理</strong>
                                <asp:TextBox ID="txtDJtime" runat="server" onfocus="setday(this)" Width="46px" 
                                    Height="20px" Visible="False"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr style="height: 30px;">
                                    <td class="MainStyle">
                                        展示日期起：<a style="color: #FF0000;font-size:18px;">*</a>
                                    </td>
                                    <td style="background-color: #ffffff" class="auto-style1" colspan="1">
                                    
                            <asp:TextBox id="txtZhanShiRiQiQi" runat="server" class="input_cxcalendar" width="320px"  ></asp:TextBox>
                        </td>
                                    <td class="MainStyle">
                                        展示日期止：<a style="color: #FF0000;font-size:18px;">*</a>
                                    </td>
                                    <td style="background-color: #ffffff" class="auto-style1" colspan="1">
                                    
                            <asp:TextBox id="txtZhanShiRiQiZhi" runat="server" class="input_cxcalendar" width="320px"  ></asp:TextBox>
                        </td>
                                    </tr>
                                    <%--<tr style="height: 30px;">
                                    <td class="MainStyle">
                                        菜单图片：<a style="color: #FF0000;font-size:18px;">*</a>
                                    </td>
                                    <td style="background-color: #ffffff" class="auto-style1" colspan="3">
                                    
                            <asp:TextBox id="txtCaiDanTuPian" runat="server" width="800px"  ></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorCaiDanTuPian" runat="server" ControlToValidate="txtCaiDanTuPian" ErrorMessage="*该项不可以为空" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                                    </tr>--%>
                                    <%--<tr style="height: 30px;">
                                    <td class="MainStyle">
                                        修改时间：&nbsp;&nbsp;
                                    </td>
                                    <td style="background-color: #ffffff" class="auto-style1" colspan="1">
                                    
                            <asp:TextBox id="txtModifyTime" runat="server" width="320px"  ></asp:TextBox>
                        </td>
                                    <td class="MainStyle">
                                        用户名：&nbsp;&nbsp;
                                    </td>
                                    <td style="background-color: #ffffff" class="auto-style1" colspan="1">
                                    
                            <asp:TextBox id="txtUserName" runat="server" width="320px"  ></asp:TextBox>
                        </td>
                                    </tr>--%>
                                    <%--<tr style="height: 30px;">
                                    <td class="MainStyle">
                                        部门：&nbsp;&nbsp;
                                    </td>
                                    <td style="background-color: #ffffff" class="auto-style1" colspan="1">
                                    
                            <asp:TextBox id="txtBuMen" runat="server" width="320px"  ></asp:TextBox>
                        </td>
                                            <td class="MainStyle">
                                            </td>
                                            <td style="background-color: #ffffff" class="auto-style1">
                                            </td>
                                            
                                        </tr>--%>
                         <tr>
                            <td class="MainStyle">
                                上传菜单图片：&nbsp;&nbsp;
                            </td>
                            <td colspan="3" 
                                style="padding-left: 5px; height: 25px; background-color: #ffffff">
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="350px" />
                                <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" ImageAlign="AbsMiddle"
                                    ImageUrl="../images/Button/UpLoad.jpg" OnClick="ImageButton2_Click" />
                            </td>
                        </tr>        
                        <tr>
                            <td class="MainStyle">
                                菜单图片：&nbsp;&nbsp;
                            </td>
                            <td colspan="3" style="padding-left: 5px; height: 25px; background-color: #ffffff">
                                <%--<uc1:UploadFiles runat="server" ID="UploadFiles" />--%>
                                
                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                                </asp:CheckBoxList>
                                &nbsp;<asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False"
                                    ImageAlign="AbsMiddle" ImageUrl="../images/Button/DelFile.jpg" OnClick="ImageButton3_Click" />
                                &nbsp; &nbsp;&nbsp;
                                <asp:ImageButton ID="ImageButton5" runat="server" CausesValidation="False" ImageAlign="AbsMiddle"
                                    ImageUrl="~/images/Button/ReadFile.gif" OnClick="ImageButton5_Click" ToolTip="如果无法阅读或编辑文件，请在主页面点击【关于】下载安装相关插件" />
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <div id="win_bumen" class="easyui-window" data-options="title:'选择部门',iconCls:'icon-search',closed:true,closable:false,minimizable:false,maximizable:false,collapsible:false,resizable:true" style="width: 350px; height: 400px;visibility: hidden; padding: 5px;">
        <iframe id="bumen" scrolling="yes" frameborder="0" src="" style="width: 100%; height: 100%;"></iframe>
    </div>
    <div id="win_user" class="easyui-window" data-options="title:'选择用户',iconCls:'icon-search',closed:true,closable:false,minimizable:false,maximizable:false,collapsible:false,resizable:true" style="width: 350px; height: 400px;visibility: hidden; padding: 5px;">
        <iframe id="user" scrolling="yes" frameborder="0" src="" style="width: 100%; height: 100%;"></iframe>
    </div>
    <div id="win_select" class="easyui-window" data-options="title:'选择项目',iconCls:'icon-search',closed:true,closable:false,minimizable:false,maximizable:false,collapsible:false,resizable:true" style="width: 600px; height: 400px; padding: 5px; visibility:hidden">
         <iframe id="iframe_select" scrolling="yes" frameborder="0" src="" style="width: 100%; height: 100%;"></iframe>
    </div>
</body>
</html>

