define(['app', 'ProveedoresService'], function (app, ProveedoresService) {
    app.controller('ProveedoresCtrl', function ($scope, $http, $rootScope, $filter, $timeout, ProveedoresService) {
    	
    	$scope.proveedoresList = [];

        $scope.proveedor = {};
        $scope.proveedor.idProveedor = 0;
        
        var getProveedores = ProveedoresService.getProveedores();

        $scope.init = function() {
            $timeout(getProveedores.then(function(proveedoresList) {
                $scope.proveedoresList = proveedoresList;
            }), 1000);
        }
         
        $scope.saveProveedores = function(p) {
            //Agrego el proveedor a la lista
            var proveedor = {};
            proveedor.nombre = p.Nombre;
            proveedor.mail = p.Mail;
            proveedor.direccion = p.Direccion;
            proveedor.telefono = p.Telefono;
            $scope.proveedoresList.push(proveedor);

            //Guardo el proveedor
            //console.log(angular.toJson($scope.proveedor));
            ProveedoresService.saveProveedores(angular.toJson($scope.proveedor));
        }

    });
});





