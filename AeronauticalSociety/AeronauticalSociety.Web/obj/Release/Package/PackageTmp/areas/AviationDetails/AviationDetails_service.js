angular.module('AviationDetails.service', ['global'])
  .factory('AviationDetailsFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          GetHead: function (id, callback) {
              $http({
                  url: path + '/api/NewApi/GetTextNewsDetial',
                  method: 'get',
                  params: { aid: id }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          }
      };
  });
