// 引导页功能
angular.module('newsCenter.controller', ['newsCenter.service'])
  .controller('newsCenter', function ($scope, newsCenterFty, $window, $location) {
      $scope.goBack = function () {
          $location.path('tab/home');
      }

      $scope.clickImgNode = function (item) {
          switch (item.ID) {
              case 1:
                  $location.path('newsAviation');
                  break;
              default:
                  $location.path('Error');
                  break;
          }
      }

      GetListDataSource();
      ///初始化滚动条
      function GetListDataSource() {
          newsCenterFty.ListDataSource(function (msg) {
              $scope.ListDataSource = msg;
          });
      }

  });

