<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERPListOfExpendedManager.aspx.cs" Inherits="Financial_ERPListOfExpendedManager" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" rel="stylesheet" />
    <link href="../Style/Style1.css" rel="stylesheet" />
    <link href="../CSS/cxcalendar.css" rel="stylesheet" />
    <link href="../JS/Manager.js" rel="stylesheet" />
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
                    <td valign="middle" style="border-bottom: #006633 1px ; height: 30px;">&nbsp;<img src="../images/BanKuaiJianTou.gif" />
                        <a class="hei" href="../Main/MyDesk.aspx">桌面</a>&nbsp;>>&nbsp;费用成本报销&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="~/images/Button/BtnRefresh.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton12_Click" Style="height: 19px" />
                    </td>
                    <td style="border-bottom: #006633 1px ; height: 30px; text-align: right; vertical-align: middle;">
                        <asp:ImageButton ID="ImageButton4" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/Button/BtnSerch.jpg" OnClick="ImageButton4_Click" Style="height: 19px" />
                        &nbsp; &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/Button/BtnAdd.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton1_Click" Style="height: 19px" />
                        <asp:ImageButton ID="ImageButton5" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/Button/BtnModify.jpg" OnClick="ImageButton5_Click" OnClientClick="javascript:return CheckModify();" Style="height: 19px" />
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/Button/BtnDel.jpg" OnClick="ImageButton3_Click" OnClientClick="javascript:return CheckDel();" Style="height: 19px" />
                        &nbsp;&nbsp;<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/Button/BtnReport.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton2_Click" Style="height: 19px" Visible="False"/>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                </tr>
            </table>
            <table style="border-width: 1px; border-style: groove; width: 100%; border-collapse: collapse; border-bottom: #444444 1px solid;" border="1" rules="all" cellpadding="3" cellspacing="0">
                <tr>
                    
                    <td align="right" valign="middle" style="height: 30px; width: 5%; background-color: #f6f6f6">
                        <strong>名称：</strong>
                    </td>
                    <td align="left" valign="middle" style="width: 10%;" class="auto-style1">
                        <asp:TextBox ID="WorkName" runat="server" Width="90%"></asp:TextBox>
                    </td>
                    
                    <td align="right" valign="middle" style="height: 30px; width: 5%; background-color: #f6f6f6">
                        <strong>合计：</strong>
                    </td>
                    <td align="left" valign="middle" style="width: 10%;" class="auto-style1">
                        <asp:TextBox ID="Amount" runat="server" Width="90%"></asp:TextBox>
                    </td>
                    
                    <td align="right" valign="middle" style="height: 30px; width: 5%; background-color: #f6f6f6">
                        <strong>部门：</strong>
                    </td>
                    <td align="left" valign="middle" style="width: 10%;" class="auto-style1">
                        <asp:TextBox ID="Department" runat="server" Width="90%"></asp:TextBox>
                    </td>
                    
                    <td align="right" valign="middle" style="height: 30px; width: 5%; background-color: #f6f6f6">
                        <strong>登记人：</strong>
                    </td>
                    <td align="left" valign="middle" style="width: 10%;" class="auto-style1">
                        <asp:TextBox ID="Username" runat="server" Width="90%"></asp:TextBox>
                    </td>
                      
                </tr>
                <tr>
                    <td align="right" valign="middle" style="height: 30px; width: 5%; background-color: #f6f6f6">
                        <strong>发起人：</strong>
                    </td>
                    <td align="left" valign="middle" style="width: 10%;" class="auto-style1">
                        <asp:TextBox ID="TextBox1" runat="server" Width="90%"></asp:TextBox>
                    </td>
                    <td align="right" valign="middle" style="height: 30px; width: 5%; background-color: #f6f6f6">
                        <strong>发起人单位：</strong>
                    </td>
                    <td align="right" valign="middle" style="height: 30px; width: 10%; text-align: left;">
                        <asp:TextBox ID="TextBox2" runat="server" Style="text-align: left" Width="90%"></asp:TextBox>
                    </td>
                    <td align="right" valign="middle" style="height: 30px; width: 5%; background-color: #f6f6f6">
                        <strong>发起时间：</strong>
                    </td>
                    <td align="right" valign="middle" style="height: 30px; width: 10%; text-align: left;">
                        <asp:TextBox ID="TimeStr_Start" runat="server" Width="95px" class="input_cxcalendar"></asp:TextBox>
                        ~<asp:TextBox ID="TimeStr_End" runat="server" Width="95px" class="input_cxcalendar"></asp:TextBox>
                    </td>
                    <td align="right" valign="middle" style="height: 30px; width: 5%; background-color: #f6f6f6">
                        <strong>当前状态：</strong></td>
                    <td align="right" valign="middle" style="height: 30px; width: 10%; text-align: left;">
                        <asp:DropDownList ID="StateNow" runat="server">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="正在办理">正在办理</asp:ListItem>
                            <asp:ListItem Value="正常结束">正常结束</asp:ListItem>
                            <asp:ListItem Value="已被驳回">已被驳回</asp:ListItem>
                            <asp:ListItem Value="不通过">不通过</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="GVData" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="tb_normal" OnRowDataBound="GVData_RowDataBound" PageSize="15"
                        Width="100%" EnableModelValidation="True">
                        <PagerSettings Mode="NumericFirstLast" Visible="False" />
                        <PagerStyle BackColor="LightSteelBlue" HorizontalAlign="Right" />
                        <HeaderStyle BackColor="#D6E2F3" Font-Size="12px" ForeColor="Black" Height="20px" />
                        <AlternatingRowStyle BackColor="WhiteSmoke" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="LabVisible" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NWorkToDoID")%>'
                                        Visible="False"></asp:Label><asp:CheckBox ID="CheckSelect" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle Width="20px" />
                                <HeaderTemplate>
                                    <input id="CheckBoxAll" onclick="CheckAll()" type="checkbox" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="合同编号--项目名称">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="False" ForeColor="#0672CA" ToolTip='<%# DataBinder.Eval(Container.DataItem, "BeiYong1")%>' NavigateUrl='<%# "NWorkToDoView.aspx?ID="+ ZWL.Common.PublicMethod.EncryptParam(DataBinder.Eval(Container.DataItem, "NWorkToDoID"))%>'><%# DataBinder.Eval(Container.DataItem, "BeiYong1").ToString().Trim().Length>40?(DataBinder.Eval(Container.DataItem, "BeiYong1").ToString().Trim().Substring(0,40)+"..."):DataBinder.Eval(Container.DataItem, "BeiYong1").ToString().Trim()%></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="WorkName" HeaderText="名称"></asp:BoundField>
                            
                            <asp:BoundField DataField="Amount" HeaderText="合计"></asp:BoundField>
                            
                            <asp:BoundField DataField="Department" HeaderText="部门"></asp:BoundField>
                            
                            <asp:BoundField DataField="Username" HeaderText="登记人"></asp:BoundField>
                            
                            <asp:BoundField DataField="TimeStr" HeaderText="发起时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundField>
                            <asp:TemplateField HeaderText="节点名称">
                                <ItemTemplate>
                                    <%# ZWL.DBUtility.DbHelperSQL.GetSHSL("select top 1 WorkFlowName from ERPNWorkFlow where ID=" + DataBinder.Eval(Container.DataItem, "WorkFlowID").ToString())%>--[<asp:HyperLink ID="HyperLink11" runat="server" Font-Underline="True" Target="_blank" NavigateUrl='<%# "NWorkFlowReView.aspx?WorkFlowID="+ DataBinder.Eval(Container.DataItem, "WorkFlowID")+"&FormID="+ DataBinder.Eval(Container.DataItem, "FormID") %>' ForeColor="Blue" ToolTip="点击查看流程图示"><%# DataBinder.Eval(Container.DataItem, "JieDianName")%></asp:HyperLink>]
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ShenPiUserList" HeaderText="审批用户"></asp:BoundField>
                            <asp:BoundField DataField="OKUserList" HeaderText="已审批用户"></asp:BoundField>
                            <asp:BoundField DataField="LateTime" HeaderText="超时时间" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-Width="80px"></asp:BoundField>
                            <asp:BoundField DataField="StateNow" HeaderText="当前状态" ItemStyle-Width="60px"></asp:BoundField>
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
                <td style="text-align:center;">
                    <asp:ImageButton ID="BtnFirst" runat="server" CommandName="First" ImageUrl="~/images/Button/First.jpg"
                        OnClick="PagerButtonClick" Style="height: 14px" />
                    <asp:ImageButton ID="BtnPre" runat="server" CommandName="Pre" ImageUrl="~/images/Button/Pre.jpg"
                        OnClick="PagerButtonClick" Style="height: 14px" />
                    <asp:ImageButton ID="BtnNext" runat="server" CommandName="Next" ImageUrl="~/images/Button/Next.jpg"
                        OnClick="PagerButtonClick" Style="height: 14px" />
                    <asp:ImageButton ID="BtnLast" runat="server" CommandName="Last" ImageUrl="~/images/Button/Last.jpg"
                        OnClick="PagerButtonClick" Style="height: 14px" />
                    &nbsp;第<asp:Label ID="LabCurrentPage" runat="server" Text="0"></asp:Label>页&nbsp; 共<asp:Label
                        ID="LabPageSum" runat="server" Text="0"></asp:Label>页&nbsp;
                    <asp:HiddenField ID="HdfPageSum" runat="server" />
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


