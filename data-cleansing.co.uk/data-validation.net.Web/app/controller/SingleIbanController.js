'use strict';
dataApp.controller('SingleIbanController',
    function SingleIbanController($scope, singleIbanData) {
        $scope.table = false;
        $scope.results = [];
        var BankName = '';
        var BranchName = '';
        var BranchAddress = '';
        var City = '';
        var Postcode = '';
        var Country = '';
        var Telephone = '';
        var Fax = '';
        var IsoCode = '';
        var Swift = '';
        var BankCode = '';
        var BranchCode = '';

        for (var item in localStorage) {

            if (localStorage.getItem(item) != null) {
                $scope.table = true;
                var val = localStorage.getItem(item);

            }
        }

        $scope.ibanCleansing = function (validate, ibanValidation) {

            var allData = [];

            if (ibanValidation.$valid) {
                singleIbanData.getData(validate, function (data) {
                    for (var i = 0; i <= data.length; i++) {
                        allData[i] = data[i];
                    }
                    allData = allData.splice(0, allData.length - 1);
                    console.log(allData);
                    $scope.answer = allData;
                    $scope.table = true;
                })
            }
        }

        $scope.moreDetails = function (i) {
            alert('Bank: ' + $scope.answer[i].BankName + '\n' + 'Branch: ' + $scope.answer[i].BranchName + '\n' + 'Address: ' + $scope.answer[i].BranchAddress + '\n' + 'City: ' + $scope.answer[i].City + '\n' + 'Post Code: ' + $scope.answer[i].Postcode + '\n' + 'Country: ' + $scope.answer[i].Country + '\n' + 'Telephone: ' + $scope.answer[i].Telephone + '\n' + 'Fax: ' + $scope.answer[i].Fax + '\n' + 'Country Code:' + $scope.answer[i].IsoCode + '\n' + 'Swift:' + $scope.answer[i].Swift + '\n' + 'Bank Code: ' + $scope.answer[i].BankCode + '\n' + 'Branch Identi Code: ' + $scope.answer[i].BranchCode + '\n')
        }
    })