$(function () {

    $table = $("#scripttable");

    $table.bootstrapTable({
        columns: [{
                field: "select",
                checkbox: true,
                title: "选择"
            },
            //{
            //    field: 'RunCity',
            //    title: '城市名称'
            //},
            {
                title: '脚本名称',
                field: 'webName'
            },
            {
                title: "间隔时间",
                field: "interval"
            },
            {
                title: "操作",
                field: "Id",
                formatter: function (value, row, index) {
                    return "<button sid='" + row.guid + "' type=\"button\" class=\"btn btn-warning btn-xs mr-10 editscript\">编辑</button>";
                },
                //width: 100
            }
        ],
        pagination: true, //是否显示分页（*）
        sortable: false, //是否启用排序
        sidePagination: "server", //分页方式：client客户端分页，server服务端分页（*）
        pageNumber: 1, //初始化加载第一页，默认第一页
        pageSize: 20, //每页的记录行数（*）
        pageList: [10, 15, 20, 25], //可供选择的每页的行数（*）
        cache: false, //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
        url: '/api/Scripts/GetWebScriptDtoPageList', //请求后台的URL（*）
        method: 'get', //请求方式（*）
        toolbar: '#toolbar', //工具按钮用哪个容器
        //clickToSelect: true,
        queryParams: function (params) {
            var param = {
                pageSize: params.limit, //页面大小
                pageIndex: (params.offset / params.limit) + 1, //页码
                search: $("#textWebName").val(),
                isTotal: true,
                isDesc: true
            };
            return param;
        },
        classes: 'table table-hover',
        rowStyle: function (row, index) {
            if (row.TaskId == null) {
                return {
                    // classes: 'info',
                    // css: {
                    //     //"background-color": "#fff"
                    // }
                };
            } else {
                return {
                    classes: '',
                    css: {}
                };
            }
        },
        onDblClickRow:function(row){
            creatIframe('/pages/scripts/editscript.html?action=edit&guid=' + row.guid, '编辑规则')
        }
    });

    $("#btnSearch,#btnRefresh").on('click', function () {
        $table.bootstrapTable('refresh');
    });

    $table.on('click', '.editscript', function () {
        var guid = $(this).attr('sid');
        creatIframe('/pages/scripts/editscript.html?action=edit&guid=' + guid, '编辑规则')
    });
});