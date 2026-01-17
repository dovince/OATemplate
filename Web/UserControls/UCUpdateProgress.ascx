<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCUpdateProgress.ascx.cs" Inherits="UserControls_UCUpdateProgress" %>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
        <%--<div class="blockUI blockOverlay" style="z-index: 99999; border: none; margin: 0px; padding: 0px; width: 100%; height: 100%; top: 0px; left: 0px; background-color: rgb(85, 85, 85); opacity: 0.1; cursor: wait; position: fixed;"></div>
        <div class="blockUI blockMsg blockPage" style="z-index: 100010; position: fixed; padding: 0px; margin: 0px; width: 30%; top: 50%; left: 35%; text-align: center; color: rgb(0, 0, 0); border: 0px; cursor: wait;">
            <div class="loading-message loading-message-boxed">
                <img src="../images/loading-spinner-grey.gif" align="center"><span>&nbsp;&nbsp;加载中...</span></div>
        </div>--%>
        <iframe class="blockUI" src="about: blank" frameborder="1" scrolling="auto" style="border-top: medium none; height: 100%; border-right: medium none; width: 100%; border-bottom: medium none; position: absolute; zoom: 1; padding-bottom: 0px; padding-top: 0px; padding-left: 0px; left: 0px; filter: alpha(opacity=0); border-left: medium none; margin: 0px; z-index: 99999; top: 0px; padding-right: 0px"></iframe>
        <div class="blockUI blockOverlay" style="cursor: wait; border-top: medium none; height: 100%; border-right: medium none; width: 100%; border-bottom: medium none; position: fixed; zoom: 1; padding-bottom: 0px; padding-top: 0px; padding-left: 9px; left: 0px; filter: alpha(opacity=10); border-left: medium none; margin: 0px; z-index: 100000; top: 0px; padding-right: 0px;"></div>
        <div class="blockUI blockMsg blockPage" style="cursor: wait; border-top: 0px; border-right: 0px; width: 30%; border-bottom: 0px; position: fixed; zoom: 1; color: #000; padding-bottom: 0px; text-align: center; padding-top: 0px; padding-left: 0px; left: 35%; border-left: 0px; margin: 0px; z-index: 100011; top: 50%; padding-right: 0px;">
            <div class="loading-message loading-message-boxed">
                <img align="center" src="../images/loading-spinner-grey.gif" />
                <span>&nbsp;&nbsp;加载中...</span>
            </div>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
