///datagrid行内编辑

function getRowIndex(target) {
    var tr = $(target).closest('tr.datagrid-row');
    return parseInt(tr.attr('datagrid-row-index'));
}
function editrow(target) {
    $('#usergrid').datagrid('beginEdit', getRowIndex(target));
}
function deleterow(target) {    
    $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
        if (r) {
            $.ajax({
                url: '/api/User/Del',
                data: { id: $('#usergrid').datagrid('getSelected').Id },
                type: 'Post',
                success: function (data) {
                    if (data == 'true' || data == true) {
                        //$('#usergrid').datagrid('deleteRow', getRowIndex(target));
                        //刷新grid
                        $('#usergrid').datagrid('reload');
                        $.messager.alert("操作成功", '操作成功');
                    }
                    else {
                        $.messager.alert("错误提示", '删除失败');
                    }
                },
                error: function () {
                    $.messager.alert("错误", '删除失败');
                }
            })
        }
    });
}
function saverow(target) {
    $('#usergrid').datagrid('endEdit', getRowIndex(target));
}
function cancelrow(target) {
    $('#usergrid').datagrid('cancelEdit', getRowIndex(target));
}