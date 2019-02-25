angular.module('DataSolutionApp', [])
    .controller('RegisterUserController', function ($scope, $http) {
        $(".preloader").fadeOut(); 

        $scope.showProcessing = false;
        $scope.isEmailMatched = true;
        $scope.isPasswordMatched = true;
        $scope.usernameExists = false;
        $scope.responseData = '';
        $scope.usernameMessage = '';
        $scope.pwdDetails = true;
        $scope.togglePassowrdInfo = false;
        $scope.toggleUsernameInfo = false;
        $scope.passwordMessage = '';
        $scope.toggleButton = false;
        $scope.buttonText = 'Sign Up';
        $scope.responseOutcome = false;
        $scope.showOutcome = false;
        //Show Hide Password
        $scope.passwordHideMessage = 'Show Password';
        $scope.passwordType = 'password';
        $scope.togglePassword = true;
   
        //Show Hide Cnfirm PAssword
        $scope.passwordHideMessageConfirm = 'Show Password';
        $scope.passwordTypeConfirm = 'password';
        $scope.togglePasswordConfirm = true;
 
        

        $scope.saveRegistration = function() {

            $scope.showProcessing = true;
            $scope.toggleButton = true;
            $http(
                {
                    method: 'POST',
                    url: '/Users/User/SaveUser',
                    params: {
                        FirstName: $scope.fname, Surname: $scope.surname, CompanyName: $scope.companyname, EmailAddress: $scope.email, Username: $scope.username,
                        Password: $scope.password, ContactNo: $scope.contactnumber
                    }
                }
            ).then(function (data) {
                $scope.responseOutcome = data.data;
                if ($scope.responseOutcome === true) {
                    $scope.responseMessage = 'Your details have been saved. You will receive confirmation when your account is activated.';
                    $scope.fname = '';
                    $scope.surname = '';
                    $scope.companyname = '';
                    $scope.email = '';
                    $scope.username = '';
                    $scope.password = '';
                    $scope.contactnumber = '';

                }
                else {
                    $scope.responseMessage = 'An error has occurred. Please try again later.';
                    
                }
                $scope.showProcessing = false;
                $scope.toggleButton = false;
                $scope.showOutcome = true;

            });
        };


        $scope.comparePasswords = function () {

            if ($scope.password != null && $scope.repeatPassword != null) {
                if ($scope.password.trim() !== $scope.repeatPassword.trim()) {
                    $scope.isPasswordMatched = false;
                    $scope.passwordMessage = 'Passwords do not match';
                }
                else {
                    $scope.isPasswordMatched = true;
                }
            }
           
        };

        $scope.compareEmail = function () {
            if ($scope.email != null && $scope.newEmail != null) {
                if ($scope.email.trim() !== $scope.newEmail.trim()) {
                    $scope.isEmailMatched = false;
                    
                }
                else {
                    $scope.isEmailMatched = true;
                }
            }

           
        };

        $scope.checkUsername = function (username) {
           

            $http(
                {
                    method: 'POST',
                    url: '/Users/User/CheckIfUsernameExists',
                    params: { Username: $scope.username }
                }
            ).then(function (response) {
           
                $scope.usernameExists = response.data;
                if ($scope.usernameExists === false) {
                    $scope.verifyUsername($scope.username);
                }
                else {
                    $scope.usernameMessage = 'Username already exists';
                    $scope.pwdDetails = true;
                }
                
            });
        };

        $scope.verifyUsername = function (username) {
            if (username.length < 6) {
                $scope.usernameMessage = 'Username is too short';
            } else if (username.length > 12) {
                $scope.usernameMessage = 'Username is too long';
            } else if (username.search(/\d/) === -1) {
                $scope.usernameMessage = 'Username must contain a number';
            } else if (username.search(/[a-zA-Z]/) === -1) {
                $scope.usernameMessage = 'Username must contain a letter';
            } else if (username.search(/[^a-zA-Z0-9]/) > 0) {
                $scope.usernameMessage = 'Username contains invalid characters';
            }
            else {
                $scope.usernameMessage = '';
            }

            if ($scope.usernameMessage.trim() !== '') {
                $scope.usernameExists = true;
                $scope.pwdDetails = true;
            }
            else {
                $scope.usernameExists = false;
                $scope.pwdDetails = false;
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

        $scope.togglePasswordMessage = function () {
            $scope.pwdDetails = true;
        };
        $scope.saveUser = function () {

         
        };

        $scope.showUsernameInfo = function () {
            if ($scope.toggleUsernameInfo === true) {
                $scope.toggleUsernameInfo = false;
            }
            else {
                $scope.toggleUsernameInfo = true;
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

        $scope.$watch('usernameExists', function () {
            if ($scope.usernameExists === false && $scope.isPasswordMatched === true && $scope.isEmailMatched === true) {
                $scope.toggleButton = false;
               // $scope.buttonText = 'Processing';
            }
            else {
                $scope.toggleButton = true;
            }
        });

        $scope.$watch('isPasswordMatched', function () {
            if ($scope.usernameExists === false && $scope.isPasswordMatched === true && $scope.isEmailMatched === true) {
                $scope.toggleButton = false;
                //$scope.buttonText = 'Processing';
            }
            else {
                $scope.toggleButton = true;
            }
        });

        $scope.$watch('isEmailMatched', function () {
            if ($scope.usernameExists === false && $scope.isPasswordMatched === true && $scope.isEmailMatched === true) {
                $scope.toggleButton = false;
                //$scope.buttonText = 'Processing';
            }
            else {
                $scope.toggleButton = true;
            }
        });

        $scope.$watch('showProcessing', function () {
           
            $scope.buttonText = $scope.showProcessing === true ? 'Processing' : 'Sign Up';
           
        });

        $scope.showPassword = function () {
            if ($scope.togglePassword === true) {
                $scope.togglePassword = false;
                $scope.passwordHideMessage = 'Hide Password';
                $scope.passwordType = 'text';
                $scope.passwordClass = false;
            }
            else {
                $scope.togglePassword = true;
                $scope.passwordHideMessage = 'Show Password';
                $scope.passwordType = 'password';
                $scope.passwordClass = true;
            }
        };

        $scope.showPasswordConfirm = function () {
            if ($scope.togglePasswordConfirm === true) {
                $scope.togglePasswordConfirm = false;
                $scope.passwordHideMessageConfirm = 'Hide Password';
                $scope.passwordTypeConfirm = 'text';
               
            }
            else {
                $scope.togglePasswordConfirm = true;
                $scope.passwordHideMessageConfirm = 'Show Password';
                $scope.passwordTypeConfirm = 'password';
             
            }
        };

        
    });