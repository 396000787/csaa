angular.module('AboutLearningRegulations.service', ['global'])
  .factory('AboutLearningRegulationsFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          GetManageRuleses: function (par, callback) {
              $http({
                  url: path + '/api/AboutSociety/GetManageRuleses',
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
