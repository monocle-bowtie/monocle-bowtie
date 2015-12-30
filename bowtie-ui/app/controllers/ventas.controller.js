define(['app', 'VentasService'], function (app, VentasService) {
    app.controller('VentasCtrl', function ($scope, $http, $rootScope, $timeout, $filter, VentasService) {

    	//Listas
        $scope.stockList = [];
        $scope.productosList = [];
        $scope.productosConCantidadList = [];
        $scope.medioPagoList = [];
        $scope.clientesList = [];
        $scope.promocionesList = [];
        $scope.tarjetasList = [];
        /*************************************************************/
        //Helpers
        $scope.cantidadFilas = 10;
        $scope.totalVentaContado = 0;
        $scope.totalVentaLista = 0;
        $scope.totalVentaGremio = 0;

        $scope.descuentoPromocion = 0;
        /*************************************************************/
        //Cabecera de venta
        $scope.venta = {};
        $scope.venta.idVenta = 0;
        $scope.venta.idVendedor = 3;
        $scope.venta.idCliente = 64;
        $scope.venta.Fecha = Date.today();
        $scope.venta.Total = 0;
        $scope.venta.totalpromocion = 0;
        $scope.venta.NroTicket = "111";
        //Se inicializan en 1 para que los select tengan un valor por defecto
        $scope.venta.idMedioPago = 1;
        $scope.venta.idSucursal = 1;
        $scope.venta.idTarjeta = 0;
        /*************************************************************/
        //Detalle de la venta
        $scope.venta.VentaDetalle = [];
        /*************************************************************/
        //Detalle de las promociones
        $scope.venta.VentaPromoDetalle = [];

        var getStock = VentasService.getStock();
        var getMedioPago = VentasService.getMedioPago();
        var getClientes = VentasService.getClientes();
        var getPromociones = VentasService.getPromociones();
        var getTarjetas = VentasService.getTarjetas();

        var getProductos = VentasService.getProductos();

        $scope.init = function() {
            getProductos.then(function(productosList) {
                $scope.productosList = productosList;
            });

            getMedioPago.then(function(medioPagoList) {
                $scope.medioPagoList = medioPagoList;
                for(var i in $scope.medioPagoList) {
                    if ($scope.medioPagoList[i].idMedioPago === 4) {
                        medioPagoList.splice(medioPagoList.indexOf(medioPagoList[i]), 1);
                    }
                }
            });

            getClientes.then(function(clientesList) {
                $scope.clientesList = clientesList;
            });

            getPromociones.then(function(promocionesList) {
                $scope.promocionesList = promocionesList;
            });

            getTarjetas.then(function(tarjetasList) {
                $scope.tarjetasList = tarjetasList;
            });

            $('#tarjetasSelect').hide();
        }

        $scope.saveVenta = function(producto) {
            var p = {};
            p.idVenta = 0;
            p.idVendedor = 3;
            p.idCliente = $scope.venta.idCliente;
            p.Fecha = Date.today();
            if ($scope.venta.idMedioPago == 1 && $scope.venta.idCliente == 64) {
                $scope.venta.Total = $scope.totalVentaContado;
            }else if ($scope.venta.idMedioPago == 2 || $scope.venta.idMedioPago == 3 &&
                        $scope.idCliente == 64) {
                $scope.venta.Total = $scope.totalVentaLista;
            }else if ($scope.venta.idCliente != 64) {
                $scope.venta.Total = $scope.totalVentaGremio;
            };
            p.totalpromocion = 0;
            p.NroTicket = "1";
            p.idMedioPago = $scope.venta.idMedioPago;
            p.idTarjeta = $scope.venta.idTarjeta;
            p.idSucursal = 1;

            for(var i in $scope.venta.VentaDetalle) {
                if ($scope.venta.idMedioPago == 1 && $scope.venta.idCliente == 64) {
                    $scope.venta.VentaDetalle[i].PrecioUnitario = $scope.venta.VentaDetalle[i].precioFinalContado;
                    $scope.venta.VentaDetalle[i].PrecioFinal = $scope.venta.VentaDetalle[i].precioFinalContado * $scope.venta.VentaDetalle[i].cantidad;
                }
                if ($scope.venta.idMedioPago == 2 || $scope.venta.idMedioPago == 3 &&
                        $scope.idCliente == 64) {
                    $scope.venta.VentaDetalle[i].PrecioUnitario = $scope.venta.VentaDetalle[i].precioFinalLista;
                    $scope.venta.VentaDetalle[i].PrecioFinal = $scope.venta.VentaDetalle[i].precioFinalLista * $scope.venta.VentaDetalle[i].cantidad;
                }
                if ($scope.venta.idCliente != 64) {
                    $scope.venta.VentaDetalle[i].PrecioUnitario = $scope.venta.VentaDetalle[i].precioFinalGremio;
                    $scope.venta.VentaDetalle[i].PrecioFinal = $scope.venta.VentaDetalle[i].precioFinalGremio * $scope.venta.VentaDetalle[i].cantidad;
                }
                delete $scope.venta.VentaDetalle[i].precioFinalContado;
                delete $scope.venta.VentaDetalle[i].precioFinalLista;
                delete $scope.venta.VentaDetalle[i].precioFinalGremio;

                delete $scope.venta.VentaDetalle[i].precioContado;
                delete $scope.venta.VentaDetalle[i].precioLista;
                delete $scope.venta.VentaDetalle[i].precioGremio;
            }
            
            VentasService.saveVenta(angular.toJson($scope.venta)); 
            refreshVenta();
        }

        $scope.addProducto = function(producto) {
            $scope.codigoSearch = "";
            $scope.nombreSearch = "";

            var checkIfExist = existeEnVentaDetalle(producto);
            if (!checkIfExist) {
                var prod = createProducto(producto)
                $scope.venta.VentaDetalle.push(prod);

                $scope.totalVentaContado += prod.precioFinalContado;
                $scope.totalVentaLista += prod.precioFinalLista;
                $scope.totalVentaGremio += prod.precioFinalGremio;
            }
        }

        $scope.removeProducto = function(obj) {
            $scope.venta.VentaDetalle.splice($scope.venta.VentaDetalle.indexOf(obj), 1);
            $scope.totalVentaContado -= obj.precioContado;
            $scope.totalVentaLista -= obj.precioLista;
            $scope.totalVentaGremio -= obj.precioGremio;
        }

        function createProducto(producto) {
            var prod = {};
            prod.descripcion = producto.nombre;
            prod.idVenta = 0;
            prod.idVentaDetalle = 0;
            prod.idProducto = producto.idProducto;
            //El precio Unitario se carga en saveVenta()
            prod.precioContado = producto.precioContado;
            prod.precioLista = producto.precioLista;
            prod.precioGremio = producto.precioGremio;

            if(producto.cantidad === undefined) {
                producto.cantidad = 1;
            }
            prod.cantidad = producto.cantidad;
            
            prod.precioFinalContado = parseInt(producto.precioContado) * parseInt(producto.cantidad);
            prod.precioFinalLista = parseInt(producto.precioLista) * parseInt(producto.cantidad);
            prod.precioFinalGremio = parseInt(producto.precioGremio) * parseInt(producto.cantidad);

            return prod;
        }

        function existeEnVentaDetalle(producto) {
            var existe = false;
            for(var i in $scope.venta.VentaDetalle) {
                if($scope.venta.VentaDetalle[i].idProducto == producto.idProducto) {
                    $scope.venta.VentaDetalle[i].cantidad += producto.cantidad;

                    $scope.venta.VentaDetalle[i].precioContado += producto.precioContado;
                    $scope.venta.VentaDetalle[i].precioLista += producto.precioLista;
                    $scope.venta.VentaDetalle[i].precioGremio += producto.precioGremio;

                    $scope.totalVentaContado += producto.precioContado;
                    $scope.totalVentaLista += producto.precioLista;
                    $scope.totalVentaGremio += producto.precioGremio;

                    existe = true;
                } 
            }
            return existe;
        }

        $scope.updateTotal = function() {
            if ($scope.venta.idCliente == 64) {
                $('#totalLista').hide();
                $('#totalContado').hide();
                $('#totalGremio').show();

            } else {
                $('#totalLista').hide();
                $('#totalContado').show();
                $('#totalGremio').hide();                
            }
        }

        $scope.updateMedioPagoSelect = function() {
            if ($scope.venta.idCliente != 64) {
                removePreventa();       
                var medioPago = {};
                medioPago.idMedioPago = 4;
                medioPago.descripcion = "PreVenta";
                medioPago.RecargoAdicional = 0;
                $scope.medioPagoList.push(medioPago);
            } else {
                removePreventa();
                $scope.venta.idMedioPago = 1;
            }
        }

        $scope.updateTarjetaSelect = function() {
            if($scope.venta.idMedioPago == 2 || $scope.venta.idMedioPago == 3) {
                $('#tarjetasSelect').show();
            } else {
                $('#tarjetasSelect').hide();
            }
         }

         function refreshVenta() {
            $scope.totalVentaContado = 0;
            $scope.totalVentaLista = 0;
            $scope.totalVentaGremio = 0;
            //Cabecera de venta
            $scope.venta = {};
            $scope.venta.idVenta = 0;
            $scope.venta.idVendedor = 3;
            $scope.venta.idCliente = 64;
            $scope.venta.Fecha = Date.today();
            $scope.venta.Total = 0;
            $scope.venta.totalpromocion = 0;
            $scope.venta.NroTicket = "111";
            //Se inicializan en 1 para que los select tengan un valor por defecto
            $scope.venta.idMedioPago = 1;
            $scope.venta.idSucursal = 1;
            $scope.venta.idTarjeta = 0;
            /*************************************************************/
            //Detalle de la venta
            $scope.venta.VentaDetalle = [];
            /*************************************************************/
            //Detalle de las promociones
            $scope.venta.VentaPromoDetalle = [];
            
        }

        function removePreventa() {
            for(var i in $scope.medioPagoList) {
                if ($scope.medioPagoList[i].idMedioPago === 4) {
                    $scope.medioPagoList.splice($scope.medioPagoList.indexOf($scope.medioPagoList[i]), 1);
                }
            }
        }

    });
});