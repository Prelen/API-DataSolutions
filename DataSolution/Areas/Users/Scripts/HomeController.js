angular.module('HomeApp', [])
    .controller('HomeController', function ($scope, $http, $timeout) {
        
        //$scope.GetData();
        $scope.ticks = [];
        $scope.datapoints = [];
        function formatter(val, axis) {
            return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        };

        var options = {
            legend: {
                show: true,
                margin: 10,
                backgroundOpacity: 0.9
            },
            points: {
                show: true,
                radius: 3
            },
            lines: {
                show: true
            },
            grid: { hoverable: true, clickable: false },
            yaxis: { min: 0, tickFormatter: formatter },
            xaxis: { ticks: $scope.ticks }

        };

         $scope.GetData = function () {
             $(".preloader").fadeIn();
             $http(
                 {
                     method: 'GET',
                     url: '/Users/User/GetChartValues'
                 }
             ).then(function (response) {
                 
                 if (response.data !== null) {

                     var json = JSON.parse(response.data);

                     for (var i = 0; i < json.length; i++) {
                         $scope.ticks.push([i, json[i].label]);
                         $scope.datapoints.push([i, json[i].value]);
                     }
                   
                     var dataset = [{ label: "Transactions", data: $scope.datapoints, color: 'purple' }]; 
                     $.plot($("#graph"), dataset, options);
                     $(".preloader").fadeOut();
                 }
             });
        };
    });