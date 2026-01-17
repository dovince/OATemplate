<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERPListOfExpendedAdd.aspx.cs" Inherits="Financial_ERPListOfExpendedAdd" %>

<html>
<head runat="server">
    <title>
        <%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <link href="../Style/Style.css" rel="stylesheet" />
    <link href="../Style/Style1.css" rel="stylesheet" />
    <link href="../CSS/cxcalendar.css" rel="stylesheet" />
    <link href="../CSS/default/easyui.css" rel="stylesheet" />
    <script type="text/javascript" src="../JS/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../CSS/calendar.js"></script>
    <script type="text/javascript" src="../JS/jquery.easyui.min.js"></script>
    <script src="../JS/common.js?v=202306020823" type="text/javascript"></script>
    <script type="text/javascript">
        var g_RCount = 0;
        $(document).ready(function () {

            g_RCount = parseInt($("#rowCount").val());
            if (g_RCount == 0)
                ERPListOfExpendedDetail_AddRow();


            InitTimeSelect();

            $('#win_bumen').window({
                onBeforeClose: function () {
                    var returnVal = "";
                    for (var i = 0; i < window.length; i++) {
                        if (window[i].frameElement.id == "bumen") {
                            //根据弹出窗内部的ifame的id来定位
                            returnVal = window[i].returnValue;
                        }
                    }
                    $('#txtTBBM')[0].value = returnVal;
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
                        $(idstr).val(returnVal);
                    }

                    //$('#win_user').window('refresh');
                }
            });
            ERPListOfExpendedDetail_RunInitHtml();
            setInterval(ERPListOfExpendedDetail_SaveFormToHide, 1000);

        });
        function InitTimeSelect() {
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
        }
        function replaceAll(str, source, target) {
            str = str.replace(source, target);
            while (str.indexOf(source) > -1) {
                str = str.replace(source, target);
            }
            return str;
        }
        function ERPListOfExpendedDetail_AddRow() {
            var rowCount = $(".ZLBody tr").length + 1;
            var html = $("#ZLBodyTr").html();
            html = replaceAll(html, "{RIndex}", "" + rowCount);
            html = replaceAll(html, "{Replace}", "" + rowCount);
            html = "<tr>" + html + "</tr>";
            $(".ZLBody").append(html);
            g_RCount++;
            $("#rowCount").val(g_RCount);
            InitTimeSelect();
        }

        //删除行事件
        function ERPListOfExpendedDetail_DelRow(sender) {
            $(sender).parent().parent().remove();
            //重置行ID
            var count = 0;
            $(".ZLBody tr").each(function (index, item) {
                var newID = index + 1;
                $(item).find(".rowIndexLabel")[0].innerHTML = newID;
                var rowIndexHidden = $(item).find(".rowIndexHidden")[0];
                rowIndexHidden.id = "rowIndex_" + newID;
                rowIndexHidden.name = "rowIndex_" + newID;
                rowIndexHidden.value = newID;
            });
            //$("#rowCount").val($(".ZLBody tr").length);
        }
        function ERPListOfExpendedDetail_SaveFormToHide() {
            $("#HiddenField_CTBody").val($(".ZLBody").html());
        }
        function ERPListOfExpendedDetail_RunInitHtml() {
            eval($("#HiddenField_InitHtml").val());
            $("#HiddenField_InitHtml").val("");
        }
        function openselectDialog(index) {
            //debugger;
            var pre = "ERPListOfExpendedDetail_";
            var xmbhid = pre + 'XHBH' + '_' + index;
            var RadNum = Math.random();
            var url = '../BusinessManage/CommonSelect.aspx?TypeStr=XMBH&Radstr=' + RadNum;
            var options = {
                title: "选择项目",
                url: url,
                onFinish: function (data) {
                    var xmbh = $("#" + xmbhid).val();
                    var options = {
                        url: '../Main/GetJsonResultHandler.ashx',
                        async: false,
                        data: { flag: "ListOfExpended", xmbh: xmbh },
                        dataType: "json",
                        success: function (data) {
                            if (data.Code && data.Data != null) {
                                var d = data.Data;
                                $("#" + pre + 'XMName' + '_' + index).val(d.XMName);
                                $("#" + pre + 'Amount' + '_' + index).val(d.HTJE);
                                $("#" + pre + 'Budget' + '_' + index).val(d.BudgetSum);
                                $("#" + pre + 'CostedAmt' + '_' + index).val(d.CostSums);
                                $("#" + pre + 'CostingPercent' + '_' + index).val(0.00 + "%");
                                var amount = d.HTJE;
                                if (d.CostMoneySUM > 0) {
                                    amount = d.CostMoneySUM;
                                }
                                else if (d.结算金额 > 0) {
                                    amount = d.结算金额;
                                }
                                $("#" + pre + 'CostedPercent' + '_' + index).val(((d.CostSums / amount) * 100).toFixed(2) + "%");
                            }
                        },
                    };
                    MakeRequestAjax(options);
                }
            }
            showPopwindow(xmbhid, options);
        }
    </script>
    <style type="text/css">

        .com_input {
            width: 100px;
        }

        .com_input_large {
            width: 150px;
        }

        .com_input_small {
            width: 100px;
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
</head>
<body onload="javascript:document.getElementById('TextBox5').readOnly=true;">
    <form id="form1" runat="server">
        <div style="display: none;">
        </div>
        <div>
            <table id="PrintHide" style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="middle" style="border-bottom: #006633 1px ; height: 30px;">&nbsp;<img src="../images/BanKuaiJianTou.gif" />
                        <a class="hei" href="../Main/MyDesk.aspx">桌面</a>&nbsp;>>&nbsp;费用成本报销
                        <div style="display: none;">
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <asp:HiddenField ID="HiddenField_formcontent" runat="server" />
                            <asp:HiddenField ID="HiddenField_xmqqbh" runat="server" />
                            <asp:HiddenField ID="HiddenField_WenJianList" runat="server" />
                            <asp:HiddenField ID="HiddenField_CTBody" runat="server" />
                            <asp:HiddenField ID="HiddenField_InitHtml" runat="server" />
                            <asp:HiddenField ID="rowCount" runat="server" Value="0" />
                        </div>
                    </td>
                    <td align="right" valign="middle" style="border-bottom: #006633 1px ; height: 30px;">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/Button/Submit.jpg" OnClick="ImageButton1_Click" />
                        <img src="../images/Button/JianGe.jpg" />&nbsp;
                        <img class="HerCss" onclick="javascript:window.history.go(-1)" src="../images/Button/BtnExit.jpg" />&nbsp;
                    </td>
                </tr>
            </table>
            <table style="width: 100%" bgcolor="#ffffff" border="0" cellpadding="2" cellspacing="0">
                <tr>
                    <td>
                        <table style="width: 100%" bgcolor="#999999" border="0" cellpadding="2" cellspacing="1">
                            <tr>
                                <td class="TitleStyle" colspan="4">
                                    <strong>费用成本报销</strong>
                                </td>
                            </tr>
                            <tr style="height: 30px;">
                                <td class="MainStyle">报账部门：&nbsp;&nbsp;
                                </td>

                                <td style="background-color: #ffffff" colspan="1">
                                    <asp:TextBox ID="txtDepartment" runat="server" Width="320px" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="MainStyle">登记人：&nbsp;&nbsp;
                                </td>

                                <td style="background-color: #ffffff" colspan="1">
                                    <asp:TextBox ID="txtUsername" runat="server" Width="320px" Enabled="false"></asp:TextBox>
                                </td>

                            </tr>

                            <tr style="height: 30px;">
                                <td class="MainStyle">说明：&nbsp;&nbsp;
                                </td>

                                <td style="background-color: #ffffff" colspan="3">
                                    <asp:TextBox ID="txtWorkName" runat="server" Width="600px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="padding: 0px;">
                                    <table id="QingDan" style="width: 100%" bgcolor="#999999" border="0" cellpadding="1" cellspacing="1">
                                        <thead>
                                            <tr>
                                                <td class="TitleStyle" colspan="12" style="text-align: center"><strong>费用成本报销详细</strong></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; background-color: #ffffff;">序号</td>

                                                <td style="text-align: center; background-color: #ffffff;">项目名称</td>

                                                <td style="text-align: center; background-color: #ffffff;">支出类别</td>

                                                <td style="text-align: center; background-color: #ffffff;">摘要*</td>

                                                <td style="text-align: center; background-color: #ffffff;">项目编号</td>

                                                <td style="text-align: center; background-color: #ffffff;">合同/结算金额</td>

                                                <td style="text-align: center; background-color: #ffffff;">预算金额</td>

                                                <td style="text-align: center; background-color: #ffffff;">已支付</td>

                                                <td style="text-align: center; background-color: #ffffff;">报销费用金额*</td>

                                                <td style="text-align: center; background-color: #ffffff;">单项支出比例</td>

                                                <td style="text-align: center; background-color: #ffffff;">所有支出比例</td>

                                                <td style="text-align: center; background-color: #ffffff;">
                                                    <img src="../images/Button/BtnAdd.jpg" style="border-width: 0px; vertical-align: middle" class="addRow" onclick="ERPListOfExpendedDetail_AddRow()" />
                                                </td>
                                            </tr>
                                        </thead>
                                        <tr id="ZLBodyTr" style="display: none;">
                                            <td style="text-align: center; background-color: #ffffff;">
                                                <label class="rowIndexLabel">{RIndex}</label>
                                                <input type="hidden" id="rowIndex_{RIndex}" name="rowIndex_{RIndex}" value="{RIndex}" class="rowIndexHidden" />
                                            </td>

                                            <td style="text-align: center; background-color: #ffffff;">
                                                <input id="ERPListOfExpendedDetail_XMName_{Replace}" name="ERPListOfExpendedDetail_XMName_{Replace}" readonly="readonly" class="com_input_large" type="text" />
                                                <img class="HerCss" onclick="openselectDialog('{Replace}')" src="../images/Button/search.gif" alt="" />
                                            </td>

                                            <td style="text-align: center; background-color: #ffffff;">
                                                <select id="ERPListOfExpendedDetail_ZCLB_{Replace}" name="ERPListOfExpendedDetail_ZCLB_{Replace}" class="com_input">
                                                    <option value="工资及津贴">1.工资及津贴</option>
                                                    <option value="节日补贴">2.节日补贴</option>
                                                    <option value="养老统筹">3.养老统筹</option>
                                                    <option value="福利费">4.福利费</option>
                                                    <option value="劳动保护费">5.劳动保护费</option>
                                                    <option value="住房公积金">6.住房公积金</option>
                                                    <option value="住房补贴">7.8.住房补贴</option>
                                                    <option value="材料费">9.材料费</option>
                                                    <option value="工程出包费">10.工程出包费</option>
                                                    <option value="固定资产">11.固定资产</option>
                                                    <option value="办公费">12.办公费</option>
                                                    <option value="差旅费">13.差旅费</option>
                                                    <option value="水电费">14.水电费</option>
                                                    <option value="物业管理费">15.物业管理费</option>
                                                    <option value="交通运输费用">16.交通运输费用</option>
                                                    <option value="邮电费用">17.邮电费用</option>
                                                    <option value="维修费用">18.维修费用</option>
                                                    <option value="会议费">19.会议费</option>
                                                    <option value="培训费">20.培训费</option>
                                                    <option value="业务招待费">21.业务招待费</option>
                                                    <option value="劳务费">22.劳务费</option>
                                                    <option value="租赁费">23.租赁费</option>
                                                    <option value="税金及附加">24.税金及附加</option>
                                                    <option value="安全生产费用">25.安全生产费用</option>
                                                    <option value="工会经费">26.工会经费</option>
                                                    <option value="其它费用">27.其他费用</option>
                                                </select>
                                            </td>

                                            <td style="text-align: center; background-color: #ffffff;">
                                                <input id="ERPListOfExpendedDetail_Summary_{Replace}" name="ERPListOfExpendedDetail_Summary_{Replace}" readonly="readonly" class="com_input" type="text" /></td>

                                            <td style="text-align: center; background-color: #ffffff;">
                                                <input id="ERPListOfExpendedDetail_XHBH_{Replace}" name="ERPListOfExpendedDetail_XHBH_{Replace}" readonly="readonly" class="com_input" type="text" />
                                            </td>

                                            <td style="text-align: center; background-color: #ffffff;">
                                                <input id="ERPListOfExpendedDetail_Amount_{Replace}" name="ERPListOfExpendedDetail_Amount_{Replace}" readonly="readonly" class="com_input" type="text" style="text-align: right;" /></td>

                                            <td style="text-align: center; background-color: #ffffff;">
                                                <input id="ERPListOfExpendedDetail_Budget_{Replace}" name="ERPListOfExpendedDetail_Budget_{Replace}" readonly="readonly" class="com_input" type="text" style="text-align: right;" /></td>

                                            <td style="text-align: center; background-color: #ffffff;">
                                                <input id="ERPListOfExpendedDetail_CostedAmt_{Replace}" name="ERPListOfExpendedDetail_CostedAmt_{Replace}" readonly="readonly" class="com_input" type="text" style="text-align: right;" /></td>

                                            <td style="text-align: center; background-color: #ffffff;">
                                                <input id="ERPListOfExpendedDetail_CostingAmt_{Replace}" name="ERPListOfExpendedDetail_CostingAmt_{Replace}" class="com_input" type="text" style="text-align: right;" /></td>

                                            <td style="text-align: center; background-color: #ffffff;">
                                                <input id="ERPListOfExpendedDetail_CostingPercent_{Replace}" name="ERPListOfExpendedDetail_CostingPercent_{Replace}" readonly="readonly" class="com_input" type="text" style="text-align: right;" /></td>

                                            <td style="text-align: center; background-color: #ffffff;">
                                                <input id="ERPListOfExpendedDetail_CostedPercent_{Replace}" name="ERPListOfExpendedDetail_CostedPercent_{Replace}" readonly="readonly" class="com_input" type="text" style="text-align: right;" /></td>

                                            <td style="text-align: center; background-color: #ffffff;">
                                                <img src="../images/Button/BtnDel.jpg" style="border-width: 0px;" onclick="ERPListOfExpendedDetail_DelRow(this)" /></td>
                                        </tr>
                                        <tbody class="ZLBody">
                                            <%if (!string.IsNullOrEmpty(this.HiddenField_CTBody.Value))
                                                { %>
                                            <%=this.HiddenField_CTBody.Value %>
                                            <%  } %>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr style="height: 30px;">
                                <td class="MainStyle">报销合计：&nbsp;&nbsp;
                                </td>

                                <td style="background-color: #ffffff" colspan="3">
                                    <asp:TextBox ID="txtAmount" runat="server" Width="320px" Enabled="false"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td class="MainStyle">附件文件：&nbsp;&nbsp;
                                </td>
                                <td colspan="3" style="padding-left: 5px; height: 25px; background-color: #ffffff">
                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                                    </asp:CheckBoxList>
                                    &nbsp;<asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False"
                                        ImageAlign="AbsMiddle" ImageUrl="../images/Button/DelFile.jpg" OnClick="ImageButton3_Click" />
                                    &nbsp; &nbsp;&nbsp;
                                <asp:ImageButton ID="ImageButton5" runat="server" CausesValidation="False" ImageAlign="AbsMiddle"
                                    ImageUrl="~/images/Button/ReadFile.gif" OnClick="ImageButton5_Click" ToolTip="如果无法阅读或编辑文件，请在主页面点击【关于】下载安装相关插件" />
                                    &nbsp; &nbsp;&nbsp;
                                <asp:ImageButton ID="ImageButton6" runat="server" CausesValidation="False" ImageAlign="AbsMiddle"
                                    ImageUrl="~/images/Button/EditFile.gif" OnClick="ImageButton6_Click" ToolTip="如果无法阅读或编辑文件，请在主页面点击【关于】下载安装相关插件" />
                                </td>
                            </tr>
                            <tr>
                                <td class="MainStyle">上传附件：&nbsp;&nbsp;
                                </td>
                                <td colspan="3"
                                    style="padding-left: 5px; height: 25px; background-color: #ffffff">
                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="350px" />
                                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" ImageAlign="AbsMiddle"
                                        ImageUrl="../images/Button/UpLoad.jpg" OnClick="ImageButton2_Click" />
                                </td>
                            </tr>
                            <tr <%= !string.IsNullOrEmpty(Request.QueryString["Nwid"]) ? "style='display:none;'" : "" %>>
                                <td class="TitleStyle" colspan="4">
                                    <strong>流程审批附加属性</strong>
                                </td>
                            </tr>
                            <tr <%= !string.IsNullOrEmpty(Request.QueryString["Nwid"]) ? "style='display:none;'" : "" %>>
                                <td class="MainStyle">下一节点选择：&nbsp;&nbsp;
                                </td>
                                <td colspan="3"
                                    style="padding-left: 5px; height: 25px; background-color: #ffffff">
                                    <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"
                                        Width="350px">
                                    </asp:DropDownList>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" Text="根据条件自动决定下一节点" />
                                </td>
                            </tr>
                            <tr <%= !string.IsNullOrEmpty(Request.QueryString["Nwid"]) ? "style='display:none;'" : "" %>>
                                <td class="MainStyle">评审模式：&nbsp;&nbsp;
                                </td>
                                <td colspan="3"
                                    style="padding-left: 5px; height: 25px; background-color: #ffffff">
                                    <asp:TextBox ID="TextBox1" runat="server" BorderStyle="None" BorderWidth="0px" ReadOnly="True"
                                        Width="350px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr <%= !string.IsNullOrEmpty(Request.QueryString["Nwid"]) ? "style='display:none;'" : "" %>>
                                <td class="MainStyle">审批人选择模式：&nbsp;&nbsp;
                                </td>
                                <td colspan="3"
                                    style="padding-left: 5px; height: 25px; background-color: #ffffff">
                                    <asp:TextBox ID="TextBox2" runat="server" BorderStyle="None" BorderWidth="0px" ReadOnly="True"
                                        Width="350px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr <%= !string.IsNullOrEmpty(Request.QueryString["Nwid"]) ? "style='display:none;'" : "" %>>
                                <td class="MainStyle">审批人选择：<a style="color: #FF0000; font-size: 18px;">*</a>
                                </td>
                                <td colspan="3"
                                    style="padding-left: 5px; height: 25px; background-color: #ffffff">
                                    <asp:TextBox ID="TextBox5" runat="server" onkeydown="javascript:return false;" Width="349px"></asp:TextBox>
                                    <img class="HerCss" id="Img3" onclick="showUserPopwindow('TextBox5')" src="../images/Button/search.gif" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox5"
                                        Display="Dynamic" ErrorMessage="*必须指定审批人"></asp:RequiredFieldValidator><asp:CheckBox ID="CHKSMS" runat="server" Checked="True" /><img
                                            src="../images/TreeImages/@sms.gif" />短消息<asp:CheckBox ID="CHKMOB" runat="server" /><img
                                                src="../images/TreeImages/mobile_sms.gif" />短信息
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
