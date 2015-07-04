function CommunityConsumptionController($rootScope, $scope, $http, $routeParams, $timeout, ViewService)
{
    $scope.consumption = [];

    $scope.view = ViewService;

    var updateConsumption = function () {
        $http.get("/api/consumption")
            .success(function (data) {
                $scope.consumption = data;
            })
            .error(function (data, status) {
                $scope.status = status;
            });

        $timeout(updateConsumption, 1000);
    };

    $rootScope.$on('community', function() {
        updateConsumption();
    });
}