/// <reference path="AnnualPlanEdit.html" />
// 引导页路由模块
angular.module('AnnualPlanEdit.route', ['AnnualPlanEdit.controllers'])
  .config(function ($stateProvider, $urlRouterProvider) {
      $stateProvider
        .state("AnnualPlanEdit", {
            url: '/AnnualPlanEdit/:id',
            templateUrl: 'Manage/AnnualPlan/AnnualPlanEdit/AnnualPlanEdit.html',
            controller: 'AnnualPlanEditCtrl'
        })
  });
