
'use strict';

var controllerId = 'registerCtrl';

app.controller(controllerId,
    ['$scope', '$rootScope', '$location', 'authService', registerCtrl]);

function registerCtrl($scope,$rootScope, $location, authService) {
    $rootScope.title = 'Register';
    $scope.pageClass = 'page-login';

    $scope.user =
    {
        name: '',
        surname: '',
        email: '',
        userName: '',
        password: '',
        confirmPassword: '',
        profilePhoto: ''
    };

    $scope.message = '';

    $scope.isBusy = false;

    $scope.savedSuccessfully = false;

    $scope.register = function () {
        $scope.isBusy = true;

        authService.register($scope.user).then(function () {
            toastr.success("You have been registered successfully. Log in here! :)");
            $scope.savedSuccessfully = true;
            $location.path('/account/login');

        }, function (response) {
            toastr.error(response.Message);
            $scope.savedSuccessfully = false;
            var errors = [];
            for (var key in response.ModelState) {
                for (var i = 0; i < response.ModelState[key].length; i++) {
                    errors.push(response.ModelState[key][i]);
                }
            }
            $scope.message = "Registration failed:" + errors.join(' ');
        }).finally(function () {
            $scope.isBusy = false;
        });
    };

  
}

