
var app = angular.module('androidtoolkit', ['ngRoute', 'ngSanitize', 'ngAnimate', 'ngTouch', 'LocalStorageModule', 'angular-loading-bar']);

app.config(function ($routeProvider) {

    $routeProvider.when("/index", {
        controller: "indexCtrl",
        templateUrl: "/app/views/home.html"
    });

    //Account Routes

    $routeProvider.when("/account/login", {
        controller: "loginCtrl",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/account/register", {
        controller: "registerCtrl",
        templateUrl: "/app/views/register.html"
    });


    $routeProvider.when("/account/manage/:userName?", {
        controller: "manageUserCtrl",
        templateUrl: "/app/views/manage-user.html"
    });

    //$routeProvider.when("/404", {
    //    controller: "homeController",
    //    templateUrl: "/app/views/home.html"
    //});

    $routeProvider.otherwise({ redirectTo: "/index" });

});

app.config(function ($httpProvider) {
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
    $httpProvider.interceptors.push('authInterceptorService');

});

app.run(['$rootScope', 'authService', '$location', function ($rootScope, authService, $location) {
    authService.fillAuthData();
    $rootScope.authData = authService.getAuthData();

    $rootScope.$on('$locationChangeStart', function (next, current) {
        if (next.templateUrl == "/app/views/manage-user.html" && !$rootScope.authData.loggedIn) {
            $location.path('/account/login');
            toastr.info("You are trying to reach secured content. Please log in!");
        }
    });

    $rootScope.logOut = function () {
        authService.logout();
    };
}]);

app.value('authData', {
    userName: '',
    password: '',
    token: '',
    loggedIn: false
});

app.constant('serviceBase', 'http://localhost:17171/');


