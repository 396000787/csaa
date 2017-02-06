angular.module('AdvertisingIndex.controllers', ['AdvertisingIndex.service'])

.controller('AdvertisingIndexCtrl', function ($scope, AdvertisingIndexFty, $location) {


    var pageIndex = 0;
    var pageSize = 10;

    var pageCallback = function (_pageIndex) {
        pageIndex = _pageIndex;
        GetDataList();
    }

    var GetDataList = function () {
        AdvertisingIndexFty.GetAdvertisementes({ StartRow: (pageIndex == 0 ? 0 : pageIndex - 1) * pageSize, PageSize: pageSize }, function (msg) {
            $scope.listData = msg.Data;
            $('#page').zk_paging({
                totalContent: msg.Total,
                pageSize: pageSize,
                pageIndex: pageIndex,
                pageCallback: pageCallback
            });
        })
    }

    GetDataList();

    $scope.new = function () {
        $location.path('AdvertisingEdit/000');
    }

    $scope.Disable = function (item) {
        AdvertisingIndexFty.StopAdvertisement(item.id, function (msg) {
            GetDataList();
        })
    }

    $scope.Edit = function (item) {
        $location.path('AdvertisingEdit/' + item.id);
    }

    $scope.Delete = function (item) {
        var isSure = confirm("您将要删除【" + item.title + '】是否继续？');
        if (!isSure) {
            return;
        }
        AdvertisingIndexFty.DelAdvertisement(item.id, function (msg) {
            GetDataList();
        })
    }

    $scope.StartAdvertisement = function (item) {
        AdvertisingIndexFty.StartAdvertisement(item.id, function (msg) {
            GetDataList();
        })
    }

    $('#page').zk_paging();

});