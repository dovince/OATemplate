<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Costassign.aspx.cs" Inherits="Financial_Costassign" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" rel="stylesheet" />
    <link href="../Style/Style1.css" rel="stylesheet" />
    <link href="../CSS/cxcalendar.css" rel="stylesheet" />
    <link href="../CSS/default/easyui.css" rel="stylesheet" />
    <link href="../CSS/icon.css" rel="stylesheet" />
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
    <script type="text/javascript" src="../JS/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../CSS/calendar.js"></script>
    <script src="../JS/jquery.easyui.min.js"></script>
    <script src="../JS/easyui-lang-zh_CN.js"></script>
    <script src="../JS/jquery.blockUI.js"></script>
    <script src="../JS/OAExtent.js"></script>
    <style type="text/css">
        .auto-style2 {
            text-align: right;
            width: 10%;
            height: 39px;
        }
    </style>
    <style type="text/css">
        body {
            margin: 0px;
            padding: 0px;
            font-size: 12px;
        }

        .table {
            border-collapse: collapse;
            height: 24px;
            line-height: 24px;
            text-align: center;
        }

            .table tr {
                border-left: solid 1px #FFF;
                border-right: solid 1px #000000;
            }

                .table tr td {
                    border: solid 1px #000000;
                    /*text-align: center;*/
                }

        .classid {
            background-image: url(../Image/Tab/Tab_14.gif);
            height: 26px;
            background-repeat: repeat-x;
            text-align: center;
        }

        .auto-style4 {
            width: 10%;
            height: 39px;
        }
    </style>
</head>
<script type="text/javascript">
    var a;
    function CheckValuePiece() {
        if (window.document.form1.GoPage.value == "") {
            alert("请输入跳转的页码！");
            window.document.form1.GoPage.focus();
            return false;
        }
        return true;
    }

    $(document).ready(function () {
        var trquery = $("#querytr");
        var isquery = $("#HiddenField_query")[0].value.toString();;
        if (isquery == "false") {
            trquery.hide();
        }
        else {
            $('#btn_query').attr({ value: "隐藏查询" });
        }

        $('#btn_query').click(function () {
            if (trquery.is(':hidden')) {
                trquery.show();
                $('#btn_query').attr({ value: "隐藏查询" });
            }
            else {
                trquery.hide();
                $('#btn_query').attr({ value: "显示查询" });
            }
        });
        $('#Button_Query').click(function () {
            trquery.show();
            $('#btn_query').attr({ value: "隐藏查询" });
            $("#HiddenField_query")[0].value = "true";
        })
        $(".input_cxcalendar").each(function () {
            //debugger
            var a = new Calendar({
                targetCls: $(this),
                type: 'yyyy-mm-dd',
                wday: 1
            }, function (val) {
                //console.log(val);
            });
        });

    });

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

</script>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="tb_normal" style="width: 100%">
                <tr>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px;">&nbsp;<img src="../images/BanKuaiJianTou.gif" />
                        财务管理&nbsp;&gt;&gt;&nbsp;项目成本费用分配&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="../images/Button/BtnRefresh.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton12_Click" Style="height: 19px" />
                        <asp:HiddenField ID="HiddenField_query" runat="server" />
                        <asp:HiddenField ID="HdfPageSum" runat="server" />
                    </td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px;">
                        <input id="btn_query" type="button" value="显示查询" />&nbsp;&nbsp; 
                        
                         <asp:Button ID="btnout" runat="server" Style="text-align: center" Text="导出成本费用分配总表" OnClick="btnout_Click" OnClientClick="CheckExcelExportOver()" />
                        &nbsp;
                         &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../images/Button/BtnReport.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton2_Click" Style="height: 19px" />&nbsp;&nbsp;&nbsp;&nbsp;</td>
                </tr>
            </table>
            <table class="tb_normal" style="width: 100%">
                <tr id="querytr">
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; background-color: #f6f6f6" class="auto-style4"><strong>项目名称:</strong></td>
                    <td align="left" valign="middle" style="border-bottom: #006633 1px;" class="auto-style4">

                        <asp:TextBox ID="TextBox_xmname" runat="server" Style="text-align: left" Width="243px"></asp:TextBox>

                        <td align="right" valign="middle" style="border-bottom: #006633 1px; background-color: #f6f6f6" class="auto-style4"><strong>部门：</strong></td>
                        <td align="right" valign="middle" style="border-bottom: #006633 1px; text-align: left;" class="auto-style4">
                            <asp:DropDownList ID="DropDownListBM" runat="server" Width="150px" OnSelectedIndexChanged="DropDownListBM_SelectedIndexChanged" AutoPostBack="true">
                                
                            </asp:DropDownList>


                        </td>
                        <td valign="middle" style="border-bottom: #006633 1px; background-color: #f6f6f6" class="auto-style2"><strong>科目类别：</strong></td>
                        <td align="right" valign="middle" style="border-bottom: #006633 1px; text-align: left;" class="auto-style4">

                            <asp:DropDownList ID="DropDownListKM" runat="server" Width="150px" OnSelectedIndexChanged="DropDownListKM_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Selected="True" Value="工资及津贴">1.工资及津贴</asp:ListItem>
                                <asp:ListItem Value="节日补贴">2.节日补贴</asp:ListItem>
                                <asp:ListItem Value="养老统筹">3.养老统筹</asp:ListItem>
                                <asp:ListItem Value="福利费">4.福利费</asp:ListItem>
                                <asp:ListItem Value="劳动保护费">5.劳动保护费</asp:ListItem>
                                <asp:ListItem Value="住房公积金">6.住房公积金</asp:ListItem>
                                <asp:ListItem Value="住房补贴">7.8.住房补贴</asp:ListItem>
                                <asp:ListItem Value="材料费">9.材料费</asp:ListItem>
                                <asp:ListItem Value="工程出包费">10.工程出包费</asp:ListItem>
                                <asp:ListItem Value="固定资产">11.固定资产</asp:ListItem>
                                <asp:ListItem Value="办公费">12.办公费</asp:ListItem>
                                <asp:ListItem Value="差旅费">13.差旅费</asp:ListItem>
                                <asp:ListItem Value="水电费">14.水电费</asp:ListItem>
                                <asp:ListItem Value="物业管理费">15.物业管理费</asp:ListItem>
                                <asp:ListItem Value="交通运输费用">16.交通运输费用</asp:ListItem>
                                <asp:ListItem Value="邮电费用">17.邮电费用</asp:ListItem>
                                <asp:ListItem Value="维修费用">18.维修费用</asp:ListItem>
                                <asp:ListItem Value="会议费">19.会议费</asp:ListItem>
                                <asp:ListItem Value="培训费">20.培训费</asp:ListItem>
                                <asp:ListItem Value="业务招待费">21.业务招待费</asp:ListItem>
                                <asp:ListItem Value="劳务费">22.劳务费</asp:ListItem>
                                <asp:ListItem Value="租赁费">23.租赁费</asp:ListItem>
                                <asp:ListItem Value="税金及附加">24.税金及附加</asp:ListItem>
                                <asp:ListItem Value="安全生产费用">25.安全生产费用</asp:ListItem>
                                <asp:ListItem Value="工会经费">26.工会经费</asp:ListItem>
                                <asp:ListItem Value="其它费用">27.其他费用</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td valign="middle" style="border-bottom: #006633 1px; text-align: right; background-color: #f6f6f6; font-weight: 700;" class="auto-style4">时间：</td>
                        <td align="right" valign="middle" style="border-bottom: #006633 1px; text-align: left;" class="auto-style4">从：<asp:TextBox ID="TextBox_Start" runat="server" Width="100px" class="input_cxcalendar"></asp:TextBox>
                            <td valign="middle" style="border-bottom: #006633 1px; text-align: left;" class="auto-style4">到：<asp:TextBox ID="TextBox_End" runat="server" Width="98px" class="input_cxcalendar"></asp:TextBox>
                            </td>
                            <td align="middle" valign="middle" style="border-bottom: #006633 1px;" class="auto-style4">
                                <asp:Button ID="Button_Query" runat="server" Style="text-align: center" Text="查询" OnClick="Button_Query_Click" />
                </tr>
            </table>
        </div>
        <table align="center" cellpadding="0px" cellspacing="0px" style="text-align: center; margin-top: 10px; border-style: none; border-bottom-color: #FFF" width="100%">
            <tr>
                <td colspan="3">
                    <asp:Label ID="lbtittle" Style="font-size: 28px; vertical-align: middle; height: 36px; line-height: 36px" runat="server" Text="工资及津贴成本费用分配表" BorderStyle="None"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="1">
                    <asp:Label ID="lbbzbm" Style="font-size: 14px" runat="server" Text="报账部门名称："></asp:Label></td>
                <td colspan="1">
                    <asp:Label ID="lbrq" Style="font-size: 14px" runat="server" Text="日期"></asp:Label></td>
                <td colspan="1">
                    <asp:Label ID="Label3" Style="font-size: 14px" runat="server" Text="单位："></asp:Label>元</td>
            </tr>
        </table>
        <table class="tb_normal" style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="GVData" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="tb_normal" OnRowDataBound="GVData_RowDataBound" ShowFooter="True" PageSize="15"
                        Width="100%" EnableModelValidation="True">
                        <PagerSettings Mode="NumericFirstLast" Visible="False" />
                        <PagerStyle BackColor="LightSteelBlue" HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" Font-Size="12px" Height="30px" />
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
                            <asp:BoundField DataField="项目名称" HeaderText="项目名称" DataFormatString="{0:###,###,##0.00}">
                                <ItemStyle ForeColor="Red" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="项目编号">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink_HTBH" runat="server" Font-Underline="True"
                                        NavigateUrl='<%# "../ProjectManage/ProjectFrame.aspx?type=XMCB&XMID="+ DataBinder.Eval(Container.DataItem, "项目编号")%>' ForeColor="Olive" ToolTip="项目详细信息"><%#DataBinder.Eval(Container.DataItem, "项目编号")%></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" ForeColor="Olive" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="合同金额" HeaderText="合同金额" DataFormatString="{0:###,###,##0.00}">
                                <ItemStyle ForeColor="#000000" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="费用分配率" HeaderText="费用分配率" DataFormatString="{0:###,###,##0.00}">
                                <ItemStyle ForeColor="#000000" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="费用分配额" HeaderText="费用分配额" DataFormatString="{0:###,###,##0.00}">
                                <ItemStyle ForeColor="#000000" HorizontalAlign="Right" />
                            </asp:BoundField>

                        </Columns>
                        <RowStyle HorizontalAlign="Center" Height="25px" />
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="center" style="border-right: black 1px; border-top: black 1px; border-left: black 1px; border-bottom: black 1px; background-color: whitesmoke;">该列表中暂时无数据！</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="text-align: center;">
                    <asp:ImageButton ID="BtnFirst" runat="server" CommandName="First" ImageUrl="../images/Button/First.jpg"
                        OnClick="PagerButtonClick" Style="height: 14px" />
                    <asp:ImageButton ID="BtnPre" runat="server" CommandName="Pre" ImageUrl="../images/Button/Pre.jpg"
                        OnClick="PagerButtonClick" Style="height: 14px" />
                    <asp:ImageButton ID="BtnNext" runat="server" CommandName="Next" ImageUrl="../images/Button/Next.jpg"
                        OnClick="PagerButtonClick" Style="height: 14px" />
                    <asp:ImageButton ID="BtnLast" runat="server" CommandName="Last" ImageUrl="../images/Button/Last.jpg"
                        OnClick="PagerButtonClick" Style="height: 14px" />
                    &nbsp;第<asp:Label ID="LabCurrentPage" runat="server" Text="0"></asp:Label>页&nbsp; 共<asp:Label
                        ID="LabPageSum" runat="server" Text="0"></asp:Label>页&nbsp;
                <asp:TextBox ID="TxtPageSize" runat="server" CssClass="TextBoxCssUnder2" Height="20px"
                    Width="35px">15</asp:TextBox>
                    行每页 &nbsp; 转到第<asp:TextBox ID="GoPage" runat="server" CssClass="TextBoxCssUnder2"
                        Height="20px" Width="33px"></asp:TextBox>
                    页&nbsp;
                <asp:ImageButton ID="ButtonGo" runat="server" OnClientClick="javascript:return CheckValuePiece();" ImageUrl="../images/Button/Jump.jpg" OnClick="ButtonGo_Click" Style="height: 18px" /></td>
            </tr>
        </table>
    </form>
    <div id="win_bumen" class="easyui-window" data-options="title:'选择部门',iconCls:'icon-search',closed:true,closable:false,minimizable:false,maximizable:false,collapsible:false,resizable:true" style="width: 350px; height: 400px; visibility: hidden; padding: 5px;">
        <iframe id="bumen" scrolling="yes" frameborder="0" src="../Main/SelectDanWei.aspx?TableName=ERPUser&LieName=UserName" style="width: 100%; height: 100%;"></iframe>
    </div>
    <div id="win_user" class="easyui-window" data-options="title:'选择用户',iconCls:'icon-search',closed:true,closable:false,minimizable:false,maximizable:false,collapsible:false,resizable:true" style="width: 350px; height: 400px; visibility: hidden; padding: 5px;">
        <iframe id="user" scrolling="yes" frameborder="0" src="../Main/SelectUser.aspx?TableName=ERPUser&LieName=UserName" style="width: 100%; height: 100%;"></iframe>
    </div>
</body>
</html>
