angular.module('memberDetialEdit.service', ['global'])
  .factory('memberDetialEditFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          //获取所在地区列表
          getAreaList: function (callback) {
              $http({
                  url: path + '/api/AccountApi/GetAreaList',
                  method: 'get',
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  callback(true);
                  console.log(error);
              });
          },
          //获取专业分会列表
          getBranchList: function (callback) {
              $http({
                  url: path + '/api/AccountApi/GetBranchList',
                  method: 'get',
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  callback(true);
                  console.log(error);
              });
          },
          //获取用户信息
          getMember: function (data, callback) {
              $http({
                  url: path + '/api/AccountApi/GetMember',
                  method: 'get',
                  params: data
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  callback(true);
                  console.log(error);
              });
          },
          //修改用户信息
          saveMember: function (data, callback) {
              $http({
                  url: path + '/api/AccountApi/UpdateMember',
                  method: 'post',
                  data: data
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  callback(true);
                  console.log(error);
              });
          },
          //获取省列表
          getProvinceList: function (callback) {
              $http({
                  url: path + '/api/NativePlace/GetProvinceList',
                  method: 'get',
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  callback(true);
                  console.log(error);
              });
          },
          //获取市市列表
          getCityList: function (parentID, callback) {
              $http({
                  url: path + '/api/NativePlace/GetCityList',
                  method: 'get',
                  params: { 'ParentID': parentID }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  callback(true);
                  console.log(error);
              });
          },
          //获取专业列表
          getProfessionList: function (callback) {
              $http({
                  url: path + '/api/AccountApi/GetProfessionList',
                  method: 'get'
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  callback(true);
                  console.log(error);
              });
          },
          //获取职业列表
          getOccupationList: function (callback) {
              $http({
                  url: path + '/api/AccountApi/GetOccupationList',
                  method: 'get'
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  callback(true);
                  console.log(error);
              });
          },
      };
  });
