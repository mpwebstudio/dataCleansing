dataApp.controller('EditBillingController',
    function EditBillingController($scope, editBillingData, $routeParams, $window, billingData) {

        billingData.getBillingInfon(1, function (data) {
            for (var i = data.length; i >= 0; i--) {
                $scope.billing = data[i];
            }
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