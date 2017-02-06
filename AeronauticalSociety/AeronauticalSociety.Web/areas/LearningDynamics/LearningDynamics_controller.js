// 引导页功能
angular.module('LearningDynamics.controller', ['LearningDynamics.service'])
  .controller('LearningDynamicsCtr', function ($scope, LearningDynamicsFty, $window, $location) {
      $scope.goBack = function () {
          //window.history.go(-1);
          $location.path('tab/home');
      }

      $scope.clickImgNode = function (item) {
          switch (item.ID) {
              case 1:
                  $location.path('LearningDynamicsList');
                  break;
              case 3:
                  $location.path('noticeList');
                  break;
              default:
                  $location.path('Error');
                  break;
          }
          //if (item.MenuName == "学会工作") {
          //    $location.path('LearningDynamicsList/' + item.TypeID);
          //}
          //if (item.MenuName == "通知通告") {
          //    $location.path('noticeList/' + item.TypeID);
          //}

      }

      GetListDataSource();
      ///初始化滚动条
      function GetListDataSource() {
          LearningDynamicsFty.ListDataSource(function (msg) {
              $scope.ListDataSource = msg;
          });
      }

  });

