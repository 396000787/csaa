angular.module('LearningDynamicsList.service', ['global'])
  .factory('LearningDynamicsListFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          ///新闻列表
          GetNewsListData: function (par, callback) {
              $http({
                  url: path + '/api/NewApi/GetAssociation',
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
