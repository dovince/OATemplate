<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AptitudeModify.aspx.cs" Inherits="Aptitude_AptitudeModify" %>
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
    <script src="../JS/easyui-lang-zh_CN.js"></script>
    <script lang ="javascript">
        var a;
        function CheckValuePiece() {
            if (window.document.form1.GoPage.value == "") {
                alert("请输入跳转的页码！");
                window.document.form1.GoPage.focus();
                return false;
            }
            return true;
        }

        $(document).ready(function () {

            $(".input_cxcalendar").each(function () {
                //debugger
                var a = new Calendar({
                    targetCls: $(this),
                    type: 'yyyy-mm-dd',
                    wday: 1
                }, function (val) {
                    //console.log(val);
                });
            });
            $('#win_select').window({
                onBeforeClose: function () {
                    //debugger
                    var returnVal = "";
                    for (var i = 0; i < window.length; i++) {
                        if (window[i].frameElement.id == "xmqqbh") {
                            //根据弹出窗内部的ifame的id来定位
                            returnVal = window[i].returnValue;
                            document.getElementById('HiddenField_xmqqbh').value = returnVal;
                            document.getElementById('btnaddsomething').click();
                        }
                    }
                    $('#txt项目编号')[0].value = returnVal;
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

            $('#win_bumen').window({
                onBeforeClose: function () {
                    var returnVal = "";
                    for (var i = 0; i < window.length; i++) {
                        if (window[i].frameElement.id == "bumen") {
                            //根据弹出窗内部的ifame的id来定位
                            returnVal = window[i].returnValue;
                        }
                    }
                    $('#txt使用单位').val(returnVal);
                }
            });
        });
        var usertypeid = "";
        function openuserDialog(utype) {
            //防止缓存之前的页面
            var RadNum = Math.random();
            $("#user")[0].src += '&Radstr=' + RadNum;
            $('#win_user').css("visibility", "visible");
            $('#win_user').window('open');
            if (utype == "spr") {
                usertypeid = "TextBox5";
            }
        }
        function openXMQQXXDialog() {
            //debugger;
            var RadNum = Math.random();
            $("#xmqqbh")[0].src += '&Radstr=' + RadNum;
            $('#win_select').css("visibility", "visible");
            //通过EasyUI的resize函数，进行窗体的大小进行弹窗位置的设置;
            $('#win_select').window('open');
        }

        function openbumenDialog() {
            var RadNum = Math.random();
            $("#bumen")[0].src += '&Radstr=' + RadNum;
            $('#win_bumen').css("visibility", "visible");
            $('#win_bumen').window('open');
        }
        function validateAptitudeFiles() {
            //验证用户是否选择的资质证照文件  

            var checkedid = $("#lb资质证照").find("input:checked");
            var selectVal = "";
            if (checkedid.length > 0) {
                for (var i = 0; i < checkedid.length; i++) {
                    var item = checkedid[i].value;
                    selectVal += item + ";";
                }

                if (window.confirm("您选择的资质证照：\n" + selectVal.replace(";", ";\n") + " 你确认提交申请吗？")) {
                    $("#HiddenField_AptitudeFiles")[0].value = selectVal;
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                window.alert("请选择资质证照！");
                return false;
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
<body onload="Load_Do();">
    <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="HiddenField_formcontent" runat="server" />
            <asp:HiddenField ID="HiddenField_WenJianList" runat="server" />
            <asp:HiddenField ID="HiddenField_AptitudeFiles" runat="server" />
            <asp:HiddenField ID="HiddenField_html" runat="server" />
        </div>
        <div>
            <table id="PrintHide" style="width: 100%" border="0" cellpadding="2" cellspacing="1">
                <tr>
                    <td valign="middle" style="border-bottom: #006633 1px ; height: 30px;">&nbsp;<img src="../images/BanKuaiJianTou.gif" />
                        <a class="hei" href="../Main/MyDesk.aspx">桌面</a>&nbsp;>>&nbsp;资质管理&nbsp;&gt;&gt;&nbsp;资质使用申请修改
                    </td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px ; height: 30px;">
                    <asp:HiddenField ID="HiddenField_xmqqbh" runat="server" />
                        <asp:ImageButton ID="btnModify" runat="server" ImageUrl="~/images/Button/Submit.jpg"
                            OnClick="btnModify_Click" OnClientClick="javascript:return validateAptitudeFiles()" Style="height: 19px" />
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
                           资质使用申请审批表</strong>

                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">申请人：<a style="color: #FF0000; font-size: 18px;">*</a>&nbsp;&nbsp;
                    </td>
                    <td style="background-color: #ffffff; text-align: left;" class="auto-style8">
                        <asp:TextBox ID="txt申请人" runat="server" Width="200px"></asp:TextBox>
                        <asp:Button ID="btnaddsomething" runat="server" BackColor="White" BorderColor="White" CausesValidation="False" ForeColor="White" Height="16px" onclick="btnaddsomething_Click" Text="1" Width="1px" />
                    </td>


                    <td class="auto-style9">使用部门：&nbsp;&nbsp;<a style="color: #FF0000; font-size: 18px;">*</a>
                    </td>


                    <td style="background-color: #ffffff; text-align: left;" class="auto-style2">
                        <asp:TextBox ID="txt使用单位" runat="server" Width="480px"></asp:TextBox>
                        <img class="HerCss" onclick="openbumenDialog()" src="../images/Button/search.gif" />
                    </td>


                </tr>
                <tr>
                   


                    <td class="auto-style3" class="auto-style9">项目名称：<a style="color: #FF0000; font-size: 18px;">*</a></td>


                    <td colspan="3" style="background-color: #ffffff; text-align: left;" class="auto-style2">
                        <asp:TextBox ID="txt项目名称" runat="server"  Width="911px" Height="40px" TextMode="MultiLine"></asp:TextBox>
                    </td>


                </tr>
                <tr>
                    <td class="auto-style3">使用期限：<a style="color: #FF0000; font-size: 18px;">*</a></td>
                    <td style="background-color: #ffffff; text-align: left;" class="auto-style8">
                        <asp:TextBox ID="txtQJSJStart" runat="server" class="input_cxcalendar" Width="120px"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidatorBGNR" runat="server" ControlToValidate="txtQJSJStart"
                            ErrorMessage="*该项不可以为空" Display="Dynamic"></asp:RequiredFieldValidator>&nbsp;至&nbsp;
                    <asp:TextBox ID="txtQJSJEnd" runat="server" Width="120px" AutoPostBack="True" onpaste="return false;" class="input_cxcalendar" CausesValidation="True" onkeypress="event.returnValue=false;"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQJSJEnd"
                            ErrorMessage="*该项不可以为空" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtQJSJStart" ControlToValidate="txtQJSJEnd" Display="Dynamic"
                            ErrorMessage="CompareValidator" Operator="GreaterThanEqual">结束时间要在开始时间之后</asp:CompareValidator>
                    </td>


                    <td class="auto-style9">资质使用范围：<a style="color: #FF0000; font-size: 18px;">*</a></td>


                    <td style="background-color: #ffffff; text-align: left;" class="auto-style2">
                        <asp:TextBox ID="txt使用范围" runat="server" Height="40px" Width="480px" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorQJYY" runat="server" ControlToValidate="txt使用范围"
                            ErrorMessage="*该项不可以为空" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>


                </tr>
                <tr>
                    <td class="auto-style3">其它证照：</td>
                    <td style="background-color: #ffffff; text-align: left;" class="auto-style8">
                        <asp:TextBox ID="txtOtherLicense" runat="server" Width="480px" Height="40px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td class="auto-style9">申请时间：</td>
                    <td style="background-color: #ffffff; text-align: left;" class="auto-style2">
                        <asp:TextBox ID="txt申请时间" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">备注：
                    </td>
                    <td colspan="3" style="background-color: #ffffff; text-align: left;">
                        <asp:TextBox ID="txtComment" runat="server" Width="1160px" Height="80px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 100%" bgcolor="#999999" border="0" cellpadding="2" cellspacing="1">
                <tr>
                    <td style="background-color: #ffffff" align="center">
                        <asp:Label ID="lb资质证照" runat="server" Text="资质证照名称"></asp:Label>
                        <asp:TextBox ID="TextBox3" runat="server" Style="display: none"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 100%" bgcolor="#999999" border="0" cellpadding="3" cellspacing="1">
                <tr>
                    <td class="auto-style3">附件文件：&nbsp;&nbsp;
                    </td>
                    <td colspan="3" style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                        </asp:CheckBoxList>
                        &nbsp;<asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False"
                            ImageAlign="AbsMiddle" ImageUrl="../images/Button/DelFile.jpg" OnClick="ImageButton3_Click" />
                        &nbsp; &nbsp;&nbsp;
                    <asp:ImageButton ID="ImageButton5" runat="server" CausesValidation="False" ImageAlign="AbsMiddle"
                        ImageUrl="~/images/Button/ReadFile.gif" OnClick="ImageButton5_Click" />
                        &nbsp; &nbsp;&nbsp;
                    <asp:ImageButton ID="ImageButton6" runat="server" CausesValidation="False" ImageAlign="AbsMiddle"
                        ImageUrl="~/images/Button/EditFile.gif" OnClick="ImageButton6_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">上传附件：&nbsp;&nbsp;
                    </td>
                    <td colspan="3" style="padding-left: 5px; height: 25px; background-color: #ffffff">
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="350px" />
                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" ImageAlign="AbsMiddle"
                            ImageUrl="../images/Button/UpLoad.jpg" OnClick="ImageButton2_Click" />
                    </td>
                </tr>

            </table>
        </div>
        <script>
            function Load_Do() {
                setTimeout("Load_Do()", 1000);
                var content = document.getElementById("lb资质证照").innerHTML;
                $("#HiddenField_html")[0].value = content;
                document.getElementById("TextBox3").value = content;
            }
            function btnyujitoubiaotime_onclick() {

            }
        </script>
    </form>
        <div id="win_user" class="easyui-window" data-options="title:'选择用户',iconCls:'icon-search',closed:true,closable:false,minimizable:false,maximizable:false,collapsible:false,resizable:true" style="width: 350px; height: 400px; visibility: hidden; padding: 5px;">
            <iframe id="user" scrolling="yes" frameborder="0" src="../Main/SelectUser.aspx?TableName=ERPUser&LieName=UserName" style="width: 100%; height: 100%;"></iframe>
        </div>
        <div id="win_bumen" class="easyui-window" data-options="title:'选择部门',iconCls:'icon-search',closed:true,closable:false,minimizable:false,maximizable:false,collapsible:false,resizable:true" style="width: 350px; height: 400px; visibility:hidden; padding: 5px;">
            <iframe id="bumen" scrolling="yes" frameborder="0" src="../Main/SelectDanWei.aspx?TableName=ERPUser&LieName=UserName" style="width: 100%; height: 100%;"></iframe>
        </div>
        <div id="win_select" class="easyui-window" data-options="title:'选择项目',iconCls:'icon-search',closed:true,closable:false,minimizable:false,maximizable:false,collapsible:false,resizable:true" style="width: 500px; height: 400px; padding: 5px; visibility:hidden">
            <iframe id="xmqqbh" scrolling="yes" frameborder="0" src="../BusinessManage/CommonSelect.aspx?TypeStr=XMQQBH" style="width: 100%; height: 100%;"></iframe>
        </div>
</body>
</html>
