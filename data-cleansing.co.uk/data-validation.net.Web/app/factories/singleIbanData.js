'use strict';

dataApp.factory('singleIbanData', function ($http) {
    return {
        getData: function (id, successcb) {
            var url = '/IbanValidation/IbanValidation/';
            $http.get(url + id)
            .success(function (data, status, headers, config) {
                successcb(data);
            })
            .error(function (data, status, headers, config) {
                if(data==404)
                    alert("You don't have enough credits! Please Top Up.")
            })
        }
    }
})