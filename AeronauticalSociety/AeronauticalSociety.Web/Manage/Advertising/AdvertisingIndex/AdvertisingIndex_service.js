angular.module('AdvertisingIndex.service', ['global'])
  .factory('AdvertisingIndexFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          ///获取
          GetAdvertisementes: function (Param, callback) {
              $http({
                  url: path + '/api/Advertisement/GetAdvertisementes',
                  method: 'get',
                  params: { StartRow: Param.StartRow, PageSize: Param.PageSize }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          ///启用
          StartAdvertisement: function (key, callback) {
              $http({
                  url: path + '/api/Advertisement/StartAdvertisement',
                  method: 'get',
                  params: { key: key }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          ///停用
          StopAdvertisement: function (key, callback) {
              $http({
                  url: path + '/api/Advertisement/StopAdvertisement',
                  method: 'get',
                  params: { key: key }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          ///删除
          DelAdvertisement: function (key, callback) {
              $http({
                  url: path + '/api/Advertisement/DelAdvertisement',
                  method: 'get',
                  params: { key: key }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          }
      };
  });
