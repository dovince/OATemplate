<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudgetDetailInfo.aspx.cs" Inherits="Financial_BudgetDetailInfo" ValidateRequest="false" %>

<!DOCTYPE html>
<html>
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
    <script src="../JS/common.js?v=202306020823" type="text/javascript"></script>
    <style type="text/css">
        .auto-style2 {
            text-align: right;
        }
    </style>
    <style type="text/css">
        body {
            margin: 0px;
            padding: 0px;
            font-size: 12px;
        }

        .tableCSS {
            table-layout: fixed;
        }

        .hidden {
            visibility: hidden;
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

        $('#costdetaildiv').css('width', document.body.clientWidth);
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

        FixTitleCol(0);
        FixTitleCol(1);
        FixTitleCol(2);
        FixTitleCol(3);
        FixTitleCol(4);
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

    var g_FixTitleCol = [];
    function FixTitleCol(colNum) {
        g_FixTitleCol.push(colNum);
        $("#GVData").attr("data-Complate", "1");

        //标记
        CTDToDiv($("#GVData tr:first th:eq(" + colNum + ")"), 0, "f3f3f3", colNum);
        //$("#GVData tr:first th:eq(" + colNum + ")").addClass("FixCol");
        var $trchild = $("#GVData tr:gt(0)");
        $trchild.each(function (i) {
            //$(this).find("td:eq(" + colNum + ")").addClass("FixCol");
            if ($trchild.length != i + 1) {
                CTDToDiv($(this).find("td:eq(" + colNum + ")"), 0, "fff", colNum);
            }
            else {
                CTDToDiv($(this).find("td:eq(" + colNum + ")"), 0, "dcdcdc", colNum);
            }
        });

        var $fixcol = $(".FixCol");
        //$fixcol.css("position", "relative").css("background-color", "#F3F3F3");
        $("#costdetaildiv").scroll(function () {
            $fixcol.css("left", $("#costdetaildiv").scrollLeft());
        });
    }

    function CTDToDiv($obj, height, bgcolor, colNum) {
        $obj.each(function (i) {
            var html = "<div class='FixCol' style='position:relative; background-color:#" + bgcolor + ";  width:100%; height:" + $(this).height() + "px; border-right:1px solid #000;'>" + $(this).html() + "</div>";
            $(this).html(html);
        });
    }
</script>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="tb_normal" style="width: 100%">
                <tr>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px;">&nbsp;<img src="../images/BanKuaiJianTou.gif" />
                        财务管理&nbsp;&gt;&gt;&nbsp;项目预算管理&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="../images/Button/BtnRefresh.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton12_Click" Style="height: 19px" />
                        <asp:HiddenField ID="HiddenField_query" runat="server" />
                        <asp:HiddenField ID="HdfPageSum" runat="server" />
                    </td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px;">&nbsp;&nbsp; 
                        &nbsp;
                        <asp:Label ID="lblLeftAdjustTimes" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="DownLoadMB" runat="server" Text="导入模板下载" OnClick="DownLoadMB_Click" ForeColor="Red" />
                        <asp:FileUpload ID="FileUpload" runat="server" />
                        <asp:ImageButton ID="ImageBtnImport" runat="server" ImageUrl="../images/Button/BtnImport.jpg" ImageAlign="AbsMiddle" OnClick="BtnImport_Click" Style="height: 19px" />
                         <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="../images/Button/BtnAdd.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton1_Click" Style="height: 19px" />
                        <%--<asp:ImageButton ID="ImageButton5" runat="server" ImageAlign="AbsMiddle" ImageUrl="../images/Button/BtnModify.jpg"
                            OnClick="ImageButton5_Click" OnClientClick="javascript:return CheckModify();" Style="height: 19px" />--%>
                        <%--<asp:ImageButton ID="ImageButton3" runat="server" OnClientClick="javascript:return CheckDel();" ImageUrl="../images/Button/BtnDel.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton3_Click" Style="height: 19px" />--%>
                        &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../images/Button/BtnReport.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton2_Click" Style="height: 19px" />&nbsp;&nbsp;<asp:ImageButton ID="ImageButton_goback" ImageUrl="~/images/Button/BtnExit.jpg" runat="server" OnClick="ImageButton_goback_Click" />
                        &nbsp;&nbsp;</td>
                </tr>
            </table>
            <table class="tb_normal" style="width: 100%">
                <tr id="querytr">
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; background-color: #f6f6f6"><strong>项目名称:</strong></td>
                    <td align="left" valign="middle" style="border-bottom: #006633 1px;" class="auto-style1">

                        <asp:TextBox ID="TextBox_xmname" runat="server" Style="text-align: left" Width="800px"></asp:TextBox>

                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; background-color: #f6f6f6" class="auto-style2"><strong>项目编号：</strong></td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; text-align: left;">

                        <asp:TextBox ID="TextBox_xmbh" runat="server" Style="text-align: left" CssClass="markTestCss" Width="100px"></asp:TextBox>
                    </td>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; text-align: left;">合同编号：
                    </td>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%;">
                        <asp:TextBox ID="TextBox_htbh" runat="server" Style="text-align: left" Width="100px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <table class="tb_normal" style="width: 100%">
            <tr>
                <td>
                    <div id="costdetaildiv" style="overflow-x: scroll;">
                        <asp:GridView CssClass="tb_normal tableCSS" ID="GVData" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            OnRowDataBound="GVData_RowDataBound" ShowFooter="True" PageSize="15"
                            Width="1850px" EnableModelValidation="True">
                            <PagerSettings Mode="NumericFirstLast" Visible="False" />
                            <PagerStyle BackColor="LightSteelBlue" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" Font-Size="12px" Height="30px" />
                            <AlternatingRowStyle BackColor="WhiteSmoke" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="LabVisible" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Version")%>'
                                            Visible="False"></asp:Label><asp:CheckBox ID="CheckSelect" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="20px" />
                                    <HeaderTemplate>
                                        <input id="CheckBoxAll" onclick="CheckAll()" type="checkbox" />
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="调整次数" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="false"
                                            NavigateUrl='<%#"../Financial/ProjectBudgetView.aspx?ID="+DataBinder.Eval(Container.DataItem, "ID")%>' ForeColor="#006699"><%#DataBinder.Eval(Container.DataItem, "Version")%></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" ForeColor="Red" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="CreatedTime" HeaderText="录入日期" DataFormatString="{0:yyyy/MM/dd}">
                                    <ItemStyle ForeColor="#723246" />
                                    <HeaderStyle Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Comment" HeaderText="摘要">
                                    <ItemStyle ForeColor="#723246" Width="10%" CssClass="content" />
                                    <HeaderStyle Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sum" HeaderText="合计" DataFormatString="{0:###,###,##0.00}">
                                    <ItemStyle ForeColor="#FF0000" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="工资及津贴" HeaderText="工资及津贴" DataFormatString="{0:###,###,##0.00}">
                                    <ItemStyle ForeColor="#FF00BB" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="工程出包费" HeaderText="工程出包费" DataFormatString="{0:###,###,##0.00}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="材料费" HeaderText="材料费" DataFormatString="{0:###,###,##0.00}">
                                    <ItemStyle ForeColor="#FF0000" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="租赁费" HeaderText="租赁费" DataFormatString="{0:###,###,##0.00}">
                                    <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="劳务费" HeaderText="劳务费" DataFormatString="{0:###,###,##0.00}">
                                    <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="安全生产费用" HeaderText="安全生产费用" DataFormatString="{0:###,###,##0.00}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="办公费" HeaderText="办公费" DataFormatString="{0:###,###,##0.00}">
                                    <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="维修费用" HeaderText="维修费用" DataFormatString="{0:###,###,##0.00}">
                                    <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="交通运输费用" HeaderText="交通运输费用" DataFormatString="{0:###,###,##0.00}">
                                    <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="差旅费" HeaderText="差旅费" DataFormatString="{0:###,###,##0.00}">
                                    <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="邮电费用" HeaderText="邮电费用" DataFormatString="{0:###,###,##0.00}">
                                    <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="水电费" HeaderText="水电费" DataFormatString="{0:###,###,##0.00}">
                                    <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="会议费" HeaderText="会议费" DataFormatString="{0:###,###,##0.00}">
                                    <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="印刷费" HeaderText="印刷费" DataFormatString="{0:###,###,##0.00}">
                                    <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="其它费用" HeaderText="其它费用" DataFormatString="{0:###,###,##0.00}">
                                    <ItemStyle HorizontalAlign="Right" />
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
                    </div>
                </td>
            </tr>
        </table>
        <table style="width: 100%; text-align: center;" class="<%=ShowPage?"":"hidden" %>">
            <tr>
                <td style="text-align:center;">
                    <div>
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
                <asp:ImageButton ID="ButtonGo" runat="server" OnClientClick="javascript:return CheckValuePiece();" ImageUrl="../images/Button/Jump.jpg" OnClick="ButtonGo_Click" Style="height: 18px" />
                    </div>
                </td>
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
