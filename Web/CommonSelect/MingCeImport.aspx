<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MingCeImport.aspx.cs" Inherits="CommonSelect_MingCeImport" %>

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


        });
        function CCC() {
            window.returnValue = "";
            if (window.parent.length > 0) {
                window.parent.$('#win_select').window('close');
            }
            else {
                window.close();
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
                                                        <asp:Label ID="lblFormTitle" runat="server"></asp:Label>
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
                                                        <div id="passedBtn" data-lui-type="lui/toolbar!Button" data-lui-parentid="toolbar" style="" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="bookedBtn" data-lui-parse-init="7" title="提交" tabindex="0" onclick="javascript:document.getElementById('btnSubmit').click();">
                                                            <div class="lui_toolbar_btn_l" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                                                <div class="lui_toolbar_btn_r">
                                                                    <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                        <div id="lui-id-31" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-31">提交</div>
                                                                        <asp:Button ID="btnSubmit" runat="server" Text="提交" OnClick="btnSubmit_Click" Style="display: none;" />
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

            <table border="1" style="margin: 0 auto; border: solid 1px #ccc; border-collapse: collapse; width: 95%;">
                <tbody>
                    <tr>
                        <td style="width: 105px; height: 40px;padding-left:5px;">Excel模板：</td>
                        <td style="padding-left:5px;">
                            <asp:HyperLink ID="TemplateExcelFilePath" runat="server" Target="_blank" Style="text-decoration: underline; color: #51b6ec;"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 105px;padding-left:5px;">上传文件：</td>
                        <td colspan="2" style="text-align: center; height: 40px;padding-left:5px;">
                            <uc1:UploadFiles runat="server" ID="UploadFiles" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 40px;padding-left:5px;">注：请按照模板的栏目填充数据，模板不一致将无法正常导入数据。
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
