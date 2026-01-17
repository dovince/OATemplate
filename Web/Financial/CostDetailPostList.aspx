<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostDetailPostList.aspx.cs" Inherits="Financial_CostDetailPostList" ValidateRequest="false" %>

<%@ Import Namespace="ZWL.Common" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge; charset=gb2312" />
    <link href="../Style/Style.css" rel="stylesheet" />
    <link href="../Style/Style1.css" rel="stylesheet" />
    <link href="../JS/superTables/superTables.css" rel="stylesheet" />
    <script type="text/javascript" src="../JS/jquery-1.11.2.min.js"></script>
    <link href="../CSS/cxcalendar.css" rel="stylesheet" />
    <link href="../CSS/default/easyui.css" rel="stylesheet" />
    <link href="../CSS/icon.css" rel="stylesheet" />
    <link href="../CSS/Loading.css" rel="stylesheet" />
    <link href="../JS/metronic/assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
    <script type="text/javascript" src="../JS/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../CSS/calendar.js"></script>
    <script type="text/javascript" src="../JS/jquery.easyui.min.js"></script>
    <script src="../JS/jquery.blockUI.js"></script>
    <script src="../JS/common.js?v=202306020823" type="text/javascript"></script>
    <style>
        input[type=text], select {
            border: 0;
            border-bottom: 1px solid #b4b4b4;
        }

        table td .fa {
            font-size: 17px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".btnShowDetail").on("click", function () {
                if ($(this).hasClass("fa-plus-square-o")) {
                    $(this).attr("class", "fa fa-minus-square-o btnShowDetail");
                }
                else {
                    if ($(this).closest("tr").next().length > 0 && $(this).closest("tr").next().prop("outerHTML").indexOf('detailitem') > 0) {
                        $(this).closest("tr").next().remove();
                    }
                    $(this).attr("class", "fa fa-plus-square-o btnShowDetail");
                }
            });
        });
        function refreshTriggerRow(btnId) {
            var currentBtn = $("#" + btnId);
            currentBtn.hide();
            currentBtn.closest("td").prev("td").html("已归还")
            var btnShowDetail = currentBtn.parents("table[id*=GVDetail]").closest("tr").find(".btnShowDetail");
            btnShowDetail.click();
            btnShowDetail.click();
        }
        var a;
        function CheckValuePiece() {
            if (window.document.form1.GoPage.value == "") {
                alert("请输入跳转的页码！");
                window.document.form1.GoPage.focus();
                return false;
            }
            return true;
        }
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
        function CheckDel() {
            var number = 0;
            for (var i = 0; i < window.document.form1.elements.length; i++) {
                var e = form1.elements[i];
                if (e.Name != "CheckBoxAll") {
                    if (e.checked == true) {
                        number = number + 1;
                    }
                }
            }
            if (number == 0) {
                alert("请选择需要删除的项！");
                return false;
            }
            if (window.confirm("你确认删除吗？")) {
                return true;
            }
            else {
                return false;
            }
        }

        function CheckModify() {
            var Modifynumber = 0;
            for (var i = 0; i < window.document.form1.elements.length; i++) {
                var e = form1.elements[i];
                if (e.Name != "CheckBoxAll") {
                    if (e.checked == true) {
                        Modifynumber = Modifynumber + 1;
                    }
                }
            }
            if (Modifynumber == 0) {
                alert("请至少选择一项！");
                return false;
            }
            if (Modifynumber > 1) {
                alert("只允许选择一项！");
                return false;
            }

            return true;
        }
        function confirmPostPrint(id, msg) {
            var result = true;
            if (msg != "") {
                result = confirm(msg);
            }
            if (result) {
                var options = {
                    url: '../Services/Services.ashx',
                    data: { f: "PrintProjectCostDetailSubmitReport", id: id },
                    async: false,
                    dataType: "json",
                    timeout: 60 * 1000,
                    success: function (data) {
                        if (data.Code) {
                            var options1 = {
                                title: "项目费用成本报销统计表:" + data.Data,
                                url: '../CommonSelect/PrintHelper.aspx?filename=' + data.Data + "&_=" + Math.random(),
                                width: $("body").width() - 40,
                                height: $("body").height() - 40,
                                onFinish: function (returnVal) {
                                    if (msg != "") {
                                        window.frameElement.src = window.frameElement.src;
                                    }
                                }
                            }
                            showPopwindow("", options1);
                        }
                        else {

                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        console.log(XMLHttpRequest);
                        hideLoading();
                    },
                    beforeSend: function () {
                        showLoading();
                    },
                    complete: function () {
                        hideLoading();
                    }
                };

                MakeRequestAjax(options);
            }
        }
        document.onkeydown = function () {
            var e = event.srcElement;
            if (event.keyCode == 13) {
                var result = "ImageButton4";
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
            <div style="display: none;">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            </div>
            <table class="tb_normal" style="width: 100%">
                <tr>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px;">&nbsp;<img src="../images/BanKuaiJianTou.gif" />
                        <a class="hei" href="../Main/MyDesk.aspx">桌面</a>&nbsp;>>&nbsp;财务管理&nbsp;>>&nbsp;成本报销管理&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="../images/Button/BtnRefresh.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton12_Click" Style="height: 19px" />
                    </td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px;">
                        <img class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectCondition.aspx?TableName=ERPNWorkToDo&LieName='+document.getElementById('DropDownList2').value+'&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox3').value=wName;}"
                            src="../images/Button/search.gif" style="display: none;" />
                        <asp:ImageButton ID="ImageButton4" runat="server" ImageAlign="AbsMiddle" ImageUrl="../images/Button/BtnSerch.jpg" OnClick="ImageButton4_Click" />
                        &nbsp; 
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../images/Button/BtnAdd.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton1_Click" />
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../images/Button/BtnReport.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton2_Click" />&nbsp;&nbsp;&nbsp;&nbsp;</td>
                </tr>
            </table>

            <table id="search" class="sDefault " style="width: 100%; border-collapse: collapse; margin: 0px;">
                <tr>
                    <td align="right" valign="middle" style="height: 30px; width: 10%;"><strong>项目/合同编号：</strong></td>
                    <td align="left" valign="middle" style="width: 10%;">
                        <asp:TextBox ID="txtHTBH" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td align="right" valign="middle" style="height: 30px; width: 5%;">
                        <strong>项目/合同名称：</strong>
                    </td>
                    <td align="left" valign="middle" style="width: 10%;" colspan="2">
                        <asp:TextBox ID="txtHTName" runat="server" Width="90%"></asp:TextBox>
                    </td>
                    <td align="right" valign="middle" style="height: 30px; width: 10%;"><strong>当前状态：</strong></td>
                    <td align="left" valign="middle" style="width: 10%;" colspan="3">
                        <asp:DropDownList ID="txtZYLB" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="middle" style="height: 30px; width: 10%;"><strong>登记人：</strong></td>
                    <td align="left" valign="middle" style="width: 10%;">
                        <asp:TextBox ID="txtUsername" runat="server" Width="100px"></asp:TextBox>
                        <img class="HerCss" id="Img2" onclick="showUserPopwindow('txtUsername')" src="../images/Button/search.gif" />
                    </td>
                    <td align="right" valign="middle" style="height: 30px; width: 10%;"><strong>部门：</strong></td>
                    <td align="right" valign="middle" style="height: 30px; width: 10%; text-align: left;">
                        <asp:TextBox ID="txtDept" runat="server" Style="text-align: left" Width="100px"></asp:TextBox>
                        <img class="HerCss" onclick="showDeptPopwindow('txtDept')" src="../images/Button/search.gif" />
                    </td>
                    <td valign="middle" style="height: 30px; width: 10%; text-align: right; font-weight: 700;">发起时间：</td>
                    <td align="right" valign="middle" style="height: 30px; width: 10%; text-align: left;">从：<asp:TextBox ID="txtDateStart" runat="server" Width="100px" class="input_cxcalendar"></asp:TextBox>
                    </td>
                    <td valign="middle" style="height: 30px; width: 10%; text-align: left;">到：<asp:TextBox ID="txtDateEnd" runat="server" Width="98px" class="input_cxcalendar"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <table class="tb_normal" style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="GVData" runat="server" AutoGenerateColumns="False"
                        OnRowDataBound="GVData_RowDataBound" EnableModelValidation="True" OnRowCommand="GVData_RowCommand"
                        PageSize="15" Width="100%" CssClass="tb_normal">
                        <PagerSettings Visible="False" />
                        <HeaderStyle Font-Size="12px" Height="20px" HorizontalAlign="Center" />
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
                            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="110px">
                                <ItemTemplate>
                                    <div>
                                        <asp:LinkButton class="fa fa-plus-square-o btnShowDetail" ID="btnShowDetails" runat="server" Style="cursor: pointer; color: blue;" Font-Underline="false" ToolTip="修改" CommandName="detail" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "ID")%>'></asp:LinkButton>&nbsp;
                                        <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GVDetail" runat="server" AutoGenerateColumns="false" Width="100%"
                                                        OnRowDataBound="GVDetail_RowDataBound" CssClass="tb_normal">
                                                        <HeaderStyle Font-Size="12px" Height="20px" HorizontalAlign="Center" />

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="序号" InsertVisible="False">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrder" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="XMName" HeaderText="项目名称" />
                                                            <asp:BoundField DataField="Item" HeaderText="支出类别" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Description" HeaderText="支出明细" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="HTBH" ItemStyle-Width="180px" HeaderText="合同/项目编号" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Amount" HeaderText="合同金额" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:###,###,##0.00}" />
                                                            <asp:BoundField DataField="SettleAmt" HeaderText="开票金额" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:###,###,##0.00}" />
                                                            <asp:BoundField DataField="ReceivedAmt" HeaderText="已到账金额" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:###,###,##0.00}" />
                                                            <asp:BoundField DataField="TotalAmt" HeaderText="已累计支出金额" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:###,###,##0.00}" />
                                                            <asp:BoundField DataField="SubmitAmt" HeaderText="本次报销金额" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:###,###,##0.00}" />
<%--                                                            <asp:BoundField DataField="ItemScale" HeaderStyle-Width="100" HeaderText="本类别支出与预算比例%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:###,###,##0.00}" />--%>
                                                            <asp:BoundField DataField="CostScale" HeaderStyle-Width="100" HeaderText="总支出与已到账金额比例%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:###,###,##0.00}" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnShowDetails" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                        <span <%# CanModify(DataBinder.Eval(Container.DataItem, "ID")) ? "":" style='display:none' " %>>
                                            <asp:LinkButton class="fa fa-pencil-square-o" ID="btnEdit" runat="server" Style="cursor: pointer; color: blue;" Font-Underline="false" ToolTip="修改" CommandName="modify" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "ID")%>'></asp:LinkButton>&nbsp;
                                        </span>
                                        <span <%# CanDelete(DataBinder.Eval(Container.DataItem, "ID")) ? "":" style='display:none' " %>>
                                            <asp:LinkButton class="fa fa-trash-o" ID="btnDelete" runat="server" Style="cursor: pointer; color: blue;" Font-Underline="false" CommandName="del" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "ID")%>' ToolTip="删除" OnClientClick="return window.confirm('你确认删除吗？')"></asp:LinkButton>
                                        </span>
                                        <span <%# CanRevert(DataBinder.Eval(Container.DataItem, "ID")) ? "":" style='display:none' " %>>
                                            <asp:LinkButton class="fa fa-rotate-left" ID="btnRevert" runat="server" Style="cursor: pointer; color: blue;" Font-Underline="false" CommandName="revert" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "ID")%>' ToolTip="撤回" OnClientClick="return window.confirm('你确认撤回吗？')"></asp:LinkButton>
                                        </span>
                                        <span <%# CanSubmit(DataBinder.Eval(Container.DataItem, "ID")) ? "":" style='display:none' " %>>
                                            <asp:LinkButton class="fa fa-check" ID="btnSubmit" runat="server" Style="cursor: pointer; color: blue;" Font-Underline="false" CommandName="submit" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "ID")%>' ToolTip="提交" OnClientClick="return window.confirm('你确认提交吗？')"></asp:LinkButton>
                                        </span>
                                        <span <%# CanSign(DataBinder.Eval(Container.DataItem, "ID")) ? "":" style='display:none' " %>>
                                            <asp:LinkButton class="fa fa-check-square-o" ID="btnSign" runat="server" Style="cursor: pointer; color: blue;" Font-Underline="false" CommandName="sign" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "ID")%>' ToolTip="完成" OnClientClick="return window.confirm('你确认标记为[完成]吗？')"></asp:LinkButton>
                                        </span>
                                        <span <%# CanPrint(DataBinder.Eval(Container.DataItem, "ID")) ? "":" style='display:none' " %>>
                                            <asp:LinkButton class="fa fa-print" ID="btnPrint" runat="server" Style="cursor: pointer; color: blue;" Font-Underline="false" CommandName="print" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "ID")%>' ToolTip="打印"></asp:LinkButton>
                                        </span>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="110px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="row" HeaderText="序号">
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="批号">
                                <ItemTemplate>
                                    <asp:HyperLink ID="linkLot" runat="server" ForeColor="Blue" Font-Underline="True" NavigateUrl="javascript:void(0);" >
                                        <%# DataBinder.Eval(Container.DataItem, "LotNo")%>
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="DJR" HeaderText="登记人" HtmlEncode="False">
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DJBM" HeaderText="部门" HtmlEncode="False">
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="支出合计">
                                <ItemTemplate>
                                    <asp:Label ID="itemAmt" runat="server"><%# DataBinder.Eval(Container.DataItem, "TotalAmt","{0:###,###,##0.00}")%></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="支出明细">
                                <ItemTemplate>
                                    <asp:Label ID="itemDetail" runat="server"><%# DataBinder.Eval(Container.DataItem, "Description")%></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="DJTime" HeaderText="登记时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
                                <ItemStyle Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="State" HeaderText="当前状态">
                                <ItemStyle Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Comment" HeaderText="备注">
                                <ItemStyle Width="120px" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle HorizontalAlign="Center" Height="25px" />
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="center" style="border: 0;">该列表中暂时无数据！</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="text-align: center;">共<asp:Label ID="Labelrowcount" runat="server" Text="0"></asp:Label>
                    条记录&nbsp;
                    <asp:ImageButton ID="BtnFirst" runat="server" CommandName="First" ImageUrl="../images/Button/First.jpg"
                        OnClick="PagerButtonClick" />
                    <asp:ImageButton ID="BtnPre" runat="server" CommandName="Pre" ImageUrl="../images/Button/Pre.jpg"
                        OnClick="PagerButtonClick" />
                    <asp:ImageButton ID="BtnNext" runat="server" CommandName="Next" ImageUrl="../images/Button/Next.jpg"
                        OnClick="PagerButtonClick" />
                    <asp:ImageButton ID="BtnLast" runat="server" CommandName="Last" ImageUrl="../images/Button/Last.jpg"
                        OnClick="PagerButtonClick" />
                    &nbsp;第<asp:Label ID="LabCurrentPage" runat="server" Text="0"></asp:Label>页&nbsp; 共<asp:Label
                        ID="LabPageSum" runat="server" Text="0"></asp:Label>页&nbsp;
                <asp:HiddenField ID="HdfPageSum" runat="server" />
                    <asp:TextBox ID="TxtPageSize" runat="server" CssClass="TextBoxCssUnder2" Height="20px"
                        Width="35px">15</asp:TextBox>
                    行每页 &nbsp; 转到第<asp:TextBox ID="GoPage" runat="server" CssClass="TextBoxCssUnder2"
                        Height="20px" Width="33px"></asp:TextBox>
                    页&nbsp;
                <asp:ImageButton ID="ButtonGo" runat="server" OnClientClick="javascript:return CheckValuePiece();" ImageUrl="../images/Button/Jump.jpg" OnClick="ButtonGo_Click" CssClass="TextBoxCssUnder2" />
                    <%--<span style="color: darkgray">*只能删除状态为"已被驳回"的工作，删除其他请联系管理员。</span>--%></td>
            </tr>
        </table>
    </form>
    <script type="text/javascript">
        var instance = Sys.WebForms.PageRequestManager.getInstance();
        if (instance) {
            instance.add_endRequest(handleRequest);
        }
        function handleRequest() {
            $(".btnShowDetail").each(function (i, item) {
                if ($(this).hasClass("fa-minus-square-o")) {
                    if ($(this).closest("tr").next().length > 0 && $(this).closest("tr").next().prop("outerHTML").indexOf('detailitem') > 0) {
                        $(this).closest("tr").next().remove();
                    }
                    $(this).closest("tr").after("<tr class='detailitem'><td colspan = '2'></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                }
            });
        }
    </script>
</body>
</html>
