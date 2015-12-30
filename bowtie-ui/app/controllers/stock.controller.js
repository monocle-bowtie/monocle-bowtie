define(['app', 'StockService'], function (app, StockService) {
    app.controller('StockCtrl', function ($scope, $http, $rootScope, $filter, $timeout, StockService) {
    	
    	$scope.stockList = [];
        $scope.cantidadFilas = 20;


        $scope.producto = {};
        
        $scope.producto.idProducto = 0;
        $scope.producto.idStock = 0;
        $scope.producto.Cantidad = 0;

        var getProductos = StockService.getProductos();

        $scope.init = function() {
            getProductos.then(function(stockList) {
                $scope.stockList = stockList;
            });
        }

        $scope.modificar = function(producto) {
            if(producto.idProducto > -1) {
                console.log('modificarProducto > -1');
                modificarProducto(producto);   
            }
            if (producto.cant) {

                modificarStock(producto);
            } 
        }

        function modificarStock(producto) {
                $scope.producto.idProducto = producto.idProducto;
                $scope.producto.idStock = producto.idStock;
                $scope.producto.Cantidad = producto.cant;
                
                StockService.modificarStock(angular.toJson($scope.producto));
        }

        function modificarProducto(producto) {
                var prod = {};
                prod.idProducto = producto.idProducto;
                prod.CodigoBarras = producto.codigoBarras;
                prod.Nombre = producto.nombre;
                prod.PrecioLista = producto.precioLista;
                prod.PrecioContado = producto.precioContado;
                prod.PrecioGremio = producto.precioGremio;
                
                StockService.modificarProducto(angular.toJson(prod));
            
        }

    });
});