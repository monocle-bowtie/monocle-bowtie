define(['app', 'StockService'], function (app, StockService) {
    app.controller('StockCtrl', function ($scope, $http, $rootScope, $filter, $timeout, ngDialog, StockService, frontEndHost) {
    	
    	$scope.stockList = [];
        $scope.proveedoresList = [];
        $scope.cantidadFilas = 20;

        var getProductos = StockService.getProductos();
        var getProveedores = StockService.getProveedores();

        $scope.init = function() {
            getProductos.then(function(stockList) {
                $scope.stockList = stockList;
            });

            getProveedores.then(function(proveedoresList) {
                $scope.proveedoresList = proveedoresList;
            });
        }

        $scope.addPorudctoNuevo = function () {
            ngDialog.open(
                    { 
                        template: frontEndHost.env+'app/views/popups/popup.html', 
                        scope: $scope
                    });
        };

        $scope.editar = function(producto) {
            var prod = {};

            prod.idProducto = producto.idProducto;
            prod.nombre = producto.nombre;
            prod.precioContado = producto.precioContado;
            prod.precioGremio = producto.precioGremio;
            prod.precioLista = producto.precioLista;
            prod.codigoBarras = producto.codigoBarras;

            var sp = StockService.saveProducto(angular.toJson(prod));
            sp.then(function(p){
                $scope.producto = p;
                $scope.producto.cantidad = producto.cant;
                $scope.producto.idStock = producto.idStock;
                console.log('prod pantalla: '+ angular.toJson(producto));
                saveStockProductoEdit($scope.producto);
            });
            
        }

        function saveStockProductoEdit(producto) {
            console.log('producto: ' + angular.toJson(producto));
            var prod = {};
            prod.idStock = producto.idStock;
            prod.idProducto = producto.idProducto;
            prod.cantidad = producto.cantidad;
            console.log('save: ' + angular.toJson(prod));
            StockService.saveStock(angular.toJson(prod));
        }

         $scope.saveProductoNuevo  = function(producto) {
            var prod = {};

            prod.idProducto = 0;
            prod.codigoBarras = producto.codigoBarras;
            prod.idProveedor = producto.idProveedor;
            prod.nombre = producto.nombre;
            prod.cantidad = producto.cantidad;
            prod.precioContado = producto.precioContado;
            prod.precioGremio = producto.precioGremio;
            prod.precioLista = producto.precioLista;
            
            var sp = StockService.saveProducto(angular.toJson(prod));

            sp.then(function(p) {
                $scope.producto = p;
                $scope.producto.cantidad = producto.cantidad;
                saveStockProductoNuevo($scope.producto);
            });
            ngDialog.close();
        }

        function saveStockProductoNuevo(producto) {
            var prod = {};
            prod.idStock = 0;
            prod.idProducto = producto.idProducto;
            prod.cantidad = producto.cantidad;

            StockService.saveStock(angular.toJson(prod));
        }
    });
});