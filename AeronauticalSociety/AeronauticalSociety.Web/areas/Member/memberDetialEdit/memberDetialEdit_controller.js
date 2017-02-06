// 引导页功能
angular.module('memberDetialEdit.controller', ['memberDetialEdit.service'])
  .controller('memberDetialEditCtrl', function ($scope, memberDetialEditFty, $window, $location, $ionicLoading) {

      $scope.goBack = function () {
          $location.path('tab/memberDetial');
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
          'addressProvinceID': 0,
          'addressProvince': '',
          'addressProvinceList': [],
          'addressCityID': 0,
          'addressCity': '',
          'address': '',
          'addressCityList': [],
          'state': '',
          'sex': 0,
          'age': 0,
          'professionID': 0,
          'professionName': '',
          'professionList': [],
          'occupationID': 0,
          'occupationName': '',
          'occupationList': [],
          'areaList': [],
          'areaName': '',
          'selArea': 0,
          'branchList': [],
          'selBranch': 0,
          'selCityID': 0,
          'selProvinceID': 0,
          'selprofessionID': 0,
          'seloccupationID': 0
      }
      $scope.showMember = false;

      $scope.errMessage = "";
      //获取省列表
      var getProvinceList = function () {
          memberDetialEditFty.getProvinceList(function (data) {
              $scope.data.addressProvinceList = data;
              if (!data) {
                  return;
              }
              if ($scope.data.addressProvinceID == 0) {
                  $scope.data.selProvinceID = data[0].id;
                  $scope.data.addressProvince = data[0].Name;
              } else {
                  $scope.data.selProvinceID = $scope.data.addressProvinceID;
              }
              getCityList($scope.data.selProvinceID);
          });
      }
      //获取城市列表
      var getCityList = function (parentID) {
          memberDetialEditFty.getCityList(parentID, function (data) {
              $scope.data.addressCityList = data;
              if (!data) {
                  return;
              }
              if ($scope.data.addressCityID == 0) {
                  $scope.data.selCityID = data[0].id;
                  $scope.data.addressCity = data[0].Name;
              } else {
                  $scope.data.selCityID = $scope.data.addressCityID;
              }
          });
      }

      //省选择项变化
      $scope.change = function () {
          $scope.data.addressCityID = 0;
          getCityList($scope.data.selProvinceID);
      }
      //获取专业分会列表
      memberDetialEditFty.getBranchList(function (data) {
          $scope.data.branchList = data;
      });

      //获取专业列表
      var getProfessionList = function () {
          memberDetialEditFty.getProfessionList(function (data) {
              $scope.data.professionList = data;
              if (!data) {
                  return;
              }
              if ($scope.data.professionID == 0) {
                  $scope.data.selprofessionID = data[0].id;
                  $scope.data.professionName = data[0].typeName;
              } else {
                  $scope.data.selprofessionID = $scope.data.professionID;
              }
          });
      }

      //获取职业列表
      var getOccupationList = function () {
          memberDetialEditFty.getOccupationList(function (data) {
              $scope.data.occupationList = data;
              if (!data) {
                  return;
              }
              if ($scope.data.occupationID == 0) {
                  $scope.data.seloccupationID = data[0].id;
                  $scope.data.occupationName = data[0].oname;
              } else {
                  $scope.data.seloccupationID = $scope.data.occupationID;
              }
          });
      }


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
          if (data.memberID) {
              $scope.showMember = true;
          } else {
              $scope.showMember = false;
          }
          if (data.levelName) {
              $scope.data.memberType = '中国航空学会' + data.levelName;
          }
          $scope.data.post = data.post;
          $scope.data.email = data.email;
          $scope.data.addressProvinceID = data.addressProvinceID;
          $scope.data.addressCityID = data.addressCityID;
          $scope.data.address = data.address;
          $scope.data.state = data.state;
          if (!data.sex) {
              data.sex = 0;
          }
          $scope.data.sex = data.sex;
          $scope.data.selArea = data.areaID;
          $scope.data.selBranch = data.branchID;
          //获取省列表
          getProvinceList();

          $scope.data.loginName = data.loginName;
          $scope.data.age = data.age;
          $scope.data.mobileNO = data.mobileNO;
          $scope.data.professionID = data.professionID;
          $scope.data.professionName = data.professionName;
          $scope.data.occupationID = data.occupationID;
          $scope.data.occupationName = data.occupationName;
          getProfessionList();
          getOccupationList();
      }


      //验证年龄
      var checkAge = function () {
          var age = $.trim($scope.data.age);
          if (!age) {
              return true;
          }
          //验证长度
          if (age.length > 2) {
              $scope.errMessage = "用户年龄不能大于两位，请重新填写";
              return false;
          }
          var reg = new RegExp("^[0-9]*$");
          //验证格式
          if (!reg.test(age)) {
              $scope.errMessage = "用户年龄只能输入数字,请重新填写";
              return false;
          }
          return true;
      }

      //验证联系方式
      var checkmobileNO = function () {
          var mobileNO = $.trim($scope.data.mobileNO);
          if (!mobileNO) {
              return true;
          }
          if (!(/^1[34578]\d{9}$/.test(mobileNO))) {
              $scope.errMessage = "手机号码有误，请重新填写";
              return false;
          }


          return true;
      }
      //验证电子邮箱
      var checkEmail = function () {
          var email = $.trim($scope.data.email);
          if (!email) {
              $scope.errMessage = "用户电子邮箱不能为空";
              return false;
          }
          re = /^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$/
          if (!re.test(email)) {
              $scope.errMessage = "电子邮箱格式错误，请重新填写";
              return false;
          }
          return true

      }
      //验证地址
      var checkAddress = function ()
      {
          var address = $.trim($scope.data.address);
          if (!address) {
              return true;
          }
          if (address.length > 128)
          {
              $scope.errMessage = "地址最大输入长度不能超过128个字符，请重新填写";
              return false;
          }
          return true;
      }
      //保存数据
      $scope.save = function () {
          $scope.errMessage = "";
          //验证年龄
          if (!checkAge()) {
              return;
          }
          if (!checkmobileNO()) {
              return;
          }
          if (!checkEmail()) {
              return;
          }
          if (!checkAddress())
          {
              return;
          }

          var data = { 'branchID': $scope.data.selBranch, 'areaID': $scope.data.selArea, 'address': $scope.data.address, 'email': $scope.data.email, 'sex': $scope.data.sex, 'id': id, 'addressProvinceID': $scope.data.selProvinceID, 'addressProvince': $('#Province').find("option:selected").text(), 'addressCityID': $scope.data.selCityID, 'addressCity': $('#City').find("option:selected").text(), 'age': $scope.data.age, 'mobileNO': $scope.data.mobileNO, 'professionID': $scope.data.selprofessionID, 'occupationID': $scope.data.seloccupationID };
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

 