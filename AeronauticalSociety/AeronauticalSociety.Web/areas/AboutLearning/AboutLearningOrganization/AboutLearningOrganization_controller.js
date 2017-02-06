// 引导页功能
angular.module('AboutLearningOrganization.controller', ['AboutLearningOrganization.service'])
  .controller('AboutLearningOrganizationCtrl', function ($scope, AboutLearningOrganizationFty, $window, $location) {
      $scope.goBack = function () {
          window.history.go(-1);
      }

      $scope.opLink = function (item) {
          if (item.TypeID == '119' || item.TypeID == '120') {
              $location.path(item.Url + item.MenuName + '/232323/' + item.TypeID);
          } else {
              $location.path(item.Url + item.MenuName + '/' + item.TypeID);
          }
      }

      var GetOrganizationalTypes = function () {
          AboutLearningOrganizationFty.GetOrganizationalTypes(function (msg) {
              $scope.listDataSource = msg;
          });
      }

      GetOrganizationalTypes();

  });

