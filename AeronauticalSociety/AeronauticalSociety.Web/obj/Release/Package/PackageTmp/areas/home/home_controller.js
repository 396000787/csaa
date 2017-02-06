// 引导页功能
angular.module('home.controller', ['home.service'])
  .controller('HomeCtrl', function ($scope, HomeFty, $window, $location) {

      $scope.$on('$ionicView.afterEnter', function (e) {
      });

      // 页面跳转
      $scope.clickMenu = function (a) {
          $location.path(a.Url);
      }

      $scope.OpenImgLink = function (item) {
          //alert(JSON.stringify(item));
          headerSwiper.startAutoplay();
          $location.path('AviationDetails/' + item.id);
      }

      ///获取头滚动数据
      getHeaderSlideData();
      ///获取主菜单
      MainMenuList();

      function MainMenuList() {
          HomeFty.GetMainMenuList(function (msg) {
              $scope.MainMenuList = msg;
          });
      }

      // 头部滚动条数据
      function getHeaderSlideData() {
          HomeFty.SlideProjector(function (msg) {
              $scope.headerSlideData = msg;
              initHeaderSlide();
          });
      }

      // 初始化头部滚动条
      var headerSwiper = null;
      function initHeaderSlide() {
          headerSwiper = new Swiper('#headerSlider', {
              paginationClickable: true,
              autoplayDisableOnInteraction: false,
              autoplay: 3000,
              loop: false,
              // 如果需要分页器
              pagination: '.swiper-pagination',
              // 改变自动更新
              observer: true,
              observeParents: true
          });
      }

  });

