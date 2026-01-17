$(document).ready(function () {
    if (document.getElementById('ShenPiUser') != null) {
        document.getElementById('ShenPiUser').readOnly = true;
        document.getElementById('ShenPiUser').style.color = "#1b83d8";
    }
    initTransformButton();
    $(".btnOriginalForm").click(function () {
        var title = $(this).text();
        $(".lui_panel_content_inside.rebuildform .lui_form_title_frame").each(function (e) {
            if ($(this).parent(".lui-component").is(":hidden")) {
                $(this).parent(".lui-component").show();
            }
            else {
                $(this).parent(".lui-component").hide();
                title = $(this).attr("data-title");
            }
        });
        $(".btnOriginalForm").text(title);
    });
    $('#win_yinzhang').window({
        onBeforeClose: function () {
            var returnVal = "";
            for (var i = 0; i < window.length; i++) {
                if (window[i].frameElement.id == "yinzhang") {
                    //根据弹出窗内部的ifame的id来定位
                    returnVal = window[i].returnValue;
                }
            }
            if (returnVal == null || returnVal == "" || imgids == null) { }
            else {
                var src = "../UploadFile/" + returnVal;
                if (imgids.id == "yinzhangplaceholder") {//点击快捷签名:1,签名自身;2,签名表单
                    var yImg = document.getElementById(imgids.id);
                    if (yImg != null) {
                        yImg.src = src;
                        yImg.style.display = "block";
                    }


                    var SignInput = $("#Hidden_SignInput").val();
                    if (SignInput != "") {
                        if (SignInput.indexOf(",")) {//全部通过可向下流转
                            var list = SignInput.split(',');
                            for (var i = 0; i < list.length; i++) {
                                var item = list[i];
                                if (item == '') continue;
                                var sImg = document.getElementById(item);
                                var sImg1 = document.getElementById("Hidden_SignImg");
                                if (sImg != null && typeof (sImg.src) != "undefined"
                                    && sImg.src.indexOf("InsertYinZhang.gif") > 0) {
                                    sImg.src = src;
                                    if (sImg1 != null) {
                                        sImg1.value = returnVal;
                                    }
                                    break;
                                }
                            }
                        }
                        else {//一人通过可向下流转
                            var sImg = document.getElementById(SignInput);
                            var sImg1 = document.getElementById("Hidden_SignImg");
                            if (sImg != null) {
                                sImg.src = src;
                                if (sImg1 != null) {
                                    sImg1.value = returnVal;
                                }
                            }
                        }
                    }
                }
                else {//点击表单签名:1,签名表单自身;2,快捷签名显示;
                    var sImg = document.getElementById(imgids.id);
                    if (sImg != null) {
                        sImg.src = src;
                    }

                    var yImg = document.getElementById("yinzhangplaceholder");
                    if (yImg != null) {
                        yImg.src = src;
                        yImg.style.display = "block";
                    }
                }
            }
        }
    });
    $('#win_user').window({
        onBeforeClose: function () {
            var returnVal = "";
            for (var i = 0; i < window.length; i++) {
                if (window[i].frameElement.id == "user") {
                    //根据弹出窗内部的ifame的id来定位
                    returnVal = window[i].returnValue;
                }
            }
            if (usertypeid != "" && returnVal != "") {
                var idstr = "#" + usertypeid;
                $(idstr)[0].value = returnVal;
            }
        }
    });

});
function initTransformButton() {
    $(".lui_panel_content_inside .lui_form_title_frame").each(function (e) {
        if ($(this).parent(".lui-component").is(":hidden")) {
            $(".btnOriginalForm").text($(this).attr("data-title"));
            return false;
        }
    });
}
function _change(op) {
    var text = op.value;
    if (text != "请选择") {
        document.getElementById('fdUsageContent').value += text + "\r\n";
    }
    else {
        document.getElementById('fdUsageContent').value = "";
    }
}
var imgids = null;
function selectyinzhang(imgidstr) {
    imgids = imgidstr;
    var RadNum = Math.random();
    var url = "../Main/SelectYinZhang.aspx";
    $("#yinzhang")[0].src = url + '?Radstr=' + RadNum;

    var scrtop = $(window).scrollTop();
    $('#win_yinzhang').show();
    $('#win_yinzhang').showWinInMiddle();
}
var usertypeid = null;
function openuserDialog(utype) {
    //防止缓存之前的页面
    usertypeid = utype;
    var RadNum = Math.random();
    var url = "../Main/SelectUser.aspx?TableName=ERPUser&LieName=UserName";
    $("#user")[0].src = url + '&Radstr=' + RadNum;
    $("#win_user").show();
    $('#win_user').showWinInMiddle();
}



//批量设置字段的可写与保密属性
function AddValue(obj, value, type) {
    if (type == "inputtext") {
        var oval = $(obj).attr("value");
        if (oval != value) {
            $(obj).attr("value", value);
        }
    }
    else if (type == "textarea") {
        var oval = obj.innerHTML;
        if (oval != value) {
            $(obj).html(value);
        }
    }
}

//由于部分浏览器的value不会写在html中，所以只能强制写入
function AddValues() {
    var inputs = $("#RebuildForm_Label_FormContent input");
    $.each(inputs, function () {
        AddValue(this, this.value, "inputtext");
    });
    var textareas = $("#RebuildForm_Label_FormContent textarea");
    $.each(textareas, function () {
        AddValue(this, this.value, "textarea");
    });
    var inputradios = $("#RebuildForm_Label_FormContent input[type='radio']");
    $.each(inputradios, function () {
        if ($(this).is(":checked")) {
            $(this).attr("checked", "checked");
        }
        else {
            $(this).removeAttr("checked");
        }
    });
    var inputSelects = $("#RebuildForm_Label_FormContent select");
    $.each(inputSelects, function (i, inputs) {
        var options = $(inputs).find("option");
        $.each(options, function (j, option) {
            if ($(option).is(":checked")) {
                $(option).attr("selected", "selected");
            }
            else {
                $(option).removeAttr("selected");
            }
        })
    });
}

function Load_Do() {
    AddValues();
    setTimeout("Load_Do()", 100);
}

//自动签名+填写审批意见到表单
function submit_client() {
    //验证密码
    if (validate()) {
        //AddValues2LabelContent();
        $("#Hidden_form").val($("#RebuildForm_Label_FormContent").prop("innerHTML"));
        showLoading();
        // 添加页面加载完成的事件处理函数
        window.onload = function () {
            hideLoading();
        };

        document.getElementById('btnSavePass').click();
        return true;
    } else {
        return false;
    }
}
function submit_client_end() {
    //验证密码
    if (validate_end()) {
        //AddValues2LabelContent();
        $("#Hidden_form").val($("#RebuildForm_Label_FormContent").prop("innerHTML"));
        showLoading();
        // 添加页面加载完成的事件处理函数
        window.onload = function () {
            hideLoading();
        };
        document.getElementById('btnSaveEnd').click();
        return true;
    } else {
        return false;
    }
}
function validate() {
    var SignInput = $("#Hidden_SignInput").val();
    //if ($("#yinzhangplaceholder").attr("src") == "") {
    //    window.alert("请选择签名或印章！");
    //    return false;
    //}

    if (SignInput != null && SignInput != "") {
        var signImg = document.getElementById(SignInput);
        if (signImg) {
            yzimg = document.getElementById(SignInput).src;
            var defaultimg = "InsertYinZhang.gif";
            if (String(yzimg).indexOf(defaultimg) > 0) {
                var btnSign = $("div[name='process_body'][data-bind='processflow'] div[onclick*='selectyinzhang(yinzhangplaceholder);']");
                if (btnSign.length > 0) {
                    $(btnSign).click();
                }
                messageShowTimeout({ msg: "请选择签名或印章！" });
                return false;
            }
        }
    }
    var content = $("#RebuildForm_Label_FormContent").prop("innerHTML");
    if (typeof (content) == "undefined" || content == "") {
        window.alert("表单出错,请刷新重试！");
    }
    var ShenPiUser = $("#ShenPiUser").val();
    if (ShenPiUser == "" || typeof (ShenPiUser) == "undefined") {
        window.alert("请选择下一个节点审批人！");
        return false;
    }
    return true;
}
function validate_end() {
    var SignInput = $("#Hidden_SignInput").val();
    if (SignInput != null && SignInput != "") {
        var signImg = document.getElementById(SignInput);
        if (signImg) {
            yzimg = document.getElementById(SignInput).src;
            var defaultimg = "InsertYinZhang.gif";
            if (String(yzimg).indexOf(defaultimg) > 0) {
                messageShowTimeout({ msg: "请选择签名或印章！" });
                return false;
            }
        }
    }
    var content = $("#RebuildForm_Label_FormContent").prop("innerHTML");
    if (typeof (content) == "undefined" || content == "") {
        window.alert("表单出错,请刷新重试！");
    }
    return true;
}
function validateReject() {
    var content = document.getElementById('fdUsageContent').value;
    if (content == "") {
        alert("驳回或者不通过的时候必须填入审批意见!");
        return false;
    }
    return true;
}

//添加到账金额行
function addnewtrdzje() {
    //防止缓存之前的页面
    var RadNum = Math.random();
    var url = "../BusinessManage/HTSK_DZJEList.html";
    var KQJE = $("#Text1368591578")[0].value || "";
    var HTID = $("#Text1492949059")[0].value || "";
    var NWorkToDoID = getUrlParms("ID");
    var scrollTop = document.documentElement.scrollTop == 0 ? $("body").scrollTop() : document.documentElement.scrollTop;
    $("#dzje")[0].src = url + '?Radstr=' + RadNum + '&KQJE=' + KQJE + '&NWorkToDoID=' + NWorkToDoID + '&HTID=' + HTID;
    $('#win_dzje').show();
    $('#win_dzje').showWinInMiddle();
}
function viewenddzje() {//view end dzje
    //防止缓存之前的页面
    var url = "../BusinessManage/HTSK_DZJEList.html";
    var KQJE = $("#Text1368591578")[0].value || "";
    var HTID = $("#Text1492949059")[0].value || "";
    var NWorkToDoID = getUrlParms("ID");
    var scrollTop = document.documentElement.scrollTop == 0 ? $("body").scrollTop() : document.documentElement.scrollTop;
    $("#dzje")[0].src = url + '?KQJE=' + KQJE + '&NWorkToDoID=' + NWorkToDoID + '&HTID=' + HTID;
    $('#win_dzje').show();
    $('#win_dzje').showWinInMiddle();
}
function refreshdzje() {
    $.ajax({
        type: "get",
        async: false,
        cache: false,
        contentTYPE: "application/text/html;charset = utf-8",
        data: { flag: "InitHTDZJE", NWorkToDoID: getUrlParms("ID"), DZJE: "", Date: GetTime() },
        dataType: "json",
        url: "../Main/GetJsonResultHandler.ashx",
        success: function (data) {
            if (data != null && data.data != null) {
                //删除旧表单的到账金额数据
                var deletecount = $("#Text681200471").parents("tr:first").nextAll().length - 1;
                for (var i = 0; i < deletecount; i++) {
                    $("#Text681200471").parents("tr:first").next().remove()
                }

                //在表单塞入到账金额
                $("#Text681200471").parents("tr:first").after(data.data.HTMLCode);

            }
        }, complete: function () {

        }, error: function (errorMsg) {
            alert("网络错误，请联系管理员！");
        }
    });
}

//获取时间
function GetTime() {
    var Nows = new Date();
    return Nows.getFullYear() +
        "-" +
        ((Nows.getMonth() + 1) < 10 ? "0" + (Nows.getMonth() + 1) : (Nows.getMonth() + 1)) +
        "-" +
        (Nows.getDate() < 10 ? "0" + Nows.getDate() : Nows.getDate());
}

function CheckModify() {
    if (window.confirm("确认要修改表单中的内容吗？")) {
        return true;
    }
    else {
        return false;
    }
}