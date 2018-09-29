$(function () {
    //css 
    $("#serviceTypeForm>div ").css("height", 35);

    var CSTIsValids = [
        { key: 1, value: '有效' },
        { key: 0, value: '无效' }
    ];
    var CSTIsDels = [
        { key: 1, value: '已删' },
        { key: 0, value: '未删' }
    ];

    //加载表格
    $('#serviceTypeGrid').datagrid({
        url: '/ServiceType/SearchInfo',
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
                    field: 'headpic', title: '图片', width: 65, align: 'left', rowspan: 2, formatter: function (value, row) {
                        if (typeof (row.CSTImg) != "undefined" && row.CSTImg != null && row.CSTImg.lastIndexOf('<img') == 0) {
                            return row.CSTImg;
                        }
                        row.CSTImg = "<img src=" + row.CSTImg + "  style='width:100px; height:60px' />";
                        return row.CSTImg;
                    }
                },
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
                { field: 'CSTTypeId', title: '编号', width: 100, align: 'right' },
                { field: 'CSTTypeName', title: '类型名称', width: 100, align: 'left' },                      
                { field: 'CSTDaliPrice', title: '价格', width: 100, align: 'left' },
                {
                    field: 'CSTCreateDate', title: '创建时间', width: 120, align: 'left' },
                {
                    field: 'CSTUpdateDate', title: '更新时间', width: 120, align: 'left' },
                {
                    field: 'CSTIsValid', title: '是否有效', width: 80, align: 'left', formatter: function (value, row) {
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
                            data: CSTIsValids,
                            required: true
                        }
                    }
                },
                {
                    field: 'CSTIsDel', title: '是否删除', width: 80, align: 'left', formatter: function (value, row) {
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
                            data: CSTIsDels,
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
                url: '/ServiceType/AddEdit',
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
    var p = $('#serviceTypeGrid').datagrid('getPager');

    $(p).pagination({
        beforePageText: '第',//页数文本框前显示的汉字 
        afterPageText: '页    共 {pages} 页',
        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',        
    });

    //form提交
    $("#serviceTypeForm").form({
        url: '/ServiceType/AddEdit',
        onSubmit: function () {
            //验证表单验证是否成功
            var isValid = $("#serviceTypeForm").form('validate');
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
                $("#serviceTypeForm").form('clear');
                //关闭当前窗口
                $("#serviceTypeDialog").window('close');
                //刷新grid
                $('#serviceTypeGrid').datagrid('reload');
            }
            else {
                $.messager.alert("错误提示", '操作失败');
            }
        }
    });

    fileboxInit();
    $("#serviceTypeFileForm").form({
        url: '/api/Upload/UploadServiceTypeImg',
        onSubmit: function (param) {
            param.id = $('#serviceTypeGrid').datagrid('getSelected').CSTTypeId;
        },
        success: function (data) {
            var data = JSON.parse(data);

            if (data.IsSucceed == true || data.IsSucceed == "true") {
                //清除Form表单数据
                //$("#importFileForm").form('clear');
                //关闭当前窗口
                $("#serviceTypeUploadPic").window('close');
                //刷新页面
                $('#serviceTypeGrid').datagrid('reload');
            }
            else {
                alert(data.ErrorMsg);
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
