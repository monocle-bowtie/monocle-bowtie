define(['app', 'StockService', 'ui-bootstrap'], function (app, StockService) {
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

        $scope.dynamicPopover = {
            content: 'Hello, World!',
            templateUrl: 'app/views/popups/myPopoverTemplate.html',
            title: 'Title'
          };

        $scope.editar = function(producto) {
            
            var prod = {};

            prod.idProducto = producto.idProducto;
            
            if (producto.desc) {
                prod.nombre = producto.desc;
            } else if (producto.desc != undefined) {
                prod.nombre = producto.desc;
            } else {
                prod.nombre = producto.descripcion;    
            }
            
            prod.precioContado = producto.precioContado;
            prod.precioGremio = producto.precioGremio;
            prod.precioLista = producto.precioLista;
            prod.codigoBarras = producto.codigoBarras;

            var sp = StockService.saveProducto(angular.toJson(prod));
            sp.then(function(p){
                $scope.producto = p;
                $scope.producto.cantidad = producto.stock;
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
            var prod = {};

            prod.idProducto = 0;
            prod.codigoBarras = producto.codigoBarras;
            prod.idProveedor = producto.idProveedor;
            prod.nombre = producto.descripcion;
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
            
            var sStock = StockService.saveStock(angular.toJson(prod));
            sStock.then(function(p) {
                $scope.producto.idStock = p.idStock;
            });
            $scope.producto.cant = producto.cantidad;
            $scope.producto.descripcion = producto.nombre;
            $scope.producto.stock = producto.cantidad;
            
            $scope.stockList.push($scope.producto);
        }
    });
});