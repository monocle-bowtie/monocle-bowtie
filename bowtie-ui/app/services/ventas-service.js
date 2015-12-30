define(['app'], function (app) {
	app.factory('VentasService',  ['$resource', '$q', '$http', '$rootScope', 'url',
		function( $resource, $q, $http, $rootScope, url) {
			var service = {};

        	service.getProductos = getProductos;
        	service.getMedioPago = getMedioPago;
        	service.getStock = getStock;
        	service.getClientes = getClientes;
        	service.getPromociones = getPromociones;
        	service.getTarjetas = getTarjetas;

        	service.guardarMovimientosCaja = guardarMovimientosCaja;
        	service.saveVenta = saveVenta;

        	return service;

        	function getTarjetas() {
	        	var callback = $q.defer();
        		$http({
					  method: 'GET',
					  url: url.environment+'api/tarjeta/get'
					}).then(function successCallback(response) 
					{						
			      		callback.resolve(response.data);
					});
				return callback.promise;
	        }

        	function getClientes() {
	        	var callback = $q.defer();
        		$http({
					  method: 'GET',
					  url: url.environment+'api/cliente/get'
					}).then(function successCallback(response) 
					{						
			      		callback.resolve(response.data);
					});
				return callback.promise;
	        }

	        function getProductos() {
	        	var callback = $q.defer();
        		$http({
					  method: 'GET',
					  url: url.environment+'api/productos/GetwithStock'
					}).then(function successCallback(response) {
			      		callback.resolve(response.data);
					});
				return callback.promise;
	        }

	        function getMedioPago() {
	        	var callback = $q.defer();
        		$http({
					  method: 'GET',
					  url: url.environment+'api/MedioPago/get'
					}).then(function successCallback(response) 
					{						
			      		callback.resolve(response.data);
					});
				return callback.promise;
	        }

	        function getStock() {
	        	var callback = $q.defer();
        		$http({
					  method: 'GET',
					  url: url.environment+'api/stock/get'
					}).then(function successCallback(response) 
					{						
			      		callback.resolve(response.data);
					});
				return callback.promise;
	        }

	         function getPromociones() {
	        	var callback = $q.defer();
        		$http({
					  method: 'GET',
					  url: url.environment+'api/promocion/getpromodetalle'
					}).then(function successCallback(response) 
					{						
			      		callback.resolve(response.data);
					});
				return callback.promise;
	        }

	        function saveVenta(venta) {
	        	return $http.post(url.environment+'api/Venta/post',
			        venta)
			        .then(function (response) {
			        	alert('La venta se guardó exitosamente');
			            return response;
			        });
				
	        }
	        
	        function guardarMovimientosCaja(movimiento, callbackOk, callbackFail) {
            	return $resource(url.environment+'api/movimiento.json', {}, {
	            	saveMovimiento: {
		                    method: 'POST'
		                }
		            }
		        ).saveMovimiento({}, angular.toJson(movimiento))
				.$promise.then(callbackOk, callbackFail);
			}
		}
	]);

});