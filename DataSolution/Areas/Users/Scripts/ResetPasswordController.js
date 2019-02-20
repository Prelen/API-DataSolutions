angular.module('ResetPasswordApp', [])
    .controller('ResetPasswordController', function ($scope, $http,$timeout,$location) {
        $(".preloader").fadeOut();
        $scope.isPasswordMatched = true;
        $scope.passwordMessage = '';
        $scope.toggleTempPassword = true;
        $scope.toggleLoginButton = false;
        $scope.buttonMessage = 'Reset Password';
        $scope.toggleLoginButton = false;
        $scope.responseLogin = false;
        $scope.responseMessage = '';
        //Toggle Old Password
        $scope.toggleOldPassword = true;
        $scope.oldPasswordTitle = 'Show Password';
        $scope.oldPasswordType = 'password';
        $scope.showOutcome = false;
        //Toggle new Password
        $scope.toggleNewPassword = true;
        $scope.newPasswordTitle = 'Show Password';
        $scope.newPasswordType = 'password';

        //Toggle confirm Password
        $scope.toggleConfirmPassword = true;
        $scope.confirmPasswordTitle = 'Show Password';
        $scope.confirmPasswordType = 'password';

        $scope.showOldPassword = function () {
            if ($scope.toggleOldPassword  === true) {
                $scope.toggleOldPassword = false;
                $scope.oldPasswordTitle = 'Hide Password';
                $scope.oldPasswordType = 'text';
            }
            else {
                $scope.toggleOldPassword = true;
                $scope.oldPasswordTitle = 'Show Password';
                $scope.oldPasswordType = 'password';
            }
        };

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

        };

        $scope.comparePasswords = function () {

            if ($scope.newpassword != null && $scope.confirmnewpassword != null) {
                if ($scope.newpassword.trim() !== $scope.confirmnewpassword.trim()) {
                    $scope.isPasswordMatched = false;
                    $scope.passwordMessage = 'Passwords do not match';
                }
                else {
                    $scope.isPasswordMatched = true;
                }
            }

        };

        $scope.showPasswordInfo = function () {
            if ($scope.togglePassowrdInfo === true) {
                $scope.togglePassowrdInfo = false;
            }
            else {
                $scope.togglePassowrdInfo = true;
            }
        };


       
        $scope.checkTempPassword = function () {
            $scope.showProcessing = true;
            toggleLoginButton = true;

            $http(
                {
                    method: 'POST',
                    url: '/Users/User/CheckTempPassword',
                    params: { TempPassword: $scope.oldpassword }
                }

            ).then(function (response) {
                $scope.toggleTempPassword = response.data;
                if ($scope.toggleTempPassword === true) {
                    toggleLoginButton = false;
                }
                $scope.showProcessing = false;
            });

        };

        $scope.$watch('showProcessing', function () {
            $scope.buttonMessage = $scope.showProcessing === true ? 'Processing' : 'Reset Password';

        });


        $scope.redirectToUserHome = function () {

            var currentURL = $location.absUrl();
            var homepage = currentURL.replace('ResetPassword', 'Home');
            window.location.href = homepage;
        };

        $scope.ResetPassword = function () {
            
            $scope.showProcessing = true;
            $http(
                {
                    method: 'POST',
                    url: '/Users/User/PasswordReset',
                    params: { NewPassword: $scope.newpassword }
                }
            ).then(function(response) {
                $scope.responseLogin = response.data;
                if ($scope.responseLogin) {
                    $scope.responseMessage = 'Password reset was successful. You will now be redirected to the Home page.';
                }
                else {
                    $scope.responseMessage = 'Password reset was unsuccessful. Please contact the system adminsitrator or try again.';
                }
                

                $scope.showProcessing = false;
                $scope.showOutcome = true;

            }).finally(function () {
                if ($scope.responseLogin) {

                    $timeout(function () { $scope.redirectToUserHome(); }, 1000);
                }
            });
        };
    });