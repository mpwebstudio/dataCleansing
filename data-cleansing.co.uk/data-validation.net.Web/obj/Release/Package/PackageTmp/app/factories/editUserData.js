dataApp.factory('editUserData', function ($http, $log, $location, $window) {
    return {
        saveInfo: function (id, successcb) {

            var info = id;
            var url = '/application/saveinfo/';

            $http.post(url,{MyEvent: info})
            .success(function (data, status, headers, config) {
                $window.location = 'application#/';
            })
        }
    }
})