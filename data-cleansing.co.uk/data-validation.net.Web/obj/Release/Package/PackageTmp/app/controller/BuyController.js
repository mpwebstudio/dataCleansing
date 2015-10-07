dataApp.controller('BuyController',
    function BuyController($scope, $routeParams, $location,billingData ) {
        $scope.id = $routeParams.id;

        billingData.getBillingInfon(1, function (data) {
                $scope.Address = data.Address;
                $scope.Address2 = data.Address2;
                $scope.City = data.City;
                $scope.PostCode = data.PostCode;
                $scope.Country = data.Country;
        })

        billingData.payPalPrePayment($routeParams.id);
    })