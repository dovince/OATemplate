//获取指定选择器下的所有EasyUI组件，如果输入空，则获取当前的所有EasyUI组件。
function getEasyUIAssembly(selector) {
    var mainSelector = selector ? $("[class^='easyui-']", selector) : $("[class^='easyui-']");
    var result = [];

    mainSelector.each(function (index, item) {
        var type = $(item).attr("class").split("easyui-")[1];
        type = type ? type.split(" ")[0] : "";

        if (type) {
            result.push({ "Assembly": item, "AssemblyName": type });
        }
    });

    return result;
}

//创建Tab新标签
function addTab(tabID, subtitle, url, icon) {
    if (!icon)
        icon = "icon-standard-application";

    if (!$("#" + tabID).tabs("exists", subtitle)) {
        $("#" + tabID).tabs("add", {
            title: subtitle,
            content: createFrame(url),
            closable: true,
            refreshable: false,
            iconCls: icon
        });
    } else {
        $("#" + tabID).tabs("select", subtitle);
    }
}

//创建Tab新标签
function addTabHref(tabID, subtitle, url, icon) {
    url = $("#indexURL").val() + "?path=" + url;

    if (!icon)
        icon = "icon-save";

    if (!$("#" + tabID).tabs("exists", subtitle)) {
        $("#" + tabID).tabs("add", {
            title: subtitle,
            content: createFrame(url),
            closable: true,
            refreshable: false,
            iconCls: icon
        });
    } else {
        $("#" + tabID).tabs("select", subtitle);
    }
}

function createFrame(url) {
    var s = "<iframe name='mainFrame' frameborder='0'  src='" + url + "' style='width:100%;height:100%;'></iframe>";
    return s;
}

//删除多条数据数据
function delDatas(obj) {
    var accessObject = {
        GuidCode: "",
        URL: ""
    };

    accessObject = $.extend({}, accessObject, obj);

    $.easyui.messager.confirm("系统提示", "您确认要删除数据吗？", function (selector) {
        if (selector) {
            $.post(accessObject.URL, JSON.parse(JSON.stringify(accessObject)), function (result) {
                accessObject.CallBack(result);
            });
        }
    });
}

//显示模态窗口处理数据
function showDialog(dataObject) {
    var baseDialogObject = {
        title: dataObject.WindowTitle,
        closed: false,
        onLoad: null,
        cache: false,
        collapsible: false,
        maximizable: false,
        minimizable: false,
        resizable: false,
        minimizable: false,
        enableSaveButton: false,
        enableApplyButton: false,
        enableCloseButton: false,
        autoCloseOnEsc: true,
        autoRestore: true,
        buttons: null,
        toolbar: null
    };

    //窗体基础参数设置部分
    var windowDivObject = $(dataObject.WindowID);
    var windowOptions = $.extend({}, baseDialogObject, eval("({" + windowDivObject.data("options") + "})"), dataObject);

    //按钮部分生成
    var buttons = $("div", dataObject.WindowID).children();
    var btnType = $("div", dataObject.WindowID).prop("id");

    if (btnType == "toolbar")
        windowOptions.toolbar = [];
    else
        windowOptions.buttons = [];

    for (var i = 0; i < buttons.length; i++) {
        if ($(buttons[i]).prop("tagName") == "SPAN") {
            windowOptions.toolbar.push("-");
            continue;
        }

        if (!$(buttons[i]).data("options"))
            continue;

        var op = getElementOption(buttons[i]);

        if (btnType == "toolbar")
            windowOptions.toolbar.push(op);
        else
            windowOptions.buttons.push(op);
    }

    //释放原先的窗体
    //closeWindow(dataObject.Dialog);

    //初始化需要打开的窗体
    windowOptions.data = dataObject;

    var result = null;
    if (windowOptions.topMost) {
        window.top.TopData = dataObject;
        window.top.TopWindow = $.easyui.showDialog(windowOptions);
        result = window.top.TopWindow;
    } else {
        window.TopData = dataObject;
        window.TopWindow = $.easyui.showDialog(windowOptions);
        result = window.TopWindow;
    }

    return result;
}

//导出数据成Excel
function exportDataToExcel(opt) {
    var exportColNames = "";
    var cols = $("#" + opt.GridID).parent().find(" .datagrid-header-row td:visible");

    $(cols).each(function (index, item) {
        var name = $(item).attr("field");

        if (name && name != "ck" && name != "action")
            exportColNames += name + (cols.length - 1 == index ? "" : ",");
    });

    var exportParams = {
        URL: opt.URL,
        SearchParams: opt.searchParamsObj,
        ExportColumns: exportColNames
    };

    jqueryDownloadFile(exportParams);
}

function getControlButton(obj) {
    var baseObject = {
        btnAttr: { plain: true, iconCls: "icon-edit", text: "" },
        clickEvent: ""
    };

    baseObject = $.extend({}, baseObject, obj);
    var btn = $("<a></a>").linkbutton(baseObject.btnAttr).attr("onclick", baseObject.clickEvent);
    var div = $("<div></div>").append(btn);

    return div.html();
}

//替换验证控件的样式为原始样式
function getOriginalClass() {
    $("[required]").each(function (index, item) {
        var baseClass = $(this).attr("class").split(" ")[0];
        $(this).attr("class", baseClass);
    });
}

//Grid分页
function pagerFilter(data) {
    if (typeof data.length == "number" && typeof data.splice == "function") {    // is array
        data = {
            total: data.length,
            rows: data
        }
    }
    var dg = $(this);
    var opts = dg.datagrid("options");
    var pager = dg.datagrid("getPager");
    pager.pagination({
        onSelectPage: function (pageNum, pageSize) {
            opts.pageNumber = pageNum;
            opts.pageSize = pageSize;
            pager.pagination("refresh", {
                pageNumber: pageNum,
                pageSize: pageSize
            });
            dg.datagrid("loadData", data);
        }
    });
    if (!data.originalRows) {
        data.originalRows = (data.rows);
    }
    var start = (opts.pageNumber - 1) * parseInt(opts.pageSize);
    var end = start + parseInt(opts.pageSize);
    data.rows = (data.originalRows.slice(start, end));
    return data;
}

//列合并
function mergeGridColCells(grid, rowFildName) {
    var rows = grid.datagrid('getRows');
    var startIndex = 0;
    var endIndex = 0;

    if (rows.length < 1) {
        return;
    }

    $.each(rows, function (i, row) {
        if (row[rowFildName] == rows[startIndex][rowFildName]) {
            endIndex = i;
        }
        else {
            grid.datagrid('mergeCells', {
                index: startIndex,
                field: rowFildName,
                rowspan: endIndex - startIndex + 1
            });
            startIndex = i;
            endIndex = i;
        }

    });
    grid.datagrid('mergeCells', {
        index: startIndex,
        field: rowFildName,
        rowspan: endIndex - startIndex + 1
    });
}

function getHTMLOption(elementById) {
    var id = elementById;
    if (/[^#]/.test(elementById))
        id = elementById.substr(1);

    return eval("({" + $("#" + id).data("options") + "})");
}

function getElementOption(element) {
    return eval("({" + $(element).data("options") + "})");
}

function checkGridRow(changeGridID, row, checkRowColName, isChcekID, checkType) {
    if ($(isChcekID).val() == "") {
        var rows = $(changeGridID).datagrid("getData").rows;

        if (checkType)
            $(changeGridID).datagrid("appendRow", row);
        else {
            for (var i = 0; i < rows.length; i++)
                if (rows[i][checkRowColName] == row[checkRowColName])
                    $(changeGridID).datagrid("deleteRow", i);
        }
    }
}

function getDatatableOperator(PageModel, MenuID) {
    var tools = {
        toolbar: null,
        operator: [],
    }
    $.ajax({
        url: "../SystemManage/SystemManageFunction.ashx?funName=GetMenuBtn&MenuID=" + MenuID,
        type: "POST",
        async: false,
        contentType: "application/json",
        data: JSON.stringify({ "MenuID": MenuID }),
        success: function (result) {
            result = eval('(' + result + ')');
            if (result.status == "success") {
                var btns = result.dataoption
                for (var i = 0; i < btns.length; i++) {
                    if (btns[i].FUNC_NAME == "创建") {
                        tools.toolbar = '<a href="javascript:void(0);" class="easyui-linkbutton" style="margin-top: 1px;" data-options="iconCls:\'' + btns[i].ICONIC + '\',plain:true" onclick="' + PageModel + '.' + btns[i].EVENT_NAME + '()">' + btns[i].FUNC_NAME + '</a>'
                    } else {
                        if (PageModel != null) {
                            tools.operator.push({
                                btnAttr: { plain: true, iconCls: btns[i].ICONIC, text: btns[i].FUNC_NAME },
                                clickEvent: PageModel + "." + btns[i].EVENT_NAME + "('{0}')"
                            })
                        } else {
                            tools.operator.push({
                                btnAttr: { plain: true, iconCls: btns[i].ICONIC, text: btns[i].FUNC_NAME },
                                clickEvent: btns[i].EVENT_NAME + "('{0}')"
                            })
                        }
                    }
                }

            }
        },
        error: function (msg) {

        }

    })
    return tools;
}

function ValueInList(u, l) {
    for (var i = 0; i < l.length; i++) {
        if (l[i] == u) {
            return true;
        }
    }
    return false;
}

var ERROR_MSG_WINDOWTITLE = "操作错误";