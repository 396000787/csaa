// 引导页路由模块
angular.module('PapersList.route', ['PapersList.controller'])
  .config(function($stateProvider, $urlRouterProvider) {

    $stateProvider
     .state('PapersList', {
         url: '/PapersList',
         templateUrl: 'areas/PapersList/PapersList.html',
         controller: 'PapersListCtrl'
     })
  });
