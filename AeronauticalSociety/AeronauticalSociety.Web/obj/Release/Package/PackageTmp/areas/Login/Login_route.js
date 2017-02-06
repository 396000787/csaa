// 引导页路由模块
angular.module('Login.route', ['Login.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {

      $stateProvider
           .state('tab.Login', {
               url: '/Login',
               views: {
                   'tab-account': {
                       templateUrl: 'areas/Login/Login.html',
                       controller: 'LoginCtrl'
                   }
               }
           })
  });
