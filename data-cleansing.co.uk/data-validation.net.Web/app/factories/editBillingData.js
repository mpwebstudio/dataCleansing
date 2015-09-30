'use strict';

dataApp.factory('editBillingData', function ($http, $log, $location, $window){
    return {
        saveBilling: function (id , successcb) {

            var info = id;
            var url = '/application/savebilling/';

            $http.post(url, { MyEvent : info})
            .success(function (data, status, headers, config){
                $window.location = 'application#/';
            })
        }
    }
})