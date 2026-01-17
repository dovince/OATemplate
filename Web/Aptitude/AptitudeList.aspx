<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AptitudeList.aspx.cs" Inherits="Aptitude_AptitudeList" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title><%=ZWL.Common.PublicMethod.GetSysTitle()%></title>
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
    <style type="text/css">
        .auto-style1 {
            height: 30px;
            width: 160px;
        }

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
            <table class="tb_normal" style="width: 100%">
                <tr>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px;">&nbsp;<img src="../images/BanKuaiJianTou.gif" />
                        资质证照管理&nbsp;&gt;&gt;&nbsp;资质使用申请审批&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="../images/Button/BtnRefresh.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton12_Click" Style="height: 19px" />
                        <asp:HiddenField ID="HiddenField_query" runat="server" />
                    </td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px;">
                        <input id="btn_query" type="button" value="显示查询" />&nbsp;&nbsp; &nbsp;
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../images/Button/BtnAdd.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton1_Click" Style="height: 19px" />
                        <%--<asp:ImageButton ID="ImageButton5" runat="server" ImageAlign="AbsMiddle" ImageUrl="../images/Button/BtnModify.jpg" OnClick="ImageButton5_Click" OnClientClick="javascript:return CheckModify();" Style="height: 19px" />--%>
                        <asp:ImageButton ID="ImageButton3" runat="server" OnClientClick="javascript:return CheckDel();" ImageUrl="../images/Button/BtnDel.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton3_Click" Style="height: 19px" Visible="False" />
                        &nbsp;
                        <%--<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../images/Button/BtnReport.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton2_Click" Style="height: 19px" />--%>
                        &nbsp;&nbsp;&nbsp;&nbsp;</td>
                </tr>
            </table>
            <table class="tb_normal" style="width: 100%">
                <tr id="querytr">
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; background-color: #f6f6f6"><strong>申请人:</strong></td>
                    <td align="left" valign="middle" style="border-bottom: #006633 1px; width: 10%;" class="auto-style1">
                        <asp:TextBox ID="TextBox_qjr" runat="server" Width="100px"></asp:TextBox>
                        <img class="HerCss" id="Img2" onclick="openuserDialog('qjr')" src="../images/Button/search.gif" />
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; background-color: #f6f6f6"><strong>使用部门：</strong></td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; text-align: left;">
                        <asp:DropDownList ID="ddlDepartment" runat="server"></asp:DropDownList>
                    </td>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; background-color: #f6f6f6" class="auto-style2"><strong>是否归还：</strong></td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; text-align: left;">
                        <asp:DropDownList ID="ddlReturn" runat="server" Width="100px" Height="22px">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="0">是</asp:ListItem>
                            <asp:ListItem Value="1">否</asp:ListItem>
                        </asp:DropDownList>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; text-align: right; background-color: #f6f6f6; font-weight: 700;">申请时间：</td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; text-align: left;">从：<asp:TextBox ID="txb_Start" runat="server" Width="100px" class="input_cxcalendar"></asp:TextBox>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%; text-align: left;">到：<asp:TextBox ID="txb_End" runat="server" Width="98px" class="input_cxcalendar"></asp:TextBox>
                    </td>
                    <td align="middle" valign="middle" style="border-bottom: #006633 1px; height: 30px; width: 10%;">
                        <asp:Button ID="Button_Query" runat="server" Style="text-align: center" Text="查询" OnClick="Button_Query_Click" />
                </tr>
            </table>
        </div>
        <table class="tb_normal" style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="GVData" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="tb_normal" OnRowDataBound="GVData_RowDataBound" PageSize="15"
                        Width="100%" EnableModelValidation="True" Style="text-align: center">
                        <PagerSettings Mode="NumericFirstLast" Visible="False" />
                        <PagerStyle BackColor="LightSteelBlue" HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" Font-Size="12px" Height="30px" />
                        <AlternatingRowStyle BackColor="WhiteSmoke" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="LabVisible" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ID")%>' Visible="False"></asp:Label>
                                    <asp:CheckBox ID="CheckSelect" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle Width="20px" />
                                <HeaderTemplate>
                                    <input id="CheckBoxAll" onclick="CheckAll()" type="checkbox" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="资质证照信息">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="True"
                                        NavigateUrl='<%# "../NWorkFlow/NWorkToDoViewNew.aspx?ID="+ ZWL.Common.PublicMethod.EncryptParam(DataBinder.Eval(Container.DataItem, "NWorkID"))%>' ForeColor="Red">查看详细信息</asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" ForeColor="Red" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="No" HeaderText="申请单据"></asp:BoundField>
                            <asp:BoundField DataField="ProjectNo" HeaderText="项目名称">
                                <ItemStyle ForeColor="#0066FF" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Operator" HeaderText="申请人"></asp:BoundField>
                            <asp:BoundField DataField="Department" HeaderText="使用部门"></asp:BoundField>
                            <asp:BoundField DataField="CreatedDate" HeaderText="申请时间" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="JieDianName" HeaderText="节点名称"></asp:BoundField>
                            <asp:BoundField DataField="ShenPiUserList" HeaderText="审批用户"></asp:BoundField>
                            <asp:BoundField DataField="OKUserList" HeaderText="已审批用户"></asp:BoundField>
                            <asp:BoundField DataField="StateNow" HeaderText="当前状态" ItemStyle-Width="60px"></asp:BoundField>
                            <asp:TemplateField HeaderText="是否归还">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblAptState"></asp:Label>
                                </ItemTemplate>
                                <%--<ItemStyle HorizontalAlign="center" ForeColor="#009933" />--%>
                            </asp:TemplateField>
                            <asp:BoundField DataField="StartDate" HeaderText="开始时间" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="EndDate" HeaderText="结束时间" DataFormatString="{0:yyyy-MM-dd}" />
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
                    <asp:HiddenField ID="HdfPageSum" runat="server" Value="15" />
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
