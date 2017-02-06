$.fn.extend({
    zk_paging: function (option) {
        var self = this;
        var _default = {
            pageSize: 10,
            pageIndex: 1,
            pageShow: 10,
            totalContent: 1,
            pageCallback: function (index) {
                alert(index);
            }
        }

        var _option = $.extend(_default, option);
        var pagezNum = Math.ceil(_option.totalContent / _option.pageSize);

        $(self).html('');

        var table = $('<table><tr></tr></table>');

        var startNum = (_option.pageIndex - 1) * _option.pageSize + 1;

        var endNum = startNum + 10 - 1;

        if (_option.pageIndex == pagezNum) {
            if (_option.totalContent <= _option.pageShow) {
                endNum = _option.totalContent;
            } else {
                endNum = _option.totalContent;
            }
        }

        //var _init = $('<td><span class="introduce">显示第 ' + startNum + ' 到第 ' + endNum + ' 条记录，总共 ' + _option.totalContent + ' 条记录</span></td>');

        //table.append(_init);

        var _nav = $('<td style="text-align:right"><nav class="pageNum"></nav></td>');
        var ul = $('<ul class="pagination">');
        _nav.find('nav').append(ul);

        table.append(_nav);

        var initialPage = function () {
            ul.html('');
            ///添加首页
            var li_first = $('<li><a>&laquo;</a></li>');
            ul.append(li_first);
            li_first.bind('click', function () {
                _option.pageIndex = 1;
                if ($.isFunction(_option.pageCallback)) {
                    _option.pageCallback(_option.pageIndex);
                }
                initialPage();
            })

            var StartNum = 1;
            var EndNum = pagezNum;
            if (_option.pageIndex >= _option.pageShow) {
                StartNum = _option.pageIndex - Math.ceil(_option.pageShow / 2);
            }

            if (StartNum + _option.pageShow >= pagezNum) {
                EndNum = pagezNum;
            } else {
                EndNum = StartNum + _option.pageShow - 1;
            }

            for (var i = StartNum; i <= EndNum; i++) {
                var li = $('<li><a>' + i + '</a></li>');
                li.data('index', (i));
                if (i == _option.pageIndex) {
                    li.addClass('active');
                }
                li.bind('click', function () {
                    _option.pageIndex = $(this).data('index');
                    if ($.isFunction(_option.pageCallback)) {
                        _option.pageCallback(_option.pageIndex);
                    }
                    initialPage();
                })
                ul.append(li);
            }
            ///添加最后一页
            var li_last = $('<li><li><a>&raquo;</a></li></li>');
            ul.append(li_last);

            li_last.bind('click', function () {
                _option.pageIndex = pagezNum;
                if ($.isFunction(_option.pageCallback)) {
                    _option.pageCallback(_option.pageIndex);
                }
                initialPage();
            })
        }
        initialPage();
        $(self).append(table);
    }
});