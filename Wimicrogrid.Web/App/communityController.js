app.controller('CommunityController', ['$scope', '$http', function ($scope, $http) {

    $scope.community = [];

    var updateCommunity = function () {
        $http.get("/api/community")
            .success(function (data) {
                $scope.community = data;
            })
            .error(function (data, status) {
                $scope.status = status;
            });
    };

    updateCommunity();
}]);
