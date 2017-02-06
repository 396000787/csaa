angular.module('Register.service', ['global'])
  .factory('RegisterFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          RegUser: function (data, callback) {
              $http({
                  url: path + '/api/AccountApi/RegUser',
                  method: 'Post',
                  data: data
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  callback("");
                  console.log(error);
              });
          },
          LoginNameIsRepeat: function (data, callback) {
              $http({
                  url: path + '/api/AccountApi/LoginNameIsRepeat',
                  method: 'get',
                  params: data
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  callback(true);
                  console.log(error);
              });
          }
      };
  });
