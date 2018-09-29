$(function () {
    //css 
    $("#userInfoForm>div ").css("height", 35);

    var Genders = [
        { key: 1, value: '男' },
        { key: 0, value: '女' }
    ];
    var UserIsValids = [
        { key: 1, value: '有效' },
        { key: 0, value: '无效' }
    ];
    var UserIsDels = [
        { key: 1, value: '已删' },
        { key: 0, value: '未删' }
    ];

    //加载表格
    $('#usergrid').datagrid({
        url: '/api/User/SearchInfo',
        singleSelect: true,
        queryParams: { name: '' },
        pagination: true,
        pageSize: 100,//每页显示的记录条数，默认为10 
        pageList: [5, 10, 100],//可以设置每页记录条数的列表 
        columns: [
            [
                //{
                //    field: 'ck', checkbox: true, rowspan: 2, formatter: function (value, row, index) {
                //        return true;
                //    }
                //},
                { title: '基本信息', colspan: 4 },
                { title: '附加信息', colspan: 5 },
                {
                    field: 'headpic', title: '头像', width: 65, align: 'left', rowspan: 2, formatter: function (value, row) {
                        if (typeof (row.PicUrl) != "undefined" && row.PicUrl != null && row.PicUrl.lastIndexOf('<img') == 0) {
                            return row.PicUrl;
                        }
                        row.PicUrl = "<img src=" + row.PicUrl + "  style='width:60px; height:60px' />";
                        return row.PicUrl;
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
                { field: 'Id', title: '编号', width: 100, align: 'right' },
                { field: 'Account', title: '账号', width: 100, align: 'left' },
                {field: 'NickName', title: '昵称', width: 100, align: 'left'},
                {
                    field: 'Gender', title: '性别', width: 50, align: 'left', formatter: function (value, row) {
                        if (value == "1") {
                            return "男";
                        } else if (value == "0") {
                            return "女";
                        }
                        return value;
                    },
                    editor: {
                        type: 'combobox',
                        options: {
                            valueField: 'key',
                            textField: 'value',
                            panelHeight: 60,
                            data: Genders,
                            required: true
                        }
                    }
                },
                {
                    field: 'Birthday', title: '出生日期', width: 120, align: 'left', editor: {
                        type: 'datebox',
                        options: {
                            formatter: function (date) {
                                return formatDate(date);
                            },
                            parser: function (date) {
                                var t = Date.parse(date);
                                if (!isNaN(t)) {
                                    return new Date(t);
                                } else {
                                    return new Date();
                                }
                            }
                        }
                    }
                },
                {
                    field: 'CreateTime', title: '创建时间', width: 120, align: 'left', editor: {
                        type: 'datebox',
                        options: {
                            formatter: function (date) {
                                return formatDate(date);
                            },
                            parser: function (date) {
                                var t = Date.parse(date);
                                if (!isNaN(t)) {
                                    return new Date(t);
                                } else {
                                    return new Date();
                                }
                            }
                        }
                    }
                },
                {
                    field: 'UpdateTime', title: '更新时间', width: 120, align: 'left', editor: {
                        type: 'datebox',
                        options: {
                            formatter: function (date) {
                                return formatDate(date);
                            },
                            parser: function (date) {
                                var t = Date.parse(date);
                                if (!isNaN(t)) {
                                    return new Date(t);
                                } else {
                                    return new Date();
                                }
                            }
                        }
                    }
                },
                {
                    field: 'UserIsValid', title: '是否有效', width: 80, align: 'left', formatter: function (value, row) {
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
                            data: UserIsValids,
                            required: true
                        }
                    }
                },
                {
                    field: 'UserIsDel', title: '是否删除', width: 80, align: 'left', formatter: function (value, row) {
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
                            data: UserIsDels,
                            required: true
                        }
                    }
                }                
            ]],
        onLoadSuccess: function (row) {
            //var rowData = row.rows;
            //$.each(rowData, function (idx, val) {   //遍历JSON  
            //    if (val.gender == "1") {
            //        $("#usergrid").datagrid("selectRow", idx);  //如果数据行为已选中则选中改行  
            //    }
            //});
        },
        onEndEdit: function (index, row) {
            //Ajax操作
            $.ajax({
                url: '/api/User/AddEdit',
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
            text: '添加用户',
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
        }, '-', {
            text: '导出Excel',
            iconCls: 'icon-excel',
            handler: function () {
                var params = {
                    name: $("#txtSearchUserName").textbox('getValue'),
                    isExport: true
                }
                outputExcel('/api/User/ExportExcel', params);
            }
        }, '-', {
            text: '上传头像',
            iconCls: 'icon-upload',
            handler: function () {
                uploadPic();
            }
        }, '-', {
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
            //, '-', {
            //    text: '扩展测试',
            //    iconCls: 'icon-edit',
            //    handler: function () {
            //        enableFilter();
            //    }
            //}
        ]
    });

    ////行渲染
    //$('#usergrid').datagrid("options").view.onBeforeRender = function (target, rows) {
    //    $.each(rows, function (index, row) {
    //        row.gender = formatSex(row.gender);
    //        row.headpic = showheadpic(row.picurl);
    //        row.createTime = formatDate(row.createTime);
    //    });
    //};
    //设置分页控件 
    var p = $('#usergrid').datagrid('getPager');

    $(p).pagination({
        //pageSize: 5,//每页显示的记录条数，默认为10 
        //pageList: [5, 10, 15],//可以设置每页记录条数的列表 
        beforePageText: '第',//页数文本框前显示的汉字 
        afterPageText: '页    共 {pages} 页',
        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
        /*onBeforeRefresh:function(){
            $(this).pagination('loading');
            alert('before refresh');
            $(this).pagination('loaded');
        }*/
    });

    //验证两次输入密码是否一致
    $.extend($.fn.validatebox.defaults.rules, {
        equals: {
            validator: function (value, param) {
                return value == $(param[0]).val();
            },
            message: '两次输入的密码不一致'
        }

    });

    //查询
    $("#btnSearch").click(function () {
        //刷新grid
        $('#usergrid').datagrid('load',
            {
                name: $("#txtSearchUserName").textbox('getValue'),
                stTime: $("#txtstTime").datebox('getValue'),
                edTime: $("#txtedTime").datebox('getValue')
            });
    });

    //form提交
    $("#userInfoForm").form({
        url: '/api/User/AddEdit',
        onSubmit: function () {
            //验证表单验证是否成功
            var isValid = $("#userInfoForm").form('validate');
            if (!isValid) {
                removeload();
            }
            return isValid;
        },
        success: function (data) {
            removeload();
            if (data == 'true') {
                //清除Form表单数据
                $("#userInfoForm").form('clear');
                //关闭当前窗口
                $("#userdialog").window('close');
                //刷新grid
                $('#usergrid').datagrid('reload');
            }
            else {
                $.messager.alert("错误提示", '操作失败');
            }
        }

    });

    fileboxInit();
    $("#importFileForm").form({
        url: '/api/Upload/UploadUserPic',
        onSubmit: function (param) {
            param.id = $('#usergrid').datagrid('getSelected').Id;
        },
        success: function (data) {
            var data = JSON.parse(data);

            if (data.IsSucceed == true || data.IsSucceed == "true") {
                //清除Form表单数据
                //$("#importFileForm").form('clear');
                //关闭当前窗口
                $("#userUploadPic").window('close');
                //刷新页面
                $('#usergrid').datagrid('reload');
            }
            else {
                alert(data.ErrorMsg);
            }
        }

    });
    //加载权限树
    LoadTree();
    //格式化日期输入框
    InitDateBox();

    $("body").keyup(function (event) {
        //"Esc"键关闭弹出窗口
        if (event.keyCode == 27) {
            $(".easyui-window").window("close");
        }
    });


});

function enableFilter() {
    $('#usergrid').datagrid('getRow');
}

function InitDateBox() {
    $('#txtstTime').datebox({
        //required: true
        width: 130,
        editable: false,
        formatter: function (date) {
            //格式化时间格式(common.js)
            return formatDate(date);
        },
        parser: function (date) {
            var t = Date.parse(date);
            if (!isNaN(t)) {
                return new Date(t);
            } else {
                return new Date();
            }
        }
    });

    $('#txtedTime').datebox({
        //required: true
        width: 130,
        editable: false,
        formatter: function (date) {
            return formatDate(date);
        },
        parser: function (date) {
            var t = Date.parse(date);
            if (!isNaN(t)) {
                return new Date(t);
            } else {
                return new Date();
            }
        }
    });
}

//// 格式化性别  
//function formatSex(sex) {
//    if (typeof (sex) != "undefined") {
//        if (sex == "1") {
//            return "男";
//        } else if (sex == "0") {
//            return "女";
//        }
//    }
//    return sex;
//}

////显示头像图片
//function showheadpic(picurl) {
//    //alert(picurl);
//    if (typeof (picurl) != "undefined") {
//        picurl = "<img src=" + picurl + "  style='width:60px; height:60px' />";
//    }
//    return picurl;
//}
