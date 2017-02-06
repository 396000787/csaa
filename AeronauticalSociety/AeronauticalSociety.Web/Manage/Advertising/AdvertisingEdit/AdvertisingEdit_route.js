/// <reference path="AdvertisingEdit.html" />
// 引导页路由模块
angular.module('AdvertisingEdit.route', ['AdvertisingEdit.controllers'])
  .config(function ($stateProvider, $urlRouterProvider) {
      $stateProvider
        .state("AdvertisingEdit", {
            url: '/AdvertisingEdit/:id',
            templateUrl: 'Manage/Advertising/AdvertisingEdit/AdvertisingEdit.html',
            controller: 'AdvertisingEditCtrl'
        })
  });
