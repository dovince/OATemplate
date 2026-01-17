<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaoCanDetail.aspx.cs" Inherits="HR_BaoCanDetail" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" rel="stylesheet" />
    <link href="../Style/Style1.css" rel="stylesheet" />
    <link href="../CSS/cxcalendar.css" rel="stylesheet" />
    <link href="../CSS/default/easyui.css" rel="stylesheet" />
    <link href="../JS/superTables/superTables.css" rel="stylesheet" />
    <script type="text/javascript" src="../JS/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../JS/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../CSS/calendar.js"></script>
    <script src="../JS/common.js?v=202306020823" type="text/javascript"></script>
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
    function sharePopWin() {
        var url = '../CommonSelect/YongCanYiChangAlert.aspx?';
        url += "_=" + Math.random();
        var options = {
            title: "发送报餐就餐异常提醒",
            url: url,
            width: 800,
            height: 500,
            onFinish: function (returnVal) {

            }
        }
        showPopwindow('btnShare', options);
        return false;
    }
</script>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="top" style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px;">&nbsp;<img src="../images/BanKuaiJianTou.gif" />
                        <a class="hei" href="../Main/MyDesk.aspx">桌面</a>&nbsp;>>&nbsp;报餐明细表&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:ImageButton ID="BtnRefresh" runat="server" ImageUrl="~/images/Button/BtnRefresh.jpg" ImageAlign="AbsMiddle" OnClick="BtnRefresh_Click" Style="height: 19px" />
                    </td>
                    <td style="border-bottom: #006633 1px; height: 30px; text-align: right; vertical-align: middle;">
                        <asp:ImageButton ID="BtnSerch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/Button/BtnSerch.jpg" OnClick="BtnSerch_Click" Style="height: 19px" />
                        <%--<asp:ImageButton ID="btnImport" runat="server" ImageUrl="../images/Button/BtnImport.jpg" ImageAlign="AbsMiddle" OnClientClick="javascript:return importPopWin();" Style="height: 19px" />--%>
                        <%--<asp:ImageButton ID="BtnAdd" runat="server" ImageUrl="~/images/Button/BtnAdd.jpg" ImageAlign="AbsMiddle" OnClick="BtnAdd_Click" Style="/*height: 19px*/" />--%>
                        <%--<asp:ImageButton ID="BtnModify" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/Button/BtnModify.jpg" OnClick="ImageButton5_Click" OnClientClick="javascript:return CheckModify();" Style="height: 19px" />--%>
                        <%--<asp:ImageButton ID="BtnDel" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/Button/BtnDel.jpg" OnClick="ImageButton3_Click" OnClientClick="javascript:return CheckDel();" Style="height: 19px" />--%>
                        &nbsp;&nbsp;
                        <asp:ImageButton ID="btnReport" runat="server" ImageUrl="~/images/Button/BtnReport.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton2_Click" Style="height: 19px" />
                        &nbsp;&nbsp;
                         <asp:LinkButton ID="btnShare" runat="server" OnClientClick="javascript:return sharePopWin();">
                            <i class="fa fa-share-alt" aria-hidden="true" >发送就餐异常提醒</i>
                        </asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;

                    </td>
                </tr>
            </table>
            <table id="search" class="sDefault " style="width: 100%; border-collapse: collapse; margin: 0px;">
                <tr>

                    <%--<td align="right" valign="middle" style="height: 30px; width: 5%;">
                        <strong>编号：</strong>
                    </td>
                    <td align="left" valign="middle" style="width: 10%;" class="auto-style1">

                        <asp:TextBox ID="BianHao" runat="server" Width="90%"></asp:TextBox>

                    </td>--%>

                    <td align="right" valign="middle" style="height: 30px; width: 5%;">
                        <strong>姓名：</strong>
                    </td>
                    <td align="left" valign="middle" style="width: 10%;">
                        <asp:TextBox ID="Name" runat="server" Width="90%"></asp:TextBox>
                    </td>
                    <td align="right" valign="middle" style="height: 30px; width: 5%;">
                        <strong>部门：</strong>
                    </td>
                    <td align="right" valign="middle" style="height: 30px; width: 10%; text-align: left;">
                        <asp:TextBox ID="Dept" runat="server" Style="text-align: left" Width="90%"></asp:TextBox>
                    </td>
                    <td align="right" valign="middle" style="height: 30px; width: 5%;">
                        <strong>日期：</strong>
                    </td>
                    <td align="right" valign="middle" style="height: 30px; width: 10%; text-align: left;">
                        <asp:TextBox ID="TimeStr_Start" runat="server" Width="95px" class="input_cxcalendar"></asp:TextBox>
                        ~<asp:TextBox ID="TimeStr_End" runat="server" Width="95px" class="input_cxcalendar"></asp:TextBox>
                    </td>
                    <td align="right" valign="middle" style="height: 30px; width: 5%;">
                        <strong>数据类型：</strong>
                    </td>
                    <td align="right" valign="middle" style="height: 30px; width: 10%; text-align: left;">
                        <asp:RadioButtonList ID="DataType" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">全部</asp:ListItem>
                            <asp:ListItem>正常</asp:ListItem>
                            <asp:ListItem>异常</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%">
            <tr>
                <td>
                    <div id="div_container" style="text-align: center;">
                        <div id="my_div" class="fakeContainer first_div" style="padding: 1px">
                            <asp:GridView ID="GVData" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                CssClass="sDefault" BorderStyle="Groove" BorderWidth="1px" OnRowDataBound="GVData_RowDataBound" PageSize="100"
                                Width="100%" EnableModelValidation="True">
                                <PagerSettings Mode="NumericFirstLast" Visible="False" />
                                <PagerStyle BackColor="LightSteelBlue" HorizontalAlign="Right" />
                                <HeaderStyle BackColor="#D6E2F3" Font-Size="12px" ForeColor="Black" Height="20px" />
                                <AlternatingRowStyle BackColor="WhiteSmoke" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="LabVisible" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ID")%>'
                                                Visible="False"></asp:Label><asp:CheckBox ID="CheckSelect" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                        <HeaderTemplate>
                                            <input id="CheckBoxAll" onclick="CheckAll()" type="checkbox" />
                                        </HeaderTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="row" HeaderText="序号" HeaderStyle-Width="60px"></asp:BoundField>
                                    
                                    <asp:TemplateField HeaderText="姓名">
                                        <ItemTemplate>
                                            <%# !string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "Name").ToString()) ? DataBinder.Eval(Container.DataItem, "Name") : DataBinder.Eval(Container.DataItem, "UserName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="Department" HeaderText="部门"></asp:BoundField>
                                    
                                    <asp:TemplateField HeaderText="日期">
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(!string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "RecordDate").ToString()) ? DataBinder.Eval(Container.DataItem, "RecordDate") : DataBinder.Eval(Container.DataItem, "BCRQ")).ToString("yyyy-MM-dd") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="星期">
                                        <ItemTemplate>
                                            <%# GetDayOfWeek(Convert.ToDateTime(!string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "RecordDate").ToString()) ? DataBinder.Eval(Container.DataItem, "RecordDate") : DataBinder.Eval(Container.DataItem, "BCRQ"))) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="用餐时段">
                                        <ItemTemplate>
                                            <%# !string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "CanShi").ToString()) ? DataBinder.Eval(Container.DataItem, "CanShi") : DataBinder.Eval(Container.DataItem, "ShiJianDian") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="是否报餐">
                                        <ItemTemplate>
                                            <%# GetBaoCanState(DataBinder.Eval(Container.DataItem, "UserName").ToString(), DataBinder.Eval(Container.DataItem, "Name").ToString()) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="是否用餐">
                                        <ItemTemplate>
                                            <%# !string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "Name").ToString()) ? "<span style='color:#0000ff;'>是</span>" : "<span style='color:#999999;'>否</span>" %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="是否异常">
                                        <ItemTemplate>
                                            <%# GetIsError(DataBinder.Eval(Container.DataItem, "UserName").ToString(), DataBinder.Eval(Container.DataItem, "Name").ToString()) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

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
                        </div>
                    </div>
                </td>
            </tr>
            <tr id="foot">
                <td style="text-align: center;">共<asp:Label ID="Labelrowcount" runat="server" Text="0"></asp:Label>
                    条记录&nbsp;
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
                    Width="35px">100</asp:TextBox>
                    行每页 &nbsp; 转到第<asp:TextBox ID="GoPage" runat="server" CssClass="TextBoxCssUnder2"
                        Height="20px" Width="33px"></asp:TextBox>
                    页&nbsp;
                <asp:ImageButton ID="ButtonGo" runat="server" OnClientClick="javascript:return CheckValuePiece();" ImageUrl="~/images/Button/Jump.jpg" OnClick="ButtonGo_Click" Style="height: 18px" />
                    <asp:HiddenField ID="HdfPageSum" runat="server" />
            </tr>
        </table>
    </form>
</body>
</html>
