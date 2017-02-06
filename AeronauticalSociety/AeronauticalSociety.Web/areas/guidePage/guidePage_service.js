angular.module('guidePage.service', [])
  .factory('GuidePageFty', function ($http) {
      var path = window.location.origin;
      return {
          GetAdvertisementList: function (count, callback) {
              $http({
                  url: path + '/api/Advertisement/GetAdvertisementList',
                  method: 'get',
                  params: { count: count }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
      };
  });
