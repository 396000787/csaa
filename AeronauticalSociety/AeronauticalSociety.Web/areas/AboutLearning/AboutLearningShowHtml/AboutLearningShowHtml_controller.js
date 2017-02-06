// 引导页功能
angular.module('AboutLearningShowHtml.controller', ['AboutLearningShowHtml.service'])
  .controller('AboutLearningShowHtmlCtrl', function ($scope, $state, $sce, AboutLearningShowHtmlFty, $window, $location) {
      $scope.goBack = function () {
          window.history.go(-1);
      }

      $scope.title = $state.params.title;
      $scope.typeid = $state.params.typeid;
      $scope.key = $state.params.key;

      $scope.$on('$ionicView.afterEnter', function (e) {
          switch ($scope.typeid) {
              ///获取学会章程
              case "48":
                  GetConstitutionDetial();
                  break;
              case "16":
                  GetHonorDetial($scope.key);
                  break;
              case "337":
                  GetManageRulesDetial($scope.key);
                  break;
              case "119":
                  ///理事会
                  GetCouncil($scope.key);
                  break;
              case "120":
                  ///获取常务理事会信息
                  GetAffairsCouncil($scope.key);
                  break;
              case "122":
                  GetWorkingCommittee($scope.key);
                  break;
              case "118":
                  GetProfessionalBranch($scope.key);
                  break;
              case "117":
                  GetLocalAssociation($scope.key);
                  break;
              case "124":
                  GetUnitMember($scope.key);
                  break;
          }
      });

      var fatHtml = function (msg) {
          $scope.Body = $sce.trustAsHtml(msg);
      }

      ///获取学会章程
      var GetConstitutionDetial = function () {
          AboutLearningShowHtmlFty.GetConstitutionDetial(function (msg) {
              fatHtml(msg);
          });
      }

      ///荣誉详情
      var GetHonorDetial = function (aid) {
          AboutLearningShowHtmlFty.GetHonorDetial({ aid: aid }, function (msg) {
              var QDom = $('<div>' + msg.Body.body + '</div>');
              QDom.find('img').css('width', '100%');
              $scope.title = msg.BaseInfro.title;
              $scope.Body = $sce.trustAsHtml(QDom.html());
          });
      }

      ///管理条例
      var GetManageRulesDetial = function (aid) {
          AboutLearningShowHtmlFty.GetManageRulesDetial({ aid: aid }, function (msg) {
              var QDom = $('<div>' + msg.Body.body + '</div>');
              QDom.find('img').css('width', '100%');
              $scope.title = msg.BaseInfro.title;
              $scope.Body = $sce.trustAsHtml(QDom.html());
          });
      }

      ///获取理事会信息
      var GetCouncil = function (aid) {
          AboutLearningShowHtmlFty.GetCouncil(function (msg) {
              $scope.Body = $sce.trustAsHtml(msg);
          });
      }

      ///获取理事会信息
      var GetAffairsCouncil = function (aid) {
          AboutLearningShowHtmlFty.GetAffairsCouncil(function (msg) {
              $scope.Body = $sce.trustAsHtml(msg);
          });
      }

      ///工作委员会
      var GetWorkingCommittee = function (aid) {
          AboutLearningShowHtmlFty.GetWorkingCommittee({ aid: aid }, function (msg) {
              $scope.Body = $sce.trustAsHtml(msg.Body.body);
          });
      }
      ///专业分会
      var GetProfessionalBranch = function (aid) {
          AboutLearningShowHtmlFty.GetProfessionalBranch({ aid: aid }, function (msg) {
              $scope.Body = $sce.trustAsHtml(msg.Body.body);
          });
      }
      ///地方学会
      var GetLocalAssociation = function (aid) {
          AboutLearningShowHtmlFty.GetLocalAssociation({ aid: aid }, function (msg) {
              $scope.Body = $sce.trustAsHtml(msg.Body.body);
          });
      }
      ///单位会员
      var GetUnitMember = function (aid) {
          AboutLearningShowHtmlFty.GetUnitMember({ aid: aid }, function (msg) {
              $scope.Body = $sce.trustAsHtml(msg.Body.body);
          });
      }

  });

