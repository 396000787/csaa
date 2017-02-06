// 引导页路由模块
angular.module('IndexList.route', ['IndexList.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {
      $stateProvider
      .state('tab.IndexList', {
          url: '/IndexList',
          views: {
              'tab-home': {
                  templateUrl: 'areas/AcademicResources/IndexList/IndexList.html',
                  controller: 'IndexListCtrl'
              }
          }
      })
  });
