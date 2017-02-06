angular.module('AboutLearningHonor.service', ['global'])
  .factory('AboutLearningHonorFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          GetHonorList: function (par, callback) {
              $http({
                  url: path + '/api/AboutSociety/GetHonorList',
                  method: 'get',
                  params: { StartRow: par.StartRow, PageSize: par.PageSize }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          }
      };
  });
