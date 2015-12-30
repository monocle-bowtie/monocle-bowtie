define(['app'], function (app) {
	app.factory('ClienteModel',  function() 
	{
		function Cliente() {};

		Cliente.prototype = 
		{
			idCliente   : 0,
			Nombre 		: "",
	        Email  		: "",
	        Telefono 	: "",
	        clearCliente	: function() 
	        {
	        	this.idCliente = 0;
	        	this.Nombre = "";
		        this.Email  = "";
		        this.Telefono = "";
	        }
		};

		return Cliente;
	});	
});