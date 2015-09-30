'use strict';

dataApp.factory('singlePhoneData', function ($http) {
    return {
        getData: function (id, successcb) {
            var url = '/phonevalidation/TelephoneValidation/';
            $http.get(url,{params: { id: id }})
            .success(function (data, status, headers, config) {
                successcb(data);
            })
            .error(function (data, status, headers, config) {

            })
        }
    }
})