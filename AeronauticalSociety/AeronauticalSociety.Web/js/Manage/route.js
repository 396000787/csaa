// 全局路由模块
angular.module('route', [
  'AdvertisingIndex.route', 'AdvertisingEdit.route', 'AnnualPlanIndex.route',
  'Login.route', 'AnnualPlanEdit.route'
])
  .config(function ($stateProvider, $urlRouterProvider) {
      $urlRouterProvider.otherwise('/Login');
  });
