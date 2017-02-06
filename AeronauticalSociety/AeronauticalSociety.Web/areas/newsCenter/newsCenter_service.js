angular.module('newsCenter.service', ['global'])
  .factory('newsCenterFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          ListDataSource: function (callback) {
              ///新闻列表
              $http({
                  url: path + '/api/MenuApi/GetNewMenu',
                  method: 'get',
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              });
          }
      };
  });
