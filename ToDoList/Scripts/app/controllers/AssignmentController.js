(function () {
    'use strict';

    angular
        .module('app')
        .controller('AssignmentController', AssignmentController);

    AssignmentController.$inject = ['$scope', '$http', 'AssignmentFactory', '$location'];

    function AssignmentController($scope, $http, AssignmentFactory, $location) {
        /* jshint validthis:true */
        var vm = this;       

        vm.assigments = AssignmentFactory.getAssigments();

        var formdata = new FormData();

        var params = {};

        var putFile;

        vm.getTheFiles = getTheFiles;

        function getTheFiles($files) {

            vm.files = {};

            for (var i = 0; i < $files.lenght; i++) {
                var reader = new FileReader();
                reader.fileName = $files[i].name;

                reader.onload = function (event) {
                    var file = {};
                    file.name = event.target.fileName;
                    file.Src = event.target.result;

                    vm.files.push(file);
                }

                reader.readAsDataURL($files[i]);
            }

            

            angular.forEach($files, function (value, key) {                
                formdata.append('file', value);
            });

        }

        vm.save = save;

        vm.cancel = cancel;

        function save() {
            var _method = "POST";
            var _url = "Add";
            var _messageSuccess = "Successfully inserted record!"

            if (vm.assigments.Id)
            {
                var _method = "PUT";
                var _url = "Edit/" + vm.assigments.Id;
                _messageSuccess = "Successfully updated record!"
            }

            formdata.append('Name', vm.assigments.Name);

            $http({
                url: '/api/Assignment/'+_url,
                method: _method,
                headers: { 'Content-Type': undefined },
                data: formdata,
                transformRequest: angular.identity
            })           
            .then(function successCallback(data, status, headers, config) {
                iziToast.success({
                    title: 'OK',
                    message: _messageSuccess,
                });
                $location.path('/home');
            }
            , function errorCallback(data, status, headers, config) {
                iziToast.error({
                    title: 'Error',
                    message: 'Occurred an error',
                });
                console.log('error');
                console.log(data);
            });
        }

        function cancel() {
            $location.path('/home');
        }
    }
})();
