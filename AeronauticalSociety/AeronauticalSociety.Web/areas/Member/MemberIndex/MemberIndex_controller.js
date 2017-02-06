// 引导页功能
angular.module('MemberIndex.controller', ['MemberIndex.service'])
  .controller('MemberIndexCtrl', function ($scope, MemberIndexFty, $window, $location) {

      $scope.$on('$ionicView.afterEnter', function (e) {

      });

      ///获取当期人员信息
      var _user = $.cookie("user");

      if (!_user) {
          $location.path('/tab/Login');
      }
      var CurrentInfoMation = $.parseJSON(_user);
      $scope.loginName = CurrentInfoMation.loginName;
      $scope.memberCode = CurrentInfoMation.memberCode;
      $scope.levelName = CurrentInfoMation.levelName;


      if (CurrentInfoMation.memberCode) {
          $scope.isShow = true;
      } else {
          $scope.isShow = false;
      }

      $scope.Follow = function () {
          $location.path('tab/followIndex');
      }

      $scope.Collection = function () {
          $location.path('tab/CollectionIndex');
      }

      $scope.Release = function () {
          //alert('Release');
      }

      $scope.SeeUserInfo = function () {
          //if ($scope.memberCode == "" || $scope.levelName == "") {
          //    return false;
          //}
          $location.path('tab/memberDetial');
      }

      $scope.bindUserInfo = function () {
          $location.path('tab/memberBind');
      }

      $scope.Exit = function () {
          MemberIndexFty.CheckOut(function (msg) {
              $location.path('tab/home');
          })
      }

      $scope.changPassword = function () {
          $location.path('tab/MemberChangePassword');
      }

  });

