// 引导页功能
angular.module('memberBind.controller', ['memberBind.service'])
  .controller('memberBindCtrl', function ($scope, memberBindFty, $window, $location, $ionicLoading) {

      $scope.$on('$ionicView.afterEnter', function (e) {

      });
      $scope.data = {
          'userName': '',
          'memberCode': ''
      }
      $scope.userNameErr = "";
      $scope.memberCodeErr = "";


      $scope.userNameGetFocus = function () {
          $scope.userNameErr = "";
      }
      //失去焦点
      $scope.userNameLoseFocus = function () {
          if (!checkUserName()) {
              return;
          }
      }
      //验证用户名称
      var checkUserName = function () {
          var _userName = $.trim($scope.data.userName)
          if (!_userName) {
              $scope.userNameErr = "会员姓名不能为空";
              return false;
          }
          $scope.userNameErr = "";
          return true;
      }


      $scope.memberCodeGetFocus = function () {
          $scope.memberCodeErr = "";
      }
      //失去焦点
      $scope.memberCodeLoseFocus = function () {
          if (!checkmemberCode()) {
              return;
          }
      }
      //验证会员证号
      var checkmemberCode = function () {
          var _memberCode = $.trim($scope.data.memberCode)
          if (!_memberCode) {
              $scope.memberCodeErr = "会员证号不能为空";
              return false;
          }
          $scope.memberCodeErr = "";
          return true;
      }


      //判断用户状态
      var user = $.cookie("user");
      var _user = null;
      if (user) {
          var _user = $.parseJSON(user);
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
              $scope.isShow = true;
          }
      }

      ChickUserState();
      //绑定
      $scope.bindMember = function () {
          if (!user) {
              $location.path('tab/login');
              return;
          }
          //验证数据
          if (!checkUserName() || !checkmemberCode()) {
              return;
          }
          var data = { 'UserName': $scope.data.userName, 'MemberCode': $scope.data.memberCode, 'UserKey': _user.userKey }
          $ionicLoading.show({
              template: "正在保存数据，请稍后..."
          });
          memberBindFty.bindMember(data, function (data) {
              $ionicLoading.hide();
              if (!data) {
                  alert('会员绑定失败');
                  return;
              }
              if (!data.isSuccess) {
                  alert(data.errMessage);
                  return;
              }
              $location.path('tab/memberDetial');
          });
      }
  });

