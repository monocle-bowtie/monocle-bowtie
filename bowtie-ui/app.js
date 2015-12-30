define(['angularAMD', 
		'angular-route', 
		'angular-cookies',
		'angular-resource',
		'angular-mocks',
		'jquery',
		'jquery-fittext',
		'jquery-ui', 
		'bootstrap',
		'metisMenu',
		'datepicker',
		'date'], 
		function (angularAMD, anguarRoute, angularCookies, angularResource, angularMocks, $, jqueryFittext, jqueryUi, bootstrap) {  
		    var app = angular.module("webapp", ['ngRoute','angular-cookies', 'ngResource']);
		    app.constant("url", {
		    	'environment': 'http://localhost:53740/'
		    	//'environment': 'http://ec2-52-11-118-155.us-west-2.compute.amazonaws.com/'
		    });
		    app.config(function ($routeProvider, $provide) {

		    	//Configuracion para que levanten los servicios
		    	app.factory    = $provide.factory;

		        $routeProvider.when("/", angularAMD.route({
		            templateUrl: 'app/views/landing-page.html', controller: 'LandingCtrl',
		            controllerUrl: '../app/controllers/landing.controller'
		        }));

		        $routeProvider.when("/login", angularAMD.route({
		            templateUrl: 'app/views/login-view.html', controller: 'LoginCtrl',
		            controllerUrl: '../app/controllers/login.controller'
		        }));

		        $routeProvider.when("/home", angularAMD.route({
		            templateUrl: 'app/views/home-view.html', controller: 'HomeCtrl',
		            controllerUrl: '../app/controllers/home.controller'
		        }));

		        $routeProvider.when("/ventas", angularAMD.route({
		            templateUrl: 'app/views/ventas-view.html', controller: 'VentasCtrl',
		            controllerUrl: '../app/controllers/ventas.controller'
		        }));

		        $routeProvider.when("/compras", angularAMD.route({
		            templateUrl: 'app/views/compras-view.html', controller: 'ComprasCtrl',
		            controllerUrl: '../app/controllers/compras.controller'
		        }));

		        $routeProvider.when("/proveedores", angularAMD.route({
		            templateUrl: 'app/views/proveedores-view.html', controller: 'ProveedoresCtrl',
		            controllerUrl: '../app/controllers/proveedores.controller'
		        }));

		         $routeProvider.when("/clientes", angularAMD.route({
		            templateUrl: 'app/views/clientes-view.html', controller: 'ClientesCtrl',
		            controllerUrl: '../app/controllers/clientes.controller'
		        }));

		        $routeProvider.when("/stock", angularAMD.route({
		            templateUrl: 'app/views/stock-view.html', controller: 'StockCtrl',
		            controllerUrl: '../app/controllers/stock.controller'
		        }));

		        $routeProvider.when("/precios-config", angularAMD.route({
		            templateUrl: 'app/views/precios-config-view.html', controller: 'PreciosConfigCtrl',
		            controllerUrl: '../app/controllers/precios.config.controller'
		        }));

		        $routeProvider.when("/caja", angularAMD.route({
		            templateUrl: 'app/views/caja-view.html', controller: 'CajaCtrl',
		            controllerUrl: '../app/controllers/caja.controller'
		        }));

		        $routeProvider.when("/promociones-config", angularAMD.route({
		            templateUrl: 'app/views/promociones-view.html', controller: 'PromocionesCtrl',
		            controllerUrl: '../app/controllers/promociones.controller'
		        }));

		        $routeProvider.when("/medioPago", angularAMD.route({
		            templateUrl: 'app/views/medioPago-view.html', controller: 'MedioPagoCtrl',
		            controllerUrl: '../app/controllers/medioPago.controller'
		        }));

		        $routeProvider.when("/config/setUp", angularAMD.route({
		            templateUrl: 'app/views/config-view.html', controller: 'ConfigCtrl',
		            controllerUrl: '../app/controllers/config.controller'
		        }));

		    });
		      
		    return angularAMD.bootstrap(app);
		});