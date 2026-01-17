<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NFormList.aspx.cs" Inherits="NWorkFlow_NFormList" %>

<html>
<head runat="server">
    <title>
        <%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
    <link href="../CSS/common/profile.theme.css" rel="stylesheet" />
    <link href="../CSS/common/tree_page.css" rel="stylesheet" />
    <script src="../JS/jquery-1.11.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ListTreeViewt0").addClass('TVN_TreeNode_Current');
            $("#ListTreeView a").click(function () {
                $('#ListTreeView a').removeClass('TVN_TreeNode_Current');
                $(this).addClass('TVN_TreeNode_Current');
            });
        });

        var a;
        function CheckValuePiece() {
            if (window.document.form1.GoPage.value == "") {
                alert("请输入跳转的页码！");
                window.document.form1.GoPage.focus();
                return false;
            }
            return true;
        }
        function CheckAll() {
            if (a == 1) {
                for (var i = 0; i < window.document.form1.elements.length; i++) {
                    var e = form1.elements[i];
                    e.checked = false;
                }
                a = 0;
            }
            else {
                for (var i = 0; i < window.document.form1.elements.length; i++) {
                    var e = form1.elements[i];
                    e.checked = true;
                }
                a = 1;
            }
        }
        function CheckDel() {
            var number = 0;
            for (var i = 0; i < window.document.form1.elements.length; i++) {
                var e = form1.elements[i];
                if (e.Name != "CheckBoxAll") {
                    if (e.checked == true) {
                        number = number + 1;
                    }
                }
            }
            if (number == 0) {
                alert("请选择需要删除的项！");
                return false;
            }
            if (window.confirm("你确认删除吗？")) {
                return true;
            }
            else {
                return false;
            }
        }

        function CheckModify() {
            var Modifynumber = 0;
            for (var i = 0; i < window.document.form1.elements.length; i++) {
                var e = form1.elements[i];
                if (e.Name != "CheckBoxAll") {
                    if (e.checked == true) {
                        Modifynumber = Modifynumber + 1;
                    }
                }
            }
            if (Modifynumber == 0) {
                alert("请至少选择一项！");
                return false;
            }
            if (Modifynumber > 1) {
                alert("只允许选择一项！");
                return false;
            }

            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="background-color: white; vertical-align: middle; height: 42px; padding-top: 13px; padding-left: 10px; border-bottom: 1px solid #d7d7d7;">
            <a id="linkToList" runat="server" target="_parent" href="../NWorkFlow/NWorkFlowFrame.aspx" style="text-decoration: none;">流程管理</a>
        </div>
        <div class="lui_profile_moduleIndex_treecontainer" style="background-color: white;">
            <asp:TreeView ID="ListTreeView" runat="server" ExpandDepth="0" ShowLines="True">
                <ParentNodeStyle HorizontalPadding="2px" />
                <RootNodeStyle HorizontalPadding="2px" />
                <LeafNodeStyle HorizontalPadding="2px" />
            </asp:TreeView>
        </div>
    </form>
</body>
</html>
