// 引导页路由模块
angular.module('Collection.route', ['Collection.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {

      $stateProvider
           .state('tab.Collection', {
               url: '/Collection',
               views: {
                   'tab-Collection': {
                       templateUrl: 'areas/Collection/Collection.html',
                       controller: 'CollectionCtrl'
                   }
               }
           })
  });
