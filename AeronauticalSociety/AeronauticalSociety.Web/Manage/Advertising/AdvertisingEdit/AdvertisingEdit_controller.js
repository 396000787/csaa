angular.module('AdvertisingEdit.controllers', ['AdvertisingEdit.service', 'blueimp.fileupload'])
.controller('AdvertisingEditCtrl', function ($scope, $stateParams, AdvertisingEditFty, $location) {

    $scope.KeyWord = "";
    $scope.userSelectTitle = "";
    $scope.userSelectValue = "";
    $scope.jianjie = "";
    $scope.currentID = '';
    var imgPath = "";

    var pageIndex = 0;
    var pageSize = 10;

    var pageCallbakc = function (_pageIndex) {
        pageIndex = _pageIndex;
        searchaddress();
    }

    var searchaddress = function () {
        var par = {
            StartRow: (pageIndex == 0 ? 0 : pageIndex - 1) * pageSize,
            PageSize: pageSize,
            TypeID: '7,114',
            SearchKey: $scope.KeyWord
        };
        AdvertisingEditFty.GetNewsByTypeID(par, function (msg) {
            $scope.sDataList = msg.Data;
            $('#page').zk_paging({
                totalContent: msg.Total,
                pageSize: pageSize,
                pageIndex: pageIndex,
                pageCallback: pageCallbakc
            });
        })
    }
    $scope.searchaddress = function () {
        pageIndex = 0;
        searchaddress();
    }

    $scope.CreateUrl = function (item) {
        $scope.userSelectTitle = item.title;
        $scope.userSelectValue = '/' + item.id;
    }

    if ($stateParams.id != 000) {
        AdvertisingEditFty.GetAdvertisement($stateParams.id, function (msg) {
            $scope.KeyWord = "";
            $scope.userSelectTitle = msg.title;
            $scope.userSelectValue = msg.targetUrl;
            $scope.jianjie = msg.title;
            imgPath = msg.imageUrl;
            $('#userImage').attr('src', imgPath);
            $scope.currentID = msg.id;
        })
    }



    $scope.cancle = function () {
        $location.path('/AdvertisingIndex');
    }

    $scope.Seave = function () {
        //验证广告图片是否为空
        if (!imgPath) {
            alert("广告图片不能为空");
            return false;
        }
        var par = {
            title: $scope.jianjie,
            targetUrl: $scope.userSelectValue,
            imageUrl: imgPath
        }
        if ($stateParams.id == 000) {
            AdvertisingEditFty.InsterAdvertisement(par, function (msg) {
                if (msg == true) {
                    $location.path('/AdvertisingIndex');
                }
            })
        } else {
            par.id = $scope.currentID;
            AdvertisingEditFty.UpdateAdvertisement(par, function (msg) {
                if (msg == true) {
                    $location.path('/AdvertisingIndex');
                }
            })
        }
    }

    $("#imsgess").fileupload({
        url: window.location.origin + '/api/FileUploadApi/FileUpload',
        autoUpload: true,//是否自动上传  
        sequentialUploads: true,
        dataType: 'json',//返回数据类型
        success: function (data) {
            imgPath = '/temp/' + data.FilesInfo[0].URL;
            $('#userImage').attr('src', imgPath);
        }
    });
    //  $('#page').zk_paging();

});