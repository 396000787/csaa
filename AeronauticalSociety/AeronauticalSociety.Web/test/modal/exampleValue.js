var valueModule = angular.module("exampleApp.Values", [])
// defind value
var now = new Date();
valueModule.value("nowValue", now);