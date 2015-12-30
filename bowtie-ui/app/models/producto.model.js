define(['app'], function (app) {
	app.factory('ProductoModel',  function() 
	{
		function Producto() {};

		Producto.prototype = 
		{
			NombreProducto 		: "",
	        costo  		        : "",
	        cantidad 	        : "",
	        Codigo				:"",

	        
	        clearProducto		: function() 
	        {
	        	this.NombreProducto = "";
		        this.costo  = "";
		        this.cantidad = "";
		        this.Codigo = "";

	        }
		};

		return Producto;
	});	
});
