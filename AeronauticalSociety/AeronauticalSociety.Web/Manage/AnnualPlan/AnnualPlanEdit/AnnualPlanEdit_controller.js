angular.module('AnnualPlanEdit.controllers', ['AnnualPlanEdit.service'])

.controller('AnnualPlanEditCtrl', function ($scope, $stateParams, AnnualPlanEditFty, $location) {


    var pageIndex = 0;
    var pageSize = 10;

    $scope.UserModel = {
        id: 0,
        year: "",
        startTime: "",
        endTime: "",
        name: "",
        content: "",
        scale: "",
        address: "",
        addressProvinceID: "",
        seladdressProvinceID: 0,
        addressProvince: "",
        seladdressCityID: 0,
        addressCityID: "",
        addressCity: "",
        Contacts: [],
        areaList: [],
        cityList: []
    };

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

    //获取工作计划详情
    var getWorkPlan = function (wid) {
        AnnualPlanEditFty.GetWorkPlanDetial(wid, function (data) {
            if (!data) {
                alert("数据获取失败");
                return;
            }
            SetControl(data);
        });
    };
    //设置控件值
    var SetControl = function (data) {
        $scope.UserModel.year = data.year;
        $scope.UserModel.startTime = data.startTime;
        $scope.UserModel.endTime = data.endTime;
        $scope.UserModel.name = data.name;
        $scope.UserModel.content = data.content;
        $scope.UserModel.scale = data.scale;
        $scope.UserModel.address = data.address;
        $scope.UserModel.addressProvinceID = data.addressProvinceID;
        $scope.UserModel.addressProvince = data.addressProvince;
        $scope.UserModel.addressCityID = data.addressCityID;
        $scope.UserModel.addressCity = data.addressCity;
        $scope.UserModel.Contacts = data.Contacts;
        $scope.WorkPlanContacts = data.Contacts;
        $("#startTime").val(FormatDate(new Date(data.startTime), 'yyyy/MM/dd'));
        $("#endTime").val(FormatDate(new Date(data.endTime), 'yyyy/MM/dd'));
        getProvinceList();
    }

    $scope.WorkPlanContacts = [];

    $scope.removeContact = function (item) {
        item.statue = 'del';
    }

    var areaChanage = function (id) {
        var key = "";
        //if (id != undefined) {
        //    key = id;
        //} else {
        //    key = $('#CityList').find('option:checked').attr('value');
        //}
        AnnualPlanEditFty.GetCityList(id, function (msg) {
            $scope.UserModel.areaList = msg;
            if ($scope.UserModel.addressCityID) {
                $scope.UserModel.seladdressCityID = $scope.UserModel.addressCityID;
                $('#addressCity').val($scope.UserModel.addressCityID);
            } else {
                $scope.UserModel.seladdressCityID = msg[0].id;
            }
        })
    }

    var getProvinceList = function () {
        AnnualPlanEditFty.GetProvinceList(function (msg) {
            $scope.UserModel.cityList = msg;

            if ($scope.UserModel.addressProvinceID) {
                $scope.UserModel.seladdressProvinceID = $scope.UserModel.addressProvinceID;
                areaChanage($scope.UserModel.addressProvinceID);
            } else {
                $scope.UserModel.seladdressProvinceID = msg[0].id;
                areaChanage(msg[0].id);
            }

        });
    }

    $scope.UserModel.id = $stateParams.id;
    if ($stateParams.id == '000') {
        getProvinceList();
    } else {
        getWorkPlan($stateParams.id);
    }


    $scope.change = function () {
        $scope.UserModel.addressCityID = "";
        areaChanage($scope.UserModel.seladdressProvinceID);
    };

    //判断是编辑还是新建工作计划

    $scope.AddUser = function () {
        if (!checkUserContact()) {
            return;
        }
        var par = {
            phone: $("#AddUserModel_phone").val(),
            contactsName: $("#AddUserModel_contactsName").val(),
            statue: "add"
        }
        $scope.WorkPlanContacts.push(par);
        $('#myModal').modal('hide');
        $("#AddUserModel_phone").val('');
        $("#AddUserModel_contactsName").val('');
        $scope.contactsNameErr = '';
        $scope.phoneErr = '';
    }

    $scope.cancleAddUser = function () {
        $('#myModal').modal('hide');
        $("#AddUserModel_phone").val('');
        $("#AddUserModel_contactsName").val('');
        $scope.contactsNameErr = '';
        $scope.phoneErr = '';
    }

    $scope.Seave = function () {
        //验证数据
        if (!checkData()) {
            return;
        }
        if ($scope.WorkPlanContacts.length > 0) {
            for (var i = 0; i < $scope.WorkPlanContacts.length; i++) {
                var node = $scope.WorkPlanContacts[i];
                var temp = {
                    phone: node.phone,
                    contactsName: node.contactsName,
                    statue: node.statue
                }
                $scope.UserModel.Contacts.push(temp);
            }
        }
        $scope.UserModel.addressProvinceID = $scope.UserModel.seladdressProvinceID;
        $scope.UserModel.addressProvince = $('#CityList').find('option:checked').text();
        $scope.UserModel.addressCityID = $scope.UserModel.seladdressCityID;
        $scope.UserModel.addressCity = $('#addressCity').find('option:checked').text();;
        $scope.UserModel.startTime = $("#startTime").val() + ' 00:00:00';
        $scope.UserModel.endTime = $("#endTime").val() + ' 23:59:59';
        $scope.UserModel.year = $scope.UserModel.startTime.substring(0, 4);
        if ($scope.UserModel.id == '000') {
            AnnualPlanEditFty.AddWorkPlan($scope.UserModel, function (msg) {
                if (!msg) {
                    alert('工作计划保存失败');
                    return;
                }
                $location.path('/AnnualPlanIndex');
            });
        } else {
            AnnualPlanEditFty.UpdateWorkPlan($scope.UserModel, function (msg) {
                if (!msg) {
                    alert('工作计划保存失败');
                    return;
                }
                $location.path('/AnnualPlanIndex');
            });
        }

    }

    //取消
    $scope.cancle = function () {
        $location.path('/AnnualPlanIndex');
    }

    // $('#page').zk_paging();
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
    $('.StartTime').val(FormatDate(new Date(), 'yyyy/MM/dd'));
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
    $('.EndTime').val(FormatDate(new Date(), 'yyyy/MM/dd'));
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

    //数据验证
    $scope.actionNameErr = '';
    $scope.contentErr = '';
    $scope.startTimeErr = '';
    $scope.endTimeErr = '';
    $scope.scaleErr = '';

    //验证活动名称
    var checkactionName = function () {
        if (!$.trim($scope.UserModel.name)) {
            $scope.actionNameErr = '活动名称不能为空';
            return false;
        }
        return true;
    }
    //活动名称获取焦点
    $scope.actionNameFouces = function () {
        $scope.actionNameErr = '';
    }
    //活动名称失去焦点
    $scope.actionNameBlur = function () {
        $scope.actionNameErr = '';
        checkactionName();
    }

    //验证内容及目的
    var checkcontent = function () {
        if (!$.trim($scope.UserModel.content)) {
            $scope.contentErr = '内容及目的不能为空';
            return false;
        }
        return true;
    }
    //内容及目的获取焦点
    $scope.contentFouces = function () {
        $scope.contentErr = '';
    }
    //内容及目的失去焦点
    $scope.contentBlur = function () {
        $scope.contentErr = '';
        checkcontent();
    }

    var checkDate = function (value) {
        var a = /^(\d{4})\/(\d{2})\/(\d{2})$/
        if (!a.test(value)) {
            return false
        }
        else {
            return true
        }
    }

    //验证开始时间
    var checkstartTime = function () {
        $scope.startTimeErr = '';
        var startTime = $.trim($("#startTime").val());
        if (!startTime) {
            $scope.startTimeErr = '开始时间不能为空';
            return false;
        }
        //验证日期格式
        if (!checkDate(startTime)) {
            $scope.startTimeErr = '开始时间日期格式不正确';
            return false;
        }
        return true;
    }
    //开始时间获取焦点
    $scope.startTimeFouces = function () {
        $scope.startTimeErr = '';
    }
    //开始时间失去焦点
    $scope.startTimeBlur = function () {
        $scope.startTimeErr = '';
        checkstartTime();
    }

    //验证结束时间
    var checkendTime = function () {
        var endTime = $.trim($("#endTime").val());

        if (!endTime) {
            $scope.endTimeErr = '结束时间不能为空';
            return false;
        }
        //验证日期格式
        if (!checkDate(endTime)) {
            $scope.endTimeErr = '结束时间日期格式不正确';
            return false;
        }
        var startTime = $.trim($("#startTime").val());
        if (!startTime) {
            return true;
        }
        //验证开始时间
        if (!checkstartTime()) {
            return false;
        }
        //比较开始时间和结束时间
        if (new Date(startTime) > new Date(endTime)) {
            $scope.endTimeErr = '结束时间日期格式不正确';
            return false;
        }
        return true;
    }
    //结束时间获取焦点
    $scope.endTimeFouces = function () {
        $scope.endTimeErr = '';
    }
    //结束时间失去焦点
    $scope.endTimeBlur = function () {
        $scope.endTimeErr = '';
        checkendTime();
    }
    //验证数值
    var checkNub = function (str) {
        var r = /^\+?[1-9][0-9]*$/;　　//正整数 
        return r.test(str);
    }
    //验证规模
    var checkscale = function () {
        $scope.scaleErr = '';
        var scale = $scope.UserModel.scale;
        if (!scale) {
            $scope.scaleErr = '规模不能为空';
            return false;
        }
        //验证数字
        if (!checkNub(scale)) {
            $scope.scaleErr = '规模只能输入数据';
            return false;
        }
        return true;
    }
    //开始时间获取焦点
    $scope.scaleFouces = function () {
        $scope.scaleErr = '';
    }
    //开始时间失去焦点
    $scope.scaleBlur = function () {
        $scope.scaleErr = '';
        checkscale();
    }

    //验证数据
    var checkData = function () {
        if (!checkactionName()) {
            return false;
        }
        if (!checkcontent()) {
            return false;
        }
        if (!checkstartTime()) {
            return false;
        }
        if (!checkendTime()) {
            return false;
        }
        if (!checkscale()) {
            return false;
        }

        return true;
    }

    //
    $scope.contactsNameErr = '';
    $scope.phoneErr = '';
    //验证用户名
    var checkcontactsName = function () {
        var contactsName = $.trim($('#AddUserModel_contactsName').val());
        if (!contactsName) {
            $scope.contactsNameErr = '用户名不能为空';
            return false;
        }
        return true;
    }
    //用户名获取焦点
    $scope.contactsNameFouces = function () {
        $scope.contactsNameErr = '';
    }
    //用户名失去焦点
    $scope.contactsNameBlur = function () {
        $scope.contactsNameErr = '';
        checkcontactsName();
    }

    //验证联系电话
    var checkphone = function () {
        var phone = $.trim($('#AddUserModel_phone').val());
        if (!phone) {
            $scope.phoneErr = '联系电话不能为空';
            return false;
        }
        var str = /(^[0-9]{3,4}\-[0-9]{3,8}$)|(^[0-9]{3,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)/;
        if (!str.test(phone)) {
            $scope.phoneErr = '联系电话格式不正确';
            return false;
        }
        return true;
    }
    //联系电话获取焦点
    $scope.phoneFouces = function () {
        $scope.phoneErr = '';
    }
    //联系电话失去焦点
    $scope.phoneBlur = function () {
        $scope.phoneErr = '';
        checkphone();
    }
    //验证联系人
    var checkUserContact = function () {
        if (!checkcontactsName()) {
            return false;
        }
        if (!checkphone()) {
            return false;
        }
        return true;
    }

});