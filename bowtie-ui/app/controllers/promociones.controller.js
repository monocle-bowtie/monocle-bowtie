define(['app', 'PromocionesService'], function (app, PromocionesService) {
    app.controller('PromocionesCtrl', function ($scope, $http, $rootScope, $filter, $timeout, PromocionesService) {
        
        $scope.promocion = {};
        $scope.promocion.descripcion = null;
        $scope.promocion.promoProducto = [];
        
        $scope.productosList = [];
        $scope.promocionesList = [];
        $scope.listadoDeProductos = [];

        $scope.cantidadFilas = 5;
        
        var getProductos = PromocionesService.getProductos();
        var getListadoPromociones = PromocionesService.getListadoPromociones();
    	
    	$scope.init = function() {
            getProductos.then(function(productosList) {
                $scope.productosList = productosList;
            });
            getListadoPromociones.then(function(promocionesList) {
                $scope.promocionesList = promocionesList;
            });
        }

        $scope.addProducto = function(prod) {
            var promoProducto = {}

            promoProducto.idPromocion = 0;
            promoProducto.idProducto = prod.idProducto;
            $scope.promocion.promoProducto.push(promoProducto);
            
            var productoToShow = {};
            productoToShow.nombre = prod.nombre;
            productoToShow.precioLista = prod.precioLista;

            $scope.listadoDeProductos.push(productoToShow);
            
        }

        $scope.guardarPromo = function(promo) {
                var p = {};
                p.idPromocion = 0;
                p.Descripcion = promo.descripcion;
                p.precio = parseInt(promo.precio);
                p.promoProducto = $scope.promocion.promoProducto;

                $scope.promocionesList.push(promo);
                PromocionesService.savePromo(p);
            
        }

        $scope.removeProducto = function(obj) {
            if(obj != -1) {
                $scope.promocion.promoProducto.splice(obj, 1);
                $scope.listadoDeProductos.splice(obj, 1);
            }
        }


        $('#ver-listado-promociones').click(function() {
            $( "#listado-promociones" ).slideToggle( "slow" );
            $( "#promociones-config" ).slideToggle( "slow" );
        });

    
        $('[data-toggle=tooltip]').hover(function(){
            // on mouseenter
            $(this).tooltip('show');
        }, function(){
            // on mouseleave
            $(this).tooltip('hide');
        });
    
    });
});