define(['app', 'MedioPagoService'], function (app, MedioPagoService) {
    app.controller('MedioPagoCtrl', function ($scope, $http, $rootScope, $filter, $timeout, MedioPagoService) {
    	
    	$scope.tarjetasList = [];
        
        var getTarjetas = MedioPagoService.getTarjetas();

        $scope.init = function() {
            getTarjetas.then(function(tarjetasList) {
                $scope.tarjetasList = tarjetasList;
            });
        }
         
        $scope.saveTarjeta = function(t) {
            var tarjeta = {};
            tarjeta.idTarjeta = 0;
            tarjeta.descripcion = t.descripcion;

            $scope.tarjetasList.push(tarjeta);
            
            MedioPagoService.saveTarjeta(angular.toJson($scope.tarjeta));
        }
    });
});