angular.module('WorkPlanIndex.service', ['global'])
  .factory('WorkPlanIndexFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          /// 获取主
          GetWorkPlanList: function (par, callback) {
              $http({
                  url: path + '/api/WorkPlan/GetWorkPlanList',
                  method: 'get',
                  params: par
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          }
      };
  });
