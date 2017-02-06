// 引导页路由模块
angular.module('newsCenter.route', ['newsCenter.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {
      $stateProvider
        .state('tab.newsCenter', {
            url: '/newsCenter',
            views: {
                'tab-home': {
                    templateUrl: 'areas/newsCenter/newsCenter.html',
                    controller: 'newsCenter'
                }
            }
        });
  });
