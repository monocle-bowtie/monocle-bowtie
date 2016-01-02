require.config({
    
    baseUrl: "bower_components",
    
    paths: {
        'app':'../app',
        'angular': 'angular/angular',
        'angular-route': 'angular-route/angular-route',
        'angularAMD': 'angularAMD/angularAMD',
        'angular-cookies': 'angular-cookies/cookies.min',
        'angular-resource': 'angular-resource/angular-resource.min',
        'angular-mocks':'angular-mocks/angular-mocks',
        'ngDialog':'ng-dialog/js/ngDialog',
        'jquery': 'jquery/dist/jquery.min',
        'jquery-fittext': 'jquery-fittext/jquery.fittext',
        'jquery-easing': 'jquery-easing/js/jquery.easing.min',
        'jquery-ui':'jquery-ui/jquery-ui.min',
        'classie': 'classie/classie',
        'bootstrap':'bootstrap/dist/js/bootstrap.min',
        'wow': 'wow/wow.min',
        'metisMenu': 'metisMenu/dist/metisMenu.min',
        'datepicker': 'datepicker/bootstrap-datepicker',
        'moment': 'moment/moment',
        'date':'date/date',
        'bootstrap-datepicker':'bootstrap-datepicker/bootstrap-datepicker',
        //Services
        'LoginService': '../app/services/login-service',
        'VentasService': '../app/services/ventas-service',
        'ComprasService': '../app/services/compras-service',
        'ProveedoresService': '../app/services/proveedores-service',
        'ClientesService': '../app/services/clientes-service',
        'StockService': '../app/services/stock-service',
        'PreciosConfigService': '../app/services/precios-config-service',
        'CajaService': '../app/services/caja-service',
        'HomeService': '../app/services/home-service',
        'PromocionesService': '../app/services/promociones-service',
        'MedioPagoService': '../app/services/medioPago-service',
        'ConfigService': '../app/services/config-service',
        // directives
        'AutocompleteDirective': '../app/directives/autocomplete.directive',
        // Models
        'ClienteModel': '../app/models/cliente.model',
        'ProductoModel': '../app/models/producto.model'
    },

    shim: { 

            angular: {
                exports: "angular"
            },
            
            'angularAMD': ['angular'], 
            'angular-route': ['angular'],
            'angular-cookies': ['angular'],
            'angular-resource': ['angular'],
            'angular-mocks':['angular'],
            'ngDialog':['angular'],
            'jquery-fittext': ['jquery'],
            'bootstrap': ['jquery'],
            'datepicker': ['jquery'],
            'bootstrap-datepicker': ['jquery']
         },
    deps: ['app']
});


