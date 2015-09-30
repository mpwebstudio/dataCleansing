dataApp.controller('TestController',
    function TestController($scope, itemsData, $location,$log) {
    $scope.new_item = {};
    $scope.addItem = function () {
        itemsData.save($scope.value).then(function (response) {
            $location.path('/card');    // back to route one
        });
    };
});