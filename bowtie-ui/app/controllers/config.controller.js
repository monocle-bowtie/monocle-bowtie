define(['app', 'ConfigService'], function (app, ConfigService) {
    app.controller('ConfigCtrl', function ($scope, $http, $rootScope, $filter, $timeout, ConfigService) {

        $scope.comprasList = [];

        var getCompras = ConfigService.getCompras;

        $scope.init = function() {
            getCompras.then(function(comprasList) {
                $scope.comprasList = comprasList;
            });
        }
         
        $scope.save = function() {
            for(var i in $scope.comprasList){
                ConfigService.save($scope.comprasList[i]);
            }
        }

    });
});





