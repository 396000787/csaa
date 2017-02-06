angular.module('CollectionIndex.service', ['global'])
  .factory('CollectionIndexFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          GetWorkingCommitteeList: function (par, callback) {
              $http({
                  url: path + '/api/Collection/GetCollectionList',
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
