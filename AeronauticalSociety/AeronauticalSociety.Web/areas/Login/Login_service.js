angular.module('Login.service', ['global'])
  .factory('LoginFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          checkIn: function (data, callback) {
              $http({
                  url: path + '/api/AccountApi/CheckIn',
                  method: 'Post',
                  data: data
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
              //$.ajax({
              //    type: 'post',
              //    url: path + '/api/AccountApi/CheckIn',
              //    data: data,
              //}).success(function (result) {
              //    if (callback) {
              //        callback(result);
              //    }
              //}).fail(function (message) {
                 
              //});
          }
      };
  });
