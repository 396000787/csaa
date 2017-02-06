// 引导页功能
angular.module('Register.controller', ['Register.service'])
  .controller('RegisterCtrl', function ($scope, RegisterFty, $window, $location, $ionicLoading) {

      $scope.$on('$ionicView.afterEnter', function (e) {

      });

      $scope.data = {
          'userName': '',
          'password': '',
          'regPassword': ''
      }
      //用户名
      // $scope.userName = "";
      $scope.userNameChekcResult = false;
      $scope.userNameErr = "";
      //获取焦点
      $scope.userNameGetFocus = function () {
          $scope.userNameErr = "";
      }
      //失去焦点
      $scope.userNameLoseFocus = function () {
          if (!checkUserName()) {
              return;
          }
          //验证用户名是否重复
          RegisterFty.LoginNameIsRepeat({ 'Param': $.trim($scope.userName) }, function (data) {
              if (data) {
                  $scope.userNameErr = "账号已经存在";
              } else {
                  $scope.userNameErr = "";
              }
          });

      }
      //验证用户名称
      var checkUserName = function () {
          var _userName = $.trim($scope.data.userName)
          if (!_userName) {
              $scope.userNameErr = "账号不能为空";
              $scope.userNameChekcResult = false;
              return false;
          }
          if (_userName.length < 3) {
              $scope.userNameErr = "账号最小长度必须大于3位字符";
              $scope.userNameChekcResult = false;
              return false;
          }
          $scope.userNameErr = "";
          $scope.userNameChekcResult = true;
          return true;

      }

      //密码
      //$scope.password = "";
      $scope.passwordChekcResult = false;
      $scope.passwordErr = "";
      $scope.passwordFocus = function () {
          $scope.passwordErr = "";
      }
      $scope.passwordLoseFocus = function () {
          checkPassword();

      }

      var checkPassword = function () {
          var _password = $.trim($scope.data.password)
          if (!_password) {
              $scope.passwordErr = "用户密码不能为空";
              $scope.passwordChekcResult = false;
              return false;
          }

          if (_password.length < 6) {
              $scope.passwordErr = "用户密码最小长度必须大于6位字符";
              $scope.passwordChekcResult = false;
              return false;
          }

          $scope.passwordErr = "";
          $scope.passwordChekcResult = true;
          return true;
      }

      //确认密码
      //  $scope.regPassword = "";
      $scope.regPasswordChekcResult = false;
      $scope.regPasswordErr = "";
      $scope.regPasswordFocus = function () {
          $scope.regPasswordErr = "";
      }
      $scope.regPasswordLoseFocus = function () {
          checkRegPassword();

      }
      var checkRegPassword = function () {
          var _password = $.trim($scope.data.password);
          var _regPassword = $.trim($scope.data.regPassword);
          if (_regPassword != _password) {
              $scope.regPasswordErr = "密码输入不一致";
              $scope.regPasswordChekcResult = false;
              return false;
          }
          return true;
      }
      //注册
      $scope.Reg = function () {
          if (!checkUserName()) {
              return;
          }
          if (!checkPassword()) {
              return;
          }
          if (!checkRegPassword()) {
              return;
          }
          $ionicLoading.show({
              template: "正在保存数据，请稍后..."
          });
          //验证用户名是否重复
          RegisterFty.LoginNameIsRepeat({ 'Param': $.trim($scope.data.userName) }, function (data) {
              if (data) {
                  $scope.userNameErr = "用户名已经存在";
                  $ionicLoading.hide();
              } else {
                  var b = new Base64();
                  var user = { 'loginName': $scope.data.userName, 'password': b.encode($scope.data.password), }
                  RegisterFty.RegUser(user, function (result) {
                      $ionicLoading.hide();
                      if (result) {
                          $location.path('tab/Login');
                          return;
                      }
                      alert('用户注册失败');
                  });
              }
          });
      }
      //取消
      $scope.Cancel = function () {
          $location.path('tab/Login');
      }

  });





