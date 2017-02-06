// 引导页功能
angular.module('WorkPlanIndex.controller', ['WorkPlanIndex.service'])
  .controller('WorkPlanIndexCtrl', function ($scope, WorkPlanIndexFty, $window, $ionicLoading, $location) {

      $scope.goBack = function () {
          window.history.go(-1);
      }

      $scope.$on('$ionicView.afterEnter', function (e) {

      });

      var time = new Date();

      $scope.NewsListData = [];
      $scope.pageSize = 2;
      $scope.pageNum = -1;
      $scope.Year = time.getFullYear();
      $scope.Month = time.getMonth() + 1;

      $('.month_' + $scope.Month).addClass('active');

      $scope.lastMonth = function () {
          if ($scope.Month - 1 == 0) {
              $scope.Month = 12;
              $scope.Year = $scope.Year - 1;
          } else {
              $scope.Month = $scope.Month - 1;
          }
          $('#Month').find('.col').removeClass('active');
          $('.month_' + $scope.Month).addClass('active');
          changeMonth();
      }

      $scope.nextMonth = function () {
          if ($scope.Month + 1 > 12) {
              $scope.Month = 1;
              $scope.Year = $scope.Year + 1;
          } else {
              $scope.Month = $scope.Month + 1;
          }
          $('#Month').find('.col').removeClass('active');
          $('.month_' + $scope.Month).addClass('active');
          changeMonth();
      }

      // 获取最新数据方法
      $scope.list_refresh = function () {
          $scope.pageNum = 0;

          var par = {
              Year: $scope.Year,
              Month: $scope.Month,
              StartRow: $scope.pageNum * $scope.pageSize,
              PageSize: $scope.pageSize,
              Title: "",
              StartTime: "",
              EndTime: ""
          }
          GetDataList(par, function (msg) {
              $scope.NewsListData = msg.Data;
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
          var par = {
              Year: $scope.Year,
              Month: $scope.Month,
              StartRow: $scope.pageNum * $scope.pageSize,
              PageSize: $scope.pageSize,
              Title: "",
              StartTime: "",
              EndTime: ""
          }
          GetDataList(par, function (msg) {
              if (msg.Data.length == 0) {
                  $scope.pms_isMoreItemsAvailable = true;
                  $ionicLoading.hide();
                  return;
              }
              $.each(msg.Data, function (i, item) {
                  $scope.NewsListData.push(item);
              })
              $scope.$broadcast('scroll.infiniteScrollComplete');
              $ionicLoading.hide();
          });
      }

      $('#Month').find('.col').bind('click', function () {
          $ionicLoading.show({
              template: "正在载入数据，请稍后..."
          });
          var self = $(this);
          $scope.Month = self.text();
          $('#Month').find('.col').removeClass('active');
          $(this).addClass('active');
          changeMonth();
      })

      var changeMonth = function () {
          $ionicLoading.show({
              template: "正在载入数据，请稍后..."
          });
          $scope.pageNum = 0;
          var par = {
              Year: $scope.Year,
              Month: $scope.Month,
              StartRow: $scope.pageNum * $scope.pageSize,
              PageSize: $scope.pageSize,
              Title: "",
              StartTime: "",
              EndTime: ""
          }
          GetDataList(par, function (msg) {
              $ionicLoading.hide();
              $scope.NewsListData = msg.Data;
          });
      }

      ///获取管理条例
      GetDataList = function (par, callback) {
          WorkPlanIndexFty.GetWorkPlanList(par, function (msg) {
              callback(msg);
          })
      }

  });

