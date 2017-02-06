// 引导页路由模块
angular.module('MemberChangePassword.route', ['MemberChangePassword.controller'])
  .config(function ($stateProvider, $urlRouterProvider) {

      $stateProvider
        .state('tab.MemberChangePassword', {
            url: '/MemberChangePassword',
            views: {
                'tab-account': {
                    templateUrl: 'areas/Member/MemberChangePassword/MemberChangePassword.html',
                    controller: 'MemberChangePasswordCtrl'
                }
            }
        });
  });
