// 引导页功能
angular.module('Login.controller', ['Login.service'])
  .controller('LoginCtrl', function ($scope, LoginFty, $window, $location) {
      $('body').find('#navigation').hide();
      $scope.data = {
          'userName': '',
          'password': ''
      }
      $scope.errMessage = "";
      //验证用户名是否为空
      var checkUserName = function () {
          var _userName = $.trim($scope.data.userName);
          if (!_userName) {
              setErr("账号不能为空");
              return false;
          }
          return true;
      }
      //验证密码是否为空
      var checkUserPasswod = function () {
          var _password = $.trim($scope.data.password);
          if (!_password) {
              setErr("登录密码不能为空");
              return false;
          }
          return true;
      }
      //设置err
      var setErr = function (message) {
          $scope.errMessage = message;
      }
      //清空err
      var clearErr = function () {
          $scope.errMessage = "";
      }
      //用户名获取焦点
      $scope.userNameFouces = function () {
          clearErr();
      }
      //用户名失去焦点
      $scope.userNameLoseFouces = function () {
          checkUserName();
      }
      //用户密码获取焦点
      $scope.userPasswordFouces = function () {
          clearErr();
      }
      //用户密码失去焦点
      $scope.userPasswordLoseFouces = function () {
          checkUserPasswod();
      }

      $scope.btn_Login = function () {
          clearErr();
          if (!checkUserName()) {
              return false;
          }
          if (!checkUserPasswod()) {
              return false;
          }
          var b = new Base64();
          LoginFty.checkIn({ 'Param1': $scope.data.userName, 'Param2': b.encode($scope.data.password) }, function (data) {
              if (!data) {
                  setErr("用户登录失败");
                  return;
              }
              if (!data.isSuccess) {
                  setErr(data.errMessage);
                  return;
              }
              ///验证成功后显示主导航
              $('body').find('#navigation').show();
              $location.path('AdvertisingIndex');
          });

      }
  });

