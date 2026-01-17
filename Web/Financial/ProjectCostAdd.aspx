<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectCostAdd.aspx.cs" Inherits="Financial_ProjectCostAdd" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head runat="server">
    <title>
        <%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" rel="stylesheet" />
    <link href="../Style/Style1.css" rel="stylesheet" />
    <link href="../CSS/cxcalendar.css" rel="stylesheet" />
    <link href="../CSS/default/easyui.css" rel="stylesheet" />
    <link href="../CSS/icon.css" rel="stylesheet" />
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
    <script type="text/javascript" src="../JS/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../JS/common.js?v=20240613"></script>
    <script type="text/javascript" src="../CSS/calendar.js"></script>
    <script type="text/javascript" src="../JS/jquery.easyui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".input_cxcalendar").each(function () {
                //debugger
                var a = new Calendar({
                    targetCls: $(this),
                    type: 'yyyy-mm-dd',
                    wday: 2
                }, function (val) {
                    //console.log(val);
                });
            });
            $('#win_select').window({
                onBeforeClose: function () {
                    var returnVal = "";
                    for (var i = 0; i < window.length; i++) {
                        if (window[i].frameElement.id == "select") {
                            //根据弹出窗内部的ifame的id来定位
                            returnVal = window[i].returnValue;
                            document.getElementById('XMJBXXID').value = returnVal;
                            document.getElementById('btnaddsomething').click();
                        }
                    }
                    //$('#txtXMID')[0].value = returnVal;
                    //document.getElementById('btnaddsomething').click();
                }
            });
        });

        function openselectDialog() {
            var RadNum = Math.random();
            $("#select")[0].src += '&Radstr=' + RadNum;
            $("#win_select").css("visibility", "visible");
            $('#win_select').window('open');
        }

        function PrintTable() {
            document.getElementById("PrintHide").style.visibility = "hidden"
            print();
            document.getElementById("PrintHide").style.visibility = "visible"
        }

    </script>
    <style>
        h1, h2, h3 {
            font: bold 36px/1 "\5fae\8f6f\96c5\9ed1";
        }

        h2 {
            font-size: 20px;
        }

        h3 {
            font-size: 16px;
        }

        fieldset {
            margin: 1em 0;
        }

            fieldset legend {
                font: bold 14px/2 "\5fae\8f6f\96c5\9ed1";
            }

        a {
            color: #06f;
            text-decoration: none;
        }

            a:hover {
                color: #00f;
            }

        .wrap {
            width: 600px;
            margin: 0 auto;
            padding: 20px 40px;
            border: 2px solid #999;
            border-radius: 8px;
            background: #fff;
            box-shadow: 0 0 10px rgba(0,0,0,0.5);
        }
    </style>
    <style type="text/css">
        #Checkbox2 {
            width: 62px;
        }

        #Text1 {
            width: 350px;
        }

        #Txtxinxibianhao {
        }

        #Text2 {
            width: 175px;
        }

        #Select1 {
            width: 175px;
        }

        #Select2 {
            width: 175px;
        }

        #Select3 {
            width: 175px;
        }

        .style22 {
            width: 117px;
            text-align: right;
            height: 25px;
        }

        .style24 {
            height: 25px;
        }

        .highlight {
            font-weight: bold;
            color: Red;
        }

        .style25 {
            height: 25px;
        }

        .style26 {
            width: 107px;
            text-align: right;
            height: 80px;
        }

        .style27 {
            height: 65px;
            width: 800px;
        }

        .style36 {
            height: 25px;
            width: 117px;
        }

        .style51 {
            text-align: center;
            height: 25px;
        }

        .style52 {
            height: 25px;
            width: 107px;
        }

        .style53 {
            width: 107px;
            text-align: right;
            height: 25px;
        }

        .style57 {
            height: 25px;
            width: 420px;
        }

        .style58 {
            width: 117px;
        }

        .style59 {
            width: 381px;
        }

        .MainStyle {
            text-align: right;
            font: 14px "微软雅黑";
            background-color: #f6f6f6;
            height: 30px;
        }

        .auto-style5 {
            height: 25px;
        }

        .auto-style7 {
            font-size: large;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="tb_normal" style="width: 100%">
                <tr>
                    <td valign="middle" style="border-bottom: #006633 1px; height: 30px;">&nbsp;<img src="../images/BanKuaiJianTou.gif" />
                        <a class="hei" href="../Main/MyDesk.aspx">桌面</a>&nbsp;&gt;&gt;&nbsp;财务管理&nbsp;&gt;&gt; 项目成本核算信息添加</td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px; height: 30px;">
                        <asp:HiddenField ID="XMJBXXID" runat="server" />
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/Button/Submit.jpg"
                            OnClick="ImageButton1_Click" Style="height: 19px" />
                        <img src="../images/Button/JianGe.jpg" /><asp:ImageButton ID="ImageButton_goback" ImageUrl="~/images/Button/BtnExit.jpg" runat="server" OnClick="ImageButton_goback_Click" CausesValidation="False" />

                    </td>
                </tr>
            </table>
            <table class="tb_normal" style="width: 100%">
                <tr>
                    <td class="TitleStyle" colspan="6">
                        <strong><span class="auto-style7">项目成本核算</span></strong></td>
                </tr>
                <tr>
                    <td class="MainStyle">项目编号：</td>
                    <td class="auto-style5" style="background-color: #ffffff">
                        <asp:TextBox ID="txtXMID" runat="server" Width="280px"
                            onkeypress="event.returnValue=false;" AutoPostBack="True" ToolTip="如果找不到项目编号，请先在项目管理中登记项目基本信息" Wrap="False" ReadOnly="True"></asp:TextBox>


                        <img class="HerCss" onclick="openselectDialog()" src="../images/Button/search.gif" />
                        <%-- <img class="HerCss" onclick="var xmlist;xmlist=window.showModalDialog('../BusinessManage/CommonSelect.aspx?TypeStr=XMBH','','dialogWidth:647px;DialogHeight=400px;status:no;help:no;resizable:yes;');if(xmlist==null){}else{document.getElementById('XMJBXXID').value=xmlist;document.getElementById('btnaddsomething').click();}"
                                    src="../images/Button/search.gif" />--%>
                        <br />
                        <a style="color: #FF0000; font-size: 14px;">(只能关联部门负责人审核通过的项目信息)</a>
                    </td>
                    <td class="MainStyle">项目名称：</td>
                    <td class="style24" style="background-color: #ffffff">
                        <asp:TextBox ID="txtXMName" runat="server" Width="320px" Height="59px" TextMode="MultiLine"></asp:TextBox>

                    </td>
                    <td class="MainStyle">合同编号：</td>
                    <td class="style24" style="background-color: #ffffff">
                        <asp:TextBox ID="txtHTBH" runat="server" Width="200px" ToolTip="如果没有合同编号可以不填，在合同签订评审时会自动关联" Wrap="False"></asp:TextBox>

                        <asp:Button ID="btnaddsomething" runat="server" BackColor="White" BorderColor="White"
                            CausesValidation="False" ForeColor="White" Height="16px" OnClick="btnaddsomething_Click"
                            Text="1" Width="1" />

                    </td>
                </tr>
                <tr>
                    <td class="MainStyle">专业类别：</td>
                    <td class="auto-style5" style="background-color: #ffffff">
                        <asp:TextBox ID="txtZYLB" runat="server" Width="320px"></asp:TextBox>

                    </td>
                    <td class="MainStyle">承接部门：</td>
                    <td class="style24" style="background-color: #ffffff">
                        <asp:TextBox ID="txtXMbumen" runat="server" Width="320px"></asp:TextBox>

                    </td>
                    <td class="MainStyle">项目负责人：</td>
                    <td class="style24" style="background-color: #ffffff">
                        <asp:TextBox ID="txtXMFZR" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="MainStyle">项目周期：</td>
                    <td class="auto-style5" style="background-color: #ffffff">
                        <asp:TextBox ID="txtXMstarttime" runat="server" Width="120px" class="input_cxcalendar"></asp:TextBox>
                        &nbsp; 至&nbsp;
                    <asp:TextBox ID="txtXMendtime" runat="server" Width="120px" class="input_cxcalendar"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtXMstarttime"
                            ControlToValidate="txtXMendtime" Display="Dynamic" ErrorMessage="CompareValidator"
                            Operator="GreaterThanEqual">截止时间要在起始日期之后</asp:CompareValidator>
                    </td>
                    <td class="MainStyle">项目状态：</td>
                    <td class="style24" style="background-color: #ffffff">
                        <asp:TextBox ID="txtXMState" runat="server" Width="320px"></asp:TextBox>
                    </td>
                    <td class="MainStyle">登记时间：</td>
                    <td class="style24" style="background-color: #ffffff">
                        <asp:TextBox ID="txtDJTime" runat="server" class="input_cxcalendar" Width="320px"></asp:TextBox>
                        &nbsp;(若为关联信息，该项不用填写)</td>
                </tr>
                <tr>
                    <td class="MainStyle">合同金额：</td>
                    <td class="auto-style5" style="background-color: #ffffff">
                        <asp:TextBox ID="txtXMJingFei" onkeypress="if((event.keyCode<48||event.keyCode>57)&&event.keyCode!=45&&event.keyCode!=46)event.returnValue=false;" Style="ime-mode: disabled;" runat="server" Width="244px"></asp:TextBox>&nbsp;元
                    </td>
                    <td class="MainStyle">结算金额：</td>
                    <td class="style24" style="background-color: #ffffff">
                        <asp:TextBox ID="txt结算金额" onkeypress="if((event.keyCode<48||event.keyCode>57)&&event.keyCode!=45&&event.keyCode!=46)event.returnValue=false;" runat="server" Width="250px"></asp:TextBox>
                        &nbsp;元</td>
                    <td class="MainStyle">应付金额：</td>
                    <td class="style24" style="background-color: #ffffff">
                        <asp:TextBox ID="txt成本支出合计" runat="server" Width="200px"></asp:TextBox>
                        &nbsp;元</td>
                </tr>
                <tr>
                    <td class="MainStyle">项目金额：</td>
                    <td class="style24" colspan="5" style="background-color: #ffffff">
                        <asp:TextBox ID="txt项目金额" runat="server" Width="244px"></asp:TextBox>
                        &nbsp;元</td>
                </tr>
            </table>
        </div>
    </form>

    <div id="win_select" class="easyui-window" data-options="title:'选择项目',iconCls:'icon-search',closed:true,closable:false,minimizable:false,maximizable:false,collapsible:false,resizable:true" style="width: 650px; height: 500px; visibility: hidden; padding: 5px;">
        <iframe id="select" scrolling="yes" frameborder="0" src="../BusinessManage/CommonSelect.aspx?TypeStr=XMBHANDHTBH" style="width: 100%; height: 100%;"></iframe>
    </div>
</body>
</html>



