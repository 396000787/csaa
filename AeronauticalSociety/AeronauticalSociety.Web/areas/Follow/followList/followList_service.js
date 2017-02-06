angular.module('followList.service', ['global'])
  .factory('followListFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          GetNewsByTypeID: function (par, callback) {
              $http({
                  url: path + '/api/NewApi/GetNewsByTypeID',
                  method: 'get',
                  params: par
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
      };
  });
