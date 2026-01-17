<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RebuildForm.ascx.cs" Inherits="UserControls_RebuildForm" %>
<div>
    <div data-lui-mark="panel.content.inside" class="lui_panel_content_inside rebuildform">
        <div data-lui-type="lui/panel!Content" id="lui-id-13" class="lui-component" data-lui-cid="lui-id-13" data-lui-parse-init="15" style='<%=(ShowTransform ? "" : "display: none;")%>' >	
            <div class="lui_form_title_frame" data-index="1" data-title="新表单">
                <div class="lui_form_subject">
                    <asp:Label ID="lblFormTitle" runat="server"></asp:Label>
                </div>
                <br>
                <table class="tb_normal" width="100%">
                    <tbody>
                        <%= BaseInfoBodyHtml %>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="lui-component" style="margin-top:5px; <%=(ShowTransform ? "display: none;" : "")%>">
            <div class="lui_form_title_frame" data-index="2" data-title="原表单">
                <asp:Label runat="server" ID="Label_FormContent"></asp:Label>
            </div>
            <script type="text/javascript">
                    <%=PiLiangSet%>
            </script>
        </div>
    </div>
</div>
