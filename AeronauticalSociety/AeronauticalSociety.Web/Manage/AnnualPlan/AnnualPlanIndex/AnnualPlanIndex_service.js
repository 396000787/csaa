angular.module('AnnualPlanIndex.service', ['global'])
  .factory('AnnualPlanIndexFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          ///获取年度计划
          GetWorkPlanList: function (Param, callback) {
              $http({
                  url: path + '/api/WorkPlan/GetWorkPlanList',
                  method: 'get',
                  params: Param
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },

          ///删除
          DelWorkPlan: function (wid, callback) {
              $http({
                  url: path + '/api/WorkPlan/DelWorkPlan',
                  method: 'get',
                  params: { wid: wid }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          }
      };
  });
