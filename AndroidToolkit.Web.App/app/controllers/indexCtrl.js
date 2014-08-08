
'use strict';

var controllerId = 'indexCtrl';

app.controller(controllerId,
    ['$scope', '$rootScope', 'authService', 'authData', indexCtrl]);

function indexCtrl($scope, $rootScope, authService, authData) {
    $rootScope.title = 'Home';
    $scope.pageClass = 'page-home';
   

}
