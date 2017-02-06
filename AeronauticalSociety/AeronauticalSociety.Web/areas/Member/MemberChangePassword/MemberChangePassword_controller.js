// 引导页功能
angular.module('MemberChangePassword.controller', ['MemberChangePassword.service'])
  .controller('MemberChangePasswordCtrl', function ($scope, MemberChangePasswordFty, $window, $location, $ionicLoading) {

      $scope.$on('$ionicView.afterEnter', function (e) {

      });

      $scope.data = {
          'Param1': '',
          'Param2': '',
          'regParam2': ''
      }
      //用户名
      // $scope.Param1 = "";
      $scope.Param1ChekcResult = false;
      $scope.Param1Err = "";
      //获取焦点
      $scope.Param1GetFocus = function () {
          $scope.Param1 = "";
      }
      //失去焦点
      $scope.Param1LoseFocus = function () {
          if (!checkParam1()) {
              return;
          }
      }
      //验证用户名称
      var checkParam1 = function () {
          var _Param1 = $.trim($scope.data.Param1)
          if (!_Param1) {
              $scope.Param1Err = "原密码不能为空";
              $scope.Param1ChekcResult = false;
              return false;
          }
          $scope.Param1Err = "";
          $scope.Param1ChekcResult = true;
          return true;

      }

      //密码
      //$scope.Param2 = "";
      $scope.Param2ChekcResult = false;
      $scope.Param2Err = "";
      $scope.Param2Focus = function () {
          $scope.Param2Err = "";
      }
      $scope.Param2LoseFocus = function () {
          checkParam2();

      }

      var checkParam2 = function () {
          var _Param2 = $.trim($scope.data.Param2)
          if (!_Param2) {
              $scope.Param2Err = "新密码不能为空";
              $scope.Param2ChekcResult = false;
              return false;
          }

          if (_Param2.length < 6) {
              $scope.Param2Err = "密码最小长度必须大于等于6位字符";
              $scope.Param2ChekcResult = false;
              return false;
          }

          $scope.Param2Err = "";
          $scope.Param2ChekcResult = true;
          return true;
      }

      //确认密码
      //  $scope.regParam2 = "";
      $scope.regParam2ChekcResult = false;
      $scope.regParam2Err = "";
      $scope.regParam2Focus = function () {
          $scope.regParam2Err = "";
      }
      $scope.regParam2LoseFocus = function () {
          checkregParam2();

      }
      var checkregParam2 = function () {
          var _Param2 = $.trim($scope.data.Param2);
          var _regParam2 = $.trim($scope.data.regParam2);
          if (_regParam2 != _Param2) {
              $scope.regParam2Err = "密码输入不一致";
              $scope.regParam2ChekcResult = false;
              return false;
          }
          return true;
      }
      //注册
      $scope.change = function () {
          if (!checkParam1()) {
              return;
          }
          if (!checkParam2()) {
              return;
          }
          if (!checkregParam2()) {
              return;
          }
          $ionicLoading.show({
              template: "正在保存数据，请稍后..."
          });
          var b = new Base64();
          //验证用户名是否重复
          MemberChangePasswordFty.UpdatePassword({ 'Param1': b.encode($scope.data.Param1), 'Param2': b.encode($scope.data.Param2) }, function (data) {
              $ionicLoading.hide();
              if (!data) {
                  $scope.regParam2Err = '密码修改失败';
                  //alert('密码修改失败');
                  return;
              }
              if (!data.isSuccess) {
                  $scope.regParam2Err = data.errMessage;
                //  alert(data.errMessage);
                  return;
              }
              $location.path('tab/Login');
          });
      }
      //取消
      $scope.Cancel = function () {
          window.history.go(-1);
      }

  });





