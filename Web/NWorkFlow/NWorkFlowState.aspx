<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NWorkFlowState.aspx.cs" Inherits="NWorkFlow_NWorkFlowState" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="tb_normal treeTable" width="100%" id="auditNoteTable1">
                <tbody>
                    <tr class="tr_normal initialized">
                        <td width="15%" class="td_normal_title">工作名称
                        </td>
                        <td width="85%" class="td_normal_title">
                            <asp:Label ID="lblWorkName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="initialized">
                        <td>发起人
                        </td>
                        <td>
                            <asp:Label ID="lblUserName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="initialized">
                        <td>发起时间
                        </td>
                        <td>
                            <asp:Label ID="lblTimeStr" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <%--<tr class="initialized">
                                        <td>附件文件
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFuJianList" runat="server" ToolTip="如果无法阅读或编辑文件，请在主页面点击【关于】下载安装相关插件"></asp:Label>
                                        </td>
                                    </tr>--%>
                    <tr class="initialized">
                        <td>当前节点名称
                        </td>
                        <td>
                            <asp:Label ID="lblJieDianName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="initialized">
                        <td>当前审批用户
                        </td>
                        <td>
                            <asp:Label ID="lblShenPiUserList" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="initialized">
                        <td>当前已审批用户
                        </td>
                        <td>
                            <asp:Label ID="lblOKUserList" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <%--<tr class="initialized">
                        <td>当前待审批用户
                        </td>
                        <td>
                            <asp:Label ID="lblNotOKUserList" runat="server">无</asp:Label>
                        </td>
                    </tr>--%>
                    <tr class="initialized">
                        <td>当前状态
                        </td>
                        <td>
                            <asp:Label ID="lblStateNow" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="initialized">
                        <td>超时时间
                        </td>
                        <td>
                            <asp:Label ID="lblLateTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
