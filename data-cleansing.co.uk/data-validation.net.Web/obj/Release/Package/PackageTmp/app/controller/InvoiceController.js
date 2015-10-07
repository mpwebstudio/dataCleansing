dataApp.controller('InvoiceController',
    function InvoiceController($scope, invoiceData) {

        invoiceData.getInvoiceInfo(1, function (data) {
            var allData = [];

            for (var i = data.length; i >= 0; i--) {
                allData[i] = data[i];
            }
            $scope.invoices = allData;
            
        })
        
    })