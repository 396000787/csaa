angular.module('AnnualPlanEdit.service', ['global'])
  .factory('AnnualPlanEditFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          ///新增工作计划
          AddWorkPlan: function (WorkPlan, callback) {
              $http({
                  url: path + '/api/WorkPlan/AddWorkPlan',
                  method: 'post',
                  data: WorkPlan
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          ///修改工作计划
          UpdateWorkPlan: function (WorkPlan, callback) {
              $http({
                  url: path + '/api/WorkPlan/UpdateWorkPlan',
                  method: 'post',
                  data: WorkPlan
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          ///获取详情
          GetWorkPlanDetial: function (wid, callback) {
              $http({
                  url: path + '/api/WorkPlan/GetWorkPlanDetial',
                  method: 'get',
                  params: { 'wid': wid }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          ///获取城市
          GetProvinceList: function (callback) {
              $http({
                  url: path + '/api/NativePlace/GetProvinceList',
                  method: 'get'
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          //区县列表
          GetCityList: function (ParentID, callback) {
              $http({
                  url: path + '/api/NativePlace/GetCityList',
                  method: 'get',
                  params: { ParentID: ParentID }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          }
      };
  });
