<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWFShowTest.aspx.cs" Inherits="FlexPaperFlash_SWFShow" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <link href="../CSS/Loading.css" rel="stylesheet" />

    <script src="../JS/FlexPaperFlash1.5.1/jquery.js"></script>
    <script src="../JS/FlexPaperFlash1.5.1/flexpaper_flash.js"></script>
    <script src="../JS/jquery.blockUI.js"></script>
    <script src="../JS/common.js?v=202306020823"></script>
    <style>
        body, form {
            padding: 0px;
            margin: 0px;
        }

        * {
            touch-action: pan-y;
        }

        form {
            margin-bottom: 0px;
        }
          /*第一次进入主界面的加载中*/
        .loading-wrap {
            position: fixed;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            background: #fff;
            opacity: 1;
            filter: Alpha(opacity=60);
            z-index: 9999;
            width: 100%;
            height: 100%;
        }

        .loading-content {
            position: absolute;
            width: 139px;
            height: 139px;
            background: url(../images/loading2.gif) center center no-repeat;
        }
    </style>

    <script>
        function getBrowserInfo() {
            var explorer = window.navigator.userAgent.toLowerCase();
            //ie 
            if (explorer.indexOf("msie") >= 0) {
                var ver = explorer.match(/msie ([\d.]+)/)[1];
                return { browser: "IE", version: ver };
            }
                //firefox 
            else if (explorer.indexOf("firefox") >= 0) {
                var ver = explorer.match(/firefox\/([\d.]+)/)[1];
                return { browser: "Firefox", version: ver };
            }
                //Chrome
            else if (explorer.indexOf("chrome") >= 0) {
                var ver = explorer.match(/chrome\/([\d.]+)/)[1];
                return { browser: "Chrome", version: ver };
            }
                //Opera
            else if (explorer.indexOf("opera") >= 0) {
                var ver = explorer.match(/opera.([\d.]+)/)[1];
                return { browser: "Opera", version: ver };
            }
                //Safari
            else if (explorer.indexOf("Safari") >= 0) {
                var ver = explorer.match(/version\/([\d.]+)/)[1];
                return { browser: "Safari", version: ver };
            }
        }

        function resizeWH() {
            var sys = getBrowserInfo();
            var width = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
            var height = window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight;
            try{
                if (sys.browser == "IE" && sys.version == "8.0") {
                    width = width - 8;
                    height = height - 8;
                }
            }catch(e){
            
            }
           
            $("#viewerPlaceHolder").css("width", document.body.clientWidth);
            $("#viewerPlaceHolder").css("height", height);
        }

        $(window).ready(function () {
            resizeWH();
            $("#loader_container").fadeOut();
        });

        $(window).resize(function () {
            resizeWH();
        });

    </script>
</head>
<body style="overflow-y:hidden;" >
    <form id="form1" runat="server">
        <a id="viewerPlaceHolder" style="display: block"></a>
        <script type="text/javascript">
            var fp = new FlexPaperViewer(
            '../JS/FlexPaperFlash1.5.1/FlexPaperViewer',    /* 对应FlexPaperViewer.swf文件*/
            'viewerPlaceHolder', {
                config: {
                    SwfFile: '<%=SwfFile%>',
                    Scale: 1.5,
                    ZoomTransition: 'easeOut',
                    ZoomTime: 0.5,
                    ZoomInterval: 0.2,
                    FitPageOnLoad: false,
                    FitWidthOnLoad: false,
                    FullScreenAsMaxWindow: false,
                    ProgressiveLoading: false,
                    MinZoomSize: 0.2,
                    MaxZoomSize: 5,
                    SearchMatchAll: false,
                    InitViewMode: 'Portrait',
                    ViewModeToolsVisible: true,
                    ZoomToolsVisible: true,
                    NavToolsVisible: true,
                    CursorToolsVisible: true,
                    SearchToolsVisible: true,
                    localeChain: 'zh_CN'
                }
            });
        </script>
    </form>
</body>
</html>
