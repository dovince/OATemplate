<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowXMBudgetCostDetail.aspx.cs" Inherits="CommonSelect_ShowXMBudgetCostDetail" %>

<%@ Import Namespace="ZWL.Common" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge; charset=gb2312" />
    <link href="../JS/metronic/assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/cxcalendar.css" rel="stylesheet" />
    <link href="../Style/Style1.css" rel="stylesheet" />
    <script type="text/javascript" src="../JS/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../CSS/calendar.js"></script>
    <script type="text/javascript" src="../JS/jquery.easyui.min.js"></script>
    <script src="../JS/common.js?v=202306020823" type="text/javascript"></script>
    <style>
        body, html, #mapcontainer {
            width: 100%;
            min-height: 100%;
            overflow: hidden;
            margin: 0;
            position: absolute;
        }

        .fa.fa-reply {
            text-decoration: none;
            color: black;
        }

        .fa.fa-refresh {
            text-decoration: none;
            color: black;
        }

        .fa-reply:before {
            color: #69c228;
        }

        .fa-refresh:before {
            color: #69c228;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#previewcontent").attr("width", ($("body").width() - 40));
            $("#previewcontent").attr("height", ($("body").height() - 40));

        });
        //iframe下载文件
        function downloadFileByIfr(url) {
            var link = document.createElement('a')
            link.style.display = 'none'
            link.href = url
            link.download = "项目费用成本报销统计表_" + dateFormat(new Date()).replace(/-/g, "");

            document.body.appendChild(link)
            link.click() // a标签自动触发点击事件
        }

        function dateFormat(date) {
            var year = date.getFullYear();                // 年
            var month = showTime(date.getMonth() + 1);        // 月
            var week = showTime(date.getDay());           // 星期
            var day = showTime(date.getDate());          // 日
            var hours = showTime(date.getHours());         // 小时
            var minutes = showTime(date.getMinutes());    // 分钟
            var second = showTime(date.getSeconds());     // 秒
            return year + '-' + month + '-' + week + '-' + day + '-' + hours + '-' + minutes + '-' + second

        }
        // 封装一个不够两位数就补零的函数
        function showTime(t) {
            var time
            time = t > 10 ? t : '0' + t
            return time
        }

        function printPdf(url) {
            var iframe = undefined;
            if (!iframe) {
                iframe = document.createElement('iframe');
                document.body.appendChild(iframe);

                iframe.style.display = 'none';
                iframe.onload = function () {
                    setTimeout(function () {
                        iframe.focus();
                        iframe.contentWindow.print();
                    }, 1);
                };
            }

            iframe.src = url;
        }
        function CCC() {
            window.returnValue = "";
            if (window.parent.length > 0) {
                window.parent.$('#win_select').window('close');
            }
            else {
                window.close();
            }
        }
        document.onkeydown = function () {
            var e = event.srcElement;
            if (event.keyCode == 13) {
                var result = "btnSearch";
                var classFlag = $(e).attr("class");
                if (classFlag == "TextBoxCssUnder2") {
                    result = "ButtonGo";
                }
                document.getElementById(result).click();
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" style="margin: 0 auto; border: solid 1px #ccc; border-collapse: collapse; width: 95%;">
                <tbody>
                    <tr>
                        <td>
                            <table border="0" cellspacing="0" cellpadding="0" style="width: 100%; height: 49px">
                                <tr>
                                    <td style="font-family: 宋体; text-align: right; vertical-align: middle;">项目编号/名称：</td>
                                    <td style="font-family: 宋体; color: red; text-align: left; vertical-align: middle;">
                                        <asp:TextBox ID="SearchKeyWord" runat="server" Height="20px" Width="90%" ToolTip="请输入需要查询的编号或名称"></asp:TextBox>
                                    </td>
                                    <td valign="middle" style="width: 10%; text-align: right;">登记日期：</td>
                                    <td align="right" valign="middle" style="text-align: left;">从：<asp:TextBox ID="StartDate" runat="server" Width="100px" class="input_cxcalendar"></asp:TextBox>
                                        到：<asp:TextBox ID="EndDate" runat="server" Width="98px" class="input_cxcalendar"></asp:TextBox>
                                    </td>
                                    <td style="font-family: 宋体; color: red; text-align: left; vertical-align: middle;">
                                        <div style="">
                                            <asp:ImageButton ID="btnSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="../images/Button/BtnSerch.jpg"
                                                ToolTip="如果查询条件为空，显示所有记录" Style="height: 19px" OnClick="btnSearch_Click" />
                                            <strong style="font-size: 12px;">
                                                <strong>
                                                    <a href="javascript:void(0)" class="fa fa-refresh" onclick="javascript:window.frameElement.src = window.frameElement.src;">刷新</a>
                                                </strong>
                                            </strong>
                                            <strong style="font-size: 12px;">
                                                <strong>
                                                    <a href="javascript:void(0)" class="fa fa-reply" onclick="javascript:window.parent.closeTab();">返回</a>
                                                </strong>
                                            </strong>
                                        </div>
                                    </td>
                                    <td colspan="2" style="text-align: center; height: 40px;"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">
                            <div>

                                <table border="0" width="100%" cellspacing="5" cellpadding="5" bordercolorlight="#c0c0c0" bordercolordark="#ffffff">
                                    <tr>
                                        <td colspan="3" style="border: 0;">
                                            <table border="0" cellspacing="0" cellpadding="0" style="width: 100%; height: 49px">
                                                <tr>
                                                    <td colspan="2" style="height: 31px; text-align: center;">
                                                        <asp:GridView ID="GVData" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                            PageSize="8" CssClass="tb_normal"
                                                            AlternatingRowStyle-CssClass="alt"
                                                            Width="100%" Font-Size="Small" BackColor="White" Style="margin-top: 0px">
                                                            <HeaderStyle HorizontalAlign="Center" Font-Size="12px" Height="30px" />
                                                            <AlternatingRowStyle BackColor="WhiteSmoke" />
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-Width="30px">
                                                                    <ItemTemplate>
                                                                        <input id="Checkbox1" value='' type="checkbox" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="row" HeaderText="序号" ItemStyle-Width="40px" />
                                                                <asp:BoundField DataField="XMBH" HeaderText="项目编号" ItemStyle-Width="120px" />
                                                                <asp:BoundField DataField="HTBH" HeaderText="合同编号" ItemStyle-Width="120px" />
                                                                <asp:BoundField DataField="XMName" HeaderText="项目名称" ItemStyle-Width="220px" />
                                                                <asp:BoundField DataField="Item" HeaderText="支出类别" ItemStyle-Width="80px" />
                                                                <asp:BoundField DataField="Budget" HeaderText="预算" ItemStyle-Width="100px" DataFormatString="{0:###,###,##0.00}" ItemStyle-HorizontalAlign="Right" />
                                                                <asp:BoundField DataField="Cost" HeaderText="支出" ItemStyle-Width="100px" DataFormatString="{0:###,###,##0.00}" ItemStyle-HorizontalAlign="Right" />
                                                                <asp:BoundField DataField="BalAmt" HeaderText="可用金额" ItemStyle-Width="100px" DataFormatString="{0:###,###,##0.00}" ItemStyle-HorizontalAlign="Right" />
                                                                <asp:TemplateField ItemStyle-Width="30px" HeaderText="可用比例" ItemStyle-HorizontalAlign="Right">
                                                                    <ItemTemplate>
                                                                        <span><%# ZWL.Common.PublicMethod.FormatMoney((ZWL.Common.PublicMethod.GetDecimal(DataBinder.Eval(Container.DataItem, "Budget"))>0? (ZWL.Common.PublicMethod.GetDecimal(DataBinder.Eval(Container.DataItem, "BalAmt"))/ZWL.Common.PublicMethod.GetDecimal(DataBinder.Eval(Container.DataItem, "Budget"))):0)*100)%>%</span>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%--<asp:BoundField DataField="BalAmt" HeaderText="可用比例" ItemStyle-Width="100px" DataFormatString="{0:###,###,##0.00}" ItemStyle-HorizontalAlign="Right" />--%>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td align="center" style="border-right: white 0px; border-top: white 0px; border-left: white 0px; border-bottom: white 0px; background-color: whitesmoke;">该列表中暂时无数据！</td>
                                                                    </tr>
                                                                </table>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">共<asp:Label ID="Labelrowcount" runat="server" Text="0"></asp:Label>条记录&nbsp;
                                <asp:ImageButton ID="BtnFirst" runat="server" CommandName="First" ImageUrl="../images/Button/First.jpg"
                                    OnClick="PagerButtonClick" Style="height: 14px" />
                                                        <asp:ImageButton ID="BtnPre" runat="server" CommandName="Pre" ImageUrl="../images/Button/Pre.jpg"
                                                            OnClick="PagerButtonClick" Style="height: 14px" />
                                                        <asp:ImageButton ID="BtnNext" runat="server" CommandName="Next" ImageUrl="../images/Button/Next.jpg"
                                                            OnClick="PagerButtonClick" Style="height: 14px" />
                                                        <asp:ImageButton ID="BtnLast" runat="server" CommandName="Last" ImageUrl="../images/Button/Last.jpg"
                                                            OnClick="PagerButtonClick" Style="height: 14px" />
                                                        &nbsp;第<asp:Label ID="LabCurrentPage" runat="server" Text="1"></asp:Label>页&nbsp; 共<asp:Label
                                                            ID="LabPageSum" runat="server" Text="0"></asp:Label>页&nbsp;<asp:HiddenField ID="HdfPageSum" runat="server" />
                                                        <asp:TextBox ID="TxtPageSize" runat="server" CssClass="TextBoxCssUnder2" Height="20px"
                                                            Width="35px">15</asp:TextBox>
                                                        行每页 &nbsp; 转到第<asp:TextBox ID="GoPage" runat="server" CssClass="TextBoxCssUnder2"
                                                            Height="20px" Width="33px"></asp:TextBox>
                                                        页&nbsp;
                                                    <asp:ImageButton ID="ButtonGo" runat="server" CssClass="TextBoxCssUnder2" OnClientClick="javascript:return CheckValuePiece();" ImageUrl="../images/Button/Jump.jpg" OnClick="ButtonGo_Click" Style="height: 18px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
