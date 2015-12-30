define(['app'], function (app) {
	app.factory('ProveedoresService',  ['$http', '$q', '$rootScope', '$resource', 'url',
		function( $http, $q, $rootScope, $resource, url) {

			var service = {};

			service.getProveedores = getProveedores;
			service.saveProveedores = saveProveedores;

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

	        function saveProveedores(proveedor) {
	        	return $http.post(url.environment+'api/proveedor/post',
			        proveedor)
			        .then(function (response) {
			            return response;
			        });
	        }
		}
	]);

});