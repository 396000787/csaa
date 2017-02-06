// 引导页路由模块
angular.module('memberDetial.route', ['memberDetial.controller'])
  .config(function($stateProvider, $urlRouterProvider) {

      $stateProvider
           .state('tab.memberDetial', {
               url: '/memberDetial',
               views: {
                   'tab-account': {
                       templateUrl: 'areas/memberDetial/memberDetial.html',
                       controller: 'memberDetialCtrl'
                   }
               }
           });
  });
