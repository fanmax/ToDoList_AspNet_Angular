(function () {
    'use strict';

    angular
        .module('app')
        .controller('RegisterController', RegisterController);

    RegisterController.$inject = ['$http', '$location']; 

    function RegisterController($http, $location) {
        /* jshint validthis:true */
        var vm = this;

        vm.register = register;
        vm.cancel = cancel;

        function register() {
            var params = {
                Email: vm.username,
                Password: vm.password1,
                ConfirmPassword: vm.password2
            };
            $http.post('/api/Account/Register', params)
            .then(function successCallback(data, status, headers, config) {                
                iziToast.success({
                    title: 'OK',
                    message: 'Registration Complete.',
                });
                $location.path('/signin');
            }
            , function errorCallback(data, status, headers, config) {
                console.log('error');
                console.log(data);
                iziToast.error({
                    title: 'Error',
                    message: 'Verify all input.',
                });
            });
        }

        function cancel() {
            $location.path('/signin');
        }

        vm.showAlert = false;
        vm.showSuccess = false;
    }

    
})();
