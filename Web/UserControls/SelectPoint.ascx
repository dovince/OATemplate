<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectPoint.ascx.cs" Inherits="UserControls_SelectPoint" %>
<%@ Import Namespace="System.Linq" %>
<input id="btnShowBaiduMap" type="button" name="btnShowBaiduMap" value="地图选址" onclick="javascript: void (0);" style="width: 70px;" />
<input id="hiddenpoint" name="HiddenPoint" type="hidden" class="hiddenpoint" value='<%= (IsSelected?Result:"")  %>' />
<script type="text/javascript">
    $(function () {
        //console.log(eval('[{"id":1,"name":"4353534.doc","desc":"上头文件"},{"id":2,"name":"67575757.doc","desc":"红头刘珊"},{"id":3,"name":"56767686.doc","desc":"老师来了"}]'));
        $(document).on("click", "#btnShowBaiduMap", function () {
            showBaiduMapModel();
        });
    });
    function setAddressValue(result) {
        var province = result.addressComponents.province;
        switch (province) {
            case "内蒙古自治区":
                province = "内蒙古";
                break;
            case "广西壮族自治区":
                province = "广西省";
                break;
            case "宁夏回族自治区":
                province = "宁夏省";
                break;
            case "新疆维吾尔自治区":
                province = "新疆省";
                break;
            case "西藏自治区":
                province = "西藏省";
                break;
            case "天津市":
                province = "天津";
                break;
            case "北京市":
                province = "北京";
                break;
            case "上海市":
                province = "上海";
                break;
            case "重庆市":
                province = "重庆";
                break;
        }
        $(".province").val(province);
        $.post("../Services/Services.ashx?f=GetDDLXian&PN=" + province, {}, function (data1) {
            data1 = eval('(' + data1 + ')');
            if (data1.Code) {
                var html = "";
                for (var i = 0; i < data1.Data.length; i++) {
                    html += '<option value = "' + data1.Data[i].text + '" >' + data1.Data[i].text + '</option>';
                }
                $(".city").html(html);
                $(".city").val(result.addressComponents.city);
                $.post("../Services/Services.ashx?f=GetDDLXian&PN=" + result.addressComponents.city, {}, function (data2) {
                    data2 = eval('(' + data2 + ')');
                    html = "";
                    for (var i = 0; i < data2.Data.length; i++) {
                        html += '<option value = "' + data2.Data[i].text + '">' + data2.Data[i].text + '</option>';
                    }
                    $(".area").html(html);
                    $(".area").val(result.addressComponents.district);
                })
            }
        })
    }
    function showBaiduMapModel() {
        var RadNum = Math.random();
        var addstr = $("#DDLSheng").val() + $("#DDLShi").val() + $("#DDLXian").val() + $("#txtstreet").val();
        var options = {
            title: "选择地址",
            url: '../CommonSelect/MapSelectHelper.aspx?address=' + addstr + '&Radstr=' + RadNum,
            width: 800,
            height: 500,
            onFinish: function (returnVal) {
                var data = returnVal;
                if (data.status == 1) {
                    $("#DDLSheng").val(data.result.addressComponents.province);
                    $("#DDLShi").val(data.result.addressComponents.city);
                    $("#DDLXian").val(data.result.addressComponents.district);
                    setAddressValue(data.result);
                    $("#txtstreet").val(data.result.addressComponents.street + data.result.addressComponents.streetNumber);
                    $(".hiddenpoint").val(JSON.stringify(data.result.point));
                }
            }
        }
        showPopwindow('', options);
    }

</script>
