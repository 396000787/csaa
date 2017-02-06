// 引导页功能
angular.module('AviationDetails.controller', ['AviationDetails.service'])
  .controller('AviationDetailsCtrl', function ($scope, $sce, $stateParams, $filter, GlobalVariable, AviationDetailsFty, $window, $location) {

      $scope.goBack = function () {
          window.history.go(-1);
      }

      $scope.$on('$ionicView.afterEnter', function (e) {
          GetHead();
      });

      var GetHead = function () {
          AviationDetailsFty.GetHead($stateParams.id, function (msg) {
              $scope.headHtml = $sce.trustAsHtml(msg.BaseInfro.title);
              //处理相对应的img图片
              var QDom = $('<div>' + msg.Body.body + '</div>');
              var imgList = QDom.find('img');
              if (imgList.length > 0) {
                  for (var i = 0; i < imgList.length; i++) {
                      var node = imgList[i];
                      var path = '';
                      var src = $(node).attr('src');
                      $(node).css('width', '100%');
                      $(node).css('height', 'auto');
                      if (src.indexOf('http') == -1 && src.indexOf('https') == -1) {
                          path = GlobalVariable.SERVER_Address + src;
                      } else {
                          path = src;
                      }
                      $(node).attr('src', path);
                  }
              }
              var tTime = new Date(parseInt(msg.BaseInfro.pubdate) * 1000);
              var time = $filter('date')(tTime, 'yyyy-MM-dd h:mm:ss');
              $scope.time = time;
              $scope.writer = msg.BaseInfro.writer;
              $scope.Body = $sce.trustAsHtml(QDom.html());
          })
      }
  });

