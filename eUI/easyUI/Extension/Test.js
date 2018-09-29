$.extend($.fn.datagrid.methods, {
    getRow: function (jq, param) {

        var cfs = $(jq).datagrid('getColumnFields');

        alert(JSON.stringify(cfs));
    }
});