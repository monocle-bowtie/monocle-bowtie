define(['app', 'StockService'], function (app, StockService) {
    app.controller('StockCtrl', function ($scope, $http, $rootScope, $filter, $timeout, ngDialog, StockService, frontEndHost) {
    	
    	$scope.stockList = [];
        $scope.proveedoresList = [];
        $scope.cantidadFilas = 20;

        $scope.producto = {};
        $scope.producto.codigoBarras = "";
        $scope.producto.idProveedor = 0;
        $scope.producto.nombre = "";
        $scope.producto.cantidad = "";
        $scope.producto.precioContado = "";
        $scope.producto.precioGremio = "";
        $scope.producto.precioLista = "";

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
                        scope: $scope,
                        codigoBarras: "",
                        idProveedor:0,
                        nombre: "",
                        cantidad: "",
                        contado: "",
                        gremio: "",
                        lista: ""
                    });
        };

        $scope.editar = function(producto) {
            var prod = {};

            prod.idProducto = producto.idProducto;
            prod.nombre = producto.desc;
            prod.precioContado = producto.precioContado;
            prod.precioGremio = producto.precioGremio;
            prod.precioLista = producto.precioLista;
            prod.codigoBarras = producto.codigoBarras;

            var sp = StockService.saveProducto(angular.toJson(prod));
            sp.then(function(p){
                $scope.producto = p;
                $scope.producto.cantidad = producto.cant;
                $scope.producto.idStock = producto.idStock;
                saveStockProductoEdit($scope.producto);
            });
        }

        function saveStockProductoEdit(producto) {
            var prod = {};
            prod.idStock = producto.idStock;
            prod.idProducto = producto.idProducto;
            prod.cantidad = producto.cantidad;

            StockService.saveStock(angular.toJson(prod));
        }

         $scope.saveProductoNuevo  = function(producto) {
            if (producto === undefined) {
                alert('Deben estar todos los campos completos');
                return;
            };
            
            if (producto.codigoBarras != "" &&
                producto.idProveedor != 0 &&
                producto.nombre != "" &&
                producto.cantidad != "" &&
                producto.precioContado != "" &&
                producto.precioGremio != "" &&
                producto.precioLista != "") {
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
            } else {
                alert('Deben estar todos los campos completos');
            }
        }

        function saveStockProductoNuevo(producto) {
            var prod = {};
            prod.idStock = 0;
            prod.idProducto = producto.idProducto;
            prod.cantidad = producto.cantidad;
            
            var sStock = StockService.saveStock(angular.toJson(prod));
            sStock.then(function(p) {
                $scope.producto.idStock = p.idStock;
                console.log(p);
            });
            $scope.producto.cant = producto.cantidad;
            $scope.stockList.push($scope.producto);
        }
    });
});