angular.module('newsAviation.service', ['global'])
  .factory('newsAviationFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          ///新闻列表
          GetNewsListData: function (par, callback) {
              $http({
                  url: path + '/api/NewApi/GetTextNews',
                  method: 'get',
                  params: { StartRow: par.StartRow, PageSize: par.PageSize }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              });
          }
      };
  });
