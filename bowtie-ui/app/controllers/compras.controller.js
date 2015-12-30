define(['app', 'ComprasService', 'AutocompleteDirective', 'ProductoModel'], function (app, ComprasService, AutocompleteDirective, ProductoModel) {
    app.controller('ComprasCtrl', function ($scope, $http, $rootScope, $filter, $timeout, ComprasService, ProductoModel) {

        $scope.productosList = [];
        $scope.proveedoresList = [];
        $scope.medioPagoList = [];
        $scope.gruposList = [];

        $scope.producto = new ProductoModel();
        $scope.productos = { autocomplete: [] };
        $scope.totalCompra = 0;

        $scope.compra = {};
        $scope.compra.idCompra = 0;
        $scope.compra.idProveedor = 0;
        $scope.compra.idGrupo = 0;
        $scope.compra.Fecha = "";
        $scope.compra.Total = 0;
        $scope.compra.Estado = "A";
        $scope.compra.idMedioPago = 0;
        $scope.compra.NroFactura = "";
        $scope.compra.CompraDetalle = [];


        var getProveedores = ComprasService.getProveedores();
        var getProductos = ComprasService.getProductos();
        var getMedioPago = ComprasService.getMedioPago();
        var getCategorias = ComprasService.getCategorias();

        $scope.init = function() {
            $timeout(getProveedores.then(function(proveedoresList) {
                $scope.proveedoresList = proveedoresList;
            }), 1000);

            $timeout(getProductos.then(function(productosList) {
                $scope.productosList = productosList;
                for (i = 0; i < productosList.length; i++){
                    $scope.productos.autocomplete.push(productosList[i].nombre);
                }
            }), 1000);

            $timeout(getMedioPago.then(function(medioPagoList) {
                for(var i in medioPagoList){
                    if (medioPagoList[i].idMedioPago === 4) {
                        medioPagoList.splice(medioPagoList.indexOf(medioPagoList[i]), 1);
                    }
                }  
                $scope.medioPagoList = medioPagoList;
            }), 1000);

            $timeout(getCategorias.then(function(gruposList) {
                $scope.gruposList = gruposList;
            }), 1000);
        }

        $scope.saveCompra = function() {
            for(var i in  $scope.compra.CompraDetalle) {
                 $scope.compra.Total += $scope.compra.CompraDetalle[i].PrecioTotal;
            }

            if ($scope.compra.CompraDetalle.length > 0 &&
                    checkCabeceraFields($scope.compra)) {
                console.log(angular.toJson($scope.compra));
                ComprasService.saveCompra(angular.toJson($scope.compra));    
            } else {
                alert('Debe ingresar al menos un producto y completar todos los campos de la cabecera');
            };
        }

        $scope.addProducto = function(prod) {
            if (prod.NombreProducto != "" &&
                    prod.cantidad != "" &&
                        prod.costo != "" &&
                            prod.Codigo != "") {
                
                $scope.compra.CompraDetalle.push(createProducto(prod));
                $scope.totalCompra += parseInt(prod.costo) * parseInt(prod.cantidad);
                $scope.producto.clearProducto();    
            } else {
                alert('Debe completar todos los campos en Detalle de Producto');
            };
            
        }

        function checkCabeceraFields(compra) {
            if(compra.idProveedor != 0 &&
                compra.idGrupo != 0 &&
                    compra.Fecha != "" &&
                        compra.idMedioPago != 0 &&
                            compra.NroFactura != "") {
                return true;

            } else {return false}
        }

        function existeProductList(producto, lista) {
            for(var i in lista) {
                if(lista[i].idProducto === producto.idProducto) {
                    return true;    
                }
            }
            return false
        }

        function createProducto(prod) {
            for (var i = 0; i < $scope.productosList.length; i++) {
                if($scope.productosList[i].nombre === prod.NombreProducto) {
                    var compraDetalle = {};
                    compraDetalle.idCompra = 0;
                    compraDetalle.idCompraDetalle = 0;
                    compraDetalle.idProducto = $scope.productosList[i].idProducto;
                    compraDetalle.NombreProducto = prod.NombreProducto;
                    compraDetalle.Cantidad = parseInt(prod.cantidad);
                    compraDetalle.PrecioUnitario = parseInt(prod.costo);
                    compraDetalle.PrecioTotal = parseInt(prod.costo) * parseInt(prod.cantidad);
                    compraDetalle.CodigoBarras = prod.Codigo;
                    
                    return compraDetalle;
                } 
            };
            return createProductoNuevo(prod);
        }

        function createProductoNuevo(prod) {
            var compraDetalle = {};
            compraDetalle.idCompra = 0;
            compraDetalle.idCompraDetalle = 0;
            compraDetalle.idProducto = 0;
            compraDetalle.NombreProducto = prod.NombreProducto;
            compraDetalle.Cantidad = parseInt(prod.cantidad);
            compraDetalle.PrecioUnitario = parseInt(prod.costo);
            compraDetalle.PrecioTotal = parseInt(prod.costo) * parseInt(prod.cantidad);
            compraDetalle.CodigoBarras = prod.Codigo;

            return compraDetalle;
        }

        $scope.remove = function(obj) {
            $scope.compra.CompraDetalle.splice($scope.compra.CompraDetalle.indexOf(obj), 1);
            $scope.totalCompra -= obj.PrecioUnitario;
        }
       
        $('#ver-detalle-compras').click(function() {
            $( "#detalle-compra" ).slideToggle( "slow" );
        });

        $('#fecha').datepicker({
            format: "mm/dd/yyyy"
        });
		    
    });
});