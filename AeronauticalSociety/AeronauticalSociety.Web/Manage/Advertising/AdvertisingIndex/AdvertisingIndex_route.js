/// <reference path="AdvertisingIndex.html" />
// 引导页路由模块
angular.module('AdvertisingIndex.route', ['AdvertisingIndex.controllers'])
  .config(function ($stateProvider, $urlRouterProvider) {
      $stateProvider
        .state("AdvertisingIndex", {
            url: '/AdvertisingIndex',
            templateUrl: 'Manage/Advertising/AdvertisingIndex/AdvertisingIndex.html',
            controller: 'AdvertisingIndexCtrl'
        })
  });
