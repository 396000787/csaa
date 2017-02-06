// 引导页路由模块
angular.module('PapersList1.route', ['PapersList1.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {
      $stateProvider
      .state('tab.PapersList1', {
          url: '/PapersList1',
          views: {
              'tab-home': {
                  templateUrl: 'areas/AcademicResources/PapersList1/PapersList1.html',
                  controller: 'PapersList1Ctrl'
              }
          }
      })
  });
