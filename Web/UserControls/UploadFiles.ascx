<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadFiles.ascx.cs" Inherits="UserControls_UploadFiles" %>
<%@ Import Namespace="System.Linq" %>
<div id="attachmentObject_attprojectMain_content_div" class="lui_upload_img_box">
    <input id="HiddenField_Attachments" name="HiddenField_Attachments" type="hidden" value="<%=Attachments %>" />
    <table id="uploader_attprojectMain" class="tb_noborder" width="100%" border="0" cellspacing="0" cellpadding="0">
        <tbody>
            <tr class="uploader">
                <td>
                    <div class="webuploader-pick">
                        <i></i>上传文件，请<span id="upload_attprojectMain_div_buttom" class="lui_text_primary">选择文件</span>
                        <small style="font-size: 80%; color: grey;">（不超过30M）</small>
                        <div>
                            <input type="file" id="fileInput" name="fileInput" onchange="submitFile();">
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td data-lui-mark="attachmentlist">
                    <div id="att_xtable_attprojectMain" runat="server" class="ui-sortable">
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
    <script type="text/javascript">
        $(function () {
            initAttachmentlist();
        });
        var authData = { "CanDel": <%=CanDel.ToString().ToLower()%>, "CanView": <%=CanView.ToString().ToLower()%>, "CanEdit": <%=CanEdit.ToString().ToLower()%>};

        function submitFile() {
            if (this.value == '') {

            } else {
                var opt = {
                    id: "fileInput",
                    frameName: "UploadFile",
                    url: "../Services/Services.ashx?f=UploadFile",
                    callBack: function (data) {
                        if (data != "" && data != null) {
                            var result = eval('(' + data + ')');
                            if (result.Code == true || result.Code == "true") {
                                callbackfunc(result.Data);
                                if (typeof (callbackFuncEx) != "undefined") {
                                    callbackFuncEx(result.Data);
                                }
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
                //alert("form.submit:" + ";id+" + JSON.stringify(data));
                //alert("form.submit:" + ";id+" + JSON.stringify($(iframe).prop("outerHTML")));
                file.appendTo(fileParent);
                iframe.remove();
                form.remove();
                opt.callBack(data);
                var obj = document.getElementById(opt.id);
                obj.outerHTML = obj.outerHTML;
            });
        }

        function initAttachmentlist() {
            $("#HiddenField_Attachments").val("");
            var list = $("#UploadFiles_att_xtable_attprojectMain.ui-sortable .upload_list_tr");
            $.each(list, function (i, item) {
                var rowIndex = (i + 1);
                $(item).attr("id", "WU_FILE_" + rowIndex);
                $(item).find(".upload_list_ordernumber").text(rowIndex + ",");
                var nowName = $(item).find(".upload_list_filename_title").attr("data-nowname");
                $("#HiddenField_Attachments").val($("#HiddenField_Attachments").val() + "|" + nowName);
            });
        }

        function callbackfunc(data) {
            //alert(JSON.stringify(data));
            if (data != null && data.length > 0) {
                $.each(data, function (i, item) {
                    $("#UploadFiles_att_xtable_attprojectMain.ui-sortable").append(item.showHtml);
                });
            }
            initAttachmentlist();
        }
        function deleteUploadFile(item) {
            $(item).parents(".upload_list_tr").remove();
            initAttachmentlist();
        }

        var callbackFuncEx;
        <%if (!string.IsNullOrEmpty(CallBackFunc))%>
        <%{ %>
        callbackFuncEx = <%=CallBackFunc%>
        <%}%>
    </script>
</div>
