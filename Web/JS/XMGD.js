
//项目归档 
function XMGD(DAH) {
    var RadNum = Math.random();
    var returnVal = window.showModalDialog('../ProjectManage/XMGuiDang.aspx?DAH=' + DAH + '&Radstr=' + RadNum, '', 'dialogWidth:380px;DialogHeight=330px;status:no;help:no;resizable:yes;');
    if (returnVal == "true") {
        location.reload();
    } 
}

//归档催办 
function XMGDCuiBan(ID) {
    $.ajax({
        cache: false,
        type: "get",
        async:false,
        //contentType:'application/x-www-form-urlencoded;charset=UTF-8',
        data: { 
            flag: "cuiban", 
            ID: ID},
        url: "../ProjectManage/XMGDCuiBanHandler.ashx",
        success: function (data) {
                alert(data);
        },
        error:function(){
            alert("网络错误！");
        },
        complete:function(){
            location.reload();
        }
    });
}


//归档信息删除
function DelXMGD(ID, NworkID) {
    if (window.confirm("你确认删除吗？")) {
        $.ajax({
            cache: false,
            type: "get",
            async: false,
            //contentType:'application/x-www-form-urlencoded;charset=UTF-8',
            data: {
                flag: "delxmgd",
                ID: ID,
                NworkID: NworkID,
            },
            url: "../ProjectManage/XMGDCuiBanHandler.ashx",
            success: function (data) {
                alert(data);
            },
            error: function () {
                alert("网络错误！");
            },
            complete: function () {
                location.reload();
            }
        });
    }
    else {
    }
   
}

