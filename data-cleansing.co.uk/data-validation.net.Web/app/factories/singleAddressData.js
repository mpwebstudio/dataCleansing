'use strict';

dataApp.factory('singleAddressData', function ($http) {
    return {
        getData: function (id, successcb) {
            var url = '/Address/SingleAddress/';
            $http.post(url, { id : id })
            .success(function (data, status, headers, config) {
                successcb(data);
            })
            .error(function (data, status, headers, config) {
                
            })
        }
    }
})