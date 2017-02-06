// 引导页路由模块
angular.module('AboutLearningHom.route', ['AboutLearningHom.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {
      $stateProvider
           .state('tab.AboutLearningHom', {
               url: '/AboutLearningHom',
               views: {
                   'tab-home': {
                       templateUrl: 'areas/AboutLearningHom/AboutLearningHom.html',
                       controller: 'AboutLearningHomCtrl'
                   }
               }
           })
  });
