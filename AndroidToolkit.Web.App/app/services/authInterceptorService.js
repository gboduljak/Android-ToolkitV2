'use strict';
app.factory('authInterceptorService', ['$q', '$location', 'localStorageService', 'authData', function ($q, $location, localStorageService, authData) {

    var authInterceptorServiceFactory = {};

    var request = function (config) {
        config.headers = config.headers || {};
        config.headers.Authorization = 'Bearer ' + authData.token;
        return config;
    }

    var responseError = function (rejection) {
        if (rejection.status === 401) {
            $location.path('/login');
        }
        return $q.reject(rejection);
    }

    authInterceptorServiceFactory.request = request;
    authInterceptorServiceFactory.responseError = responseError;

    return authInterceptorServiceFactory;
}]);