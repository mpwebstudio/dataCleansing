'use strict';

dataApp.factory('singleBicData', function ($http) {
    return {
        getData: function (id, successcb) {
            var url = '/Bic/SingleBic/';
            $http.get(url + id)
            .success(function (data, status, headers, config) {
                successcb(data);
            })
            .error(function (data, status, headers, config) {
                
            })
        }
    }
})