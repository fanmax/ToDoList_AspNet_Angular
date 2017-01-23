(function () {
    'use strict';

    angular
        .module('app')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$http', '$cookieStore', '$uibModal', '$window', 'AssignmentFactory', '$location'];

    function HomeController($http, $cookieStore, $uibModal, $window, AssignmentFactory, $location) {
        /* jshint validthis:true */
        var vm = this;

        var pages = '/Scripts/app/pages/';

        vm.assignments = {};

        vm.list = list;

        list();

        vm.add = add;

        vm.edit = edit;

        vm.remove = remove;

        vm.complete = complete;

        vm.openNew = openNew;

        function add() {
            AssignmentFactory.removeAssigments();
            $location.path('/assigment');
        }

        function edit(data) {
            AssignmentFactory.addAssigments(data);
            $location.path('/assigment');
        }

        function remove(id) {

            iziToast.show({
                color: 'red',
                icon: 'glyphicon glyphicon-remove',
                title: 'Hey',
                message: 'You have certain what can remove this register?',
                position: 'center', // bottomRight, bottomLeft, topRight, topLeft, topCenter, bottomCenter
                progressBarColor: 'rgb(0, 255, 184)',
                buttons: [
                    ['<button>Ok</button>', function (instance, toast) {
                        $http({
                            url: '/api/Assignment/Remove/' + id,
                            method: "DELETE",
                        })
                        .then(function successCallback(response, status, headers, config) {
                            list();
                        }
                        , function errorCallback(data, status, headers, config) {
                            console.log(data);
                        });

                        instance.hide({
                            transitionOut: 'fadeOutUp',
                            onClose: function (instance, toast, closedBy) {
                                console.info('closedBy: ' + closedBy); //btn2
                            }
                        }, toast, 'close', 'btn2');
                    }],
                    ['<button>Close</button>', function (instance, toast) {
                        instance.hide({
                            transitionOut: 'fadeOutUp',
                            onClose: function (instance, toast, closedBy) {
                                console.info('closedBy: ' + closedBy); //btn2
                            }
                        }, toast, 'close', 'btn2');
                    }]
                ],
                onOpen: function (instance, toast) {
                    
                },
                onClose: function (instance, toast, closedBy) {
                    
                }
            });



        }

        function complete(id) {
            $http({
                url: '/api/Assignment/Complete/' + id,
                method: "PUT",
            })
            .then(function successCallback(response, status, headers, config) {
                list();
            }
            , function errorCallback(data, status, headers, config) {
                console.log(data);
            });
        }

        function list() {
            $http({
                url: '/api/assignment/list',
                method: "GET",
            })
            .then(function successCallback(response, status, headers, config) {
                vm.assignments = response.data;
            }
            , function errorCallback(data, status, headers, config) {
                console.log(data);
            });
        }

        function openNew() {
            var modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: pages + 'formAssignment.html',
                controller: 'AssignmentController',
                controllerAs: 'assignmentVm',
            });
        }

        vm.downloadFile = function (data) {
            $window.open(data, "_blank")
        };

    }


})();
