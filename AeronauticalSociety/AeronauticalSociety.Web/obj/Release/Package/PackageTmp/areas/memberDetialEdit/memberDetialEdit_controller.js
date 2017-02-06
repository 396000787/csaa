// 引导页功能
angular.module('memberDetialEdit.controller', ['memberDetialEdit.service'])
  .controller('memberDetialEditCtrl', function ($scope, memberDetialEditFty, $window, $location, $ionicLoading) {

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
          'selArea': 0,
          'branchList': [],
          'selBranch': 0
      }

      //姓名
      //$scope.userName = "";
      ////会员编号
      //$scope.memberCode = "";
      ////会员类别
      //$scope.memberType = "";
      ////本会任职
      //$scope.post = "";
      ////电子邮箱
      //$scope.email = "";
      ////通讯地址
      //$scope.address = "";
      ////会员状态
      //$scope.state = "";
      ////性别
      //$scope.sex = 0;
      ////地区列表
      //$scope.areaList = [];
      $scope.errMessage = "";
      //$scope.selArea = 0;
      //获取所在地区列表
      memberDetialEditFty.getAreaList(function (data) {
          $scope.data.areaList = data;
          //if ($scope.selArea) {
          //    $scope.selArea = 0;
          //}
          //$scope.selArea = 0;
      });
      //专业分会列表
      //  $scope.branchList = [];
      // $scope.selBranch = 0;
      //获取专业分会列表
      memberDetialEditFty.getBranchList(function (data) {
          $scope.data.branchList = data;
          //if (!$scope.selArea) {
          //    $scope.selBranch = 0;
          //}
          //$scope.selBranch = 0;
      });
      var id = "";
      var user = $.cookie("user");
      if (user) {
          var _user = $.parseJSON(user);
          id = _user.id;
      } else { $location.path('tab/login'); }

      //获取会员信息
      memberDetialEditFty.getMember({ 'Param': id }, function (data) {
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
          $scope.data.post = data.post;
          $scope.data.email = data.email;
          $scope.data.address = data.address;
          $scope.data.state = data.state;
          if (!data.sex) {
              data.sex = 0;
          }
          $scope.data.sex = data.sex;
          $scope.data.selArea = data.areaID;
          $scope.data.selBranch = data.branchID;
      }

      //保存数据
      $scope.save = function () {
          $scope.errMessage = "";
          //if (!$scope.data.selArea || $scope.data.selArea == 0) {
          //    $scope.errMessage = "所属地区不能为空";
          //    return;
          //}
          //if (!$scope.data.selBranch || $scope.data.selBranch == 0) {
          //    $scope.errMessage = "所属专业分会不能为空";
          //    return;
          //}
          if (!$.trim($scope.data.email)) {
              $scope.errMessage = "用户电子邮箱不能为空";
              return;
          }

          var data = { 'branchID': $scope.data.selBranch, 'areaID': $scope.data.selArea, 'address': $scope.data.address, 'email': $scope.data.email, 'sex': $scope.data.sex, 'id': id };
          //alert(JSON.stringify(data));
          $ionicLoading.show({
              template: "正在保存数据，请稍后..."
          });
          memberDetialEditFty.saveMember(data, function (data) {
              $ionicLoading.hide();
              if (!data) {
                  alert('保存失败');
              } else {
                  $location.path('tab/memberDetial');
              }
          });
      }

  });

