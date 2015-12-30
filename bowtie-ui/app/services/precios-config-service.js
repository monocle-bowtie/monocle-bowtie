define(['app'], function (app) {
	app.factory('PreciosConfigService',  ['$http', '$q', '$rootScope', '$resource', 'url',
		function( $http, $q, $rootScope, $resource, url) {

			var service = {};

			service.getCategorias = getCategorias;
			service.saveGrupo = saveGrupo;

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

	        function saveGrupo(grupo) {
	        	return $http.post(url.environment+'api/grupo/post',
			        grupo)
			        .then(function (response) {
			            return response;
			        });
				
	        }
        	
		}
	]);

});