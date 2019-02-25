angular.module('DataSolutionApp', [])
    .controller('PersonalProductController', function ($scope,$http) {
        $(".preloader").fadeOut();
        

        $scope.pageLoad = function () {
            $scope.consumerprofile = false;
            $scope.consumerprofileaddress = false;
            $scope.personaltrace = false;
            $scope.isProcessing = false;
            $scope.buttonText = 'Submit';
            $scope.reportType = '0';
            $scope.showButton = false;
            $scope.provinces = [];
            $scope.province = {};
            $scope.GetProvinces();
            $scope.reports = [];
            $scope.report = {};
            $scope.selectedProvince = '0';
            $scope.functionCall = '';
            $scope.params = {};
            $scope.GetReports();
        };

        $scope.callProduct = function () {
            $scope.isProcessing = true;
            switch ($scope.reportType) {
                case 1:
                    $scope.functionCall = 'GetConsumerProfile';
                    $scope.params = {
                        FirstName: $scope.firstname, Surname: $scope.surname, IDNumber: $scope.idNumber, ProductID: $scope.reportType
                    };
                    break;
                case 2:
                    $scope.functionCall = 'GetConsumerProfileWithAddress';
                    $scope.params = {
                        FirstName: $scope.firstname, Surname: $scope.surname, IDNumber: $scope.idNumber, ProductID: $scope.reportType,
                        AddressLin1: $scope.address1, AddressLine2: $scope.address2, Suburb: $scope.suburb, City: $scope.city, ProvinceCode: $scope.selectedProvince
                    };
                    break;
                case 3:
                    $scope.functionCall = 'PersonalTraceOrder';
                    $scope.params = {
                        IDNumber: $scope.idNumber, ProductID: $scope.reportType
                    };
                    break;
                default:
                    breaks;
            }

            var funcurl = '/Products/Product/' + $scope.functionCall;

            $http(
                {
                    method: 'GET',
                    url: funcurl,
                    params: $scope.params

                }
            ).then(function (response) {
                var resp = response.data;

                $scope.isProcessing = false;
            });
        };

        $scope.$watch('isProcessing', function () {
            $scope.buttonText = $scope.isProcessing === true ? 'Processing' : 'Submit';
        });

        $scope.reportChange = function () {
            switch ($scope.reportType) {
                case 0:
                    $scope.consumerprofile = false;
                    $scope.consumerprofileaddress = false;
                    $scope.personaltrace = false;
                    $scope.showButton = false;
                    break;
                case 1 :
                    $scope.consumerprofile = true;
                    $scope.consumerprofileaddress = false;
                    $scope.personaltrace = false;
                    $scope.showButton = true;
                    break;
                case 2 :
                    $scope.consumerprofile = false;
                    $scope.consumerprofileaddress = true;
                    $scope.personaltrace = false;
                    $scope.showButton = true;
                    break;
                case 3:
                    $scope.consumerprofile = false;
                    $scope.consumerprofileaddress = false;
                    $scope.personaltrace = true;
                    $scope.showButton = true;
                    break;
                default:
                    break;
            }
        };

        $scope.GetProvinces = function () {
            $http(
                {
                    method: 'GET',
                    url: '/Products/Product/GetProvinces'

                }

            ).then(function (response) {
                if (response.data) {
                    var json = JSON.stringify(response.data);
                    var provincelist = JSON.parse(json);
                    $scope.province = {
                        provinceCode: '0',
                        provinceName: 'Select a province'
                    };
                    $scope.provinces.push($scope.province);

                    for (var i = 0; i < provincelist.length; i++) {
                        if (provincelist[i] != null) {
                            $scope.province = {
                                provinceCode: provincelist[i].ProvinceCode,
                                provinceName: provincelist[i].ProvinceName
                            };
                            $scope.provinces.push($scope.province);
                        }
                    }
                }

            });
        };

        $scope.GetReports = function () {
            $http(
                {
                    method: 'GET',
                    url: '/Products/Product/GetConsumerProducts',
                    params: {
                        ReportType: $scope.reportType
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
    });