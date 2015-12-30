define(['app'], function (app) {
	app.factory('MedioPagoService',  ['$http', '$q', '$rootScope', '$resource', 'url',
		function( $http, $q, $rootScope, $resource, url) {

			var service = {};

			service.getTarjetas = getTarjetas;
			service.saveTarjeta = saveTarjeta;

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

	        function saveTarjeta(tarjeta) {
	        	return $http.post(url.environment+'api/tarjeta/post',
			        tarjeta)
			        .then(function (response) {
			        	alert('La tarjeta fue ingresada con éxito');
			            return response;
			        });
	        }
		}
	]);

});