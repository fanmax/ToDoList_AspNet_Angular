(function() {
    'use strict';

    angular
        .module('app')
        .directive('ngFiles', ngFiles);

    ngFiles.$inject = ['$parse'];
    
    function ngFiles($parse) {
        var directive = {
            link: link,
        };
        return directive;

        function link(scope, element, attrs) {
            var onChange = $parse(attrs.ngFiles);

            element.on('change', function (event) {
                onChange(scope, {$files: event.target.files })
            });
        }
    }

})();