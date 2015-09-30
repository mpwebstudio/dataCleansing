dataApp.controller('FileUploadController',
    function FileUploadController($scope, $http) {
        var dropbox = document.getElementById("dropbox");
        $scope.firstPart = true;
        $scope.secoundPart = false;
        $scope.dropText = 'Drop a CSV files here...'

        // init event handlers
        function dragEnterLeave(evt) {
            evt.stopPropagation()
            evt.preventDefault()
            $scope.$apply(function () {
                $scope.dropText = 'Drop a CSV files here...'
                $scope.dropClass = ''
            })
        }
        dropbox.addEventListener("dragenter", dragEnterLeave, false)
        dropbox.addEventListener("dragleave", dragEnterLeave, false)
        dropbox.addEventListener("dragover", function (evt) {
            evt.stopPropagation()
            evt.preventDefault()
            var clazz = 'not-available'
            var ok = evt.dataTransfer && evt.dataTransfer.types && evt.dataTransfer.types.indexOf('Files') >= 0
            evt.dataTransfer.setData("text/csv", "Ole")

            $scope.$apply(function () {
                $scope.dropText = ok ? 'Drop a CSV files here...' : 'Only files are allowed!'
                $scope.dropClass = ok ? 'over' : 'not-available'
            })
        }, false)
        dropbox.addEventListener("drop", function (evt) {

            evt.stopPropagation()
            evt.preventDefault()
            $scope.$apply(function () {
                $scope.dropText = 'Drop files here...'
                $scope.dropClass = ''
            })
            var files = evt.dataTransfer.files
            if (files.length > 0) {
                $scope.$apply(function () {
                    $scope.files = []
                    for (var i = 0; i < files.length; i++) {
                        $scope.files.push(files[i])
                    }
                })
            }
        }, false)
        //============== DRAG & DROP =============

        $scope.setFiles = function (element) {
            $scope.$apply(function ($scope) {

                // Turn the FileList object into an Array
                $scope.files = []
                for (var i = 0; i < element.files.length; i++) {
                    $scope.files.push(element.files[i])
                }
                $scope.progressVisible = false
            });
        };

        $scope.removeElement = function (index) {

            $scope.files.splice(index, 1);

        }

        $scope.uploadFile = function () {

            var fileNumber = 0;
            var fd = new FormData()
            for (var i in $scope.files) {
                fd.append("fileToUpload", $scope.files[i])
                fileNumber++;
            }
            $scope.cleansingProgress = true;
            var uploadUrl = '/Address/BulkAddress/';

            $http.post(uploadUrl, fd, {
                withCredentials: false,
                headers: {
                    'Content-Type': undefined
                },
                transformRequest: angular.identity
            })
            .success(function (data, status, headers, config) {
                if (fileNumber >= 2) {
                    var file = new Blob([data], { type: 'application/octet-stream' });
                    var fileURL = URL.createObjectURL(file);
                    var a = document.createElement('a');
                    a.href = fileURL;
                    a.target = '_blank';
                    a.download = 'data-cleansing.zip';
                    document.body.appendChild(a);
                    a.click();
                    $scope.cleansingProgress = false;
                    $scope.firstPart = false;
                    $scope.secoundPart = true;
                    chartToDisplay();
                    

                }
                else {
                    var file = new Blob([data], { type: 'text/csv' });
                    var fileURL = URL.createObjectURL(file);
                    var a = document.createElement('a');
                    a.href = fileURL;
                    a.target = '_blank';
                    a.download = 'data-cleansing.csv';
                    document.body.appendChild(a);
                    a.click();
                    $scope.cleansingProgress = false;
                    $scope.firstPart = false;
                    $scope.secoundPart = true;
                    chartToDisplay();
                   
                }
            })
            .error(function (data, status, headers, config) {
                alert('error');
            })
        }

        function uploadProgress(evt) {
            $scope.$apply(function () {
                if (evt.lengthComputable) {
                    $scope.progress = Math.round(evt.loaded * 100 / evt.total)
                } else {
                    $scope.progress = 'unable to compute'
                }
            })
        }

        function uploadComplete(evt) {

        }

        function uploadFailed(evt) {
            alert("There was an error attempting to upload the file.")
        }

        function uploadCanceled(evt) {
            $scope.$apply(function () {
                $scope.progressVisible = false
            })
            alert("The upload has been canceled by the user or the browser dropped the connection.")
        }

        function chartToDisplay() {
            
            var recordsFound;
            var recordsNotFound;
            var recordsUploaded;

            $http.get('/Address/Chart/')
            .success(function (data, status, headers, config) {
                for (var i = 0; i < data.length; i++) {
                    recordsFound = data[i].AddressCorrected;
                    recordsNotFound = data[i].AddressNotFound;
                    recordsUploaded = data[i].RecordsUploaded;
                }
                var chart = new CanvasJS.Chart("chartContainer",
                {
                    backgroundColor: "#F3F6F9",
                    title: {
                        text: "Address Cleansing Results. Total records submited " + recordsUploaded
                    },
                    animationEnabled: true,
                    data: [
                    {
                        type: "doughnut",
                        startAngle: 2,
                        toolTipContent: "{legendText}: {y} - <strong>#percent% </strong>",
                        showInLegend: true,
                        dataPoints: [
                            { y:recordsFound, indexLabel: "Address corected #percent%", legendText: "Address corected" },
                            { y: recordsNotFound, indexLabel: "Address not found #percent%", legendText: "Address not found" }

                        ]
                    }
                    ]
                });
                chart.render();
            })
        }
    })
