app.controller('ClockController', ['$scope', '$http', '$timeout', function ($scope, $http, $timeout)
{
    $scope.clock = '';
    $scope.clockRunning = 'stopped';

    var updateTime = function () {
        $http.get("/api/time")
            .success(function (data) {
                $scope.clock = data;

                if (CoolClock) {
                    var hours = new Date(data.Current).getHours();
                    var minutes = new Date(data.Current).getMinutes();
                    CoolClock.setTime(hours, minutes);
                }
            })
            .error(function (data, status) {
                $scope.status = status;
            });

        $timeout(updateTime, 1000);
    };

    $scope.startClock = function () {
        $http.get("/api/start")
            .success(function () {
                $scope.clockRunning = 'running';
            })
            .error(function (data, status) {
                $scope.status = status;
            });
    }

    $scope.stopClock = function () {
        $http.get("/api/stop")
            .success(function () {
                $scope.clockRunning = 'stopped';
            })
            .error(function (data, status) {
                $scope.status = status;
            });
    }

    updateTime();
}]);
