<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NWorkFlowLog.aspx.cs" Inherits="NWorkFlow_NWorkFlowLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
    <script src="../JS/jquery-1.11.2.min.js" type="text/javascript"></script>
    <title></title>
    <script type="text/javascript">
        $(document).ready(function () {
            var id = $(window.frameElement).attr("id");
            if (id == "SP_IFrame") {
                $(window.frameElement).height($("#auditNoteTable").height() + 8);
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="tb_normal treeTable" width="100%" id="auditNoteTable" style="text-align: center;">
                <tbody>
                    <tr class="tr_normal initialized">
                        <td width="6%" class="td_normal_title">序号
                        </td>
                        <td width="15%" class="td_normal_title">节点名称
                        </td>
                        <td width="10%" class="td_normal_title">操作者
                        </td>
                        <td width="10%" class="td_normal_title">操作
                        </td>
                        <td width="18%" class="td_normal_title">时间
                        </td>
                        <td width="40%" class="td_normal_title">处理意见
                        </td>

                    </tr>
                    <%=WorkLogHelper.GetProcessingHtml(ZWL.Common.PublicMethod.CheckInt(ZWL.Common.PublicMethod.GetDecryptParam("ID")), false) %>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
