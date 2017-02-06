// 引导页功能
angular.module('AboutLearningRegulations.controller', ['AboutLearningRegulations.service'])
  .controller('AboutLearningRegulationsCtrl', function ($scope, $ionicLoading, AboutLearningRegulationsFty, $window, $location) {
      $scope.goBack = function () {
          window.history.go(-1);
      }


      $scope.NewsListData = [];
      $scope.pageSize = 10;
      $scope.pageNum = -1;

      $scope.oplink = function (item) {
          $location.path('/tab/AboutLearningShowHtml/' + item.title + '/' + item.id + '/337');
      }


      // 获取最新数据方法
      $scope.list_refresh = function () {
          $scope.pageNum = 0;
          GetManageRuleses({ StartRow: $scope.pageNum * $scope.pageSize, PageSize: $scope.pageSize }, function (msg) {
              $scope.NewsListData = msg;
              $scope.pms_isMoreItemsAvailable = false;
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
          GetManageRuleses({ StartRow: $scope.pageNum * $scope.pageSize, PageSize: $scope.pageSize }, function (msg) {
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
      GetManageRuleses = function (par, callback) {
          AboutLearningRegulationsFty.GetManageRuleses(par, callback)
      }

  });

