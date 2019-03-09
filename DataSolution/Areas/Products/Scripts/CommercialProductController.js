angular.module('DataSolutionApp', [])
    .controller('CommercialProductController', function ($scope,$http) {
        $(".preloader").fadeOut();

        $scope.pageLoad = function () {
            $scope.showButton = true;
            $scope.isProcessing = false;
            $scope.buttonText = 'Submit';
            $scope.selectedReport = '0';
            $scope.reports = [];
            $scope.report = {};
            $scope.GetReports();
            $scope.functionCall = '';
            $scope.params = '';
        };

       

      
      
        $scope.GetReports = function () {
            $http(
                {
                    method: 'GET',
                    url: '/Products/Product/GetConsumerProducts',
                    params: {
                        ProductType: 2
                    }
                }

            ).then(function (response) {
                if (response.data != null) {
                    var json = JSON.stringify(response.data);
                    var reportlist = JSON.parse(json);

                    $scope.report = {
                        reportID: '0',
                        reportName: 'Select a report'
                    };
                    $scope.reports.push($scope.report);

                    for (var i = 0; i < reportlist.length; i++) {
                        $scope.report = {
                            reportID: reportlist[i].ProductID,
                            reportName: reportlist[i].ProductName
                        };
                        $scope.reports.push($scope.report);
                    }

                }
            });
        };

        $scope.callProduct = function () {
            $scope.isProcessing = true;
            switch ($scope.selectedReport) {
                case 4:
                    $scope.functionCall = 'BMSAlert';
                    $scope.params = {
                        StarDate: $scope.startDate, EndDate: $scope.endDate
                    };
                    break;
                default:
                    break;
            }

            var funcurl = '/Products/Product/' + $scope.functionCall;

            $http(
                {
                    method: 'POST',
                    url: funcurl,
                    params: $scope.params
                }
            ).then(function(response) {
                if (response.data != null) {
                    var json = JSON.stringify(response.data);
                    var result = JSON.parse(json);
                }
                $scope.isProcessing = false;
            });
        };
    });