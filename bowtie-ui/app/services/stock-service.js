define(['app'], function (app) {
	app.factory('StockService',  ['$http', '$q', '$rootScope', '$resource', 'url',
		function( $http, $q, $rootScope, $resource, url) {

			var service = {};

			service.getProductos = getProductos;
			service.modificarStock = modificarStock;
			service.modificarProducto = modificarProducto;

        	return service;

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

	        function modificarProducto(producto) {
	        	return $http.post(url.environment+'api/productos/post',
			        producto)
			        .then(function (response) {
			        	alert('El producto se actualizó correctamente');
			            return response;
			        });
	        }

	        function modificarStock(producto) {
	        	return $http.post(url.environment+'api/stock/post',
			        producto)
			        .then(function (response) {
			        	alert('El stock se actualizó correctamente');
			            return response;
			        });
				
	        }

	       
		}
	]);

});