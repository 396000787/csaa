angular.module('AboutLearningOrganization.service', ['global'])
  .factory('AboutLearningOrganizationFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          GetOrganizationalTypes: function (callback) {
              $http({
                  url: path + '/api/MenuApi/GetOrganizationalTypes',
                  method: 'get'
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          }
      };
  });
