'use strict';

dataApp.controller('BicFileUploadController',
    function BicFileUploadController($scope, $http) {
        var dropbox = document.getElementById("dropbox")
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
                        $scope.files.push(files[i])
                    }
                })
            }
        }, false)
        //============== DRAG & DROP =============

        $scope.setFiles = function (element) {
            $scope.$apply(function ($scope) {
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
            var uploadUrl = '/BicCleansing/BulkBic/';

            $http.post(uploadUrl, fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined },
                responseType: 'arraybuffer'
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
                }
            })
            .error(function (data, status, headers, config) {
                if(data == 404)
                    alert("You don't have enought credits! Please TopUp.")
                else if(data == 415)
                    alert("Wrong file format!")
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
