<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadFile.ascx.cs" Inherits="UserControls_UploadFile" %>
<%@ Import Namespace="System.Linq" %>
<link href="../Style/Style.css" rel="stylesheet" />
<link href="../Style/Style1.css" rel="stylesheet" />
<style type="text/css">
    .MainStyle {
        text-align: right;
        font: 14px "微软雅黑";
        background-color: #f6f6f6;
        height: 30px;
        width: 13%;
    }

    .TitleStyle {
        padding-left: 20px;
        line-height: 20px;
        background-color: #f6f6f6;
        background: url(../CSS/common/images/menu/icon-tabpage-title.png) no-repeat 10% 5px;
        text-align: center;
        font: 16px "宋体";
    }

        .TitleStyle span {
            color: #2f84fb;
        }

        .TitleStyle strong {
            color: #2f84fb;
        }

    .attachmentsTD {
        padding-right: 50px;
    }
</style>
<table class="tb_normal" style="width: 100%">
    <tr>
        <td class="MainStyle"></td>
        <td colspan="3" style="padding-left: 5px; height: 25px; background-color: #ffffff">
            <input id="HiddenField_Attachments" name="HiddenField_Attachments" type="hidden" value="<%=Attachments %>" />
            <input id="HiddenField_CanDel" name="HiddenField_CanDel" type="hidden" value="<%=CanDel %>" />
            <input id="HiddenField_CanView" name="HiddenField_CanView" type="hidden" value="<%=CanView %>" />
            <input id="HiddenField_CanEdit" name="HiddenField_CanEdit" type="hidden" value="<%=CanEdit %>" />
            <table id="ckbAttachments" class="attachmentsTable" border="0">
                <tbody>
                    <%if (_list.Any())%>
                    <%{
                            var i = 0;
                            foreach (var item in _list)
                            { %>
                    <tr class="attachmentsTR">
                        <td class="attachmentsTD">
                            <input id="ckbAttachments_<%=i %>" class='ckbAttachments <%=(CanDel?"":"hidden") %>' checked="checked" type="checkbox" value="<%=item.NowName %>" name="ckbAttachments$<%=i %>">
                            <label for="ckbAttachments_<%=i %>">
                                <a href="../UploadFile/<%=item.NowName %>" target="_blank"><%=item.OldName %></a>
                            </label>
                        </td>
                        <td>
                            <%if (CanView && ZWL.Common.PublicMethod.CheckFileAllowed(item.NowName)) %>
                            <%{ %>
                            <a id="readBtn<%=i %>" href="../FlexPaperFlash/SWFShow.aspx?f=<%=item.NowName %>&n=<%=item.OldName %>" target="_blank" class="btn-xs btn-info">阅读文件</a>
                            <%} %>
                            <%if (CanEdit && ZWL.Common.PublicMethod.CheckFileAllowed(item.NowName)) %>
                            <%{ %>
                            <a id="editBtn<%=i %>" href="../DsoFramer/EditFile.aspx?FilePath=<%=item.NowName %>" target="_blank" class="btn-xs btn-info">编辑文件</a>
                            <%} %>
                        </td>
                    </tr>
                    <%i++;
                            }
                        }
                        else
                        { %>
                    <tr class="attachmentsTR hidden">
                        <td class="attachmentsTD">
                            <input id="ckbAttachments_0" class='ckbAttachments <%=(CanDel?"":"hidden") %>' checked="checked" type="checkbox" value="" name="ckbAttachments$0">
                            <label for="ckbAttachments_0">
                                <a href="javascript(0);" target="_blank"></a>
                            </label>
                        </td>
                        <td>
                            <a id="readBtn0" href="javascript(0);" target="_blank" class='btn-xs btn-info <%=(CanDel?"":"hidden") %>'>阅读文件</a>
                            <a id="editBtn0" href="javascript(0);" target="_blank" class='btn-xs btn-info <%=(CanDel?"":"hidden") %>'>编辑文件</a>
                        </td>
                    </tr>
                    <%} %>
                </tbody>
            </table>
        </td>
    </tr>
    <tr>
        <td class="MainStyle">上传附件：&nbsp;&nbsp;
        </td>
        <td colspan="3" style="padding-left: 5px; height: 25px; background-color: #ffffff;">
            <div>
                <input type="file" id="fileInput" name="fileInput" onchange="submitFile();"><span style="color:#FF0000">(请上传表单相关的附件)</span>
            </div>
        </td>
    </tr>
</table>
<script src="../JS/jquery-1.11.2.min.js" type="text/javascript"></script>
<script src="../JS/common.js?v=202306020823" type="text/javascript"></script>
<script type="text/javascript">
    function ckbAttachments() {
        var attachments = $("#HiddenField_Attachments");
        var list = $(".ckbAttachments");
        if (list.length > 0) {
            for (var i = 0; i < list.length; i++) {
                var item = list.eq(i);
                var val = item.val();
                if (item.is(":checked")) {
                    if (attachments.val().indexOf(val) < 0) {
                        attachments.val(attachments.val() + "|" + val);
                    }
                }
                else {
                    if (attachments.val().indexOf(val) > 0) {
                        attachments.val(attachments.val().split("|" + val).join(''));
                    }
                }
            }
        }
    }
    $(function () {
        $(document).on("click", ".ckbAttachments", function () {
            ckbAttachments();
        });
    });

    function submitFile() {
        if (this.value == '') {

        } else {
            var opt = {
                id: "fileInput",
                frameName: "fileUpload",
                url: "../Services/Services.ashx?f=FileUpload",
                callBack: function (data) {
                    if (data != "" && data != null) {
                        var result = eval('(' + data + ')');
                        if (result.Code == true || result.Code == "true") {
                            auccessAdd(result);
                        }
                        else {
                            alert(result.Message);
                        }
                    }
                }
            };
            ajaxUpload(opt);
        }
    }
    function ajaxUpload(opt) {
        /*
            参数说明:
            opt.id : 页面里file控件的ID;
            opt.frameName : iframe的name值;
            opt.url : 文件要提交到的地址;
            opt.callBack : 上传成功后回调;
        */
        var iName = opt.frameName; //太长了，变短点
        var iframe, form, file, fileParent;
        //创建iframe和form表单
        iframe = $('<iframe style="width:0;height:0;" name="' + iName + '" />');
        form = $('<form method="post" style="display:none;" target="' + iName + '" action="' + opt.url + '"  name="form_' + iName + '" enctype="multipart/form-data" />');
        file = $('#' + opt.id); //通过id获取flie控件
        fileParent = file.parent(); //存父级
        file.appendTo(form);
        //插入body
        $(document.body).append(iframe).append(form);
        //取得所选文件的扩展名
        form.submit();//格式通过验证后提交表单;

        //文件提交完后
        iframe.load(function () {
            var data = $(this).contents().find('body').html();
            file.appendTo(fileParent);
            iframe.remove();
            form.remove();
            opt.callBack(data);
        })
    }
    function auccessAdd(data) {
        var item = $(".attachmentsTable tr:last");
        if (!item.hasClass("hidden")) {
            $(".attachmentsTable tbody").append(item.prop("outerHTML"));
            item = $(".attachmentsTable tr:last");
            item.addClass("hidden");
        }
        var attachments = $("#HiddenField_Attachments");
        var canDel = $("#HiddenField_CanDel").val();
        var canView = $("#HiddenField_CanView").val();
        var canEdit = $("#HiddenField_CanEdit").val();
        var index = item.index();
        var input = item.find("td input");
        var label = item.find("td label");
        var alink = item.find("td label a");
        var fileUrl = "../UploadFile/" + data.Data.NowName;
        var readUrl = "../FlexPaperFlash/SWFShow.aspx?f=" + data.Data.NowName + "&n=" + data.Data.OldName;
        var editUrl = "../DsoFramer/EditFile.aspx?FilePath=" + data.Data.NowName;
        var readbtn = item.find("td:last a[id^='readBtn']");
        var editbtn = item.find("td:last a[id^='editBtn']");
        input.attr("id", "ckbAttachments_" + index);
        input.attr("name", "ckbAttachments$" + index);
        input.val(data.Data.NowName);
        label.attr("for", "ckbAttachments_" + index);
        readbtn.attr("id", "readBtn" + index);
        readbtn.attr("href", readUrl);
        editbtn.attr("id", "editBtn" + index);
        editbtn.attr("href", editUrl);
        if (canDel != '' && canDel == 'false') {
            input.addClass('hidden');
        }
        else {
            input.removeClass('hidden');
        }

        if ((data.Data.ID == '0' || data.Data.ID == 0) && canView != '' && canView == 'false') {
            readbtn.addClass("hidden");
        }
        else {
            readbtn.removeClass("hidden");
        }
        if ((data.Data.ID == '0' || data.Data.ID == 0) && canEdit != '' && canEdit == 'false') {
            editbtn.addClass("hidden");
        }
        else {
            editbtn.removeClass("hidden");
        }
        alink.text(data.Data.OldName);
        alink.attr("href", fileUrl);
        if (item.hasClass("hidden")) {
            item.removeClass("hidden");
        }
        if (attachments.val().indexOf(data.Data.NowName) < 0) {
            attachments.val(attachments.val() + "|" + data.Data.NowName);
        }
    }
</script>
