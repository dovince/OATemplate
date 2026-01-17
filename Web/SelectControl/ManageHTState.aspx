<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageHTState.aspx.cs" Inherits="SelectControl_ManageHTState" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>选择需要修改的项目</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../CSS/Loading.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
    <link href="../CSS/common/widget.theme.css" rel="stylesheet" />
    <link href="../CSS/common/process_tab_main.css" rel="stylesheet" />
    <link href="../CSS/default/easyui.css" rel="stylesheet" />
    <link href="../CSS/cxcalendar.css" rel="stylesheet" />
    <link href="../CSS/icon.css" rel="stylesheet" />
    <link href="../CSS/common/form.theme.css" rel="stylesheet" />
    <link href="../CSS/common/dialog.theme.css" rel="stylesheet" />
    <link href="../CSS/common/dnd.css" rel="stylesheet" />
    <link href="../CSS/common/upload.css" rel="stylesheet" />
    <script src="../JS/jquery-1.11.2.min.js" type="text/javascript"></script>
    <script src="../CSS/calendar.js" type="text/javascript"></script>
    <script src="../JS/jquery.easyui.min.js"></script>
    <script src="../JS/jquery.blockUI.js" type="text/javascript"></script>
    <script src="../JS/common.js?v=202306020823" type="text/javascript"></script>
    <script src="../JS/lui/tabcontent.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#XMName").bind("keyup", function () {
                var name = $(this).val();
                if (name == null || name == "") {
                    hideMessage();
                }
                else {
                    $.ajax({
                        cache: false,
                        type: "get",
                        data: { f: "CheckXMQQName", name: escape(name) },
                        dataType: "text",
                        url: "../Services/Services.ashx",
                        success: function (data) {
                            var res = eval('(' + data + ')');
                            if (res.Data == "true") {
                                showMessage('×系统已有类似项目登记信息，请进行"重复性检查"确认是否重复登记！');
                            }
                            else if (res.Data == "false") {
                                showSuccessMessage('√该项目可进行登记！');
                            } else {
                                showMessage("×系统已有类似项目【" + res.Data + "】，请确认是否重复登记！");
                            }
                        }
                    });
                }
            });
        });
        function ccc() {
            if (window.parent.length > 0) {
                window.parent.$('#win_select').window('close');
            }
            else {
                window.close();
            }
        }
    </script>
</head>
<body class="lui_form_body " style="margin-top: 43px; margin-bottom: 46px; width: 90%;">
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
                                                        <div id="backBtn" data-lui-type="lui/toolbar!Button" style="" id="lui-id-3" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="backBtn" data-lui-parse-init="3" title="返回" tabindex="0" onclick="javascript:ccc();">
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
                                                        <div class="lui_item_txt" title="项目台账" onclick="javascript:void(0);">项目台账</div>
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
                                                            <%--基本信息开始--%>
                                                            <div>
                                                                <div data-lui-mark="panel.content.inside" class="lui_panel_content_inside">
                                                                    <div data-lui-type="lui/panel!Content" style="" id="lui-id-12" class="lui-component" data-lui-cid="lui-id-12" data-lui-parse-init="12">
                                                                        <div class="lui_form_title_frame">
                                                                            <div class="lui_form_subject">
                                                                            </div>
                                                                            <div class="lui_form_baseinfo">
                                                                            </div>
                                                                        </div>
                                                                        <table class="tb_normal" width="100%">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class="td_normal_title" width="15%">项目编号
                                                                                    </td>
                                                                                    <td colspan="3" width="85%">
                                                                                        <asp:Label ID="XMBH" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td_normal_title" width="15%">项目名称
                                                                                    </td>
                                                                                    <td colspan="3" width="85%">
                                                                                        <asp:Label ID="XMName" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td_normal_title" width="15%">合同状态
                                                                                    </td>
                                                                                    <td colspan="3" width="85%">
                                                                                        <asp:RadioButtonList ID="HTState" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                                            <asp:ListItem Selected="True">谈判未签订</asp:ListItem>
                                                                                            <asp:ListItem>签订流程中</asp:ListItem>
                                                                                            <asp:ListItem>已签订</asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                                <div data-lui-mark="panel.content.operation" class="lui_portlet_operations clearfloat"></div>
                                                            </div>

                                                            <%--基本信息结束 --%>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="lui_tabpage_float_footer_l">
                                                    <div class="lui_tabpage_float_footer_r">
                                                        <div class="lui_tabpage_float_footer_c"></div>
                                                    </div>
                                                </div>
                                            </div>
                                            <iframe frameborder="0" class="lui_tabpage_float_navs_mark" scrolling="no"></iframe>
                                            <div class="lui_tabpage_float_navs">
                                                <div class="lui_tabpage_float_navs_l">
                                                    <div class="lui_tabpage_float_navs_r">
                                                        <div class="lui_tabpage_float_navs_c" style="max-width: 1200px; text-align: center; padding-top: 5px; padding-bottom: 5px; height: 40px;">
                                                            <div id="lui-id-175" class="lui-component lui_widget_btn lui_toolbar_btn_def" data-lui-cid="lui-id-170" title="提交" onclick="javascript:document.getElementById('btnSubmit').click();">
                                                                <div class="lui_toolbar_btn_l lui_toolbar_m" data-lui-mark="toolbar_button_inner" style="text-align: center; height: 33px; line-height: 33px;">
                                                                    <div class="lui_toolbar_btn_r">
                                                                        <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                            <div id="lui-id-176" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-171">提交</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="lui-id-183" class="lui-component lui_widget_btn lui_toolbar_btn_def" data-lui-cid="lui-id-170" title="返回" onclick="javascript:ccc();">
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
