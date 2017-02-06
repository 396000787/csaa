angular.module('MemberIndex.service', ['global'])
  .factory('MemberIndexFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          ///首页推荐信息
          CheckOut: function (callback) {
              //GetHeadline
              $http({
                  url: path + '/api/AccountApi/CheckOut',
                  method: 'get'
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          }
      };
  });
