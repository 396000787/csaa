// 引导页路由模块
angular.module('AboutLearningIntroduction.route', ['AboutLearningIntroduction.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {

      $stateProvider
            .state('tab.AboutLearningIntroduction', {
                url: '/AboutLearningIntroduction',
                views: {
                    'tab-home': {
                        templateUrl: 'areas/AboutLearning/AboutLearningIntroduction/AboutLearningIntroduction.html',
                        controller: 'AboutLearningIntroductionCtrl'
                    }
                }
            })
  });
