$(document).ready(function () {
    $(".lui_tabpage_float_collapse").click(function () {
        if ($(".lui_tabpage_float_navs_mark").is(":hidden")) {
            $(this).removeClass("lui_tabpage_collapsed");
            $(this).removeClass("lui_tabpage_uncollapse");
            $(this).attr("title", "收缩");
            $(".lui_tabpage_float_collapse .txt").text("收缩");
            $(".lui_tabpage_float_navs").fadeIn("slow");
            $(".lui_tabpage_float_navs_mark").fadeIn("slow");
        }
        else {
            //$(this).addClass("lui_tabpage_collapsed");
            $(this).addClass("lui_tabpage_uncollapse");
            $(this).attr("title", "展开");
            $(".lui_tabpage_float_collapse .txt").text("展开");
            $(".lui_tabpage_float_navs").fadeOut("slow");
            $(".lui_tabpage_float_navs_mark").fadeOut("slow");
        }
    });

    $(".lui_tabpage_float_nav_item").click(function () {
        var name = $(this).find(".lui_tabpage_float_nav_item_c").text();
        if ($(this).hasClass("selected")) {
            $(".lui_tabpage_float_content").each(function (e) {
                if ($(this).find(".lui_tabpage_float_header_text").text() == name) {
                    var sTop = $(this).offset().top;
                    if (name == "基本信息") {
                        sTop = 0;
                    }
                    $('html, body').animate({
                        scrollTop: sTop
                    }, 100);
                }
            });
        }
        else {
            $(this).addClass("selected");
            $(".lui_tabpage_float_content").each(function (e) {
                if ($(this).find(".lui_tabpage_float_header_text").text() == name) {
                    if ($(this).find(".lui_tabpage_float_content_l").is(":hidden")) {
                        $(this).find(".lui_tabpage_float_content_l").fadeIn("slow");
                    }
                    var sTop = $(this).offset().top;
                    if (name == "基本信息") {
                        sTop = 0;
                    }
                    $('html, body').animate({
                        scrollTop: sTop
                    }, 100);
                }
            });
        }
    });
    $(".lui_tabpage_float_header_close").click(function () {
        var name = $(this).prev().text();
        if ($(this).hasClass("lui_tabpage_float_header_open")) {
            $(this).removeClass("lui_tabpage_float_header_open");
            $(this).attr("title", "最小化");
            $(this).parents(".lui_tabpage_float_content").find(".lui_tabpage_float_content_l").fadeIn("slow");
            $(".lui_tabpage_float_nav_item").each(function (e) {
                if ($(this).find(".lui_tabpage_float_nav_item_c").text() == name) {
                    $(this).addClass("selected");
                }
            });
        }
        else {
            $(this).addClass("lui_tabpage_float_header_open");
            $(this).attr("title", "最大化");
            $(this).parents(".lui_tabpage_float_content").find(".lui_tabpage_float_content_l").fadeOut("slow");
            $(".lui_tabpage_float_nav_item").each(function (e) {
                if ($(this).find(".lui_tabpage_float_nav_item_c").text() == name) {
                    $(this).removeClass("selected");
                }
            });
        }
    });
    $("#process_review_tabcontent .lui_flowstate_tab_heading .lui_flowstate_tabhead li").click(function () {
        $(".lui_flowstate_tab_heading .lui_flowstate_tabhead li").each(function (e) {
            $(this).removeClass("active");
            $(this).attr("data-isclick", "false");
        });
        $("#process_review_tabcontent div[name='process_body']").each(function (e) {
            $(this).removeClass("process_body_checked_true").addClass("process_body_checked_false");
        });
        $(this).addClass("active");
        $(this).attr("data-isclick", "true");
        var databind = $(this).attr("data-bind");
        $("#process_review_tabcontent div[name='process_body'][data-bind='" + databind + "']").removeClass("process_body_checked_false").addClass("process_body_checked_true");
        var loadIframe = $("#process_review_tabcontent div[name='process_body'][data-bind='" + databind + "'] iframe");
        if (typeof (loadIframe) != "undefined" && loadIframe.length > 0 && loadIframe.attr("src") == "") {
            loadIframe.attr("src", loadIframe.attr("data-src"));
        }
    });
    $('.com_gototop').click(function () {
        $('html,body').animate({ scrollTop: 0 }, 'fast');
    });
    $(window).scroll(function () {
        if ($(this).scrollTop() > 1) {//当window的scrolltop距离大于1时，go to top按钮淡出，反之淡入
            $(".com_gototop").fadeIn();
        } else {
            $(".com_gototop").fadeOut();
        }
    });
});

function SetTabcontentActive(index) {
    var selectedItem = null;
    $("#process_review_tabcontent .lui_flowstate_tab_heading .lui_flowstate_tabhead li").each(function (e) {
        if (selectedItem==null && e == index) {
            selectedItem = $(this);
        }
    });
    if (selectedItem != null) {
        $(".lui_flowstate_tab_heading .lui_flowstate_tabhead li").each(function (e) {
            $(this).removeClass("active");
            $(this).attr("data-isclick", "false");
        });
        $("#process_review_tabcontent div[name='process_body']").each(function (e) {
            $(this).removeClass("process_body_checked_true").addClass("process_body_checked_false");
        });
        $(selectedItem).addClass("active");
        $(selectedItem).attr("data-isclick", "true");
        var databind = $(selectedItem).attr("data-bind");
        $("#process_review_tabcontent div[name='process_body'][data-bind='" + databind + "']").removeClass("process_body_checked_false").addClass("process_body_checked_true");
        var loadIframe = $("#process_review_tabcontent div[name='process_body'][data-bind='" + databind + "'] iframe");
        if (typeof (loadIframe) != "undefined" && loadIframe.length > 0 && loadIframe.attr("src") == "") {
            loadIframe.attr("src", loadIframe.attr("data-src"));
        }
    }
}

//$(document).ready(function () {
//    $("li[name='process_head_tab']").click(function () {
//        $("li[name='process_head_tab']").attr("class", "");
//        $(this).attr("class", "active");

//        //兼容有些模块下无法触发onresize事件的问题
//        var isClick = $(this).attr("data-isClick");
//        var dataLoad = $(this).attr("data-load");
//        if (!isClick) {
//            $(this).attr("data-isClick", "true");
//            if (dataLoad) {
//                lbpm[dataLoad]();
//            }
//        }

//        $("div[name='process_body']").attr("class", "process_body_checked_false");
//        var lis = $(this).parent().children();
//        for (var i = 0; i < lis.length; i++) {
//            var classValue = $(lis[i]).attr("class");
//            if (classValue === "active") {
//                var process_bodys = $("div[name='process_body']");
//                $(process_bodys[i]).attr("class", "process_body_checked_true");
//            }
//        }
//    });
//});
