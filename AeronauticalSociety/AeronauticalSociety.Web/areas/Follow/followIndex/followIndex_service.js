angular.module('followIndex.service', ['global'])
  .factory('followIndexFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          GetWorkingCommitteeList: function (par, callback) {
              $http({
                  url: path + '/api/Concerns/GetConcernsList',
                  method: 'get',
                  params: { StartRow: par.StartRow, PageSize: par.PageSize }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
      };
  });
