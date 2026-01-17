$(document).ready(function () {
    var ec = echarts;

    GetSelectData("XMQQXXStatics");

    GetAjaxComPieData(ec, "currentday");

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

    addEvent(document.getElementById("company"), 'change', function () {
        dropHandle();
    });

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

//ajax 获取数据
function GetAjaxComPieData(ec, querytype) {
    var colors_money = ["#749f83", "#d48265"];
    var colors_count = ["#61a0a6", "#d48e28"];

    blockUI(
        {
            target: "#form1",
            message: "加载中..."
        });
    setTimeout(function () {
        var rows = [];
        $.ajax({
            type: "post",
            async: false,
            url: "../BusinessManage/GetStatisticsResultHandler.ashx",
            dataType: "json",//返回数据形式为json 
            data: {
                flag: "GetXMQQMX",
                company: $("#company option:selected").val(),
                department: $("#department option:selected").val(),
                querytype: querytype,
                starttime: $("#starttime").val(),
                endtime: $("#endtime").val()
            },
            success: function (data) {
                rows = JSON.parse(data.rows);
            },
            error: function (errorMsg) {
                alert("查询数据结果为空，请联系管理员！");
            }
        });

        if (!IsDataNull(rows)) {
            $("#barElement").hide();
        } else {
            $("#barElement").show();
            var xData = [];
            if (rows[0].xData != null && rows[0].xData.length > 0) {
                xData = rows[0].xData;
            } else {
                xData = rows[1].xData;
            }

            GetNormalBar(ec, "moneybar", "金额（单位：元）", rows[0].legendData, xData, rows[0].yData_money, rows[0].MinValue_money, rows[0].MinIndex_money,
                rows[1].yData_money, rows[1].MinValue_money, rows[1].MinIndex_money, colors_money);

            GetNormalBar(ec, "numbar", "数量（单位：个）", rows[0].legendData, xData, rows[0].yData_num, rows[0].MinValue_num, rows[0].MinIndex_num,
                rows[1].yData_num, rows[1].MinValue_num, rows[1].MinIndex_num, colors_count);
        }
        $.unblockUI();
    }, 500);

}

//柱形图
function GetNormalBar(ec, id, titletext, legendData, xData, PrefData, PreMin, PreMinIndex, CurrentData, CurrentMin, CurrentMinIndex,colors) {
    ec.dispose(document.getElementById(id))
    var myChart = ec.init(document.getElementById(id))
    myChart.showLoading({
        text: "加载中..."
    });
    var option = {
        title: {
            text: titletext,
            x: 'center',
            y: 'top',
            subtext: "",
            textStyle: {
                fontSize: 14
            },
            subtextStyle: {
                fontSize: 14,
                color: '#000'
            }
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            data: legendData,
            x: 'right',
            textStyle: {
                fontSize: 14
            },
            padding:[5,80,5,5],
            top:'5%'
        },
        toolbox: {
            show: false,
            dataView: { show: true, readOnly: true }
        },
        grid: {//直角坐标系内绘图网格
            show: true,//是否显示直角坐标系网格。[ default: false ]
            //left:"20%",//grid 组件离容器左侧的距离。
            //right:"30px",
            //borderColor:"#c45455",//网格的边框颜色
            top:"20%",
            bottom: "20%", 
            heigth:200
        },
        calculable: false,
        xAxis: [{
            type: 'category',
            data: xData,
            //max:10,
            axisLabel: {
                show: true,
                textStyle: {
                    fontSize: 13
                },
                interval: 0,
                rotate: "45"
            }
        }],
        yAxis: [{
            type: 'value',
            axisLabel: {
                show: true,
                text: "",
                textStyle: {
                    fontSize: 13
                }
            }
        }],
        series: [{
            name: legendData[0],
            type: 'bar',
            data: PrefData,
            itemStyle: {
                normal: {
                    color: colors[0]
                }
            },
            //barMaxWidth:30,
            markPoint: {
                symbol: 'pin',
                //symbolSize: 60,
                data: [
                    { type: 'max', name: '最大值' },
                    { name: '最小值', value: PreMin, xAxis: xData[PreMinIndex], yAxis: PreMin }
                ],
                itemStyle: {
                    normal: {
                        label: {
                            formatter: function (value) {
                                return MarkPointFormat(titletext, value.value);
                            },
                            textStyle: {
                                fontSize: 14,
                                color: '#3C3C3C'
                            }
                        }
                    },
                    emphasis: {
                        label: {
                            formatter: function (value) {
                                return MarkPointFormat(titletext, value.value);
                            },
                            textStyle: {
                                fontSize: 14,
                                color: '#3C3C3C'
                            }
                        }
                    }
                }
            },
            markLine: {
                data: [
                    { type: 'average', name: '平均值' }
                ],
                itemStyle: {
                    normal: {
                        label: {
                            formatter: function (value) {
                                return MarkPointFormat(titletext, value.value);
                            },
                            textStyle: {
                                fontSize: 14,
                                fontWeight: 'bold'
                            }
                        },
                        emphasis: {
                            label: {
                                formatter: function (value) {
                                    return MarkPointFormat(titletext, value.value);
                                },
                                textStyle: {
                                    fontSize: 14
                                }
                            }
                        }
                    }
                }
            },
        },
        {
            name: legendData[1],
            type: 'bar',
            data: CurrentData,
            itemStyle: {
                normal: {
                    color: colors[1]
                }
            },
            markPoint: {
                data: [
                    //{ name: '最大值', value: 182.2, xAxis: 7, yAxis: 183 },
                    //{ name: '最小值', value: 2.3, xAxis: 11, yAxis: 3 }
                    { type: 'max', name: '最大值' },
                    { name: '最小值', value: CurrentMin, xAxis: xData[CurrentMinIndex], yAxis: CurrentMin }
                ],
                itemStyle: {
                    normal: {
                        label: {
                            formatter: function (value) {
                                return MarkPointFormat(titletext, value.value);
                            },
                            textStyle: {
                                fontSize: 14,
                                color: '#3C3C3C'
                            }
                        }
                    },
                    emphasis: {
                        label: {
                            formatter: function (value) {
                                return MarkPointFormat(titletext, value.value);
                            },
                            textStyle: {
                                fontSize: 14,
                                color: '#3C3C3C'
                            }
                        }
                    }
                }
            },
            markLine: {
                data: [
                    { type: 'average', name: '平均值' }
                ],
                itemStyle: {
                    normal: {
                        label: {
                            formatter: function (value) {
                                return MarkPointFormat(titletext, value.value);
                            },
                            textStyle: {
                                fontSize: 14,
                                fontWeight: 'bold'
                            }
                        }
                    },
                    emphasis: {
                        label: {
                            formatter: function (value) {
                                return MarkPointFormat(titletext, value.value);
                            },
                            textStyle: {
                                fontSize: 14
                            }
                        }
                    }
                }
            }
        }
        ]
    };
    myChart.setOption(option);
    myChart.hideLoading();
}

//判断数据是否为空
function IsDataNull(data) {
    var flag = false;
    var msg = "";
    if (data[0] != null && data[0].xData.length == 0) {
        msg += data[0].legendData[0] + "数据为空！\n";
    } else {
        flag = true;
    }
    if (data[1] != null && data[1].xData.length == 0) {
        msg += data[0].legendData[1] + "数据为空！\n";
    } else {
        flag = true;
    }
    if (msg != "") {
        alert(msg);
    }
    return flag;
}

function MarkPointFormat(titletext, value) {
    if (titletext.indexOf("金额") != -1) {
        return number_format(value, ".", ",");
    } else {
        return value;
    }
}




