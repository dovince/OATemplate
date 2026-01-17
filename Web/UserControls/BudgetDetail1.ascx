<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BudgetDetail1.ascx.cs" Inherits="UserControls_BudgetDetail1" %>


<tr>
    <td class="auto-style115">1.工资及津贴：</td>
    <td class="auto-style5" style="background-color: #ffffff">
        <asp:TextBox ID="txt工资及津贴" runat="server" Width="150px" Style="text-align: right" ToolTip="以上季度合同登记总额为标准分配"></asp:TextBox>
        &nbsp;<strong>元</strong>&nbsp; 
                                <br />
    </td>
    <td class="auto-style115">2.工程出包费：</td>
    <td class="auto-style5" style="background-color: #ffffff">
        <asp:TextBox ID="txt工程出包费" runat="server" Width="150px" Style="text-align: right" ToolTip="按项目归集费用，列入相应项目成本。同时要与相应项目工作量配比。"></asp:TextBox>
        &nbsp;<strong>元</strong>&nbsp;
    </td>
</tr>
<tr>
    <td class="auto-style114">3.材料费：</td>
    <td class="auto-style110" style="background-color: #ffffff" colspan="">
        <asp:TextBox ID="txt材料费" runat="server" Width="150px" Style="text-align: right" ToolTip="按项目归集费用，列入相应项目成本。未明确项目的材料费，5000元以下的，列入当月合同登记总额最大的项目成本，5000元以上的，按上季度合同登记总额为标准分配"></asp:TextBox>
        &nbsp;<strong>元 </strong>
    </td>
    <td class="auto-style114">4.租赁费：</td>
    <td class="auto-style110" style="background-color: #ffffff">
        <asp:TextBox ID="txt租赁费" runat="server" Width="150px" Style="text-align: right" ToolTip="按上季度合同登记总额为标准分配"></asp:TextBox>
        &nbsp;<strong>元</strong>&nbsp; 
    </td>
</tr>
<tr>
    <td class="auto-style115">5.劳务费：</td>
    <td class="auto-style5" style="background-color: #ffffff">
        <asp:TextBox ID="txt劳务费" runat="server" Width="150px" Style="text-align: right" ToolTip="临时性人员工资，按上季度合同登记总额为标准分配。"></asp:TextBox>
        &nbsp;<strong>元&nbsp; </strong>
    </td>
    <td class="auto-style114">6.安全生产费用:</td>
    <td class="auto-style110" style="background-color: #ffffff">
        <asp:TextBox ID="txt安全生产费用" runat="server" Width="150px" Style="text-align: right" ToolTip="按项目归集费用，列入相应项目成本。"></asp:TextBox>
        &nbsp;<strong>元 </strong>
    </td>
</tr>
<tr>
    <td class="auto-style115">7.办公费：</td>
    <td class="auto-style5" style="background-color: #ffffff">
        <asp:TextBox ID="txt办公费" runat="server" Width="150px" Style="text-align: right" ToolTip="按项目归集费用，一次性发生的办公费5000元以上的，按上季度合同登记总额为标准分配"></asp:TextBox>
        &nbsp;<strong>元</strong>
    </td>
    <td class="auto-style115">8.维修费用：</td>
    <td class="auto-style5" style="background-color: #ffffff">
        <asp:TextBox ID="txt维修费用" runat="server" Width="150px" Style="text-align: right" ToolTip="指仪器设备维修费用，5000元以下的，列入当时正在开展项目成本，5000元以上的，按上季度合同登记总额为标准分配。"></asp:TextBox>
        &nbsp;<strong>元</strong>&nbsp;       
    </td>
</tr>
<tr>
    <td class="auto-style115">9.交通运输费用：</td>
    <td class="auto-style5" style="background-color: #ffffff">
        <asp:TextBox ID="txt交通运输费用" runat="server" Width="150px" Style="text-align: right" ToolTip="油料费，按项目归集费用，列入相应项目成本；修理费、年审费、保险费、税金等费用，5000元以下的，列入当月合同登记总额最大的项目成本，5000元以上的，按上季度合同登记总额为标准分配。"></asp:TextBox>
        &nbsp;<strong>元 </strong>
    </td>
    <td class="auto-style114">10.差旅费：</td>
    <td class="auto-style110" style="background-color: #ffffff">
        <asp:TextBox ID="txt差旅费" runat="server" Width="150px" Style="text-align: right" ToolTip="按项目归集费用，列入相应项目成本。"></asp:TextBox>
        &nbsp;<strong>元 </strong>
    </td>
</tr>
<tr>    
    <td class="auto-style114">11.邮电费用：</td>
    <td class="auto-style110" style="background-color: #ffffff">
        <asp:TextBox ID="txt邮电费用" runat="server" Width="150px" Style="text-align: right" ToolTip="按项目归集费用，列入相应项目成本。"></asp:TextBox>
        &nbsp;<strong>元 </strong>&nbsp;
    </td>
    <td class="auto-style114">12.其他费用：</td>
    <td class="auto-style110" style="background-color: #ffffff">
        <asp:TextBox ID="txt其他费用" runat="server" Width="150px" Style="text-align: right" ToolTip="按项目归集费用，列入相应项目成本。"></asp:TextBox>
        &nbsp;<strong>元 </strong>
    </td>
</tr>
