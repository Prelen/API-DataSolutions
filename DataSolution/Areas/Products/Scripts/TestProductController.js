angular.module('TestProductApp', [])
    .controller('TestProductController', function ($scope, $http) {
        $scope.idNumber = '';
        $scope.userID = '4';

        $scope.callProduct = function () {
            var y = '';
            $http(
                {
                    method: 'GET',
                    url: '/Products/Product/GetTestProduct',
                    params: {
                        IDNumber: $scope.idNumber
                    }
                }
            ).then(function (response) {
                var x = response.data;
            });
        };

    });