define(['app'], function (app) {
	app.factory('ConfigService',  ['$http', '$q', '$rootScope', '$resource', 'url',
		function( $http, $q, $rootScope, $resource, url) {

			var service = {};

			service.save = save;
			service.getCompras = getCompras();

        	return service;
	        
	        function getCompras() {
	        	var callback = $q.defer();
        		$http({
					  method: 'GET',
					  url: url.environment+'/api/compras.json'
					  //URL Amazon
					  //url: 'https://s3-us-west-2.amazonaws.com/bowtie-bucket/api/compras.json'
					}).then(function successCallback(response) 
					{	
						alert('Compra Inicial obtenida');
			      		callback.resolve(response.data);
					});
				return callback.promise;
	        }

	        function save(compra) {
	        	return $http.post(url.environment+'api/Compra/post',
			        compra)
			        .then(function (response) {
			        	alert('Compra guardada exitosamente');
			            return response;
			        });
	        }
		}
	]);

});