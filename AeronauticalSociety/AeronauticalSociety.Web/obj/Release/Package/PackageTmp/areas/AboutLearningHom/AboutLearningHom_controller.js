// 引导页功能
angular.module('AboutLearningHom.controller', ['AboutLearningHom.service'])
  .controller('AboutLearningHomCtrl', function ($scope, AboutLearningHomFty, $window, $location) {

      $scope.$on('$ionicView.afterEnter', function (e) {

      });

      ///查看全部
      $scope.SeeProfile = function () {
          $location.path("tab/AboutLearningIntroduction");
      }

      ///学会章程
      $scope.constitution = function () {
          $location.path('/ShowHtml/学会章程/AboutLearningConstitution');
      }

      ///管理条例
      $scope.Regulations = function () {
          $location.path('/tab/AboutLearningRegulations');
      }

      ///学会荣誉
      $scope.Honor = function () {
          $location.path('tab/AboutLearningHonor');
      }

      ///组织机构
      $scope.Organization = function () {
          $location.path('tab/AboutLearningOrganization');
      }

  });

