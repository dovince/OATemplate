<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YongCanYiChangAlert.aspx.cs" Inherits="CommonSelect_YongCanYiChangAlert" %>

<%@ Import Namespace="ZWL.Common" %>
<%@ Register Src="~/UserControls/UploadFiles.ascx" TagPrefix="uc1" TagName="UploadFiles" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge; charset=gb2312" />
    <link href="../CSS/common/zone.theme.css" rel="stylesheet" />
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
    <link href="../CSS/common/widget.theme.css" rel="stylesheet" />
    <link href="../CSS/common/process_tab_main.css" rel="stylesheet" />
    <link href="../CSS/default/easyui.css" rel="stylesheet" />
    <link href="../CSS/Loading.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/cxcalendar.css" rel="stylesheet" />
    <link href="../CSS/icon.css" rel="stylesheet" />
    <link href="../CSS/common/form.theme.css" rel="stylesheet" />
    <link href="../CSS/common/dialog.theme.css" rel="stylesheet" />
    <link href="../CSS/common/dnd.css" rel="stylesheet" />
    <link href="../CSS/common/upload.css" rel="stylesheet" />
    <link href="../JS/metronic/assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JS/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../CSS/calendar.js"></script>
    <script type="text/javascript" src="../JS/jquery.easyui.min.js"></script>
    <script src="../JS/common.js?v=202306020823" type="text/javascript"></script>
    <style>
        body, html, #mapcontainer {
            width: 100%;
            min-height: 100%;
            overflow: hidden;
            margin: 0;
            position: absolute;
        }
    </style>
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
                    getYongCanYiChangInfoByDateChanged(val);
                });
            });
            $("#ReceivedUser").on("blur", function () {
                var date = $("#TimeStr").val()
                var users = $(this).val();
                getYongCanYiChangInfo(date, users);
            });

        });
        function getYongCanYiChangInfoByDateChanged(date) {
            getYongCanYiChangInfo(date, '');
        }
        function getYongCanYiChangInfo(date, users) {
            var option = {
                url: '../Services/Services.ashx',
                data: {
                    f: 'YongCanYiChangInfo',
                    date: date,
                    users: users
                },
                type: 'POST',
                dataType: "json",
                success: function (data) {
                    if (data.Code) {
                        $("#ReceivedUser").val(data.Data.Users);
                        $("#SavedUsers").val(JSON.stringify(data.Data.MList));
                        var html = "<ol>";
                        for (var i = 0; i < data.Data.MList.length; i++) {
                            var item = data.Data.MList[i];
                            html += "<li style='list-style: decimal;'>" + item.Key + ":" + item.Value + "</li>";
                        }
                        html += "</ol>";
                        $("#lblContent").html(html);
                    }
                    else {
                        $("#ReceivedUser").val('');
                        $("#lblContent").html('');
                        alertMessage(data.Message);
                    }
                }
            };
            MakeRequestAjax(option);
        }
        function CCC() {
            window.returnValue = "";
            if (window.parent.length > 0) {
                window.parent.$('#win_select').window('close');
            }
            else {
                window.close();
            }
        }
        function submitForm() {
            document.getElementById('btnSubmit').click();
            disabledLuiBtn('发送');
        }
        function disabledLuiBtn(title) {
            var submitBtns = $(".lui-component[title='" + title + "']");
            for (var i = 0; i < submitBtns.length; i++) {
                var item = submitBtns.eq(i);
                if (item.parents("div").attr("class").indexOf("lui_toolbar_btn") >= 0) {//顶部暂存按钮
                    item.removeClass("lui_toolbar_btn_on");
                    item.find(".lui-component").removeClass("lui_widget_btn_txt");
                }
                item.removeClass("lui_widget_btn");
                item.removeAttr("onclick");
                item.addClass("lui_widget_btn_disabled");
            }
        }
    </script>
</head>
<body class="lui_form_body " style="margin-top: 43px; margin-bottom: 46px;">
    <form id="form1" runat="server">
        <div>
            <div id="toolbar" data-lui-type="lui/toolbar!ToolBar" style="" class="lui-component" data-lui-cid="toolbar" data-lui-parse-init="1">
                <div>
                    <div style="display: none;">
                        <asp:TextBox ID="SavedUsers" runat="server"></asp:TextBox>
                    </div>
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
                                                        <asp:Label ID="lblFormTitle" runat="server" Text="报餐就餐异常提醒"></asp:Label>
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
                                                        <div id="passedBtn" data-lui-type="lui/toolbar!Button" data-lui-parentid="toolbar" style="" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="bookedBtn" data-lui-parse-init="7" title="发送" tabindex="0" onclick="javascript:submitForm();">
                                                            <div class="lui_toolbar_btn_l" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                                                <div class="lui_toolbar_btn_r">
                                                                    <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                        <div id="lui-id-31" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-31">发送</div>
                                                                        <asp:Button ID="btnSubmit" runat="server" Text="发送" OnClick="btnSubmit_Click" Style="display: none;" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td lui-button-container="2">
                                                    <div class="lui_toolbar_btn" data-lui-on-class="lui_toolbar_btn_on" data-lui-status-class="lui_toolbar_btn_toggle_on">
                                                        <div id="backBtn" data-lui-type="lui/toolbar!Button" style="" id="lui-id-3" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="backBtn" data-lui-parse-init="3" title="返回" tabindex="0" onclick="javascript:CCC();">
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

            <table class="tb_normal process_review_panel" width="100%">
                <tbody>
                    <tr>
                        <td class="td_normal_title" style="width: 15%;" title="所选日期不能大于当前日期">日期：</td>
                        <td colspan="2" style="text-align: left; height: 40px; padding-left: 5px;">
                            <div class="inputselectsgl" style="width: 21%; min-height: 20px;">
                                <div class="input">
                                    <asp:TextBox ID="TimeStr" runat="server" class="input_cxcalendar"></asp:TextBox>
                                </div>
                                <div class="inputdatetime" onclick="javascript:document.getElementById('TimeStr').click();"></div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_normal_title" style="width: 15%;">接收人：<span class="txtstrong">*</span></td>
                        <td colspan="2" style="text-align: left; height: 40px; padding-left: 5px;">
                            <asp:TextBox ID="ReceivedUser" runat="server" Style="width: 95%; height: 80px;" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_normal_title" style="width: 15%;">提醒内容：</td>
                        <td colspan="2" style="text-align: left; height: 40px; padding-left: 5px;">
                            <asp:Label ID="lblContent" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 40px; padding-left: 5px;"></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
