'use strict';

dataApp.factory('singleBicData', function ($http,$log) {
    return {
        getData: function (id, successcb) {
            var url = '/BicValidation/BicValidation/';
            $http.get(url + id)
            .success(function (data, status, headers, config) {
                successcb(data);
            })
            .error(function (data, status, headers, config) {
                if (data == 404)
                    alert("You don't have enought credits! Please TopUp.")
            })
        }
    }
})