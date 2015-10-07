'use strict';
dataApp.controller('SingleCardValidationController',
    function SingleCardValidationController($scope, singleCardData) {
        $scope.table = false;
        $scope.results = [];
        var CardNumber = '';
        var CardIssue = '';
        var IsValid = '';
        
        //when load the page if they are any previous results in local Storage load them to results array
        for (var item in localStorage) {
                if (localStorage.getItem(item) != null) {
                    $scope.table = true;
                    var val = localStorage.getItem(item);
                    if (item.substring(0, 4) == 'Card') {
                        var value = val.split(",");
                        if (value[1] != undefined) {
                            var res0 = value[0].split(":");
                            var res1 = value[1].split(":");
                            var res2 = value[2].split(":");
                            var res3 = value[3].split(":");
                            //Clear all because of Firefox
                            var newItem = {
                                Id: res0[1],
                                CardNumber: res1[1].replace('"', '').replace('"', ''),
                                CardIssue: res2[1].replace('"', '').replace('"', ''),
                                IsValid: res3[1].replace('"', '').replace('}', '').replace('"', '')
                            };
                            $scope.results.push(newItem);
                        }
                    }
                }
            }
        
        $scope.cardCleansing = function (validate, cardValidation) {
            var allData = [];
            if (cardValidation.$valid) {
                singleCardData.getData(validate, function (data) {
                    for (var i = 0; i <= data.length; i++) {
                        allData[i] = data[i];
                    }
                    allData = allData.splice(0, allData.length - 1);
                    $scope.table = true;
                    CardNumber = allData[0].CardNumber;
                    IsValid = allData[0].IsValid;
                    CardIssue = allData[0].CardIssue;

                    var newResult = {
                        Id: localStorage.length,
                        CardNumber: CardNumber,
                        CardIssue: CardIssue,
                        IsValid: IsValid,
                    };
                    //Save result to localStorage
                    localStorage.setItem('Card' + localStorage.length, JSON.stringify(newResult));
                    //Save to array
                    $scope.results.push(newResult);
                   })
            }
        }

        $scope.deleteRecord = function (index, item) {
            //Delete item from localStorage
            localStorage.removeItem('Card' + item.Id);
            //Remove item from results array
            $scope.results.splice(index, 1);
        }
    })




