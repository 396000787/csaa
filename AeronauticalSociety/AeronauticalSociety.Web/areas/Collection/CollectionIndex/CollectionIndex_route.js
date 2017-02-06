/// <reference path="CollectionIndex.html" />
// 引导页路由模块
angular.module('CollectionIndex.route', ['CollectionIndex.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {

      $stateProvider
           .state('tab.CollectionIndex', {
               url: '/CollectionIndex',
               views: {
                   'tab-Collection': {
                       templateUrl: 'areas/Collection/CollectionIndex/CollectionIndex.html',
                       controller: 'CollectionIndexCtrl'
                   }
               }
           })
  });
