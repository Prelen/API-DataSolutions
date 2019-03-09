angular.module('DataSolutionApp', [])
    .controller('ChangePasswordController', function ($scope, $http) {
        $scope.pageLoad = function () {
            $(".preloader").fadeOut();
            $scope.isPasswordMatched = true;
            $scope.newpassword = '';
            $scope.confirmnewpassword = '';
            $scope.isProcessing = false;
            $scope.toggleMessage = false;
            $scope.responseMessage = '';
            $scope.buttonText = 'Submit';
            $scope.togglePassowrdInfo = false;
            $scope.isPasswordMatched = true;
            $scope.passwordMessage = '';
            $scope.newPasswordTitle = 'Show Password';
            $scope.togglePassowrdInfo = false;
            $scope.confirmPasswordTitle = 'Show Password';
            $scope.toggleConfirmPassword = 'false';
            $scope.newPasswordType = 'password';
            $scope.confirmPasswordType = 'password';
            $scope.toggleNewPassword = true;
            $scope.toggleConfirmPassword = true;
        };

        $scope.comparePasswords = function () {

            if ($scope.newpassord != null && $scope.confirmpassword != null) {
                if ($scope.newpassord.trim() !== $scope.confirmpassword.trim()) {
                    $scope.isPasswordMatched = false;
                    $scope.passwordMessage = 'Passwords do not match';
                }
                else {
                    $scope.isPasswordMatched = true;
                }
            }

        };

        $scope.checkPassword = function (password) {
            if (password.length < 6) {
                $scope.passwordMessage = 'Password is too short';
            } else if (password.length > 20) {
                $scope.passwordMessage = 'Password is too long';
            } else if (password.search(/\d/) === -1) {
                $scope.passwordMessage = 'Password must contain a number';
            } else if (password.search(/[a-zA-Z]/) === -1) {
                $scope.passwordMessage = 'Password must contain a letter';
            } else if (username.search(/[^a-zA-Z0-9]/) < 0) {
                $scope.passwordMessage = 'Username must contain a special character';
            }
            else {
                $scope.passwordMessage = '';
            }

            if ($scope.passwordMessage.trim() !== '') {
                $scope.isPasswordMatched = false;

            }
            else {
                $scope.isPasswordMatched = true;

            }

            $scope.togglePassowrdInfo = !$scope.isPasswordMatched;
        };

        $scope.$watch('isProcessing', function () {
            $scope.buttonText = $scope.isProcessing === true ? 'Processing' : 'Submit';

        });

        $scope.showNewPassword = function () {
            if ($scope.toggleNewPassword === true) {
                $scope.toggleNewPassword = false;
                $scope.newPasswordTitle = 'Hide Password';
                $scope.newPasswordType = 'text';
            }
            else {
                $scope.toggleNewPassword = true;
                $scope.newPasswordTitle = 'Show Password';
                $scope.newPasswordType = 'password';
            }
        };

        $scope.showConfirmPassword = function () {
            if ($scope.toggleConfirmPassword === true) {
                $scope.toggleConfirmPassword = false;
                $scope.confirmPasswordTitle = 'Hide Password';
                $scope.confirmPasswordType = 'text';
            }
            else {
                $scope.toggleConfirmPassword = true;
                $scope.confirmPasswordTitle = 'Show Password';
                $scope.confirmPasswordType = 'password';
            }
        };

        $scope.savePassword = function () {
            $scope.isProcessing = true;
            $http(
                {
                    method: 'POST',
                    url: '/Users/User/UpdatePassword',
                    params: {
                        NewPassword: $scope.newpassord.trim()
                    }
                }
            ).then(function (response) {
                if (response.data != null) {
                    $scope.toggleMessage = true;
                    $scope.responseResult = response.data;
                    if ($scope.responseResult) {
                        $scope.responseMessage = 'Password has been updated.';
                    }
                    else {
                        $scope.responseMessage = 'An error has occurred. Please try again or contact the system administrator.';
                    }
                }

                $scope.isProcessing = false;
            });
        };

    });