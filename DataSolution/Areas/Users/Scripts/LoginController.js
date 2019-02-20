angular.module('LoginApp', [])
    .controller('LoginController', function ($scope, $http, $timeout, $location) {
        $(".preloader").fadeOut();

        $scope.showProcessing = false;
        $scope.buttonRecoverText = 'Recover';
        $scope.responseRecover = false;
        $scope.toggleRecoverMessage = false;
        $scope.responseMessage = '';
        $scope.toggleLogin = false;
        $scope.responseLogin = false;
        $scope.loginMessage = '';
        $scope.buttonLoginMessage = 'Login';
        $scope.userInfo = null;
        $scope.passwordMessage = 'Show Password';
        $scope.passwordType = 'password';
        $scope.togglePassword = true;
  

        $("#recover").click(function () {
            $("#loginform").slideUp();
            $("#loginform").fadeOut();
            $("#recoverform").fadeIn();
            $("#recoverform").slideDown();

            $("#recoverEmail").val('');
            $("#recoverMessage").css('display', 'none');
        });

        $("#to-login").click(function () {
            $("#recoverform").slideUp();
            $("#recoverform").fadeOut();
            $("#loginform").fadeIn();
            $("#loginform").slideDown();

            $("#username").val('');
            $("#password").val('');
            $("#loginMessage").css('display', 'none');

            
        });

        $scope.$watch('showProcessing', function() {
            $scope.buttonRecoverText = $scope.showProcessing === true ? 'Processing' : 'Recover';
            $scope.buttonLoginMessage = $scope.showProcessing === true ? 'Processing' : 'Login';
        });


        $scope.recoverPassword = function () {
           
            $scope.showProcessing = true;

            $http(
                {
                    method: 'POST',
                    url: '/Users/User/ResetPassword',
                    params: {
                        Email: $scope.recoveremail
                    }
                }
            ).then(function (response) {
                $scope.responseRecover = response.data;

                if ($scope.responseRecover === true) {
                    $scope.responseMessage = 'Your reset password was successful. Please check your email.';
                    $scope.recoveremail = '';
                }
                else {
                    $scope.responseMessage = 'An error has occurred. Please try again later.';
                }

                $scope.showProcessing = false;
                $scope.toggleRecoverMessage = true;
                $("#recoverMessage").css('display', 'block');
            });
        };


        $scope.Login = function () {

            $scope.showProcessing = true;

            $http(
                {
                    method: 'POST',
                    url: '/Users/User/Login',
                    params: {
                        Username: $scope.username, Password: $scope.password
                    }

                }

            ).then(function (response) {
                $scope.userInfo = response.data;
                
                $scope.toggleLogin = true;
                if ($scope.userInfo.UserID !== 0) {
                    if ($scope.userInfo.IsLocked !== true) {
                        $scope.loginMessage = 'Login was successful. You will now be redirected to the user home page.';
                        $scope.responseLogin = true;
                    }
                    else {
                        $scope.loginMessage = 'Login was unsuccessful. Your account is locked. Please contact the system administrator for further assistance.';
                        
                    }
                }
                else {
                    $scope.loginMessage = 'Login was unsuccessful. Please check your username and password and try again.';
                }
                $scope.showProcessing = false;

            }).finally(function() {
                if ($scope.responseLogin === true) {
                   
                    $timeout(function () { $scope.redirectToUserHome(); }, 1000);
                }
            });


        };

        $scope.redirectToUserHome = function () {
           
            var currentURL = $location.absUrl();
            var homepage = currentURL.replace('Login', 'Home');
            window.location.href = homepage;
        };

        $scope.showPassword = function () {
            if ($scope.togglePassword === true) {
                $scope.togglePassword = false;
                $scope.passwordMessage = 'Hide Password';
                $scope.passwordType = 'text';
               
            }
            else {
                $scope.togglePassword = true;
                $scope.passwordMessage = 'Show Password';
                $scope.passwordType = 'password';
               
            }
        };
    });