define(['app'], function (app) {
	app.factory('LoginService',  ['$resource', '$http', '$rootScope', 'url',
		function( $resource, $http, $rootScope, url){
			
			return {
				login: function(callback) {
					//Desde aca se va a ir al backend
					var response = {success: true};
					callback(response);
				},
			}
		}
	]);

});