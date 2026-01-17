<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NWorkToDoViewNew.aspx.cs" Inherits="NWorkFlow_NWorkToDoViewNew" %>

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
    <link href="../CSS/common/form.theme.css" rel="stylesheet" />
    <link href="../CSS/common/upload.css" rel="stylesheet" />
    <link href="../CSS/Loading.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/default/easyui.css" rel="stylesheet" />
    <script src="../JS/jquery-1.9.1.js" type="text/javascript"></script>
    <%--<script src="../JS/jquery-1.4.1.min.js" type="text/javascript"></script>--%>
    <script src="../JS/jquery.easyui.min.js"></script>
    <script src="../JS/jquery.blockUI.js" type="text/javascript"></script>
    <script src="../JS/BrowserVersion.js" type="text/javascript"></script>
    <script src="../JS/common.js?v=202306020823" type="text/javascript"></script>
    <script src="../JS/lui/tabcontent.js" type="text/javascript"></script>
    <% if (ZWL.Common.PublicMethod.XiangMuFormIDsint.Contains(FormId)
                            || ZWL.Common.PublicMethod.HRFormIDsint.Contains(FormId))%>
    <%{ %>
    <% if (ZWL.Common.PublicMethod.HRFormIDsint.Contains(FormId))%>
    <%{ %>
    <script src="../JS/jquery.jqprint-0.3.js" type="text/javascript"></script>
    <script src="../JS/printThis.js" type="text/javascript"></script>
    <style media="print">
        div {
            font-size: 32px;
            padding-top: 10px
        }
    </style>
    <%} %>
    <%} %>
    <script type="text/javascript">
        var intHeight = 40;
        var intTop = 20;
        var intLeft = "5%";
        $(function () {
            <% if (ZWL.Common.PublicMethod.JingYingFormIDsint.Contains(FormId) && ZWL.Common.PublicMethod.GetSessionValue("Department").Contains("经营管理科"))%>
            <%{ %>
            $(".editBtntd").show();
            <%} %>
            <% if (ZWL.Common.PublicMethod.JingYingFormIDsint.Contains(FormId) && ZWL.Common.PublicMethod.GetSessionValue("JiaoSe").Contains("超级管理员"))%>
            <%{ %>
            $(".editBtntd").show();
            <%} %>
            <% if (ZWL.Common.PublicMethod.GetSessionValue("JiaoSe").Contains("资料室") && FormId == 73)%>
            <%{ %>
            $(".editBtntd").show();
            <%} %>

            <% if (ZWL.Common.PublicMethod.JingYingFormIDsint.Contains(FormId) || ZWL.Common.PublicMethod.XiangMuFormIDsint.Contains(FormId) || ZWL.Common.PublicMethod.HRFormIDsint.Contains(FormId))%>
            <%{ %>
            setformstyle();
            <%} %>
            <% if (ZWL.Common.PublicMethod.HRFormIDsint.Contains(FormId))%>
            <%{ %>
            intHeight = 150;
            intTop = "10%";
            intLeft = "8%";
            <%} %>
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
            <%if (FormId == 46)%>
            <%{ %>
            refreshdzje();
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
        });
        function showInput(ipid) {
            //if ($(".ReplaceComplate").length > 0) {
            //    return true;
            //}
            var jqobj = $("input[name='" + ipid + "']");
            if (jqobj.length > 0) {
                jqobj.after("<span class='ReplaceComplate'>" + jqobj.val() + "</span>");
                jqobj.hide();
                return true;
            }
            else {
                return false;
            }
            console.log(ipid + "");
        }
        function prn_preview() {
            <%if (FormId == 115)%>
            <%{ %>
            confirmPostPrint(getUrlParms('ID'));
            <%} %>
            <%else %>
            <%{ %>
            window.open('../<%=UrlReferBasePath %>/PrintWork.aspx?ID=' + getUrlParms('ID') + '&Action=auto');
            <%} %>
        };

        function confirmPostPrint(id) {
            var options = {
                url: '../Services/Services.ashx',
                data: { f: "PrintProjectArchiveDetailReport", id: id },
                async: false,
                dataType: "json",
                timeout: 60 * 1000,
                success: function (data) {
                    if (data.Code) {
                        var options1 = {
                            title: "资料归档表-勘查:" + data.Data,
                            url: '../CommonSelect/PrintHelper.aspx?filename=' + data.Data + "&_" + Math.random(),
                            width: $("body").width() - 40,
                            height: $("body").height() - 40,
                            onFinish: function (returnVal) {
                                window.frameElement.src = window.frameElement.src;
                            }
                        }
                        showPopwindow("", options1);
                    }
                    else {

                    }
                    if (data.Message != "") {
                        alert(data.Message);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(XMLHttpRequest);
                    hideLoading();
                },
                beforeSend: function () {
                    showLoading();
                },
                complete: function () {
                    hideLoading();
                }
            };

            MakeRequestAjax(options);
        }
        function CreateOneFormPage() {
            showOriginForm();
            var nwidth = 0, nheight = intHeight;
            var strhtml = "";
            strhtml = $("#RebuildForm_Label_FormContent")[0].innerHTML;
            nwidth = $("#RebuildForm_Label_FormContent")[0].scrollWidth;
            nheight += $("#RebuildForm_Label_FormContent")[0].scrollHeight;
        };
        function SetBSStateOver() {
            if (<%=FormId%> == 78 || <%=FormId%> == 94) {
                $.ajax({
                    type: "post",
                    url: "../HR/HRPost.aspx",
                    data: { M: "SetBSStateOver", ID: <%=Id%> },
                    success: function (response) {
                        if (response != "OK") {
                            alert("更新报销状态出错," + response);
                        }
                    }
                });
            }
        }
        //打印预览
        function Preview() {
            showOriginForm();
            SetBSStateOver();
            var rooturl = getRootPath_web();
            var html = $("#RebuildForm_Label_FormContent").prop("innerHTML");
            var data = html.split('../').join(rooturl + '/');
            $("#RebuildForm_Label_FormContent").html(data);
            $("#RebuildForm_Label_FormContent").printThis({
                debug: false,
                importCSS: true,
                importStyle: true,
                printContainer: true,
                pageTitle: "表单打印",
                removeInline: false,
                printDelay: 333,
                header: null,
                formValues: true
            });
        };

        //JQuery打印
        function JQueryPrint() {
            showOriginForm();
            SetBSStateOver();
            //$("#lblFormContent").css("padding-top", "#20px");
            $("#RebuildForm_Label_FormContent").jqprint({
                debug: false, //如果是true则可以显示iframe查看效果（iframe默认高和宽都很小，可以再源码中调大），默认是false
                importCSS: true, //true表示引进原来的页面的css，默认是true。（如果是true，先会找$("link[media=print]")，若没有会去找$("link")中的css文件）
                printContainer: true, //表示如果原来选择的对象必须被纳入打印（注意：设置为false可能会打破你的CSS规则）。
                operaSupport: true//表示如果插件也必须支持歌opera浏览器，在这种情况下，它提供了建立一个临时的打印选项卡。默认是true
            });
        }
        function showOriginForm() {
            $(".lui_panel_content_inside.rebuildform .lui_form_title_frame").each(function (e) {
                var title = $(this).attr("data-title");
                if (title == "原表单") {
                    $(this).parent(".lui-component").show();
                }
                else {
                    $(this).parent(".lui-component").hide();
                }
            });
        }
        //设置表单中的控件的读写属性
        function setformstyle() {
            $("input:disabled").removeAttr("disabled").attr("readonly", "readonly");
            $("textarea:disabled").removeAttr("disabled").attr("readonly", "readonly");
            $("p:disabled").removeAttr("disabled").attr("readonly", "readonly");
            //$(":text").attr("disabled", false);
            //$(":text").attr("readonly", true);
            //$("img[id^=img]").attr("disabled", true);
            //var datelist = $('input[id^=Date]');
            //datelist.attr("disabled", true);
            //$(":checkbox").attr("disabled", false);
            $("#RebuildForm_Label_FormContent textarea").css("overflow", "hidden").css("border", "0");
        }
        function CheckModify() {
            if (window.confirm("确认要修改表单中的内容吗？")) {
                return true;
            }
            else {
                return false;
            }
        }
        function opensubwin() {
            if (CheckModify()) {
                var url = '';
                <% if (!string.IsNullOrEmpty(EditModelUrl))%>
                <%{%>
                url =<%=EditModelUrl%>;
                <%}%>
                if (url != '') {
                    var data = eval(url);
                    if (data.win == "win") {
                        window.open(data.url);
                    }
                    else {
                        location.href = data.url;
                    }
                }
            }
        }
        function refreshdzje() {
            $.ajax({
                type: "get",
                async: false,
                cache: false,
                contentTYPE: "application/text/html;charset = utf-8",
                data: { flag: "InitHTDZJE", NWorkToDoID: getUrlParms("ID"), DZJE: "", Date: GetTime() },
                dataType: "json",
                url: "../Main/GetJsonResultHandler.ashx",
                success: function (data) {
                    if (data != null && data.data != null) {
                        //删除旧表单的到账金额数据
                        var deletecount = $("#Text681200471").parents("tr:first").nextAll().length - 1;
                        for (var i = 0; i < deletecount; i++) {
                            $("#Text681200471").parents("tr:first").next().remove()
                        }

                        //在表单塞入到账金额
                        $("#Text681200471").parents("tr:first").after(data.data.HTMLCode);

                    }
                }, complete: function () {

                }, error: function (errorMsg) {
                    alert("网络错误，请联系管理员！");
                }
            });
        }
        function GetTime() {
            var Nows = new Date();
            return Nows.getFullYear() +
                "-" +
                ((Nows.getMonth() + 1) < 10 ? "0" + (Nows.getMonth() + 1) : (Nows.getMonth() + 1)) +
                "-" +
                (Nows.getDate() < 10 ? "0" + Nows.getDate() : Nows.getDate());
        }
        function setInputStyle(strformid) {
            //对于不同的表单做相应的修改,因为制作表单时名称不统一            
            if (strformid == "43" || strformid == "46" || strformid == "44" || strformid == "45") {
                //对于合同签订评审表单，对合同名称和项目名称做处理
                var htname = $("input[alt='合同名称']")[0].value.toString();
                if (htname != "" && !$("input[alt='合同名称']").next().is("p")) {
                    var strhtname = "<p>" + htname + "</p>";
                    var htp = $(strhtname);
                    $("input[alt='合同名称']").after(htp).hide();
                }
                if ($("input[alt='项目名称']").length > 0 && !$("input[alt='项目名称']").next().is("p")) {
                    var xmname = $("input[alt='项目名称']")[0].value.toString();
                    if (xmname != "") {
                        $("input[alt='项目名称']").parent().removeAttr("width");//   对于合同签订评审表中的项目名称，需要取消上级元素td的width属性才能显示正常。
                        var strxmname = "<p>" + xmname + "</p>";
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
                        var strxmname = "<p>" + xmname + "</p>";
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
                    var strtbname = "<p>" + tbname + "</p>";
                    var tbp = $(strtbname);
                    $("input[alt='投标项目名称']").after(tbp).hide();
                }
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
<body class="lui_form_body " style="margin-top: 43px; margin-bottom: 46px;">
    <form id="form1" runat="server" target="_blank">
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
                                                        <div id="refreshBtn" data-lui-type="lui/toolbar!Button" data-lui-parentid="toolbar" style="" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="refreshBtn" data-lui-parse-init="7" title="刷新表单" tabindex="0" onclick="javascript: window.location=window.location;">
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
                                                        <div id="bookedBtn" data-lui-type="lui/toolbar!Button" data-lui-parentid="toolbar" style="" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="bookedBtn" data-lui-parse-init="7" title="原始表单" tabindex="0" onclick="javascript:window.location.href='../<%=UrlReferBasePath%>/NWorkToDoView.aspx?ID='+getUrlParms('ID')">
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
                                                <td>
                                                    <div class="lui_toolbar_btn" data-lui-on-class="lui_toolbar_btn_on" data-lui-status-class="lui_toolbar_btn_toggle_on">
                                                        <div id="printBtn" data-lui-type="lui/toolbar!Button" data-lui-parentid="toolbar" style="" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="bookedBtn" data-lui-parse-init="7" title="打印" tabindex="0" onclick="javascript:prn_preview();">
                                                            <div class="lui_toolbar_btn_l" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                                                <div class="lui_toolbar_btn_r">
                                                                    <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                        <div id="lui-id-52" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-30">打印</div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <% if (ZWL.Common.PublicMethod.HRFormIDsint.Contains(FormId))%>
                                                <%{ %>
                                                <td>
                                                    <div class="lui_toolbar_btn" data-lui-on-class="lui_toolbar_btn_on" data-lui-status-class="lui_toolbar_btn_toggle_on">
                                                        <div id="printBtn1" data-lui-type="lui/toolbar!Button" data-lui-parentid="toolbar" style="" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="bookedBtn" data-lui-parse-init="7" title="打印1" tabindex="0" onclick="javascript:Preview();">
                                                            <div class="lui_toolbar_btn_l" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                                                <div class="lui_toolbar_btn_r">
                                                                    <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                        <div id="lui-id-56" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-30">打印1</div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="lui_toolbar_btn" data-lui-on-class="lui_toolbar_btn_on" data-lui-status-class="lui_toolbar_btn_toggle_on">
                                                        <div id="printBtn2" data-lui-type="lui/toolbar!Button" data-lui-parentid="toolbar" style="" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="bookedBtn" data-lui-parse-init="7" title="打印2" tabindex="0" onclick="javascript:JQueryPrint();">
                                                            <div class="lui_toolbar_btn_l" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                                                <div class="lui_toolbar_btn_r">
                                                                    <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                        <div id="lui-id-57" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-30">打印2</div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <%} %>
                                                <td>
                                                    <div class="lui_toolbar_btn" data-lui-on-class="lui_toolbar_btn_on" data-lui-status-class="lui_toolbar_btn_toggle_on">
                                                        <div data-lui-type="lui/toolbar!Button" style="" id="lui-id-3" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="lui-id-3" data-lui-parse-init="3" title="返回" tabindex="0" onclick="javascript:goback();">

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
                                                        <div class="lui_item_txt" title="工作信息" onclick="javascript:void(0);">工作信息</div>
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
                                            <%if (ZWFiles.Any())%>
                                            <% { %>
                                            <div class="lui_tabpage_float_content" style="">
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
                                                <div class="lui_tabpage_float_content_l">
                                                    <div class="lui_tabpage_float_content_r">
                                                        <div class="lui_tabpage_float_content_c">
                                                            <div data-lui-mark="panel.content.inside" class="lui_panel_content_inside">
                                                                <div data-lui-type="lui/panel!Content" id="lui-id-13" class="lui-component" data-lui-cid="lui-id-13" data-lui-parse-init="15">
                                                                    <div class="lui_form_title_frame" data-index="1" data-title="正文文件">
                                                                        <table class="tb_normal treeTable" width="100%" style="text-align: center;">
                                                                            <tbody>
                                                                                <%if (ZWFiles.Any())
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
                                                                                <%for (int i = 0; i < ZWFiles.Count; i++)%>
                                                                                <%{
                                                                                        var item = ZWFiles[i];
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
                                                                                                <a target="_blank" style="text-decoration: underline;" href="<%=downUrl %>"><%=item.OldName %></a>
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
                                                                                <%} %>
                                                                                <%}
                                                                                    else
                                                                                    { %>
                                                                                <tr class="tr_normal initialized">
                                                                                    <td colspan="4" style="height: 26px;">无附件!</td>
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
                                                                                <%for (int i = 0; i < Files.Count; i++)%>
                                                                                <%{
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
                                                                                                <a target="_blank" style="text-decoration: underline;" href="<%=downUrl %>"><%=item.OldName %></a>
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
                                                                                <%} %>
                                                                                <%}
                                                                                    else
                                                                                    { %>
                                                                                <tr class="tr_normal initialized">
                                                                                    <td colspan="4" style="height: 26px;">无附件!</td>
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
                                                                        <iframe width="100%" height="auto" scrolling="yes" frameborder="0" id="SP_IFrame"
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
                                                                                <li name="process_head_tab" data-bind="processstate" class="active" data-isclick="true"><a href="javascript:void(0);">流程状态</a></li>
                                                                                <%--<li name="process_head_tab" data-bind="processlog"><a href="javascript:void(0);">审批记录</a></li>--%>
                                                                                <li name="process_head_tab" data-bind="processmap" data-load="flow_chart_load_Frame"><a href="javascript:void(0);">流程图</a></li>
                                                                            </ul>
                                                                        </div>
                                                                        <!--end 选项卡头部 -->

                                                                        <!-- begin流程状态 -->
                                                                        <div name="process_body" data-bind="processstate" class="process_body_checked_true">
                                                                            <iframe width="100%" height="100%" scrolling="yes" frameborder="0" id="ST_IFrame" src='../NWorkFlow/NWorkFlowState.aspx?ID=<%=ZWL.Common.PublicMethod.CheckInt(ZWL.Common.PublicMethod.GetDecryptParam("ID")) %>'
                                                                                data-src='../NWorkFlow/NWorkFlowState.aspx?ID=<%=ZWL.Common.PublicMethod.CheckInt(ZWL.Common.PublicMethod.GetDecryptParam("ID")) %>' style="min-height: 300px;"></iframe>
                                                                        </div>
                                                                        <!--end 流程状态  -->

                                                                        <!--begin 审批记录  -->
                                                                        <%--<div name="process_body" data-bind="processlog" class="process_body_checked_false">
                                                                            <iframe width="100%" height="100%" scrolling="yes" frameborder="0" id="SP_IFrame" src=""
                                                                                data-src='../NWorkFlow/NWorkFlowLog.aspx?ID=<%=ZWL.Common.PublicMethod.CheckInt(ZWL.Common.PublicMethod.GetDecryptParam("ID")) %>' style="min-height: 300px;"></iframe>
                                                                        </div>--%>
                                                                        <!--end 审批记录  -->

                                                                        <!-- begin流程图 -->
                                                                        <div name="process_body" data-bind="processmap" class="process_body_checked_false">
                                                                            <iframe width="100%" height="100%" scrolling="yes" frameborder="0" id="WF_IFrame" src="" data-src='../NWorkFlow/NWorkFlowMap.aspx?WorkFlowID=<%=WorkFlowId %>&CurrentNodeID=<%=CurrentNodeId %>' style="z-index: 9999; min-height: 484px;"></iframe>
                                                                            <table class="tb_normal process_review_panel" width="100%">
                                                                                <tbody>
                                                                                    <tr class="tr_normal_title" style="display: none">
                                                                                        <td align="left" colspan="4">
                                                                                            <label>
                                                                                                <input type="checkbox" id="flowGraphicShowCheckbox" value="true">
                                                                                                流程图</label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="flowGraphic">
                                                                                        <td id="workflowInfoTD" _onresize="lbpm.flow_chart_load_Frame();" colspan="4" data-init-resize="true"></td>
                                                                                    </tr>

                                                                                </tbody>
                                                                            </table>
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
                                                            <div id="lui-id-173" class="lui-component lui_widget_btn lui_toolbar_btn_def" data-lui-cid="lui-id-170" title="原始表单" onclick="javascript:window.location.href='../<%=UrlReferBasePath%>/NWorkToDoView.aspx?ID='+getUrlParms('ID')">
                                                                <div class="lui_toolbar_btn_l lui_toolbar_m" data-lui-mark="toolbar_button_inner" style="text-align: center; height: 33px; line-height: 33px;">
                                                                    <div class="lui_toolbar_btn_r">
                                                                        <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                            <div id="lui-id-174" class="lui-component lui_widget_btn_txt btnOriginalForm" data-lui-cid="lui-id-171">原表单</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="lui-id-135" class="lui-component lui_widget_btn lui_toolbar_btn_def" data-lui-cid="lui-id-170" title="打印" onclick="javascript:prn_preview();">
                                                                <div class="lui_toolbar_btn_l lui_toolbar_m" data-lui-mark="toolbar_button_inner" style="text-align: center; height: 33px; line-height: 33px;">
                                                                    <div class="lui_toolbar_btn_r">
                                                                        <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                            <div id="lui-id-136" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-171">打印</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <% if (ZWL.Common.PublicMethod.HRFormIDsint.Contains(FormId))%>
                                                            <%{ %>
                                                            <div id="lui-id-121" class="lui-component lui_widget_btn lui_toolbar_btn_def" data-lui-cid="lui-id-170" title="打印1" onclick="javascript:Preview();">
                                                                <div class="lui_toolbar_btn_l lui_toolbar_m" data-lui-mark="toolbar_button_inner" style="text-align: center; height: 33px; line-height: 33px;">
                                                                    <div class="lui_toolbar_btn_r">
                                                                        <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                            <div id="lui-id-122" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-171">打印1</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="lui-id-123" class="lui-component lui_widget_btn lui_toolbar_btn_def" data-lui-cid="lui-id-170" title="打印2" onclick="javascript:JQueryPrint();">
                                                                <div class="lui_toolbar_btn_l lui_toolbar_m" data-lui-mark="toolbar_button_inner" style="text-align: center; height: 33px; line-height: 33px;">
                                                                    <div class="lui_toolbar_btn_r">
                                                                        <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                            <div id="lui-id-124" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-171">打印2</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <%} %>
                                                            <div id="lui-id-183" class="lui-component lui_widget_btn lui_toolbar_btn_def" data-lui-cid="lui-id-170" title="返回" onclick="javascript:goback();">
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
                                            <div class="lui_tabpage_float_collapse" title="收起"><a class="txt">收起</a></div>
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
                        if (children[i].id != null && children[i].id != '' && children[i].id.indexOf('Date') >= 0) {
                            if (children[i].id != 'Date957883128') {
                                children[i].style.width = "100px";
                            }
                            else {
                                children[i].style.width = "98%";
                            }
                        }
                    }
                }
            }

        })
        var ShowTransform = <%= Util.ShowTransformSetting(Id).ToString().ToLower() %>;
        if (ShowTransform) {
            SetTabcontentActive(2);
        }
    </script>
</body>
</html>
