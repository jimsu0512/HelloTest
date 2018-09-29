$(function () {
    //css 
    $("#bannerInfoForm>div ").css("height", 35);

    var CBIsValids = [
        { key: 1, value: '有效' },
        { key: 0, value: '无效' }
    ];
    var CBIsDels = [
        { key: 1, value: '已删' },
        { key: 0, value: '未删' }
    ];

    //加载表格
    $('#bannerInfoGrid').datagrid({
        url: '/BannerInfo/SearchInfo',
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
                        if (typeof (row.CBannerUrl) != "undefined" && row.CBannerUrl != null && row.CBannerUrl.lastIndexOf('<img') == 0) {
                            return row.CBannerUrl;
                        }
                        row.CBannerUrl = "<img src=" + row.CBannerUrl + "  style='width:100px; height:60px' />";
                        return row.CBannerUrl;
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
                { field: 'CBannerId', title: '编号', width: 100, align: 'right' },
                { field: 'CBTypeName', title: '类型名称', width: 100, align: 'left' },
                { field: 'CBannerName', title: 'Banner名称', width: 100, align: 'left', editor: 'text' },              
                { field: 'CSort', title: '排序名称', width: 100, align: 'left' },
                {
                    field: 'CBCreateDate', title: '创建时间', width: 120, align: 'left' },
                {
                    field: 'CBUpdateDate', title: '更新时间', width: 120, align: 'left' },
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
    var p = $('#bannerInfoGrid').datagrid('getPager');

    $(p).pagination({
        beforePageText: '第',//页数文本框前显示的汉字 
        afterPageText: '页    共 {pages} 页',
        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',        
    });

    //form提交
    $("#bannerInfoForm").form({
        url: '/BannerInfo/AddEdit',
        onSubmit: function () {
            //验证表单验证是否成功
            var isValid = $("#bannerInfoForm").form('validate');
            if (!isValid) {
                removeload();
            }
            return isValid;
        },
        success: function (data) {
            removeload();
            if (data != 'true' && data != true) {
                //清除Form表单数据
                $("#bannerInfoForm").form('clear');
                //关闭当前窗口
                $("#bannerInfoDialog").window('close');
                //刷新grid
                $('#bannerInfoGrid').datagrid('reload');
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

    fileboxInit();
    $("#bannerInfoFileForm").form({
        url: '/api/Upload/UploadBannerImg',
        onSubmit: function (param) {
            param.id = $('#bannerInfoGrid').datagrid('getSelected').CBannerId;
        },
        success: function (data) {
            var data = JSON.parse(data);

            if (data.IsSucceed == true || data.IsSucceed == "true") {
                //清除Form表单数据
                //$("#importFileForm").form('clear');
                //关闭当前窗口
                $("#bannerInfoUploadPic").window('close');
                //刷新页面
                $('#bannerInfoGrid').datagrid('reload');
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
