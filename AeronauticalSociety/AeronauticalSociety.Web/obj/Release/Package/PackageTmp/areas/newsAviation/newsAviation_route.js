// 引导页路由模块
angular.module('newsAviation.route', ['newsAviation.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {
      $stateProvider
        .state('newsAviation', {
            url: '/newsAviation',
            templateUrl: 'areas/newsAviation/newsAviation.html',
            controller: 'newsAviation'
        });
  });
