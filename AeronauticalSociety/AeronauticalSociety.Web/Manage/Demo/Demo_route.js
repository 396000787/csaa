// 引导页路由模块
angular.module('Demo.route', ['Demo.controller'])
  .config(function($stateProvider, $urlRouterProvider) {

    $stateProvider
     .state('Demo', {
         url: '/Demo',
         templateUrl: 'areas/Demo/Demo.html',
         controller: 'DemoCtrl'
     })
  });
