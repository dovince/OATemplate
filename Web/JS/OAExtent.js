//////
//用于存放一些额外的方法
/////

//用$.blockUI来做遮挡,然后检测session，如果有CheckOver_DFFN的话就执行语句$.unblockUI,要有jqblockUI
function CheckExcelExportOver() {
    $.ajax({
        type: "post",
        async: false,
        data: { SessionName: "CheckOver_DFFN", NeedClear: "1" },
        url: "../Main/CheckSession.aspx"
    });
    $.blockUI({
        message: '<h3><img src="../images/Loading.gif"/>正在导出数据，请稍等···</h3>',
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#0066CC',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            width: '15%',
            opacity: .6,
            color: 'white'
        }
    });
    CheckIsExportOver();
}

function CheckIsExportOver() {
    $.ajax({
        type: "post",
        data: { SessionName: "CheckOver_DFFN", NeedClear: "1" },
        url: "../Main/CheckSession.aspx",
        success: function (response) {
            if (response == "true") {
                $.unblockUI();
            }
            else {
                setTimeout(CheckIsExportOver, 500);
            }
        },
        error: function () {
            $.unblockUI();
        }
    });
}