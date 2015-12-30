define(['app', 'CajaService'], function (app, CajaService) {
    app.controller('CajaCtrl', function ($scope, $http, $rootScope, $filter, $timeout, CajaService) {
    	
        $scope.totalMovimientos = 0;
        $scope.movimientosList = [];
        $scope.movimientosToday = [];
        $scope.movByRange = [];
        $scope.conceptoList = [];
        $scope.totalDay = 0;

        $scope.dateFrom = "";
        $scope.dateTo = "";
        $scope.cantFilas = 5;
        
        $scope.movimiento = {};
        $scope.movimiento.fechaAlta = "";
        $scope.movimiento.descripcion = "";
        $scope.movimiento.monto = 0;
        $scope.movimiento.tipoMovimiento = "";

    	var getCaja = CajaService.getCaja();
        var getmMovimientosToday = CajaService.getmMovimientosToday();
        var getmMovimientosByRange = CajaService.getmMovimientosByRange($scope.dateFrom, $scope.dateTo);
        var getConceptos = CajaService.getConceptos();
    	
    	$scope.init = function() {
            getCaja.then(function(movimientosList) {
                $scope.movimientosList = movimientosList;
            });
            getmMovimientosToday.then(function(movimientosToday) {
                $scope.movimientosToday = movimientosToday;
                for(var i in movimientosToday) {
                    $scope.totalDay += movimientosToday[i].monto;
                }
            });

            getConceptos.then(function(conceptoList) {
                $scope.conceptoList = conceptoList;
            });

            $('.input-daterange').datepicker({
                todayBtn: "linked"    
            });
            
            $("#movByRange").hide();
            
        }

        $scope.saveMovimiento = function(mov) {
                if (mov.descripcion != "" &&
                    mov.concepto != "" &&
                    mov.monto > 0) {
                var date = new Date();
                var day = date.getDate();
                var month = date.getMonth()+1;
                var year = date.getFullYear();
                var todayFormat = day+'-'+month+'-'+year;
                
                $scope.movimiento.fechaAlta = todayFormat;

                var movimiento = {};
                movimiento.idCaja = 0;
                movimiento.idConcepto = mov.concepto;
                movimiento.TipoMovimiento = mov.tipoMovimiento;
                movimiento.Descripcion = mov.descripcion;
                movimiento.Monto = mov.monto
                $scope.totalDay += parseInt(mov.monto);
                CajaService.saveMovimiento(angular.toJson(movimiento)); 
                
                getmMovimientosToday.then(function(movimientosToday) {
                    $scope.movimientosToday = movimientosToday;
                    for(var i in movimientosToday) {
                        $scope.totalDay += parseInt(movimientosToday[i].monto);
                    }
                });

                $scope.movimientosToday.push(mov);    
                }else{
                    alert('Debe completar todos los campos de la cabecera');
                } 

        }

        $scope.addMovimiento = function(mov) {
            
            
        }

        $scope.getmMovimientosByRange = function() {
            if($scope.dateFrom != "" && $scope.dateTo != "") {
                var getmMovimientosByRange = CajaService.getmMovimientosByRange($scope.dateFrom, $scope.dateTo);
                
                getmMovimientosByRange.then(function(movByRange) {
                    $scope.movByRange = movByRange;
                });
                $("#filterByRange").click(function(){
                    $("#movimientosToday").hide();
                });
                $("#filterByRange").click(function(){
                    $("#movByRange").show();
                });
            } else {
                alert('Los campos Desde y Hasta no pueden estar vacíos');
            }

        }
    });
});