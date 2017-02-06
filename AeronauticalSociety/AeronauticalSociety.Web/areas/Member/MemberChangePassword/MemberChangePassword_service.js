angular.module('MemberChangePassword.service', ['global'])
  .factory('MemberChangePasswordFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          UpdatePassword: function (data, callback) {
              $http({
                  url: path + '/api/AccountApi/UpdatePassword',
                  method: 'Post',
                  data: data
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  callback("");
                  console.log(error);
              });
          }
         
      };
  });
