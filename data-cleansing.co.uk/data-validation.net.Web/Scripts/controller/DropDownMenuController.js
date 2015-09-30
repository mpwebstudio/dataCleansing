'use strict';

var DropDownMenuController = function ($scope) {
    $scope.categories = [
        {
            name: 'Profile',
            page: '/Application#'
        },
        {
            name: 'Services',
            page: '/Application#/services'
        },
        {
            name: 'Top up',
            page: '/Application#/topUp'
        },
        {
            name: 'Billing information',
            page:'/Application#/editBilling'
        }
    ];
}

DropDownMenuController.$inject = ['$scope'];