angular.module('AnnualPlanIndex.controllers', ['AnnualPlanIndex.service'])

.controller('AnnualPlanIndexCtrl', function ($scope, AnnualPlanIndexFty, $location) {


    var pageIndex = 0;
    var pageSize = 10;

    var pageCallbakc = function (_pageIndex) {
        pageIndex = _pageIndex;
        GetDataList();
    }

    var GetDataList = function () {
        var par = {
            Year: "",
            Month: "",
            StartRow: (pageIndex == 0 ? 0 : pageIndex - 1) * pageSize,
            PageSize: pageSize,
            Title: $('#Param_Name').val(),
            StartTime: $('#Param_StartTime').val(),
            EndTime: $('#Param_EndTime').val()
        }
        AnnualPlanIndexFty.GetWorkPlanList(par, function (msg) {
            $scope.listData = msg.Data;
            $('#page').zk_paging({
                totalContent: msg.Total,
                pageSize: pageSize,
                pageIndex: pageIndex,
                pageCallback: pageCallbakc
            });
        })
    }



    var getCurrentMonthFirst = function () {
        var date = new Date();
        date.setDate(1);
        return date;
    }

    var getCurrentMonthLast = function () {
        var date = new Date();
        var currentMonth = date.getMonth();
        var nextMonth = ++currentMonth;
        var nextMonthFirstDay = new Date(date.getFullYear(), nextMonth, 1);
        var oneDay = 1000 * 60 * 60 * 24;
        return new Date(nextMonthFirstDay - oneDay);
    }

    var FormatDate = function (date, fmt) { //author: meizz 
        var o = {
            "M+": date.getMonth() + 1, //月份 
            "d+": date.getDate(), //日 
            "h+": date.getHours(), //小时 
            "m+": date.getMinutes(), //分 
            "s+": date.getSeconds(), //秒 
            "q+": Math.floor((date.getMonth() + 3) / 3), //季度 
            "S": date.getMilliseconds() //毫秒 
        };
        if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o)
            if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        return fmt;
    }

    //查询
    $scope.SearchData = function () {
        pageIndex = 0;
        GetDataList();
    }
    $('.StartTime').val(FormatDate(getCurrentMonthFirst(), 'yyyy/MM/dd'));
    $('.StartTime').datetimepicker({
        language: 'zh-CN',
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        format: 'yyyy/mm/dd',
        minView: 2,
        forceParse: 0,
        showMeridian: 1
    });
    $('.EndTime').val(FormatDate(getCurrentMonthLast(), 'yyyy/MM/dd'));
    $('.EndTime').datetimepicker({
        language: 'zh-CN',
        weekStart: 1,
        todayBtn: 1,
        format: 'yyyy/mm/dd',
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        minView: 2,
        forceParse: 0,
        showMeridian: 1
    });

    $scope.new = function () {
        $location.path('AnnualPlanEdit/000');
    }

    ///查看
    $scope.SeeInfo = function (item) {
        //AnnualPlanIndexFty.StopAdvertisement(item.id, function (msg) {
        //    GetDataList();
        //})
    }


    //编辑
    $scope.Edit = function (item) {
        $location.path('AnnualPlanEdit/' + item.id);
    }



    ///删除
    $scope.Delete = function (item) {
        var isSure = confirm("您将要删除【" + item.name + '】是否继续？');
        if (!isSure) {
            return;
        }
        AnnualPlanIndexFty.DelWorkPlan(item.id, function (msg) {
            GetDataList();
        })
    }
    GetDataList();

   
    // $('#page').zk_paging();

}).filter('DateFormat', function ($filter) {
    return function (a, b) {
        var newData = new Date(a);
        var time = $filter('date')(newData, 'yyyy/MM/dd');
        return time;
    }
});