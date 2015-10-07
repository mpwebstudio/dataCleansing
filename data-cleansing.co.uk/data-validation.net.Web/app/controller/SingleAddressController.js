'use strict';
dataApp.controller('SingleAddressController',
    function SingleAddressController($scope, singleAddressData) {
        
        $scope.addressCleansing = function (validate, addressValidation) {
            
            var allData = [];

            if (addressValidation.$valid) {

                singleAddressData.getData(validate, function(data) {
                    for (var i = 0; i <= data.length; i++) {
                        allData[i] = data[i];
                    }

                    allData = allData.splice(0, allData.length - 1);
                    $scope.answer = allData;
                    $scope.show = true;
                    $scope.cleansingProgress = false;
                }, 
                function (data) {
                    if (data == 400) {
                        alert("You don't have enough credits! Please Top Up.");
                    }
                    $scope.cleansingProgress = false;
                })
            }
        }
    })