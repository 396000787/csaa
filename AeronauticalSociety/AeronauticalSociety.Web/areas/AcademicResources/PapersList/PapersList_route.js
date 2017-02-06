// 引导页路由模块
angular.module('PapersList.route', ['PapersList.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {
      $stateProvider
      .state('tab.PapersList', {
          url: '/PapersList',
          views: {
              'tab-home': {
                  templateUrl: 'areas/AcademicResources/PapersList/PapersList.html',
                  controller: 'PapersListCtrl'
              }
          }
      })
  });
