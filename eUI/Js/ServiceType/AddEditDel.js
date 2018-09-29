/////用户增删改查

function Add() {
    $("#txtHideID").val(-1);
    openDialog('新增服务类型');
}

function openDialog(title) {
    $('#serviceTypeDialog').dialog({
        title: title,
        modal: true,
        width: 400,
        shadow: true,
        height: 350,
        closed: false,
        buttons: [{
            text: '保存',
            handler: function () {
                onloading('正在保存...');
                $("#serviceTypeForm").submit();
            }
        }, {
            text: '取消',
            handler: function () {
                $('#serviceTypeForm').window('close');
            }
        }],
        onClose: function () {
            //清除Form表单数据
            $("#serviceTypeForm").form('clear');
            //$(this).dialog('destroy');
        }
    });
}