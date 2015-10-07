dataApp.factory('creditsData', function ($http, $log) {

    return {
        getCredits: function (id, successcb) {

            $http({ method: 'GET', url: '/application/getcredits' })
                .success(function (data, status, headers, config) {
                    successcb(data);
                })
                .error(function (data, status, headers, config) {
                    
                });
        }
    }
})