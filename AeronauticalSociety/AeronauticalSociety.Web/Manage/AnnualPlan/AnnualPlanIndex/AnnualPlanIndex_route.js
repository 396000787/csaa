/// <reference path="AnnualPlanIndex.html" />
/// <reference path="AnnualPlanIndex.html" />
// 引导页路由模块
angular.module('AnnualPlanIndex.route', ['AnnualPlanIndex.controllers'])
  .config(function ($stateProvider, $urlRouterProvider) {
      $stateProvider
        .state("AnnualPlanIndex", {
            url: '/AnnualPlanIndex',
            templateUrl: 'Manage/AnnualPlan/AnnualPlanIndex/AnnualPlanIndex.html',
            controller: 'AnnualPlanIndexCtrl'
        })
  });
