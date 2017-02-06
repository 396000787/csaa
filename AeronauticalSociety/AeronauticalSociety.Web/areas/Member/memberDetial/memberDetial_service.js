angular.module('memberDetial.service', ['global'])
  .factory('memberDetialFty', function ($http, $q) {
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
          }

      };
  });
