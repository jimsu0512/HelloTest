$(function () {
   

    var NIsValid = [
        { key: 1, value: '有效' },
        { key: 0, value: '无效' }
    ];
    

    //加载表格
    $('#newsInfoGrid').datagrid({
        url: '/NewsInfo/SearchInfo',
        singleSelect: true,
        queryParams: { name: '' },
        pagination: true,
        pageSize: 2,//每页显示的记录条数，默认为10 
        pageList: [2, 10, 100],//可以设置每页记录条数的列表 
        columns: [[
            { field: 'NId', title: '编号', width: 50, align: 'right' },
            { field: 'NType', title: '新闻类型', width: 100, align: 'right' },
            { field: 'NTitle', title: '标题', width: 100, align: 'right' },
            { field: 'NContent', title: '内容', width: 100, align: 'right' },
            { field: 'NCreateTime', title: '创建时间', width: 120, align: 'left'},
            { field: 'NUpdateTime', title: '更新时间', width: 120, align: 'left' },
            {
                field: 'NIsValid', title: '是否有效', width: 80, align: 'left', formatter: function (value, row) {
                    if (value == 1) {
                        return "有效";
                    } else if (value == 0) {
                        return "无效";
                    }
                    return value;
                },
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'key',
                        textField: 'value',
                        panelHeight: 60,
                        data: NIsValid,
                        required: true
                    }
                }
            }
        ]],
        onLoadSuccess: function (row) {
        },
        onEndEdit: function (index, row) {
            //Ajax操作
            $.ajax({
                url: '/NewsInfo/AddEdit',
                type: 'POST',
                dataType: "json",
                data: row,
                success: function (data) {
                    if (data != 'true' && data != true) {
                        $.messager.alert("操作失败", '操作失败');
                    } else {
                        $.messager.alert("操作成功", '操作成功');
                    }
                },
                error: function (data) {
                    $.messager.alert("操作异常", '操作异常');
                }
            });
        },
        onBeforeEdit: function (index, row) {
            row.editing = true;
            $(this).datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $(this).datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $(this).datagrid('refreshRow', index);
        }
    });
    //设置分页控件 
    var p = $('#newsInfoGrid').datagrid('getPager');

    $(p).pagination({
        beforePageText: '第',//页数文本框前显示的汉字 
        afterPageText: '页    共 {pages} 页',
        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
    });

    //form提交
    $("#newsInfoForm").form({
        url: '/NewsInfo/AddEdit',
        onSubmit: function () {
            //验证表单验证是否成功
            var isValid = $("#newsInfoForm").form('validate');
            if (!isValid) {
                removeload();
            }
            return isValid;
        },
        success: function (data) {
            removeload();
            if (data != 'true' && data != true) {
                //清除Form表单数据
                $("#newsInfoForm").form('clear');
                //关闭当前窗口
                $("#newsInfoDialog").window('close');
                //刷新grid
                $('#newsInfoGrid').datagrid('reload');
            } else {
                $.messager.alert("操作成功", '操作成功');
            }
            if (data == 'true') {
                alert(1);

            }
            else {
                $.messager.alert("错误提示", '操作失败');
            }
        }

    });

    $("body").keyup(function (event) {
        //"Esc"键关闭弹出窗口
        if (event.keyCode == 27) {
            $(".easyui-window").window("close");
        }
    });
});
