$(function () {
    $("#userInfoForm>div ").css("height", 35);

    var CBIsValids = [
        { key: 1, value: '有效' },
        { key: 0, value: '无效' }
    ];
    var CBIsDels = [
        { key: 1, value: '已删' },
        { key: 0, value: '未删' }
    ];

    //加载表格
    $('#bannerTypeGird').datagrid({
        url: '/BannerType/SearchInfo',
        singleSelect: true,
        queryParams: { name: '' },
        pagination: true,
        pageSize: 100,//每页显示的记录条数，默认为10 
        pageList: [5, 10, 100],//可以设置每页记录条数的列表 
        columns: [
            [
                { title: '基本信息', colspan: 4 },                          
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
                { field: 'CBTypeId', title: '编号', width: 100, align: 'right' },
                { field: 'CBTypeName', title: '类型名称', width: 100, align: 'left', editor: 'text'},               
                {
                    field: 'CBIsValid', title: '是否有效', width: 80, align: 'left', formatter: function (value, row) {
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
                            data: CBIsValids,
                            required: true
                        }
                    }
                },
                {
                    field: 'CBIsDel', title: '是否删除', width: 80, align: 'left', formatter: function (value, row) {
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
                            data: CBIsDels,
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
                url: '/BannerType/AddEdit',
                type: 'POST',
                dataType: "json",
                data: row,
                success: function (data) {  
                    console.log(data);
                    if (data.success) {                        
                        $.messager.alert("操作成功", '操作成功');
                    } else {
                        $.messager.alert("操作失败", '操作失败');
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
            text: '添加类型',
            iconCls: 'icon-add',
            handler: function () {
                add();
            }
        }, '-', {
            text: '修改',
            iconCls: 'icon-edit2',
            handler: function () {
                edit();
            }
        },   '-', {
            text: '权限设置',
            iconCls: 'icon-security',
            handler: function () {
                EditPermission();
            }
        }, '-', {
            text: '删除',
            iconCls: 'icon-delete',
            handler: function () {
                del();
            }
        }
        ]
    });

    //form提交
    $("#bannerTypeForm").form({
        url: '/BannerType/AddEdit',
        onSubmit: function () {
            //验证表单验证是否成功
            var isValid = $("#bannerTypeForm").form('validate');
            if (!isValid) {
                removeload();
            }
            return isValid;
        },
        success: function (data) {
            var data = JSON.parse(data);
            removeload();
            if (data.success) {
                //清除Form表单数据
                $("#bannerTypeForm").form('clear');
                //关闭当前窗口
                $("#bannerTypeDialog").window('close');
                //刷新grid
                $('#bannerTypeGird').datagrid('reload');
            }
            else {
                $.messager.alert("错误提示", '操作失败');
            }
        }

    });
})