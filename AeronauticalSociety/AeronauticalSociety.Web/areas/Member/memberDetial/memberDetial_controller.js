// 引导页功能
angular.module('memberDetial.controller', ['memberDetial.service'])
  .controller('memberDetialCtrl', function ($scope, memberDetialFty, $window, $location, $ionicLoading) {

      $scope.goBack = function () {
          $location.path('tab/MemberIndex');
      }

      $scope.$on('$ionicView.afterEnter', function (e) {

      });
      $scope.data = {
          'loginName': '',
          'userName': '',
          'memberCode': '',
          'memberType': '',
          'post': '',
          'email': '',
          'mobileNO': '',
          'addressProvinceID': '',
          'addressProvince': '',
          'addressCityID': '',
          'addressCity': '',
          'address': '',
          'state': '',
          'sex': '',
          'age': '',
          'professionID': '',
          'professionName': '',
          'occupationID': '',
          'occupationName': '',
          'areaList': [],
          'areaName': '',
          'selArea': 0,
          'branchList': [],
          'selBranch': 0
      }
      $scope.showMember = false;
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
          if (data.memberID) {
              $scope.showMember = true;
          } else {
              $scope.showMember = false;
          }
          $scope.data.memberCode = data.memberID;
          if (data.levelName) {
              $scope.data.memberType = '中国航空学会' + data.levelName;
          }
          $scope.data.areaName = data.areaName;
          $scope.data.branchName = data.branchName;
          $scope.data.post = data.post;
          $scope.data.email = data.email;
          $scope.data.addressProvinceID = data.addressProvinceID;
          $scope.data.addressProvince = data.addressProvince;
          $scope.data.addressCityID = data.addressCityID;
          $scope.data.addressCity = data.addressCity;
          $scope.data.address = data.address;
          $scope.data.state = data.state;
          if (!data.sex) {
              data.sex = 0;
          }
          $scope.data.sex = data.sex == 1 ? '女' : '男';
          $scope.data.selArea = data.areaID;
          $scope.data.selBranch = data.branchID;
          $scope.data.loginName = data.loginName;
          $scope.data.age = data.age;
          $scope.data.mobileNO = data.mobileNO;
          $scope.data.professionID = data.professionID;
          $scope.data.professionName = data.professionName;
          $scope.data.occupationID = data.occupationID;
          $scope.data.occupationName = data.occupationName;
      }

      //点击编辑
      $scope.save = function () {
          $location.path('tab/memberDetialEdit')
      }

  });

