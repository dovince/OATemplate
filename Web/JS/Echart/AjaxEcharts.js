$(document).ready(function () {
    var ec = echarts;

    GetSelectData("StatisticsView");

    handleGoTop();

    GetAjaxComPieData(ec, "month");

    $(".input_cxcalendar").each(function () {
        //debugger
        var a = new Calendar({
            targetCls: $(this),
            type: 'yyyy-mm-dd',
            wday: 1
        }, function (val) {
            //console.log(val);
            timechange(ec);
        });
    });

    $(".data-pick").click(function () {
        $('.data-pick').removeClass('current');
        $(this).addClass('current');
        $("#starttime").val("");
        $("#endtime").val("");
        GetAjaxComPieData(ec, $(this)[0].id);
    });

    //addEvent(document.getElementById("company"), 'change', function () {
    //    dropHandle();
    //});

    addEvent(document.getElementById("department"), 'change', function () {
        dropHandle();
    });

});

function dropHandle() {
    var querytype = "";
    $(".data-pick").each(function (i, item) {
        if ($("#" + item.id).hasClass('current')) {
            querytype = item.id;
        }
    });
    if (querytype != "") {
        GetAjaxComPieData(echarts, querytype);
    } else {
        GetAjaxComPieData(echarts, "time");
    }
}

//重新加载数据
function reload() {
    var querytype = "";
    $(".data-pick").each(function (i, item) {
        if ($("#" + item.id).hasClass('current')) {
            querytype = item.id;
        }
    });
    if (querytype != "") {
        GetAjaxComPieData(echarts, querytype);
    } else {
        GetAjaxComPieData(echarts, "time");
    }
}

//选择起始日期事件
function timechange(eCharts) {
    $('.data-pick').removeClass('current');
    var starttime_str = $("#starttime").val();
    var endtime_str = $("#endtime").val();
    if (endtime_str == "") {
        $("#endtime").val(addDate(starttime_str, 1));
    }
    if (starttime_str != "" && endtime_str != "") {
        if (new Date(starttime_str) > new Date(endtime_str)) {
            alert("结束时间需要大于开始时间！");
            $("#endtime").val("")
            return;
        }
    }
    GetAjaxComPieData(eCharts, "time");

}






