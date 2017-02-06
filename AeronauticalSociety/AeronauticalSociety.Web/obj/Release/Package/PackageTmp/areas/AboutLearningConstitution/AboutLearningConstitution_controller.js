// 引导页功能
angular.module('AboutLearningConstitution.controller', ['AboutLearningConstitution.service'])
  .controller('AboutLearningConstitutionCtrl', function ($scope, AboutLearningConstitutionFty, $window, $location) {
      $scope.goBack = function () {
          window.history.go(-1);
      }
      $scope.$on('$ionicView.afterEnter', function (e) {

      });
  });

