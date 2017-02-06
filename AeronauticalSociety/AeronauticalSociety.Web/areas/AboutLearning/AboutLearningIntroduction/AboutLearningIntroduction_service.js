angular.module('AboutLearningIntroduction.service', ['global'])
  .factory('AboutLearningIntroductionFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          ///首页推荐信息
          GetHtml: function (callback) {
              //GetHeadline
              $http({
                  url: path + '/api/AboutSociety/GetSocietyIntroduction',
                  method: 'get'
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          }
      };
  });
