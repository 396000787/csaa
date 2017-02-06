// tab路由模块
angular.module('tab.route', ['ssstab.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {
      $stateProvider
        .state('tab', {
            url: '/tab',
            abstract: true,
            templateUrl: 'areas/tab/tabs.html',
            controller: 'ssstabCtrl'
        });
  });

