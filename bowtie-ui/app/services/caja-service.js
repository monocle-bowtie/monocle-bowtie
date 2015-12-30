define(['app'], function (app) {
	app.factory('CajaService',  ['$http', '$q', '$rootScope', '$resource', 'url',
		function( $http, $q, $rootScope, $resource, url) {

			var service = {};
			service.getCaja = getCaja;
			service.getmMovimientosToday = getmMovimientosToday;
			service.getmMovimientosByRange = getmMovimientosByRange;
			service.getConceptos = getConceptos;
			service.saveMovimiento = saveMovimiento;

        	return service;

        	function saveMovimiento(movimiento) {
	        	return $http.post(url.environment+'api/caja/post',
			        movimiento)
			        .then(function (response) {
			        	alert('El movimiento se guardó exitosamente');
			            return response;
			        });
	        }

        	function getConceptos() {
	        	var callback = $q.defer();
        		$http({
					  method: 'GET',
					  url: url.environment+'api/concepto/get'
					}).then(function successCallback(response) 
					{						
			      		callback.resolve(response.data);
					});
				return callback.promise;
	        }

        	function getCaja() {
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

	        function getmMovimientosToday() {
	        	var date = new Date();
	            var day = date.getDate();
	            var month = date.getMonth()+1;
	            var year = date.getFullYear();
	            var todayFormat = year+'-'+month+'-'+day;

	        	var callback = $q.defer();
        		$http({
					  method: 'GET',
					  url: url.environment+'api/caja/GetDate?p_desde='+todayFormat+'&p_hasta='+todayFormat

					}).then(function successCallback(response) 
					{						
			      		callback.resolve(response.data);
					});
				return callback.promise;
	        }

	        function getmMovimientosByRange(from, to) {
	        	if (from != "" && to != "") {//Valido para que no haga un request en el onLoad (async)
	        		var callback = $q.defer();
        			$http({
					  method: 'GET',
					  url: url.environment+'api/caja/GetDate?p_desde='+from+'&p_hasta='+to
					}).then(function successCallback(response) 
					{						
			      		callback.resolve(response.data);
					});
				return callback.promise;
	        	};


	        }
        	
		}
	]);

});