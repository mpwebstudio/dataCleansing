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
                if(data == 404)
                    alert("You don't have enought credits! Please TopUp.")
                else if(data == 403)
                    alert("You have enter a non digit symbol!")
            })
        }
    }
})