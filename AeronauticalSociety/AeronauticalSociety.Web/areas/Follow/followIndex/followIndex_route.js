// 引导页路由模块
angular.module('followIndex.route', ['followIndex.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {

      $stateProvider
         .state('tab.followIndex', {
             url: '/followIndex',
             views: {
                 'tab-account': {
                     templateUrl: 'areas/Follow/followIndex/followIndex.html',
                     controller: 'followIndexCtrl'
                 }
             }
         });
  });
