dataApp.controller('BillingController',
    function BillingController($scope, billingData, $http, $routeParams) {

        billingData.getBillingInfon(1, function (data) {
            for (var i = data.length; i >= 0; i--) {
                $scope.Company = data[0].Company;
                $scope.Address = data[0].Address;
                $scope.Address2 = data[0].Address2;
                $scope.City = data[0].City;
                $scope.PostCode = data[0].PostCode;
                $scope.Country = data[0].Country;
                $scope.CompanyNumber = data[0].CompanyNumber;
                $scope.VATNumber = data[0].VATNumber;
            }
        })
    })