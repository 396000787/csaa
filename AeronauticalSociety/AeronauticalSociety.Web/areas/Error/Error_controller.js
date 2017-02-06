// 引导页功能
angular.module('Error.controller', ['Error.service'])
  .controller('ErrorCtrl', function ($scope, ErrorFty, $window, $location) {

      $scope.goBack = function () {
          window.history.go(-1);
      }

      $scope.$on('$ionicView.afterEnter', function (e) {

      });

     
  })

