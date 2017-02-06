angular.module('AboutLearningShowHtml.service', ['global'])
  .factory('AboutLearningShowHtmlFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          ///获取学会章程
          GetConstitutionDetial: function (callback) {
              //GetHeadline
              $http({
                  url: path + '/api/AboutSociety/GetConstitutionDetial',
                  method: 'get'
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          ///荣誉详情
          GetHonorDetial: function (par, callback) {
              //GetHeadline
              $http({
                  url: path + '/api/AboutSociety/GetHonorDetial',
                  method: 'get',
                  params: { aid: par.aid }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          ///管理条例
          GetManageRulesDetial: function (par, callback) {
              //GetHeadline
              $http({
                  url: path + '/api/AboutSociety/GetManageRulesDetial',
                  method: 'get',
                  params: { aid: par.aid }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          ///获取理事会详情
          GetCouncil: function (callback) {
              //GetHeadline
              $http({
                  url: path + '/api/AboutSociety/GetCouncil',
                  method: 'get'
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          ///获取常务理事会详情
          GetAffairsCouncil: function (callback) {
              //GetHeadline
              $http({
                  url: path + '/api/AboutSociety/GetAffairsCouncil',
                  method: 'get'
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          ///获取公告委员会详情
          GetWorkingCommittee: function (par, callback) {
              //GetHeadline
              $http({
                  url: path + '/api/AboutSociety/GetWorkingCommittee',
                  method: 'get',
                  params: { aid: par.aid }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          ///获取专业分会详情
          GetProfessionalBranch: function (par, callback) {
              //GetHeadline
              $http({
                  url: path + '/api/AboutSociety/GetProfessionalBranch',
                  method: 'get',
                  params: { aid: par.aid }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          ///获取地方学会详情
          GetLocalAssociation: function (par, callback) {
              //GetHeadline
              $http({
                  url: path + '/api/AboutSociety/GetLocalAssociation',
                  method: 'get',
                  params: { aid: par.aid }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          ///获取单位会员详情
          GetUnitMember: function (par, callback) {
              //GetHeadline
              $http({
                  url: path + '/api/AboutSociety/GetUnitMember',
                  method: 'get',
                  params: { aid: par.aid }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
      };
  });
