angular.module('Login.service', ['global'])
  .factory('LoginFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          checkIn: function (data, callback) {
              $http({
                  url: path + '/api/AccountApi/AdminCheckIn',
                  method: 'Post',
                  data: data
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          }
      };
  });

