// 引导页功能
angular.module('CollectionIndex.controller', ['CollectionIndex.service'])
  .controller('CollectionIndexCtrl', function ($scope, $ionicLoading, CollectionIndexFty, $window, $location) {

      $scope.goBack = function () {
          window.history.go(-1);
      }

      $scope.$on('$ionicView.afterEnter', function (e) {

      });


      ///获取当期人员信息
      var _user = $.cookie("user");

      if (!_user) {
          $location.path('/tab/Login');
          return false;
      }


      $scope.openLink = function (item) {
          $location.path('/AviationDetails/' + item.id);
      }

      $scope.NewsListData = [];
      $scope.pageSize = 10;
      $scope.pageNum = -1;

      // 获取最新数据方法
      $scope.list_refresh = function () {
          $scope.pageNum = 0;
          GetData({ StartRow: $scope.pageNum * $scope.pageSize, PageSize: $scope.pageSize }, function (msg) {
              $scope.NewsListData = msg;
              $scope.pms_isMoreItemsAvailable = false;
              // 停止广播ion-refresher
              $scope.$broadcast('scroll.refreshComplete');
              $scope.$broadcast('scroll.infiniteScrollComplete');
          });
      }

      // 加载更多数据方法
      $scope.page_refresh = function () {
          $scope.pms_isMoreItemsAvailable = false;
          $ionicLoading.show({
              template: "正在载入数据，请稍后..."
          });
          $scope.pageNum = $scope.pageNum + 1;
          GetData({ StartRow: $scope.pageNum * $scope.pageSize, PageSize: $scope.pageSize }, function (msg) {            
              if (msg.length == 0) {
                  $scope.pms_isMoreItemsAvailable = true;
                  $ionicLoading.hide();
                  return;
              }
              if (msg.length < 10) {
                  $scope.pms_isMoreItemsAvailable = true;
              }
              $.each(msg, function (i, item) {
                  $scope.NewsListData.push(item);
              })
              $scope.$broadcast('scroll.infiniteScrollComplete');
              $ionicLoading.hide();
          });
      }

      var GetData = function (par, callback) {
          CollectionIndexFty.GetWorkingCommitteeList(par, callback);
      }

  });

