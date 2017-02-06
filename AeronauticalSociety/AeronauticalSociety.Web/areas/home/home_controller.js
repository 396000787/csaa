// 引导页功能
angular.module('home.controller', ['home.service'])
  .controller('HomeCtrl', function ($scope, HomeFty, $window, $location) {

      $scope.$on('$ionicView.afterEnter', function (e) {
          GetWorkPlay();
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

      $scope.OpenWorkPlay = function () {
          $location.path('/tab/WorkPlanIndex');
      }

      ///获取头滚动数据
      getHeaderSlideData();
      ///获取主菜单
      MainMenuList();

      var time = new Date();
      $scope.currentMonth = time.getMonth() + 1;

      function MainMenuList() {
          HomeFty.GetMainMenuList(function (msg) {
              $scope.MainMenuList = msg;
          });
      }

      $scope.isHaveWorkPlay = false;

      function GetWorkPlay() {
          var time = new Date();
          var par = {
              Year: time.getFullYear(),
              Month: time.getMonth() + 1,
              StartRow: 0,
              PageSize: 1,
              Title: "",
              StartTime: "",
              EndTime: ""
          }
          HomeFty.GetWorkPlanList(par, function (msg) {
              if (msg.Total == 0) {
                  $scope.isHaveWorkPlay = true;
              }
              else {
                  $scope.isHaveWorkPlay = false;
              }
              $scope.YearPlayData = msg.Data;
          })
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

