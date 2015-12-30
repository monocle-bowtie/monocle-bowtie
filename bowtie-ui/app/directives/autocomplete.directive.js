define(['app'], function (app) {
	app.directive('autocomplete',  function($parse) {		
		return {
    		restrict: 'A',
    		scope: 
    		{ 
	           	 data: "=",
	           	 ngModel: '='
	      	},
	      	link: function(scope, element, attr) 
	      	{
	      		$(element).autocomplete({
			      	source: scope.data,
				  	select: function( event, ui ) 
				  	{
				  		scope.$apply(function() {
				        	scope.ngModel = ui.item.value;
				   		});
     				}
     			});
	      	}
		};
	});	
});