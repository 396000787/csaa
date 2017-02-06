// 引导页路由模块
angular.module('ShowHtml.route', ['ShowHtml.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {

      $stateProvider
       .state('ShowHtml', {
           url: '/ShowHtml/:title/:key',
           templateUrl: 'areas/ShowHtml/ShowHtml.html',
           controller: 'ShowHtmlCtrl'
       })
  });
