angular.module('routerApp', ['ui.router', 'AdvertisingIndex.controllers'])
.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.when('', 'main');
    $stateProvider
    .state("main", {
        url: '/main',
        templateUrl: '/Manage/Advertising/AdvertisingIndex/AdvertisingIndex.html',
        controller: 'AdvertisingIndexCtrl'
    })

})