define(['app', 'PreciosConfigService'], function (app, PreciosConfigService) {
    app.controller('PreciosConfigCtrl', function ($scope, $http, $rootScope, $filter, $timeout, PreciosConfigService) {
    	
    	$scope.categoriasList = [];

    	$scope.grupo = {};
    	$scope.grupo.idGrupo = 0;
    	$scope.grupo.Estado = "A";

    	var getCategorias = PreciosConfigService.getCategorias();

    	$scope.init = function() {
    		$timeout(getCategorias.then(function(categoriasList) {
                $scope.categoriasList = categoriasList;
            }), 1000);
    	}
    	
    	$scope.addGrupo = function(g) {
          	var grupo = {};
          	grupo.descripcion = g.Descripcion;
            PreciosConfigService.saveGrupo(angular.toJson($scope.grupo));
            $scope.categoriasList.push(grupo);
        }

    });
});