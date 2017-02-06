// 引导页路由模块
angular.module('followList.route', ['followList.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {

      $stateProvider
         .state('tab.followList', {
             url: '/followList/:key',
             views: {
                 'tab-account': {
                     templateUrl: 'areas/Follow/followList/followList.html',
                     controller: 'followListCtrl'
                 }
             }
         });
  });
