define(['app'], function (app) {
	app.factory('ClientesService',  ['$http', '$q', '$rootScope', '$resource', 'url', 
		function( $http, $q, $rootScope, $resource, url) {

			var service = {};

			service.getClientes = getClientes;
			service.saveCliente = saveCliente;

        	return service;

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

	         function saveCliente(cliente) {
	        	return $http.post(url.environment+'api/cliente/post',
			        cliente)
			        .then(function (response) {
			        	alert('El Cliente se creó exitosamente');
			            return response;
			        });
	        }
		}
	]);

});