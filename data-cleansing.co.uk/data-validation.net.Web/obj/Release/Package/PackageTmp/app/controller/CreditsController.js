dataApp.controller('CreditsController',
    function CreditsController($scope, creditsData, $http, $routeParams, $log) {

        creditsData.getCredits(1,function(data){
                $scope.Credits = data.Credits;
                $scope.DateExpire = data.DateExpire;
        })

    })