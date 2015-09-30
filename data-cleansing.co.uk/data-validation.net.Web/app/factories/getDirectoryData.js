dataApp.factory('getDirectoryData', function ($http) {
    
    return {
        getData: function (id, successcb) {
            $http({ method: 'GET', url: 'application/getUserDirectory' })
            .success(function (data, status, headers, config) {
                successcb(data);
            })
        }
    }

})