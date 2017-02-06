// 引导页功能
angular.module('memberDetial.controller', ['memberDetial.service'])
  .controller('memberDetialCtrl', function ($scope, memberDetialFty, $window, $location, $ionicLoading) {

      $scope.$on('$ionicView.afterEnter', function (e) {

      });
      $scope.data = {
          'userName': '',
          'memberCode': '',
          'memberType': '',
          'post': '',
          'email': '',
          'address': '',
          'state': '',
          'sex': '',
          'areaList': [],
          'areaName': '',
          'selArea': 0,
          'branchList': [],
          'selBranch': 0
      }

      var id = "";
      var user = $.cookie("user");
      if (user) {
          var _user = $.parseJSON(user);
          id = _user.id;
      } else { $location.path('tab/login'); }

      //获取会员信息
      memberDetialFty.getMember({ 'Param': id }, function (data) {
          if (!data) {
              alert("数据获取失败");
              return;
          }
          setControl(data);
      });

      //设置控件
      var setControl = function (data) {
          $scope.data.userName = data.memberName;
          $scope.data.memberCode = data.memberID;
          if (data.levelName) {
              $scope.data.memberType = '中国航空学会' + data.levelName;
          }
          $scope.data.areaName = data.areaName;
          $scope.data.branchName = data.branchName;
          $scope.data.post = data.post;
          $scope.data.email = data.email;
          $scope.data.address = data.address;
          $scope.data.state = data.state;
          if (!data.sex) {
              data.sex = 0;
          }
          $scope.data.sex = data.sex == 1 ? '女' : '男';
          $scope.data.selArea = data.areaID;
          $scope.data.selBranch = data.branchID;
      }

      //点击编辑
      $scope.save = function () {
          $location.path('tab/memberDetialEdit')
      }

  });

