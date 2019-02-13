angular.module('LoginApp', [])
    .controller('LoginController', function ($scope, $http) {
        $(".preloader").fadeOut();

        $("#recover").click(function () {
            $("#loginform").slideUp();
            $("#loginform").fadeOut();
            $("#recoverform").fadeIn();
            $("#recoverform").slideDown();
        });

        $("#to-login").click(function () {
            $("#recoverform").slideUp();
            $("#recoverform").fadeOut();
            $("#loginform").fadeIn();
            $("#loginform").slideDown();
            
        });

        $scope.recoverPassword = function () {
            var s = 'xxx';
        };
    });