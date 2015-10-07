dataApp.controller('BillingController',
    function BillingController($scope, billingData, $http, $routeParams) {

        billingData.getBillingInfon(1, function (data) {
                $scope.Company = data.Company;
                $scope.Address = data.Address;
                $scope.Address2 = data.Address2;
                $scope.City = data.City;
                $scope.PostCode = data.PostCode;
                $scope.Country = data.Country;
                $scope.CompanyNumber = data.CompanyNumber;
                $scope.VATNumber = data.VATNumber;
                $scope.Id = data.Id;
        })
    })