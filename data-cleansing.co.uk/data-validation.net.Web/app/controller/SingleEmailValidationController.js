'use strict';
dataApp.controller('SingleEmailValidationController',
    function SingleEmailValidationController($scope, singleEmailData,$log) {
        $scope.table = false;
        $scope.results = [];
        var IsValid = '';
        var Email = '';
        var MxRecord = '';
        var Message = '';

        for (var item in localStorage) {
            if(localStorage.getItem(item) != null){
                $scope.table = true;
                var val = localStorage.getItem(item);
                if (item.substring(0, 5) == 'Email') {
                    var value = val.split(",");
                    if (value[1] != undefined) {
                        var res0 = value[0].split(":");
                        var res1 = value[1].split(":");
                        var res2 = value[2].split(":");
                        var res3 = value[3].split(":");
                        var res4 = value[4].split(":");
                        
                        var newItem = {
                            Id: res0[1],
                            Email: res1[1].replace('"', '').replace('"', ''),
                            IsValid: res2[1].replace('"', '').replace('"', ''),
                            Message:res3[1].replace('"','').replace('"',''),
                            MxRecord: res4[1].replace('"', '').replace('}', '').replace('"', '')
                        };
                        $scope.results.push(newItem);
                    }
                }
            }
        }


        $scope.emailCleansing = function (validate, emailValidation) {

            var allData = [];

            if (emailValidation.$valid) {
                singleEmailData.getData(validate, function (data) {
                    for (var i = 0; i <= data.length; i++) {
                        allData[i] = data[i];
                    }
                    allData = allData.splice(0, allData.length - 1);
                    $scope.table = true;
                    Email = allData[0].Email;
                    IsValid = allData[0].IsValid.replace(',','');
                    Message = allData[0].Message;
                    MxRecord = allData[0].MxRecord;

                    var newResult = {
                        Id: localStorage.length,
                        Email: Email,
                        IsValid: IsValid,
                        MxRecord: MxRecord,
                        Message:Message,
                    };

                    localStorage.setItem('Email' + localStorage.length, JSON.stringify(newResult));
                    $scope.results.push(newResult);
                })
            }
        }

        $scope.deleteRecord = function (index, item) {
            localStorage.removeItem('Email' + item.Id);
            $scope.results.splice(index, 1);
        }
    })