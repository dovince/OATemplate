<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadForm.aspx.cs" Inherits="NWorkFlow_UploadForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
    <title></title>
    <script type="text/javascript">
        $(function () {
            parent.document.getElementById("Text578754774").value = document.getElementById("HiddenField_ZhengWenListName").value;
        });
        function deleteFile(url) {
            var zw = document.getElementById("HiddenField_ZhengWenList").value;
            var zwlist = zw.split("|");
            var newzw = "";
            for (var i = 0; i < zwlist.length; i++) {
                if (zwlist[i] != url) {
                    newzw += zwlist[i] + "|";
                }
            }
            if (newzw.length > 0) {
                newzw = newzw.substr(0, newzw.length - 1);
            }
            document.getElementById("HiddenField_Deletefile").value = url;
            document.getElementById("HiddenField_ZhengWenList").value = newzw;
            document.getElementById("ImageButton31").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="display:none;">
            <asp:HiddenField ID="HiddenField_ZhengWenListName" runat="server" />
            <asp:HiddenField ID="HiddenField_Deletefile" runat="server" />
            <asp:ImageButton ID="ImageButton31" runat="server" CausesValidation="False" ImageAlign="AbsMiddle" ImageUrl="../images/Button/DelFile.jpg" OnClick="ImageButton31_Click" />
        </div>
        <div>
            <div class="lui_tabpage_float_content" style="">
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
                                                            <div class="" title="删除" onclick="javascript:deleteFile('<%=item.NowName %>');">
                                                                <span style="text-decoration: underline; color: blue; display: inline-block; cursor: pointer;">删除</span>
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
                                                <tr class="tr_normal initialized">
                                                    <td class="td_normal_title">
                                                    </td>
                                                    <td class="td_normal_title" colspan="3">
                                                        <asp:HiddenField ID="HiddenField_ZhengWenList" runat="server" />
                                                        <asp:FileUpload ID="FileUpload2" runat="server" Width="350px" Height="25px" />
                                                        <asp:ImageButton ID="ImageButton21" runat="server" CausesValidation="False" ImageAlign="AbsMiddle" ImageUrl="../images/Button/UpLoad.jpg" OnClick="ImageButton21_Click" /><br />
                                                        <%--<asp:CheckBoxList ID="CheckBoxList2" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                                                                                        </asp:CheckBoxList>
                                                                                        &nbsp;<asp:ImageButton ID="ImageButton31" runat="server" CausesValidation="False" ImageAlign="AbsMiddle" ImageUrl="../images/Button/DelFile.jpg" OnClick="ImageButton31_Click" />
                                                                                        &nbsp; &nbsp;&nbsp;<asp:ImageButton ID="ImageButton51" runat="server" CausesValidation="False" ImageAlign="AbsMiddle" ImageUrl="~/images/Button/ReadFile.gif" OnClick="ImageButton51_Click" />
                                                                                        &nbsp; &nbsp;&nbsp;<asp:ImageButton ID="ImageButton61" runat="server" CausesValidation="False" ImageAlign="AbsMiddle" ImageUrl="~/images/Button/EditFile.gif" OnClick="ImageButton61_Click" />--%>
                                                    </td>
                                                </tr>
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
        </div>
    </form>
</body>
</html>
