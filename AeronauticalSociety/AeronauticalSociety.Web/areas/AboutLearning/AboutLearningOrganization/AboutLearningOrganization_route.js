// 引导页路由模块
angular.module('AboutLearningOrganization.route', ['AboutLearningOrganization.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {
      $stateProvider
        .state('tab.AboutLearningOrganization', {
            url: '/AboutLearningOrganization',
            views: {
                'tab-home': {
                    templateUrl: 'areas/AboutLearning/AboutLearningOrganization/AboutLearningOrganization.html',
                    controller: 'AboutLearningOrganizationCtrl'
                }
            }
        })
  });
