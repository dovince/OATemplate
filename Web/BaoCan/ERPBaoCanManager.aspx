
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERPBaoCanManager.aspx.cs" Inherits="RequireCreate_ERPBaoCanManager" %>

<!DOCTYPE html>
<html>
<head>
    <title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" type="text/css" rel="STYLESHEET">
    <link href="../Style/Style1.css" rel="stylesheet" />
    <link href="../CSS/cxcalendar.css" rel="stylesheet" />
    <script type="text/javascript" src="../JS/Manager.js"></script>
    <script type="text/javascript" src="../JS/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../CSS/calendar.js"></script>
</head>
<script type="text/javascript">
    $(document).ready(function () {
        $(".input_cxcalendar").each(function () {
            //debugger
            var a = new Calendar({
                targetCls: $(this),
                type: 'yyyy-mm-dd',
                wday: 2
            }, function (val) {
                //console.log(val);
            });
        });
    });
</script>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%; border: 0;" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="middle" style="border-bottom: #006633 1px dashed; height: 30px;">&nbsp;<img src="../images/BanKuaiJianTou.gif" />
                        <a class="hei" href="../Main/MyDesk.aspx">桌面</a>&nbsp;>>&nbsp;历史报餐记录&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="~/images/Button/BtnRefresh.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton12_Click" Style="height: 19px" />
                    </td>
                    <td style="border-bottom: #006633 1px dashed; height: 30px; text-align: right; vertical-align: middle;">
                        <asp:ImageButton ID="ImageButton4" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/Button/BtnSerch.jpg" OnClick="ImageButton4_Click" Style="height: 19px" />
                        <%--<asp:ImageButton ID="ImageButton6" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/Button/ResultSerch.jpg" OnClick="ImageButton6_Click" Visible="False" Style="height: 19px" />--%>&nbsp; &nbsp;<%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/Button/BtnAdd.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton1_Click" Style="height: 19px" />--%>
                        <%--<asp:ImageButton ID="ImageButton5" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/Button/BtnModify.jpg" OnClick="ImageButton5_Click" OnClientClick="javascript:return CheckModify();" Style="height: 19px" />
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/Button/BtnDel.jpg" OnClick="ImageButton3_Click" OnClientClick="javascript:return CheckDel();" Style="height: 19px" />--%>
                        &nbsp;&nbsp;<%--<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/Button/BtnReport.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton2_Click" Style="height: 19px" Visible="False"/>--%>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                </tr>
            </table>
            <table style="border-width: 1px; border-style: groove; width: 100%; border-collapse: collapse; border-bottom: #444444 1px solid;" border="1" rules="all" cellpadding="3" cellspacing="0">
                <tr>
                    
                    <td align="right" valign="middle" style="height: 30px; width: 5%; background-color: #D6E2F3">
                        <strong>报餐日期：</strong>
                    </td>
                    <td align="left" valign="middle" style="width: 10%;" class="auto-style1">
                        <asp:TextBox ID="BCRQ_Start" runat="server" Width="95px" class="input_cxcalendar"></asp:TextBox>
                        ~<asp:TextBox ID="BCRQ_End" runat="server" Width="95px" class="input_cxcalendar"></asp:TextBox>
                          
                    </td>
                    
                    <td align="right" valign="middle" style="height: 30px; width: 5%; background-color: #D6E2F3">
                        <strong>是否取消：</strong>
                    </td>
                    <td align="left" valign="middle" style="width: 10%;" class="auto-style1">
                        
                        <%--<asp:TextBox ID="IsCancel" runat="server" Width="90%"></asp:TextBox>--%>
                        <asp:DropDownList ID="IsCancel" runat="server" CssClass="inputsgl" Style="width: 90%;">
                            <asp:ListItem>否</asp:ListItem>
                            <asp:ListItem>是</asp:ListItem>
                            <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    
                    <td align="right" valign="middle" style="height: 30px; width: 5%; background-color: #D6E2F3">
                        <strong>用户名：</strong>
                    </td>
                    <td align="left" valign="middle" style="width: 10%;" class="auto-style1">
                        
                        <asp:TextBox ID="UserName" runat="server" Width="90%"></asp:TextBox>
                          
                    </td>
                    
                    <td align="right" valign="middle" style="height: 30px; width: 5%; background-color: #D6E2F3">
                        <strong>部门：</strong>
                    </td>
                    <td align="left" valign="middle" style="width: 10%;" class="auto-style1">
                        
                        <asp:TextBox ID="BuMen" runat="server" Width="90%"></asp:TextBox>
                          
                    </td>
                      
                </tr>
                <%--<tr>
                    <td align="right" valign="middle" style="height: 30px; width: 5%; background-color: #D6E2F3">
                        <strong>发起时间：</strong>
                    </td>
                    <td align="right" valign="middle" style="height: 30px; width: 10%; text-align: left;">
                        <asp:TextBox ID="TimeStr_Start" runat="server" Width="95px" class="input_cxcalendar"></asp:TextBox>
                        ~<asp:TextBox ID="TimeStr_End" runat="server" Width="95px" class="input_cxcalendar"></asp:TextBox>
                    </td>
                </tr>--%>
            </table>
        </div>
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="GVData" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                        BorderStyle="Groove" BorderWidth="1px" OnRowDataBound="GVData_RowDataBound" OnRowCommand="GVData_RowCommand" PageSize="15"
                        Width="100%" EnableModelValidation="True">
                        <PagerSettings Mode="NumericFirstLast" Visible="False" />
                        <PagerStyle BackColor="LightSteelBlue" HorizontalAlign="Right" />
                        <HeaderStyle BackColor="#D6E2F3" Font-Size="12px" ForeColor="Black" Height="20px" />
                        <AlternatingRowStyle BackColor="WhiteSmoke" />
                        <Columns>
                            <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="LabVisible" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ID")%>'
                                        Visible="False"></asp:Label><asp:CheckBox ID="CheckSelect" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle Width="20px" />
                                <HeaderTemplate>
                                    <input id="CheckBoxAll" onclick="CheckAll()" type="checkbox" />
                                </HeaderTemplate>
                            </asp:TemplateField>--%>
                            <%--<asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <asp:ImageButton Visible='<%# CanCancel(DataBinder.Eval(Container.DataItem, "ID")) %>' ID="ImageButton7" runat="server" ImageAlign="AbsMiddle" ImageUrl="../images/Button/btndel.png" CssClass="imgbtn" OnClientClick="return window.confirm('您确认取消该报餐吗？')" CommandName="quxiao" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "ID")%>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>--%>
                            
                            <asp:TemplateField HeaderText="是否报餐">
                                <ItemTemplate>
                                    <span style='<%# DataBinder.Eval(Container.DataItem, "IsCancel").ToString() == "否" ? "color:blue;" : "color:red;" %>'>
                                        <%# DataBinder.Eval(Container.DataItem, "IsCancel").ToString() == "否" ? "是" : "否" %>
                                    </span>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="BCRQ" HeaderText="报餐日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundField>

                            <asp:BoundField DataField="ShiJianDian" HeaderText="早餐或午餐"></asp:BoundField>
                            
                            <%--<asp:BoundField DataField="IsCancel" HeaderText="是否取消报餐"></asp:BoundField>--%>
                            
                            <asp:BoundField DataField="CancelTime" HeaderText="修改日期"></asp:BoundField>
                            
                            <asp:BoundField DataField="UserName" HeaderText="报餐人"></asp:BoundField>
                            
                            <asp:BoundField DataField="BuMen" HeaderText="报餐人部门"></asp:BoundField>
                            
                        </Columns>
                        <RowStyle HorizontalAlign="Center" Height="25px" />
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="text-align: center; border-right: black 1px; border-top: black 1px; border-left: black 1px; border-bottom: black 1px; background-color: whitesmoke;">该列表中暂时无数据！</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="border-top: #000000 1px solid; border-bottom: #000000 1px solid">
                    <asp:ImageButton ID="BtnFirst" runat="server" CommandName="First" ImageUrl="~/images/Button/First.jpg"
                        OnClick="PagerButtonClick" Style="height: 14px" />
                    <asp:ImageButton ID="BtnPre" runat="server" CommandName="Pre" ImageUrl="~/images/Button/Pre.jpg"
                        OnClick="PagerButtonClick" Style="height: 14px" />
                    <asp:ImageButton ID="BtnNext" runat="server" CommandName="Next" ImageUrl="~/images/Button/Next.jpg"
                        OnClick="PagerButtonClick" Style="height: 14px" />
                    <asp:ImageButton ID="BtnLast" runat="server" CommandName="Last" ImageUrl="~/images/Button/Last.jpg"
                        OnClick="PagerButtonClick" Style="height: 14px" />
                    &nbsp;第<asp:Label ID="LabCurrentPage" runat="server" Text="Label"></asp:Label>页&nbsp; 共<asp:Label
                        ID="LabPageSum" runat="server" Text="Label"></asp:Label>页&nbsp;
                <asp:TextBox ID="TxtPageSize" runat="server" CssClass="TextBoxCssUnder2" Height="20px"
                    Width="35px">15</asp:TextBox>
                    行每页 &nbsp; 转到第<asp:TextBox ID="GoPage" runat="server" CssClass="TextBoxCssUnder2"
                        Height="20px" Width="33px"></asp:TextBox>
                    页&nbsp;
                <asp:ImageButton ID="ButtonGo" runat="server" OnClientClick="javascript:return CheckValuePiece();" ImageUrl="~/images/Button/Jump.jpg" OnClick="ButtonGo_Click" Style="height: 18px" />
            </tr>
        </table>
    </form>
</body>
</html>


