/// <reference path="WorkPlanIndex.html" />
// 引导页路由模块
angular.module('WorkPlanIndex.route', ['WorkPlanIndex.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {

      $stateProvider
           .state('tab.WorkPlanIndex', {
               url: '/WorkPlanIndex',
               views: {
                   'tab-home': {
                       templateUrl: 'areas/WorkPlan/WorkPlanIndex/WorkPlanIndex.html',
                       controller: 'WorkPlanIndexCtrl'
                   }
               }
           })
  });
