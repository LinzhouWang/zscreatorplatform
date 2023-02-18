function loadjs(path) {
    document.write('<script src=\'' + path + '\' ></script>');
}

function loadcss(path) {
    document.write('<link href=\'' + path + '\' rel=\'stylesheet\'>');
}

loadcss('../../static/h-ui/css/H-ui.min.css');
loadcss('../../static/h-ui.admin/css/H-ui.admin.css');
loadcss('../../lib/Hui-iconfont/1.0.8/iconfont.css');
loadcss('../../static/h-ui.admin/skin/default/skin.css');
loadcss('../../static/h-ui.admin/css/style.css');

loadjs('../../lib/jquery/1.9.1/jquery.min.js');
loadjs('../../lib/layer/2.4/layer.js');
loadjs('../../static/h-ui.admin/js/H-ui.admin.js');
loadjs('../../static/h-ui/js/H-ui.min.js');

loadjs('../../lib/jquery.contextmenu/jquery.contextmenu.r2.js');


//工具
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]);
    return null;
}
String.prototype.replaceAll = function (s1, s2) {
    return this.replace(new RegExp(s1, "gm"), s2);
}
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

var baseUrl = window.location.origin + "/";//"http://localhost:14393/";


function postJson(url, data, success) {
    console.log(JSON.stringify(data));
    $.ajax({
        type: 'post',
        url: baseUrl + url,
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: function (msg) {
            success(msg);
        }
    });
}

function postObject(url, data, success) {
    $.ajax({
        type: 'post',
        url: baseUrl + url,
        contentType: 'application/json',
        data: data,
        success: function (msg) {
            success(msg);
        }
    });
}

function showEditScriptPage(id, title) {
    var index = parent.layer.open({
        type: 2,
        content: '/script/scriptedit.html?id=' + id,
        area: ['1280px', '768px'],
        maxmin: true,
        resize: true,
        title: title
    });
    parent.layer.iframeAuto(index);
}

function InitProvince(id) {
    if (id) {
        postObject('/api/tool/getprovince', '', function (msg) {
            if (msg) {
                var html = '';
                $(msg).each(function (index, object) {
                    html += '<option value="' + object.ef_pid + '">' + object.ef_pName + '</option>';
                });
                $('#' + id + '').html(html);
            }
        })
    }
}



