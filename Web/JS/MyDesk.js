var isshow = false;
var piccount = 0;

$(document).ready(function () {

    Metronic.init();

    $(".dwtz .tab_top li").bind("click", function () {
        var titlelist = $(".dwtz .tab_top li");
        var panellist = $(".dwtz .contain .content");
        var index = $(this).attr("data-index");
        for (var i = 0; i < titlelist.length; i++) {
            var item = titlelist.eq(i);
            var panel = panellist.eq(i);
            item.addClass("others");
            panel.addClass("hidden");
        }
        $(this).removeClass("others");
        panellist.eq(index).removeClass("hidden");
    });

    $(".dongtaiimg").bind("click", function () {
        var link = $(this).attr("data-link");
        var el = document.createElement("a");
        document.body.appendChild(el);
        el.href = link;
        el.click();
        document.body.removeChild(el);
    });

    $(".oadt_list_more").bind("click", function () {
        //start ajax
        var count = $(".oadt_list ul li").length;
        $.ajax({
            cache: false,
            type: "get",
            data: { f: "GetMoreOADongTai", count: encodeURIComponent(count) },
            dataType: "text",
            url: "../Services/Services.ashx",
            success: function (data) {
                var res = eval('(' + data + ')');
                if (res.Data == "" || res.Data == null) {
                    $(".oadt_list_more_link").html("没有更多");
                } else {
                    var list = res.Data;
                    for (var i = 0; i < list.length; i++) {
                        var item = list[i];

                        var html = "<li><div class='tu_ditu_img'><dl><img class='dongtaiimg' src='" + item.ImgPath + "' data-link='/GongGao/GongGaoView.aspx?ID=" + item.ID + "' /></dl></div>"
                            + "<div class='ditu_tab_right'>"
                            + "<div class='tu_ditu_list'><a href='/GongGao/GongGaoView.aspx?ID=" + item.ID + "' class='news_zwdh'>" + item.TitleStr + "</a></div>"
                            + "<div class='tulist_fabu'>"
                            + "<strong>发布时间:</strong><span>" + formatDatebox(item.TimeStr) + "</span><strong style='padding-left:150px'>发布人:</strong><span>" + item.UserName + "</span></div>"
                            + "<div class='tu_ditu_wz'>" + item.Summary + "</div></div>"
                            + "</li>"
                        $(".oadt_list ul").append(html);
                    }
                    $(".oadt_list_more_link").html("更多");
                }
            }
        });
        //end ajax
    });

    $(".oadt_list_more_new").bind("click", function () {
        //start ajax
        var count = $(".scroller ul li").length;
        $.ajax({
            cache: false,
            type: "get",
            data: { f: "GetMoreOADongTai", count: encodeURIComponent(count) },
            dataType: "text",
            url: "../Services/Services.ashx",
            success: function (data) {
                var res = eval('(' + data + ')');
                if (res.Data == "" || res.Data == null) {
                    $(".oadt_list_more_new .oadt_list_more_link").html("没有更多");
                } else {
                    var list = res.Data;
                    for (var i = 0; i < list.length; i++) {
                        var item = list[i];

                        var html = "<li><div class='tu_ditu_img'><dl><img class='dongtaiimg' src='" + item.ImgPath + "' data-link='/GongGao/GongGaoView.aspx?ID=" + item.ID + "' /></dl></div>"
                            + "<div class='ditu_tab_right'>"
                            + "<div class='tu_ditu_list'><a href='/GongGao/GongGaoView.aspx?ID=" + item.ID + "' class='news_zwdh'>" + item.TitleStr + "</a></div>"
                            + "<div class='tulist_fabu'>"
                            + "<strong>发布时间:</strong><span>" + formatDatebox(item.TimeStr) + "</span><strong style='padding-left:150px'>发布人:</strong><span>" + item.UserName + "</span></div>"
                            + "<div class='tu_ditu_wz'>" + item.Summary + "</div></div>"
                            + "</li>"
                        $(".oadt_list ul").append(html);
                    }
                    $(".oadt_list_more_new .oadt_list_more_link").html("更多");
                }
            }
        });
        //end ajax
    });

    $.ajax({
        type: "post",
        url: "../Services/Services.ashx",
        data: { f: 'GetPhotoNews'},
        dataType: "text",
        error: function (XMLHttpRequest, textStatus, errorThrow) {
            //alert("网络慢，正在努力加载新闻图片信息，请稍等！");
        },
        success: function (res) {
            var data = eval('(' + res + ')');
            if (data.Code) {
                $.each(data.Data, function (i, item) {
                    var PicName, PicDescribe, ImgPath, PicHref;//一个Result对应一条图片新闻
                    for (var j = 0; j < data.Data.length; j++) {
                        PicName = item.PicName;
                        PicDescribe = item.PicDescribe;
                        ImgPath = item.ImgPath;
                        PicHref = item.PicHref;
                        if (PicName != null && PicDescribe != null && PicHref != null && ImgPath != null) {
                            var tempHTMLpic = "";
                            var tempHTMLnews = "";
                            var temp = "";
                            var textLimitLength = 40;
                            if (PicDescribe.length > textLimitLength) {
                                temp = PicDescribe.substring(0, textLimitLength) + "...";
                            } else {
                                temp = PicDescribe;
                            }
                            //如果四个参数都有值，设置图片信息
                            if (i == 0) {
                                tempHTMLpic = '<div id="switch_' + i + '" style="width:100%;height:100%;"><a href="' + PicHref + '" target="_blank"><img alt="' + PicDescribe + '" src="' + ImgPath + '" width="100%" height="100%" /></a> </div>';
                                tempHTMLnews = '<li style="text-align:left;"><a style="text-align:left;" id="focus_' + i + '" class="up" onmouseover="show_focus_image(' + i + ');" href="' + PicHref + '" target="_blank"title="' + PicDescribe + '">' + temp + '</a></li>'
                            } else {
                                tempHTMLpic = '<div id="switch_' + i + '" style="display:none;width:100%;height:100%;"><a href="' + PicHref + '" target="_blank"><img alt="' + PicDescribe + '" src="' + ImgPath + '" width="100%" height="100%" /></a> </div>';
                                tempHTMLnews = '<li style="text-align:left;"><a style="text-align:left;" id="focus_' + i + '" onmouseover="show_focus_image(' + i + ');" href="' + PicHref + '" target="_blank" title="' + PicDescribe + '">' + temp + '</a></li>'
                            }
                            $("#topnewspic").append(tempHTMLpic);
                            $("#newslist").append(tempHTMLnews);
                            piccount += 1;
                            //清空参数
                            break;//找下一条记录
                        }
                    }
                });
            }
        },
        error: function (msg) {
            alert(msg.status);
        }
    });

    //setTimeout(InitMap(), 1000);

    //InitMap();

    $("#leftPic").on("click", function () {

        if (isshow) {
            $("#drawDivPan").css('right', '13px')
            $("#drawDiv").hide(150);
            $("#leftPic").css("background-image", 'url("../images/picRight.png")');
            isshow = false;
        } else {
            $("#drawDivPan").css('right', '0px')
            $("#drawDiv").show(150);
            $("#leftPic").css("background-image", 'url("../images/picLeft.png")');
            isshow = true;
        }
    })

    var ShowArea = $("#ShowArea")[0].value;
    switch (ShowArea) {
        case "UnitDesk":
            $("#UnitDesk").show();
            $("#PersonDesk").hide();
            break;
        case "PersonDesk":
            $("#PersonDesk").show();
            $("#UnitDesk").hide();
            break;
        default:
            $("#UnitDesk").hide();
            $("#PersonDesk").hide();
            break;
    }

});
var icon_markerList = ["../images/BaiduMap/icon_mark0.png",
    "../images/BaiduMap/icon_mark1.png",
    "../images/BaiduMap/icon_mark2.png",
    "../images/BaiduMap/icon_mark3.png",
    "../images/BaiduMap/icon_mark4.png",
    "../images/BaiduMap/icon_mark5.png",
    "../images/BaiduMap/icon_mark6.png",
    "../images/BaiduMap/icon_mark7.png",
    "../images/BaiduMap/icon_mark8.png",
    "../images/BaiduMap/icon_mark9.png"]
var map = null;//地图实例
//初始化百度地图
function InitMap() {
    var initPoint = new BMap.Point(108.205973, 33.048804)
    //
    map = new BMap.Map("Map", { mapType: BMAP_NORMAL_MAP });
    map.centerAndZoom(initPoint, 4);
    map.enableKeyboard(); //启用键盘上下左右键移动地图
    map.enableContinuousZoom();   // 开启连续缩放效果
    map.enableInertialDragging(); // 开启惯性拖拽效果    
    var NavigationControl = new BMap.NavigationControl()

    map.addControl(NavigationControl);
    SearchXM();
}

function SearchXM() {
    $.ajax({
        type: "post", //要用post方式                 
        url: "MyDesk.aspx/GetXm",//方法所在页面和方法名
        data: "{XMKeyWord:''}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //alert(JSON.stringify(data.d));//返回的数据用data.d获取内容
            var result = eval('(' + data.d + ')');
            if (result.status == "success") {
                var XMList = result.dataoption;
                for (var i = 0; i < XMList.length; i++) {
                    var point = new BMap.Point(XMList[i].Lng, XMList[i].Lat);
                    var marker = new BMap.Marker(point);
                    map.addOverlay(marker);
                }
            } else {
                alert(result.message);
            }
        },
        error: function (err) {
            alert(JSON.stringify(err))
        }
    });
}

function openXmMap() {
    var parentWin = window.parent;
    parentWin.openXmMap(true);  //method为父页面的方法
}


//*************************日历控件***************************
function formatDatebox(value) {
    var date = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var currDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    var hour = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
    var minite = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
    var second = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();
    return date.getFullYear() + "/" + month + "/" + currDate + " " + hour + ":" + minite + ":" + second;
}

var userFormatString;

if (window.dialogArguments == null) {
    userFormatString = "yyyy-mm--dd";
}
else {
    userFormatString = window.dialogArguments;
}

with (new Date()) {
    var Nyear = getYear();
    var Nmonth = getMonth() + 1;
    var Ndate = getDate();
}

window.returnValue = new dataObj(Nyear, Nmonth, Ndate).getDateString(userFormatString);

window.document.onclick = function () {
    var obj = window.event.srcElement;
    if (obj.tagName.toLowerCase() == "span" && obj.parentNode.className.replace(/Ctable/ig, "star") == "star") {
        try {
            window.currentActiveItem.runtimeStyle.cssText = "";
        }
        catch (e) { }
        Nyear = obj.id.split("-")[0];
        Nmonth = obj.id.split("-")[1];
        Ndate = obj.id.split("-")[2];
        window.currentActiveItem = obj;
        window.currentSelectDate = window.currentActiveItem.id;
        window.currentActiveItem.runtimeStyle.cssText = "background:url(../images/bg.gif) no-repeat 12px 6px;color:#F00;padding-top:1px;font-weight:bold";
    }
}


function dataObj(year, month, date) {
    this.year = year
    this.month = month
    this.date = date
    this.getDateString =
        function (formatString) {
            return formatString.replace(/yyyy/ig, this.year).replace(/mm/ig, this.month).replace(/dd/ig, this.date)
        }
}

window.onload = function () {
    if (document.all.titleYear == null) {
        return;
    }
    window.document.attachEvent("onclick", doCmd);
    window.document.attachEvent("onmouseover", buttonOver);
    window.document.attachEvent("onmouseout", buttonOut);
    window.document.attachEvent("onmousedown", buttonDown);
    window.document.attachEvent("onmouseup", buttonUp);
    window.document.attachEvent("ondblclick",
        function () {
            var obj = window.event.srcElement;
            if (obj.tagName.toLowerCase() == "span" && obj.parentNode.className.replace(/Ctable/ig, "star") == "star") {
                var mydate = new dataObj(obj.id.split("-")[0], obj.id.split("-")[1], obj.id.split("-")[2]);
                window.returnValue = mydate.getDateString(userFormatString)
                window.close();
            }
        }

    );

    document.all.titleYear.innerHTML = TranYearMonthTitle(Nyear, Nmonth);
    document.all.weekNameBox.insertAdjacentHTML("afterBegin", makeWeekNameHtmlStr());
    document.all.calendarBox.innerHTML = makeCalendarHtmlStr(Nyear, Nmonth);
    window.currentSelectDate = starCaTran(Nyear, Nmonth, Ndate);
    window.document.all.calendarBox.show = show;
    window.currentActiveItem = window.document.getElementById(currentSelectDate);
    if (window.currentActiveItem)
        window.currentActiveItem.click();
    window.document.all.calendarBox.show();
}


function starCalendar(year, month) {
    this.year = year;
    this.month = month;
    this.monthTable = function () {
        var aMonth = new Array();
        for (i = 1; i < 7; i++) aMonth[i] = new Array(i);

        var dCalDate = new Date(this.year, this.month - 1, 1);
        var iDayOfFirst = dCalDate.getDay();
        var iDaysInMonth = new Date(this.year, this.month, 0).getDate();
        var iOffsetLast = new Date(this.year, this.month - 1, 0).getDate() - iDayOfFirst + 1;
        var iDate = 1;
        var iNext = 1;

        for (d = 0; d < 7; d++)
            aMonth[1][d] = (d < iDayOfFirst) ? (-iDayOfFirst + d + 1) : iDate++;
        for (w = 2; w < 7; w++)
            for (d = 0; d < 7; d++)
                aMonth[w][d] = iDate++;
        return aMonth;
    }
}
function makeWeekNameHtmlStr() {
    var tmpStr = "";
    var weekName = ["日", "一", "二", "三", "四", "五", "六"];
    for (var i = 0; i < 7; i++) tmpStr += "<span class=weekName>" + weekName[i] + "</span>";
    return tmpStr;
}
function makeCalendarHtmlStr(year, month) {
    window.theCalendar = new starCalendar(year, month);
    var theCaArr = theCalendar.monthTable();
    var theDaysInMonth = new Date(year, month, 0).getDate();
    var theCaHtml = "<div class=Ctable>";
    for (var i = 1; i < 7; i++)
        for (var j = 0; j < 7; j++)
            theCaHtml = theCaHtml + "<span class=" + ((theCaArr[i][j] < 1 || theCaArr[i][j] > theDaysInMonth) ? "OtherMonthDate" : "Cdate") + " id=" + starCaTran(year, month, theCaArr[i][j]) + ">" + starCaTran(year, month, theCaArr[i][j]).split("-")[2] + "</span>";
    return theCaHtml + "</div>";
}
function starCaTran(year, month, date) {
    with (new Date(year, month - 1, date))
        return getYear() + "-" + (getMonth() + 1) + "-" + getDate();
}
function TranYearMonthTitle(year, month) {
    with (new Date(year, month - 1, 1))
        return "<span style='text-decoration:underline;cursor:hand;font-weight:bold;padding:1 2 0 1;width:40px;' onclick=showMore(1940,2050,this.innerHTML) onmouseover=\"this.runtimeStyle.cssText='color:#fff;'\" onmouseout=\"this.runtimeStyle.cssText=''\" onpropertychange=showC()>" + getYear() + "</span>" + "年" + "<span style='text-decoration:underline;cursor:hand;font-weight:bold;padding:1 2 0 1;width:20px;' onclick=showMore(1,12,this.innerHTML) onmouseover=\"this.runtimeStyle.cssText='color:#fff;'\" onmouseout=\"this.runtimeStyle.cssText=''\" onpropertychange=showC()>" + (getMonth() + 1) + "</span>" + "月";
}

function showC() {
    if (event.propertyName != "innerHTML") return;
    window.theCalendar.year = new Number(document.all.titleYear.getElementsByTagName("span")[0].innerHTML);
    window.theCalendar.month = new Number(document.all.titleYear.getElementsByTagName("span")[1].innerHTML);
    window.document.all.calendarBox.innerHTML = makeCalendarHtmlStr(window.theCalendar.year, window.theCalendar.month);
    window.document.all.calendarBox.show = show; window.document.all.calendarBox.show();
}
function showMore(starNum, endNum, selectedValue) {
    var obj = window.event.srcElement;
    var selectedIndex = selectedValue - starNum;

    if (obj.selectBox) {
        obj.selectBox.selectedIndex = selectedIndex;
        return obj.selectBox.show(document.all.calendarBox.offsetHeight + document.all.weekNameBox.offsetHeight);
    }
    var selectBox = window.document.createElement("div");
    selectBox.className = "selectBox";
    selectBox.style.height = 0;
    selectBox.style.top = window.event.clientY - window.event.offsetY + window.event.srcElement.offsetHeight;
    selectBox.style.left = window.event.clientX - window.event.offsetX;
    selectBox.show = showBox;
    selectBox.selectedIndex = selectedIndex;
    selectBox.onclick = function () {
        var selectedObj = window.event.srcElement;
        if ("nobr" == selectedObj.tagName.toLowerCase() && selectBox.contains(selectedObj)) {
            if (obj.innerHTML != selectedObj.innerHTML) obj.innerHTML = selectedObj.innerHTML;
        }
    }
    selectBox.onlosecapture = alert
    var iString = "";
    for (var i = starNum; i <= endNum; i++) {
        iString += "<nobr  onmouseover=\"this.parentNode.getElementsByTagName('nobr')[this.parentNode.selectedIndex].style.cssText='';this.style.cssText='background-color:#00006C;color:#fff;'\"  onmouseout=this.style.cssText=''>" + i + "</nobr><br>"

    }
    selectBox.insertAdjacentHTML("afterBegin", iString);
    window.document.body.appendChild(selectBox);
    obj.selectBox = selectBox;
    obj.selectBox.show(document.all.calendarBox.offsetHeight + document.all.weekNameBox.offsetHeight);

}


function showBox(iHeight) {
    var box = this;
    box.style.height = 1;
    box.style.display = "block";
    window.clearInterval(box.timeHandle);
    box.timeHandle = window.setInterval(interValHandle, 1);

    var s = 0, t = 1;
    function interValHandle() {
        box.scrollTop = 1000000;
        s = s + t * t;
        t += 0.5;
        box.style.height = parseInt(box.style.height) + Math.floor(s);
        box.style.width = 65 / iHeight * box.offsetHeight;
        if (box.offsetHeight > iHeight) {
            window.clearInterval(box.timeHandle);
            box.style.height = iHeight;
            box.scrollTop = box.childNodes[0].offsetHeight * box.selectedIndex;
            box.getElementsByTagName("nobr")[box.selectedIndex].style.cssText = 'background-color:#00006C;color:#fff;';
            window.document.attachEvent("onclick",
                box.hide = function () {
                    box.style.display = "none";
                    window.document.detachEvent("onclick", box.hide)
                }
            );

        }
    }
}
function buttonOver() {
    var obj = window.event.srcElement;
    if (obj.tagName.toLowerCase() == "span" && obj.className.replace(/controlButton/ig, "star") == "star") {
        obj.runtimeStyle.cssText = "border-color:#fff #606060 #808080 #fff;padding:3 0 0 0 ";
    }
    if (obj.tagName.toLowerCase() == "span" && obj.parentNode.className.replace(/Ctable/ig, "star") == "star") {
        obj.style.backgroundColor = "#fff";
    }
}
function buttonOut() {
    var obj = window.event.srcElement;
    if (obj.tagName.toLowerCase() == "span" && obj.className.replace(/controlButton/ig, "star") == "star") {
        obj.runtimeStyle.cssText = "";
    }
    if (obj.tagName.toLowerCase() == "span" && obj.parentNode.className.replace(/Ctable/ig, "star") == "star") {
        window.setTimeout(function () { obj.style.backgroundColor = ""; }, 300);
    }
}
function buttonDown() {
    var obj = window.event.srcElement;
    if (obj.tagName.toLowerCase() == "span" && obj.className.replace(/controlButton/ig, "star") == "star") {
        obj.setCapture();
        obj.runtimeStyle.borderColor = "#808080 #fefefe #fefefe #808080";
    }
}
function buttonUp() {
    var obj = window.event.srcElement;
    if (obj.tagName.toLowerCase() == "span" && obj.className.replace(/controlButton/ig, "star") == "star") {
        obj.releaseCapture();
        obj.runtimeStyle.cssText = "";
    }
}
function doCmd() {
    var obj = window.event.srcElement;
    if (obj.tagName.toLowerCase() == "span" && obj.className.replace(/controlButton/ig, "star") == "star") {
        switch (obj.getAttribute("cmd")) {
            case "py":
                window.document.all.titleYear.innerHTML = window.TranYearMonthTitle(window.theCalendar.year - 1, window.theCalendar.month);
                window.document.all.calendarBox.innerHTML = makeCalendarHtmlStr(window.theCalendar.year - 1, window.theCalendar.month);
                break;
            case "pm":
                window.document.all.titleYear.innerHTML = window.TranYearMonthTitle(window.theCalendar.year, window.theCalendar.month - 1);
                window.document.all.calendarBox.innerHTML = makeCalendarHtmlStr(window.theCalendar.year, window.theCalendar.month - 1);
                break;
            case "nm":
                window.document.all.titleYear.innerHTML = window.TranYearMonthTitle(window.theCalendar.year, window.theCalendar.month + 1);
                window.document.all.calendarBox.innerHTML = makeCalendarHtmlStr(window.theCalendar.year, window.theCalendar.month + 1);
                break;
            case "ny":
                window.document.all.titleYear.innerHTML = window.TranYearMonthTitle(window.theCalendar.year + 1, window.theCalendar.month);
                window.document.all.calendarBox.innerHTML = makeCalendarHtmlStr(window.theCalendar.year + 1, window.theCalendar.month);
                break;
        }
        window.document.all.calendarBox.show();
        window.currentSelectDate = starCaTran(Nyear, Nmonth, Ndate);
        window.currentActiveItem = window.document.getElementById(currentSelectDate);
        if (window.currentActiveItem) window.currentActiveItem.runtimeStyle.cssText = "background:url(../images/choiceit.gif) no-repeat 12px 6px;color:#000;padding-top:1px;font-weight:bold";
    }
}


function show() {
    var box = this;
    window.clearTimeout(box.timeHandle);
    var CdateBoxs = this.getElementsByTagName("span");
    for (var i = 0; i < CdateBoxs.length; i++) {
        CdateBoxs[i].defaultValue = new Number(CdateBoxs[i].innerHTML);
        CdateBoxs[i].innerHTML = 0;
    }
    showDate();

    function showDate() {
        for (var i = 0; i < CdateBoxs.length; i++) {
            if (new Number(CdateBoxs[i].innerHTML) + 1 <= new Number(CdateBoxs[i].defaultValue))
                CdateBoxs[i].innerHTML = new Number(CdateBoxs[i].innerHTML) + 1
        }
        box.timeHandle = window.setTimeout(showDate, 1);
    }
    this.show = show1
}

function show1() {
    var box = this;
    window.clearTimeout(box.timeHandle);
    var CdateBoxs = this.getElementsByTagName("span");
    for (var i = 0; i < CdateBoxs.length; i++) CdateBoxs[i].style.display = "none";
    showDate(CdateBoxs[0]);

    function showDate(obj) {
        if (!obj) return;
        obj.style.display = "inline";
        box.timeHandle = window.setTimeout(function () { showDate(obj.nextSibling); }, 1);
    }
    this.show = show
}
function SwitchMenu(theClass) {
    var alldivTags = document.getElementsByTagName("div");
    for (i = 0; i < alldivTags.length; i++) {
        if (alldivTags[i].className == theClass) {
            if (alldivTags[i].style.display == 'none') {
                alldivTags[i].style.display = 'block';
            } else {
                alldivTags[i].style.display = 'none';
            }
        }
    }
}
var CurrentHotScreen = 0;

function setHotQueryList(screen) {
    var Vmotion = "forward";
    var MaxScreen = piccount;
    if (screen >= MaxScreen) {
        screen = 0;
        Vmotion = "reverse";
    }
    cleanallstyle();
    if (document.getElementById("focus_" + screen) != null) {

        document.getElementById("focus_" + screen).className = "up";
        if (hot_query_td != null && null != hot_query_td.filters) {
            hot_query_td.filters[0].apply();
            hot_query_td.filters[0].motion = Vmotion;
        }
        for (i = 0; i < MaxScreen; i++) {
            document.getElementById("switch_" + i).style.display = "none";
        }
        document.getElementById("switch_" + screen).style.display = "block";
        if (hot_query_td != null && null != hot_query_td.filters) {
            hot_query_td.filters[0].play();
        }
        CurrentHotScreen = screen;
    }

}

function refreshHotQuery() {
    refreshHotQueryTimer = null;
    setHotQueryList(CurrentHotScreen + 1);
    refreshHotQueryTimer = setTimeout('refreshHotQuery();', 5000);
}
function _delmodel(a) {
    msg = "确认不显示此模块吗?";
    if (window.confirm(msg)) {
        window.location.href = 'MyDeskDel.aspx?ModelName=' + a;
    }
}
function cleanallstyle() {
    for (i = 0; i < piccount; i++) {
        if (document.getElementById("focus_" + i) != null) {
            document.getElementById("focus_" + i).className = "";
        }

    }
}

function show_focus_image(index) {
    clearTimeout(refreshHotQueryTimer);
    setHotQueryList(index);
    refreshHotQueryTimer = setTimeout('refreshHotQuery();', 5000);
}

var refreshHotQueryTimer = null;
var hot_query_td = document.getElementById('hotsearchlist');
setHotQueryList(CurrentHotScreen);
refreshHotQueryTimer = setTimeout('refreshHotQuery();', 5000);