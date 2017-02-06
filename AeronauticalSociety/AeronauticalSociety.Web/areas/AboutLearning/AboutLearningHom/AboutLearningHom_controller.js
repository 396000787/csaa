// 引导页功能
angular.module('AboutLearningHom.controller', ['AboutLearningHom.service'])
  .controller('AboutLearningHomCtrl', function ($scope, AboutLearningHomFty, $sce, $window, $location) {

      $scope.$on('$ionicView.afterEnter', function (e) {
          GetAbout();
          GetContent();
          GetAboutMenu();
      });

      ///查看全部
      $scope.SeeProfile = function () {
          $location.path("tab/AboutLearningIntroduction");
      }

      ///获取简介
      var GetAbout = function () {
          AboutLearningHomFty.GetHtml(function (msg) {
              var QDom = $('<div>' + msg + '</div>');
              var imgList = QDom.find('a');
              imgList.remove();
              var eigth = QDom.find('*');
              for (var i = 0; i < 8; i++) {
                  eigth[i].remove();
              }
              $scope.Body = $.trim(QDom.text()).substring(0, 100) + '...';
          })
      }

      ///获取菜单
      var GetAboutMenu = function () {
          AboutLearningHomFty.GetAboutMenu(function (msg) {
              $scope.AboutMenu = msg;
          })
      }

      ///获取联系我们
      var GetContent = function () {
          AboutLearningHomFty.GetContent(function (msg) {
              $scope.content = msg;
          })
      }

      $scope.openLink = function (item) {
          $location.path(item.Url);
      }

  });

