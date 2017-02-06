// 引导页路由模块
angular.module('memberBind.route', ['memberBind.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {

      $stateProvider
       .state('tab.memberBind', {
           url: '/memberBind',
           views: {
               'tab-account': {
                   templateUrl: 'areas/memberBind/memberBind.html',
                   controller: 'memberBindCtrl'
               }
           }
       });
  });
