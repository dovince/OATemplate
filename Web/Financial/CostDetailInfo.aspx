<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostDetailInfo.aspx.cs" Inherits="Financial_CostDetailInfo" %>
<%@ Import Namespace="ZWL.Common" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" rel="stylesheet" />
    <link href="../Style/Style1.css" rel="stylesheet" />
    <%--<link href="../CSS/cxcalendar.css" rel="stylesheet" />--%>
    <link href="../CSS/default/easyui.css" rel="stylesheet" />
    <link href="../CSS/icon.css" rel="stylesheet" />
    <link href="../JS/superTables/superTables.css" rel="stylesheet" />
    <link href="../CSS/Loading.css" rel="stylesheet" />
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />

    <script type="text/javascript" src="../JS/jquery-1.11.2.min.js"></script>
    <%--<script type="text/javascript" src="../CSS/calendar.js"></script>--%>
    <%--<script src="../JS/jquery.easyui.min.js"></script>--%>
    <script src="../JS/easyui-lang-zh_CN.js"></script>
    <script src="../JS/jquery.blockUI.js"></script>
    <script src="../JS/common.js?v=202306020823"></script>

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
        /*.content {
            width:100%;
            white-space:nowrap;
            overflow:hidden;
            text-overflow:ellipsis;
        }*/
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

        #title_table {
            border: solid #000;
            border-width: 1px 0px 0px 1px;
        }

            #title_table tr td {
                border: solid #000;
                border-width: 0px 1px 1px 0px;
            }

        .auto-style3 {
            width: 739px;
        }

        #GVData_New td, #GVData_New th {
            height: 20px;
            min-width: 80px;
            font-family: "Helvetica Neue", "Microsoft Yahei", Arial, sans-serif;
            font-size: 12px;
        }

        #GVData_New th {
            background: #F2F2F2;
            background-image: linear-gradient(to bottom, #f8f8f8 0%, #ececec 100%);
        }

        @media screen and (min-width: 1500px) {
            #GVData_New td, #GVData_New th {
                height: 30px;
                width: 10%;
            }
        }

        #GVData_New tr:nth-child(odd) {
            background: whitesmoke;
        }

        .auto-style4 {
            color: #FF0000;
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

        //$('#costdetaildiv').css('width', document.body.clientWidth);
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
        //$('#win_bumen').window({
        //    onBeforeClose: function () {
        //        var returnVal = "";
        //        for (var i = 0; i < window.length; i++) {
        //            if (window[i].frameElement.id == "bumen") {
        //                //根据弹出窗内部的ifame的id来定位
        //                returnVal = window[i].returnValue;
        //            }
        //        }
        //        $('#TextBox_bm')[0].value = returnVal;
        //    }
        //});
        //$('#win_user').window({
        //    onBeforeClose: function () {
        //        var returnVal = "";
        //        for (var i = 0; i < window.length; i++) {
        //            if (window[i].frameElement.id == "user") {
        //                //根据弹出窗内部的ifame的id来定位
        //                returnVal = window[i].returnValue;
        //            }
        //        }
        //        if (usertypeid != "") {
        //            var idstr = "#" + usertypeid;
        //            $(idstr)[0].value = returnVal;
        //        }

        //        //$('#win_user').window('refresh');
        //    }
        //});

        //FixTitleCol(0);
        //FixTitleCol(1);
        //FixTitleCol(2);
        //FixTitleCol(3);
        //FixTitleCol(4);
        //FixRow(0, 4);

        $.unblockUI();
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
        //$("#costdetaildiv").scroll(function () {
        //    $fixcol.css("left", $("#costdetaildiv").scrollLeft());
        //});
        $(window).scroll(function () {
            $fixcol.css("left", $(window).scrollLeft());
        });
    }

    function FixRow(colNum, gt) {
        $("#GVData").attr("data-Complate", "1");

        var $fRow = $("#GVData tr:eq(" + colNum + ") th:gt(" + gt + ")");
        $fRow.each(function (i) {
            var html = "<div class='FixRow' style='position:relative; background-color:#F3F3F3;  width:100%; border-right:1px solid #000;'>" + $(this).html() + "</div>";
            $(this).html(html);
        });
        var $fixrow = $(".FixRow");
        //$fRow.css("position", "relative").css("background-color", "#F3F3F3");
        $(window).scroll(function () {
            $fixrow.css("top", $(window).scrollTop());
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
    <%Response.Buffer = true;
        Response.Write("<script language='javascript' defer>blockUI({target:'body'});</script>");
    %>
    <form id="form1" runat="server">
        <div id="top">
            <table class="tb_normal" style="width: 100%">
                <tr>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px;">&nbsp;<img src="../images/BanKuaiJianTou.gif" />
                        财务管理&nbsp;&gt;&gt;&nbsp;项目支出管理&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="../images/Button/BtnRefresh.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton12_Click" Style="height: 19px" />
                        <asp:HiddenField ID="HiddenField_query" runat="server" />
                        <asp:HiddenField ID="HdfPageSum" runat="server" />
                    </td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px;">&nbsp;&nbsp; 
                        &nbsp;
                         <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="../images/Button/BtnAdd.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton1_Click" Style="height: 19px" />
                        <asp:ImageButton ID="ImageButton5" runat="server" ImageAlign="AbsMiddle" ImageUrl="../images/Button/BtnModify.jpg"
                            OnClick="ImageButton5_Click" OnClientClick="javascript:return CheckModify();" Style="height: 19px" />
                        <asp:ImageButton ID="ImageButton3" runat="server" OnClientClick="javascript:return CheckDel();" ImageUrl="../images/Button/BtnDel.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton3_Click" />
                        &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../images/Button/BtnReport.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton2_Click" Style="height: 19px" />&nbsp;&nbsp;
                        <asp:ImageButton ID="ImageButton_goback" ImageUrl="~/images/Button/BtnExit.jpg" runat="server" OnClick="ImageButton_goback_Click" ImageAlign="AbsMiddle" Style="height: 19px" />&nbsp;&nbsp;</td>
                </tr>
            </table>
            <table class="tb_normal" style="width: 100%">
                <tr id="querytr">
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; background-color: #f6f6f6"><strong>项目名称:</strong></td>
                    <td align="left" valign="middle" style="border-bottom: #006633 1px;" class="auto-style3">

                        <asp:Label ID="TextBox_xmname" runat="server" Style="text-align: left" Width="100%"></asp:Label>

                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; background-color: #f6f6f6" class="auto-style2"><strong>项目编号：</strong></td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; text-align: left;">

                        <asp:Label ID="TextBox_xmbh" runat="server" Style="text-align: left" Width="100px"></asp:Label>
                    </td>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; background-color: #f6f6f6" class="auto-style2"><strong>合同编号：</strong>
                    </td>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%;">
                        <asp:Label ID="TextBox_htbh" runat="server" Style="text-align: left" Width="100px" Text="无信息！"></asp:Label>
                    </td>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; background-color: #f6f6f6" class="auto-style2">
                        <strong>合同金额：</strong></td>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%;">
                        <asp:Label ID="Label_htje" runat="server" Style="text-align: left" Width="100px" Text="无信息！"></asp:Label>
                        &nbsp;</td>
                </tr>
                <tr id="querytr">
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; background-color: #f6f6f6">&nbsp;</td>
                    <td align="left" valign="middle" style="border-bottom: #006633 1px;" class="auto-style3">
                    &nbsp;<td valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; background-color: #f6f6f6" class="auto-style2"><strong>开工安全：</strong></td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; text-align: left;">

                        <asp:Label ID="Label_KGAQ" runat="server" Font-Bold="True" ForeColor="#CC3300"></asp:Label>
                    </td>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; background-color: #f6f6f6" class="auto-style2"><strong>成果审核[钻孔进尺]：</strong></td>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%;">
                        <asp:Label ID="Label_zkjc" runat="server" Font-Bold="True" ForeColor="#CC3300"></asp:Label>&nbsp;</td>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; background-color: #f6f6f6" class="auto-style2">&nbsp;</td>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%;">&nbsp;</td>
                </tr>
            </table>
        </div>

        <div id="div_container" style="text-align: center;">
            <div id="my_div" class="fakeContainer first_div" style="padding: 1px">
                <%if (!IsDataNull)
                    { %>

                <table border="1" id="GVData_New" style="margin-top: 5px; border-collapse: collapse;">
                    <tr>
                        <th class="center" style="width: 30px; min-width: 30px;">
                            <input id="CheckBoxAll" onclick="CheckAll()" type="checkbox" /></th>
                        <th class="center">支出期数</th>
                        <th class="center">录入日期</th>
                        <th class="center">摘要</th>
                        <th class="center">合计</th>
                        <th class="center">工资及津贴</th>
                        <th class="center">节日补贴</th>
                        <th class="center">养老统筹</th>
                        <th class="center">福利费</th>
                        <th class="center">劳动保护费</th>
                        <th class="center">住房公积金</th>
                        <th class="center">住房补贴</th>
                        <th class="center">材料费</th>
                        <th class="center">工程出包费</th>
                        <th class="center"><span class="auto-style4">钻探出包费</th>
                        <th class="center"><span class="auto-style4">钻孔进尺</span></th>
                        <th class="center">固定资产</th>
                        <th class="center">办公费</th>
                        <th class="center">差旅费</th>
                        <th class="center">水电费</th>
                        <th class="center">物业管理费</th>
                        <th class="center">交通运输费用</th>
                        <th class="center">邮电费用</th>
                        <th class="center">维修费用</th>
                        <th class="center">会议费</th>
                        <th class="center">培训费</th>
                        <th class="center">业务招待费</th>
                        <th class="center">劳务费</th>
                        <th class="center">租赁费</th>
                        <th class="center">税金及附加</th>
                        <th class="center">安全生产费用</th>
                        <th class="center">工会经费</th>
                        <th class="center">印刷费</th>
                        <th class="center">其它费用</th>
                    </tr>
                    <asp:Repeater runat="server" ID="datalist">
                        <ItemTemplate>
                            <tr>
                                <td style="width: 30px; min-width: 30px;">
                                    <asp:Label ID="CostDetailID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ID")%>' Visible="False" />
                                    <asp:Label ID="LabVisible" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ID")+"&sum="+DataBinder.Eval(Container.DataItem, "sum")%>' Visible="False" />
                                    <asp:CheckBox ID="CheckSelect" runat="server" />
                                </td>
                                <td class="center"><%#Eval("期间").ToString()%></td>
                                <td class="center"><%#(!string.IsNullOrEmpty(Eval("beiyong2").ToString())?Convert.ToDateTime(Eval("beiyong2")).ToString("yyyy-MM-dd"):"")%></td>
                                <td style="text-align:left;"><%#Eval("beiyong1").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("sum").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("工资及津贴").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("节日补贴").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("养老统筹").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("福利费").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("劳动保护费").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("住房公积金").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("住房补贴").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("材料费").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("工程出包费").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("钻探出包费").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("钻孔进尺").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("固定资产").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("办公费").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("差旅费").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("水电费").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("物业管理费").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("交通运输费用").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("邮电费用").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("维修费用").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("会议费").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("培训费").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("业务招待费").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("劳务费").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("租赁费").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("税金及附加").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("安全生产费用").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("工会经费").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("印刷费").ToString()%></td>
                                <td style="text-align:right;"><%#Eval("其它费用").ToString()%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>

                <asp:GridView CssClass="tableCSS" ID="GVData" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                    OnRowDataBound="GVData_RowDataBound" ShowFooter="True" PageSize="300" Style="display: none;"
                    EnableModelValidation="True">
                    <PagerSettings Mode="NumericFirstLast" Visible="False" />
                    <PagerStyle BackColor="LightSteelBlue" HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" Font-Size="12px" Height="30px" />
                    <AlternatingRowStyle BackColor="WhiteSmoke" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="LabVisible" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ID")+"&sum="+DataBinder.Eval(Container.DataItem, "sum")%>'
                                    Visible="False"></asp:Label><asp:CheckBox ID="CheckSelect" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle Width="20px" />
                            <HeaderTemplate>
                                <input id="CheckBoxAll" onclick="CheckAll()" type="checkbox" />
                            </HeaderTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="支出期数" HeaderStyle-Width="3%">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="True"
                                    NavigateUrl='<%# "../Financial/CostDetailModify.aspx?ID="+ DataBinder.Eval(Container.DataItem, "ID")+"&sum="+DataBinder.Eval(Container.DataItem,"sum")%>' ForeColor="#006699"><%#DataBinder.Eval(Container.DataItem, "期间")%></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="center" ForeColor="Red" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="beiyong2" HeaderText="录入日期" DataFormatString="{0:yyyy/MM/dd}">
                            <ItemStyle ForeColor="#723246" />
                            <HeaderStyle Width="3%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="beiyong1" HeaderText="摘要">
                            <ItemStyle ForeColor="#723246" Width="10%" CssClass="content" />
                            <HeaderStyle Width="4%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="sum" HeaderText="合计" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle ForeColor="#FF0000" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="工资及津贴" HeaderText="工资及津贴" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle ForeColor="#FF00BB" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="节日补贴" HeaderText="节日补贴" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle ForeColor="#FF0000" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="养老统筹" HeaderText="养老统筹" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle ForeColor="#FF3300" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="福利费" HeaderText="福利费" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle ForeColor="#FF3300" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="劳动保护费" HeaderText="劳动保护费" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle ForeColor="#FF3300" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="住房公积金" HeaderText="住房公积金" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle ForeColor="#FF3300" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="住房补贴" HeaderText="住房补贴" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle ForeColor="#FF3300" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="材料费" HeaderText="材料费" DataFormatString="{0:###,###,##0.00}">

                            <ItemStyle ForeColor="#FF0000" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="工程出包费" HeaderText="工程出包费" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>

                        <asp:BoundField DataField="固定资产" HeaderText="固定资产" DataFormatString="{0:###,###,##0.00}">

                            <ItemStyle ForeColor="#FF0000" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="办公费" HeaderText="办公费" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="差旅费" HeaderText="差旅费" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="水电费" HeaderText="水电费" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="物业管理费" HeaderText="物业管理费" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="交通运输费用" HeaderText="交通运输费用" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="邮电费用" HeaderText="邮电费用" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="维修费用" HeaderText="维修费用" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="会议费" HeaderText="会议费" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="培训费" HeaderText="培训费" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="业务招待费" HeaderText="业务招待费" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="劳务费" HeaderText="劳务费" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="租赁费" HeaderText="租赁费" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle ForeColor="#0066FF" HorizontalAlign="Right" />
                        </asp:BoundField>

                        <asp:BoundField DataField="税金及附加" HeaderText="税金及附加" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="安全生产费用" HeaderText="安全生产费用" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="工会经费" HeaderText="工会经费" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="印刷费" HeaderText="印刷费" DataFormatString="{0:###,###,##0.00}">
                            <ItemStyle HorizontalAlign="Right" />
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

                <%}
                    else
                    {%>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center" style="border-right: black 1px; border-top: black 1px; border-left: black 1px; border-bottom: black 1px; background-color: whitesmoke;">该列表中暂时无数据！</td>
                    </tr>
                </table>
                <%} %>
            </div>
        </div>

        <script src="../JS/superTables/superTables.js?v=20190610"></script>
        <script type="text/javascript">
            //此处调用superTables.js里需要的函数
            $(function () {
                if ($("#GVData_New").length > 0) {
                    new superTable("GVData_New", {
                        cssSkin: "sDefault",
                        fixedCols: 5, //固定几列
                        headerRows: 1,  //头部固定行数
                        onStart: function () {
                            this.start = new Date();
                        },
                        onFinish: function () {
                            initSuperTableWH();
                        }
                    });


                }
            })
            //window.onload = function () {
            //    if ($("#GVData_New").length > 0) {
            //        new superTable("GVData_New", {
            //            cssSkin: "sDefault",
            //            fixedCols: 5, //固定几列
            //            headerRows: 1,  //头部固定行数
            //            onStart: function () {
            //                this.start = new Date();
            //            },
            //            onFinish: function () {
            //                initSuperTableWH();
            //            }
            //        });


            //    }
            //}

            $(window).resize(function () {
                location.reload();
                //initSuperTableWH();
            });

            function initSuperTableWH() {
                var width = $("#form1").width() - 10;
                var winHeight = $("body").height();
                if (parent.$(".tabs-panels").length > 0) {
                    winHeight = parent.$(".tabs-panels").height()
                } else if (parent.Ext) {
                    winHeight = parent.Ext.getCmp('TabPanelID').activeTab.lastSize.height;
                }
                //|| parent.Ext.getCmp('TabPanelID').activeTab.lastSize.height
                var cheight = winHeight - $("#top").height() - $("#foot").height() - 10;
                var height = $(".sFData").height() + $("#top").height() + $("#foot").height() > winHeight ? (cheight < 0 ? winHeight : cheight) : $(".sFData").height();
                $("#div_container").css("width", width);//这个宽度是容器宽度，不同容器宽度不同
                $(".fakeContainer").css("height", height);//这个高度是整个table可视区域的高度，不同情况高度不同


                //.sData是调用superTables.js之后页面自己生成的  这块就是出现滚动条 达成锁定表头和列的效果
                if ($(".sData").length > 0) {
                    $(".sData").css("width", width - $(".sData").css("margin-left").replace("px", ""));//这块的宽度是用$("#div_container")的宽度减去锁定的列的宽度
                    $(".sData").css("height", height - $(".sHeader").height());//这块的高度是用$("#div_container")的高度减去锁定的表头的高度
                }

                //有兼容问题的话可以在下面判断浏览器的方法里写
                //if (navigator.appName == "Microsoft Internet Explorer" && navigator.appVersion.match(/9./i) == "9.") {//alert("IE 9.0");

                //} else if (!!window.ActiveXObject || "ActiveXObject" in window) {//alert("IE 10");

                //} else {//其他浏览器
                //    //alert("其他浏览器");
                //}

                $("#GVData_New tr").find("td:eq(0),td:eq(1),td:eq(2),td:eq(3),td:eq(4)").css("background", "#F2F2F2");

                var c, isdb;
                $(".sData #GVData_New tr").each(function () {
                    $(this).bind("mouseover", function () {
                        $(this).css("backgroundColor", "#E4F4FF");
                    });
                    $(this).bind("mouseout", function () {
                        $(this).css("backgroundColor", "#fff");
                    });

                    $(this).bind("click", function (_selected) {
                        SimpleCheckBox(_selected);
                    });

                    $(this).bind("dblclick", function (_selected) {
                        MuliCheckBox(_selected);
                    })
                })

            }

            function SimpleCheckBox(_selected) {
                var count = 0;
                isdb = false;
                window.setTimeout(cc, 300);
                function cc() {
                    if (isdb)
                        return;
                    var selected = $(_selected)[0].currentTarget;
                    var _rowIndex = selected.rowIndex;
                    var checkboxSelectd = $(".sFDataInner table tr:eq(" + _rowIndex + ") td").children(":checkbox")[0];

                    $(selected).toggleClass("active_table");

                    var trs = $(selected).siblings();

                    $.each(trs, function (i, item) {
                        if (hasClass(selected, "active_table")) {
                            count += 1;
                        }
                    });

                    if (count > 2) {
                        $.each(trs, function (i, item) {
                            if (item != selected && i != 0) {
                                removeClass(item, "active_table");
                                $(item).css("background-color", "");
                                $(".sFDataInner table tr:eq(" + i + ") td").children(":checkbox")[0].checked = false;
                            }
                        });
                    }

                    if (hasClass(selected, "active_table")) {
                        checkboxSelectd.checked = true;
                    } else {
                        checkboxSelectd.checked = false;
                        $(selected).css("background-color", "");
                    }

                    //var checkboxList = [];
                    //var parentDOM = $(selected).parents("form")[0];
                    //$.each(parentDOM, function (i, item) {
                    //    if (item.id.indexOf("_CheckSelect") != -1) {
                    //        checkboxList.push(item);
                    //    }
                    //});
                    //if (checkboxSelectd.checked==true) {
                    //    $.each(checkboxList, function (i, item) {
                    //        item.checked = false;
                    //    });
                    //    checkboxSelectd.checked = true;
                    //}
                }
            }

            function MuliCheckBox(_selected) {
                isdb = true;
                var selected = $(_selected)[0].currentTarget;
                var _rowIndex = selected.rowIndex;
                var checkboxSelectd = $(".sFDataInner table tr:eq(" + _rowIndex + ") td").children(":checkbox")[0];
                $(selected).toggleClass("active_table");
                if (hasClass(selected, "active_table")) {
                    checkboxSelectd.checked = true;
                } else {
                    checkboxSelectd.checked = false;
                    $(selected).css("background-color", "");
                }
            }

        </script>

        <table id="foot" style="width: 100%; text-align: center">
            <tr>
                <td style="border-top: #000000 0px none; border-bottom: #000000 0px none">
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
                <asp:TextBox ID="TxtPageSize" runat="server" CssClass="TextBoxCssUnder2" Height="20px" Width="35px">300</asp:TextBox>
                        行每页 &nbsp; 转到第<asp:TextBox ID="GoPage" runat="server" CssClass="TextBoxCssUnder2"
                            Height="20px" Width="33px"></asp:TextBox>
                        页&nbsp;
                <asp:ImageButton ID="ButtonGo" runat="server" OnClientClick="javascript:return CheckValuePiece();" ImageUrl="../images/Button/Jump.jpg" OnClick="ButtonGo_Click" Style="height: 18px" />
                    </div>
                </td>
            </tr>

        </table>
    </form>

</body>
</html>
