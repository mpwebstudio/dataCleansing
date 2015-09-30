dataApp.factory('invoiceData', function ($http, $log, $location) {
    return {
        getInvoiceInfo: function(id, successcb){

            $http({ method: 'GET', url: '/application/getinvoice'})
            .success(function (data, status, headers, config) {
                successcb(data);
                
            })
            .error(function (data, status, headers, config) {
                $log.error(data);
                
            });

        }
    }
})