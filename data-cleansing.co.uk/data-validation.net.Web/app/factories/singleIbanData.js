'use strict';

dataApp.factory('singleIbanData', function ($http) {
    return {
        getData: function (id, successcb) {
            var url = '/iban/SingleIban/';
            $http.get(url + id)
            .success(function (data, status, headers, config) {
                successcb(data);
            })
            .error(function (data, status, headers, config) {
            })
        }
    }
})