var G_tempa;
function CheckValuePiece() {
    if (window.document.form1.GoPage.value == "") {
        alert("请输入跳转的页码！");
        window.document.form1.GoPage.focus();
        return false;
    }
    return true;
}
function CheckAll() {
    if (G_tempa == 1) {
        for (var i = 0; i < window.document.form1.elements.length; i++) {
            var e = form1.elements[i];
            e.checked = false;
        }
        G_tempa = 0;
    }
    else {
        for (var i = 0; i < window.document.form1.elements.length; i++) {
            var e = form1.elements[i];
            e.checked = true;
        }
        G_tempa = 1;
    }
}
function CheckDel() {
    var number = 0;
    for (var i = 0; i < window.document.form1.elements.length; i++) {
        var e = form1.elements[i];
        if (e.Name != "CheckBoxAll") {
            if (e.checked == true) {
                number = number + 1;
            }
        }
    }
    if (number == 0) {
        alert("请选择需要删除的项！");
        return false;
    }
    if (window.confirm("你确认删除吗？")) {
        return true;
    }
    else {
        return false;
    }
}
function CheckModify() {
    var Modifynumber = 0;
    for (var i = 0; i < window.document.form1.elements.length; i++) {
        var e = form1.elements[i];
        if (e.Name != "CheckBoxAll") {
            if (e.checked == true) {
                Modifynumber = Modifynumber + 1;
            }
        }
    }
    if (Modifynumber == 0) {
        alert("请至少选择一项！");
        return false;
    }
    if (Modifynumber > 1) {
        alert("只允许选择一项！");
        return false;
    }

    return true;
}