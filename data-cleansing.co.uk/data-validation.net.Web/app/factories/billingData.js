dataApp.factory('billingData', function ($http, $log, $resource) {

    var resourse = $resource('payment/paypalprepayment/:id', { id: '@id' });

    return {
        getBillingInfon: function (id, successcb) {

            $http({ method: 'GET', url: '/application/billinginfo' })
            .success(function (data, status, headers, config) {
                successcb(data);
            })
            .error(function (data, status, headers, config) {
                $log.error(data);
            });
        },
        payPalPrePayment: function (id) {
            resourse.get({ id: id });
        }
    }
})