angular.module('AdvertisingEdit.service', ['global'])
  .factory('AdvertisingEditFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          GetNewsByTypeID: function (Param, callback) {
              $http({
                  url: path + '/api/NewApi/GetNewsBySearchKey',
                  method: 'get',
                  params: { StartRow: Param.StartRow, PageSize: Param.PageSize, TypeID: Param.TypeID, SearchKey: Param.SearchKey }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          InsterAdvertisement: function (Param, callback) {
              $http({
                  url: path + '/api/Advertisement/InsterAdvertisement',
                  method: 'post',
                  data: Param
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          ///获取详情
          GetAdvertisement: function (key, callback) {
              $http({
                  url: path + '/api/Advertisement/GetAdvertisement',
                  method: 'get',
                  params: { key: key }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          ///保存编辑
          UpdateAdvertisement: function (param, callback) {
              $http({
                  url: path + '/api/Advertisement/UpdateAdvertisement',
                  method: 'post',
                  data: param
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
      };
  });
