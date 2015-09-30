'use strict';

dataApp.factory('singleCardData', function ($http) {
    return {
        getData: function (id, successcb) {
            var url = '/cardvalidation/CardValidation/';
            $http.get(url, { params: { id: id } })
            .success(function (data, status, headers, config) {
                successcb(data);
            })
            .error(function (data, status, headers, config) {
                console.log(data);
            })
        }
    }
})