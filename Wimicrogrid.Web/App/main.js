var app = angular.module('wimicroGridApp', ['ngRoute'])
    .directive('draggable', function() {
        return function(scope, element) {
            var el = element[0];

            el.draggable = true;

            el.addEventListener(
                'dragstart',
                function(e) {
                    e.dataTransfer.effectAllowed = 'move';
                    e.dataTransfer.setData('Text', this.id);
                    this.classList.add('drag');
                    return false;
                },
                false
            );

            el.addEventListener('dragend', function() {
                this.classList.remove('drag');
                return false;
            }, false);
        }
    })
    .directive('droppable', function () {
        return {
            scope: {
                drop: '&',
                inside: '='
            },
            link: function (scope, element) {
                var el = element[0];

                el.addEventListener(
                    'dragover',
                    function (e) {
                        e.dataTransfer.dropEffect = 'move';
                        if (e.preventDefault) e.preventDefault();
                        this.classList.add('over');
                        return false;
                    },
                    false
                );

                el.addEventListener(
                    'dragenter',
                    function () {
                        this.classList.add('over');
                        return false;
                    },
                    false
                );

                el.addEventListener(
                    'dragleave',
                    function () {
                        this.classList.remove('over');
                        return false;
                    },
                    false
                );

                el.addEventListener(
                    'click',
                    function (e) {
                        if (e.stopPropagation) e.stopPropagation();

                        var appliance = document.getElementById(e.target.id);

                        scope.$parent.clickAppliancePowerSwitch(appliance);
                        scope.$apply('click()');

                        return false;
                    },
                    false
                );

                el.addEventListener(
                    'drop',
                    function (e) {
                        if (e.stopPropagation) e.stopPropagation();

                        this.classList.remove('over');

                        var appliance = document.getElementById(e.dataTransfer.getData('Text'));

                        if (this.classList[0] == 'storeroom')
                        {
                            scope.$parent.removeAppliance(appliance);
                        }

                        if (this.classList[0] == 'inside') {
                            scope.$parent.addAppliance(appliance);
                        }

                        scope.$apply('drop()');

                        return false;
                    },
                    false
                );
            }
        }
    })
    .config(function($routeProvider) {
        $routeProvider
            .when('/household/:id', {
                templateUrl: 'household.html',
                controller: 'HouseholdController'
            })
            .otherwise({ redirectTo: '/' });
    });