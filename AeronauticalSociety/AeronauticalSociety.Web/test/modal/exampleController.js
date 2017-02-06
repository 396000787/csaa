var controllerModule = angular.module("exampleApp.Controllers", []);
// function : define a controller named dayCtrl
// the controller include two param:
// param detail:
// param one : name of controller
// param two : a factory function 
// the param $scope of factory function show information to view
controllerModule.controller("dayCtrl", function ($scope, days) {       // days : use custom service
    // today is ...
    $scope.day = days.today;
    // tomorrow is ...
    $scope.tomorrow = days.tomorrow;
})