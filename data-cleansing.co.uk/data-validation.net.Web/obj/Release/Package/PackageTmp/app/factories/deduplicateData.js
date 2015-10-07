'use strict';

dataApp.factory('deduplicateData', function ($http) {
    return {
        getColumn: function (id, successcb) {
            var url = '/deduplicate/ColumnName/';
            $http.post(url, { id: id })
            .success(function (data, status, headers, config) {
                successcb(data);
            })
            .error(function (data, status, headers, config) {

            })
        }
    }
})