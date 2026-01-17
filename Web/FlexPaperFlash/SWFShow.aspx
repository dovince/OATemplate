<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWFShow.aspx.cs" Inherits="FlexPaperFlash_SWFShow" ValidateRequest="false" %>

<%@ Import Namespace="System.Linq" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=System.Configuration.ConfigurationManager.AppSettings["SYSTitle"]%></title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <link href="../Style/Style1.css" rel="stylesheet" />
    <script src="../JS/jquery-1.11.2.min.js"></script>
    <script src="../JS/common.js?v=202306020823"></script>
    <style>
        html, body {
            height: 100%;
        }

        #div0 {
            width: 94%;
            height: calc(100% - 100px);
            position: relative;
            background: rgba(5, 105, 245, 0.11);
            border-radius: 2px;
            margin-left: auto;
            margin-right: auto;
        }

        #div1 {
            width: 100%;
            height: calc(100% - 100px);
        }

        #div2 {
            width: 100%;
            height: 500px;
            padding-right: 60px;
            overflow-y: scroll;
        }

        #div3 {
            width: 100%;
            height: calc(100% - 100px);
            padding: 5px;
            overflow-wrap: break-word;
        }

        #scroll-bar {
            position: absolute;
            top: 0;
            right: 0;
            width: 10px;
            border-radius: 2px;
            cursor: pointer;
            background-color: rgba(190, 180, 190, 0.50);
        }

            #scroll-bar:hover {
                background-color: rgba(175, 175, 175, 0.70);
            }

        #scroll-btn {
            position: absolute;
            right: 0;
            width: 10px;
            border-radius: 2px;
            background-color: rgba(130, 158, 175, 0.71);
            background: -webkit-gradient(linear, 0% 0%, 90% 0%,from(rgba(130, 158, 175, 0.91)), to(rgba(222, 235, 245, 0.91)));
            opacity: 0.8;
            cursor: pointer;
        }

            #scroll-btn:hover {
                opacity: 1;
            }
    </style>
    <script type="text/javascript">
        var timer = null;
        $(document).ready(function () {
            if (!isNaN($("body").height()))
                $("#div2").height($("body").height() - 100);
            timer = setInterval(function () {
                checkWaitingLoadImage();
            }, 1000);
        });
        function checkWaitingLoadImage() {
            var msg = "";
            var list = $("#div3 img[class='waitingLoadImage'][src*='loading-spinner-grey']")
            if (list.length > 0) {

                for (var i = 0; i < list.length; i++) {
                    var item = list[i];
                    var src = $(item).attr("src");
                    if (src.indexOf("loading-spinner-grey") > 0) {

                        var filename = $(item).attr("data-filename");
                        var idx = $(item).attr("data-idx");
                        var options = {
                            url: '../Services/Services.ashx',
                            data: { f: "GetPDF2OneImage", filename: filename, idx: idx },
                            dataType: "json",
                            timeout: 60 * 1000,
                            success: function (data) {
                                if (data.Code) {
                                    var id = "#div3 img[class='waitingLoadImage'][data-idx='" + data.Data.Index + "']";
                                    $(id).attr("src", "SWFFiles/" + filename + "/" + data.Data.Name);
                                    loadImg($(id)[0]);
                                    $(id).attr("class", "loaded");
                                }
                                else {
                                    if (msg == "" && data.Message != null && data.Message.length > 0) {
                                        msg = data.Message;
                                    }
                                }
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                console.log(XMLHttpRequest);
                            }
                        };

                        MakeRequestAjax(options);

                    }

                }
            }
            else {
                clearInterval(timer);
            }
            if (msg != "") {
                alert(msg);
            }
        }
    </script>
</head>
<body style="overflow: hidden;">
    <form id="form1" runat="server">
        <div style="text-align: center; width: 95vw; margin: auto;">
            <h2><%=!string.IsNullOrEmpty(Request["n"])?Request["n"]:Request["f"] %><span style="font-size: 14px;">
                [<a href="../DsoFramer/DownLoadFile.aspx?f=<%=Request["f"] %>&n=<%=Request["n"] %>" target="_blank">下载</a>]</span>
            </h2>
        </div>
        <div id="div0">
            <div id="div1">
                <div id="div2">
                    <div id="div3">
                        <%foreach (var item in SwfFile.OrderBy(r => r.Key))%>
                        <%{%>
                        <%if (item.Key == 0)%>
                        <%{%>
                        <%=item.Value %>
                        <%}%>
                        <%else%>
                        <%{%>
                        <%if (item.Value.Contains("UploadFile"))%>
                        <%{%>
                        <div>
                            <img src="<%=item.Value %>" style='max-width: 100%; width: auto;' />
                        </div>
                        <%}%>
                        <%else%>
                        <%{%>
                        <div title="第<%=item.Key %>页(<%=!string.IsNullOrEmpty(Request["n"])?Request["n"]:Request["f"] %>)">
                            <img src='../images/loading-spinner-grey.gif' style='max-width: 100%; width: auto' class="waitingLoadImage" data-filename="<%=item.Value %>" data-idx="<%=(item.Key-1) %>" />
                        </div>
                        <%}%>
                        <%}%>
                        <%}%>

                        <%if (!SwfFile.Any(r => r.Key == 0))%>
                        <%{%>
                        <script>
                            document.getElementById('div3').style.textAlign = 'center';
                        </script>
                        <%}%>
                    </div>
                </div>
            </div>
            <!--滚动条-->
            <div id="scroll-bar">
                <div id="scroll-btn"></div>
            </div>
        </div>

        <div id="outerdiv" style="position: fixed; top: 0; left: 0; background: rgba(0, 0, 0, 0.7); z-index: 2; width: 100%; height: 100%; display: none;">
            <img id="bigimg" src="" />
        </div>
    </form>

    <script>
        /**
         * 实现图片点击放大、拖拽、滚轴滚动焦点缩放功能，相关参数、函数声明
         */
        var imgWidth, imgHeight; // 图片点击放大初始尺寸参数
        var maxZoom = 4; //最大缩放倍数
        var minreduce = 0.5; // 最小缩放倍数
        var initScale = 1; //滚动缩放初始倍数，并不是图片点击放大的倍数
        var isPointerdown = false; //鼠标按下的标识
        //记录鼠标按下坐标和按下移动时坐标
        var lastPointermove = {
            x: 0,
            y: 0,
        };
        //移动过程从上一个坐标到下一个坐标之间的差值
        var diff = {
            x: 0,
            y: 0,
        };
        //图片放大后左上角的坐标，主要结合diff参数用于鼠标焦点缩放时图片偏移坐标
        var x = 0;
        var y = 0;

        // 记录节点
        var outerdiv = document.querySelector("#outerdiv");
        var image = outerdiv.querySelector("#bigimg");

        function loadImg(item) {
            if (item != null) {
                item.addEventListener("click", (e) => {
                    var that = e.target;
                    image.style.transform = "scale(1)";
                    //图片放大展示函数调用
                    imgShow(that);
                    // 监听鼠标滚动事件
                    window.addEventListener("wheel", handleStopWheel, {
                        passive: false,
                    });
                    // 拖转事件调用
                    imgDrag();
                });
            }
        };

        function imgShow(that) {
            var src = that.getAttribute("src");
            image.setAttribute("src", src);

            // 设置尺寸和调整比例
            var windowW = document.documentElement.clientWidth;
            var windowH = document.documentElement.clientHeight;
            var realWidth = image.naturalWidth; //获取图片的原始宽度
            var realHeight = image.naturalHeight; //获取图片的原始高度
            var outsideScale = 0.8;
            var belowScale = 1.4;
            var realRatio = realWidth / realHeight;
            var windowRatio = windowW / windowH;

            // 说明：下面是我自己的一些判断逻辑，大致意思就是图片的真实尺寸大于屏幕尺寸则使用屏幕尺寸，如果小于屏幕尺寸就使用自己本身的尺寸；并根据大于或者小于的比例对图片的尺寸进一步调整。coder可以根据自己的要求进行修改。
            if (realRatio >= windowRatio) {
                if (realWidth > windowW) {
                    imgWidth = windowH * outsideScale;
                    imgHeight = (imgWidth / realWidth) * realHeight;
                } else {
                    if (realWidth * belowScale < windowW) {
                        imgWidth = realWidth * (belowScale - 0.2);
                        imgHeight = (imgWidth / realWidth) * realHeight;
                    } else {
                        imgWidth = realWidth;
                        imgHeight = realHeight;
                    }
                }
            } else {
                if (realHeight > windowH) {
                    imgHeight = windowH * outsideScale;
                    imgWidth = (imgHeight / realHeight) * realWidth;
                } else {
                    if (realHeight * belowScale < windowW) {
                        imgHeight = realHeight * (belowScale - 0.2);
                        imgWidth = (imgHeight / realHeight) * realWidth;
                    } else {
                        imgWidth = realWidth;
                        imgHeight = realHeight;
                    }
                }
            }

            //设置放大图片的尺寸、偏移量并展示
            image.style.width = imgWidth + "px";
            image.style.height = imgHeight + "px";
            x = (windowW - imgWidth) * 0.5;
            y = (windowH - imgHeight) * 0.5;
            image.style.transform = "translate3d(" + x + "px, " + y + "px, 0)";
            outerdiv.style.display = "block";

            // 点击蒙版及外面区域放大图片关闭
            outerdiv.onclick = function () {
                outerdiv.style.display = "none";
                initScale = 1;
                window.removeEventListener("wheel", handleStopWheel);
            };

            // 阻止事件冒泡
            image.onclick = (e) => {
                if (outerdiv.style.display == "block") {
                    outerdiv.style.display = "none";
                }
                e.stopPropagation();
            };
        }

        function handleStopWheel(e) {
            var itemSizeChange = 1.1; //每一次滚动放大的倍数
            if (e.target.id == "bigimg") {
                // 说明：e.dataY如果大于0则表示鼠标向下滚动，反之则向上滚动，这里设计为向上滚动为缩小，向下滚动为放大
                if (e.deltaY < 0) {
                    itemSizeChange = 1 / 1.1;
                }
                var _initScale = initScale * itemSizeChange;

                // 说明：在超过或低于临界值时，虽然让initScale等于maxZoom或minreduce，但是在后续的判断中放大图片的最终倍数并没有达到maxZoom或minreduce，而是跳过。
                if (_initScale > maxZoom) {
                    initScale = maxZoom;
                } else if (_initScale < minreduce) {
                    initScale = minreduce;
                } else {
                    initScale = _initScale;
                }
                var origin = {
                    x: (itemSizeChange - 1) * imgWidth * 0.5,
                    y: (itemSizeChange - 1) * imgHeight * 0.5,
                };
                // 计算偏移量
                if (_initScale < maxZoom && _initScale > minreduce) {
                    x -= (itemSizeChange - 1) * (e.clientX - x) - origin.x;
                    y -= (itemSizeChange - 1) * (e.clientY - y) - origin.y;
                    image.style.transform = "translate3d(" + x + "px, " + y + "px, 0) scale(" + initScale + ")";
                }
            }

            // 阻止默认事件
            e.preventDefault();
        }

        function imgDrag() {
            // 绑定 鼠标按下事件
            image.addEventListener("pointerdown", pointerdown);
            // 绑定 鼠标移动事件
            image.addEventListener("pointermove", pointermove);
            image.addEventListener("pointerup", function (e) {
                if (isPointerdown) {
                    isPointerdown = false;
                }
            });
            image.addEventListener("pointercancel", function (e) {
                if (isPointerdown) {
                    isPointerdown = false;
                }
            });
        }

        function pointerdown(e) {
            isPointerdown = true;
            console.log(e.pointerId)

            // 说明：Element.setPointerCapture()将特定元素指定为未来指针事件的捕获目标。指针的后续事件将以捕获元素为目标，直到捕获被释放。可以理解为：在窗口不是全屏情况下，我在拖动放大图片时即使鼠标移出可窗口之外，此时事件还是捕获在该放大图片上。
            image.setPointerCapture(e.pointerId);

            lastPointermove = {
                x: e.clientX,
                y: e.clientY,
            };
        }

        function pointermove(e) {
            if (isPointerdown) {
                var current1 = {
                    x: e.clientX,
                    y: e.clientY,
                };
                diff.x = current1.x - lastPointermove.x;
                diff.y = current1.y - lastPointermove.y;
                lastPointermove = {
                    x: current1.x,
                    y: current1.y,
                };
                x += diff.x;
                y += diff.y;
                image.style.transform = "translate3d(" + x + "px, " + y + "px, 0) scale(" + initScale + ")";
            }
            e.preventDefault();
        }
    </script>
</body>
</html>
