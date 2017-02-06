// 引导页功能
angular.module('Login.controller', ['Login.service'])
  .controller('LoginCtrl', function ($scope, LoginFty, $window, $location) {

      ///页面加载成功后添加事件
      $scope.$on('$ionicView.afterEnter', function (e) {

      });

      $scope.data = {
          'userName': '',
          'password': ''
      }

      ////用户名
      //$scope.userName = "";
      ////密码
      //$scope.password = "";
      //错误信息
      $scope.errMessage = "";
      //用户名获取焦点事件
      $scope.userNameFouces = function () {
          clearErr();
      }
      $scope.isShow = false;
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


      //验证用户名
      var checkUserName = function () {
          var _userName = $.trim($scope.data.userName);
          if (!_userName) {
              setErr("账号不能为空");
              return false;
          }
          return true;
      }

      //密码获取焦点事件
      $scope.passwordFouces = function () {
          clearErr();
      }

      //验证password
      var checkPassword = function () {
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
      ///登录
      $scope.Login = function () {
          //清空err
          clearErr();
          //验证用户名
          if (!checkUserName()) {
              return;
          }
          //验证密码
          if (!checkPassword()) {
              return;
          }
          var b = new Base64();
          var data = {
              'Param1': $scope.data.userName,
              'Param2': b.encode($scope.data.password),
          }
          LoginFty.checkIn(data, function (data) {
              if (!data || !data.isSuccess) {
                  setErr(data.errMessage);
                  return;
              }
              ChickUserState();
          });
      }

      ///注册
      $scope.Register = function () {
          $location.path('Register');
      }

      //忘记密码
      $scope.ForgetPassword = function () {
          $location.path('Error');
      }

  });

