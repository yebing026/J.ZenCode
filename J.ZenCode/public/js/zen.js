var backId;
$(function () {
    $.ajaxSetup({
        cache: false,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            if (typeof (errorThrown) != "undefined")
                top.alertMsg.error("调用服务器失败。<br />" + errorThrown);
            else {
                var error = "<b style='color: #f00'>" + XMLHttpRequest.status + "  " + XMLHttpRequest.statusText + "</b>";
                var start = XMLHttpRequest.responseText.indexOf("<title>");
                var end = XMLHttpRequest.responseText.indexOf("</title>");
                if (start > 0 && end > start)
                    error += "<br /><br />" + XMLHttpRequest.responseText.substring(start + 7, end);
                top.alertMsg.error("调用服务器失败。<br />" + error);
            }
        }
    });

    $(".pure-table").attr("border", "0").attr("cellpadding", "0").attr("cellspacing", "1");
    $(".pure-table tr:odd").children().css('background', '#F9F9F9');

    $("a[target=iajax]").click(function () {
        var url = $(this).attr("href");
        $.ajaxjson(url, {}, function (d) {
            if (d.Success == true) {
                top.alertMsg.correct(d.Message);
            } else {
                top.alertMsg.error(d.Message);
            }
            if (d.Reload == true) {
                updated();
            }
            if (d.fun != "") {
                eval(d.fun);
            }
        });
        event.preventDefault();
    });
    $("a[target=istatus]").click(function () {
        var url = $(this).attr("href");
        $this = $(this);
        $.ajaxjson(url, {}, function (d) {
            if ($this.attr("class") == 'statusFalse') {
                $this.attr("class", "statusTrue");
            } else {
                $this.attr("class", "statusFalse");
            }
        });
        event.preventDefault();
    });
    $("input[target=ichange]").change(function () {
        $this = $(this);
        var url = "/AjaxTool/SetVal.cspx?val=" + encodeURIComponent($this.val()) + "&" + $this.attr("ref");
        $.ajaxjson(url);
    });

    $("a[target=idialog]").click(function () {
        var config = {};
        config['title'] = $(this).attr('title');
        config['id'] = 'id_' + $(this).attr('title');
        config['width'] = parseInt($(this).attr('width') || '800');
        config['height'] = parseInt($(this).attr('height') || '450');
        if ($(this).attr('follow')) {
            config['follow'] = $(this)[0];
        }
        var rel = $(this).attr('href');
        if (rel.charAt(0) == '#') {
            config['content'] = $(rel)[0];
            dialog = $.dialog(config);
        } else {
            dialog = $.dialog.open(rel, config);
        }
        event.preventDefault();
    });

    $("a[target=iback]").toggle(
                     function (e) {
                         backId = $(this).attr('backid');
                         var showId = $(this).attr('href');
                         var left = $(this).offset().left;
                         var top = $(this).offset().top + $(this).height() + 3;
                         $(showId).addClass('popover').css({ top: top + "px", left: left + "px" }).show();
                     }, function () {
                         $(".popover").hide();
                     }
                );


    $("a[target=itab]").each(function () {
        $(this).click(function () {
            var title = $(this).attr("title");
            var url = $(this).attr("href");
            top.navTab.openTab(title, url, { title: title, external: true, fresh: true, data: {} });
            event.preventDefault();
        })
    });

    $("#aspnetForm").validate({
        event: "blur",
        errorElement: "em",
        errorPlacement: function (error, element) {
            error.appendTo(element.parent());
        },
        success: function (label) {
            label.addClass("success");
        }
    });


    $(".selAll").click(function () {
        $name = $(this).attr('target');
        if (this.checked) {
            $("input[name='" + $name + "']").each(function () { this.checked = true; });
        } else {
            $("input[name='" + $name + "']").each(function () { this.checked = false; });
        }
    });

    $(".toggle").toggle(function () {
        $target = $($(this).attr("rel"));
        $target.slideDown("slow");
    }, function () {
        $target = $($(this).attr("rel"));
        $target.slideUp("hide");
    });
    /*选项卡效果*/
    $('.taber .head a').hover(function () {
        $('.taber .body').hide();
        $('.taber ' + $(this).attr('href')).show();
        $('.taber .head a').removeClass('selected');
        $(this).addClass('selected');
    });
    /*tooltip*/
    $('.tip').hover(
                        function () {
                            var str = $(this).attr('rel');
                            $('<div class="tooltip" style="display:none; top:' + ($(this).offset().top + $(this).height() + 5) + 'px;left:' + ($(this).offset().left - 10) + 'px;">' + str + '<div class="arrow"></div></div>').appendTo('body').fadeIn();
                        },
                        function () {
                            $('.tooltip').fadeOut().remove();
                        }
                    );

    /*popover*/
    $('.pop').toggle(
                        function (e) {
                            var str = $($(this).attr('rel')).html();
                            var left;
                            if ($(this).offset().left + 150 > document.body.clientWidth) {
                                left = $(this).offset().left - 300;
                            } else {
                                left = $(this).offset().left;
                            }
                            $('<div class="popover"  style="top:' + ($(this).offset().top + $(this).height() + 3) + 'px;left:' + left + 'px;">' + str + '</div>').appendTo('body').show();
                        }, function () {
                            $('.popover').hide().remove();
                        }
                    );

});


function idialog(url, width, height) {
    var config = {};
    config['width'] = width || 800;
    config['height'] = height || 400;
    dialog = $.dialog.open(url, config);
};
function backDialog(msg, refresh) {
    if (refresh == 0) {
        $.dialog.alert(msg);
    } else if (refresh == 1) {
        $.dialog.confirm(msg + ",是否刷新？", function () {
            updated();
        }, function () {
        });
    } else {
        $.dialog.confirm(msg + ",是否刷新？", function () {
            parent.updated();
        }, function () {
        });
    }
};
function closeDialog() {
    var list = $.dialog.list;
    for (var i in list) {
        list[i].close();
    };
}
function updated() {
    if ($("#_btnSearch").length > 0) {
        $("#_btnSearch").trigger("click");
    } else {
        window.location.href = window.location.href;
    }
};


$.ajaxjson = function (url, dataMap, fnSuccess) {
    $.ajax({
        type: "POST",
        url: url,
        data: dataMap,
        dataType: "json",
        success: fnSuccess
    });
}
$.ajaxtext = function (url, dataMap, fnSuccess) {
    $.ajax({
        type: "POST",
        url: url,
        data: dataMap,
        success: fnSuccess
    });
}

jQuery.extend(String.prototype, {
    replaceAll: function (os, ns) {
        return this.replace(new RegExp(os, "gm"), ns);
    },
    trim: function () {
        return this.replace(/(^\s*)|(\s*$)|\r|\n/g, "");
    },
    format: function () {
        var args = arguments;
        return this.replace(/\{(\d+)\}/g,
            function (m, i) {
                return args[i];
            });
    }
});


jQuery.urlVal = function (name) {
    var urlt = window.location.href.split("?");
    var gets = urlt[urlt.length - 1].split("&");
    var location_get_vars = new Array();
    for (var i = 0; i < gets.length; i++) {
        var get = gets[i].split("=");
        eval("location_get_vars['" + get[0] + "'] = '" + unescape(get[1]) + "';");
    }
    return location_get_vars[name];
};

//功能函数 
function getCbVal(name, dec) {
    var ids = [];
    $("input[name$='" + name + "']:checked").each(function () {
        ids.push($(this).val());
    });
    if (ids.length == 0) {
        top.alertMsg.error(dec || "请选择数据");
        return false;
    }
    return ids.join(',');
}

function rowspan(_w_table_id, _w_table_colnum) {
    _w_table_firsttd = "";
    _w_table_currenttd = "";
    _w_table_SpanNum = 0;
    _w_table_Obj = $(_w_table_id + " tr td:nth-child(" + _w_table_colnum + ")");
    _w_table_Obj.each(function (i) {
        if (i == 0) {
            _w_table_firsttd = $(this);
            _w_table_SpanNum = 1;
        } else {
            _w_table_currenttd = $(this);
            if (_w_table_firsttd.text() == _w_table_currenttd.text()) {
                _w_table_SpanNum++;
                _w_table_currenttd.hide(); //remove();  
                _w_table_firsttd.attr("rowSpan", _w_table_SpanNum);
                _w_table_firsttd.attr("valign", "middle");
                _w_table_firsttd.attr("bgcolor", "#CCCCCC");
            } else {
                _w_table_firsttd = $(this);
                _w_table_SpanNum = 1;
            }
        }
    });
}
