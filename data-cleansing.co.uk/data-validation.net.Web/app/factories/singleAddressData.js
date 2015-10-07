'use strict';

dataApp.factory('singleAddressData', function ($http) {
    return {
        getData: function (id, successcb,error) {
            var url = '/AddressValidation/GetAddress/';
            $http.post(url + id )
            .success(function (data, status, headers, config) {
                successcb(data);
            })
            .error(function (data, status, headers, config) {
                               
                error(status);
              })
        }
    }
})