(function () {
    'use strict';

    var pages = '/Scripts/app/pages/';

    angular.module('app', [
    'ngRoute',
    'ngCookies',
    'ui.bootstrap'
    ])
.config(['$provide', '$routeProvider', '$httpProvider', function ($provide, $routeProvider, $httpProvider) {

    //====================================
    //AJAX errors
    //====================================
    $httpProvider.interceptors.push(['$q', '$location', function ($q, $location) {
        return {
            'responseError': function (response) {
                if (response.status === 401)
                    $location.url('/signin');
                return $q.reject(response);
            }
        };
    }]);


    //====================================
    // Routes
    //====================================
    $routeProvider.when('/home', {
        templateUrl: pages+'home.html',
        controller: 'HomeController as homeVm'
    });

    $routeProvider.when('/assigment', {
        templateUrl: pages + 'formAssignment.html',
        controller: 'AssignmentController as assignmentVm'
    });

    $routeProvider.when('/signin/:message?', {
        templateUrl: pages + 'signin.html',
        controller: 'SigninController as signinVm'
    });

    $routeProvider.when('/register', {
        templateUrl: pages + 'register.html',
        controller: 'RegisterController as registerVm'
    });

    $routeProvider.otherwise({
        redirectTo: '/home'
    });

}])
.run(['$http', '$cookies', '$cookieStore', function ($http, $cookies, $cookieStore) {
    $http.defaults.headers.common.Authorization = 'Bearer ' + $cookieStore.get('_Token');
    $http.defaults.headers.common.RefreshToken = $cookieStore.get('_RefreshToken');
}])
})();