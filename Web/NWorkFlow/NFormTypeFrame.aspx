<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NFormTypeFrame.aspx.cs" Inherits="NWorkFlow_NFormTypeFrame" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
    <link href="../CSS/common/profile.theme.css" rel="stylesheet" />
    <link href="../CSS/common/tree_page.css" rel="stylesheet" />
    <script src="../JS/jquery-1.11.2.min.js" type="text/javascript"></script>
    <script src="../JS/lui/ctrl.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!-- topFrame -->
		<div class="lui_profile_moduleIndex_top" data-frame="topFrame"></div>
		<!-- downFrame -->
		<div class="lui_profile_moduleIndex_down" data-frame="downFrame">
			<!-- treeFrame -->
			<div class="lui_profile_moduleIndex_tree" data-frame="treeFrame">
				<iframe name="treeFrame" class="lui_profile_moduleIndex_treeframe" src="NFormTypeList.aspx" frameborder="no" border="0"></iframe>
			</div>
			<!-- ctrlFrame1 -->
			<div class="lui_profile_moduleIndex_ctrl1" data-frame="ctrl1Frame">
				<div class="lui_profile_moduleIndex_ctrlborder"></div>
				<div class="lui_profile_moduleIndex_ctrlbtn toLeft"></div>
				<div class="lui_profile_moduleIndex_ctrlbtn toRight"></div>
			</div>
			<!-- orgFrame -->
			<div class="lui_profile_moduleIndex_org" data-frame="orgFrame" style="width: 0px;">
				<iframe name="orgFrame" class="lui_profile_moduleIndex_orgFrame" src="NForm.aspx?TypeID=0" frameborder="no" border="0"></iframe>
			</div>
			<!-- ctrlFrame2 -->
			<div class="lui_profile_moduleIndex_ctrl2" data-frame="ctrl2Frame" style="width: 0px;">
				<div class="lui_profile_moduleIndex_ctrlborder"></div>
				<div class="lui_profile_moduleIndex_ctrlbtn toLeft"></div>
				<div class="lui_profile_moduleIndex_ctrlbtn toRight"></div>
			</div>
			<div class="lui_profile_moduleIndex_right" data-frame="rightFrame" style="left: 200px;">
				<div class="lui_profile_moduleIndex_viewStroke"></div>
				<div class="lui_profile_moduleIndex_view">
					<iframe name="viewFrame" class="lui_profile_moduleIndex_viewframe" src="NForm.aspx?TypeID=0" frameborder="no" border="0"></iframe>
				</div>
				<div class="lui_profile_moduleIndex_ctrl3" data-frame="ctrl3Frame"></div>
				<div class="lui_profile_moduleIndex_doc">
					<iframe name="docFrame" class="lui_profile_moduleIndex_docframe" src="" frameborder="no" border="0"></iframe>
				</div>
			</div>
		</div>
    </form>
</body>
</html>
