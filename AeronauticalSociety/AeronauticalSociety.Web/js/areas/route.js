// 全局路由模块
angular.module('route', [
    'AviationDetailsAd.route',
    'tab.route', 'home.route', 'newsCenter.route',
    'newsAviation.route', 'Login.route',
    'Register.route', 'AviationDetails.route',
    'LearningDynamics.route', 'LearningDynamicsList.route',
    'memberDetial.route', 'memberBind.route',
    'noticeList.route', 'Error.route', 'AboutLearningIntroduction.route', 'memberDetialEdit.route',
    'guidePage.route', 'CollectionIndex.route', 'AboutLearningHom.route',
    'ShowHtml.route', 'IndexList.route', 'PapersList.route', 'PapersList1.route',
    'AboutLearningOrganizationList.route',
    'AboutLearningShowHtml.route', 'AboutLearningRegulations.route', 'AboutLearningHonor.route',
    'AboutLearningOrganization.route', 'followIndex.route', 'followList.route',
    'MemberIndex.route', 'MemberChangePassword.route', 'WorkPlanIndex.route'
])
  .config(function ($stateProvider, $urlRouterProvider) {
      $urlRouterProvider.otherwise('/guidePage');
  });
