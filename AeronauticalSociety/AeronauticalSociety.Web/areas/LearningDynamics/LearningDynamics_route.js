// 引导页路由模块
angular.module('LearningDynamics.route', ['LearningDynamics.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {
      $stateProvider
        .state('tab.LearningDynamics', {
            url: '/LearningDynamics',
            views: {
                'tab-newsCenter': {
                    templateUrl: 'areas/LearningDynamics/LearningDynamics.html',
                    controller: 'LearningDynamicsCtr'
                }
            }
        })
  });
