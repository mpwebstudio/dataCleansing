'use strict';

var dataApp = angular
    .module('dataApp', ['ngResource', 'ngRoute', 'ngFileUpload', 'ngStorage', 'checklist-model'])
    .config(function ($routeProvider) {
        $routeProvider
            .when('/', {
                templateUrl: '/app/templates/profile.html'
            })
            .when('/editUser', {
                templateUrl: '/app/templates/editUser.html'
            })
            .when('/editBilling', {
                templateUrl: '/app/templates/editBilling.html'
            })
            .when('/invoice', {
                templateUrl: '/app/templates/invoice.html'
            })
            .when('/topUp', {
                templateUrl: '/app/templates/topUp.html'
            })
            .when('/buy/:id', {
                templateUrl: '/app/templates/buy.html'
            })
            .when('/success', {
                templateUrl: '/app/templates/success.html'
            })
            .when('/services', {
                templateUrl: '/app/templates/services.html'
            })
            .when('/address', {
                templateUrl: '/app/templates/single/address.html'
            })
            .when('/iban', {
                templateUrl: '/app/templates/single/iban.html'
            })
            .when('/phone', {
                templateUrl: '/app/templates/single/telephone.html'
            })
            .when('/email', {
                templateUrl: '/app/templates/single/email.html'
            })
            .when('/bic', {
                templateUrl: '/app/templates/single/bic.html'
            })
            .when('/card', {
                templateUrl: '/app/templates/single/card.html',
            })
            .when('/bulkAddress', {
                templateUrl: '/app/templates/bulk/bulkAddress.html'
            })
            .when('/bulkEmail', {
                templateUrl: '/app/templates/bulk/bulkEmail.html'
            })
            .when('/bulkPhone', {
                templateUrl: '/app/templates/bulk/bulkTelephone.html'
            })
            .when('/bulkIban', {
                templateUrl: '/app/templates/bulk/bulkIban.html'
            })
            .when('/bulkBic', {
                templateUrl: '/app/templates/bulk/bulkBic.html'
            })
            .when('/bulkCard', {
                templateUrl: '/app/templates/bulk/bulkCard.html'
            })
            .when('/addressCleansing', {
                templateUrl: '/app/templates/bulk/addressCleansing.html'
            })
            .when('/deduplicate', {
                templateUrl: '/app/templates/deduplicate.html'
            })
            .otherwise({ redirectTo: '/' });
    });
