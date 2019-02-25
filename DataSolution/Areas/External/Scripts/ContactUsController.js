angular.module('DataSolutionApp', [])
    .controller('ContactUsController', function($scope, $http) {
        $scope.responseMessage = '';
        $scope.divStyle = false;
        $scope.divClass = '';
        $scope.toggle = true;
        $scope.buttonToggle = false;

        $(".preloader").fadeOut();


        //Send the email
        $scope.Submit = function () {
            $scope.toggle = false;
            $scope.buttonToggle = true;
          
          
            $http(
                {
                    method: 'POST',
                    url: '/External/External/SendContactEmail',
                    params: { FirstName: $scope.firstName, Surname: $scope.surname, EmailAddress: $scope.emailAddress, ContactNo: $scope.contactNumber, Message: $scope.message }
                }
            ).then(function (data, status, headers, config) {


                $scope.divStyle = true;

                if (data.data === true) {
                    $scope.responseMessage = 'Your enquiry has been successfully submitted.';
                    $scope.divClass = 'success';
                    $scope.firstName = '';
                    $scope.surname = '';
                    $scope.emailAddress = '';
                    $scope.contactNumber = '';
                    $scope.message = '';

                }
                else {
                    $scope.responseMessage = 'An error has occurred. Please try again later.';
                    $scope.divClass = 'failed';
                }

                $scope.toggle = true;
                $scope.buttonToggle = false;
            });

         
        };

        $scope.$watch('toggle', function () {
            $scope.buttonText = $scope.toggle ? 'Submit' : 'Processing';
        });

    });

