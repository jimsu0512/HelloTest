$(function () {
    //css 
    $("#userActivityForm>div ").css("height", 65);

    var UAIsValids = [
        { key: 1, value: '有效' },
        { key: 0, value: '无效' }
    ];
    var UAIsDels = [
        { key: 1, value: '已删' },
        { key: 0, value: '未删' }
    ];
    var UAStates = [
        { key: 0, value: '未开始' },
        { key: 1, value: '开始' },
        { key: 2, value: '半结束' },
        { key: 3, value: '活动结束' }
    ];

    //加载表格
    $('#userActivityGrid').datagrid({
        url: '/UserActivity/SearchInfo',
        singleSelect: true,
        queryParams: { name: '' },
        pagination: true,
        pageSize: 2,//每页显示的记录条数，默认为10 
        pageList: [2, 10, 100],//可以设置每页记录条数的列表 
        columns: [
            [
                { title: '基本信息', colspan: 4 },
                { title: '附加信息', colspan: 4 },                
                {
                    field: 'action', title: '操作', width: 80, align: 'center', rowspan: 2,
                    formatter: function (value, row, index) {
                        if (row.editing) {
                            var s = '<a href="#" onclick="saverow(this)">保存</a> ';
                            var c = '<a href="#" onclick="cancelrow(this)">取消</a>';
                            return s + c;
                        } else {
                            var e = '<a href="#" onclick="editrow(this)">修改</a> ';
                            var d = '<a href="#" onclick="deleterow(this)">无效</a>';
                            return e + d;
                        }
                    }
                }
            ], [
                { field: 'UActivityId', title: '编号', width: 100, align: 'right' },
                { field: 'NickName', title: '昵称', width: 100, align: 'left', editor: 'text'},
                { field: 'UAActivityName', title: '活动名称', width: 100, align: 'left', editor: 'text' },              
                { field: 'UAIntroduce', title: '介绍', width: 100, align: 'left', editor: 'text' },
                { field: 'UAPrice', title: '价格', width: 100, align: 'left', editor: 'text' },
                {
                    field: 'UACreateTime', title: '创建时间', width: 120, align: 'left' },
                {
                    field: 'UAUpdateTime', title: '更新时间', width: 120, align: 'left' },
                {
                    field: 'UAIsValid', title: '是否有效', width: 80, align: 'left', formatter: function (value, row) {
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
                            data: UAIsValids,
                            required: true
                        }
                    }
                },
                {
                    field: 'UAIsDel', title: '是否删除', width: 80, align: 'left', formatter: function (value, row) {
                        if (value == 1) {
                            return "已删";
                        } else if (value == 0) {
                            return "未删";
                        }
                        return value;
                    },
                    editor: {
                        type: 'combobox',
                        options: {
                            valueField: 'key',
                            textField: 'value',
                            panelHeight: 60,
                            data: UAIsDels,
                            required: true
                        }
                    }
                },
                {
                    field: 'UAState', title: '状态', width: 80, align: 'left', formatter: function (value, row) {
                        if (value == 1) {
                            return "已开始";
                        } else if (value == 0) {
                            return "未开始";
                        }
                        else if (value == 2) {
                            return "半结束";
                        }
                        else if (value == 3) {
                            return "活动结束";
                        }
                        return value;
                    },
                    editor: {
                        type: 'combobox',
                        options: {
                            valueField: 'key',
                            textField: 'value',
                            panelHeight: 60,
                            data: UAStates,
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
                url: '/BannerInfo/AddEdit',
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
        },
        toolbar: [{
            text: '添加Banner',
            iconCls: 'icon-add',
            handler: function () {
                Add();
            }
        }, '-', {
            text: '上传图片',
            iconCls: 'icon-upload',
            handler: function () {
                uploadPic();
            }
        }
        ]       
    });
    //设置分页控件 
    var p = $('#userActivityGrid').datagrid('getPager');

    $(p).pagination({
        beforePageText: '第',//页数文本框前显示的汉字 
        afterPageText: '页    共 {pages} 页',
        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',        
    });

    //form提交
    $("#userActivityGrid").form({
        url: '/BannerInfo/AddEdit',
        onSubmit: function () {
            //验证表单验证是否成功
            var isValid = $("#userActivityGrid").form('validate');
            if (!isValid) {
                removeload();
            }
            return isValid;
        },
        success: function (data) {
            removeload();
            if (data != 'true' && data != true) {
                //清除Form表单数据
                $("#userActivityGrid").form('clear');
                //关闭当前窗口
                $("#userActivityDialog").window('close');
                //刷新grid
                $('#userActivityGrid').datagrid('reload');
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
