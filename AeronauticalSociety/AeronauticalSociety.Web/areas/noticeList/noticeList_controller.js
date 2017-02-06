// 引导页功能
angular.module('noticeList.controller', ['noticeList.service'])
  .controller('noticeListCrt', function ($scope, $stateParams, noticeListFty, $location, $window, $ionicLoading) {
      $scope.NewsListData = [];
      $scope.pageSize = 10;
      $scope.pageNum = 0;
      $scope.goBack = function () {
          window.history.go(-1);
      }

      // 事件监听
      $scope.$on('$ionicView.beforeEnter', function (e) {
          $scope.func_refreshGoodsList();
      });

      ///调转详情
      $scope.details = function (item) {
          $location.path('AviationDetails/' + item.id);
      };

      // 获取最新数据方法
      $scope.func_refreshGoodsList = function () {
          $scope.pageNum = 0;
          noticeListFty.GetNewsListData({ StartRow: $scope.pageNum * $scope.pageSize, PageSize: $scope.pageSize }, function (msg) {
              $scope.NewsListData = msg;
              // 停止广播ion-refresher          
              $scope.$broadcast('scroll.infiniteScrollComplete');
              $scope.$broadcast('scroll.refreshComplete');
          });
      }
      var isLock = false;
      // 加载更多数据方法
      $scope.func_loadMoreGoodsList = function () {
          if (!isLock) {
              isLock = true;
              return;
          }
          $ionicLoading.show({
              template: "正在载入数据，请稍后..."
          });
          $scope.pageNum = $scope.pageNum + 1;
          noticeListFty.GetNewsListData({ StartRow: $scope.pageNum * $scope.pageSize, PageSize: $scope.pageSize }, function (msg) {
              if (msg.length == 0) {
                  $scope.pms_isMoreItemsAvailable = true;
                  $ionicLoading.hide();
                  return;
              }
              $.each(msg, function (i, item) {
                  $scope.NewsListData.push(item);
              })
              $scope.$broadcast('scroll.infiniteScrollComplete');
              $scope.$broadcast('scroll.refreshComplete');
              $ionicLoading.hide();
          });
      }
  }).filter('DateFormat', function ($filter) {
      return function (a, b) {
          var newData = new Date(a * 1000);
          var time = $filter('date')(newData, 'yyyy-MM-dd h:mm:ss');
          return time;
      }
  });

