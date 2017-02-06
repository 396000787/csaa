// 引导页路由模块
angular.module('AboutLearningOrganizationList.route', ['AboutLearningOrganizationList.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {
      $stateProvider
        .state('tab.AboutLearningOrganizationList', {
            url: '/AboutLearningOrganizationList/:title/:key',
            views: {
                'tab-home': {
                    templateUrl: 'areas/AboutLearning/AboutLearningOrganizationList/AboutLearningOrganizationList.html',
                    controller: 'AboutLearningOrganizationListCtrl'
                }
            }
        })
  });
