dataApp.controller('BuyController',
    function BuyController($scope, $routeParams, $location,billingData ) {
        $scope.id = $routeParams.id;

        billingData.getBillingInfon(1, function (data) {
            for (var i = 0; i < data.length; i++) {
                $scope.Address = data[0].Address;
                $scope.Address2 = data[0].Address2;
                $scope.City = data[0].City;
                $scope.PostCode = data[0].PostCode;
                $scope.Country = data[0].Country;
            }
        })

        billingData.payPalPrePayment($routeParams.id);
    })