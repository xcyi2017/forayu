function HouseholdController($scope, $http, $routeParams, $timeout)
{
    $scope.household = [];

    var updateHousehold = function () {
        $http.get("/api/household/" + $routeParams.id)
            .success(function (data) {
                $scope.household = data;
            })
            .error(function (data, status) {
                $scope.status = status;
            });

        $timeout(updateHousehold, 1000);
    };

    $scope.addAppliance = function (appliance)
    {
        var housholdData = { household: $routeParams.id, type: appliance.title };

        $http.post("/api/appliances", housholdData)
            .success(function (data) {
                $scope.household.Appliances.push(data);
            })
            .error(function (response) {
                $scope.errormessage = response.errormessage;
            });
    };

    $scope.removeAppliance = function(appliance)
    {
        $scope.household.Appliances.forEach(function(element, index)
        {
            if (element.Id == appliance.id)
            {
                $http.delete("/api/appliances/" + element.Id)
                    .success(function() {
                        $scope.household.Appliances.splice(index, 1);
                    })
                    .error(function (response) {
                        $scope.errormessage = response.errormessage;
                    });
            };
        });
    }

    $scope.clickAppliancePowerSwitch = function (appliance)
    {
        $scope.household.Appliances.forEach(function (element)
        {
            if (element.Id == appliance.id)
            {
                $http.put("/api/appliances/" + element.Id)
                    .success(function () {
                        element.On = !element.On;
                    })
                    .error(function (response) {
                        $scope.errormessage = response.errormessage;
                    });
            }
        });
    }

    updateHousehold();
}