dataApp.controller('EditBillingController',
    function EditBillingController($scope, editBillingData, $routeParams, $window, billingData) {

        billingData.getBillingInfon(1, function (data) {
            $scope.billing = data;
        })

        $scope.saveBilling = function(billing, newBillingInfo){
            if(newBillingInfo.$valid){
                editBillingData.saveBilling(billing);
            }
        }

        $scope.cancelEdit = function () {
            $window.location = 'application#/';
        }
    })