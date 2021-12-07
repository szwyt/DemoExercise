

//表单提交操作
function doPostBack(action) {
    var from = $("form:first");
    if (action != "") {
        from.attr("action", action);
    }
    from.submit();
};

$(function () {
    var oldPageSize = 15;
    var oldPageNumber = 1;
    /*页面变量初始化*/
    oldPageSize = $("#pageSize").val();
    oldPageNumber = $("#pageNumber").val();


    //表格排序
    $(".dataList table thead tr th[name='sortTitle']").click(function () {
        $("#sortColumn").val($(this).attr("column"));
        $("#sortDirection").val($(this).attr("direction"));
        doPostBack("");
    });

    //排序提示
    var sortColumn = $("#sortColumn").val();
    if (sortColumn != "") {
        var sortDirection = $("#sortDirection").val();
        var flagHtml = "";
        if (sortDirection == "ASC") {
            flagHtml = "<b>↑</b>";
            sortDirection = "DESC";
        }
        else {
            flagHtml = "<b>↓</b>";
            sortDirection = "ASC";
        }
        $(".dataList th[name='sortTitle'][column='" + sortColumn + "']").attr("direction", sortDirection).append(flagHtml);
    }

    var oldDisplayOrder = 0;
    //修改排序值
    $(".sortinput").focus(function () {
        var sortInputObj = $(this);
        oldDisplayOrder = sortInputObj.val();
        sortInputObj.val("").attr("class", "selectedsortinput");
    });
    $(".sortinput").blur(function () {
        var sortInputObj = $(this);
        if (sortInputObj.val() == "") {
            sortInputObj.val(oldDisplayOrder)
        }
        else {
            var reg = /^-?\d+$/;
            if (!reg.test(sortInputObj.val())) {
                sortInputObj.val(oldDisplayOrder).attr("class", "selectedsortinput");
                alert("只能输入数字！")
                return;
            }
            else {
                if (oldDisplayOrder != sortInputObj.val()) {
                    $.jBox.tip("正在更新...", 'loading');
                    $.get(sortInputObj.attr("url") + "&displayOrder=" + sortInputObj.val(), function (data, textStatus) {
                        if (data != "0") {
                            $.jBox.tip('更新成功！', 'success');
                        } else {
                            sortInputObj.val(oldDisplayOrder);
                            $.jBox.error('更新失败，请联系管理员！', '更新失败');
                        }
                    });
                }
            }
        }
        sortInputObj.attr("class", "unselectedsortinput");
    });

    //页数按钮
    $(".dataList .pagination  .bt").click(function () {      
        $("#pageNumber").val($(this).attr("page"));
        doPostBack("");     
        return false;
    });

    //每页显示条数输入框
    $("#pageSize").focus(function () {
        $(this).val("");
    });
    $("#pageSize").blur(function () {
        var value = $(this).val();
        if (value == "") {
            $(this).val(oldPageSize);
        }
        else {
            var regex = /^\d+$/;
            if (!regex.test(value)) {
                alert("只能输入数字!");
                $(this).val(oldPageSize);
            }
            else if (parseInt(value) != oldPageSize) {
                doPostBack("");
            }
        }
    });

    //跳转到指定页输入框
    $("#pageNumber").focus(function () {
        $(this).val("");
    });
    $("#pageNumber").blur(function () {
        var value = $(this).val();
        if (value == "") {
            $(this).val(oldPageNumber);
        }
        else {
            var regex = /^\d+$/;
            if (!regex.test(value)) {
                alert("只能输入数字!");
                $(this).val(oldPageNumber);
            }
            else {
                var totalPages = $(this).attr("totalPages");
                if (parseInt(value) > parseInt(totalPages)) {
                    alert("跳转页数不能大于" + totalPages);
                    $(this).val(oldPageNumber);
                }
                else if (parseInt(value) != oldPageNumber) {
                    doPostBack("");
                }
            }

        }
    });

});