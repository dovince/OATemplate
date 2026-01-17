<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MapSelectHelper.aspx.cs" Inherits="CommonSelect_MapSelectHelper" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Style/Style.css?v=20200327" rel="stylesheet" />
    <link href="../Style/Style1.css?v=20200327" rel="stylesheet" />
    <link href="../CSS/bootstrap.min.css?v=20200327" rel="stylesheet" />
    <link href="../CSS/default/easyui.css?v=20200327" rel="stylesheet" />
    <link href="../CSS/icon.css?v=20200327" rel="stylesheet" />
    <script type="text/javascript" src="../JS/jquery-1.11.2.min.js?v=20200327"></script>
    <script type="text/javascript" src="../JS/jquery.easyui.min.js?v=20200327"></script>
    <%--<script type="text/javascript" src="../JS/baidumap-2.0.js?v=20200327"></script>--%>
    <script type="text/javascript" src="https://api.map.baidu.com/api?v=3.0&ak=duoi1gyaAhMd2GPLCHBBl4H54IH0qmcU"></script>
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
        var _popWin = "win_select";
        $(function () {
            InitMap();
        });
        var map;
        var marker;
        var Geocoder;
        var geolocation;
        function InitMap() {
            Geocoder = new BMap.Geocoder();
            geolocation = new BMap.Geolocation();
            var initPoint = new BMap.Point(113.12325252690745, 23.029536644272907);

            map = new BMap.Map("mapcontainer", { mapType: BMAP_NORMAL_MAP });
            map.enableScrollWheelZoom();
            map.centerAndZoom(initPoint, 12);
            map.addControl(new BMap.ScaleControl()); //添加比例尺控件(左下角显示的比例尺控件)
            map.enableScrollWheelZoom(); //启用地图滚轮放大缩小
            map.enableKeyboard(); //启用键盘上下左右键移动地图
            map.enableContinuousZoom();   // 开启连续缩放效果
            map.enableInertialDragging(); // 开启惯性拖拽效果

            var mapType1 = new BMap.MapTypeControl({
                mapTypes: [BMAP_NORMAL_MAP, BMAP_SATELLITE_MAP],
                offset: new BMap.Size(80, 10)
            });

            var overView = new BMap.OverviewMapControl();
            var overViewOpen = new BMap.OverviewMapControl({ isOpen: true, anchor: BMAP_ANCHOR_BOTTOM_RIGHT });
            var myIcon = new BMap.Icon("../images/icons/marker_icon_24_24.png", new BMap.Size(24, 24));
            marker = new BMap.Marker(initPoint, { enableDragging: true, icon: myIcon });
            marker.addEventListener("dragend", function (type) {
                Geocoder.getLocation(type.point, function (rs) {
                    $("#lblSelectedAddr").text(rs.address);
                    recordLocation(rs.point, rs.addressComponents);
                })
            });
            map.addEventListener('ondragging', function () {
                marker.setPosition(map.getCenter());
            });
            //   地图停止移动后获取mk经纬度
            map.addEventListener('moveend', function () {
                var pos = marker.getPosition();
                //console.log(pos);
                // 创建地址解析对象
                Geocoder.getLocation(pos, function (rs) {
                    $("#lblSelectedAddr").text(rs.address);
                    recordLocation(rs.point, rs.addressComponents);
                });

            });

            var pos = marker.getPosition();
            marker.setPosition(pos.point);
            Geocoder.getLocation(pos, function (rs) {
                $("#lblSelectedAddr").text(rs.address);
                recordLocation(rs.point, rs.addressComponents);
            });

            geolocation.getCurrentPosition(function (r) {
                if (this.getStatus() == BMAP_STATUS_SUCCESS) {
                    marker.setPosition(r.point);
                    map.panTo(r.point);
                }
                else {
                    //alert('failed' + this.getStatus());
                }
            }, { enableHighAccuracy: true });

            map.addControl(mapType1);          //2D图，卫星图
            map.addControl(overView);          //添加默认缩略地图控件
            map.addControl(overViewOpen);      //右下角，打开
            map.addOverlay(marker);
        }
        function GetMapPotint() {
            var address = $("#txtSearch").val();
            var url = "<%=ZWL.Common.PublicMethod.BaiduGeocodingUrl%>";
            $.ajax(url + address, {
                dataType: 'jsonp',//服务器返回json格式数据
                crossDomain: true,
                jsonp: 'callback',
                jsonpCallback: 'Callback',
                type: 'get',//HTTP请求类型
                timeout: 10000,//超时时间设置为10秒；
                success: function (data2) {
                    if (data2.status == 0) {
                        var point = new BMap.Point(data2.result.location.lng, data2.result.location.lat);
                        marker.setPosition(point);
                        map.panTo(point);
                        map.setCenter(point);

                        var pos = marker.getPosition();
                        Geocoder.getLocation(pos, function (rs) {
                            $("#lblSelectedAddr").text(rs.address);
                            recordLocation(rs.point, rs.addressComponents);
                        });
                    }
                },
                error: function (xhr, type, errorThrown) {
                    //console.log(type);
                }
            });
        }
        document.onkeyup = function ()        //按Eneter键,搜索
        {
            if (window.event.keyCode == 13) {
                $("#btnSearch").click();
            }
        }
        function recordLocation(point, address) {
            //'{ "status":0, "result":{ "location":{ "lng":113.12647780300941, "lat":23.026133331951049 },"precise":1, "confidence":80, "comprehension":100, "level":"门址" } }';
            var result = '{';
            if (typeof (point) != 'undefined' && point != '') {
                result += '"point":' + JSON.stringify(point) + ",";
            }

            if (typeof (address) != 'undefined' && address != '') {
                result += '"addressComponents":' + JSON.stringify(address);
            }
            result += '}';
            $("#locationresult").val(result);
        }
        function CheckSelect() {
            var locat = $("#locationresult").val();
            if (locat == "") {
                return '{"status": 0, "result": "" }';
            }
            else {
                return '{"status":1,"result":' + locat + '}';
            }
        }

        function sendFormChild() {
            var json = JSON.parse(CheckSelect());
            if (json == null || json.length <= 0) {
                alert("请至少选择一项.");
            }
            else {
                window.returnValue = json;
                if (window.parent.length > 0) {
                    try {
                        window.parent.$('#' + _popWin).window('close');
                    } catch (ex) {
                        window.parent.$('#' + _popWin).window();
                    }
                }
                else {
                    window.close();
                }
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
                            <strong style="font-size: 16px;"><strong>地图选点</strong></strong>
                            <input type="hidden" id="locationresult" />
                        </td>
                    </tr>
                    <%--<tr>
                        <td colspan="2">
                            <select id="Province" runat="server" class="province" style="width:80px;height:20px;" onselect="" onserverchange="Province_ServerChange" ></select>
                        </td>
                    </tr>--%>
                    <tr>
                        <td style="width: 180px; text-align: right;">
                            <div style="padding-right: 2px;">
                                <label style="padding-left: 100px;"><strong>关键词:</strong></label>
                            </div>
                        </td>
                        <td>
                            <div style="padding: 2px;">
                                <div>
                                    <input type="text" id="txtSearch" runat="server" style="width: 50%;" />
                                    <a id="btnSearch" href="javascript:GetMapPotint();" style="width: 50px;" class='btn btn-info'>搜索</a>
                                    <a id="btnConfirm" href="javascript:sendFormChild();" style="width: 50px;" class='btn btn-info'>确定</a>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 180px; text-align: right;">
                            <div style="padding-right: 2px;">
                                <label id="lblSelectedTitle" style="padding-left: 100px;"><strong>选中地址:</strong></label>
                            </div>
                        </td>
                        <td>
                            <div style="padding: 2px;">
                                <table border="0" style="width: 100%">
                                    <tr>
                                        <td style="width: 500px;">
                                            <label id="lblSelectedAddr"></label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="mapcontainer" style="margin: 0 auto; margin-top: 10px; width: 95%; min-height: 75%; overflow: hidden; position: absolute;"></div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
