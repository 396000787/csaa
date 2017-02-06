// 全局路由模块
angular.module('route', [
    'tab.route', 'home.route', 'newsCenter.route',
    'newsAviation.route', 'Login.route',
    'Register.route', 'AviationDetails.route',
    'LearningDynamics.route', 'LearningDynamicsList.route',
    'memberDetial.route', 'memberBind.route',
    'noticeList.route', 'Error.route', 'AboutLearningIntroduction.route', 'memberDetialEdit.route',
    'guidePage.route', 'Collection.route', 'AboutLearningHom.route',
    'ShowHtml.route',
    'AboutLearningConstitution.route', 'AboutLearningRegulations.route', 'AboutLearningHonor.route',
    'AboutLearningOrganization.route',
])
  .config(function ($stateProvider, $urlRouterProvider) {
      console.log('路由寻找失败！');
      $urlRouterProvider.otherwise('/tab/home');
  });
