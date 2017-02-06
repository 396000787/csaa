// 引导页路由模块
angular.module('Error.route', ['Error.controller'])
  .config(function($stateProvider, $urlRouterProvider) {

    $stateProvider
     .state('Error', {
         url: '/Error',
         templateUrl: 'areas/Error/Error.html',
         controller: 'ErrorCtrl'
     })
  });
