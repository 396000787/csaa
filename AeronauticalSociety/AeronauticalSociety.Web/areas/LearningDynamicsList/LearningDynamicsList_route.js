// 引导页路由模块
angular.module('LearningDynamicsList.route', ['LearningDynamicsList.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {
      $stateProvider
        .state('LearningDynamicsList', {
            url: '/LearningDynamicsList',
            templateUrl: 'areas/LearningDynamicsList/LearningDynamicsList.html',
            controller: 'LearningDynamicsListCrt'
        });
  });
