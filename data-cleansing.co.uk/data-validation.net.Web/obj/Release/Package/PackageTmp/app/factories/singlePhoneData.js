'use strict';

dataApp.factory('singlePhoneData', function ($http,$log) {
    return {
        getData: function (id, successcb) {
            var url = '/TelephoneValidation/TelephoneValidation/';
            $http.get(url,{params: { id: id }})
            .success(function (data, status, headers, config) {
                successcb(data);
                $log.error(data);
            })
            .error(function (data, status, headers, config) {

            })
        }
    }
})