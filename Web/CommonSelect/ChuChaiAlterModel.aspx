<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChuChaiAlterModel.aspx.cs" Inherits="CommonSelect_ChuChaiAlterModel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Style/Style.css" rel="stylesheet" />
    <link href="../Style/Style1.css" rel="stylesheet" />
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
    <link href="../CSS/common/widget.theme.css" rel="stylesheet" />
    <link href="../CSS/cxcalendar.css" rel="stylesheet" />
    <link href="../CSS/default/easyui.css" rel="stylesheet" />
    <link href="../CSS/icon.css" rel="stylesheet" />
    <link href="../CSS/Loading.css" rel="stylesheet" />
    <script type="text/javascript" src="../JS/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../CSS/calendar.js"></script>
    <script src="../JS/jquery.easyui.min.js"></script>
    <script src="../JS/easyui-lang-zh_CN.js"></script>
    <script src="../JS/jquery.blockUI.js"></script>
    <script src="../JS/common.js?v=20230713"></script>
    <script type="text/javascript">

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
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 80%;border:0;" class="tb_normal" border="0">
                <tbody>
                    <tr>
                        <td valign="middle" style="border-right: #006633 0px; border-right: 0px; height: 30px;"></td>
                        <td align="right" valign="middle" style="border: 0px; height: 30px;">
                            <div style="display: none;">
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:ImageButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" />
                            </div>
                            <div data-lui-type="lui/toolbar!Button" style="" id="lui-id-45" class="lui-component lui_widget_btn lui_toolbar_btn_def" data-lui-cid="lui-id-45" data-lui-parse-init="44" title="提交" tabindex="0" onclick="javascript:showLoading();document.getElementById('btnSubmit').click();">
                                <div class="lui_toolbar_btn_l lui_toolbar_m" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                    <div class="lui_toolbar_btn_r">
                                        <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                            <div id="lui-id-46" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-46">提交</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div data-lui-type="lui/toolbar!Button" style="" id="lui-id-47" class="lui-component lui_widget_btn lui_toolbar_btn_def" data-lui-cid="lui-id-45" data-lui-parse-init="44" title="返回" tabindex="1" onclick="javascript:CCC();">
                                <div class="lui_toolbar_btn_l lui_toolbar_m" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                    <div class="lui_toolbar_btn_r">
                                        <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                            <div id="lui-id-48" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-46">返回</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
            <table style="width: 80%" class="tb_normal">
                <thead></thead>
                <tbody>
                    <tr>
                        <td style="width: 100px;">原表单信息</td>
                        <td>
                            <asp:Label ID="lblFormContent" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px;">变更信息说明</td>
                        <td>
                            <asp:TextBox ID="Desc" runat="server" TextMode="MultiLine" Height="200px" Width="95%" BorderColor="#d2d2d2"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
