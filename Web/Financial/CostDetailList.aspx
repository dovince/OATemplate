<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostDetailList.aspx.cs" Inherits="Financial_CostDetailList" %>
<html>
<head runat="server">
    <title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" rel="stylesheet" />
    <link href="../Style/Style1.css" rel="stylesheet" />
    <link href="../CSS/cxcalendar.css" rel="stylesheet" />
    <link href="../CSS/default/easyui.css" rel="stylesheet" />
    <link href="../CSS/icon.css" rel="stylesheet" />
    <script type="text/javascript" src="../JS/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../CSS/calendar.js"></script>
    <script src="../JS/jquery.easyui.min.js"></script>
    <script src="../JS/easyui-lang-zh_CN.js"></script>
    <style type="text/css">
        
        .auto-style2
        {
            text-align: right;
        }
    </style>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            font-size: 12px;
        }

        .table
        {
            border-collapse: collapse;
            height: 24px;
            line-height: 24px;
            text-align: center;
        }

            .table tr
            {
                border-left: solid 1px #FFF;
                border-right: solid 1px #000000;
            }

                .table tr td
                {
                    border: solid 1px #000000;
                    /*text-align: center;*/
                }

        .classid
        {
            background-image: url(../Image/Tab/Tab_14.gif);
            height: 26px;
            background-repeat: repeat-x;
            text-align: center;
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



</script>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="middle" style="border-bottom: #006633 1px ; height: 30px;">&nbsp;<img src="../images/BanKuaiJianTou.gif" /> 财务管理&nbsp;&gt;&gt;&nbsp;项目支出管理&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="../images/Button/BtnRefresh.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton12_Click" Style="height: 19px" />
                        <asp:HiddenField ID="HiddenField_query" runat="server" />
                        <asp:HiddenField ID="HdfPageSum" runat="server" />
                    </td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px ; height: 30px;">
                        <input id="btn_query" type="button" value="显示查询" />&nbsp;&nbsp; 
                        &nbsp;
                         <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="../images/Button/BtnAdd.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton1_Click" Style="height: 19px" />
                        <asp:ImageButton ID="ImageButton5" runat="server" ImageAlign="AbsMiddle" ImageUrl="../images/Button/BtnModify.jpg"
                            OnClick="ImageButton5_Click" OnClientClick="javascript:return CheckModify();" Style="height: 19px" />
                        <asp:ImageButton ID="ImageButton3" runat="server" OnClientClick="javascript:return CheckDel();" ImageUrl="../images/Button/BtnDel.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton3_Click" Style="height: 19px" Visible="False" />
                        &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../images/Button/BtnReport.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton2_Click" Style="height: 19px" />&nbsp;&nbsp;&nbsp;&nbsp;</td>
                </tr>
            </table>
            <table style="width: 100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#99BCFC">
                <tr id="querytr">
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; background-color: #f6f6f6"><strong>项目名称:</strong></td>
                    <td align="left" valign="middle" style="border-bottom: #006633 1px; width: 10%;" class="auto-style1">
                        
                        <asp:TextBox ID="TextBox_xmname" runat="server" Style="text-align: left" Width="243px"></asp:TextBox>
                                                
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; background-color: #f6f6f6"><strong>部门：</strong></td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; text-align: left;">
                        <asp:TextBox ID="TextBox_bm" runat="server" Style="text-align: left" Width="100px"></asp:TextBox>
                        <img class="HerCss" onclick="openbumenDialog()"
                                    src="../images/Button/search.gif" />
                    </td>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; background-color: #f6f6f6" class="auto-style2"><strong>专业类别：</strong></td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; text-align: left;">
                       
                        <asp:TextBox ID="TextBox_xmbh" runat="server" Style="text-align: left" Width="100px"></asp:TextBox>
                                               
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; text-align: right; background-color: #f6f6f6; font-weight: 700;">时间：</td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; text-align: left;">从：<asp:TextBox ID="TextBox_Start" runat="server" Width="100px" class="input_cxcalendar"></asp:TextBox>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; text-align: left;">到：<asp:TextBox ID="TextBox_End" runat="server" Width="98px" class="input_cxcalendar"></asp:TextBox>
                    </td>
                    <td align="middle" valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%;">
                        <asp:Button ID="Button_Query" runat="server" Style="text-align: center" Text="查询" OnClick="Button_Query_Click" />
                </tr>
            </table>
        </div>
        <table class="table" align="center" cellpadding="0px" cellspacing="0px" style="margin-top: 10px" width="100%">
            <tr>
                <td>
                    <asp:GridView ID="GVData" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="tb_normal" OnRowDataBound="GVData_RowDataBound" PageSize="15"
                        Width="100%" EnableModelValidation="True" >
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
                            <asp:TemplateField HeaderText="项目名称">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink2" runat="server" Font-Underline="True"
                                        NavigateUrl='<%# "../Financial/CostDetailModify.aspx?ID="+ DataBinder.Eval(Container.DataItem, "ID")%>' ForeColor="Red" ToolTip="成本支出详细信息"><%#DataBinder.Eval(Container.DataItem, "beiyong1")%></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" ForeColor="Red" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="XMBH" HeaderText="项目编号"></asp:BoundField>
                            <asp:BoundField DataField="HTBH" HeaderText="合同编号" >
                            <ItemStyle ForeColor="#009933" />
                            </asp:BoundField>
                            <asp:BoundField DataField="劳务成本" HeaderText="劳务成本" >
                            <ItemStyle ForeColor="#9900FF" />
                            </asp:BoundField>
                            <asp:BoundField DataField="人工费" HeaderText="人工费">
                                <ItemStyle ForeColor="#FF3300" />
                            </asp:BoundField>
                            <asp:BoundField DataField="材料费" HeaderText="材料费"></asp:BoundField>
                            <asp:BoundField DataField="其他直接费用" HeaderText="其他直接费用">

                                <ItemStyle ForeColor="#0066FF" />
                            </asp:BoundField>
                            <asp:BoundField DataField="间接费用_其他" HeaderText="间接费用" />
                             
                             <asp:TemplateField HeaderText="期间">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="True"
                                        NavigateUrl='<%# "../Financial/CostDetailList.aspx?ID="+ DataBinder.Eval(Container.DataItem, "ID")%>' ForeColor="#006699"><%#DataBinder.Eval(Container.DataItem, "期间")%></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" ForeColor="Red" />
                            </asp:TemplateField>
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
                <td style="text-align:center;">
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
     <div id="win_bumen" class="easyui-window" data-options="title:'选择部门',iconCls:'icon-search',closed:true,closable:false,minimizable:false,maximizable:false,collapsible:false,resizable:true" style="width: 350px; height: 400px; visibility:hidden; padding: 5px;">
         <iframe id="bumen" scrolling="yes" frameborder="0" src="../Main/SelectDanWei.aspx?TableName=ERPUser&LieName=UserName" style="width: 100%; height: 100%;"></iframe>
    </div>
     <div id="win_user" class="easyui-window" data-options="title:'选择用户',iconCls:'icon-search',closed:true,closable:false,minimizable:false,maximizable:false,collapsible:false,resizable:true" style="width: 350px; height: 400px; visibility:hidden; padding: 5px;">
         <iframe id="user" scrolling="yes" frameborder="0" src="../Main/SelectUser.aspx?TableName=ERPUser&LieName=UserName" style="width: 100%; height: 100%;"></iframe>
    </div>
</body>
</html>


