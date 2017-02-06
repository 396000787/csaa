// 引导页功能
angular.module('AviationDetailsAd.controller', ['AviationDetailsAd.service'])
  .controller('AviationDetailsAdCtrl', function ($scope, $sce, $stateParams, $filter, $timeout, GlobalVariable, $ionicPopup, AviationDetailsAdFty, $window, $location) {

      $scope.goBack = function () {
          window.location.href = '#/tab/home';
      }

      $scope.$on('$ionicView.afterEnter', function (e) {
          GetHead();
      });

      $scope.Collection = function (key) {
          var _user = $.cookie("user");

          if (!_user) {
              // 一个提示对话框            
              var alertPopup = $ionicPopup.alert({
                  template: '请登录',
                  buttons: []
              });
              $timeout(function () {
                  alertPopup.close();
              }, 2000);
              return;
          }
          if (key) {
              ///关注
              AviationDetailsAdFty.AddColletion($stateParams.id, function (msg) {
                  if (msg == true) {
                      $scope.isCollection = true;
                  }
              });
          }
          else {
              ///取消关注
              ///关注
              AviationDetailsAdFty.CancelColletion($stateParams.id, function (msg) {
                  if (msg == true) {
                      $scope.isCollection = false;
                  }
              });
          }
      }
      $scope.Fouce = function (key) {
          ///验证是否登录
          var _user = $.cookie("user");

          if (!_user) {
              // 一个提示对话框            
              var alertPopup = $ionicPopup.alert({
                  template: '请登录',
                  buttons: []
              });
              $timeout(function () {
                  alertPopup.close();
              }, 2000);
              return;
          }
          if (key) {
              ///关注
              AviationDetailsAdFty.AddConcerns($scope.writer, function (msg) {
                  if (msg == true) {
                      $scope.isFouce = true;
                  }
              });
          }
          else {
              ///取消关注
              AviationDetailsAdFty.CancelConcerns($scope.writer, function (msg) {
                  if (msg == true) {
                      $scope.isFouce = false;
                  }
              });
          }
      }

      var GetHead = function () {
          AviationDetailsAdFty.GetHead($stateParams.id, function (msg) {             
              $scope.isCollection = msg.isCollection;
              $scope.isFouce = msg.isFouce;
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

