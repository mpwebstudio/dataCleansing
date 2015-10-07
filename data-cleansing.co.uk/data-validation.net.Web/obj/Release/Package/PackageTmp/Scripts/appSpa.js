'use strict';

var dataCleansingApp = angular
    .module('dataCleansingApp', ['ngResource', 'ngRoute', 'ngFileUpload', 'ngStorage', "ngSanitize", "ngCsv"])
    .config(function ($routeProvider) {
        $routeProvider
            .when('/addressCleansing', {
                templateUrl: '/Scripts/templates/addressCleansing.html'
            })
            .when('/emailCleansing', {
                templateUrl: '/Scripts/templates/emailCleansing.html'
            })
           .when('/telephoneCleansing', {
               templateUrl: '/Scripts/templates/telephoneCleansing.html'
            })
            .when('/deduplicate', {
                templateUrl: '/Scripts/templates/deduplicate.html'
            })
    });

