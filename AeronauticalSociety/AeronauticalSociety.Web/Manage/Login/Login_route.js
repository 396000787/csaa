// 引导页路由模块
angular.module('Login.route', ['Login.controller'])
  .config(function($stateProvider, $urlRouterProvider) {

    $stateProvider
     .state('Login', {
         url: '/Login',
         templateUrl: 'Manage/Login/Login.html',
         controller: 'LoginCtrl'
     })
  });
