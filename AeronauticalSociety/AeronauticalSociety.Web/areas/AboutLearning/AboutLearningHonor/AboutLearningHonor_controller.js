// 引导页功能
angular.module('AboutLearningHonor.controller', ['AboutLearningHonor.service'])
  .controller('AboutLearningHonorCtrl', function ($scope, $ionicLoading, AboutLearningHonorFty, $window, $location) {

      $scope.goBack = function () {
          window.history.go(-1);
      }

      $scope.$on('$ionicView.afterEnter', function (e) {

      });

      $scope.openLink = function (item) {
          $location.path('/tab/AboutLearningShowHtml/学会荣誉/' + item.id + '/16');
      }

      $scope.NewsListData = [];
      $scope.pageSize = 10;
      $scope.pageNum = -1;

      // 获取最新数据方法
      $scope.list_refresh = function () {
          $scope.pageNum = 0;
          GetHonorList({ StartRow: $scope.pageNum * $scope.pageSize, PageSize: $scope.pageSize }, function (msg) {
              $scope.NewsListData = msg;
              // 停止广播ion-refresher
              $scope.$broadcast('scroll.refreshComplete');
          });
      }

      // 加载更多数据方法
      $scope.page_refresh = function () {
          $ionicLoading.show({
              template: "正在载入数据，请稍后..."
          });
          $scope.pageNum = $scope.pageNum + 1;
          GetHonorList({ StartRow: $scope.pageNum * $scope.pageSize, PageSize: $scope.pageSize }, function (msg) {
              if (msg.length == 0) {
                  $scope.pms_isMoreItemsAvailable = true;
                  $ionicLoading.hide();
                  return;
              }
              $.each(msg, function (i, item) {
                  $scope.NewsListData.push(item);
              })
              $scope.$broadcast('scroll.infiniteScrollComplete');
              $ionicLoading.hide();
          });
      }

      ///获取管理条例
      GetHonorList = function (par, callback) {
          AboutLearningHonorFty.GetHonorList(par, function (msg) {
              callback(msg);
          })
      }

  });

