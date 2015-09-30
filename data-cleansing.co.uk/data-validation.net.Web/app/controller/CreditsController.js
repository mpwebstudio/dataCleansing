dataApp.controller('CreditsController',
    function CreditsController($scope, creditsData, $http, $routeParams, $log) {

        creditsData.getCredits(1,function(data){
            
            for (var i = data.length; i >= 0; i--) {
                
                $scope.Credits = data[0].Credits;
                $scope.DatePurchase = data[0].DatePurchase;
                $scope.DateExpire = data[0].DateExpire;
            }
        })

    })