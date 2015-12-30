define(['app'], function (app) {
	app.factory('PromocionesService',  ['$http', '$q', '$rootScope', '$resource', 'url',
		function( $http, $q, $rootScope, $resource, url) {

			var service = {};
			
			service.getListadoPromociones = getListadoPromociones;
			service.savePromo = savePromo;
			service.getProductos = getProductos;

        	return service;

        	function getProductos() {
	        	var callback = $q.defer();
        		$http({
					  method: 'GET',
					  url: url.environment+'api/productos/get'
					}).then(function successCallback(response) {
			      		callback.resolve(response.data);
					});
				return callback.promise;
	        }

	        function getListadoPromociones() {
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

	        function savePromo(promocion) {
	        	return $http.post(url.environment+'api/promocion/post',
			        promocion)
			        .then(function (response) {
			            return response;
			        });
	        }
        	
		}
	]);

});