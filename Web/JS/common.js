$(document).ready(function () {
    initSelectDateInput();
});
function initSelectDateInput() {
    $.each($('.input_cxcalendar'), function (i, item) {
        var id = $(item).attr("id");
        if (typeof (Calendar) == 'function') {
            var a = new Calendar({
                targetCls: $(this),
                type: 'yyyy-mm-dd',
                wday: 2
            }, function (val) {
                $("#" + id).trigger("change");
            });
        }
    });
    $.each($('.input_cxcalendar'), function (i, item) {
        var id = $(item).attr("id");
        if (typeof (document.getElementById(id)) != "undefined") {
            document.getElementById(id).readOnly = true;
            document.getElementById(id).style.font = "14px Arial";
            document.getElementById(id).style.color = "#1b83d8";
        }
    });
}


//获取地址的参数
function getUrlParms(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) {
        return unescape(r[2]);
        return null;
    }
}
function getRootPath_web() {
    //获取当前网址，如： http://localhost:8083/uimcardprj/share/meun.jsp
    var curWwwPath = window.document.location.href;
    //获取主机地址之后的目录，如： uimcardprj/share/meun.jsp
    var pathName = window.document.location.pathname;
    var pos = curWwwPath.indexOf(pathName);
    //获取主机地址，如： http://localhost:8083
    var localhostPaht = curWwwPath.substring(0, pos);
    //获取带"/"的项目名，如：/uimcardprj
    var projectName = pathName.substring(0, pathName.substr(1).indexOf('/') + 1);
    return (localhostPaht + projectName);
}

function DoWorkBack() {
    var param = getUrlParms("backtype");
    if (param != null && param != "" && param == "NowWorkFlow") {
        window.location.href = 'NowWorkFlow.aspx';
    }
    var ctype = getUrlParms("ctype");
    if (ctype != null && ctype != "") {
        switch (ctype) {
            case "yiban":
                window.location.href = 'YiBanWork.aspx';
                break;
            case "daiban":
                window.location.href = 'NowWorkFlow.aspx';
                break;
            case "CommonQuery":
                window.location.href = '../BusinessManage/CommonQuery.aspx?TypeStr=合同收款台账';
                break;
            default:
                break;
        }
    } else {
        if (self != top) {
            window.history.go(-1);
        }
        else {
            window.history.go(-1);
        }
    }
}

function SimpleCheckBox(selected) {
    var count = 0;
    isdb = false;    window.setTimeout(cc, 300);    function cc() {
        if (isdb)            return;        //var checkboxSelectd = $(selected).children().get(0).children[0];        var checkboxSelectd = $(selected).children().eq(0).find("input[type='checkbox']").get(0);        $(selected).toggleClass("active_table");        var trs = $(selected).siblings();        $.each(trs, function (i, item) {
            if (hasClass(selected, "active_table")) {
                count += 1;
            }
        });        if (count > 2) {
            $.each(trs, function (i, item) {
                if (item != selected) {
                    removeClass(item, "active_table");
                }
            });
        }
        if (hasClass(selected, "active_table")) {
            if (checkboxSelectd) {
                checkboxSelectd.checked = true;
            }

        } else {
            if (checkboxSelectd) {
                checkboxSelectd.checked = false;
            }

        }        var checkboxList = [];        var parentDOM = $(selected).parents("form")[0];        $.each(parentDOM, function (i, item) {
            if (item.id.indexOf("_CheckSelect") != -1) {
                checkboxList.push(item);
            }
        });        if ($(checkboxSelectd).attr("checked")) {
            $.each(checkboxList, function (i, item) {
                item.checked = false;
            });            $(checkboxSelectd).attr("checked", "checked");
        }

        //处理新的使用了supertable的列表
        if (checkboxSelectd) {
            var id = checkboxSelectd.id;
            if ($(selected).parents(".sData").length > 0 && $(selected).parents(".sData").siblings(".sFData").children().children().children("tbody").children("tr").length > 0) {
                $($(selected).parents(".sData").siblings(".sFData").children().children().children("tbody").children("tr")).each(function () {
                    if ($(this)[0].children[0].children[0] && $(this)[0].children[0].children[0].id == id && checkboxSelectd.checked == true) {
                        $(this)[0].children[0].children[0].checked = true;
                        $($(this)[0]).toggleClass("active_table");
                    } else {
                        if ($(this)[0].children[0].children[0]) {
                            $(this)[0].children[0].children[0].checked = false;
                        }
                        removeClass($(this)[0], "active_table");
                    }
                });
            }

            if ($(selected).parents(".sFData").length > 0 && $(selected).parents(".sFData").siblings(".sData").children().children("tbody").children("tr").length > 0) {
                $($(selected).parents(".sFData").siblings(".sData").children().children("tbody").children("tr")).each(function () {
                    if ($(this)[0].children[0].children[0] && $(this)[0].children[0].children[0].id == id && checkboxSelectd.checked == true) {
                        $(this)[0].children[0].children[0].checked = true;
                        $($(this)[0]).toggleClass("active_table");
                    } else {
                        if ($(this)[0].children[0].children[0]) {
                            $(this)[0].children[0].children[0].checked = false;
                        }
                        removeClass($(this)[0], "active_table");
                    }
                });
            }
        }
        //$(".sFDataInner #" + id).attr("checked", "checked");
    }
}

function MuliCheckBox(selected) {
    isdb = true;
    //var checkboxSelectd = $(selected).children().get(0).children[0];    var checkboxSelectd = $(selected).children().eq(0).find("input[type='checkbox']").get(0);

    $(selected).toggleClass("active_table");
    if (hasClass(selected, "active_table")) {
        checkboxSelectd.checked = true;
    } else {
        checkboxSelectd.checked = false;
    }

    //处理新的使用了supertable的列表
    if (checkboxSelectd) {
        var id = checkboxSelectd.id;
        if ($(selected).parents(".sData").length > 0 && $(selected).parents(".sData").siblings(".sFData").children().children().children("tbody").children("tr").length > 0) {
            $($(selected).parents(".sData").siblings(".sFData").children().children().children("tbody").children("tr")).each(function () {
                if ($(this)[0].children[0].children[0] && $(this)[0].children[0].children[0].id == id && checkboxSelectd.checked == true) {
                    $(this)[0].children[0].children[0].checked = true;
                    $($(this)[0]).addClass("active_table");
                    //$($(this)[0]).toggleClass("active_table");
                } else {
                    removeClass($(this)[0], "active_table");
                }
            });

            for (var i = 0; i < $(selected).parents(".sData").siblings(".sFData").children().children().children("tbody").children("tr").length; i++) {
                var tr = $(selected).parents(".sData").siblings(".sFData").children().children().children("tbody").children("tr")[i];
                var checked = tr.children[0].children[0].checked;
                if (checked) {
                    $(tr).addClass("active_table");
                }
            }
        }

        if ($(selected).parents(".sFData").length > 0 && $(selected).parents(".sFData").siblings(".sData").children().children("tbody").children("tr").length > 0) {
            $($(selected).parents(".sFData").siblings(".sData").children().children("tbody").children("tr")).each(function () {
                if ($(this)[0].children[0].children[0].id == id && checkboxSelectd.checked == true) {
                    $(this)[0].children[0].children[0].checked = true;
                    $($(this)[0]).addClass("active_table");
                } else {
                    removeClass($(this)[0], "active_table");
                }
            });

            for (var i = 0; i < $(selected).parents(".sFData").siblings(".sData").children().children("tbody").children("tr").length; i++) {
                var tr = $(selected).parents(".sFData").siblings(".sData").children().children("tbody").children("tr")[i];
                var checked = tr.children[0].children[0].checked;
                if (checked) {
                    $(tr).addClass("active_table");
                }
            }
        }

    }
}

function hasClass(element, cls) {
    return (' ' + element.className + ' ').indexOf(' ' + cls + ' ') > -1;
}

function removeClass(element, cls) {
    if (hasClass(element, cls)) {
        element.className = element.className.replace(new RegExp("(\\s|^)" + cls + "(\\s|$)"), " ");
    }
}

//加载中事件
//需要 引入<script src="../JS/jquery.blockUI.js"></script>
//需要 引入<link href="../CSS/Loading.css" rel="stylesheet" />
function blockUI(options) {
    options = $.extend(true, {}, options);
    if (options.target) { // element blocking
        var el = $(options.target);
        if (el.height() <= ($(window).height())) {
            options.cenrerY = true;
        }
    }
    if (typeof ($.blockUI) == 'function') {
        $.blockUI({
            message: '<div class="loading-message loading-message-boxed"><img src="../images/loading-spinner-grey.gif" align="center"><span>&nbsp;&nbsp;' + (options.message ? options.message : '加载中...') + '</span></div>',
            target: options.target,
            baseZ: 99999,
            centerY: options.cenrerY !== undefined ? options.cenrerY : false,
            css: {
                top: '50%',
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
}

function addTab_parent(href, title) {
    try {
        var tabs = parent.Ext.getCmp("TabPanelID");
        if (tabs != null) {
            for (var i = 0; i < tabs.items.length; i++) {
                if (tabs.items.items[i].title.replace(" ", "") == title) {
                    //Ext.Msg.alert("消息","该菜单项[ " + node.attributes.text + " ]已经存在Tab里面！");
                    parent.Ext.get('workflow_' + title).dom.src = href
                    tabs.activate(tabs.items.items[i]);//如果存在，直接激活
                    return;
                }
            }
            var tabsAdd = tabs.add({
                title: title,
                html: "<iframe id='workflow_" + title + "' scrolling='true' width='100%' height='100%'  frameborder='0' src='" + href + "'></iframe>",
                closable: true
            })
            tabs.activate(tabsAdd);
        }
    } catch (e) {
        try {
            //该方法在Main_New.aspx
            parent.addTab(title, href);
        }
        catch (e) {
            //alert(e.message);
            window.location.href = href;
        }
    }
}

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

/** 
* 将数值格式化成金额形式 
* 
* @param num 数值(Number或者String) 
* @param precision 精度，默认不变
* @param separator 分隔符，默认为逗号
* @return 金额格式的字符串,如'1,234,567'，默认返回NaN
* @type String 
*/
function formatNumber(num, precision, separator) {
    var parts;
    // 判断是否为数字
    if (!isNaN(parseFloat(num)) && isFinite(num)) {
        // 把类似 .5, 5. 之类的数据转化成0.5, 5, 为数据精度处理做准, 至于为什么
        // 不在判断中直接写 if (!isNaN(num = parseFloat(num)) && isFinite(num))
        // 是因为parseFloat有一个奇怪的精度问题, 比如 parseFloat(12312312.1234567119)
        // 的值变成了 12312312.123456713
        num = Number(num);
        // 处理小数点位数
        num = (typeof precision !== 'undefined' ? (Math.round(num * Math.pow(10, precision)) / Math.pow(10, precision)).toFixed(precision) : num).toString();
        // 分离数字的小数部分和整数部分
        parts = num.split('.');
        // 整数部分加[separator]分隔, 借用一个著名的正则表达式
        parts[0] = parts[0].toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1' + (separator || ','));

        return parts.join('.');
    }
    return NaN;
}

String.prototype.replaceAll = function (a, b) {
    var reg = new RegExp(a, "g");
    return this.replace(reg, b);
}

/*2014年9月19日11:11:07 By 王美建*/
function ajaxUpload(opt) {
    /*
        参数说明:
        opt.id : 页面里file控件的ID;
        opt.frameName : iframe的name值;
        opt.url : 文件要提交到的地址;
        opt.callBack : 上传成功后回调;
    */
    var iName = opt.frameName; //太长了，变短点
    var iframe, form, file, fileParent;
    //创建iframe和form表单
    iframe = $('<iframe style="width:0;height:0;" name="' + iName + '" />');
    form = $('<form method="post" style="display:none;" target="' + iName + '" action="' + opt.url + '"  name="form_' + iName + '" enctype="multipart/form-data" />');
    file = $('#' + opt.id); //通过id获取flie控件
    fileParent = file.parent(); //存父级
    file.appendTo(form);
    //插入body
    $(document.body).append(iframe).append(form);
    //取得所选文件的扩展名
    form.submit();//格式通过验证后提交表单;

    //文件提交完后
    iframe.load(function () {
        var data = $(this).contents().find('body').html();
        file.appendTo(fileParent);
        iframe.remove();
        form.remove();
        opt.callBack(data);
    })
}

function changeZhongbiaoTongzhishu(e, htbh) {
    var lbl = $(e);
    var msg = "确认将合同[" + htbh + "]的中标通知书改为'是',变更后将不能修改.";
    if (confirm(msg)) {
        $.ajax({
            cache: false,
            type: "get",
            data: { flag: "ChangeZhongbiaoTongzhishu", htbh: htbh },
            dataType: "json",
            url: "../Main/GetJsonResultHandler.ashx",
            success: function (data) {
                if (data != null && !data.Code) {
                    alert("变更失败.");
                }
                else if (data.Code) {
                    lbl.text(data.Message);
                    lbl.attr("onclick", "javascript:void(0);");
                    lbl.css("text-decoration", "none");
                    lbl.css("color", "black");
                    alert("变更成功.");
                }
            }
        });
    }
}
//标准货币格式转换：带千分号、并且小数点后保留两位
function regexNum(str) {
    var regex = /(\d)(?=(\d\d\d)+(?!\d))/g;

    if (str.indexOf(".") == -1) {

        str = str.replace(regex, ',') + '.00';
        return str;
    } else {
        var newStr = str.split('.');
        var str_2 = newStr[0].replace(regex, ',');

        if (newStr[1].length <= 1) {
            //小数点后只有一位时
            str_2 = str_2 + '.' + newStr[1] + '0';
            return str_2;

        } else if (newStr[1].length > 1) {
            //小数点后两位以上时
            var decimals = newStr[1].substr(0, 2);
            var srt_3 = str_2 + '.' + decimals;
            return srt_3;
        }
    }
};

function formatmoney(op) {
    $(op).val(regexNum($(op).val()));
}

Number.prototype.numberFormat = function (c, d, t) {
    var n = this,
        c = isNaN(c = Math.abs(c)) ? 2 : c,
        d = d == undefined ? "." : d,
        t = t == undefined ? "," : t,
        s = n < 0 ? "-" : "",
        i = String(parseInt(n = Math.abs(Number(n) || 0).toFixed(c))),
        j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};

/**  
* 实时动态强制更改用户录入  
* arg1 inputObject  
**/
function amount(th) {
    var regStrs = [
        ['^0(\\d+)$', '$1'], //禁止录入整数部分两位以上，但首位为0  
        ['[^\\d\\.]+$', ''], //禁止录入任何非数字和点  
        ['\\.(\\d?)\\.+', '.$1'], //禁止录入两个以上的点  
        ['^(\\d+\\.\\d{2}).+', '$1'] //禁止录入小数点后两位以上  
    ];
    th.value = th.value.split(',').join('');
    for (var i = 0; i < regStrs.length; i++) {
        var reg = new RegExp(regStrs[i][0]);
        th.value = th.value.replace(reg, regStrs[i][1]);
    }
}

/**  
* 录入完成后，输入模式失去焦点后对录入进行判断并强制更改，并对小数点进行0补全  
* arg1 inputObject  
**/
function overFormat(th) {
    var v = th.value.split(',').join('');
    if (v === '') {
        v = '0.00';
    } else if (v === '0') {
        v = '0.00';
    } else if (v === '0.') {
        v = '0.00';
    } else if (/^0+\d+\.?\d*.*$/.test(v)) {
        v = v.replace(/^0+(\d+\.?\d*).*$/, '$1');
        v = inp.getRightPriceFormat(v).val;
    } else if (/^0\.\d$/.test(v)) {
        v = v + '0';
    } else if (!/^\d+\.\d{2}$/.test(v)) {
        if (/^\d+\.\d{2}.+/.test(v)) {
            v = v.replace(/^(\d+\.\d{2}).*$/, '$1');
        } else if (/^\d+$/.test(v)) {
            v = v + '.00';
        } else if (/^\d+\.$/.test(v)) {
            v = v + '00';
        } else if (/^\d+\.\d$/.test(v)) {
            v = v + '0';
        } else if (/^[^\d]+\d+\.?\d*$/.test(v)) {
            v = v.replace(/^[^\d]+(\d+\.?\d*)$/, '$1');
        } else if (/\d+/.test(v)) {
            v = v.replace(/^[^\d]*(\d+\.?\d*).*$/, '$1');
            ty = false;
        } else if (/^0+\d+\.?\d*$/.test(v)) {
            v = v.replace(/^0+(\d+\.?\d*)$/, '$1');
            ty = false;
        } else {
            v = '0.00';
        }
    }
    th.value = v;
}
/**  
* 录入完成后，输入模式失去焦点后对录入进行判断并强制更改，并对小数点进行0补全  
* arg1 inputObject  
**/
function overFormatInt(th) {
    var v = th.value.split(',').join('');
    if (v === '') {
        v = '0';
    } else if (v === '0') {
        v = '0';
    } else if (v === '0.') {
        v = '0';
    } else if (/^0+\d+\.?\d*.*$/.test(v)) {
        v = v.replace(/^0+(\d+\.?\d*).*$/, '$1');
        v = inp.getRightPriceFormat(v).val;
    } else if (/^0\.\d$/.test(v)) {
        v = v + '0';
    } else if (!/^\d+\.\d{2}$/.test(v)) {
        if (/^\d+\.\d{2}.+/.test(v)) {
            v = v.replace(/^(\d+\.\d{2}).*$/, '$1');
        } else if (/^\d+$/.test(v)) {
            v = v + '';
        } else if (/^\d+\.$/.test(v)) {
            v = v + '';
        } else if (/^\d+\.\d$/.test(v)) {
            v = v + '0';
        } else if (/^[^\d]+\d+\.?\d*$/.test(v)) {
            v = v.replace(/^[^\d]+(\d+\.?\d*)$/, '$1');
        } else if (/\d+/.test(v)) {
            v = v.replace(/^[^\d]*(\d+\.?\d*).*$/, '$1');
            ty = false;
        } else if (/^0+\d+\.?\d*$/.test(v)) {
            v = v.replace(/^0+(\d+\.?\d*)$/, '$1');
            ty = false;
        } else {
            v = '0';
        }
    }
    th.value = v;
}

function goback(postbackcount) {
    //showLoading();
    if (window.frameElement == null) {
        if (window.parent.opener != null) {
            window.location.href = "about:blank";
            window.opener = null;
            window.open('', '_self');
            window.close();
        }
        else {
            if (typeof postbackcount != "undefined") {
                window.history.go(-(postbackcount + 1));
            }
            else {
                window.history.go(-1);
            }
        }
    }
    else {
        if (typeof (window.frameElement.src) != "undefined") {
            if (window.name == "viewFrame") {
                window.parent.history.go(-1);
                //var refUrl = $(window.parent.document).find("[name='viewFrame']").attr("data-linktolist");
                //if (typeof (refUrl) != "undefined" && refUrl != '')
                //    window.parent.location.href = refUrl;
                //else
                //    window.parent.history.go(-1);
            }
            else {
                if (document.referrer.indexOf("Action=Edit") > 0) {
                    gobacktolist();
                }
                else {
                    if (typeof postbackcount != "undefined") {
                        window.history.go(-(postbackcount + 1));
                    }
                    else {
                        window.history.go(-1);
                    }
                }
            }
        }
        else {
            if (typeof postbackcount != "undefined") {
                window.history.go(-(postbackcount + 1));
            }
            else {
                window.history.go(-1);
            }
        }
    }
}
function gobacktolist() {
    //showLoading();
    if (window.frameElement == null) {
        if (window.parent.opener != null) {
            window.location.href = "about:blank";
            window.opener = null;
            window.open('', '_self');
            window.close();
        }
        else {
            window.history.go(-1);
        }
    }
    else {
        if (typeof (window.frameElement.src) != "undefined") {
            if (window.name == "viewFrame") {
                window.parent.history.go(-1);
                var refUrl = $(window.parent.document).find("[name='viewFrame']").attr("data-linktolist");
                if (typeof (refUrl) != "undefined" && refUrl != '')
                    window.parent.location.href = refUrl;
                else
                    window.parent.history.go(-1);
            }
            else {
                if (window.location.href == window.frameElement.src) {
                    window.parent.closeTab();
                }
                else {
                    window.frameElement.src = window.frameElement.src;
                }
            }
        }
        else {
            window.history.go(-1);
        }
    }
}
function redirectUrl(url) {
    if (typeof (url) != "undefined" && url != '') {
        var refurl = document.referrer;
        var cururl = "";
        var list = url.split('/');
        for (var i = 0; i < list.length; i++) {
            var item = list[i];
            if (item == null || item == '') continue;
            if (cururl == "" && item.indexOf('aspx') > 0) {
                var li = item.split('?');
                for (var j = 0; j < li.length; j++) {
                    var ol = li[j];
                    if (ol.indexOf('aspx') > 0) {
                        cururl = ol;
                        break;
                    }
                }
            }
            if (cururl != '')
                break;
        }
        if (refurl.indexOf(cururl) > 0 || cururl.indexOf("FormChangeSP.aspx") > 0
            || (url.indexOf("DoWorkNew.aspx") > 0 && refurl.indexOf("Action=Edit") > 0)
            || (refurl.indexOf("DoWorkNew.aspx") > 0 && url.indexOf("Action=Edit") > 0)) {
            if (window.frameElement != null) {
                window.frameElement.src = window.frameElement.src;
            }
            else {
                if (window.parent.opener != null) {
                    window.location.href = "about:blank";
                    window.opener = null;
                    window.open('', '_self');
                    window.close();
                }
                else {
                    window.location.href = url;
                }
            }
        }
        else {
            window.location.href = url;
        }
    }
}

function showMessage(message) {
    hideMessage();
    var html = '<div style="color: red;font-size: 20px;font-weight: bold;"><font>' + message + '</font></div>';
    $("#lui_validate_message").append(html);
    //window.setTimeout(hideMessage, 1000*5)
}
function showSuccessMessage(message) {
    hideMessage();
    var html = '<div style="color: green;font-size: 20px;font-weight: bold;"><font>' + message + '</font></div>';
    $("#lui_validate_message").append(html);
}
function hideMessage() {
    $("#lui_validate_message").html("");
}
var timeout;
function showLoading() {
    blockUI({ target: '#TabPanelID' });
    timeout = setTimeout("hideLoading()", 5 * 1000);
}
function hideLoading() {
    if (typeof ($.unblockUI) == 'function') {
        $.unblockUI();
    }
    if (timeout) {
        clearTimeout(timeout);
    }
}
function disabledSubmitBtn(options) {
    options = options || {};
    this.title = options.title || "提交";
    this.clickjs = options.clickjs || "void(0)";
    var submitBtns = $(".lui-component[title='" + this.title + "']");
    for (var i = 0; i < submitBtns.length; i++) {
        var item = submitBtns.eq(i);
        if (item.parents("div").attr("class").indexOf("lui_toolbar_btn") >= 0) {//顶部提交按钮
            item.removeClass("lui_toolbar_btn_on");
            item.find(".lui-component").removeClass("lui_widget_btn_txt");
        }
        item.removeClass("lui_widget_btn");
        item.removeAttr("onclick");
        item.addClass("lui_widget_btn_disabled");
        item.attr("onclick", "javascript:" + this.clickjs + ";");
    }
}
function enabledSubmitBtn(options) {
    options = options || {};
    this.title = options.title || "提交";
    this.clickjs = options.clickjs || "checkSubmitBtn(this)";
    var submitBtns = $(".lui-component[title='" + this.title + "']");
    for (var i = 0; i < submitBtns.length; i++) {
        var item = submitBtns.eq(i);
        item.removeClass("lui_widget_btn_disabled");
        item.addClass("lui_widget_btn");
        if (item.parents("div").attr("class").indexOf("lui_toolbar_btn") >= 0) {//顶部提交按钮
            item.addClass("lui_toolbar_btn_on");
            item.find(".lui-component").addClass("lui_widget_btn_txt");
        }
        item.attr("onclick", "javascript:" + this.clickjs + ";");
    }
}
function showUserPop(id, options) {
    showLoading();
    var RadNum = Math.random();
    options = options || {};
    this.title = options.title || "选择用户";
    this.url = options.url || "../Main/SelectUser.aspx?TableName=ERPUser&LieName=UserName" + '&Radstr=' + RadNum;
    this.width = parseInt(options.width || 350);
    this.height = parseInt(options.height || 500);
    callbackFunc = options.onFinish || null;

    var pop = $("#win_user.easyui-window");
    if (pop.length > 0) {
        var parentwin = $(pop).parents(".panel.window");
        if (parentwin.length > 0) {
            var parentwinnext = $(parentwin).next(".window-shadow");
            if (parentwinnext.length > 0) {
                parentwinnext.remove();
            }
            parentwin.remove();
        }
    }

    var popwin = $('<div id="win_user" class="easyui-window" style="width: ' + this.width + 'px; height: ' + this.height + 'px; padding: 5px; display: none;"></div>');
    popwin.attr("data-options", "title:'" + this.title + "',iconCls:'icon-search',closed:true,closable:false,minimizable:false,maximizable:false,collapsible:false,resizable:true");
    var frame = $('<iframe id="user" frameborder="0" src="" style="width: 100%; height: 100%;" onload="javascript:hideLoading();"></iframe>');
    frame.attr('src', this.url);
    frame.appendTo(popwin);
    popwin.appendTo("body");

    $('#win_user').window({
        onBeforeClose: function () {
            var returnVal = "";
            for (var i = 0; i < window.length; i++) {
                if (window[i].frameElement.id == "user") {
                    //根据弹出窗内部的ifame的id来定位
                    returnVal = window[i].returnValue;
                    break;
                }
            }
            if (returnVal != "") {
                id && $("#" + id).val(returnVal);
                callbackFunc && callbackFunc(returnVal);
            }
        }
    });
    $('#win_user').showWinInMiddle();
    $('#win_user').show();
}
function showUserPopwindow(id, url) {
    showUserPop(id, { url: url });
}
function showDeptPopwindow(id, url) {
    showLoading();
    if (typeof (url) == "undefined") {
        url = "../Main/SelectDanWei.aspx?TableName=ERPUser&LieName=UserName";
    }
    var RadNum = Math.random();
    url = url + '&_=' + RadNum;

    var pop = $("#win_bumen.easyui-window");
    if (pop.length > 0) {
        var parentwin = $(pop).parents(".panel.window");
        if (parentwin.length > 0) {
            var parentwinnext = $(parentwin).next(".window-shadow");
            if (parentwinnext.length > 0) {
                parentwinnext.remove();
            }
            parentwin.remove();
        }
    }

    var popwin = $('<div id="win_bumen" class="easyui-window" style="width: 350px; height: 400px; padding: 5px; display: none;"></div>');
    popwin.attr("data-options", "title:'选择部门',iconCls:'icon-search',closed:true,closable:false,minimizable:false,maximizable:false,collapsible:false,resizable:true");
    var frame = $('<iframe id="bumen" frameborder="0" src="" style="width: 100%; height: 100%;" onload="javascript:hideLoading();"></iframe>');
    frame.attr('src', url);
    frame.appendTo(popwin);
    popwin.appendTo("body");

    $('#win_bumen').window({
        onBeforeClose: function () {
            var returnVal = "";
            for (var i = 0; i < window.length; i++) {
                if (window[i].frameElement.id == "bumen") {
                    //根据弹出窗内部的ifame的id来定位
                    returnVal = window[i].returnValue;
                }
            }
            if (returnVal != "") {
                $('#' + id)[0].value = returnVal;
            }
        }
    });
    $('#win_bumen').showWinInMiddle();
    $('#win_bumen').show();
}
var callbackFunc;
function showPopwindow(id, options) {
    showLoading();
    options = options || {};
    this.title = options.title || "选择选项";
    this.url = options.url || "";
    this.width = parseInt(options.width || 600);
    this.height = parseInt(options.height || 500);
    var dataToSend = options.data || null;
    callbackFunc = options.onFinish || null;

    var pop = $("#win_select.easyui-window");
    if (pop.length > 0) {
        var parentwin = $(pop).parents(".panel.window");
        if (parentwin.length > 0) {
            var parentwinnext = $(parentwin).next(".window-shadow");
            if (parentwinnext.length > 0) {
                parentwinnext.remove();
            }
            parentwin.remove();
        }
    }

    var popwin = $('<div id="win_select" class="easyui-window" style="width: ' + this.width + 'px; height: ' + this.height + 'px; padding: 5px; display: none;"></div>');
    popwin.attr("data-options", "title:'" + this.title + "',iconCls:'icon-search',closed:true,closable:true,minimizable:false,maximizable:false,collapsible:false,resizable:true");
    var frame = $('<iframe id="select" frameborder="0" src="" style="width: 100%; height: 100%;"></iframe>');
    frame.attr('src', this.url + "&_=" + Math.random());
    frame.appendTo(popwin);
    popwin.appendTo("body");

    // iframe 加载后发送数据
    frame.on("load", function () {
        hideLoading();
        if (dataToSend) {
            var tempJson = {
                type: "INIT_DATA",
                payload: dataToSend
            }
            this.contentWindow.postMessage(JSON.stringify(tempJson), "*"); // 如果要限制来源，可以替换 * 为指定 origin
        }
    });

    $('#win_select').window({
        onBeforeClose: function () {
            var returnVal = "";
            for (var i = 0; i < window.length; i++) {
                if (window[i].frameElement.id == "select") {
                    //根据弹出窗内部的ifame的id来定位
                    var tempval = window[i].returnValue;
                    if (tempval) {
                        returnVal = tempval;
                    }
                    break;
                }
            }
            if (returnVal != "" && id) {
                id && $("#" + id).val(returnVal);
            }
            callbackFunc && callbackFunc(returnVal);
        }
    });
    $('#win_select').showWinInMiddle();
    $('#win_select').show();
}

function checkEasyUI() {
    var headerHTML = null;
    for (var i = 0; i < document.childNodes.length; i++) {
        if (headerHTML == null && document.childNodes[i].tagName == "HTML") {
            for (var j = 0; j < document.childNodes[i].childNodes.length; j++) {
                if (document.childNodes[i].childNodes[j].tagName == "HEAD") {
                    headerHTML = document.childNodes[i].childNodes[j].innerHTML;
                    break;
                }
            }
        }
    }
    if (headerHTML == null) {
        headerHTML = document.documentElement.innerHTML;
    }
    if (headerHTML.indexOf('easyui.css') > 0 && headerHTML.indexOf('jquery.easyui') > 0) {
        return true;
    }
    else {
        return false;
    }
}

function messageShowTimeout(options) {
    options = options || {};
    this.title = options.title || "提示";
    this.msg = options.msg || "出错了";
    this.width = parseInt(options.width || 300);
    this.height = parseInt(options.height || 150);
    this.seconds = parseInt(options.seconds || 1500);
    this.redtUrl = options.url || "";
    this.closeFuncEx = options.closeFuncEx || null;

    if (checkEasyUI()) {
        var timeout = this.seconds; // 5秒
        var interval = 1000; // 1秒
        var redUrl = this.redtUrl;
        var cfuncex = this.closeFuncEx;
        if (this.msg != "" && (this.msg.indexOf("成功") >= 0 || this.msg.length <= 10)) {
            var win = $.messager.show({
                title: this.title + "(" + timeout / 1000 + "秒)",
                width: this.width,
                height: this.height,
                msg: this.msg,
                timeout: timeout,
                showType: "show",
                style: {}
            });
            var timer = setInterval(function () {
                timeout -= interval; // 减少时间
                if (timeout <= 0) {
                    if (redUrl != '') {
                        redirectUrl(redUrl);
                    }
                    win.window({
                        onClose: function () {
                            // 在窗口关闭后执行的代码
                            if (cfuncex != null) {
                                cfuncex();
                            }
                        }
                    });
                    clearInterval(timer); // 清除定时器
                } else {
                    var seconds = Math.floor(timeout / 1000); // 计算剩余秒数
                    win.window("setTitle", this.title + "(" + seconds + "秒)"); // 修改窗口标题
                }
            }, interval);
        }
        else {
            $.messager.defaults.ok = "确定";
            $.messager.alert({
                modal: true,
                Ok: "确定",
                title: this.title,
                msg: this.msg,
                showType: "info",
                onBeforeClose: function () {
                    //在窗口关闭后执行的代码
                    if (redUrl != '') {
                        redirectUrl(redUrl);
                    }
                    if (cfuncex != null) {
                        cfuncex();
                    }
                }
            });
            //$.messager.show({
            //    title: this.title,
            //    width: this.width,
            //    height: this.height,
            //    msg: this.msg,
            //    timeout: 0,
            //    showType: "show",
            //    style: {},
            //    onClose: function () {
            //        // 在窗口关闭后执行的代码
            //        if (redUrl != '') {
            //            redirectUrl(redUrl);
            //        }
            //        if (cfuncex != null) {
            //            cfuncex();
            //        }
            //    }
            //});
        }
    }
    else {
        alert(msg);
        if (this.redtUrl != "") {
            redirectUrl(this.redtUrl);
        }
    }

}

function alertMessage(msg) {
    messageShowTimeout({ msg: msg });
}
function alertMessageAndRedirect(msg, url) {
    messageShowTimeout({ msg: msg, url: url });
}
function AsyncShowAndForceRedirect(msg, url) {
    messageShowTimeout({ msg: msg, url: url });
}
function alertMessageAndParentRedirect(msg, url) {
    messageShowTimeout({
        msg: msg, closeFuncEx: function () {
            parent.window.location.href = url;
        }
    });
}
$.fn.extend({
    showWinInMiddle: function () {
        var id = this.prop("id");
        var top = $(document).scrollTop() + ($(window).height() - $('#' + id).height()) * 0.5 - $(window).height() / 20;
        $('#' + id).window('open').window('resize',
            {
                top: top < 0 ? 43 : top,
                left: ($(window).width() - $('#' + id).width()) * 0.5
            });
    }
});

function getRadioButtonListCheckedText(id) {
    var rbltable = document.getElementById(id);
    var rbs = rbltable.getElementsByTagName("input");
    var result = "";
    for (var i = 0; i < rbs.length; i++) {
        if (rbs[i].checked) {
            result = rbs[i].value;
            break;
        }
    }
    return result;
}
//由于部分浏览器的value不会写在html中，所以只能强制写入
function AddValues2Label3() {
    AddValues("Label3");
}
function AddValues2LabelContent() {
    AddValues("RebuildForm_Label_FormContent");
}
function AddValues(containerid) {
    var inputs = $("#" + containerid + " input");
    $.each(inputs, function () {
        AddValue(this, this.value, "inputtext");
    });
    var textareas = $("#" + containerid + " textarea");
    $.each(textareas, function () {
        AddValue(this, this.value, "textarea");
    });
    var inputradios = $("#" + containerid + " input[type='radio']");
    $.each(inputradios, function () {
        if ($(this).is(":checked")) {
            $(this).attr("checked", "checked");
        }
        else {
            $(this).removeAttr("checked");
        }
    });
}
//批量设置字段的可写与保密属性
function AddValue(obj, value, type) {
    if (type == "inputtext") {
        console.log("obj.outerHTML:" + obj.outerHTML != null && obj.outerHTML != "");
        console.log("obj.outerHTML.indexOf() < 0:" + obj.outerHTML.indexOf("value=") < 0);
        if (obj.outerHTML != null && obj.outerHTML != "") {
            if (obj.outerHTML.indexOf("value=") < 0) {
                $(obj).attr("value", value);
            }
            else {
                $(obj).attr("value", value);
            }
        }
    }
    else if (type == "textarea") {
        $(obj).html(value);
    }
}
function MakeRequestAjax(options) {
    options = options || {};
    this.cache = options.cache || false;
    this.async = options.async || true;
    this.url = options.url || "";
    this.dataType = options.dataType || "text";
    this.contentType = options.contentType || "application/x-www-form-urlencoded;charset=utf-8";
    this.data = options.data || {};
    this.type = options.type || "get";
    this.beforeSend = options.beforeSend || function (XMLHttpRequest) { };
    this.success = options.success || function (data, textStatus, XMLHttpRequest) { };
    this.error = options.error || function (XMLHttpRequest, textStatus, errorThrown) { };
    this.complete = options.complete || function (XMLHttpRequest, textStatus) { };
    $.ajax({
        cache: this.cache,
        async: this.async,
        type: this.type,
        data: this.data,
        dataType: this.dataType,
        url: this.url,
        beforeSend: this.beforeSend,
        success: this.success,
        error: this.error,
        complete: this.complete
    });
}
window.console = window.console || (function () {
    var c = {};
    c.log = c.warn = c.debug = c.info = c.error = c.time = c.dir = c.profile = c.clear = c.exception = c.trace = c.assert = function () { };
    return c;
})();


// 字符串转Base64（兼容IE8）
function base64encode(str) {
    if (typeof btoa === "function") {
        // 非IE8浏览器
        return btoa(unescape(encodeURIComponent(str)));
    } else {
        // IE8浏览器
        var base64 = "";
        var keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
        var input = unescape(encodeURIComponent(str));
        var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
        var i = 0;

        do {
            chr1 = input.charCodeAt(i++);
            chr2 = input.charCodeAt(i++);
            chr3 = input.charCodeAt(i++);

            enc1 = chr1 >> 2;
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
            enc4 = chr3 & 63;

            if (isNaN(chr2)) {
                enc3 = enc4 = 64;
            } else if (isNaN(chr3)) {
                enc4 = 64;
            }

            base64 +=
                keyStr.charAt(enc1) +
                keyStr.charAt(enc2) +
                keyStr.charAt(enc3) +
                keyStr.charAt(enc4);
        } while (i < input.length);

        return base64;
    }
}

// Base64转字符串（兼容IE8）
function base64decode(base64) {
    if (typeof atob === "function") {
        // 非IE8浏览器
        return decodeURIComponent(escape(atob(base64)));
    } else {
        // IE8浏览器
        var keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
        var output = "";
        var chr1, chr2, chr3;
        var enc1, enc2, enc3, enc4;
        var i = 0;

        base64 = base64.replace(/[^A-Za-z0-9\+\/\=]/g, "");

        do {
            enc1 = keyStr.indexOf(base64.charAt(i++));
            enc2 = keyStr.indexOf(base64.charAt(i++));
            enc3 = keyStr.indexOf(base64.charAt(i++));
            enc4 = keyStr.indexOf(base64.charAt(i++));

            chr1 = (enc1 << 2) | (enc2 >> 4);
            chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
            chr3 = ((enc3 & 3) << 6) | enc4;

            output += String.fromCharCode(chr1);

            if (enc3 !== 64) {
                output += String.fromCharCode(chr2);
            }
            if (enc4 !== 64) {
                output += String.fromCharCode(chr3);
            }
        } while (i < base64.length);

        return decodeURIComponent(escape(output));
    }
}
function integBizSysAPIBaseUrl() {
    return "http://202.105.18.110:8053";
}
function servicesBaseUrl() {
    return "../Services/Services.ashx";
}

