<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LanEmailShou.aspx.cs" Inherits="LanEmail_LanEmailShou" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
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
        //if(Modifynumber>1)
        //{
        //  alert("只允许选择一项！");
        //  return false;
        //}
        return true;
    }



</script>
<body class="lui_form_body">
    <form id="form1" runat="server">
        <div>
            <table class="tb_normal" style="width: 100%">
                <tr>
                    <td valign="middle" style="border-bottom: #006633 1px ;" class="auto-style1">&nbsp;<img src="../images/BanKuaiJianTou.gif" />
                        <a class="hei" href="../Main/MyDesk.aspx">桌面</a>&nbsp;>>&nbsp;内部邮件&nbsp;>>&nbsp;收件箱&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    </td>
                    <td valign="middle" style="border-bottom: #006633 1px ;" class="auto-style2">
                        <asp:Button ID="Buttonallread" runat="server" OnClientClick="javascript:return CheckModify();" Text="勾选的邮件设置为已读" ToolTip="将当前页中选中的邮件全部设置为已读" OnClick="ButtonSelectread_Click" Width="155px" />
                    </td>
                    <td valign="middle" style="border-bottom: #006633 1px ;" class="auto-style2">
                        <asp:Button ID="Buttonallread0" runat="server" Text="全部设置为已读" ToolTip="将收件箱中的全部邮件设置为已读" OnClick="Buttonallread_Click" Width="164px" />
                    </td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px ; height: 30px;">主题：<asp:TextBox ID="TextBox1" runat="server" Height="20px" Width="60px"></asp:TextBox>发送人：<asp:TextBox
                        ID="TextBox2" runat="server" Height="20px" Width="60px"></asp:TextBox><img class="HerCss"
                            onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectUser.aspx?TableName=ERPUser&LieName=UserName&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox2').value=wName;}"
                            src="../images/Button/search.gif" />状态：<asp:TextBox ID="TextBox3" runat="server" Height="20px" Width="60px"></asp:TextBox><img
                                class="HerCss" onclick="var wName;var RadNum=Math.random();wName=window.showModalDialog('../Main/SelectCondition.aspx?TableName=ERPLanEmail&LieName=EmailState&Radstr='+RadNum,'','dialogWidth:350px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(wName==null){}else{document.getElementById('TextBox3').value=wName;}"
                                src="../images/Button/search.gif" />
                        <asp:ImageButton
                            ID="ImageButton4" runat="server" ImageAlign="AbsMiddle" ImageUrl="../images/Button/BtnSerch.jpg"
                            OnClick="ImageButton4_Click" />&nbsp;
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../images/Button/BtnAdd.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton1_Click" />&nbsp;
                    <asp:ImageButton ID="ImageButton3" runat="server" OnClientClick="javascript:return CheckDel();" ImageUrl="../images/Button/BtnDel.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton3_Click" Style="height: 19px" />
                        &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../images/Button/BtnReport.jpg" ImageAlign="AbsMiddle" OnClick="ImageButton2_Click" />&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px ; height: 30px;">&nbsp;</td>
                </tr>
            </table>
            <table class="tb_normal" style="width: 100%">
                <tr>
                    <td>
                        <asp:GridView ID="GVData" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
                            OnRowDataBound="GVData_RowDataBound" PageSize="15" Width="100%" CssClass="tb_normal">
                            <PagerSettings Mode="NumericFirstLast" Visible="False" />
                            <PagerStyle BackColor="LightSteelBlue" HorizontalAlign="Right" />
                            <HeaderStyle  HorizontalAlign="Center" Font-Size="12px" Height="30px" />
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
                                    <HeaderTemplate>
                                        <input id="CheckBoxAll" onclick="CheckAll()" type="checkbox" />
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="邮件主题">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="True"
                                            NavigateUrl='<%# "EmailView.aspx?ID="+ DataBinder.Eval(Container.DataItem, "ID")%>'><%# DataBinder.Eval(Container.DataItem, "EmailTitle")%></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="项目编号@项目名称">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink2" runat="server" Font-Underline="True"
                                            NavigateUrl='<%# "../BusinessManage/NWorkToDoView.aspx?FormID="+ DataBinder.Eval(Container.DataItem, "FormID") + "&WorkFlowID=" + DataBinder.Eval(Container.DataItem, "WorkFlowID") + "&BeiYong1=" + DataBinder.Eval(Container.DataItem, "BeiYong1")%>'><%# DataBinder.Eval(Container.DataItem, "BeiYong1")%>
                                        </asp:HyperLink>
                                    </ItemTemplate>

                                </asp:TemplateField>


                                <asp:BoundField DataField="FromUser" HeaderText="发送人">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TimeStr" HeaderText="发送时间">
                                    <ItemStyle Width="130px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EmailState" HeaderText="邮件状态">
                                    <ItemStyle Width="100px" />
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
                    <td style="text-align:center;">共<asp:Label ID="Labelrowcount" runat="server" Text="0"></asp:Label>
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
                <asp:TextBox ID="TxtPageSize" runat="server" CssClass="TextBoxCssUnder2" Height="20px"
                    Width="35px">15</asp:TextBox>
                        行每页 &nbsp; 转到第<asp:TextBox ID="GoPage" runat="server" CssClass="TextBoxCssUnder2"
                            Height="20px" Width="33px"></asp:TextBox>
                        页&nbsp;
                <asp:ImageButton ID="ButtonGo" runat="server" OnClientClick="javascript:return CheckValuePiece();" ImageUrl="../images/Button/Jump.jpg" OnClick="ButtonGo_Click" /></td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
