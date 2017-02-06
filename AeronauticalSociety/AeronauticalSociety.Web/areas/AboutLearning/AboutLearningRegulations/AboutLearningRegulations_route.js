// 引导页路由模块
angular.module('AboutLearningRegulations.route', ['AboutLearningRegulations.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {

      $stateProvider
       .state('tab.AboutLearningRegulations', {
           url: '/AboutLearningRegulations',
           views: {
               'tab-home': {
                   templateUrl: 'areas/AboutLearning/AboutLearningRegulations/AboutLearningRegulations.html',
                   controller: 'AboutLearningRegulationsCtrl'
               }
           }
       })
  });
