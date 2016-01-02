define(['app'], function (app) {
	app.factory('StockService',  ['$http', '$q', '$rootScope', '$resource', 'url',
		function( $http, $q, $rootScope, $resource, url) {

			var service = {};

			service.getProductos = getProductos;
			service.saveStock = saveStock;
			service.saveProducto = saveProducto;
			service.getProveedores = getProveedores;

        	return service;

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


        	function getProductos() {
	        	var callback = $q.defer();
        		$http({
					  method: 'GET',
					  url: url.environment+'api/productos/GetwithStock'
					}).then(function successCallback(response) 
					{						
			      		callback.resolve(response.data);
					});
				return callback.promise;
	        }

	        function saveProducto(producto) {
	        	var callback = $q.defer();
	        		$http.post(url.environment+'api/productos/post',
			        	producto).then(function (response) {
			            callback.resolve(response.data);
			            alert('El producto se agregó correctamente');
			        });
			        return callback.promise;
	        }

	        function saveStock(producto) {
	        	var callback = $q.defer();
	        	$http.post(url.environment+'api/stock/post',
			        producto).then(function (response) {
			            callback.resolve(response.data);
			        });
				return callback.promise;
	        }
		}
	]);

});