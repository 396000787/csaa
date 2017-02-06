// 引导页路由模块
angular.module('AviationDetailsAd.route', ['AviationDetailsAd.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {

      $stateProvider
       .state('AviationDetailsAd', {
           url: '/AviationDetailsAd/:id',
           templateUrl: 'areas/AviationDetailsAd/AviationDetailsAd.html',
           controller: 'AviationDetailsAdCtrl'
       })
  });
