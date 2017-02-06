/// <reference path="MemberIndex.html" />
// 引导页路由模块
angular.module('MemberIndex.route', ['MemberIndex.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {

      $stateProvider
             .state('tab.MemberIndex', {
                 url: '/MemberIndex',
                 views: {
                     'tab-account': {
                         templateUrl: 'areas/Member/MemberIndex/MemberIndex.html',
                         controller: 'MemberIndexCtrl'
                     }
                 }
             });
  });
