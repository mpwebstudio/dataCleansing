'use strict';

dataApp.factory('singleEmailData', function ($http) {
    return {
        getData: function (id, successcb) {
            var url = '/Email/EmailValidation/';
            $http.post(url + id )
            .success(function (data, status, headers, config) {
                successcb(data);
            })
            .error(function (data, status, headers, config) {
                alert("You don't have enought credits! Please TopUp.")
            })
        }
    }
})