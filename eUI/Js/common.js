
//yyy-MM-DD
var formatDate = function (date) {
    date = new Date(date);
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    m = m < 10 ? '0' + m : m;
    var d = date.getDate();
    d = d < 10 ? ('0' + d) : d;
    return y + '-' + m + '-' + d;
};

//yyyy-MM-DD HH:mm:ss
var formatDateTime = function (date) {
    date = new Date(date);
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    m = m < 10 ? ('0' + m) : m;
    var d = date.getDate();
    d = d < 10 ? ('0' + d) : d;
    var h = date.getHours();
    var minute = date.getMinutes();
    minute = minute < 10 ? ('0' + minute) : minute;
    var second = date.getSeconds();
    second = second < 10 ? ('0' + second) : second;
    return y + '-' + m + '-' + d + ' ' + h + ':' + minute + ':' + second;
};

function onloading(msg) {
    if (msg == undefined) {
        msg = "正在加载数据，请稍候...";
    }
    $("<div class=\"datagrid-mask\"></div>").css({ display: "block", width: "100%", "z-index": 10000, height: $(window).height() }).appendTo("body");
    $("<div class=\"datagrid-mask-msg\"></div>").html(msg).appendTo("body").css({ display: "block", "z-index": 10000, left: ($(document.body).outerWidth(true) - 190) / 2, top: ($(window).height() - 45) / 2 });
}
function removeload() {
    $(".datagrid-mask").remove();
    $(".datagrid-mask-msg").remove();
}