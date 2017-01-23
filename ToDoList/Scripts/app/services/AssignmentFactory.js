(function () {
    'use strict';

    angular
        .module('app')
        .factory('AssignmentFactory', AssignmentFactory);

    AssignmentFactory.$inject = ['$http'];

    function AssignmentFactory($http) {

        var assigments = {}

        var service = {
            addAssigments: addAssigments,
            removeAssigments: removeAssigments,
            getAssigments: getAssigments
        };

        return service;

        function addAssigments(obj)
        {
            assigments = obj;
        }

        function removeAssigments() {
            assigments = {};
        }

        function getAssigments() {
            return assigments;
        }
    }
})();