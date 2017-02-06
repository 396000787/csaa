// 引导页路由模块
angular.module('noticeList.route', ['noticeList.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {
      $stateProvider
        .state('noticeList', {
            url: '/noticeList',
            templateUrl: 'areas/noticeList/noticeList.html',
            controller: 'noticeListCrt'
        });
  });
