<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HTDaoZhangList.aspx.cs" Inherits="CommonSelect_HTDaoZhangList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center; font-weight: bold;">
            <h2>合同到账明细</h2>
            <div style="width: 780px; margin: 5px auto;">
                <asp:GridView ID="GVData" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="tb_normal" OnPreRender="GVData_PreRender">
                    <HeaderStyle Font-Size="12px" Height="20px" HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="序号" InsertVisible="False">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID").ToString()%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField ItemStyle-Width="60px" DataField="Type" HeaderText="收款类型" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="KaiPiaoJE" HeaderText="开票金额" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SQTime" HeaderText="开票日期" DataFormatString="{0:yyyy-MM-dd}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DaoZhangJE" HeaderText="到账金额" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DaoZhangTime" HeaderText="到账日期" DataFormatString="{0:yyyy-MM-dd}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
