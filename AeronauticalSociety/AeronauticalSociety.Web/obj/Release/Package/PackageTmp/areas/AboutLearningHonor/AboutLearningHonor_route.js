// 引导页路由模块
angular.module('AboutLearningHonor.route', ['AboutLearningHonor.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {

      $stateProvider
        .state('tab.AboutLearningHonor', {
            url: '/AboutLearningHonor',
            views: {
                'tab-home': {
                    templateUrl: 'areas/AboutLearningHonor/AboutLearningHonor.html',
                    controller: 'AboutLearningHonorCtrl'
                }
            }
        })
  });
