dataApp.controller('EditUserController',
    function EditUserController($scope,editUserData, userData, $http, $routeParams, $log, $window) {

        userData.getUser(1, function (data) {

            for (var i = data.length; i >= 0; i--) {
                $scope.user = data[i];
            }
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