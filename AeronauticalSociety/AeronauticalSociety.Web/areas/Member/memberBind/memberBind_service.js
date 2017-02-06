angular.module('memberBind.service', ['global'])
  .factory('memberBindFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          //绑定会员
          bindMember: function (data, callback) {
              $http({
                  url: path + '/api/AccountApi/BindMember',
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
