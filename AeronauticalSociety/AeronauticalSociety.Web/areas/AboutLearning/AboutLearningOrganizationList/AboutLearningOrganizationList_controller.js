// 引导页功能
angular.module('AboutLearningOrganizationList.controller', ['AboutLearningOrganizationList.service'])
  .controller('AboutLearningOrganizationListCtrl', function ($scope, $ionicLoading, $location, $state, AboutLearningOrganizationListFty, $window, $location) {
      $scope.goBack = function () {
          window.history.go(-1);
      }

      $scope.key = $state.params.key;
      $scope.title = $state.params.title;

      $scope.openLink = function (item) {
          $location.path('/tab/AboutLearningShowHtml/' + item.title + '/' + item.id + '/' + $scope.key);
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
              $.each(msg, function (i, item) {
                  $scope.NewsListData.push(item);
              })
              $scope.$broadcast('scroll.infiniteScrollComplete');
              $ionicLoading.hide();
          });
      }


      var GetData = function (par, callback) {
          switch ($scope.key) {
              case "122":
                  GetWorkingCommitteeList(par, callback);
                  break;
              case "118":
                  GetProfessionalBranchList(par, callback);
                  break;
              case "117":
                  GetLocalAssociationList(par, callback);
                  break;
              case "124":
                  GetProfessionalBranchList(par, callback);
                  break;
          }
      }

      var GetWorkingCommitteeList = function (par, callback) {
          AboutLearningOrganizationListFty.GetWorkingCommitteeList(par, callback);
      }

      var GetProfessionalBranchList = function (par, callback) {
          AboutLearningOrganizationListFty.GetProfessionalBranchList(par, callback);
      }

      var GetLocalAssociationList = function (par, callback) {
          AboutLearningOrganizationListFty.GetLocalAssociationList(par, callback);
      }

      var GetUnitMemberList = function (par, callback) {
          AboutLearningOrganizationListFty.GetUnitMemberList(par, callback);
      }


  });

