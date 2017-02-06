// 引导页功能
angular.module('Demo.controller', ['Demo.service'])
  .controller('DemoCtrl', function ($scope, DemoFty, $window, $location) {

      $scope.$on('$ionicView.afterEnter', function (e) {

      });
  });

