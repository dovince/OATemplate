<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoWorkNew.aspx.cs" Inherits="NWorkFlow_DoWorkNew" EnableViewState="true" ValidateRequest="false" %>

<%@ Import Namespace="System.Linq" %>

<%@ Register Src="~/UserControls/RebuildForm.ascx" TagPrefix="uc1" TagName="RebuildForm" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title><%= ZWL.Common.PublicMethod.GetSysTitle()%></title>
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
    <link href="../CSS/common/widget.theme.css" rel="stylesheet" />
    <link href="../CSS/common/process_tab_main.css" rel="stylesheet" />

    <link href="../CSS/default/easyui.css" rel="stylesheet" />
    <%--<link href="../CSS/common/webmain.css" rel="stylesheet" />
    <link href="../CSS/common/bootstrap_yeti.css" rel="stylesheet" />
    <link href="../CSS/common/css.css" rel="stylesheet" />--%>
    <link href="../CSS/common/form.theme.css" rel="stylesheet" />
    <link href="../CSS/common/dialog.theme.css" rel="stylesheet" />
    <link href="../CSS/common/upload.css" rel="stylesheet" />
    <link href="../CSS/Loading.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-1.11.2.min.js" type="text/javascript"></script>
    <script src="../JS/jquery.easyui.min.js"></script>
    <script src="../JS/jquery.blockUI.js" type="text/javascript"></script>
    <%--<script src="../JS/common/indexpublic.js" type="text/javascript"></script>
    <script src="../JS/common/js.js" type="text/javascript"></script>--%>
    <script src="../JS/common.js?v=202508020823" type="text/javascript"></script>
    <script src="../JS/lui/tabcontent.js" type="text/javascript"></script>
    <script src="../JS/lui/dowork.js?v=202305171241" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            <% if (JieDianName == "合同归档" && FormId.ToString() == "43")
        { %>
            $("#Drop1321863117").removeAttr("disabled");
            $("#Drop223660142").removeAttr("disabled");
            $("#Drop1192318942").removeAttr("disabled");
            $("#Drop1800778345").removeAttr("disabled");
            <%} %>
            <% if (JieDianName == "领导审批" && FormId.ToString() == "78" && UserName != "蔡晓帆")
        {%>
            if ($("#Text88422759").val() == "飞机") {
                $("#Text88422759").hide();
            }
            <%} %>
            <% else if (FormId.ToString() == "78")
        {%>
            $("#Text88422759").show();
            <%} %>
            <% if (JieDianName == "经营科核定" && FormId.ToString() == "60")
        {//控制是否可以使用保存并结束 %>
            $(".saveendBtntd").show();
            $(".savepassBtntd").hide();
            <%} %>
            <% if (FormId.ToString() == "46")
        {//合同收款管理 %>
            $(".receiptBtntd").show();
            if ($("#btnsaverow").length > 0)
                $("#btnsaverow").hide();
            if ($("#btnadddzje").length > 0)
                $("#btnadddzje").hide();
                <% if (JieDianName == "财务科出纳")
        { %>
            viewenddzje();
                <%} %>
            <%} %>
            <%if (!string.IsNullOrEmpty(EditModelUrl))%>
            <%{%>
            var editModel = <%=EditModelUrl%>;
            if (editModel != '') {
                var data = eval(editModel);
                if (data.url != '') {
                    $(".editBtntd").show();
                }
            }
            <%}%>
            <% if (FormId == 110 || FormId == 111)%>
            <%{ %>
            intHeight = 200;
            $(".ReplaceComplate").remove();
            showInput("JYLY");
            for (var i = 1; i < 30; i++) {
                if (!showInput("ERPSMZLCGJieYueDtl_ZiLiaoMingCheng_" + i)) {
                    //break;
                }
            }
            for (var i = 1; i < 30; i++) {
                if (!showInput("ERPSMZLCGJieYueDtl_DangAnHao_" + i)) {
                    //break;
                }
            }

            <%} %>

            <%if (FormId == 43)%>
            <%{ %>
            if ($("#RebuildForm_Label_FormContent").html().indexOf("合同签订评审") > 0) {
                var tdlist = $("#RebuildForm_Label_FormContent td");
                for (var i = 0; i < tdlist.length; i++) {
                    var item = tdlist[i];
                    if ($(item).text() == "合同签订日期" || $(item).text() == "合同截止时间") {
                        $(item).css("word-break", "keep-all");
                    }
                }
            }
            <%} %>
            setInputStyle(<%=FormId%>);
            <%if (FormId == 48 && JieDianName.Contains("传阅"))%>
            <%{ %>
            forwardInput();
            <%} %>
        });
        function opensubwin() {
            var url = '';
                <% if (!string.IsNullOrEmpty(EditModelUrl))%>
                <%{%>
            url =<%=EditModelUrl%>;
                <%}%>
            if (url != '') {
                var data = eval(url);
                if (data.win == "window") {
                    window.open(data.url + "&target=blank");
                }
                else {
                    self.location = data.url + "&target=self";
                }
            }
        }
        function showInput(ipid) {
            //if ($(".ReplaceComplate").length > 0) {
            //    return true;
            //}
            var jqobj = $("input[name='" + ipid + "']");
            if (jqobj.length > 0 && jqobj.val().length > 0) {
                jqobj.after("<span class='ReplaceComplate'>" + jqobj.val() + "</span>");
                //jqobj.html("<div style='display:none;'>" + jqobj.html() + "</div>")
                jqobj.hide();
                return true;
            }
            else {
                return false;
            }
        }
        function setInputStyle(strformid) {
            //对于不同的表单做相应的修改,因为制作表单时名称不统一            
            if (strformid == "43" || strformid == "46" || strformid == "44" || strformid == "45") {
                //对于合同签订评审表单，对合同名称和项目名称做处理
                var htname = $("input[alt='合同名称']")[0].value.toString();
                if (htname != "" && !$("input[alt='合同名称']").next().is("p")) {
                    var strhtname = "<p disabled='disabled'>" + htname + "</p>";
                    var htp = $(strhtname);
                    $("input[alt='合同名称']").after(htp).hide();
                }
                if ($("input[alt='项目名称']").length > 0 && !$("input[alt='项目名称']").next().is("p")) {
                    var xmname = $("input[alt='项目名称']")[0].value.toString();
                    if (xmname != "") {
                        $("input[alt='项目名称']").parent().removeAttr("width");//   对于合同签订评审表中的项目名称，需要取消上级元素td的width属性才能显示正常。
                        var strxmname = "<p disabled='disabled'>" + xmname + "</p>";
                        var xmp = $(strxmname);
                        $("input[alt='项目名称']").after(xmp).hide();
                    }
                }
                if (strformid == "46") {
                    //合同收款审批
                    $("td:contains('合同编号')").css("width", "125");
                }
                $("input[alt='合同编号']").parent().css("valign", "center");
            }
            else {
                if ($("input[alt='项目名称']").length > 0 && !$("input[alt='项目名称']").next().is("p")) {//项目管理--项目基本信息
                    var xmname = $("input[alt='项目名称']")[0].value.toString();
                    if (xmname != "") {
                        $("input[alt='项目名称']").parent().removeAttr("width");
                        var strxmname = "<p disabled='disabled'>" + xmname + "</p>";
                        var xmp = $(strxmname);
                        $("input[alt='项目名称']").after(xmp);
                        $("input[alt='项目名称']").hide();
                    }
                }
            }
            if (strformid == "39" || strformid == "42" || strformid == "41" || strformid == "40") {//投标项目审批或者投标总结
                //投标项目名称
                var tbname = $("input[alt='投标项目名称']")[0].value.toString();
                if (tbname != "" && !$("input[alt='投标项目名称']").next().is("p")) {
                    $("input[alt='投标项目名称']").parent().removeAttr("width");
                    var strtbname = "<p  disabled='disabled'>" + tbname + "</p>";
                    var tbp = $(strtbname);
                    $("input[alt='投标项目名称']").after(tbp).hide();
                }
            }
        }
        function submit_client_ex() {
            if (validateEx()) {
                //AddValues2LabelContent();
                $("#Hidden_form").val($("#RebuildForm_Label_FormContent").prop("innerHTML"));
                var fdUsageContent = $("#fdUsageContent").val();
                if (fdUsageContent == "") {
                    var text = "同意";
                    if ($("#SelectedNextNode option:selected").text().indexOf('传阅') > -1) {
                        text = "已阅";
                    }
                    $("#fdUsageContent").val(text);
                }
                showLoading();
                // 添加页面加载完成的事件处理函数
                window.onload = function () {
                    hideLoading();
                };

                document.getElementById('btnSavePass').click();
                return true;
            } else {
                return false;
            }
        }
        function validateEx() {
            var result = validate();
            if (result) {
                var formTitle = $("#RebuildForm_lblFormTitle").text();
                var formid =<%=FormId%>;
                var jiedianname ='<%=JieDianName%>';
                if ((formid == 43 || formid == 39) && jiedianname == "经营科负责人审核") {
                    var checked = true;
                    if (formid == 43) {
                        var htlb = $("#Text863179841").val();
                        if (htlb != "收款") {
                            checked = false;
                        }
                    }
                    if (checked) {
                        var msg = $("#lui_validate_message p").text();
                        if (msg.indexOf("无【项目承接决策评估登记】") >= 0 || msg.indexOf("未关联【项目承接决策评估登记】") >= 0 || msg.indexOf("【项目承接决策评估登记】正在办理中") >= 0) {
                            if (confirm(msg + ",跳过验证继续办理吗？")) {
                                result = true;
                            }
                            else {
                                result = false;
                                hideLoading();
                            }
                        }
                    }
                }
                else if (formTitle.indexOf("盖章") >= 0 && jiedianname.indexOf("经营") >= 0) {
                    var msg = $("#lui_validate_message div").text();
                    if (msg != "" && msg.indexOf("此项目已暂被列入黑名单") >= 0) {
                        if (confirm("注意：此项目已暂被列入黑名单，确认跳过继续办理吗？")) {
                            result = true;
                        }
                        else {
                            result = false;
                            hideLoading();
                        }
                    }
                }
            }
            return result;
        }
        function forwardInput() {
            $(".forwardBtntd").show();
            $(".returnBtntd").hide();
            $(".rejectBtntd").hide();
            $(".editBtntd").hide();
            $(".passedBtnText").html("已阅");
            document.getElementById('fdUsageContent').value = "已阅";
            document.getElementById('commonUsages').value = "已阅";
            var shenpiusers = $("#ShenPiUser").val();
            if ($("#SelectedNextNode").text().indexOf('传阅') > 0 && shenpiusers != "") {
                $("#ShenPiUser").attr("readOnly", "readOnly");
                $("div[class*='inputselectsgl'] input[id='ShenPiUser']").removeAttr("onclick");
                $("div[class*='inputselectsgl'] div[class='orgelement']").removeAttr("onclick");
            }
        }
        function doforward() {
            var id = getUrlParms("ID");
            var content = $("#fdUsageContent").val();
            var url = '../CommonSelect/FlowForward.aspx?ID=' + id + '&_=' + Math.random()
            var options = {
                title: "转发并阅办",
                url: url,
                width: 800,
                height: 500,
                data: content,
                onFinish: function (returnVal) {
                    //if (returnVal != null && returnVal != '') {
                    //    if (returnVal.indexOf("成功") > -1) {
                    //        alert("操作成功。");
                    //        $("#backBtn").click();
                    //    }
                    //    else {
                    //        alert(returnVal);
                    //    }
                    //}
                }
            }
            showPopwindow('LinkProject', options);
        }
        function openuserDialogExt(utype) {
            var shenpiuser = $("#" + utype).val();
            var nextnode = $("#SelectedNextNode").val();
            var psmoshi = $("#PingshenMoshi").val();
            var spmoshi = $("#ShenpiMoshi").val();
            if (shenpiuser != "" && shenpiuser == "默认" && nextnode == "0"
                && psmoshi == "默认" && spmoshi == "默认") {
            }
            else {
                openuserDialog(utype);
            }
        }
    </script>

    <style type="text/css">
        #RebuildForm_Label_FormContent input[id^='Text'], select {
            height: 22px;
            border: 0 !important;
        }

        #RebuildForm_Label_FormContent input[id^='Date'] {
            height: 22px;
            width: 120px;
            border: 0 !important;
        }

        #RebuildForm_Label_FormContent textarea {
            border: 0 !important;
        }

        .selectTdClass {
            background-color: #edf5fa !important
        }

        #RebuildForm_Label_FormContent table.noBorderTable td, table.noBorderTable th, table.noBorderTable caption {
            border: 1px dashed #ddd !important
        }

        #RebuildForm_Label_FormContent table {
            margin-bottom: 10px;
            border-collapse: collapse;
            display: table;
        }

        #RebuildForm_Label_FormContent td, th {
            background: white;
            padding: 5px 5px;
            border: 1px solid #DDD;
        }

        #RebuildForm_Label_FormContent caption {
            border: 1px dashed #DDD;
            border-bottom: 0;
            padding: 3px;
            text-align: center;
        }

        #RebuildForm_Label_FormContent th {
            border-top: 2px solid #BBB;
            background: #F7F7F7;
        }

        #RebuildForm_Label_FormContent td p {
            margin: 0;
            padding: 0;
        }
    </style>
</head>
<body class="lui_form_body " style="margin-top: 43px; margin-bottom: 46px;" onload="Load_Do();">
    <form id="form1" runat="server">
        <div style="display: none;">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </div>
        <div>
            <div id="toolbar" data-lui-type="lui/toolbar!ToolBar" style="" class="lui-component" data-lui-cid="toolbar" data-lui-parse-init="1">
                <div>
                    <iframe frameborder="0" class="lui_toolbar_frame_float_mark" scrolling="no"></iframe>
                    <div class="lui_toolbar_frame_float">
                        <div class="lui_toolbar_left">
                            <div class="lui_toolbar_right">
                                <div class="lui_toolbar_content" style="max-width: 1200px;">
                                    <table style="margin: 0px auto;">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <div class="lui_toolbar_btn" data-lui-on-class="lui_toolbar_btn_on" data-lui-status-class="lui_toolbar_btn_toggle_on">
                                                        <div id="refreshBtn" data-lui-type="lui/toolbar!Button" data-lui-parentid="toolbar" style="" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="refreshBtn" data-lui-parse-init="7" title="刷新表单" tabindex="0" onclick="javascript:window.location=window.location;">
                                                            <div class="lui_toolbar_btn_l" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                                                <div class="lui_toolbar_btn_r">
                                                                    <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                        <div id="lui-id-21" class="lui-component lui_widget_btn_txt btnRefreshForm" data-lui-cid="lui-id-21">刷新</div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="lui_toolbar_btn" data-lui-on-class="lui_toolbar_btn_on" data-lui-status-class="lui_toolbar_btn_toggle_on">
                                                        <div id="bookedBtn" data-lui-type="lui/toolbar!Button" data-lui-parentid="toolbar" style="" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="bookedBtn" data-lui-parse-init="7" title="原始表单" tabindex="0" onclick="javascript:void(0);">
                                                            <div class="lui_toolbar_btn_l" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                                                <div class="lui_toolbar_btn_r">
                                                                    <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                        <div id="lui-id-30" class="lui-component lui_widget_btn_txt btnOriginalForm" data-lui-cid="lui-id-30">原表单</div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class="savepassBtntd">
                                                    <div class="lui_toolbar_btn" data-lui-on-class="lui_toolbar_btn_on" data-lui-status-class="lui_toolbar_btn_toggle_on">
                                                        <div id="passedBtn" data-lui-type="lui/toolbar!Button" data-lui-parentid="toolbar" style="" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="bookedBtn" data-lui-parse-init="7" title="保存并通过" tabindex="0" onclick="javascript:return submit_client_ex();">
                                                            <div class="lui_toolbar_btn_l" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                                                <div class="lui_toolbar_btn_r">
                                                                    <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                        <div id="lui-id-31" class="lui-component lui_widget_btn_txt passedBtnText" data-lui-cid="lui-id-31">保存并通过</div>
                                                                        <asp:Button ID="btnSavePass" runat="server" Text="保存并通过" OnClick="btnSavePass_Click" Style="display: none;" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class="forwardBtntd" style="display: none;">
                                                    <div class="lui_toolbar_btn" data-lui-on-class="lui_toolbar_btn_on" data-lui-status-class="lui_toolbar_btn_toggle_on">
                                                        <div id="forwardBtn" data-lui-type="lui/toolbar!Button" data-lui-parentid="toolbar" style="" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="bookedBtn" data-lui-parse-init="7" title="转发并阅办" tabindex="0" onclick="javascript:return doforward();">
                                                            <div class="lui_toolbar_btn_l" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                                                <div class="lui_toolbar_btn_r">
                                                                    <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                        <div id="lui-id-32" class="lui-component lui_widget_btn_txt forwardBtnText" data-lui-cid="lui-id-31">转发并阅办</div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class="saveendBtntd" style="display: none;">
                                                    <div class="lui_toolbar_btn" data-lui-on-class="lui_toolbar_btn_on" data-lui-status-class="lui_toolbar_btn_toggle_on">
                                                        <div id="saveendBtn" data-lui-type="lui/toolbar!Button" data-lui-parentid="toolbar" style="" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="bookedBtn" data-lui-parse-init="7" title="保存并结束" tabindex="0" onclick="javascript:return submit_client_end();">
                                                            <div class="lui_toolbar_btn_l" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                                                <div class="lui_toolbar_btn_r">
                                                                    <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                        <div id="lui-id-38" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-31">保存并结束</div>
                                                                        <asp:Button ID="btnSaveEnd" runat="server" Text="保存并结束" OnClick="btnSaveEnd_Click" Style="display: none;" CausesValidation="false" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class="editBtntd" style="display: none;">
                                                    <div class="lui_toolbar_btn" data-lui-on-class="lui_toolbar_btn_on" data-lui-status-class="lui_toolbar_btn_toggle_on">
                                                        <div id="editBtn" data-lui-type="lui/toolbar!Button" data-lui-parentid="toolbar" style="" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="bookedBtn" data-lui-parse-init="7" title="修改" tabindex="0" onclick="javascript:opensubwin();">
                                                            <div class="lui_toolbar_btn_l" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                                                <div class="lui_toolbar_btn_r">
                                                                    <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                        <div id="lui-id-45" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-31">修改</div>
                                                                        <%--<asp:Button ID="BtnEditModel" runat="server" Text="修改" OnClick="BtnEditModel_Click" OnClientClick="javascript:return CheckModify();" Style="display: none;" CausesValidation="false"/>--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class="returnBtntd" style="">
                                                    <div class="lui_toolbar_btn" data-lui-on-class="lui_toolbar_btn_on" data-lui-status-class="lui_toolbar_btn_toggle_on">
                                                        <div id="returnedBtn" data-lui-type="lui/toolbar!Button" data-lui-parentid="toolbar" style="" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="returnedBtn" data-lui-parse-init="7" title="驳回" tabindex="0" onclick="javascript:document.getElementById('btnReturn').click();">
                                                            <div class="lui_toolbar_btn_l" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                                                <div class="lui_toolbar_btn_r">
                                                                    <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                        <div id="lui-id-2" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-31">驳回</div>
                                                                        <asp:Button ID="btnReturn" runat="server" Text="驳回" OnClick="btnReturn_Click" OnClientClick="return validateReject();" Style="display: none;" CausesValidation="false" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class="rejectBtntd" style="">
                                                    <div class="lui_toolbar_btn" data-lui-on-class="lui_toolbar_btn_on" data-lui-status-class="lui_toolbar_btn_toggle_on">
                                                        <div id="rejectedBtn" data-lui-type="lui/toolbar!Button" data-lui-parentid="toolbar" style="" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="rejectedBtn" data-lui-parse-init="7" title="不通过" tabindex="0" onclick="javascript:document.getElementById('btnReject').click();">
                                                            <div class="lui_toolbar_btn_l" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                                                <div class="lui_toolbar_btn_r">
                                                                    <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                        <div id="lui-id-34" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-31">不通过</div>
                                                                        <asp:Button ID="btnReject" runat="server" Text="不通过" OnClick="btnReject_Click" OnClientClick="return validateReject()" Style="display: none;" CausesValidation="false" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class="receiptBtntd" style="display: none;">
                                                    <div class="lui_toolbar_btn" data-lui-on-class="lui_toolbar_btn_on" data-lui-status-class="lui_toolbar_btn_toggle_on">
                                                        <div id="shouzhangBtn" data-lui-type="lui/toolbar!Button" data-lui-parentid="toolbar" style="" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="receiptBtn" data-lui-parse-init="7" title="收账金额管理" tabindex="0" onclick="javascript:addnewtrdzje();">
                                                            <div class="lui_toolbar_btn_l" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                                                <div class="lui_toolbar_btn_r">
                                                                    <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                        <div id="lui-id-35" class="lui-component lui_widget_btn_txt btnReceipt" data-lui-cid="lui-id-31">收账金额管理</div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="lui_toolbar_btn" data-lui-on-class="lui_toolbar_btn_on" data-lui-status-class="lui_toolbar_btn_toggle_on">
                                                        <div id="backBtn" data-lui-type="lui/toolbar!Button" style="" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="backBtn" data-lui-parse-init="3" title="返回" tabindex="0" onclick="javascript:goback(<%=PostBackCount %>);">
                                                            <div class="lui_toolbar_btn_l" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                                                <div class="lui_toolbar_btn_r">
                                                                    <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                        <div id="lui-id-26" class="lui-component lui_widget_btn_txt btnGoback" data-lui-cid="lui-id-26">返回</div>
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
                                                        <div class="lui_item_txt" title="办理工作" onclick="javascript:void(0);">办理工作</div>
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
            <div id="lui_validate_message" style="width: 90%; min-width: 980px; max-width: 1200px; margin: 0px auto;">
                <div style="font-size: 18px;">
                    <%=MessageHtml %>
                </div>
            </div>
            <table class="tempTB" style="width: 90%; min-width: 980px; max-width: 1200px; margin: 0px auto;">
                <tbody>
                    <tr>
                        <!-- “左侧” 表单内容展示区TD单元格 -->
                        <td valign="top" class="lui_form_content_td">
                            <div class="lui_form_content">
                                <div data-lui-type="lui/panel!TabPage" style="" id="lui-id-11" class="lui-component" data-lui-cid="lui-id-11" data-lui-parse-init="13">
                                    <div class="lui_tabpage_frame">
                                        <div class="lui_tabpage_float_contents">
                                            <%--基本信息 开始--%>
                                            <div class="lui_tabpage_float_content" style="">
                                                <div class="lui_tabpage_float_header_l">
                                                    <div class="lui_tabpage_float_header_r">
                                                        <div class="lui_tabpage_float_header_c">
                                                            <div class="lui_tabpage_float_header_title">
                                                                <div class="lui_tabpage_float_header_text">基本信息</div>
                                                                <div class="lui_tabpage_float_header_close" title="最小化"></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="lui_tabpage_float_content_l">
                                                    <div class="lui_tabpage_float_content_r">
                                                        <div class="lui_tabpage_float_content_c">
                                                            <uc1:RebuildForm runat="server" ID="RebuildForm" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="lui_tabpage_float_footer_l">
                                                    <div class="lui_tabpage_float_footer_r">
                                                        <div class="lui_tabpage_float_footer_c"></div>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--基本信息 结束--%>
                                            <%--正文文件 开始--%>

                                            <%if (FormId == 49)%>
                                            <% { %>
                                            <div class="lui_tabpage_float_header_l">
                                                <div class="lui_tabpage_float_header_r">
                                                    <div class="lui_tabpage_float_header_c">
                                                        <div class="lui_tabpage_float_header_title">
                                                            <div class="lui_tabpage_float_header_text">正文文件</div>
                                                            <div class="lui_tabpage_float_header_close" title="最小化"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <iframe frameborder="0" style="width: 100%;" src="UploadForm.aspx?ID=<%= ZWL.Common.PublicMethod.GetDecryptParam("ID") %>"></iframe>
                                            <% } %>
                                            <%--正文文件 结束--%>
                                            <%--附件列表 开始--%>
                                            <%if (Files.Any())%>
                                            <% { %>
                                            <div class="lui_tabpage_float_content" style="">
                                                <div class="lui_tabpage_float_header_l">
                                                    <div class="lui_tabpage_float_header_r">
                                                        <div class="lui_tabpage_float_header_c">
                                                            <div class="lui_tabpage_float_header_title">
                                                                <div class="lui_tabpage_float_header_text">附件列表</div>
                                                                <div class="lui_tabpage_float_header_close" title="最小化"></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="lui_tabpage_float_content_l">
                                                    <div class="lui_tabpage_float_content_r">
                                                        <div class="lui_tabpage_float_content_c">
                                                            <div data-lui-mark="panel.content.inside" class="lui_panel_content_inside">
                                                                <div data-lui-type="lui/panel!Content" id="lui-id-13" class="lui-component" data-lui-cid="lui-id-13" data-lui-parse-init="15">
                                                                    <div class="lui_form_title_frame" data-index="1" data-title="附件列表">
                                                                        <table class="tb_normal treeTable" width="100%" style="text-align: center;">
                                                                            <tbody>
                                                                                <%if (Files.Any())
                                                                                    { %>
                                                                                <tr class="tr_normal initialized">
                                                                                    <td width="6%" class="td_normal_title">序号
                                                                                    </td>
                                                                                    <td width="64%" class="td_normal_title">文件名
                                                                                    </td>
                                                                                    <td width="20%" class="td_normal_title">操作
                                                                                    </td>
                                                                                    <td width="10%" class="td_normal_title">文件大小
                                                                                    </td>
                                                                                </tr>
                                                                                <%for (int i = 0; i < Files.Count; i++)%>                                                                                <%{
                                                                                                                                                                                                                 var item = Files[i];
                                                                                                                                                                                                                 var downUrl = "../DsoFramer/DownLoadFile.aspx?f=../UpLoadFile/" + item.NowName + "&n=" + item.OldName;
                                                                                                                                                                                                                 var readUrl = "../FlexPaperFlash/SWFShow.aspx?f=" + item.NowName + "&n=" + item.OldName;
                                                                                %>
                                                                                <tr>
                                                                                    <td class="td_normal_title">
                                                                                        <%=(i+1) %>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <div class="upload_list_filename_view" title="<%=item.OldName %>">
                                                                                            <span class="upload_list_filename_title" style="max-width: 584px;">
                                                                                                <a target="_blank" href="<%=downUrl %>"><%=item.OldName %></a>
                                                                                            </span>
                                                                                            <span class="upload_list_filename_ext"></span>
                                                                                        </div>
                                                                                    </td>
                                                                                    <td class="td_normal_title">
                                                                                        <div class="upload_list_operation">
                                                                                            <div class="" title="下载" onclick="javascript:window.open('<%=downUrl %>');">
                                                                                                <span style="text-decoration: underline; color: blue; display: inline-block; cursor: pointer;">下载</span>
                                                                                            </div>
                                                                                            <div class="" title="阅读" onclick="javascript:window.open('<%=readUrl %>');">
                                                                                                <span style="text-decoration: underline; color: blue; display: inline-block; cursor: pointer;">阅读</span>
                                                                                            </div>
                                                                                        </div>
                                                                                    </td>
                                                                                    <td class="td_normal_title">
                                                                                        <%=ZWL.Common.PublicMethod.GetFileSize(item.NowName) %>
                                                                                    </td>
                                                                                </tr>
                                                                                <%} %>                                                                                <%}
                                                                                                                                                                          else
                                                                                                                                                                          { %>
                                                                                <tr class="tr_normal initialized">
                                                                                    <td colspan="4">无附件!</td>
                                                                                </tr>
                                                                                <%} %>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="lui_tabpage_float_footer_l">
                                                        <div class="lui_tabpage_float_footer_r">
                                                            <div class="lui_tabpage_float_footer_c"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <% } %>
                                            <%--附件列表 结束--%>


                                            <%--审批记录 开始--%>
                                            <div class="lui_tabpage_float_content" style="">
                                                <div class="lui_tabpage_float_header_l">
                                                    <div class="lui_tabpage_float_header_r">
                                                        <div class="lui_tabpage_float_header_c">
                                                            <div class="lui_tabpage_float_header_title">
                                                                <div class="lui_tabpage_float_header_text">审批记录</div>
                                                                <div class="lui_tabpage_float_header_close" title="最小化"></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="lui_tabpage_float_content_l">
                                                    <div class="lui_tabpage_float_content_r">
                                                        <div class="lui_tabpage_float_content_c">
                                                            <div data-lui-mark="panel.content.inside" class="lui_panel_content_inside">
                                                                <div data-lui-type="lui/panel!Content" id="lui-id-13" class="lui-component" data-lui-cid="lui-id-13" data-lui-parse-init="15">
                                                                    <div class="lui_form_title_frame" data-index="1" data-title="审批记录">
                                                                        <iframe width="100%" height="100%" scrolling="yes" frameborder="0" id="SP_IFrame"
                                                                            src='../NWorkFlow/NWorkFlowLog.aspx?ID=<%=ZWL.Common.PublicMethod.CheckInt(ZWL.Common.PublicMethod.GetDecryptParam("ID")) %>' style="min-height: 50px;"></iframe>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="lui_tabpage_float_footer_l">
                                                        <div class="lui_tabpage_float_footer_r">
                                                            <div class="lui_tabpage_float_footer_c"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--审批记录 结束--%>

                                            <%--流程信息 开始--%>
                                            <div class="lui_tabpage_float_content">
                                                <div class="lui_tabpage_float_header_l">
                                                    <div class="lui_tabpage_float_header_r">
                                                        <div class="lui_tabpage_float_header_c">
                                                            <div class="lui_tabpage_float_header_title">
                                                                <div class="lui_tabpage_float_header_text">流程信息</div>
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
                                                                                <li name="process_head_tab" data-bind="processflow" class="active" data-isclick="true"><a href="javascript:void(0);">流程处理</a></li>
                                                                                <li name="process_head_tab" data-bind="processstate"><a href="javascript:void(0);">流程状态</a></li>
                                                                                <%--<li name="process_head_tab" data-bind="processlog"><a href="javascript:void(0);">审批记录</a></li>--%>
                                                                                <li name="process_head_tab" data-bind="processmap" data-load="flow_chart_load_Frame"><a href="javascript:void(0);">流程图</a></li>
                                                                            </ul>
                                                                        </div>
                                                                        <!--end 选项卡头部 -->

                                                                        <!-- begin 流程处理 -->
                                                                        <div name="process_body" data-bind="processflow" class="process_body_checked_true">
                                                                            <table class="tb_normal process_review_panel" width="100%">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td class="td_normal_title" width="15%">流程说明</td>
                                                                                        <td colspan="3">
                                                                                            <span id="fdFlowDescription" runat="server"></span>
                                                                                        </td>
                                                                                    </tr>

                                                                                    <%--<tr class="tr_normal_title" id="followOptRow">
                                                                                        <td align="left" colspan="4">
                                                                                            <a href="javascript:void(0);" id="followOptButton" class="com_btn_link" style="margin: 0px 10px 0px 0px; text-decoration: none;">下一节点流程属性
                                                                                            </a>
                                                                                        </td>
                                                                                    </tr>--%>

                                                                                    <tr id="nextNodeRow">
                                                                                        <td id="nextNodeTDTitle" class="td_normal_title" width="15%">下一节点选择
                                                                                        </td>
                                                                                        <td colspan="3" id="nextNodeTD">
                                                                                            <asp:UpdatePanel ID="UpdatePanelWorkFlow" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:DropDownList ID="SelectedNextNode" runat="server" AutoPostBack="True" Width="350px" OnSelectedIndexChanged="SelectedNextNode_SelectedIndexChanged" CssClass="inputsgl">
                                                                                                    </asp:DropDownList>&nbsp;&nbsp;
                                                                                                    <asp:CheckBox ID="CheckAutoNextNode" runat="server" Checked="True" Text="根据条件字段自动决定下一节点" ForeColor="Black" />
                                                                                                </ContentTemplate>
                                                                                                <Triggers>
                                                                                                    <asp:AsyncPostBackTrigger ControlID="SelectedNextNode" EventName="SelectedIndexChanged" />
                                                                                                </Triggers>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="manualBranchNodeRow">
                                                                                        <td class="td_normal_title" width="15%">评审模式
                                                                                        </td>
                                                                                        <td colspan="3" id="manualNodeSelectTD">
                                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:TextBox ID="PingshenMoshi" runat="server" BorderStyle="None" BorderWidth="0px" ReadOnly="True" Height="22px" Width="350px"></asp:TextBox>
                                                                                                </ContentTemplate>
                                                                                                <Triggers>
                                                                                                    <asp:AsyncPostBackTrigger ControlID="SelectedNextNode" EventName="SelectedIndexChanged" />
                                                                                                </Triggers>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="operationMethodsRow">
                                                                                        <td class="td_normal_title" width="15%">审批人选择模式
                                                                                        </td>
                                                                                        <td colspan="3" id="operationMethodsGroup">
                                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:TextBox ID="ShenpiMoshi" runat="server" BorderStyle="None" BorderWidth="0px" ReadOnly="True" Height="22px" Width="350px"></asp:TextBox>
                                                                                                </ContentTemplate>
                                                                                                <Triggers>
                                                                                                    <asp:AsyncPostBackTrigger ControlID="SelectedNextNode" EventName="SelectedIndexChanged" />
                                                                                                </Triggers>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                    </tr>

                                                                                    <tr id="notifyOptionTR">
                                                                                        <td class="td_normal_title" width="15%">审批人选择
                                                                                            <span class="txtstrong">*</span>
                                                                                        </td>
                                                                                        <td colspan="3">
                                                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <div class="inputselectsgl" style="width: 350px;">
                                                                                                        <div class="input">
                                                                                                            <asp:TextBox ID="ShenPiUser" runat="server" onKeyDown="javascript:return false;" CssClass="inputsgl" onclick="openuserDialogExt('ShenPiUser')"></asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="inputselectsgl" runat="server" id="ShenPiUserIcon">
                                                                                                        <div onclick="openuserDialogExt('ShenPiUser')" class="orgelement"></div>
                                                                                                    </div>
                                                                                                    <span class="txtstrong">*</span>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ShenPiUser"
                                                                                                        Display="Dynamic" ErrorMessage="必须指定审批人"></asp:RequiredFieldValidator>
                                                                                                    &nbsp;&nbsp;
					                                                                                <label>
                                                                                                        <asp:CheckBox ID="CHKSMS" runat="server" Checked="True" />内部邮件
                                                                                                    </label>
                                                                                                    &nbsp;&nbsp;
					                                                                                <label>
                                                                                                        <asp:CheckBox ID="CHKMOB" runat="server" Checked="True" />手机短信
                                                                                                    </label>
                                                                                                </ContentTemplate>
                                                                                                <Triggers>
                                                                                                    <asp:AsyncPostBackTrigger ControlID="SelectedNextNode" EventName="SelectedIndexChanged" />
                                                                                                </Triggers>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                    </tr>

                                                                                    <!--通知紧急程度 -->
                                                                                    <%--<tr id="notifyLevelRow">
                                                                                        <td class="td_normal_title" width="15%">通知紧急程度
                                                                                        </td>
                                                                                        <td colspan="3" id="notifyLevelTD">

                                                                                            <label class="lui-lbpm-radio">
                                                                                                <input type="radio" name="sysWfBusinessForm.fdNotifyLevel" value="1"><span style="color: #ff0000;" class="radio-label">紧急</span></label>
                                                                                            <label class="lui-lbpm-radio">
                                                                                                <input type="radio" name="sysWfBusinessForm.fdNotifyLevel" value="2"><span style="color: #0000ff;" class="radio-label">急</span></label>
                                                                                            <label class="lui-lbpm-radio">
                                                                                                <input type="radio" name="sysWfBusinessForm.fdNotifyLevel" value="3" checked=""><span style="color: #000000;" class="radio-label">一般</span></label>

                                                                                        </td>
                                                                                    </tr>--%>

                                                                                    <tr id="descriptionRow">
                                                                                        <td class="td_normal_title" width="15%">审批意见</td>
                                                                                        <td colspan="3">
                                                                                            <table width="100%" border="0" class="tb_noborder">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td id="optionButtons">
                                                                                                            <div data-lui-mark="dialog.content.buttons" class="lui_dialog_buttons clearfloat">
                                                                                                                <div class="lui_dialog_buttons_container" style="float: left;">
                                                                                                                    常用审批:&nbsp;&nbsp;
                                                                                                            <asp:DropDownList ID="commonUsages" onchange="_change(this)" runat="server" Style="width: 275px; overflow-x: hidden" CssClass="inputsgl">
                                                                                                                <asp:ListItem>请选择</asp:ListItem>
                                                                                                            </asp:DropDownList>&nbsp;&nbsp;
                                                                                                                    <div id="lui-id-170" class="lui-component lui_widget_btn lui_toolbar_btn_def" data-lui-cid="lui-id-170" title="电子签名" tabindex="0" style="display: inline-block; margin: 0px 10px;">
                                                                                                                        <div class="lui_toolbar_btn_l lui_toolbar_m" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                                                                                                            <div class="lui_toolbar_btn_r">
                                                                                                                                <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                                                                                    <div id="lui-id-171" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-171" onclick="selectyinzhang(yinzhangplaceholder);">电子签名</div>
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <%--<a href="javascript:;" class="com_btn_link" id="signature" onclick="selectyinzhang(yinzhangplaceholder);">电子签名</a>--%>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td id="fdUsageContentTd" width="85%">
                                                                                                            <span id="fdUsageContentSpan" style="display: block;">
                                                                                                                <asp:TextBox ID="fdUsageContent" runat="server" Height="100px" TextMode="MultiLine" Style="width: 97%; padding: 0;"></asp:TextBox>
                                                                                                                <span id="mustSignStar" class="txtstrong" style="margin-top: 65px; position: absolute; display: none">*</span>
                                                                                                            </span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <div id="nodeDescriptionDiv" style="display: none;">
                                                                                                                <div class="lui_kmCard_wrap">
                                                                                                                    <p class="lui_kmCard_title">
                                                                                                                        <label id="currentNodeDescription"></label>
                                                                                                                    </p>
                                                                                                                    <div id="extNodeDescriptionDiv">
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <!-- 流程页签下展示已盖的签章 -->
                                                                                    <tr id="showSignature" style="">
                                                                                        <td class="td_normal_title" width="15%">电子签名展示
                                                                                        </td>
                                                                                        <td id="signaturePic" colspan="3" width="85%">
                                                                                            <ul id="signaturePicUL" class="clearfloat lui_sns_signatureList">
                                                                                                <asp:Image ID="yinzhangplaceholder" runat="server" Width="110" Height="60" alt="签名图片" src="" disabled="disabled" Style="display: none;" />
                                                                                                <%--<img disabled="disabled" id="yinzhangplaceholder" runat="server" width="110" height="60" alt="签名图片" src="" style="display: none;" />--%>
                                                                                            </ul>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="assignmentRow" style="">
                                                                                        <td class="td_normal_title" width="15%">审批附件
                                                                                        </td>
                                                                                        <td colspan="3">
                                                                                            <!-- Attachments -->
                                                                                            <asp:FileUpload ID="ShenPiFuJian" runat="server" Width="351px" />
                                                                                            <%--<asp:CheckBoxList ID="FujianList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                                                                                            </asp:CheckBoxList>&nbsp;
                                                                                            <asp:ImageButton ID="btnDelFile" runat="server" CausesValidation="False"
                                                                                                ImageAlign="AbsMiddle" ImageUrl="../images/Button/DelFile.jpg" OnClick="btnDelFile_Click" />&nbsp; &nbsp;&nbsp;
                                                                                            <asp:ImageButton ID="btnReadFile" runat="server" CausesValidation="False" ImageAlign="AbsMiddle"
                                                                                                     ImageUrl="~/images/Button/ReadFile.gif" OnClick="btnReadFile_Click" />--%>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <%--<tr id="otherCanViewCurNodeTR" style="display: none;">
                                                                                        <td class="td_normal_title" width="15%">本节点意见的其他可阅读者</td>
                                                                                        <td>
                                                                                            <input type="hidden" name="wf_otherCanViewCurNodeIds" value="170cd37c6f0749a40f2499d4eb08a4b8">
                                                                                            <textarea name="wf_otherCanViewCurNodeNames" style="width: 85%" readonly=""></textarea>
                                                                                            <a href="javascript:;" class="com_btn_link" onclick="Dialog_Address(true,'wf_otherCanViewCurNodeIds','wf_otherCanViewCurNodeNames', ';',ORG_TYPE_ALL,function myFunc(rtv){lbpm.globals.updateXml(rtv,'otherCanViewCurNode');});">选择</a>
                                                                                        </td>
                                                                                    </tr>--%>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                        <!--end 流程处理 -->

                                                                        <!-- begin流程状态 -->
                                                                        <div name="process_body" data-bind="processstate" class="process_body_checked_false">
                                                                            <iframe width="100%" height="100%" scrolling="yes" frameborder="0" id="ST_IFrame" src=""
                                                                                data-src='../NWorkFlow/NWorkFlowState.aspx?ID=<%=ZWL.Common.PublicMethod.CheckInt(ZWL.Common.PublicMethod.GetDecryptParam("ID")) %>' style="min-height: 484px;"></iframe>
                                                                        </div>
                                                                        <!--end 流程状态  -->

                                                                        <!--begin 审批记录  -->
                                                                        <%--<div name="process_body" data-bind="processlog" class="process_body_checked_false">
                                                                            <iframe width="100%" height="100%" scrolling="yes" frameborder="0" id="SP_IFrame" src=""
                                                                                data-src='../NWorkFlow/NWorkFlowLog.aspx?ID=<%=ZWL.Common.PublicMethod.CheckInt(ZWL.Common.PublicMethod.GetDecryptParam("ID")) %>' style="min-height: 484px;"></iframe>
                                                                        </div>--%>
                                                                        <!--end 审批记录  -->

                                                                        <!-- begin流程图 -->
                                                                        <div name="process_body" data-bind="processmap" class="process_body_checked_false">
                                                                            <iframe width="100%" height="100%" scrolling="yes" frameborder="0" id="WF_IFrame" src="" data-src='../NWorkFlow/NWorkFlowMap.aspx?WorkFlowID=<%=WorkFlowId %>&CurrentNodeID=<%=CurrentNodeId %>' style="z-index: 9999; height: 484px;"></iframe>
                                                                        </div>
                                                                        <!-- end流程图 -->

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--流程信息 结束--%>
                                            <iframe frameborder="0" class="lui_tabpage_float_navs_mark" scrolling="no"></iframe>
                                            <div class="lui_tabpage_float_navs">
                                                <div class="lui_tabpage_float_navs_l">
                                                    <div class="lui_tabpage_float_navs_r">
                                                        <div class="lui_tabpage_float_navs_c" style="max-width: 1200px; text-align: center; padding-top: 5px; padding-bottom: 5px; height: 40px;">
                                                            <%--<div id="lui-id-173" class="lui-component lui_widget_btn lui_toolbar_btn_def" data-lui-cid="lui-id-170" title="原始表单">
                                                                <div class="lui_toolbar_btn_l lui_toolbar_m" data-lui-mark="toolbar_button_inner" style="text-align: center;height: 33px;line-height: 33px;">
                                                                    <div class="lui_toolbar_btn_r">
                                                                        <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                            <div id="lui-id-174" class="lui-component lui_widget_btn_txt btnOriginalForm" data-lui-cid="lui-id-171" onclick="javascript:void(0);">原表单</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>--%>
                                                            <div id="lui-id-175" class="lui-component lui_widget_btn lui_toolbar_btn_def savepassBtntd" data-lui-cid="lui-id-170" title="保存并通过" onclick="javascript:return submit_client_ex();">
                                                                <div class="lui_toolbar_btn_l lui_toolbar_m" data-lui-mark="toolbar_button_inner" style="text-align: center; height: 33px; line-height: 33px;">
                                                                    <div class="lui_toolbar_btn_r">
                                                                        <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                            <div id="lui-id-176" class="lui-component lui_widget_btn_txt passedBtnText" data-lui-cid="lui-id-171">保存并通过</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="lui-id-181" class="lui-component lui_widget_btn lui_toolbar_btn_def forwardBtntd" data-lui-cid="lui-id-170" title="转发并阅办" onclick="javascript:return doforward();" style="display: none;">
                                                                <div class="lui_toolbar_btn_l lui_toolbar_m" data-lui-mark="toolbar_button_inner" style="text-align: center; height: 33px; line-height: 33px;">
                                                                    <div class="lui_toolbar_btn_r">
                                                                        <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                            <div id="lui-id-1566" class="lui-component lui_widget_btn_txt forwardBtnText" data-lui-cid="lui-id-171">转发并阅办</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="lui-id-185" class="lui-component lui_widget_btn lui_toolbar_btn_def saveendBtntd" data-lui-cid="lui-id-170" title="保存并结束" style="display: none;">
                                                                <div class="lui_toolbar_btn_l lui_toolbar_m" data-lui-mark="toolbar_button_inner" style="text-align: center; height: 33px; line-height: 33px;">
                                                                    <div class="lui_toolbar_btn_r">
                                                                        <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                            <div id="lui-id-186" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-171" onclick="javascript:return submit_client_end();">保存并结束</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="lui-id-177" class="lui-component lui_widget_btn lui_toolbar_btn_def returnBtntd" data-lui-cid="lui-id-170" title="驳回" onclick="javascript:document.getElementById('btnReturn').click();">
                                                                <div class="lui_toolbar_btn_l lui_toolbar_m" data-lui-mark="toolbar_button_inner" style="text-align: center; height: 33px; line-height: 33px;">
                                                                    <div class="lui_toolbar_btn_r">
                                                                        <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                            <div id="lui-id-178" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-171">驳回</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="lui-id-179" class="lui-component lui_widget_btn lui_toolbar_btn_def rejectBtntd" data-lui-cid="lui-id-170" title="不通过" onclick="javascript:document.getElementById('btnReject').click();">
                                                                <div class="lui_toolbar_btn_l lui_toolbar_m" data-lui-mark="toolbar_button_inner" style="text-align: center; height: 33px; line-height: 33px;">
                                                                    <div class="lui_toolbar_btn_r">
                                                                        <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                            <div id="lui-id-180" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-171">不通过</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <%--<div id="lui-id-181" class="lui-component lui_widget_btn lui_toolbar_btn_def receiptBtntd" data-lui-cid="lui-id-170" title="收账金额管理" style="display: none;" onclick="javascript:addnewtrdzje();">
                                                                <div class="lui_toolbar_btn_l lui_toolbar_m" data-lui-mark="toolbar_button_inner" style="text-align: center; height: 33px; line-height: 33px;">
                                                                    <div class="lui_toolbar_btn_r">
                                                                        <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                            <div id="lui-id-182" class="lui-component lui_widget_btn_txt btnReceipt" data-lui-cid="lui-id-171">收账金额管理</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>--%>
                                                            <%--<div id="lui-id-183" class="lui-component lui_widget_btn lui_toolbar_btn_def" data-lui-cid="lui-id-170" title="返回">
                                                                <div class="lui_toolbar_btn_l lui_toolbar_m" data-lui-mark="toolbar_button_inner" style="text-align: center;height: 33px;line-height: 33px;">
                                                                    <div class="lui_toolbar_btn_r">
                                                                        <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                            <div id="lui-id-184" class="lui-component lui_widget_btn_txt btnGoback" data-lui-cid="lui-id-171" onclick="javascript:window.location.href='<%=UrlReferrer%>'">返回</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>--%>
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
        <div style="display: none;">
            <asp:HiddenField runat="server" ID="MD5" />
            <asp:HiddenField runat="server" ID="Hidden_form" />
            <asp:HiddenField runat="server" ID="Hidden_SignInput" />
            <asp:HiddenField runat="server" ID="Hidden_SignImg" />
        </div>
        <script type="text/javascript">
             <%=PiLiangSet%>
        </script>
    </form>
    <div id="win_yinzhang" class="easyui-window" data-options="title:'选择印章',iconCls:'icon-search',closed:true,closable:true,minimizable:false,maximizable:false,collapsible:false,resizable:true" style="width: 400px; height: 500px; padding: 5px; display: none;">
        <iframe id="yinzhang" scrolling="yes" frameborder="0" src="" style="width: 100%; height: 100%;"></iframe>
    </div>
    <div id="win_user" class="easyui-window" data-options="title:'选择用户',iconCls:'icon-search',closed:true,closable:false,minimizable:false,maximizable:false,collapsible:false,resizable:true" style="width: 350px; height: 500px; padding: 5px; display: none;">
        <iframe id="user" scrolling="yes" frameborder="0" src="" style="width: 100%; height: 100%;"></iframe>
    </div>
    <div id="win_dzje" class="easyui-window" data-options="title:'到账金额管理',iconCls:'icon-search',closed:true,closable:true,minimizable:false,maximizable:false,collapsible:false,resizable:true" style="width: 80%; height: 700px; padding: 5px; display: none;">
        <iframe id="dzje" scrolling="auto" frameborder="0" src="" style="width: 100%; height: 100%; overflow: hidden"></iframe>
    </div>
    <script type="text/javascript">
        $(function () {
            // 获取td标签下的所有子元素
            var children = document.querySelectorAll("#RebuildForm_Label_FormContent td > *");

            // 遍历子元素
            for (var i = 0; i < children.length - 1; i++) {
                // 判断当前元素是否是input标签，且id为"id"，type为"text"
                if (children[i].tagName === "INPUT" && children[i].type === "text") {
                    var childParent = children[i].parentNode;
                    // 判断下一个元素是否是文本节点，且包含文字“元”
                    if (childParent != null && childParent.tagName === "TD" && childParent.innerText === "元") {
                        // 设置input标签的宽度为50px
                        if (children[i].style.width === "99%") {
                            children[i].style.width = "60%";
                        }
                    }
                    if (children[i].id != null && children[i].id != '' && children[i].id != 'Date957883128' && children[i].id.indexOf('Date') >= 0) {
                        children[i].style.width = "100px";
                    }
                }
            }

        })
    </script>
</body>
</html>
