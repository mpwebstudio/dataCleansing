dataApp.controller('EditUserController',
    function EditUserController($scope,editUserData, userData, $http, $routeParams, $log, $window) {

        userData.getUser(1, function (data) {
                $scope.user = data;
        })

        $scope.saveUser = function (user,newUserInfo) {
            if (newUserInfo.$valid) {
                editUserData.saveInfo(user);
            }
        }

        $scope.cancelEdit = function () {
            $window.location = 'application#/';
        }
    })