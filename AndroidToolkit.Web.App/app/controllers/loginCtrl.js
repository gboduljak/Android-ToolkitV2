
'use strict';

var controllerId = 'loginCtrl';

app.controller(controllerId,
     ['$rootScope', '$scope', 'authService', loginCtrl]);

function loginCtrl($rootScope, $scope, authService) {
    $rootScope.title = 'Login';
    $scope.pageClass = 'page-login';

    $scope.user =
    {
        userName: '',
        password: ''
    };

    $scope.message = '';

    $scope.isBusy = false;

    $scope.login = function () {
        $scope.isBusy = true;
        authService.login($scope.user).then(function (response) {
            toastr.success("You have been logged in successfully.");
        }, function (error) {
            toastr.error(error.Message);
            $scope.message = 'Login failed';
        }).finally(function() {
            $scope.isBusy = false;
        });
    }



}
