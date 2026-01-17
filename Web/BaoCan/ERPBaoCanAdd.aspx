<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERPBaoCanAdd.aspx.cs" Inherits="RequireCreate_ERPBaoCanAdd" %>

<!DOCTYPE html>
<head>
    <title>
        <%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" type="text/css" rel="STYLESHEET">
    <link href="../Style/Style1.css" rel="stylesheet" />
    <link href="../CSS/common/form.theme.css" rel="stylesheet" />
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
    <link href="../CSS/common/widget.theme.css" rel="stylesheet" />
    <link href="../CSS/common/process_tab_main.css" rel="stylesheet" />
    <link href="../CSS/common/widget.theme.css" rel="stylesheet" />
    <link href="../CSS/cxcalendar.css" rel="stylesheet" />
    <link href="../CSS/default/easyui.css" rel="stylesheet" />
    <script type="text/javascript" src="../JS/jquery-1.9.1.js"></script>
    <script src="../JS/lui/tabcontent.js" type="text/javascript"></script>
    <script type="text/javascript" src="../CSS/calendar.js"></script>
    <script src="../JS/jquery.easyui.min.js"></script>
    <script src="../JS/common.js?v=202306020823" type="text/javascript"></script>
    <script type="text/javascript">
        var g_RCount = 0;
        $(document).ready(function () {


            InitTimeSelect();

            //$('#win_bumen').window({
            //    onBeforeClose: function () {
            //        var returnVal = "";
            //        for (var i = 0; i < window.length; i++) {
            //            if (window[i].frameElement.id == "bumen") {
            //                //根据弹出窗内部的ifame的id来定位
            //                returnVal = window[i].returnValue;
            //            }
            //        }
            //        if (bumentypeid != "") {
            //            var idstr = "#" + bumentypeid;
            //            $(idstr).val(returnVal);
            //        }
            //        else {
            //            $('#txtTBBM')[0].value = returnVal;
            //        }
            //    }
            //});
            //$('#win_user').window({
            //    onBeforeClose: function () {
            //        var returnVal = "";
            //        for (var i = 0; i < window.length; i++) {
            //            if (window[i].frameElement.id == "user") {
            //                //根据弹出窗内部的ifame的id来定位
            //                returnVal = window[i].returnValue;
            //            }
            //        }
            //        if (usertypeid != "") {
            //            var idstr = "#" + usertypeid;
            //            $(idstr).val(returnVal);
            //        }

            //        //$('#win_user').window('refresh');
            //    }
            //});
            $(".checkbox_wc").each(function () {
                if (!$(this).prop('checked')) {
                    $("#DropDownListShiJianDian_WuCan").prop('checked', false);
                }
            })
            $(".checkbox_zc").each(function () {
                if (!$(this).prop('checked')) {
                    $("#DropDownListShiJianDian_ZaoCan").prop('checked', false);
                }
            })
        });
        function InitTimeSelect() {
            $.each($('.input_cxcalendar'), function (i, item) {
                var p = $(item);
                var a = new Calendar({
                    targetCls: $(this),
                    type: 'yyyy-mm-dd',
                    wday: 2
                }, function (val) {
                    if (typeof (__doPostBack) == 'function') {
                        __doPostBack(p.attr("id"), '');
                    }
                    else {
                        p.change();
                    }
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

        function replaceAll(str, source, target) {
            str = str.replace(source, target);
            while (str.indexOf(source) > -1) {
                str = str.replace(source, target);
            }
            return str;
        }

        function doSubmitData() {
            document.getElementById("ImageButton1").click();
        }

        function changeSJD(sender, checkboxclass) {
            if ($("#" + sender).prop('checked')) {
                $("." + checkboxclass).prop("checked", true);
            }
            else {
                $("." + checkboxclass).prop("checked", false);
                //$("." + checkboxclass).removeAttr("checked");
            }
        }
        //function timechange() {
        //    var times = $("#txtBCRQ_Start").val();
        //    var timee = $("#txtBCRQ_End").val();
        //    var sjd = $("#DropDownListShiJianDian").val();



        //}


        //function ERPBaoCan_AddRow() {
        //    var rowCount = $(".ZLBody tr").length + 1;
        //    var html = $("#ZLBodyTr").html();
        //    html = replaceAll(html, "{RIndex}", "" + rowCount);
        //    html = replaceAll(html, "{Replace}", "" + rowCount);
        //    html = "<tr>" + html + "</tr>";
        //    $(".ZLBody").append(html);
        //    g_RCount++;
        //    $("#rowCount").val(g_RCount);
        //    InitTimeSelect();
        //}

        ////删除行事件
        //function ERPBaoCan_DelRow(sender) {
        //    $(sender).parent().parent().remove();
        //    //重置行ID
        //    var count = 0;
        //    $(".ZLBody tr").each(function (index, item) {
        //        var newID = index + 1;
        //        $(item).find(".rowIndexLabel")[0].innerHTML = newID;
        //        var rowIndexHidden = $(item).find(".rowIndexHidden")[0];
        //        rowIndexHidden.id = "rowIndex_" + newID;
        //        rowIndexHidden.name = "rowIndex_" + newID;
        //        rowIndexHidden.value = newID;
        //    });
        //    //$("#rowCount").val($(".ZLBody tr").length);
        //}
    </script>

    <style>
        input, label {
            vertical-align: middle;
        }

        #DropDownListBCFW label {
            padding-left: 2px;
            margin-right: 10px;
        }

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

        .com_input {
            width: 100px;
        }
    </style>
</head>
<body class="lui_form_body " style="margin-top: 43px; margin-bottom: 46px;" onload="javascript:document.getElementById('TextBox5').readOnly=true;">
    <form id="form1" runat="server">
        <div style="display: none;">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <%--<asp:HiddenField ID="HiddenField_formcontent" runat="server" />--%>
            <asp:HiddenField ID="HiddenField_xmqqbh" runat="server" />
            <asp:HiddenField ID="HiddenField_WenJianList" runat="server" />
            <asp:HiddenField ID="HiddenField_CTBody" runat="server" />
            <asp:HiddenField ID="HiddenField_InitHtml" runat="server" />
            <asp:HiddenField ID="HiddenField_UserName" runat="server" />
            <asp:HiddenField ID="HiddenField_Department" runat="server" />
            <asp:HiddenField ID="rowCount" runat="server" Value="0" />
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/Button/Submit.jpg"
                OnClick="ImageButton1_Click" Style="display: none;" />
            <img src="../images/Button/JianGe.jpg" />
        </div>
        <div>
            <div id="toolbar" data-lui-type="lui/toolbar!ToolBar" style="" class="lui-component" data-lui-cid="toolbar" data-lui-parse-init="1">
                <div>
                    <iframe frameborder="0" class="lui_toolbar_frame_float_mark" scrolling="no"></iframe>
                    <div class="lui_toolbar_frame_float">
                        <div class="lui_toolbar_left">
                            <div class="lui_toolbar_right">
                                <div class="lui_toolbar_content" style="max-width: 1200px;">
                                    <table style="margin: 0px auto; position: relative;">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <div class="lui_form_subject">
                                                        <span id="RebuildForm_lblFormTitle">我要报餐</span>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table style="margin: 0px auto;">
                                        <tbody>
                                            <tr>
                                                <td lui-button-container="1">
                                                    <div class="lui_toolbar_btn" data-lui-on-class="lui_toolbar_btn_on" data-lui-status-class="lui_toolbar_btn_toggle_on">
                                                        <div id="passedBtn" data-lui-type="lui/toolbar!Button" data-lui-parentid="toolbar" style="" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="bookedBtn" data-lui-parse-init="7" title="提交" tabindex="0" onclick="javascript:doSubmitData();">
                                                            <div class="lui_toolbar_btn_l" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                                                <div class="lui_toolbar_btn_r">
                                                                    <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                        <div id="lui-id-31" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-31">提交</div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td lui-button-container="2">
                                                    <div class="lui_toolbar_btn" data-lui-on-class="lui_toolbar_btn_on" data-lui-status-class="lui_toolbar_btn_toggle_on">
                                                        <div id="backBtn" data-lui-type="lui/toolbar!Button" style="" id="lui-id-3" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="backBtn" data-lui-parse-init="3" title="返回" tabindex="0" onclick="javascript:gobacktolist();">
                                                            <div class="lui_toolbar_btn_l" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                                                <div class="lui_toolbar_btn_r">
                                                                    <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                        <div id="lui-id-26" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-26">返回</div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="lui_form_path_frame" style="width: 90%; min-width: 980px; max-width: 1200px; margin: 0px auto;">

                <div data-lui-type="lui/menu!Menu" style="" id="lui-id-7" class="lui-component" data-lui-cid="lui-id-7" data-lui-parse-init="9">
                    <div class="lui_menu_frame_nav">
                        <div class="lui_menu_left">
                            <div class="lui_menu_right">
                                <div class="lui_menu_content">
                                    <div class="lui_menu_item_split"></div>
                                    <div class="lui_menu_item">
                                        <div data-lui-type="lui/menu!MenuItem" style="cursor: pointer;" id="lui-id-9" class="lui-component lui_item" data-lui-cid="lui-id-9" data-lui-parse-init="11" data-lui-switch-class="lui_item_stitch_class">

                                            <div class="lui_item_left">
                                                <div class="lui_item_right">
                                                    <div class="lui_item_content">
                                                        <div class="lui_icon_s lui_icon_s_home"></div>
                                                        <div class="lui_item_txt" title="桌面">桌面</div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="lui_menu_item_split"></div>
                                    <div class="lui_menu_item">
                                        <div data-lui-type="lui/menu!MenuItem" style="cursor: pointer;" id="lui-id-10" class="lui-component lui_item" data-lui-cid="lui-id-10" data-lui-parse-init="12" data-lui-switch-class="lui_item_stitch_class">

                                            <div class="lui_item_left">
                                                <div class="lui_item_right">
                                                    <div class="lui_item_content">
                                                        <div class="lui_item_txt" title="报餐管理" onclick="javascript:void(0);">报餐管理</div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div id="lui_validate_message" style="width: 90%; min-width: 980px; max-width: 1200px; margin: 0px auto;"></div>
            <table class="tempTB" style="width: 90%; min-width: 980px; max-width: 1200px; margin: 0px auto;">
                <tbody>
                    <tr>
                        <!-- “左侧” 表单内容展示区TD单元格 -->
                        <td valign="top" class="lui_form_content_td">
                            <div class="lui_form_content">
                                <div data-lui-type="lui/panel!TabPage" style="" id="lui-id-11" class="lui-component" data-lui-cid="lui-id-11" data-lui-parse-init="13">
                                    <div class="lui_tabpage_frame">
                                        <div class="lui_tabpage_float_contents">
                                            <div class="lui_tabpage_float_content">
                                                <div class="lui_tabpage_float_header_l">
                                                    <div class="lui_tabpage_float_header_r">
                                                        <div class="lui_tabpage_float_header_c">
                                                            <div class="lui_tabpage_float_header_title">
                                                                <div class="lui_tabpage_float_header_text">我要报餐</div>
                                                                <div class="lui_tabpage_float_header_close" title="最小化"></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="lui_tabpage_float_content_l">
                                                    <div class="lui_tabpage_float_content_r">
                                                        <div class="lui_tabpage_float_content_c">
                                                            <div>
                                                                <div data-lui-mark="panel.content.inside" class="lui_panel_content_inside">
                                                                    <div id="process_review_tabcontent" data-lui-type="lui/panel!Content" style="" class="lui-component" data-lui-cid="process_review_tabcontent" data-lui-parse-init="25">
                                                                        <!--start 选项卡头部 -->
                                                                        <div class="lui_flowstate_tab_heading">
                                                                            <ul class="lui_flowstate_tabhead">
                                                                                <li name="process_head_tab" data-bind="processflow" class="active" data-isclick="true"><a href="javascript:void(0);">我要报餐</a></li>
                                                                                <li name="process_head_tab" data-bind="processmap" data-load="flow_chart_load_Frame"><a href="javascript:void(0);">报餐记录</a></li>
                                                                            </ul>
                                                                        </div>
                                                                        <!--end 选项卡头部 -->

                                                                        <!-- begin 我要报餐 -->
                                                                        <div name="process_body" data-bind="processflow" class="process_body_checked_true">
                                                                            <div>
                                                                                <table class="tb_normal process_review_panel" width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td class="TitleStyle" colspan="4">
                                                                                                <strong>
                                                                                                    <asp:TextBox ID="txtWorkName" runat="server" Width="53px" Visible="False"></asp:TextBox>
                                                                                                    个人报餐</strong>
                                                                                                <asp:TextBox ID="txtDJtime" runat="server" onfocus="setday(this)" Width="46px"
                                                                                                    Height="20px" Visible="False"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="MainStyle">注意事项：</td>
                                                                                            <td colspan="3" width="85%">
                                                                                                <strong>1、报餐时间：当天晚上24点前可报第二天及以后的早餐及午餐，最长可报1个月。</strong><br />
                                                                                                <strong>2、报餐修改：凡需修改报餐的，直接在本界面修改报餐记录，修改完毕点击“提交”按钮，即可完成。早餐修改最迟在当天早上6点30分完成，午餐修改最迟在当天上午10点30分完成。</strong><br />
                                                                                                <strong>3、报餐提醒：职工报餐结束前两天，系统会发短信提醒。请职工收到短信提醒后，及时登录OA进行报餐。</strong>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 30px;">
                                                                                            <td class="MainStyle">报餐范围：
                                                                                            </td>
                                                                                            <td style="background-color: #ffffff" class="auto-style1" colspan="1">
                                                                                                <asp:RadioButtonList ID="DropDownListBCFW" runat="server" RepeatDirection="Horizontal" CssClass="lui-lbpm-radio" AutoPostBack="True" OnTextChanged="DropDownListBCFW_Changed">
                                                                                                    <asp:ListItem Selected="True">本周</asp:ListItem>
                                                                                                    <asp:ListItem>明天</asp:ListItem>
                                                                                                    <asp:ListItem>下周</asp:ListItem>
                                                                                                    <asp:ListItem>本月</asp:ListItem>
                                                                                                    <asp:ListItem>未来30天</asp:ListItem>
                                                                                                </asp:RadioButtonList>
                                                                                            </td>
                                                                                            <tr style="height: 30px;">
                                                                                                <td class="MainStyle">报餐日期起：<a style="color: #FF0000; font-size: 18px;">*</a>
                                                                                                </td>
                                                                                                <td style="background-color: #ffffff" class="auto-style1" colspan="1">
                                                                                                    <div class="inputdatetime" onclick="javascript:document.getElementById('txtBCRQ_Start').click();"></div>
                                                                                                    <asp:TextBox ID="txtBCRQ_Start" runat="server" class="input_cxcalendar" Width="320px" AutoPostBack="True" OnTextChanged="Time_TextChanged"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorBCRQ" runat="server" ControlToValidate="txtBCRQ_Start" ErrorMessage="*该项不可以为空" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                                </td>

                                                                                                <td class="MainStyle">报餐日期止：<a style="color: #FF0000; font-size: 18px;">*</a>
                                                                                                </td>
                                                                                                <td style="background-color: #ffffff" class="auto-style1" colspan="1">
                                                                                                    <div class="inputdatetime" onclick="javascript:document.getElementById('txtBCRQ_End').click();"></div>
                                                                                                    <asp:TextBox ID="txtBCRQ_End" runat="server" class="input_cxcalendar" Width="320px" AutoPostBack="True" OnTextChanged="Time_TextChanged"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBCRQ_End" ErrorMessage="*该项不可以为空" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                            </tr>
                                                                                        <tr>
                                                                                            <td class="MainStyle">报餐时间点：<a style="color: #FF0000; font-size: 18px;">*</a></td>
                                                                                            <td style="background-color: #ffffff" class="auto-style1" colspan="3">
                                                                                                <div id="_xform_fdProjectZYType" _xform_type="radio">
                                                                                                    <%--<asp:RadioButtonList ID="DropDownListShiJianDian" runat="server" RepeatDirection="Horizontal" CssClass="lui-lbpm-radio" AutoPostBack="True" OnTextChanged="Time_TextChanged">
                                        <asp:ListItem Selected="True">全天</asp:ListItem>
                                        <asp:ListItem>早餐</asp:ListItem>
                                        <asp:ListItem>午餐</asp:ListItem>
                                    </asp:RadioButtonList>--%>
                                                                                                    <asp:CheckBox ID="DropDownListShiJianDian_ZaoCan" runat="server" Text="早餐" Checked="true" onclick="changeSJD('DropDownListShiJianDian_ZaoCan', 'checkbox_zc')" />
                                                                                                    <asp:CheckBox ID="DropDownListShiJianDian_WuCan" runat="server" Text="午餐" Checked="true" onclick="changeSJD('DropDownListShiJianDian_WuCan', 'checkbox_wc')" />
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="4" style="padding: 0px;">
                                                                                                <table id="QingDan" class="tb_normal" style="width: 100%" border="0">
                                                                                                    <tr>
                                                                                                        <td class="TitleStyle" colspan="12" style="text-align: center"><strong>报餐信息调整列表</strong></td>
                                                                                                    </tr>
                                                                                                    <%--<thead>
                                <tr>
                                    <td class="TitleStyle" colspan="12" style="text-align: center"><strong>报餐情况</strong></td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; background-color: #ffffff;" class="auto-style2">序号</td>
                                    
                                    <td style="text-align: center; background-color: #ffffff;" class="auto-style2">报餐日期</td>
                                        
                                    <td style="text-align: center; background-color: #ffffff;" class="auto-style2">时间点</td>

                                    <td style="text-align: center; background-color: #ffffff;" class="auto-style4">
                                        <img src="../images/Button/BtnAdd.jpg" style="border-width: 0px; vertical-align: middle" class="addRow" onclick="ERPBaoCan_AddRow()" />
                                    </td>
                                </tr>
                            </thead>--%>
                                                                                                    <%--<tr id="ZLBodyTr" style="display: none;">
                                <td class="MainStyle">
                                    <span id="ERPBaoCan_BCRQSpan_{Replace}">{Replace}：</span>
                                    <input style="display:none;" id="ERPBaoCan_BCRQ_{Replace}" name="ERPBaoCan_BCRQ_{Replace}" class="com_input" readonly='readonly' type="text" />
                                </td>
                                <td style="background-color: #ffffff" class="auto-style1"  colspan="3">
                                    <div _xform_type="radio">
                                        <%= DropDownListShiJianDian.SelectedItem.Value %>
                                        <input id="ERPBaoCan_ShiJianDian_{Replace}_1" name="ERPBaoCan_ShiJianDian_{Replace}" type="radio" value="全天" <%= DropDownListShiJianDian.SelectedItem.Value == "全天" ? "checked=true" : "" %> /><label for="ERPBaoCan_ShiJianDian_{Replace}_1">全天</label>
                                        <input id="ERPBaoCan_ShiJianDian_{Replace}_2" name="ERPBaoCan_ShiJianDian_{Replace}" type="radio" value="早餐" <%= DropDownListShiJianDian.SelectedItem.Value == "早餐" ? "checked=true" : "" %> /><label for="ERPBaoCan_ShiJianDian_{Replace}_2">早餐</label>
                                        <input id="ERPBaoCan_ShiJianDian_{Replace}_3" name="ERPBaoCan_ShiJianDian_{Replace}" type="radio" value="午餐" <%= DropDownListShiJianDian.SelectedItem.Value == "午餐" ? "checked=true" : "" %> /><label for="ERPBaoCan_ShiJianDian_{Replace}_3">午餐</label>
                                        <input id="ERPBaoCan_ShiJianDian_{Replace}_4" name="ERPBaoCan_ShiJianDian_{Replace}" type="radio" value="不报" /><label for="ERPBaoCan_ShiJianDian_{Replace}_4">不报</label>
                                    </div>
                                </td>

                            </tr>--%>
                                                                                                    <tbody class="ZLBody">
                                                                                                        <% for (var i = 0; i < SelectDateList.Count; i++)
                                                                                                            {
                                                                                                                var SelectDate = SelectDateList[i];
                                                                                                                var CaiDanTuPian = GetCaiDanTuPian(SelectDate);
                                                                                                        %>
                                                                                                        <tr>
                                                                                                            <td class="MainStyle">
                                                                                                                <span style="white-space: nowrap;" id="ERPBaoCan_BCRQSpan_<%= i %>"><%= SelectDate.ToString("yyyy-MM-dd") %>(<%= GetDayOfWeek(SelectDate) %>)：</span>
                                                                                                                <input value="<%= SelectDate.ToString("yyyy-MM-dd") %>" style="display: none;" id="ERPBaoCan_BCRQ_<%= i %>" name="ERPBaoCan_BCRQ_<%= i %>" class="com_input" readonly='readonly' type="text" />
                                                                                                            </td>
                                                                                                            <td style="background-color: #ffffff" class="auto-style1" colspan="3">
                                                                                                                <div _xform_type="radio">
                                                                                                                    <% if (DateTime.Now.ToString("yyyy-MM-dd") == SelectDate.ToString("yyyy-MM-dd"))
                                                                                                                        { %>
                                                                                                                    <% if (DateTime.Now < DateTime.Now.Date.AddHours(6.5))%>
                                                                                                                    <% {  %>
                                                                                                                    <input id="ERPBaoCan_ShiJianDian_ZaoCan_<%= i %>" name="ERPBaoCan_ShiJianDian_ZaoCan_<%= i %>" type="checkbox" value="早餐" class="checkbox_zc" <%= IsChecked(SelectDate, "早餐") %> />
                                                                                                                    <label for="ERPBaoCan_ShiJianDian_ZaoCan_<%= i %>">早餐</label>
                                                                                                                    &nbsp;
                                                    <%} %>
                                                                                                                    <% if (DateTime.Now < DateTime.Now.Date.AddHours(10.5))%>
                                                                                                                    <% {  %>
                                                                                                                    <input id="ERPBaoCan_ShiJianDian_WuCan_<%= i %>" name="ERPBaoCan_ShiJianDian_WuCan_<%= i %>" type="checkbox" value="午餐" class="checkbox_wc" <%= IsChecked(SelectDate, "午餐") %> />
                                                                                                                    <label for="ERPBaoCan_ShiJianDian_WuCan_<%= i %>">午餐</label>
                                                                                                                    <%} %>
                                                                                                                    <% }
                                                                                                                        else if (DateTime.Now < SelectDate)
                                                                                                                        { %>
                                                                                                                    <input id="ERPBaoCan_ShiJianDian_ZaoCan_<%= i %>" name="ERPBaoCan_ShiJianDian_ZaoCan_<%= i %>" type="checkbox" value="早餐" class="checkbox_zc" <%= IsChecked(SelectDate, "早餐") %> />
                                                                                                                    <label for="ERPBaoCan_ShiJianDian_ZaoCan_<%= i %>">早餐</label>
                                                                                                                    &nbsp;
                                                <input id="ERPBaoCan_ShiJianDian_WuCan_<%= i %>" name="ERPBaoCan_ShiJianDian_WuCan_<%= i %>" type="checkbox" value="午餐" class="checkbox_wc" <%= IsChecked(SelectDate, "午餐") %> />
                                                                                                                    <label for="ERPBaoCan_ShiJianDian_WuCan_<%= i %>">午餐</label>
                                                                                                                    <% } %>


                                                                                                                    <%--<input id="ERPBaoCan_ShiJianDian_<%= i %>_1" name="ERPBaoCan_ShiJianDian_<%= i %>" type="radio" value="全天" <%= DropDownListShiJianDian.SelectedItem.Value == "全天" ? "checked=true" : "" %>/><label for="ERPBaoCan_ShiJianDian_<%= i %>_1">全天</label>
                                            <input id="ERPBaoCan_ShiJianDian_<%= i %>_2" name="ERPBaoCan_ShiJianDian_<%= i %>" type="radio" value="早餐" <%= DropDownListShiJianDian.SelectedItem.Value == "早餐" ? "checked=true" : "" %>/><label for="ERPBaoCan_ShiJianDian_<%= i %>_2">早餐</label>
                                            <input id="ERPBaoCan_ShiJianDian_<%= i %>_3" name="ERPBaoCan_ShiJianDian_<%= i %>" type="radio" value="午餐" <%= DropDownListShiJianDian.SelectedItem.Value == "午餐" ? "checked=true" : "" %>/><label for="ERPBaoCan_ShiJianDian_<%= i %>_3">午餐</label>
                                            <input id="ERPBaoCan_ShiJianDian_<%= i %>_4" name="ERPBaoCan_ShiJianDian_<%= i %>" type="radio" value="不报" /><label for="ERPBaoCan_ShiJianDian_<%= i %>_4">不报</label>--%>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                            <%--<td style="background-color: #ffffff" class="auto-style1"  colspan="3">
                                        <% if (CaiDanTuPian != "") { %>
                                        <a href="<%= "../UploadFile/" + CaiDanTuPian %>" target="_blank">当周菜单<span style="color:red;">[点击打开]</span></a>
                                        <% }else{ %>
                                        <% } %>
                                    </td>--%>
                                                                                                        </tr>
                                                                                                        <%} %>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td class="MainStyle">菜单展示：</td>
                                                                                            <td style="background-color: #ffffff" class="auto-style1" colspan="3">
                                                                                                <div>
                                                                                                    <asp:HiddenField ID="CaiDanImage" runat="server" />
                                                                                                    <% if (CaiDanImage.Value == "")
                                                                                                        { %>
                                        暂无菜单
                                    <% }
                                        else
                                        {%>
                                                                                                    <img style="max-width: 1000px; max-height: 600px;" src="../UploadFile/<%= CaiDanImage.Value %>" />
                                                                                                    <% } %>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>

                                                                            </div>
                                                                        </div>
                                                                        <!--end 我要报餐 -->

                                                                        <!-- begin报餐记录 -->
                                                                        <div name="process_body" data-bind="processmap" class="process_body_checked_false">
                                                                            <iframe width="100%" height="100%" scrolling="yes" frameborder="0" id="WF_IFrame" src="" data-src='../BaoCan/ERPBaoCanAddManager.aspx' style="z-index: 9999; height: 484px;"></iframe>

                                                                        </div>
                                                                        <!-- end报餐记录 -->

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <iframe frameborder="0" class="lui_tabpage_float_navs_mark" scrolling="no"></iframe>
                                            <div class="lui_tabpage_float_navs">
                                                <div class="lui_tabpage_float_navs_l">
                                                    <div class="lui_tabpage_float_navs_r">
                                                        <div class="lui_tabpage_float_navs_c" style="max-width: 1200px; text-align: center; padding-top: 5px; padding-bottom: 5px; height: 40px;">
                                                            <div id="lui-id-175" class="lui-component lui_widget_btn lui_toolbar_btn_def" data-lui-cid="lui-id-170" title="提交" onclick="javascript:doSubmitData();">
                                                                <div class="lui_toolbar_btn_l lui_toolbar_m" data-lui-mark="toolbar_button_inner" style="text-align: center; height: 33px; line-height: 33px;">
                                                                    <div class="lui_toolbar_btn_r">
                                                                        <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                            <div id="lui-id-176" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-171">提交</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="lui-id-183" class="lui-component lui_widget_btn lui_toolbar_btn_def" data-lui-cid="lui-id-170" title="返回" onclick="javascript:gobacktolist();">
                                                                <div class="lui_toolbar_btn_l lui_toolbar_m" data-lui-mark="toolbar_button_inner" style="text-align: center; height: 33px; line-height: 33px;">
                                                                    <div class="lui_toolbar_btn_r">
                                                                        <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                            <div id="lui-id-184" class="lui-component lui_widget_btn_txt btnGoback" data-lui-cid="lui-id-171">返回</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--<div class="lui_tabpage_float_collapse" title="收起"><a class="txt">收起</a></div>--%>
                                        </div>
                                    </div>

                                </div>
                        </td>
                        <!-- “右侧” 侧边栏展示区TD单元格 -->
                        <td valign="top" style="width: 30%; display: none;" class="lui_form_sidebar_td">
                            <div style="padding-left: 15px;" class="lui_form_sidebar">
                            </div>
                        </td>

                    </tr>
                </tbody>
            </table>
            <div id="top" data-lui-type="lui/top!totop" style="" class="lui-component com_goto" data-lui-cid="top" data-lui-parse-init="29">
                <div class="com_gototop" style="display: none;"></div>
            </div>
        </div>
    </form>
</body>
</html>

