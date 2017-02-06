angular.module('AviationDetailsAd.service', ['global'])
  .factory('AviationDetailsAdFty', function ($http, $q) {
      var path = window.location.origin;
      return {
          GetHead: function (id, callback) {
              $http({
                  url: path + '/api/NewApi/GetTextNewsDetial',
                  method: 'get',
                  params: { aid: id }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          ///添加收藏
          AddColletion: function (id, callback) {
              $http({
                  url: path + '/api/Collection/AddColletion',
                  method: 'Post',
                  data: { aid: id }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          ///添加关注
          AddConcerns: function (write, callback) {
              $http({
                  url: path + '/api/Concerns/AddConcerns',
                  method: 'Post',
                  data: { authorName: write }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          CancelColletion: function (id, callback) {
              $http({
                  url: path + '/api/Collection/CancelColletion',
                  method: 'get',
                  params: { Param: id }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          },
          CancelConcerns: function (id, callback) {
              $http({
                  url: path + '/api/Concerns/CancelConcerns',
                  method: 'get',
                  params: { Param: id }
              }).success(function (msg) {
                  callback(msg);
              }).error(function (error) {
                  console.log(error);
              })
          }
      };
  });
