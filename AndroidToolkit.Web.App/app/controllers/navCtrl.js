
'use strict';

var controllerId = 'navCtrl';
app.controller(controllerId,
    ['$scope', '$location', navCtrl]);

function navCtrl($scope, $location) {

    $scope.isActive = function (viewLocation) {
        return viewLocation === $location.path();
    };
}
