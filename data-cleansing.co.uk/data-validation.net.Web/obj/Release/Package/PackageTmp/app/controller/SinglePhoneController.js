'use strict';
dataApp.controller('SinglePhoneController',
    function SinglePhoneController($scope, singlePhoneData,$log) {
        $scope.table = false;
        $scope.results = [];
        var Number = '';
        var Area = '';
        var IsValid = '';

        for (var item in localStorage) {
            if (localStorage.getItem(item) != null) {
                $scope.table = true;
                var val = localStorage.getItem(item);
                console.log(item.substring(0, 5));
                if (item.substring(0, 5) == 'Phone') {
                    var value = val.split(',');
                    if (value[1] != undefined) {
                        var res0 = value[0].split(':');
                        var res1 = value[1].split(':');
                        var res2 = value[2].split(':');
                        var res3 = value[3].split(':');

                        var newItem = {
                            Id: res0[1],
                            Number: res1[1].replace('"', '').replace('"',''),
                            Area: res2[1].replace('"', '').replace('"',''),
                            IsValid: res3[1].replace('"', '').replace('}', '').replace('"','')
                        };
                        $scope.results.push(newItem);
                    }
                }
            }
        }

        $scope.phoneCleansing = function (validate, phoneValidation) {

            var allData = [];

            if (phoneValidation.$valid) {
                singlePhoneData.getData(validate, function (data) {
                    for (var i = 0; i <= data.length; i++) {
                        allData[i] = data[i];
                    }
                    allData = allData.splice(0, allData.length - 1);
                    $scope.table = true;
                    Number = allData[0].Number;
                    IsValid = allData[0].IsValid;
                    Area = allData[0].Area;

                    var newResult = {
                        Id: localStorage.length,
                        Number: Number,
                        Area: Area,
                        IsValid: IsValid
                    };
                    localStorage.setItem('Phone' + localStorage.length, JSON.stringify(newResult));
                    $scope.results.push(newResult);
                })
            }
        }

        $scope.deleteRecord = function (index, item) {
            localStorage.removeItem('Phone' + item.Id);
            $scope.results.splice(index, 1);
        }
    })