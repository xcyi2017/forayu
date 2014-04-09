function CommunityConsumptionController($scope, $http, $routeParams, $timeout)
{
    $scope.consumption = [];

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

    updateConsumption();
}