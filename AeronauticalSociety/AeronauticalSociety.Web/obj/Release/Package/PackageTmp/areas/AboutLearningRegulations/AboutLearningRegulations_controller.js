// 引导页功能
angular.module('AboutLearningRegulations.controller', ['AboutLearningRegulations.service'])
  .controller('AboutLearningRegulationsCtrl', function ($scope, AboutLearningRegulationsFty, $window, $location) {
      $scope.goBack = function () {
          window.history.go(-1);
      }
      $scope.$on('$ionicView.afterEnter', function (e) {

      });
  });

