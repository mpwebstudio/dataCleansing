'use strict';

dataApp.controller('SingleBicController',
    function SingleBicController($scope, singleBicData) {
        $scope.table = false;
        $scope.bicCleansing = function (validate, bicValidation) {

            var allData = [];
            
            if (bicValidation.$valid) {
                singleBicData.getData(validate, function (data) {
                    for (var i = 0; i <= data.length; i++) {
                        allData[i] = data[i];
                    }
                    allData = allData.splice(0, allData.length - 1);
                    $scope.answer = allData;
                    $scope.table = true;
                    
                })
            }
        }

        $scope.moreDetails = function (i) {
            alert('Bank: ' + $scope.answer[i].BankOrInstitution + '\n' + 'Branch: ' + $scope.answer[i].Branch + '\n' + 'City: ' + $scope.answer[i].City + '\n' + 'Country: ' + $scope.answer[i].Country + '\n' + 'Swift: ' + $scope.answer[i].SwiftCode + '\n');
        }
    })