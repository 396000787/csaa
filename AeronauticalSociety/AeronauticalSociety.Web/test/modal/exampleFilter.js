var filterModule = angular.module("exampleApp.Filters", []);
// function : define a fitler named dayName
filterModule.filter('dayName', function () {

    var dayNames = ['Sunday', "Monday", 'Tuesday', 'Wednesday', 'Thurday', 'Friday', 'Saturday'];
    return function (input) {
        // input is the value of data binding
        return angular.isNumber(input % 7) ? dayNames[input % 7] : input % 7;
    };
})