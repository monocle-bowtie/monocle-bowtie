define(['app', 'ClientesService'], function (app, ClientesService, ClienteModel) {
    app.controller('ClientesCtrl', function ($scope, $http, $rootScope, $filter, $timeout, ClientesService) {
    	
    	$scope.clientesList = [];

        $scope.cliente = {};
        $scope.cliente.idCliente = 0;



        var getClientes = ClientesService.getClientes();

        $scope.init = function() {
            $timeout(getClientes.then(function(clientesList) {
                $scope.clientesList = clientesList;
            }), 1000);    
        }

        $scope.saveCliente = function(c) {
            var cliente = {};
            cliente.nombre = c.Nombre;
            cliente.direccion = c.Direccion;
            cliente.telefono = c.Telefono;
            cliente.email = c.Email;
            //Esto es provisorio hasta que se configuren las ciudades
            $scope.cliente.idCiudad = 1;

            $scope.clientesList.push(cliente);
            //console.log(angular.toJson($scope.cliente));
            ClientesService.saveCliente(angular.toJson($scope.cliente));
        }
    });
});