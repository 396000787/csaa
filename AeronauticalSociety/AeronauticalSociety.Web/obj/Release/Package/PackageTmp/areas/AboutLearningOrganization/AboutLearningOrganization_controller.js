// 引导页功能
angular.module('AboutLearningOrganization.controller', ['AboutLearningOrganization.service'])
  .controller('AboutLearningOrganizationCtrl', function ($scope, AboutLearningOrganizationFty, $window, $location) {
      $scope.goBack = function () {
          window.history.go(-1);
      }
      $scope.$on('$ionicView.afterEnter', function (e) {

      });
  });

