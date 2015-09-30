'use strict';

dataCleansingApp.controller('EmailCleansingController',
    function EmailCleansingController($scope, $http, Upload) {
        var dropbox = document.getElementById("dropbox");
        $scope.downloadDiv = false;
        $scope.dropText = 'Drop files here...'


        // init event handlers
        function dragEnterLeave(evt) {
            evt.stopPropagation()
            evt.preventDefault()
            $scope.$apply(function () {
                $scope.dropText = 'Drop files here...'
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
                $scope.dropText = ok ? 'Drop files here...' : 'Only files are allowed!'
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
                        if (files[i].size > 1024 * 1024) {
                            alert('File ' + files[i].name + ' is too big!');
                            continue;
                        }
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
                    if (element.files[i].size > 1024 * 1024) {
                        alert('File ' + element.files[i].name + ' is too big!');
                        continue;
                    }
                    $scope.files.push(element.files[i])
                }
                $scope.progressVisible = false
            });
        };

        $scope.removeElement = function (index) {
            $scope.files.splice(index, 1);
        }

        $scope.results=[];

        $scope.uploadFile = function () {
            var fd = new FormData();
            
            for (var i in $scope.files) {
                fd.append("fileToUpload", $scope.files[i])
            }

            $scope.cleansingProgress = true;
            var uploadUrl = '/EmailCleansing/GetData';

            $http.post(uploadUrl, fd, {
                withCredentials: false,
                headers: {
                    'Content-Type': undefined
                },
                transformRequest: angular.identity
            })
            .success(function (data, status, headers, config) {
                $scope.time = data[0];
                data.splice(0, 1);
                $(document).ready(function () {
                    $scope.downloadDiv = true;
                    var container = document.getElementById('basic_example');

                    $scope.results = data;

                    var hot = new Handsontable(container, {
                        data: data,
                        height: 396,
                        colHeaders: ["Email","Is Valid?","Message","Mx Record"],
                        rowHeaders: true,
                        stretchH: 'all',
                        columnSorting: true,
                        contextMenu: true
                    });
                });
                $scope.cleansingProgress = false;
            })
            .error(function (data, status, headers, config) {
                console.log(data);
                if (data == 404) {
                    alert('No more that 10 records please!');

                }
                $scope.cleansingProgress = false;
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

        
    })