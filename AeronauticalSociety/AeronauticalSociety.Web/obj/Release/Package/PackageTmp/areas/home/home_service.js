angular.module('home.service', ['global'])
  .factory('HomeFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          ///首页推荐信息
          SlideProjector: function (callback) {
              //GetHeadline
              $http({
                  url: path + '/api/NewApi/GetHeadline',
                  method: 'get',
                  params: { Count: 4 }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          /// 获取主
          GetMainMenuList: function (callback) {
              $http({
                  url: path + '/api/MenuApi/GetMainMenu',
                  method: 'get'
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          }
      };
  });
