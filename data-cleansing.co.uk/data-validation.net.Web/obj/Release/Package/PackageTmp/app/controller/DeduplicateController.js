'use strict';
dataApp.controller('DeduplicateController',
    function DeduplicateController($scope, $http, $location) {
        $scope.stepOne = true;
        $scope.stepTwo = false;
        $scope.stepThree = false;
        $scope.stepFour = false;
        $scope.warning = false;
        $scope.checkedCol = [];
        var resultData = [];
        $scope.columns = [];


        //====== PREVENT COPY =====
        function md(e) {
            try { if (event.button == 2 || event.button == 3) return false; }
            catch (e) { if (e.which == 3) return false; }
        }
        document.oncontextmenu = function () { return false; }
        document.ondragstart = function () { return false; }
        document.onmousedown = md;

        //============== DRAG & DROP =============
        var dropbox = document.getElementById("dropbox")
        $scope.dropText = 'Drop a CSV file here...'

        // init event handlers
        function dragEnterLeave(evt) {
            evt.stopPropagation()
            evt.preventDefault()
            $scope.$apply(function () {
                $scope.dropText = 'Drop a CSV file here...'
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
                $scope.dropText = ok ? 'Drop a CSV file here...' : 'Only file are allowed!'
                $scope.dropClass = ok ? 'over' : 'not-available'
            })
        }, false)
        dropbox.addEventListener("drop", function (evt) {

            evt.stopPropagation()
            evt.preventDefault()
            $scope.$apply(function () {
                $scope.dropText = 'Drop file here...'
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


        //Show all attached files
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
        //Remove attached file
        $scope.removeElement = function (index) {
            $scope.files.splice(index, 1);
        }

        //========= GETTING COLUMN ==========

        $scope.uploadFile = function () {

            var allData = [];
            var fileNumber = 0;

            var fd = new FormData();

            for (var i in $scope.files) {
                fd.append("fileToUpload", $scope.files[i])
                fileNumber++;
            }
            $scope.cleansingProgress = true;
            var uploadUrl = '/Deduplicate/GetColumn/';

            $http.post(uploadUrl, fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined },
            })
            .success(function (data, status, headers, config) {
                $scope.time = data[0];
                data.splice(0, 1);

                for (var i = 0; i < data.length; i++) {
                    allData[i] = data[i];
                }
                $scope.cleansingProgress = false;
                $scope.stepOne = false;
                $scope.stepTwo = true;
                $scope.columns = allData;
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

        //====== HOLD WITCH COLUMNS ARE CHECKED
        $scope.user = {
            roles: ['']
        };

        //====== PREPARE PREVIEW ===========

        $scope.submitFile = function (matchCriteria, time) {

            //=== Prepare what will send to controller
            var ff = new FormData();

            ff.append("matchCriteria", matchCriteria);
            ff.append("toggleCheck", $scope.user.columns);
            ff.append("time", $scope.time);

            $scope.cleansingProgress = true;
            var uploadUrl = '/Deduplicate/GetData/';

            $http.post(uploadUrl, ff, {
                withCredentials: false,
                headers: {
                    'Content-Type': undefined
                },
                transformRequest: angular.identity
            })
            .success(function (data, status, headers, config) {
                $scope.stepTwo = false;
                $scope.stepThree = true;
                $scope.cleansingProgress = false;
                $scope.price = data[0];

                data.splice(0, 1);

                $(document).ready(function () {

                    var container = document.getElementById('basic_example');

                    var hot = new Handsontable(container, {
                        data: data,
                        height: 396,
                        colHeaders: $scope.columns,
                        rowHeaders: true,
                        stretchH: 'all',
                        columnSorting: true,
                        contextMenu: true
                    });
                });
            })
            .error(function (data, status, headers, config) {
                alert("You don't have enought credits. You will be now redirected to TopUp page.");
                $scope.stepTwo = false;
                $scope.cleansingProgress = false;
                $location.path("/topUp");
            })
        }

        $scope.downloadFile = function (matchCriteria, time) {

            var ff = new FormData();
            ff.append("matchCriteria", matchCriteria);
            ff.append("toggleCheck", $scope.user.columns);
            ff.append("time", $scope.time);
            
            for (var i in $scope.columns) {
                ff.append("columns", $scope.columns[i])
            }


            $scope.cleansingProgress = true;
            var uploadUrl = '/Deduplicate/DownloadFile/';

            $http.post(uploadUrl, ff, {
                withCredentials: false,
                headers: {
                    'Content-Type': undefined
                },
                transformRequest: angular.identity
            })
            .success(function (data, status, headers, config) {
                var file = new Blob([data], { type: 'text/csv' });
                var fileURL = URL.createObjectURL(file);
                var a = document.createElement('a');
                a.href = fileURL;
                a.target = '_blank';
                a.download = 'data-cleansing.csv';
                document.body.appendChild(a);
                a.click();
                $scope.cleansingProgress = false;
                $scope.stepThree = false;
                $scope.stepFour = true;
                chartToDisplay();
            })
            .error(function (data, status, headers, config){
                alert('error');
            })
        }

        function chartToDisplay() {
            
            var duplicateRecords;
            var uniqueRecords;
            var submitedRecords;

            $http.get('/Deduplicate/Chart/')
            .success(function (data, status, headers, config) {
                for (var i = 0; i < data.length; i++) {
                    uniqueRecords = data[i].UniqueRecords;
                    duplicateRecords = data[i].DuplicateRecords;
                    submitedRecords = data[i].SubmitedRecords;
                }
                var chart = new CanvasJS.Chart("chartContainer",
                {
                    backgroundColor: "#F3F6F9",
                    title: {
                        text: "Deduplicate Results. Total records submited " + submitedRecords
                    },
                    animationEnabled: true,
                    data: [
                    {
                        type: "doughnut",
                        startAngle: 2,
                        toolTipContent: "{legendText}: {y} - <strong>#percent% </strong>",
                        showInLegend: true,
                        dataPoints: [
                            { y: uniqueRecords, indexLabel: "Unique records #percent%", legendText: "Unique records" },
                            { y: duplicateRecords, indexLabel: "Duplicate records found #percent%", legendText: "Duplicate records" }

                        ]
                    }
                    ]
                });
                chart.render();
            })
        }
    })