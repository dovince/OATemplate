<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostDetailPostItemsWorkload.aspx.cs" Inherits="Financial_CostDetailPostItemsWorkload" ValidateRequest="false" %>

<%@ Register Assembly="EditableDropDownList" Namespace="EditableControls" TagPrefix="editable" %>

<%@ Import Namespace="ZWL.Common" %>
<%@ Register Src="~/UserControls/UploadFiles.ascx" TagPrefix="uc1" TagName="UploadFiles" %>


<!DOCTYPE html>
<html>
<head runat="server">
    <title><%= ZWL.Common.PublicMethod.GetSysTitle()%></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge; charset=gb2312">
    <link href="../CSS/common/zone.theme.css" rel="stylesheet" />
    <link href="../CSS/common/common.theme.css" rel="stylesheet" />
    <link href="../CSS/common/widget.theme.css" rel="stylesheet" />
    <link href="../CSS/common/process_tab_main.css" rel="stylesheet" />
    <link href="../CSS/default/easyui.css" rel="stylesheet" />
    <link href="../CSS/Loading.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/cxcalendar.css" rel="stylesheet" />
    <link href="../CSS/icon.css" rel="stylesheet" />
    <link href="../CSS/common/form.theme.css" rel="stylesheet" />
    <link href="../CSS/common/dialog.theme.css" rel="stylesheet" />
    <link href="../CSS/common/dnd.css" rel="stylesheet" />
    <link href="../CSS/common/upload.css" rel="stylesheet" />
    <link href="../JS/jquery-ui/css/ui-lightness/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
    <%--<link href="../JS/jquery-ui/css/jquery-ui.css" rel="stylesheet" type="text/css" />--%>
    <script src="../JS/jquery-1.11.2.min.js" type="text/javascript"></script>
    <script src="../JS/jquery.blockUI.js" type="text/javascript"></script>
    <script src="../CSS/calendar.js" type="text/javascript"></script>
    <script src="../JS/jquery.easyui.min.js"></script>
    <script src="../JS/common.js?v=202306020823" type="text/javascript"></script>
    <script src="../JS/lui/tabcontent.js" type="text/javascript"></script>
    <script src="../JS/jquery-ui/js/jquery-ui-1.10.4.custom.min.js"></script>
    <script src="../JS/jquery-ui/js/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../JS/jquery-ui/js/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../JS/jquery-ui/js/jquery.ui.button.js" type="text/javascript"></script>
    <script src="../JS/jquery-ui/js/jquery.ui.position.js" type="text/javascript"></script>
    <script src="../JS/jquery-ui/js/jquery.ui.autocomplete.js" type="text/javascript"></script>
    <script src="../JS/jquery-ui/js/jquery.ui.combobox.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            <%if (!PiLiangSet.IsNullOrEmpty())%>
            <%{%>
            window.returnValue = <%=PiLiangSet%>;
            if (window.parent.length > 0) {
                window.parent.$('#win_select').window('close');
            }
            else {
                window.close();
            }
            <%}%>
            <%if (Action != "View")%>
            <%{%>
            initSupplierAutoComplete();
            <%}%>
            Load_Do();
            <%if (Action == "View")%>
            <%{%>
            setViewInput();
            <%}%>

        });
        function initSupplierAutoComplete() {
            $(".SupplierAutoComplete").autocomplete({
                source: function (request, response) {
                    var thisid = $(this)[0].element.context.id;
                    if (thisid != document.activeElement.id) return;
                    var preid = getElementPreId(thisid);
                    MakeRequestAjax({
                        url: '../Services/Services.ashx',
                        data: {
                            f: 'SupplierFilter',
                            keyword: encodeURIComponent(request.term)
                        },
                        dataType: "json",
                        success: function (data) {
                            response($.map(data.Data, function (item, i) {
                                return {
                                    FBDWMC: item.FBDWMC,
                                    FBFW: item.FBFW,
                                    ZCDZ: item.ZCDZ,
                                    value: (i + 1) + ": " + item.FBDWMC + " (" + item.FBFW + ")" + "(" + item.ZCDZ + ")"
                                };
                            }));
                        }
                    });
                },
                minLength: 1,
                multiple: true,//多查询
                select: function (event, ui) {
                    var id = $(this).attr("id");
                    var preid = id.substring(0, id.lastIndexOf('_') + 1);
                    $(this).val(ui.item.FBDWMC);
                    $(this).attr("title", $(this).attr("title") + ui.item.FBDWMC + ";");
                    return false;
                }
            });
        }
        function projectCostItemBudget(preid) {
            var recodeid = $("#" + preid + "RecordId").val();
            var item = $("#" + preid + "Item").val();
            var sumamt = $("#" + preid + "SubmitAmt").val();
            var option = {
                url: '../Services/Services.ashx',
                data: {
                    f: 'ProjectCostItemBudget',
                    RecordId: encodeURIComponent(recodeid),
                    Item: encodeURIComponent(item),
                    SubmitAmt: encodeURIComponent(sumamt)
                },
                dataType: "json",
                success: function (data) {
                    if (data.Code && data.Data != null) {
                        $("#" + preid + "TotalAmt").val(data.Data.TotalAmt);
                        $("#" + preid + "CostedAmt").val(data.Data.CostedAmt);
                        $("#" + preid + "BudgetAmt").val(data.Data.BudgetAmt);
                        $("#" + preid + "ItemScale").val(data.Data.ItemScale);
                        $("#" + preid + "CostScale").val(data.Data.CostScale);
                    }
                }
            };
            MakeRequestAjax(option);
        }
        function setViewInput() {
            var selects = $(".lui-component #GVData select");
            for (var i = 0; i < selects.length; i++) {
                var item = selects[i];
                item.disabled = true;
            }
            var inputs = $(".lui-component input[type='text']");
            for (var i = 0; i < inputs.length; i++) {
                var item = inputs[i];
                item.readOnly = true;
            }
            var submitBtns = $(".lui-component[title='暂存']");
            for (var i = 0; i < submitBtns.length; i++) {
                var item = submitBtns.eq(i);
                if (item.parents("div").attr("class").indexOf("lui_toolbar_btn") >= 0) {//顶部暂存按钮
                    item.removeClass("lui_toolbar_btn_on");
                    item.find(".lui-component").removeClass("lui_widget_btn_txt");
                }
                item.removeClass("lui_widget_btn");
                item.removeAttr("onclick");
                item.addClass("lui_widget_btn_disabled");
            }
            var viewBtns = $(".lui-component #GVData .btnView");
            for (var i = 0; i < viewBtns.length; i++) {
                var item = viewBtns[i];
                $(item).remove();
            }
            var subamtTxts = $(".lui-component #GVData input[id$='_SubmitAmt']");
            for (var i = 0; i < subamtTxts.length; i++) {
                var item = subamtTxts[i];
                $(item).attr("onblur", "overFormat(this)");
            }
        }
        function overFormatEx(op) {
            var id = $(op).attr("id");
            var preid = getElementPreId(id);
            var qty = $("#" + preid + "Quantity").val();
            var price = $("#" + preid + "Price").val();
            var calperc = $("#" + preid + "CalcPercent").val();
            if (qty != '' && price != '') {
                __doPostBack($(".TextChangedEvent").attr("id").replaceAll("_", "$"), '');
            }
        }
        function amountEx(op) {
            amount(op);
            itemchange(op);
        }
        function checkEnterCode(op) {
            if (event.keyCode == 13) {
                return false;
            }
        }
        function itemchange(op) {
            var id = $(op).attr("id");
            var preid = id.substring(0, id.lastIndexOf('_') + 1);
            projectCostItemBudget(preid);
        }
        function getElementPreId(id) {
            var preid = id.substring(0, id.lastIndexOf('_') + 1);
            return preid;
        }
        function Load_Do() {
            readonlyinput();
        }
        function readonlyinput() {
            var list = $("#GVData .readonly_input");
            for (var i = 0; i < list.length; i++) {
                var item = list[i];
                var id = $(item).attr("id");
                var val = $(item).val();
                document.getElementById(id).readOnly = true;
                if ($(item).hasClass("mp_input") && val != '') {
                    document.getElementById(id).value = regexNum(val);
                }
            }
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
            if (event.keyCode == 27) {
                CCC();
                return false;
            }
        }
        //function SetFocusForTab(rowid) {
        //    var activeEl = document.activeElement;
        //    if (typeof ($("#GVData[data-row=" + rowid + "]").html()) != 'undefined') {
        //        if ($(".AddDetail[data-row=" + rowid + "]").hasClass("hidden")) {
        //            SetFocusForTab(Number(rowid) + 1);
        //        } else {
        //            $(".AddDetail[data-row=" + rowid + "]").children(":eq(1)").children("input[type='text']").focus();
        //        }
        //    }
        //}
    </script>
</head>
<body class="lui_form_body " style="margin-top: 43px; margin-bottom: 46px;">
    <form id="form1" runat="server">
        <div style="display: none">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        </div>
        <div>
            <div id="toolbar" data-lui-type="lui/toolbar!ToolBar" style="" class="lui-component" data-lui-cid="toolbar" data-lui-parse-init="1">
                <div>
                    <iframe frameborder="0" class="lui_toolbar_frame_float_mark" scrolling="no"></iframe>
                    <div class="lui_toolbar_frame_float">
                        <div class="lui_toolbar_left">
                            <div class="lui_toolbar_right">
                                <div class="lui_toolbar_content" style="max-width: 100%;">
                                    <table style="margin: 0px auto; position: relative;">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <div class="lui_form_subject">
                                                        <span id="RebuildForm_lblFormTitle">成本报销<%=Action=="Edit"?"编辑":(Action=="View"?"预览":"") %></span>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table style="margin: 0px auto;">
                                        <tbody>
                                            <tr>
                                                <td lui-button-container="1">
                                                    <div class="lui_toolbar_btn" data-lui-on-class="lui_toolbar_btn_on" data-lui-status-class="lui_toolbar_btn_toggle_on">
                                                        <div id="passedBtn" data-lui-type="lui/toolbar!Button" data-lui-parentid="toolbar" style="" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="bookedBtn" data-lui-parse-init="7" title="暂存" tabindex="0" onclick="javascript:document.getElementById('btnSubmit').click();">
                                                            <div class="lui_toolbar_btn_l" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                                                <div class="lui_toolbar_btn_r">
                                                                    <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                        <div id="lui-id-31" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-31">暂存</div>
                                                                        <asp:Button ID="btnSubmit" runat="server" Text="暂存" OnClick="btnSubmit_Click" Style="display: none;" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td lui-button-container="2">
                                                    <div class="lui_toolbar_btn" data-lui-on-class="lui_toolbar_btn_on" data-lui-status-class="lui_toolbar_btn_toggle_on">
                                                        <div id="backBtn" data-lui-type="lui/toolbar!Button" style="" class="lui-component lui_widget_btn lui_toolbar_btn_on" data-lui-cid="backBtn" data-lui-parse-init="3" title="返回" tabindex="0" onclick="javascript:CCC();">
                                                            <div class="lui_toolbar_btn_l" data-lui-mark="toolbar_button_inner" style="text-align: center;">
                                                                <div class="lui_toolbar_btn_r">
                                                                    <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                        <div id="lui-id-26" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-26">返回</div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="lui_form_path_frame" style="width: 98%; min-width: 980px; max-width: 100%; margin: 0px auto;">

                <div data-lui-type="lui/menu!Menu" style="" id="lui-id-7" class="lui-component" data-lui-cid="lui-id-7" data-lui-parse-init="9">
                    <div class="lui_menu_frame_nav">
                        <div class="lui_menu_left">
                            <div class="lui_menu_right">
                                <div class="lui_menu_content">
                                    <div class="lui_menu_item_split"></div>
                                    <div class="lui_menu_item">
                                        <div data-lui-type="lui/menu!MenuItem" style="cursor: pointer;" id="lui-id-9" class="lui-component lui_item" data-lui-cid="lui-id-9" data-lui-parse-init="11" data-lui-switch-class="lui_item_stitch_class">

                                            <div class="lui_item_left">
                                                <div class="lui_item_right">
                                                    <div class="lui_item_content">
                                                        <div class="lui_icon_s lui_icon_s_home"></div>
                                                        <div class="lui_item_txt" title="桌面">桌面</div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="lui_menu_item_split"></div>
                                    <div class="lui_menu_item">
                                        <div data-lui-type="lui/menu!MenuItem" style="cursor: pointer;" id="lui-id-10" class="lui-component lui_item" data-lui-cid="lui-id-10" data-lui-parse-init="12" data-lui-switch-class="lui_item_stitch_class">

                                            <div class="lui_item_left">
                                                <div class="lui_item_right">
                                                    <div class="lui_item_content">
                                                        <div class="lui_item_txt" title="成本报销管理" onclick="javascript:void(0);">成本报销</div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="lui_menu_item_split"></div>
                                    <div class="lui_menu_item">
                                        <div data-lui-type="lui/menu!MenuItem" style="cursor: pointer;" id="lui-id-10" class="lui-component lui_item" data-lui-cid="lui-id-10" data-lui-parse-init="12" data-lui-switch-class="lui_item_stitch_class">

                                            <div class="lui_item_left">
                                                <div class="lui_item_right">
                                                    <div class="lui_item_content">
                                                        <div class="lui_item_txt" title="成本报销管理" onclick="javascript:void(0);">工作量录入</div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div id="lui_validate_message" style="width: 98%; min-width: 980px; max-width: 100%; margin: 0px auto;"></div>
            <table class="tempTB" style="width: 98%; min-width: 980px; max-width: 100%; margin: 0px auto;">
                <tbody>
                    <tr>
                        <!-- “左侧” 表单内容展示区TD单元格 -->
                        <td valign="top" class="lui_form_content_td">
                            <div class="lui_form_content">
                                <div data-lui-type="lui/panel!TabPage" style="" id="lui-id-11" class="lui-component" data-lui-cid="lui-id-11" data-lui-parse-init="13">
                                    <div class="lui_tabpage_frame">
                                        <div class="lui_tabpage_float_contents">
                                            <div class="lui_tabpage_float_content" style="">
                                                <div class="lui_tabpage_float_header_l">
                                                    <div class="lui_tabpage_float_header_r">
                                                        <div class="lui_tabpage_float_header_c">
                                                            <div class="lui_tabpage_float_header_title">
                                                                <div class="lui_tabpage_float_header_text">基本信息</div>
                                                                <div class="lui_tabpage_float_header_close" title="最小化"></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="lui_tabpage_float_content_l">
                                                    <div class="lui_tabpage_float_content_r">
                                                        <div class="lui_tabpage_float_content_c">
                                                            <%--基本信息开始--%>
                                                            <div>
                                                                <div data-lui-mark="panel.content.inside" class="lui_panel_content_inside">
                                                                    <div data-lui-type="lui/panel!Content" style="" id="lui-id-12" class="lui-component" data-lui-cid="lui-id-12" data-lui-parse-init="12">
                                                                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                            <ContentTemplate>--%>
                                                                        <table class="tb_normal" width="100%">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td colspan="4" style="text-align: center;">
                                                                                        <asp:Label ID="XMName" runat="server" Style="color: #2f84fb; font-size: 16px; font-weight: normal;"></asp:Label><br />
                                                                                        <label style="color: #2f84fb; font-size: 16px; font-weight: normal;">分包费用报销工作量</label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4">
                                                                                        <%--<fieldset>
                                                                                            <legend>
                                                                                                <label style="color: #2f84fb; font-size: 16px; font-weight: normal;">待选择的项目</label></legend>--%>
                                                                                        <div>
                                                                                            <table style="width: 100%;">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <div style="height: 450px; overflow-y: scroll;">
                                                                                                            <asp:GridView ID="GVData" runat="server" ShowFooter="true" AutoGenerateColumns="False"
                                                                                                                Width="100%" EnableModelValidation="True" CssClass="tb_normal"
                                                                                                                OnRowDataBound="GVData_RowDataBound" OnRowCreated="GVData_RowCreated">
                                                                                                                <PagerSettings Visible="False" />
                                                                                                                <HeaderStyle Font-Size="12px" Height="20px" HorizontalAlign="Center" />
                                                                                                                <Columns>
                                                                                                                    <asp:BoundField DataField="RowNumber" HeaderText="序号" ItemStyle-Width="30px" />
                                                                                                                    <%--0--%>
                                                                                                                    <asp:TemplateField HeaderText="类别"><%--1--%>
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:HiddenField ID="ID" runat="server" Value='<%# Bind("ID") %>' />
                                                                                                                            <asp:HiddenField ID="ParentId" runat="server" Value='<%# Bind("ParentId") %>' />
                                                                                                                            <asp:DropDownList ID="ItemType" runat="server" AppendDataBoundItems="true" CssClass="inputsgl" AutoPostBack="true" OnSelectedIndexChanged="Item_SelectedIndexChanged" Width="100%">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </ItemTemplate>
                                                                                                                        <ItemStyle Width="70px" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="分类" ItemStyle-CssClass="Item"><%--2--%>
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:DropDownList ID="ItemId" runat="server" AppendDataBoundItems="true" CssClass="inputsgl" AutoPostBack="true" OnSelectedIndexChanged="ItemId_SelectedIndexChanged" Width="100%">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </ItemTemplate>
                                                                                                                        <ItemStyle Width="150px" />
                                                                                                                        <FooterStyle HorizontalAlign="Center" />
                                                                                                                        <FooterTemplate>
                                                                                                                            <asp:LinkButton ID="btnChange" runat="server" OnClick="btnChange_Click" CssClass="TextChangedEvent" Style="display: none;"></asp:LinkButton>
                                                                                                                            合计
                                                                                                                        </FooterTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="单位*"><%--3--%>
                                                                                                                        <ItemTemplate>
                                                                                                                            <editable:EditableDropDownList ID="Unit" runat="server" Text='<%# Bind("Unit") %>' CssClass="inputsgl" Width="70%"></editable:EditableDropDownList>
                                                                                                                        </ItemTemplate>
                                                                                                                        <ItemStyle Width="70px" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="数量"><%--4--%>
                                                                                                                        <ItemTemplate>
                                                                                                                            <div class="inputselectsgl" onclick="" style="width: 100%;">
                                                                                                                                <div class="input">
                                                                                                                                    <asp:TextBox ID="Quantity" runat="server" Style="text-align: right;" Text='<%# Bind("Quantity") %>' onBlur="overFormatEx(this)" CssClass="mf_input mp_input"></asp:TextBox>
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                        </ItemTemplate>
                                                                                                                        <ItemStyle Width="90px" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="单价"><%--5--%>
                                                                                                                        <ItemTemplate>
                                                                                                                            <div class="inputselectsgl" onclick="" style="width: 100%;">
                                                                                                                                <div class="input">
                                                                                                                                    <asp:TextBox ID="Price" runat="server" Style="text-align: right;" Text='<%# Bind("Price") %>' onBlur="overFormatEx(this)" CssClass="mf_input mp_input"></asp:TextBox>
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                        </ItemTemplate>
                                                                                                                        <ItemStyle Width="90px" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="合计计算比例(%)"><%--6--%>
                                                                                                                        <ItemTemplate>
                                                                                                                            <div class="inputselectsgl" onclick="" style="width: 100%;">
                                                                                                                                <div class="input">
                                                                                                                                    <asp:TextBox ID="CalcPercent" runat="server" Style="text-align: right;" Text='<%# Bind("CalcPercent") %>' onBlur="overFormatEx(this)" CssClass="mf_input mp_input"></asp:TextBox>
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                        </ItemTemplate>
                                                                                                                        <ItemStyle Width="90px" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="合计"><%--7--%>
                                                                                                                        <ItemTemplate>
                                                                                                                            <div class="inputselectsgl" onclick="" style="width: 100%;">
                                                                                                                                <div class="input">
                                                                                                                                    <asp:TextBox ID="Amount" runat="server" Style="text-align: right; color: #1b83d8;" Text='<%# Bind("Amount") %>' onchange="overFormat(this)" CssClass="mf_input mp_input readonly_input"></asp:TextBox>
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                        </ItemTemplate>
                                                                                                                        <ItemStyle Width="90px" />
                                                                                                                        <FooterStyle HorizontalAlign="Right" />
                                                                                                                        <FooterTemplate>
                                                                                                                            <asp:Label ID="lblAmount" runat="server">0.00</asp:Label>
                                                                                                                        </FooterTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="分包公司名称*"><%--8--%>
                                                                                                                        <ItemTemplate>
                                                                                                                            <div class="inputselectsgl" onclick="" style="width: 100%;">
                                                                                                                                <div class="input">
                                                                                                                                    <asp:TextBox ID="Supplier" runat="server" Text='<%# Bind("Supplier") %>' CssClass="SupplierAutoComplete" ToolTip="如多个使用逗号(,)分隔"></asp:TextBox>
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                        </ItemTemplate>
                                                                                                                        <%--<ItemStyle Width="200px" />--%>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField><%--9--%>
                                                                                                                        <HeaderTemplate>
                                                                                                                            操作
                                                                                                                        </HeaderTemplate>
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:LinkButton ID="btnCopy" runat="server" OnClick="btnCopy_Click" Style="color: dodgerblue; float: left; margin-right: 3px;" CssClass="btnView">复制</asp:LinkButton>&nbsp;
                                                                                                                            <asp:LinkButton ID="btnDel" runat="server" OnClick="btnDel_Click" Style="color: dodgerblue;" CssClass="btnView">删除</asp:LinkButton>
                                                                                                                        </ItemTemplate>
                                                                                                                        <ItemStyle Width="80px" />
                                                                                                                        <FooterStyle HorizontalAlign="Center" />
                                                                                                                        <FooterTemplate>
                                                                                                                            <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click" Style="color: dodgerblue; float: left;" CssClass="btnView">添加</asp:LinkButton>
                                                                                                                        </FooterTemplate>
                                                                                                                    </asp:TemplateField>
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
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                        <%--</fieldset>--%>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <%--</ContentTemplate>
                                                                        </asp:UpdatePanel>--%>
                                                                    </div>
                                                                </div>
                                                                <div data-lui-mark="panel.content.operation" class="lui_portlet_operations clearfloat"></div>
                                                            </div>
                                                            <%--基本信息结束 --%>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="lui_tabpage_float_footer_l">
                                                    <div class="lui_tabpage_float_footer_r">
                                                        <div class="lui_tabpage_float_footer_c"></div>
                                                    </div>
                                                </div>
                                            </div>
                                            <iframe frameborder="0" class="lui_tabpage_float_navs_mark" scrolling="no"></iframe>
                                            <div class="lui_tabpage_float_navs">
                                                <div class="lui_tabpage_float_navs_l">
                                                    <div class="lui_tabpage_float_navs_r">
                                                        <div class="lui_tabpage_float_navs_c" style="max-width: 100%; text-align: center; padding-top: 5px; padding-bottom: 5px; height: 40px;">
                                                            <div id="lui-id-175" class="lui-component lui_widget_btn lui_toolbar_btn_def" data-lui-cid="lui-id-170" title="暂存" onclick="javascript:document.getElementById('btnSubmit').click();">
                                                                <div class="lui_toolbar_btn_l lui_toolbar_m" data-lui-mark="toolbar_button_inner" style="text-align: center; height: 33px; line-height: 33px;">
                                                                    <div class="lui_toolbar_btn_r">
                                                                        <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                            <div id="lui-id-176" class="lui-component lui_widget_btn_txt" data-lui-cid="lui-id-171">暂存</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="lui-id-183" class="lui-component lui_widget_btn lui_toolbar_btn_def" data-lui-cid="lui-id-170" title="返回" onclick="javascript:CCC();">
                                                                <div class="lui_toolbar_btn_l lui_toolbar_m" data-lui-mark="toolbar_button_inner" style="text-align: center; height: 33px; line-height: 33px;">
                                                                    <div class="lui_toolbar_btn_r">
                                                                        <div class="lui_toolbar_btn_c" data-lui-mark="toolbar_button_content">
                                                                            <div id="lui-id-184" class="lui-component lui_widget_btn_txt btnGoback" data-lui-cid="lui-id-171">返回</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--<div class="lui_tabpage_float_collapse" title="收起"><a class="txt">收起</a></div>--%>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </td>
                        <!-- “右侧” 侧边栏展示区TD单元格 -->
                        <td valign="top" style="width: 30%; display: none;" class="lui_form_sidebar_td">
                            <div style="padding-left: 15px;" class="lui_form_sidebar">
                            </div>
                        </td>

                    </tr>
                </tbody>
            </table>
            <div id="top" data-lui-type="lui/top!totop" style="" class="lui-component com_goto" data-lui-cid="top" data-lui-parse-init="29">
                <div class="com_gototop" style="display: none;"></div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var instance = Sys.WebForms.PageRequestManager.getInstance();
        if (instance) {
            instance.add_initializeRequest(showLoading);
            instance.add_endRequest(function () {
                initSelectDateInput();
                Load_Do();
                var action = getUrlParms("Action");
                if (action == 'View') {
                    setViewInput();
                }
            });
            instance.add_pageLoading(hideLoading);
        }
    </script>
</body>
</html>
