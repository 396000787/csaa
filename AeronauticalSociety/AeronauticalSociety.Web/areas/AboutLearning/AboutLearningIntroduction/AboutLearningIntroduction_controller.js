// 引导页功能
angular.module('AboutLearningIntroduction.controller', ['AboutLearningIntroduction.service'])
  .controller('AboutLearningIntroductionCtrl', function ($scope, $sce, AboutLearningIntroductionFty, $window, $location) {

      $scope.goBack = function () {
          window.history.go(-1);
      }

      $scope.$on('$ionicView.afterEnter', function (e) {
          initialPage();
      });

      var initialPage = function () {
          AboutLearningIntroductionFty.GetHtml(function (msg) {
              var QDom = $('<div>' + msg + '</div>');
              var imgList = QDom.find('a');
              imgList.remove();
              var eigth = QDom.find('*');
              for (var i = 0; i < 8; i++) {
                  eigth[i].remove();
              }
              $scope.Body = $sce.trustAsHtml(QDom.html());
          })
      }
  });

