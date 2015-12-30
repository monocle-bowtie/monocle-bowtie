define(['app', 'HomeService'], function (app, HomeService) {
    app.controller('HomeCtrl', function ($scope, $timeout, HomeService) {

    	$scope.ultimosMovimientos = [];
    	$scope.cantidadFilas = -5;

    	var getUltimosMovimientos = HomeService.getUltimosMovimientos();

    	$scope.init = function(){
    		$timeout(getUltimosMovimientos.then(function(ultimosMovimientos) {
                $scope.ultimosMovimientos = ultimosMovimientos;
            }), 1000);
    	}
    });
});