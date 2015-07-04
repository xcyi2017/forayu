app.controller('CommunityController', ['$rootScope', '$scope', '$http', 'ViewService', function ($rootScope, $scope, $http, ViewService) {

    $scope.community = [];

    $scope.view = ViewService;

    var updateCommunity = function () {
        $http.get("/api/community")
            .success(function (data) {
                $scope.community = data;

                $rootScope.$broadcast('community', 'initialised');
            })
            .error(function (data, status) {
                $scope.status = status;
            });
    };

    //$scope.household = ViewService.isShowing["household"];

    updateCommunity();
}]);
