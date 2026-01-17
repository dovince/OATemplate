<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectCost.aspx.cs" Inherits="Financial_ProjectCost" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <%--    <link href="../Style/Style.css" rel="stylesheet" />
    <link href="../Style/Style1.css" rel="stylesheet" />--%>
    <link href="../CSS/cxcalendar.css" rel="stylesheet" />
    <link href="../CSS/default/easyui.css" rel="stylesheet" />
    <link href="../CSS/icon.css" rel="stylesheet" />
    <link href="../Style/Style1.css" rel="stylesheet" />
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
    <link href="../JS/metronic/assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JS/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../CSS/calendar.js"></script>
    <script type="text/javascript" src="../JS/common.js?v=20240613"></script>
    <script src="../JS/jquery.easyui.min.js"></script>
    <script src="../JS/easyui-lang-zh_CN.js"></script>
    <style type="text/css">
        body {
            margin: 0px;
            padding: 0px;
            font-size: 12px;
        }

        .auto-style2 {
            text-align: right;
        }

        .TDR {
            TEXT-ALIGN: right;
            background-color: #f6f6f6;
            width: 8%;
            padding: 5px;
            font-size: 12px;
        }

        .TDL {
            TEXT-ALIGN: left;
            width: 8%;
            color: #ff0000;
            padding: 5px;
            font-size: 14px;
        }
    </style>
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
            $('#win_bumen').window({
                onBeforeClose: function () {
                    var returnVal = "";
                    for (var i = 0; i < window.length; i++) {
                        if (window[i].frameElement.id == "bumen") {
                            //根据弹出窗内部的ifame的id来定位
                            returnVal = window[i].returnValue;
                        }
                    }
                    $('#TextBox_bm')[0].value = returnVal;
                }
            });
            $('#win_user').window({
                onBeforeClose: function () {
                    var returnVal = "";
                    for (var i = 0; i < window.length; i++) {
                        if (window[i].frameElement.id == "user") {
                            //根据弹出窗内部的ifame的id来定位
                            returnVal = window[i].returnValue;
                        }
                    }
                    if (usertypeid != "") {
                        var idstr = "#" + usertypeid;
                        $(idstr)[0].value = returnVal;
                    }

                    //$('#win_user').window('refresh');
                }
            });
        });
        function openbumenDialog() {
            var RadNum = Math.random();
            $("#bumen")[0].src += '&Radstr=' + RadNum;
            $('#win_bumen').css("visibility", "visible");
            $('#win_bumen').window('open');
        }
        var usertypeid = "";
        function openuserDialog(utype) {
            //防止缓存之前的页面
            var RadNum = Math.random();
            $("#user")[0].src += '&Radstr=' + RadNum;
            $('#win_user').css("visibility", "visible");
            $('#win_user').window('open');
            if (utype == "qjr") {
                usertypeid = "TextBox_qjr";
            }
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
            var item = null;
            for (var i = 0; i < window.document.form1.elements.length; i++) {
                var e = form1.elements[i];
                if (e.Name != "CheckBoxAll") {
                    if (e.checked == true) {
                        Modifynumber = Modifynumber + 1;
                        item = e;
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
        function redirectEditPage() {
            if (CheckModify()) {
                var item = null;
                for (var i = 0; i < window.document.form1.elements.length; i++) {
                    var e = form1.elements[i];
                    if (e.Name != "CheckBoxAll") {
                        if (e.checked == true) {
                            item = e;
                        }
                    }
                }
                if (item != null) {
                    var id = $(item).parent('span').attr('data-id');
                    addTab_parent('../Financial/ProjectCostModify.aspx?ID=' + id + '&sum=0', '修改项目成本');
                }
            }
            return false;
        }
        function validatecondition5() {
            //debugger
            if (document.getElementById("FileUpload5").value == "") {
                alert("请选择要导入的Excel文件！");
                return false;
            }
            else {
                if (document.getElementById("FileUpload5").value.lastIndexOf('.xls') < 0) {
                    alert("请选择Excel文件！");
                    return false;
                }
            }
        }
        function validatecondition6() {
            //debugger
            var o = confirm("是否导入符合所选条件的数据？")
            if (o == true) { return true; }
            else { return false; }
        }
        document.onkeydown = function () {
            var e = event.srcElement;
            if (event.keyCode == 13) {
                var result = "ImageButton1";
                var classFlag = $(e).attr("class");
                if (classFlag == "TextBoxCssUnder2") {
                    result = "ButtonGo";
                }
                document.getElementById(result).click();
                return false;
            }
        }
        function lotFiled() {
            if (confirm('执行合并后暂不支持撤销，确定要对所有存在先支出再签合同的预算和支出的项目执行合并操作吗？')) {
                document.getElementById('btnLotFiled').click();
            }
        }
        function addProjectCost() {
            addTab_parent('../Financial/ProjectCostAdd.aspx', '添加项目成本');
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="tb_normal" style="width: 100%">
                <tr>
                    <td valign="middle" style="border-bottom: #006633 1px;">&nbsp;<img src="../images/BanKuaiJianTou.gif" />
                        财务管理&nbsp;&gt;&gt;&nbsp;项目成本管理&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="../images/Button/BtnRefresh.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton12_Click" Style="height: 19px" />
                        <asp:HiddenField ID="HiddenField_query" runat="server" />
                    </td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px;">
                        <%--<input id="btn_query" type="button" value="显示查询" />&nbsp;&nbsp; --%>
                        <asp:Button ID="BtnYS" OnClientClick="javascript:return CheckModify();" runat="server" Text="预算" OnClick="BtnYS_Click" />
                        &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" ImageAlign="AbsMiddle" ImageUrl="../images/Button/BtnSerch.jpg" OnClick="Button_Query_Click" Style="height: 19px" />
                        &nbsp;
                         <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="../images/Button/BtnAdd.jpg" ImageAlign="AbsMiddle" OnClientClick="return addProjectCost(); " Style="height: 19px" />
                        <asp:ImageButton ID="ImageButton5" runat="server" ImageAlign="AbsMiddle" ImageUrl="../images/Button/BtnModify.jpg" OnClientClick="javascript:return redirectEditPage();" Style="height: 19px" />
                        <asp:ImageButton ID="ImageButton3" runat="server" OnClientClick="javascript:return CheckDel();" ImageUrl="../images/Button/BtnDel.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton3_Click" Style="height: 19px" Visible="False" />
                        &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../images/Button/BtnReport.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton2_Click" Style="height: 19px" />&nbsp;
                        <%if (ZWL.Common.PublicMethod.StrIFIn("|cost001C|", QuanXian))
                            { %>
                        <span class="fa fa-yahoo" aria-hidden="true" style="cursor: pointer; color: yellowgreen;" onclick="javascript:return lotFiled();">
                            <span style="font-size: 10pt; color: black;" title="自动合并先支出再签合同的预算和支出">合并</span>
                        </span>
                        <asp:ImageButton ID="btnLotFiled" runat="server" ImageUrl="../images/Button/BtnReport.jpg" ImageAlign="AbsMiddle" OnClick="btnLotFiled_Click" Style="height: 19px; display: none;" />
                        <%} %>
                        &nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            <table class="tb_normal" style="width: 100%">
                <tr id="querytr">
                    <td class="MainStyle" style="width: 10%;"><strong>项目名称:</strong></td>
                    <td align="left" valign="middle" style="width: 10%;" class="auto-style1">

                        <asp:TextBox ID="TextBox_xmname" runat="server" Style="text-align: left" Width="243px"></asp:TextBox></td>

                    <td class="MainStyle" style="width: 10%;"><strong>部门：</strong></td>
                    <td align="right" valign="middle" style="width: 10%; text-align: left;">
                        <asp:DropDownList ID="DropDownList_cjbm" runat="server" CssClass="auto-style5" Height="25px" Width="125px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_cjbm_SelectedIndexChanged">
                        </asp:DropDownList>
                        <%--<asp:TextBox ID="TextBox_bm" runat="server" Style="text-align: left" Width="100px"></asp:TextBox>
                        <img class="HerCss" onclick="openbumenDialog()"
                                    src="../images/Button/search.gif" />--%>
                    </td>
                    <td class="MainStyle" style="width: 10%;"><strong>专业类别：</strong></td>
                    <td align="right" valign="middle" style="width: 10%; text-align: left;">
                        <asp:DropDownList ID="DropDownList_xmsubjecttype" runat="server" CssClass="auto-style5" Height="25px" Width="125px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_xmsubjecttype_SelectedIndexChanged">
                        </asp:DropDownList>
                        <%--<asp:TextBox ID="TextBox_zylb" runat="server" Style="text-align: left" Width="100px"></asp:TextBox>--%>
                    <td class="MainStyle" style="width: 10%;">时间：</td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; width: 10%; text-align: left;">从：<asp:TextBox ID="TextBox_Start" runat="server" Width="100px" class="input_cxcalendar"></asp:TextBox>
                    <td valign="middle" style="width: 10%; text-align: left;">到：<asp:TextBox ID="TextBox_End" runat="server" Width="98px" class="input_cxcalendar"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="MainStyle" style="width: 10%;">支出明细数据导入：</td>
                    <td align="right" colspan="3" valign="middle" style="width: 10%; text-align: left;">
                        <asp:FileUpload ID="FileUpload5" runat="server" Width="410px" />

                        &nbsp;&nbsp;&nbsp;&nbsp;
                        
                    <asp:Button ID="btnUpdateUserphonenumber0" runat="server" ForeColor="Red" OnClientClick="validatecondition5()" Style="text-align: left" Text="导入明细数据" ToolTip="导入明细数据" Width="104px" OnClick="CostDetailin_Click" />

                    </td>
                    <td align="right" valign="middle" style="width: 10%;">
                        <asp:DropDownList ID="DropDownListBH" runat="server" CssClass="auto-style5" Height="25px" Width="100px">
                            <asp:ListItem Value="pc.XMBH" Selected="True">项目编号</asp:ListItem>
                            <asp:ListItem Value="pc.HTBH">合同编号</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" valign="middle" style="width: 10%;">

                        <asp:TextBox ID="txtXMBH" runat="server" Style="text-align: left" Width="157px"></asp:TextBox></td>
                    <td class="MainStyle" colspan="1" style="width: 10%;"><strong>导入项目信息：</strong></td>
                    <td align="right" colspan="2" valign="middle" style="width: 10%; text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        
                    <asp:Button ID="btnProjectcostin" runat="server" ForeColor="Red" Style="text-align: center" Text="导入项目成本管理项目信息" ToolTip="导入项目成本管理项目信息" Width="268px" OnClientClick="javascript:return validatecondition6();" OnClick="btnProjectcostin_Click" />

                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table class="tb_normal" style="width: 100%">
                <tr>
                    <td class="TDR">项目金额合计:
                    </td>
                    <td class="TDL">
                        <asp:Label ID="LabelXMJEHJ" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="TDR">合同金额合计:
                    </td>
                    <td class="TDL">
                        <asp:Label ID="LabelHTJEHJ" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="TDR">结算金额合计:
                    </td>
                    <td class="TDL">
                        <asp:Label ID="LabelJSJEHJ" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="TDR">开票金额合计:
                    </td>
                    <td class="TDL">
                        <asp:Label ID="LabelKPJEHJ" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="TDR">收款金额合计:
                    </td>
                    <td class="TDL">
                        <asp:Label ID="LabelSKJEHJ" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="TDR">成本支出合计:
                    </td>
                    <td class="TDL">
                        <asp:Label ID="LabelCBZCHJ" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <table class="table" align="center" cellpadding="0px" cellspacing="0px" style="width: 100%;">
            <tr>
                <td>
                    <asp:GridView ID="GVData" runat="server" AutoGenerateColumns="False" CssClass="tb_normal"
                        OnRowDataBound="GVData_RowDataBound" OnPreRender="GVData_PreRender" PageSize="25" ShowFooter="True" Width="100%">
                        <PagerSettings Mode="NumericFirstLast" Visible="False" />
                        <PagerStyle BackColor="LightSteelBlue" HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" Font-Size="12px" Height="30px" />
                        <AlternatingRowStyle BackColor="WhiteSmoke" />
                        <Columns>
                            <asp:TemplateField><%--0--%>
                                <ItemTemplate>
                                    <asp:Label ID="LabVisible" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ID")+"&sum="+DataBinder.Eval(Container.DataItem, "CostSums")%>'
                                        Visible="False"></asp:Label><asp:CheckBox ID="CheckSelect" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle Width="20px" />
                                <HeaderTemplate>
                                    <input id="CheckBoxAll" onclick="CheckAll()" type="checkbox" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="序号"><%--1--%>
                                <ItemTemplate>
                                    <asp:HyperLink ID="linkNo" runat="server" ForeColor="Blue" Font-Underline="True" NavigateUrl="javascript:void(0);" ToolTip="成本支出详细信息">
                                        <%# DataBinder.Eval(Container.DataItem, "row")%>
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="项目名称"><%--2--%>
                                <ItemTemplate>
                                    <asp:HyperLink ID="XMNameLink" runat="server" Font-Underline="True"
                                        NavigateUrl='javascript:void(0);' ForeColor="Red" ToolTip="成本支出详细信息">
                                        <%# getStringShort(DataBinder.Eval(Container.DataItem, "XMName"), 25)%></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" ForeColor="Red" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="项目编号"><%--3--%>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink_XMBH" runat="server" Font-Underline="True"
                                        NavigateUrl='javascript:void(0);' ForeColor="Blue" ToolTip="项目详细信息"><%#DataBinder.Eval(Container.DataItem, "XMBH")%></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" ForeColor="DarkViolet" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="合同编号"><%--4--%>
                                <ItemTemplate>
                                    <asp:HyperLink ID="linkHTBH" runat="server" Font-Underline="True"
                                        NavigateUrl='javascript:void(0);' ForeColor="Olive" ToolTip="合同详细信息"><%#DataBinder.Eval(Container.DataItem, "HTBH")%></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" ForeColor="Olive" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="XMBM" HeaderText="项目部门"></asp:BoundField>
                            <%--5--%>


                            <asp:BoundField DataField="XMFZR" HeaderText="项目负责人"><%--6--%>
                                <ItemStyle ForeColor="#0066FF" />
                            </asp:BoundField>

                            <asp:BoundField DataField="XMJF" HeaderText="项目金额" DataFormatString="{0:###,###,##0.00}"><%--7--%>
                                <ItemStyle ForeColor="#9900FF" HorizontalAlign="Right" />
                            </asp:BoundField>

                            <asp:BoundField DataField="HTJE" HeaderText="合同金额" DataFormatString="{0:###,###,##0.00}"><%--8--%>
                                <ItemStyle ForeColor="#9900FF" HorizontalAlign="Right" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="结算金额"><%--9--%>
                                <ItemTemplate>
                                    <asp:HyperLink ID="linkJieSuan" runat="server" Font-Underline="True"
                                        NavigateUrl="javascript:void(0);" ForeColor="#ee2200" DataFormatString="{0:###,###,##0.00}" ToolTip="合同结算信息">
                                        <%#DataBinder.Eval(Container.DataItem, "结算金额","{0:###,###,##0.00}")%>
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="开票金额"><%--10--%>
                                <ItemTemplate>
                                    <a id='GVData_ct<%#DataBinder.Eval(Container.DataItem, "ID") %>_HyperLinkKaiPiao<%#DataBinder.Eval(Container.DataItem, "ID") %>' title="合同开票到账信息" dataformatstring="{0:###,###,##0.00}"
                                        href="javascript:void(0);" style="color: #006699; text-decoration: underline;"
                                        onclick="addTab_parent('../BusinessManage/HTShouKuanList.aspx?FormID=46&WorkFlowID=40&BeiYong1=<%#DataBinder.Eval(Container.DataItem, "HTBH")+"@" %>','合同开票到账信息(<%#DataBinder.Eval(Container.DataItem, "XMBH") %>)')">
                                        <%#DataBinder.Eval(Container.DataItem, "开票金额","{0:###,###,##0.00}")%>
                                    </a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="收款金额" HeaderText="收款金额" DataFormatString="{0:###,###,##0.00}"><%--11--%>
                                <ItemStyle ForeColor="#333300" HorizontalAlign="Right" />
                            </asp:BoundField>

                            <asp:BoundField DataField="DJTime" HeaderText="登记时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"><%--12--%>
                                <ItemStyle ForeColor="#55AA00" HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="项目预算" Visible="true"><%--13--%>
                                <ItemTemplate>
                                    <a id='GVData_ct<%#DataBinder.Eval(Container.DataItem, "ID") %>_HyperLinkBudget<%#DataBinder.Eval(Container.DataItem, "ID") %>' title="成本预算详细信息" dataformatstring="{0:###,###,##0.00}"
                                        href="javascript:void(0);" style="color: #006699; text-decoration: underline;"
                                        onclick="addTab_parent('../Financial/BudgetDetailInfo.aspx?ShowPage=false&ID=<%#DataBinder.Eval(Container.DataItem, "ID") %>','成本预算详细信息(<%#DataBinder.Eval(Container.DataItem, "XMBH") %>)')">
                                        <%#DataBinder.Eval(Container.DataItem, "BudgetSum","{0:###,###,##0.00}")%>
                                    </a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="成本支出合计"><%--14--%>
                                <ItemTemplate>
                                    <a id='GVData_ct<%#DataBinder.Eval(Container.DataItem, "ID") %>_HyperLinkCost<%#DataBinder.Eval(Container.DataItem, "ID") %>' title="成本支出详细信息" dataformatstring="{0:###,###,##0.00}"
                                        href="javascript:void(0);" style="color: #006699; text-decoration: underline;" onclick="addTab_parent('../Financial/CostDetailInfo.aspx?ID=<%#DataBinder.Eval(Container.DataItem, "ID") %>','成本支出详细信息(<%#DataBinder.Eval(Container.DataItem, "XMBH") %>)')">
                                        <%#DataBinder.Eval(Container.DataItem, "CostSums","{0:###,###,##0.00}")%>
                                    </a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="CostSums" HeaderText="隐藏列" Visible="true"><%--15--%>
                                <ItemStyle ForeColor="#55AA00" HorizontalAlign="Right" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="支出比例" Visible="true"><%--16--%>
                                <ItemStyle ForeColor="#000000" HorizontalAlign="Right" />
                            </asp:BoundField>

                            <asp:BoundField DataField="结算金额" HeaderText="隐藏列2" Visible="true"><%--17--%>
                                <ItemStyle ForeColor="#55AA00" HorizontalAlign="Right" />
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
                <td style="text-align: center;">共<asp:Label ID="Labelrowcount" runat="server" Text="0"></asp:Label>
                    条记录&nbsp;
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
                <asp:HiddenField ID="HdfPageSum" runat="server" />
                    <asp:TextBox ID="TxtPageSize" runat="server" CssClass="TextBoxCssUnder2" Height="20px"
                        Width="35px">18</asp:TextBox>
                    行每页 &nbsp; 转到第<asp:TextBox ID="GoPage" runat="server" CssClass="TextBoxCssUnder2"
                        Height="20px" Width="33px"></asp:TextBox>
                    页&nbsp;
                <asp:ImageButton ID="ButtonGo" runat="server" CssClass="TextBoxCssUnder2" OnClientClick="javascript:return CheckValuePiece();" ImageUrl="../images/Button/Jump.jpg" OnClick="ButtonGo_Click" Style="height: 18px" /></td>
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

