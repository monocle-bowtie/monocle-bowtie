define(['app'], function (app) {
	app.factory('ComprasService',  ['$http', '$q', '$rootScope', '$resource', 'url',
		function( $http, $q, $rootScope, $resource, url) {
			var service = {};

			service.getProveedores = getProveedores;
			service.saveCompra = saveCompra;
			service.getProductos = getProductos;
			service.getMedioPago = getMedioPago;
			service.getCategorias = getCategorias;

        	return service;

        	function getCategorias() {
	        	var callback = $q.defer();
        		$http({
					  method: 'GET',
					  url: url.environment+'api/grupo/get'
					}).then(function successCallback(response) 
					{						
			      		callback.resolve(response.data);
					});
				return callback.promise;
	        }

	        function saveCompra(compra) {
	        	return $http.post(url.environment+'api/Compra/post',
			        compra)
			        .then(function (response) {
			        	alert('La compra se guardó exitosamente');
			            return response;
			        });
	        }

	        function getProveedores() {
	        	var callback = $q.defer();
        		$http({
					  method: 'GET',
					  url: url.environment+'api/proveedor/get'
					}).then(function successCallback(response) 
					{						
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

	        function getProductos() {
	        	var callback = $q.defer();
        		$http({
					  method: 'GET',
					  url: url.environment+'api/productos/get'
					}).then(function successCallback(response) 
					{						
			      		callback.resolve(response.data);
					});
				return callback.promise;
	        }
		}
	]);

});