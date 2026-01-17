<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreviewQLCHelper.aspx.cs" Inherits="CommonSelect_PreviewQLCHelper" %>

<%@ Import Namespace="ZWL.Common" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge; charset=gb2312" />
    <link href="../JS/metronic/assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JS/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../CSS/calendar.js"></script>
    <script type="text/javascript" src="../JS/jquery.easyui.min.js"></script>
    <script src="../JS/common.js?v=202306020823" type="text/javascript"></script>
    <style>
        body, html, #mapcontainer {
            width: 100%;
            min-height: 100%;
            overflow: hidden;
            margin: 0;
            position: absolute;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#previewcontent").attr("width", ($("body").width() - 40));
            $("#previewcontent").attr("height", ($("body").height() - 40));

        });
        //iframe下载文件
        function downloadFileByIfr(url) {
            var link = document.createElement('a')
            link.style.display = 'none'
            link.href = url
            link.download = "项目费用成本报销统计表_" + dateFormat(new Date()).replace(/-/g, "");

            document.body.appendChild(link)
            link.click() // a标签自动触发点击事件
        }

        function dateFormat(date) {
            var year = date.getFullYear();                // 年
            var month = showTime(date.getMonth() + 1);        // 月
            var week = showTime(date.getDay());           // 星期
            var day = showTime(date.getDate());          // 日
            var hours = showTime(date.getHours());         // 小时
            var minutes = showTime(date.getMinutes());    // 分钟
            var second = showTime(date.getSeconds());     // 秒
            return year + '-' + month + '-' + week + '-' + day + '-' + hours + '-' + minutes + '-' + second

        }
        // 封装一个不够两位数就补零的函数
        function showTime(t) {
            var time
            time = t > 10 ? t : '0' + t
            return time
        }

        function printPdf(url) {
            var iframe = undefined;
            if (!iframe) {
                iframe = document.createElement('iframe');
                document.body.appendChild(iframe);

                iframe.style.display = 'none';
                iframe.onload = function () {
                    setTimeout(function () {
                        iframe.focus();
                        iframe.contentWindow.print();
                    }, 1);
                };
            }

            iframe.src = url;
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" style="margin: 0 auto; border: solid 1px #ccc; border-collapse: collapse; width: 95%;">
                <tbody>
                    <tr>
                        <td colspan="2" style="text-align: center; height: 40px;">
                            <%--<strong style="font-size: 16px;">
                                <strong>
                                    <a href="javascript:void(0)" class="fa fa-print" onclick="javascript:printPdf('<%="{0}/UploadFile/DocumentPreview/{1}.pdf".FormatWith(BaseUrl,Get("filename")) %>')">打印</a>
                                </strong>
                            </strong>
                            <strong style="font-size: 16px;">
                                <strong>
                                    <a href="javascript:void(0)" class="fa fa-download" onclick="javascript:downloadFileByIfr('<%="{0}/UploadFile/DocumentPreview/{1}.pdf".FormatWith(BaseUrl,Get("filename")) %>')">下载</a>
                                </strong>
                            </strong>--%>

                            <span class="fa fa-lg fa-reply" aria-hidden="true" style="cursor: pointer; color: yellowgreen;"
                                onclick="javascript:CCC();">
                                <span style="font-size: smaller; color: black;">返回</span>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">
                            <div>
                                <iframe id="previewcontent" scrolling='true' width='100%' height='100%' frameborder='0' src='<%="{0}/UploadFile/DocumentPreview/{1}.htm".FormatWith(BaseUrl, Get("filename")) %>'></iframe>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
