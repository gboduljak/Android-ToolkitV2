(function () {
    'use strict';

    var id = 'app';

    var app = angular.module('app', [
        'ngAnimate',
        'ngRoute',
        'ngSanitize',
        'angular-loading-bar',
        'LocalStorageModule'
    ]);

    app.config(function ($routeProvider) {

        $routeProvider.when("/home", {
            controller: "homeController",
            templateUrl: "/app/views/home.html"
        });

        $routeProvider.when("/login", {
            controller: "loginController",
            templateUrl: "/app/views/login.html"
        });

        $routeProvider.when("/signup", {
            controller: "signupController",
            templateUrl: "/app/views/signup.html"
        });

        $routeProvider.when("/orders", {
            controller: "ordersController",
            templateUrl: "/app/views/orders.html"
        });

        $routeProvider.otherwise({ redirectTo: "/home" });

    });

    app.config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptorService');
    });

    app.run(['authService', function (authService) {
       
    }]);

    app.run(['$q', '$rootScope','authService',
        function ($q, $rootScope, authService) {
            authService.fillAuthData();
        }]);
})();