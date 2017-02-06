// 引导页功能
angular.module('AboutLearningHonor.controller', ['AboutLearningHonor.service'])
  .controller('AboutLearningHonorCtrl', function ($scope, AboutLearningHonorFty, $window, $location) {

      $scope.goBack = function () {
          window.history.go(-1);
      }

      $scope.$on('$ionicView.afterEnter', function (e) {

      });
  });

