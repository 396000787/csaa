// 引导页路由模块
angular.module('AboutLearningShowHtml.route', ['AboutLearningShowHtml.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {

      $stateProvider
        .state('tab.AboutLearningShowHtml', {
            url: '/AboutLearningShowHtml/:title/:key/:typeid',
            views: {
                'tab-home': {
                    templateUrl: 'areas/AboutLearning/AboutLearningShowHtml/AboutLearningShowHtml.html',
                    controller: 'AboutLearningShowHtmlCtrl'
                }
            }
        })
  });
