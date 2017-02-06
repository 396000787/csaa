// 引导页功能
angular.module('followIndex.controller', ['followIndex.service'])
  .controller('followIndexCtrl', function ($scope, $ionicLoading, followIndexFty, $window, $location) {

      $scope.goBack = function () {
          window.history.go(-1);
      }

      $scope.$on('$ionicView.afterEnter', function (e) {

      });

      $scope.openLink = function (item) {
          $location.path('tab/followList/' + item.authorName)
      }

      $scope.NewsListData = [];
      $scope.pageSize = 10;
      $scope.pageNum = -1;

      // 获取最新数据方法
      $scope.list_refresh = function () {
          $scope.pageNum = 0;
          GetData({ StartRow: $scope.pageNum * $scope.pageSize, PageSize: $scope.pageSize }, function (msg) {
              $scope.NewsListData = msg;
              $scope.pms_isMoreItemsAvailable = true;
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
              $scope.$broadcast('scroll.refreshComplete');
              $scope.$broadcast('scroll.infiniteScrollComplete');
              $ionicLoading.hide();
          });
      }

      var GetData = function (par, callback) {
          followIndexFty.GetWorkingCommitteeList(par, callback);
      }

  });

