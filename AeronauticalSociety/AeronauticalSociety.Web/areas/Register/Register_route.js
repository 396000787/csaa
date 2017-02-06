// 引导页路由模块
angular.module('Register.route', ['Register.controller'])
  .config(function($stateProvider, $urlRouterProvider) {

      $stateProvider
       .state('Register', {
           url: '/Register',
           templateUrl: 'areas/Register/Register.html',
           controller: 'RegisterCtrl'
       });
  });
