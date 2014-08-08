
'use strict';

var controllerId = 'manageUserCtrl';


app.controller(controllerId,
    ['$scope', '$rootScope', 'authService', 'authData', manageUserCtrl]);

function manageUserCtrl($scope,$rootScope, authService, authData) {
    $rootScope.title = 'Manage - '+authData.userName ;
    $scope.access_token = authData.token;
    $scope.pageClass = 'page-manage';

    $scope.isBusy = true;

    $scope.authData = authData;

    $scope.message = '';

    $scope.user = {
        oldPassword: '',
        newPassword: '',
       confirmPassword: ''
    };

    $scope.$on('$viewContentLoaded', function () {
        activate();
    });

    $scope.changePassword = function () {
        var promise = authService.changePassword($scope.user.newPassword, $scope.user.confirmPassword);
        promise.then(function () {
            toastr.info("You have changed your password successfully. You are going to be logged out for security reasons. Please log in again with the new password! ");
        }, function () {
            $scope.message = 'Change password failed';
        });
    }


    function activate() {
        var promise = authService.getUserData(authData.userName);
        promise.then(function (data) {
            toastr.success("Userdata loaded!");
            $scope.user = data;
        }, function (data) {
            toastr.error("Cannot get user data. Please try again later :(");
            toastr.error(data.Message);
        }).finally(function () {
            $scope.isBusy = false;
        });
    }
}
