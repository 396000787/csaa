// 引导页功能
angular.module('ShowHtml.controller', ['ShowHtml.service'])
  .controller('ShowHtmlCtrl', function ($scope, $state, ShowHtmlFty, $window, $location) {

      $scope.goBack = function () {
          window.history.go(-1);
      }

      $scope.$on('$ionicView.afterEnter', function (e) {

      });
      $scope.title = $state.params.title;
      $scope.key = $state.params.key;

  });

