// 引导页功能
angular.module('guidePage.controller', ['guidePage.service'])
  .controller('GuidePageCtrl', function ($scope, $location, $timeout, GuidePageFty, $state, $sce, $ionicLoading) {

      $ionicLoading.show({
          template: "正在载入数据，请稍后..."
      });

      $scope.$on('$ionicView.beforeEnter', function (e) {
          GetDataList();
      });

      $scope.returnIndex = function (item, $event) {
          $location.path('/tab/home');
          $event.stopPropagation();
      }

      $scope.SeeInfo = function (item, $event) {
          if (item.targetUrl == "") {
              return false;
          }
          $location.path('/AviationDetailsAd/' + item.targetUrl);
          $event.stopPropagation();
      }

      var GetDataList = function () {
          GuidePageFty.GetAdvertisementList(6, function (msg) {
              console.log(msg);
              if (msg.length == 1) {
                  $timeout(function () {
                      window.location.href = '#/tab/home';
                  }, 4000);
              }
              $scope.DataLstSource = msg;
              initialHead();
              $ionicLoading.hide();
          });
      }

      //引导页slide初始化
      var initialHead = function () {
          var guideSlide = new Swiper('#guideSlide', {
              autoplay: 6000,
              observer: true,
              autoplayStopOnLast: true,
              observeParents: true,
              onSlideChangeEnd: function (swiper) {
                  if (swiper.activeIndex + 1 == $scope.DataLstSource.length) {
                      $timeout(function () {
                          window.location.href = '#/tab/home';
                      }, 4000);
                  }
              }
          });
      }

      /**
       * 跳转到首页功能
       */
      //$scope.func_goHome=function(){
      //  $state.go("tab.home");
      //}

      //document.getElementById("close").addEventListener("click", function () {
      //    $state.go("tab.home");
      //}, false)

  })

