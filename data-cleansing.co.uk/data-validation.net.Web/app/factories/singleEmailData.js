'use strict';

dataApp.factory('singleEmailData', function ($http) {
    return {
        getData: function (id, successcb) {
            var url = '/email/EmailValidation/';
            $http.post(url, { id: id })
            .success(function (data, status, headers, config) {
                successcb(data);
            })
            .error(function (data, status, headers, config) {
            })
        }
    }
})