define(['app', 'LoginService'], function (app, LoginService) {
    app.controller('LoginCtrl', function ($scope, $rootScope, $http, Cookies, LoginService, $location) {
        
        $scope.login = function() {
            //Metodo para validar el response del backend
            LoginService.login(function (response) {
                if (response.success) {
                    $location.path('/home');
                } 
            });
        }
        
    });        
});