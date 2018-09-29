////用户面板设置

$(function () {
    createpie();
    createcolumn();

    $("#userpanel .panel").css('float', 'left');
});

function createpie() {
    var url = "/Home/UserReport";
    ////var url = "http://mp.weixin.qq.com/s?__biz=MjM5NzAwNzMyMA==&mid=2659788256&idx=1&sn=53470df8ff9d4c3c7591bcfdfc4e102e&chksm=bd9d1f578aea96410ec414f29698bdc8c843ad603875a8cea756a0c9a5d0ff5a6edeb3be8fb6&scene=0#wechat_redirect";
    var userpiepanel = "<div id='boarduserpie'>";
    userpiepanel += "<iframe scrolling='auto' frameborder='0'  src='" + url + "' style='width:100%;height:99%;'>";
    userpiepanel += "</iframe>";
    userpiepanel += "</div>";
    userpiepanel += "<div class='panel' >&nbsp;</div>";
    $("#userpanel").append(userpiepanel);

    $('#boarduserpie').panel({
        cache: false,
        width: 610,
        height: 520,
        title: '用户报表(饼图)',
        tools: [{
            iconCls: 'icon-resize',
            handler: function () {
                var panelwidth = $('#boarduserpie').panel('options').width;
                if (panelwidth > 800) {
                    $('#boarduserpie').panel('resize', {
                        width: 610,
                        height: 520
                    });
                }
                else {
                    $('#boarduserpie').panel('resize', {
                        width: 1000,
                        height: 520
                    });
                }
            }
        }, {
            iconCls: 'icon-delete',
            handler: function () {
                $.messager.confirm('提示', '您确认想要删除该面板吗？', function (r) {
                    if (r) {
                        //确认
                        //关闭panel
                        $("#boarduserpie").panel('close');
                    }
                });
            }
        }],
        onOpen: function () {

        }
    });
}

function createcolumn() {
    var url = "/Home/UserColumnReport";
    var userpiepanel = "<div id='boardusercolumn'>";
    userpiepanel += "<iframe scrolling='auto' frameborder='0'  src='" + url + "' style='width:100%;height:99%;'>";
    userpiepanel += "</iframe>";
    userpiepanel += "</div>";
    $("#userpanel").append(userpiepanel);

    $('#boardusercolumn').panel({
        cache: false,
        width: 610,
        height: 520,
        title: '用户报表(柱状图)',
        tools: [{
            iconCls: 'icon-resize',
            handler: function () {
                var panelwidth = $('#boardusercolumn').panel('options').width;
                if (panelwidth > 800) {
                    $('#boardusercolumn').panel('resize', {
                        width: 610,
                        height: 520
                    });
                }
                else {
                    $('#boardusercolumn').panel('resize', {
                        width: 1000,
                        height: 520
                    });
                }
            }
        }, {
            iconCls: 'icon-delete',
            handler: function () {
                $.messager.confirm('提示', '您确认想要删除该面板吗？', function (r) {
                    if (r) {
                        //确认
                        //关闭panel
                        $("#boardusercolumn").panel('close');
                    }
                });
            }
        }]
    });
}
