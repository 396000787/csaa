// 引导页路由模块
angular.module('AviationDetails.route', ['AviationDetails.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {

      $stateProvider
       .state('AviationDetails', {
           url: '/AviationDetails/:id',
           templateUrl: 'areas/AviationDetails/AviationDetails.html',
           controller: 'AviationDetailsCtrl'
       })
  });
