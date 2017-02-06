// 引导页路由模块
angular.module('AboutLearningConstitution.route', ['AboutLearningConstitution.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {

      $stateProvider
        .state('tab.AboutLearningConstitution', {
            url: '/AboutLearningConstitution',
            views: {
                'tab-home': {
                    templateUrl: 'areas/AboutLearningConstitution/AboutLearningConstitution.html',
                    controller: 'AboutLearningConstitutionCtrl'
                }
            }
        })
  });
