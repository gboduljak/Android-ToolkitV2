
'use strict';

var serviceId = 'authService';

app.factory(serviceId, ['$http', '$location', '$rootScope', '$q', 'serviceBase', 'localStorageService', 'authData', authService]);

function authService($http, $location, $rootScope, $q, serviceBase, localStorageService, authData) {

    var service = {
        register: register,
        login: login,
        logout: logout,
        changePassword:changePassword,
        getUserData: getUserData,
        fillAuthData: fillAuthData,
        getAuthData: getAuthData
    };

    return service;

    //#region Internal Methods    

    function register(user) {
        var deffered = $q.defer();
        var data = createRegisterData(user.name, user.surname, user.userName, user.password, user.confirmPassword, user.email, user.profilePhoto);
        $http.post(serviceBase + 'api/account/register', data).success(function (response) {
            deffered.resolve(response);
        }).error(function (response) {
            deffered.reject(response);
        });
        return deffered.promise;
    }

    function login(user) {

        var deffered = $q.defer();

        $http.post(serviceBase + 'api/account/login', createLoginData(user.userName, user.password), { headers: { 'Content-Type': 'application/json' } }).success(function (data) {
            deffered.resolve(data);
            setAuthData(user.userName, user.password, data.Token);
            $rootScope.authData = authData;
            $location.path('/index');

        }).error(function (data) {
            deffered.reject(data);
            authData.loggedIn = false;
            $rootScope.authData = authData;
        });

        return deffered.promise;
    }

    function logout() {
        localStorageService.remove('username');
        localStorageService.remove('password');
        localStorageService.remove('token');
        localStorageService.remove('loggedIn');
        localStorageService.set('loggedIn', false);
        authData.loggedIn = false;
        $location.path('/index');
        toastr.success('You have been logged out successfully.');
    }

    function getUserData(username) {

        var deffered = $q.defer();

        $http.get(serviceBase + 'api/account/user/' + username).success(function (response) {
            deffered.resolve(response);
        }).error(function (reason) {
            deffered.reject(reason);
        });

        return deffered.promise;

    }

    function changePassword(newPassword,confirmPassword) {
        var deffered = $q.defer();

        $http.post(serviceBase + 'api/account/changepassword', createChangePasswordData(newPassword,confirmPassword)).success(
            function (response) {
                deffered.resolve(response);
                logout();
            }).error(function (reason) {
                deffered.reject(reason);
            });

        return deffered.promise;
    }

    function createLoginData(username, password) {
        var data = {
            Username: username,
            Password: password
        };
        return data;
    }

    function createChangePasswordData(newPassword,confirmPassword) {
        var data = {
            OldPassword: authData.password,
            NewPassword: newPassword,
            ConfirmPassword:confirmPassword
        };
        return data;
    }

    function createRegisterData(name, surname, username, password, confirmPassword, email, profilePhoto) {
        var data = {
            Name: name,
            Surname: surname,
            Email: email,
            Username: username,
            Password: password,
            ConfirmPassword: confirmPassword,
            ProfilePhoto: profilePhoto
        };
        //data.ProfilePhoto = $('profilePhotoCanvas').toDataURL();
        data.ProfilePhoto = profilePhoto;
        return data;
    }

    function setAuthData(username, password, token) {
        localStorageService.clearAll();
        localStorageService.set('username', username);
        localStorageService.set('password', password);
        localStorageService.set('token', token);
        localStorageService.set('loggedIn', true);

        authData.userName = username;
        authData.password = password;
        authData.token = token;
        authData.loggedIn = true;
    }

    function fillAuthData() {
        authData.userName = localStorageService.get('username');
        authData.password = localStorageService.get('password');
        authData.token = localStorageService.get('token');
        authData.loggedIn = localStorageService.get('loggedIn');
    }

    function getAuthData() {
        fillAuthData();
        return authData;
    }

    //#endregion
};