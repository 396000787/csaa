var directiveModule = angular.module("exampleApp.Directives", []);
// function : define a directive named highlight
// it accepts two param
// param one : the name of directive 
// param two : a factory method
directiveModule.directive("highlight", function ($filter) {

    // get the filter function
    var dayFilter = $filter("dayName");

    // param detail:
    // scope : view scope of action
    // element : the element which uses the custom directive
    // attrs : the attrs of the element
    return function (scope, element, attrs) {
        // console.log(dayFilter(scope.day));
        if (dayFilter(scope.day) == attrs['highlight']) {
            element.css("color", 'red');
        }
    }
})