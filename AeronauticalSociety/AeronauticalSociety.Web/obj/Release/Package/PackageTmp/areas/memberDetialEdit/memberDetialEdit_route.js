// 引导页路由模块
angular.module('memberDetialEdit.route', ['memberDetialEdit.controller'])
  .config(function($stateProvider, $urlRouterProvider) {

      $stateProvider
           .state('tab.memberDetialEdit', {
               url: '/memberDetialEdit',
               views: {
                   'tab-account': {
                       templateUrl: 'areas/memberDetialEdit/memberDetialEdit.html',
                       controller: 'memberDetialEditCtrl'
                   }
               }
           });
  });
