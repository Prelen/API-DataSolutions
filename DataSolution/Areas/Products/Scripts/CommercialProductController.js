angular.module('DataSolutionApp', [])
    .controller('CommercialProductController', function ($scope,$http) {
        $(".preloader").fadeOut();

        $scope.pageLoad = function () {
            $scope.showButton = true;
            $scope.isProcessing = false;
            $scope.buttonText = 'Submit';
            $scope.selectedReport = '0';
            $scope.selectedCriteria = '0';
            $scope.reports = [];
            $scope.report = {};
            $scope.functionCall = '';
            $scope.params = '';
            $scope.businessName = '';
            $scope.searchTypes = [];
            $scope.searchType = {};
            $scope.GetReports();
            $scope.callsearchTypes();
            $scope.selectedEnquiryReason = '0';
            $scope.enquires = [];
            $scope.enquire = {};
            $scope.enquireTypes();
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
                            reportID: reportlist[i].ProductCode.trim(),
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
                case 'ONLINE':
                    $scope.functionCall = 'BMSAlert';
                    $scope.params = {
                        StarDate: $scope.startDate, EndDate: $scope.endDate
                    };
                    break;
                case 'INV':
                    var str = 'TODO';
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

        $scope.callsearchTypes = function () {
            $http(
                {
                    method: 'GET',
                    url: '/Products/Product/GetSearchTypes'
                }
            ).then(function (response) {
                if (response.data != null) {
                    var json = JSON.stringify(response.data);
                    var results = JSON.parse(json);
                    $scope.searchType = {
                        searchID: '0',
                        searchName: 'Select Search type'
                    };

                    $scope.searchTypes.push($scope.searchType);

                    for (var i = 0; i < results.length; i++) {
                        $scope.searchType = {
                            searchID: results[i].SearchCode.trim(),
                            searchName: results[i].SearchDescription
                        };
                        $scope.searchTypes.push($scope.searchType);
                    }
                }
            });
        };

        $scope.enquireTypes = function () {
            $http(
                {
                    method: 'GET',
                    url: '/Products/Product/GetEnquirerReasons'
                }
            ).then(function (response) {
                if (response.data != null) {
                    var json = JSON.stringify(response.data);
                    var enq = JSON.parse(json);
                    $scope.enquire = {
                        enqID: '0',
                        enqDescription: 'Select an Enquiry Type'
                    };

                    $scope.enquires.push($scope.enquire);

                    for (var i = 0; i < enq.length; i++) {
                        $scope.enquire = {
                            enqID: enq[i].EnquiryCode.trim(),
                            enqDescription: enq[i].EnquiryDescription
                        };
                        $scope.enquires.push($scope.enquire);
                    }
                }
            });
        };
    });