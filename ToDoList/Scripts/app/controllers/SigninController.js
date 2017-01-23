(function () {
    'use strict';

    angular
        .module('app')
        .controller('SigninController', SigninController);

    SigninController.$inject = ['$rootScope', '$http', '$cookies', '$cookieStore', '$location', '$routeParams'];

    function SigninController($rootScope, $http, $cookies, $cookieStore, $location, $routeParams) {
        /* jshint validthis:true */
        var vm = this;

        vm.message = $routeParams.message;
        vm.signIn = signIn;
        vm.register = register;

        function signIn() {
            var params = "grant_type=password&username=" + vm.username + "&password=" + vm.password;

            $http({
                url: '/Token',
                method: "POST",
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                data: params
            })
            .then(function successCallback(response, status, headers, config) {
                $http.defaults.headers.common.Authorization = "Bearer " + response.data.access_token;                
                $cookieStore.put('_Token', response.data.access_token);                
                $location.path('/home');
            },
            function errorCallback(data, status, headers, config) {
                iziToast.error({
                    title: 'Error',
                    message: 'Verify you email and/or password',
                });
            });
        }

        function register() {
            $location.path('/register');
        }

        welcome();

        function welcome() {
            iziToast.show({
                class: 'test',
                color: 'dark',
                icon: 'icon-contacts',
                title: 'Hello!',
                message: 'Welcome the my project. Do you like it?',
                position: 'topCenter',
                transitionIn: 'flipInX',
                transitionOut: 'flipOutX',
                progressBarColor: 'rgb(0, 255, 184)',
                image: 'https://avatars2.githubusercontent.com/u/2992930?v=3&s=460',
                imageWidth: 70,
                layout: 2,
                onClose: function () {
                    console.info('onClose');
                },
                iconColor: 'rgb(0, 255, 184)'
            });
        }
    }
})();
