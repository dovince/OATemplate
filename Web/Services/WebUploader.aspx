<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebUploader.aspx.cs" Inherits="Services_WebUploader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        var maxsize = 30 * 1024 * 1024;//2M 
        var errMsg = "上传的文件不能超过30M！！！";
        var tipMsg = "您的浏览器暂不支持计算上传文件的大小，确保上传文件不要超过30M，建议使用IE、FireFox、Chrome浏览器。";
        var browserCfg = {};
        var ua = window.navigator.userAgent;
        if (ua.indexOf("MSIE") >= 1 || ua.indexOf("Gecko") >= 1) {
            browserCfg.ie = true;
        } else if (ua.indexOf("Firefox") >= 1) {
            browserCfg.firefox = true;
        } else if (ua.indexOf("Chrome") >= 1) {
            browserCfg.chrome = true;
        }

        function selectAndUpload() {
            var theFileUpload = document.getElementById("<%=FileUploadInput.ClientID%>");
            theFileUpload.onchange = function () {
                if (fileChange(theFileUpload)) {
                    var myForm = document.getElementById("<%=form1.ClientID%>");
                    myForm.submit();
                }
            }
            theFileUpload.click();
        }

        function callback(data) {
            parent.callback(data);
        }
        function fileChange(target) {
            try {
                if (target.value == "") {
                    alert("请先选择上传文件");
                    return false;
                }
                var filesize = 0;
                if (browserCfg.firefox || browserCfg.chrome) {
                    filesize = target.files[0].size;
                } else if (browserCfg.ie) {
                    if (!target.files) {
                        var filePath = target.value;
                        try {
                            var fileSystem = new ActiveXObject("Scripting.FileSystemObject");
                            var file = fileSystem.GetFile(filePath);
                            filesize = file.Size;
                        } catch (e){
                            return true;
                        }
                    } else {
                        filesize = target.files[0].size;
                    }
                } else {
                    alert(tipMsg);
                    return false;
                }
                if (filesize == -1) {
                    //alert(tipMsg);
                    return true;
                } else if (filesize > maxsize) {
                    //alert(errMsg);
                    return true;
                } else {
                    return true;
                }
            } catch (e) {
                alert(e);
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:FileUpload runat="server" ID="FileUploadInput" Style="width: 350px;"></asp:FileUpload>
        </div>
        <div id="upload_list_tr_edit_block" runat="server" style="visibility: hidden;">
            <div id="WU_FILE_0" class="upload_list_tr upload_list_tr_edit upload_list_tr_edit_block">
                <div class="upload_list_tr_edit_l" style="width: 776px;">
                    <div title="序号"><i class="upload_list_ordernumber">1,</i></div>
                    <div class="upload_list_filename_edit" title="{0}" style="width: 652px;">
                        <span class="upload_list_filename_title" data-nowname="{4}" data-oldname="{0}" style="max-width: 613px;">{0}</span>
                    </div>
                    <div class="upload_list_size" style="top:1px;" title="文件大小">{1}</div>
                </div>
                <div class="upload_list_tr_edit_r" style="width: 180px;">
                    <div class="upload_list_progress_img upload_item_hide">
                        <div class="upload_progress_border">
                            <div class="upload_progress_val" style="width: {2}%;"></div>
                        </div>
                    </div>
                    <div class="upload_list_progress_text" title="进度">
                        <div class="upload_progress_text" style="font-size:14px;">{2}%</div>
                    </div>
                    <div class="upload_list_status" title="状态">
                        <div class="upload_opt_status success">
                            <i></i>{3}
                        </div>
                    </div>
                    <div class="upload_list_operation" style="min-width: 0px;">
                        <div>
                            <div class="upload_opt_icon upload_opt_delete" title="删除" onclick="javascript:deleteUploadFile(this);">
                                <span class="upload_opt_tip">
                                    <i class="upload_opt_tip_arrow"></i>
                                    <i class="upload_opt_tip_inner">删除</i>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="upload_list_tr_view_block" runat="server" style="visibility: hidden;">
            <div id="WU_FILE_1" class="upload_list_tr upload_list_tr_view upload_list_tr_view_block">
                <div class="upload_list_tr_view_l" style="cursor: pointer; width: 776px;">
                    <div title="序号"><i class="upload_list_ordernumber">1,</i></div>
                    <div class="upload_list_filename_view" title="{0}" style="width: 637px;">
                        <span class="upload_list_filename_title" data-nowname="{4}" data-oldname="{0}" style="max-width: 584px;">{0}</span>
                    </div>
                    <div class="upload_list_size" style="top:1px;">{1}</div>
                </div>
                <div class="upload_list_tr_view_r" style="width: 180px;">
                    <div class="upload_list_operation">
                        <div class="upload_opt_icon upload_opt_view" data-link="{2}" title="阅读" onclick="javascript:window.open('{2}');">
                            <span class="upload_opt_tip">
                                <i class="upload_opt_tip_arrow"></i>
                                <i class="upload_opt_tip_inner">阅读</i>
                            </span>
                        </div>
                        <div class="upload_opt_icon upload_opt_down" data-link="{3}" title="下载" onclick="javascript:window.open('{3}');">
                            <span class="upload_opt_tip">
                                <i class="upload_opt_tip_arrow"></i>
                                <i class="upload_opt_tip_inner">下载</i>
                            </span>
                        </div>
                        <div class="upload_opt_icon upload_opt_delete" title="删除" onclick="javascript:deleteUploadFile(this);">
                            <span class="upload_opt_tip">
                                <i class="upload_opt_tip_arrow"></i>
                                <i class="upload_opt_tip_inner">删除</i>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
