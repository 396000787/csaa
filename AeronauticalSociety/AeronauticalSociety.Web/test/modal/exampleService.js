var serviceModule = angular.module("exampleApp.Service", [])
// function : define a service named days
serviceModule.service("days", function (nowValue) {
    this.today = nowValue.getDay();
    this.tomorrow = this.today + 1;
})