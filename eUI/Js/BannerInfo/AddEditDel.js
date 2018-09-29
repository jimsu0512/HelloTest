﻿/////用户增删改查

function Add() {
    $("#txtHideID").val(-1);
    bannerOpenDialog('新增Banner');

    $('#selBannerType').combobox({
        valueField: 'CBTypeId',
        textField: 'CBTypeName',
        url: '/BannerType/GetBannerList',
    });
}

function bannerOpenDialog(title) {
    $('#bannerInfoDialog').dialog({
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
                $("#bannerInfoForm").submit();
            }
        }, {
            text: '取消',
            handler: function () {
                $('#bannerInfoForm').window('close');
            }
        }],
        onClose: function () {
            //清除Form表单数据
            $("#bannerInfoForm").form('clear');
            //$(this).dialog('destroy');
        }
    });
}