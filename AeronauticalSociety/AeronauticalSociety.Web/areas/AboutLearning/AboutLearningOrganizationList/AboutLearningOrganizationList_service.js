angular.module('AboutLearningOrganizationList.service', ['global'])
  .factory('AboutLearningOrganizationListFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          GetWorkingCommitteeList: function (par, callback) {
              $http({
                  url: path + '/api/AboutSociety/GetWorkingCommitteeList',
                  method: 'get',
                  params: { StartRow: par.StartRow, PageSize: par.PageSize }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          GetProfessionalBranchList: function (par, callback) {
              $http({
                  url: path + '/api/AboutSociety/GetWorkingCommitteeList',
                  method: 'get',
                  params: { StartRow: par.StartRow, PageSize: par.PageSize }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          GetLocalAssociationList: function (par, callback) {
              $http({
                  url: path + '/api/AboutSociety/GetLocalAssociationList',
                  method: 'get',
                  params: { StartRow: par.StartRow, PageSize: par.PageSize }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          GetUnitMemberList: function (par, callback) {
              $http({
                  url: path + '/api/AboutSociety/GetUnitMemberList',
                  method: 'get',
                  params: { StartRow: par.StartRow, PageSize: par.PageSize }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },

      };
  });
