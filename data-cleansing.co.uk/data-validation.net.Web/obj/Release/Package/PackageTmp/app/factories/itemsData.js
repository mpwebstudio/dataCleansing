dataApp.factory('itemsData', function ($http) {
    var items = [];
    return {
        list: function () {
        //    //if (items.length !== 0) {    // items array is empty so populate it and return list from server to controller
        //    //    // i just modified a little the request for jsfiddle
        //    //    $http.get('/cardvalidation/CardValidation/'+ id).then(function (response) {

        //    //        Array.prototype.push.apply(items, response.data);
        //    //    });
        //        return items;
        //    }
            return items;   // items exist already so just return the array
        },
        save: function (id) {
            return $http.get('/CardValidation/CardValidation/' + id).then(function (response) {
                Array.prototype.push.apply(items, response.data);
                return items;
            });
        }
    }
});