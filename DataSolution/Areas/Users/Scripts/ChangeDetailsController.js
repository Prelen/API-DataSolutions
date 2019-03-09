angular.module('DataSolutionApp', [])
    .controller('ChangeDetailsController', function ($scope, $http) {

        $scope.pageLoad = function () {
            $(".preloader").fadeOut();
            $scope.isProcessing = false; 
            $scope.buttonText = 'Submit';
            $scope.toggleRecoverMessage = false;
            $scope.firstName = '';
            $scope.surname = '';
            $scope.companyName = '';
            $scope.email = '';
            $scope.mobileNo = '';
            $scope.businessNo = '';
            $scope.GetUserDetails();
            $scope.responseResult = true;
            $scope.toggleMessage = false;
            $scope.responseMessage = '';
        };

        $scope.GetUserDetails = function () {
            $http(
                {
                    method: 'GET',
                    url: '/Users/User/GetUserDetails'
                }
            ).then(function (response) {
                if (response.data != null) {
                    var jsonresult = JSON.stringify(response.data);
                    var user = JSON.parse(jsonresult);
                    $scope.firstName = user[0].FirstName;
                    $scope.surname = user[0].Surname;
                    $scope.companyName = user[0].OrganizationName;
                    $scope.email = user[0].Email;
                    $scope.mobileNo = user[0].MobileNo;
                    $scope.businessNo = user[0].WorkNo;

                }
            });
        };

        $scope.saveDetails = function () {
            $scope.isProcessing = true;
            $http(
               {
                    method: 'POST',
                    url: '/Users/User/SaveUserDetails',
                    params: {
                        FirstName: $scope.firstName, Surname: $scope.surname, CompanyName: $scope.companyName, Email: $scope.email,
                        MobileNo: $scope.mobileNo, BusinessNo: $scope.businessNo
                    }
                }
            ).then(function (response) {
                if (response.data != null) {
                    $scope.responseResult = response.data;
                    if ($scope.responseResult) {
                        $scope.responseMessage = 'User details have been updated.';
                    }
                    else {
                        $scope.responseMessage = 'An error has occurred. Please try again or contact the system administrator.';
                    }
                }
                $scope.toggleMessage = true;
                $scope.isProcessing = false;
            });
        };

        $scope.$watch('isProcessing', function () {
            $scope.buttonText = $scope.showProcessing === true ? 'Processing' : 'Submit';
            
        });
    });