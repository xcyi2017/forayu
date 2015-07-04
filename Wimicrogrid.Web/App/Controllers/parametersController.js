app.controller('ParametersController', ['$scope', '$http', '$timeout', 'ViewService', function($scope, $http, $timeout, ViewService) {
    $scope.view = ViewService;
}]);