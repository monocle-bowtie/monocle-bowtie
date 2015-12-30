define(['app'], function (app) {
	app.factory('HomeService',  ['$http', '$q', '$rootScope', '$resource', 'url',
		function( $http, $q, $rootScope, $resource, url) {

			var service = {};
			service.getUltimosMovimientos = getUltimosMovimientos;

        	return service;

        	function getUltimosMovimientos() {
	        	var callback = $q.defer();
        		$http({
					  method: 'GET',
					  url: url.environment+'api/caja/get'
					}).then(function successCallback(response) 
					{						
			      		callback.resolve(response.data);
					});
				return callback.promise;
	        }
		}
	]);
});