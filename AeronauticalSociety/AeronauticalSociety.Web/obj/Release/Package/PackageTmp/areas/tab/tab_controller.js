// 引导页功能
angular.module('ssstab.controller', ['tab.service'])
  .controller('ssstabCtrl', function ($scope, tabFty, $window, $location) {

      $scope.$on('$ionicView.afterEnter', function (e) {
      });
      $scope.href = '#/tab/Login';
      $scope.accountClick = function () {
          ChickUserState()
      }

      $scope.LearningDynamics = function () {
          $location.path("tab/LearningDynamics");
      }

      $scope.home = function () {
          $location.path("tab/home");
      }

      $scope.Collection = function () {
          $location.path("tab/Collection");
      }

      //判断用户状态
      var ChickUserState = function () {
          var user = $.cookie("user");

          if (user) {
              var _user = $.parseJSON(user);
              if (_user.memberCode) {
                  $location.path('tab/memberDetial');
                  return;
              } else {
                  $location.path('tab/memberBind');
                  return;
              }
          } else {
              $location.path('tab/Login');
          }
      }

  });

