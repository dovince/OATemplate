// 事件绑定
function addEvent(obj, type, handle) {
    try {  // Chrome、FireFox、Opera、Safari、IE9.0及其以上版本
        obj.addEventListener(type, handle, false);
    } catch (e) {
        try {  // IE8.0及其以下版本
            obj.attachEvent('on' + type, handle);
        } catch (e) {  // 早期浏览器
            obj['on' + type] = handle;
        }
    }
}

// 事件解绑
function removeEvent(element, eType, handle, bol) {
    if (element.addEventListener) {
        element.removeEventListener(eType, handle, bol);
    } else if (element.attachEvent) {
        element.detachEvent("on" + eType, handle);
    } else {
        element["on" + eType] = null;
    }
}

//要格式化的数字
function number_format(number, decimals, dec_point, thousands_sep) {
    /*
    * 参数说明：
    * number：要格式化的数字
    * decimals：保留几位小数
    * dec_point：小数点符号
    * thousands_sep：千分位符号
    * */
    number = (number + '').replace(/[^0-9+-Ee.]/g, '');
    var n = !isFinite(+number) ? 0 : +number,
        prec = !isFinite(+decimals) ? 0 : Math.abs(decimals),
        sep = (typeof thousands_sep === 'undefined') ? ',' : thousands_sep,
        dec = (typeof dec_point === 'undefined') ? '.' : dec_point,
        s = '',
        toFixedFix = function (n, prec) {
            var k = Math.pow(10, prec);
            return '' + Math.ceil(n * k) / k;
        };

    s = (prec ? toFixedFix(n, prec) : '' + Math.round(n)).split('.');
    var re = /(-?\d+)(\d{3})/;
    while (re.test(s[0])) {
        s[0] = s[0].replace(re, "$1" + sep + "$2");
    }

    if ((s[1] || '').length < prec) {
        s[1] = s[1] || '';
        s[1] += new Array(prec - s[1].length + 1).join('0');
    }
    return s.join(dec);
}

//加载中事件
function blockUI(options) {
    options = $.extend(true, {}, options);
    if (options.target) { // element blocking
        var el = $(options.target);
        if (el.height() <= ($(window).height())) {
            options.cenrerY = true;
        }
    }
    $.blockUI({
        message: '<div class="loading-message loading-message-boxed"><img src="../images/loading-spinner-grey.gif" align="center"><span>&nbsp;&nbsp;' + (options.message ? options.message : '加载中...') + '</span></div>',
        target: options.target,
        baseZ: 99999,
        centerY: options.cenrerY !== undefined ? options.cenrerY : false,
        css: {
            //top: '50%',
            border: '0',
            padding: '0',
            backgroundColor: 'none'
        },
        overlayCSS: {
            backgroundColor: '#555',
            opacity: 0.1,
            cursor: 'wait'
        }
    });
}

// 日期，在原有日期基础上，增加days天数，默认增加1天
function addDate(date_str, days) {
    if (days == undefined || days == '') {
        days = 1;
    }
    var date = stringToDate(date_str);
    date.setDate(date.getDate() + days);
    var month = date.getMonth() + 1;
    var day = date.getDate();
    return date.getFullYear() + '-' + getFormatDate(month) + '-' + getFormatDate(day);
}

function stringToDate(dateStr, separator) {
    if (!separator) {
        separator = "-";
    }
    var dateArr = dateStr.split(separator);
    var year = parseInt(dateArr[0]);
    var month;
    //处理月份为04这样的情况                         
    if (dateArr[1].indexOf("0") == 0) {
        month = parseInt(dateArr[1].substring(1));
    } else {
        month = parseInt(dateArr[1]);
    }
    var day = parseInt(dateArr[2]);
    var date = new Date(year, month - 1, day);
    return date;
}

// 日期月份/天的显示，如果是1位数，则在前面加上'0'
function getFormatDate(arg) {
    if (arg == undefined || arg == '') {
        return '';
    }
    var re = arg + '';
    if (re.length < 2) {
        re = '0' + re;
    }
    return re;
}

// Handles the go to top button at the footer
function handleGoTop() {
    var offset = 300;
    var duration = 500;

    if (navigator.userAgent.match(/iPhone|iPad|iPod/i)) {  // ios supported
        $(window).bind("touchend touchcancel touchleave", function (e) {
            if ($(this).scrollTop() > offset) {
                $('.scroll-to-top').fadeIn(duration);
            } else {
                $('.scroll-to-top').fadeOut(duration);
            }
        });
    } else {  // general 
        $(window).scroll(function () {
            if ($(this).scrollTop() > offset) {
                $('.scroll-to-top').fadeIn(duration);
            } else {
                $('.scroll-to-top').fadeOut(duration);
            }
        });
    }

    $('.scroll-to-top').click(function (e) {
        e.preventDefault();
        $('html, body').animate({ scrollTop: 0 }, duration);
        return false;
    });
};


//获取下拉框的数据(公司和部门的数据)
function GetSelectData(_type) {
    $.ajax({
        type: "post",
        async: false,
        url: "../BusinessManage/GetStatisticsResultHandler.ashx",
        dataType: "json",//返回数据形式为json 
        data: {
            flag: "GetSelectData",
            type: _type
        },
        success: function (data) {
            var rows = JSON.parse(data.rows);
            var item = rows[0];
            //if (item.company == "无权限") {
            //document.getElementById("company").options.length = 0;
            //document.getElementById("company").options.add(new Option("无权限", "无权限"));
            //}
            if (item.dept == "无权限") {
                document.getElementById("department").options.length = 0;
                document.getElementById("department").options.add(new Option("无权限", "无权限"));
            } else {
                var items = item.dept.split(',');
                if (items.length > 1) {
                    document.getElementById("department").options.add(new Option("全部", "全部", true));
                }
                $.each(items, function (i, item) {
                    document.getElementById("department").options.add(new Option(item, item));
                });
            }
        },
        error: function (errorMsg) {
            alert("查询数据结果为空，请联系管理员！");
        }
    });
}

function GetAjaxComPieDataByData(ec, querytype, data) {
    blockUI(
        {
            target: "#form1",
            message: "加载中..."
        });
    setTimeout(function () {
        var colors = ["#61A0A8", "#D81B60", "#605ca8", "#FF703C", "#a772dd", "#5ac464", "#D48265"];
        var rows = data;
        $(".fullWidth").hide();
        if (!IsDataNull(rows)) {
            for (var i = 0; i < rows.length; i++) {
                var item = rows[i];
                GetComPieData(ec, item.elementID, item.categroy, item.value, colors[i]);
            }
        } else {
            for (var i = 0; i < rows.length; i++) {
                var item = rows[i];
                if (item.value != "0") {
                    ComPieClickHandle(item.elementID);
                }
                GetComPieData(ec, item.elementID, item.categroy, item.value, colors[i]);
                if (item.value == "0") {
                    //数据为空的时候，隐藏圆形图
                    $("#" + item.elementID + "_count").parents(".fullWidth").hide();
                    $("#" + item.elementID + "_money").parents(".fullWidth").hide();
                } else {
                    var pieInfos = item.pieInfos;
                    for (var j = 0; j < pieInfos.length; j++) {
                        var pie = pieInfos[j];
                        var type = pie.type;
                        var elementid = item.elementID + "_" + type;
                        var zhCN = type == "count" ? "数量" : "金额";
                        var unit = type == "count" ? "个" : "元";
                        if (pie.PieInfoMingXiList.length > 0) {
                            $("#" + elementid).parents(".fullWidth").show();
                        }

                        GetNormalPie(ec, elementid, item.categroy + zhCN, pie.PieInfoMingXiList, unit, pie.total);
                    }
                }
            }
        }
        $.unblockUI();
    }, 500);
}
//ajax 获取数据
function GetAjaxComPieData(ec, querytype) {
    var rows = [];
    $.ajax({
        type: "post",
        async: false,
        url: "../BusinessManage/GetStatisticsResultHandler.ashx",
        dataType: "json",//返回数据形式为json 
        data: {
            flag: "GetNormalCount",
            //company: $("#company option:selected").val(),
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
    GetAjaxComPieDataByData(ec, querytype, rows);
}

//点击第一行数据的时候，移动到对应位置
function ComPieClickHandle(ID) {
    var duration = 500;
    if (document.getElementById(ID) != null) {
        addEvent(document.getElementById(ID), 'click', function () {
            try {
                $('html, body').animate({ scrollTop: $("#" + ID + "_count").offset().top - 210 }, duration);
            } catch (e) {
                window.location.hash = ID + "_count";
            }
        });
    }
}

//显示各种项目的数量（第一行图表）
function GetComPieData(ec, id, _name, count, color) {
    if (id != "" && $("#" + id).length > 0) {
        ec.dispose(document.getElementById(id))
        var myChart = ec.init(document.getElementById(id));
        myChart.showLoading({
            text: "加载中..."
        });
        var option = {
            title: {
                text: '+' + count,
                subtext: _name,
                x: 'center',
                y: 30,
                textStyle: {
                    fontSize: 25,
                    color: color
                },
                subtextStyle: {
                    fontSize: 20,
                    color: color
                }
            },
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)",
                show: false
            },
            color: [color],
            toolbox: {
                show: false,
                feature: {
                    mark: { show: true },
                    dataView: { show: true, readOnly: true },
                    restore: { show: true },
                    saveAsImage: { show: false }
                }
            },
            calculable: false, //圆形外框
            series: [{
                name: _name,
                type: 'pie',
                radius: ['78%', '80%'],
                silent: true,
                clickable: false,
                itemStyle: {
                    normal: {
                        label: {
                            show: false,
                            position: 'center',
                            textStyle: {
                                fontSize: '20',
                                fontWeight: 'bold'
                            }
                        },
                        labelLine: {
                            show: false
                        }
                    }
                },
                data: [
                    { name: _name, value: count },
                    { name: '1', value: 0.00001 }
                ]
            }]

        };
        myChart.setOption(option);
        myChart.hideLoading();
    }
}

//圆形图
function GetNormalPie(ec, id, titletext, data, unit, total) {
    if (id != "" && $("#" + id).length > 0) {
        ec.dispose(document.getElementById(id))
        var myChart = ec.init(document.getElementById(id));
        myChart.clear();
        myChart.showLoading({
            text: "加载中..."
        });
        if (id != "" && $("#" + id).length > 0) {
            var legenddata = [];
            $.each(data, function (i, item) {
                if (item.name != "") {
                    legenddata.push(item.name);
                }
            })
            var option = {
                title: {
                    text: titletext,
                    //subtext: subtext,
                    x: 100,
                    y: 200,
                    textAlign: 'left',
                    textStyle: {
                        fontWeight: 'normal',
                        fontSize: 20,
                    },
                    subtextStyle: {
                        fontSize: 14,
                    }
                },
                tooltip: {
                    trigger: 'item',
                    //formatter: "{a} <br/>{b} : {c} ({d}%)"
                    //formatter: "{c} " + unit + " 占比 {d}%"
                    formatter: function (value) {
                        if (titletext.indexOf("金额") != -1) {
                            return value.name + "：" + number_format(value.value, ".", ",") + " 占比：" + value.percent + "%";
                        } else {
                            return value.name + "：" + value.value + " 占比：" + value.percent + "%";
                        }
                    }
                },
                legend: {
                    orient: 'vertical',
                    x: 320,
                    y: 25,
                    data: legenddata,
                    textStyle: {
                        fontSize: 13
                    },
                    tooltip: {
                        show: false
                    }
                },
                toolbox: {
                    show: false,
                    feature: {
                        mark: { show: true },
                        dataView: { show: true, readOnly: true },
                        restore: { show: true },
                    }
                },
                calculable: false,
                series: [{
                    name: '',
                    type: 'pie',
                    radius: ['60%', '70%'],
                    center: ['72%', '50%'],
                    itemStyle: {
                        normal: {
                            label: {
                                show: true,
                                //formatter: '{b} : {c}',
                                formatter: function (value) {
                                    if (titletext.indexOf("金额") != -1) {
                                        return value.name + "：" + number_format(value.value, ".", ",");
                                    } else {
                                        return value.name + "：" + value.value;
                                    }
                                },
                                textStyle: {
                                    fontSize: 14,
                                    position: 'inner'
                                }
                            },
                            labelLine: {
                                show: true,
                                length: 5
                            },
                        },
                        emphasis: {
                            label: {
                                textStyle: {
                                    fontSize: 14,
                                    fontWeight: 'bold',
                                }
                            }
                        }
                    },
                    data: data
                },
                {
                    name: '',
                    type: 'pie',
                    radius: [0, '30%'],
                    center: ['72%', '50%'],
                    tooltip: {
                        show: false
                    },
                    itemStyle: {
                        normal: {
                            label: {
                                show: true,
                                //formatter: '{b} : {c}',
                                formatter: function (value) {
                                    var old_str = value.name + "：" + value.value;
                                    var strs = old_str.split(''); //字符串数组
                                    var str = old_str;
                                    var MaxLength = 20;
                                    if (strs.length > MaxLength) {
                                        for (var i = 0, s; s = strs[i++];) { //遍历字符串数组
                                            str += s;
                                            if (!(i % MaxLength)) str += '\n'; //按需要求余
                                        }
                                    }
                                    return str;
                                },
                                position: 'center',
                                textStyle: {
                                    color: '#666',
                                    fontSize: 20
                                }
                            },
                            labelLine: {
                                show: false
                            },
                            color: 'rgba(255, 255, 255, 0)'
                        }
                    },

                    data: [
                        { value: total + unit, name: '合计' }
                    ]
                }

                ]
            };
            myChart.setOption(option);
            myChart.hideLoading();
        }
    }
}

//圆形图(横向)
function GetNormalPie_hx(ec, id, titletext, data, unit, total) {
    var myChart = ec.init(document.getElementById(id));
    myChart.showLoading({
        text: "加载中..."
    });
    if (id != "" && $("#" + id).length > 0) {
        var legenddata = [];
        $.each(data, function (i, item) {
            if (item.name != "") {
                legenddata.push(item.name);
            }
        })
        var option = {
            title: {
                text: titletext,
                //subtext: subtext,
                x: 'center',
                y: 'top',
                textAlign: 'left',
                textStyle: {
                    fontWeight: 'normal',
                    fontSize: 20,
                },
                subtextStyle: {
                    fontSize: 14,
                }
            },
            tooltip: {
                trigger: 'item',
                //formatter: "{a} <br/>{b} : {c} ({d}%)"
                //formatter: "{c} " + unit + " 占比 {d}%"
                formatter: function (value) {
                    if (titletext.indexOf("金额") != -1) {
                        ;
                        return value.name + "：" + number_format(value.value, ".", ",") + " 占比：" + value.percent + "%";
                    } else {
                        return value.name + "：" + value.value + " 占比：" + value.percent + "%";
                    }
                }
            },
            legend: {
                orient: 'vertical',
                x: 250,
                y: 25,
                data: legenddata,
                show: false,
                textStyle: {
                    fontSize: 13
                },
                tooltip: {
                    show: false
                }
            },
            toolbox: {
                show: false,
                feature: {
                    mark: { show: true },
                    dataView: { show: true, readOnly: true },
                    restore: { show: true },
                }
            },
            calculable: false,
            series: [{
                name: '',
                type: 'pie',
                y: 100,
                radius: ['50%', '60%'],
                center: ['50%', '50%'],
                itemStyle: {
                    normal: {
                        label: {
                            show: true,
                            //formatter: '{b} : {c}',
                            formatter: function (value) {
                                if (titletext.indexOf("金额") != -1) {
                                    return value.name + "：" + number_format(value.value, ".", ",");
                                } else {
                                    return value.name + "：" + value.value;
                                }
                            },
                            textStyle: {
                                fontSize: 14,
                                position: 'inner'
                            }
                        },
                        labelLine: {
                            show: true,
                            length: 5
                        },
                    },
                    emphasis: {
                        label: {
                            textStyle: {
                                fontSize: 14,
                                fontWeight: 'bold',
                            }
                        }
                    }
                },
                data: data
            },
            {
                name: '',
                type: 'pie',
                radius: [0, '30%'],
                //center: ['72%', '50%'],
                tooltip: {
                    show: false
                },
                itemStyle: {
                    normal: {
                        label: {
                            show: true,
                            //formatter: '{b} : {c}',
                            formatter: function (value) {
                                var old_str = value.name + "：" + value.value;
                                var strs = old_str.split(''); //字符串数组
                                var str = old_str;
                                var MaxLength = 20;
                                if (strs.length > MaxLength) {
                                    for (var i = 0, s; s = strs[i++];) { //遍历字符串数组
                                        str += s;
                                        if (!(i % MaxLength)) str += '\n'; //按需要求余
                                    }
                                }
                                return str;
                            },
                            position: 'center',
                            textStyle: {
                                color: '#666',
                                fontSize: 20
                            }
                        },
                        labelLine: {
                            show: false
                        },
                        color: 'rgba(255, 255, 255, 0)'
                    }
                },

                data: [
                    { value: total + unit, name: '合计' }
                ]
            }

            ]
        };
        myChart.setOption(option);
        myChart.hideLoading();
    }
}

//柱形图
function GetNormalBar(ec, id, titletext, subtext) {
    var myChart = ec.init(document.getElementById(id))
    var option = {
        title: {
            text: titletext,
            y: '10%',
            subtext: subtext,
            textStyle: {
                fontSize: 21
            },
            subtextStyle: {
                fontSize: 19,
                color: '#000'
            }
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            data: ['上一年', '今年'],
            x: 'right',
            textStyle: {
                fontSize: 20
            }
        },
        toolbox: {
            show: false,
            dataView: { show: true, readOnly: true }
        },
        calculable: false,
        xAxis: [{
            type: 'category',
            data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'],
            axisLabel: {
                show: true,
                textStyle: {
                    fontSize: 20
                }
            }
        }],
        yAxis: [{
            type: 'value',
            axisLabel: {
                show: true,
                textStyle: {
                    fontSize: 20
                }
            }
        }],
        series: [{
            name: '上一年',
            type: 'bar',
            data: [2.0, 4.9, 7.0, 23.2, 25.6, 76.7, 135.6, 162.2, 32.6, 20.0, 6.4, 3.3],
            markPoint: {
                symbolSize: 30,
                data: [
                    { type: 'max', name: '最大值' },
                    { type: 'min', name: '最小值' }
                ],
                itemStyle: {
                    normal: {
                        label: {
                            textStyle: {
                                fontSize: 17
                            }
                        }
                    },
                    emphasis: {
                        label: {
                            textStyle: {
                                fontSize: 17
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
                            textStyle: {
                                fontSize: 19,
                                fontWeight: 'bold'
                            }
                        },
                        emphasis: {
                            label: {
                                textStyle: {
                                    fontSize: 17
                                }
                            }
                        }
                    }
                }
            },
        },
        {
            name: '今年',
            type: 'bar',
            data: [2.6, 5.9, 9.0, 26.4, 28.7, 70.7, 175.6, 182.2, 48.7, 18.8, 6.0, 2.3],
            itemStyle: {
                normal: {
                    color: '#3fb1e3'
                }
            },
            markPoint: {
                data: [
                    { name: '年最高', value: 182.2, xAxis: 7, yAxis: 183, symbolSize: 18 },
                    { name: '年最低', value: 2.3, xAxis: 11, yAxis: 3 }
                ],
                itemStyle: {
                    normal: {
                        label: {
                            textStyle: {
                                fontSize: 17
                            }
                        }
                    },
                    emphasis: {
                        label: {
                            textStyle: {
                                fontSize: 17
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
                            textStyle: {
                                fontSize: 19,
                                fontWeight: 'bold'
                            }
                        }
                    },
                    emphasis: {
                        label: {
                            textStyle: {
                                fontSize: 17
                            }
                        }
                    }
                }
            }
        }
        ]
    };
    myChart.setOption(option);
}

//判断数据是否为空
function IsDataNull(data) {
    var flag = false;
    for (var i = 0; i < data.length; i++) {
        var item = data[i];
        if (item.value != "0") {
            flag = true;
            break;
        }
    }
    return flag;
}