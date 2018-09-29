//////权限设置

function LoadTree() {
    $('#permissionTree').tree({
        checkbox: true,
        onlyLeafCheck: false,
        data: [{
            text: '类型管理',
            id:0,
            state: 'open',
            children: [{
                id: 1,
                text: '添加用户'
            }, {
                id: 2,
                text: '修改用户'
            }, {
                id: 3,
                text: '删除用户'
            }, {
                id: 4,
                text: '导出Excel'
            }, {
                id: 5,
                text: '上传头像'
            }, {
                id: 6,
                text: '权限设置'
            }]
        }, {
            text: '商品管理',
            id: 0,
            state: 'open',
            children: [{
                id: 7,
                text: '添加商品'
            }, {
                id: 8,
                text: '修改商品'
            }, {
                id: 9,
                text: '删除商品'
            }]
        }]
    });
}

//权限修改
function EditPermission() {
    //获取选中行记录
    if ($('#usergrid').datagrid('getSelected') == null) {
        $.messager.alert("提示", '请先选中一条数据');
        return;
    }
    selectTreeNode();

    $('#userPermission').dialog({
        title: '修改权限',
        modal: true,
        width: 350,
        shadow: true,
        height: 300,
        closed: false,
        buttons: [{
            text: '保存',
            handler: function () {
                var treedata = getSelectedNode();
                treedata.UserID = $('#usergrid').datagrid('getSelected').Id;

                if (treedata.ActionIDList == undefined || treedata.ActionIDList == "undefined"
                    || treedata.ActionIDList == null
                    || treedata.ActionIDList.length <= 0
                    ) {
                    alert("请至少选择一项权限");
                    return;
                }
                $.ajax({
                    url: '/api/PermissionSet/SetPermission',
                    type: 'POST',
                    dataType: "json",
                    //data: {
                    //    UserID: 1,
                    //    ActionIDList: [{ ActionID: 10 }, { ActionID: 20 }]
                    //},
                    data: treedata,
                    success: function (data) {
                        if (data.IsSucceed == true || data.IsSucceed == "true") {
                            //关闭窗体
                            $("#userPermission").window('close');
                        }
                        else {
                            alert('权限设置发生错误');
                        }
                    }
                });
            }
        }]
    });
}

//选中对应用户的权限节点
function selectTreeNode() {
    //先全部取消选择节点
    var unselectTreeNode = $('#permissionTree').tree('getChecked');
    for (var j = 0; j < unselectTreeNode.length; j++) {
        if (unselectTreeNode[j].id != undefined && unselectTreeNode[j].id != "undefined") {
            var unselectNode = $('#permissionTree').tree('find', unselectTreeNode[j].id);
            $('#permissionTree').tree('uncheck', unselectNode.target);
        }
    }

    $.ajax({
        url: '/api/PermissionSet/GetUserPermission',
        type: 'POST',
        dataType: "json",
        data: {
            UserID: $('#usergrid').datagrid('getSelected').Id
        },
        success: function (data) {
            //获取后台返回的json格式数据并选中节点
            for (var i = 0; i < data.length; i++) {
                var id = data[i].ActionID;
                var node = $('#permissionTree').tree('find', id);
                $('#permissionTree').tree('check', node.target);
            }
        }
    });
}

//获取选中的Tree节点信息
function getSelectedNode() {
    var treedata = {};

    treedata.ActionIDList = [];

    var treeNode = $('#permissionTree').tree('getChecked');

    for (var i = 0; i < treeNode.length; i++) {
            treedata.ActionIDList[i] = { ActionID: treeNode[i].id };
    }
    return treedata;
}