var layerIndex = '';
    $(function () {
        $('#province').on('change', function () {
            postJson('/api/tool/GetCity', {
                Pid: $(this).val()
            }, function (msg) {
                if (msg) {
                    var html = '';
                    var optionHtml = '<option value="-1">不限</option>';
                    $(msg).each(function (index, object) {
                        optionHtml += '<option value="' + object.ef_cId + '">' + object.ef_cName + '</option>';
                        html += '<span class="pl-10"><input data-id="' + object.ef_cId + '" class="btn btn-danger-outline radius mb-10" type="button" value="' + object.ef_cName + '"></span>';
                    });
                    $("#citysContent").html(html);
                    $('#cityid').html(optionHtml);
                }
            })
        });

        //加载所有城市
        $('#loadAllcity').on('click', function () {
            var allState = $('#allcitystate').val();
            if (allState === '-1') {
                postJson('/api/tool/GetCity', {
                    Pid: -1
                }, function (msg) {
                    if (msg) {
                        $('#allcitystate').val('1');
                        var html = '';
                        $(msg).each(function (index, object) {
                            //html += '<option value="' + object.ef_cId + '">' + object.ef_cName + '</option>';
                            html += '<span class="pl-10"><input data-id="' + object.ef_cId + '" class="btn btn-danger-outline radius mb-10" type="button" value="' + object.ef_cName + '"></span>';
                        });
                        $("#citysContent").html(html);
                        if(checkSelectedCity){
                            checkSelectedCity();
                        }

                    }
                })
            } else {
                var pid = $('#province option:selected').val();
                postJson('/api/tool/GetCity', {
                    Pid: pid
                }, function (msg) {
                    if (msg) {
                        $('#allcitystate').val('-1');
                        var html = '';
                        $(msg).each(function (index, object) {
                            //html += '<option value="' + object.ef_cId + '">' + object.ef_cName + '</option>';
                            html += '<span class="pl-10"><input data-id="' + object.ef_cId + '" class="btn btn-danger-outline radius mb-10" type="button" value="' + object.ef_cName + '"></span>';
                        });
                        $("#citysContent").html(html);
                    }
                })
            }
        });

        //全选
        $('#selectAllcity').on('click', function () {
            $(this).parent().hide();
            $(this).removeAttr("checked");
            $('#disSelectAllcity').parent().show();
            $('#citysContent').find('.btn').addClass('active');
        });
        //取消全选
        $('#disSelectAllcity').on('click', function () {
            $(this).parent().hide();
            $(this).removeAttr("checked");
            $('#selectAllcity').parent().show();
            $('#citysContent').find('.btn').removeClass('active');
        });

        //城市选择完毕(父页面需要有方法seclcetCtiyCallback)
        $('#btnAddcity').on('click', function () {
            var data = getdata();
            $('#cityframe').hide();
            parent.seclcetCtiyCallback(data);
        });

        function getdata() {
            //alert("ok");
            var selectitems = $(".panel-body").find(".active");
            if (selectitems) {
                var hid = "";
                $(selectitems).each(function (i, item) {
                    hid += "," + $(item).attr("data-id");
                });
                if (hid.length > 0) {
                    //alert(hid);
                    hid = hid + ",";
                    return hid;
                } else {
                    return '';
                }
            } else {
                return '';
            }
        }

        //弹出城市选择页面
        $('#btnAddCity').on('click', function () {
            var oldCitys = '';
            if (actionkey != 'add') {
                oldCitys = $('#hidScriptCity').val();
            }
            layerIndex = layer.open({
                id: 'cityframe',
                type: 2,
                area: ['80%', '90%'],
                content: '/addcity.html?oldcity=' + oldCitys //这里content是一个URL，如果你不想让iframe出现滚动条，你还可以content: ['http://sentsin.com', 'no']
            });
        })

    });