// 引导页功能
angular.module('followList.controller', ['followList.service'])
  .controller('followListCtrl', function ($scope, $state, $ionicLoading, followListFty, $window, $location) {

      $scope.goBack = function () {
          window.history.go(-1);
      }

      $scope.$on('$ionicView.afterEnter', function (e) {

      });

      $scope.openLink = function (item) {
          $location.path('/AviationDetails/' + item.id);
      }

      $scope.NewsListData = [];
      $scope.pageSize = 10;
      $scope.pageNum = -1;

      // 获取最新数据方法
      $scope.list_refresh = function () {
          $scope.pageNum = 0;

          var par = {
              StartRow: $scope.pageNum * $scope.pageSize,
              PageSize: $scope.pageSize,
              Author: $state.params.key
          }
          GetData(par, function (msg) {
              alert(JSON.stringify(msg));
              $scope.NewsListData = msg.Data;
              if (msg.Total < $scope.pageSize) {
                  $scope.pms_isMoreItemsAvailable = false;
              }
              // 停止广播ion-refresher
              $scope.$broadcast('scroll.refreshComplete');
              $scope.$broadcast('scroll.infiniteScrollComplete');
          });
      }

      // 加载更多数据方法
      $scope.page_refresh = function () {
          $ionicLoading.show({
              template: "正在载入数据，请稍后..."
          });
          $scope.pageNum = $scope.pageNum + 1;
          var par = {
              StartRow: $scope.pageNum * $scope.pageSize,
              PageSize: $scope.pageSize,
              Author: $state.params.key
          }
          GetData(par, function (msg) {
              if (msg.Data.length == 0) {
                  $scope.pms_isMoreItemsAvailable = true;
                  $ionicLoading.hide();
                  return;
              }
              if (msg.Data.length < 10) {
                  $scope.pms_isMoreItemsAvailable = true;
              }
              $.each(msg.Data, function (i, item) {
                  $scope.NewsListData.push(item);
              })
              $scope.$broadcast('scroll.refreshComplete');
              $scope.$broadcast('scroll.infiniteScrollComplete');
              $ionicLoading.hide();
          });
      }

      var GetData = function (par, callback) {
          followListFty.GetNewsByTypeID(par, callback);
      }

  });

