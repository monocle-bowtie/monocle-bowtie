using SistAdmin.Models;
using SistAdmin.TransferObjects;
using SistAdmin.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SistAdmin.App_Filters;

namespace SistAdmin.Controllers
{
    public class ProductosController : BaseApiController
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(ProductosController));

        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                ProductoService service = (ProductoService)new ProductoService().setDatabase(db);
                List<Producto> productos = service.getAll();



                response = this.getSuccessResponse(productos);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        public HttpResponseMessage GetwithStock()
        {
            HttpResponseMessage response;
            try
            {
                ProductoService service = (ProductoService)new ProductoService().setDatabase(db);
                List<productosWithStock> productos = service.getProdConStock();



                response = this.getSuccessResponse(productos);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        //[ApiAuthenticationFilter(true)]
        public HttpResponseMessage GetProductos(long id)
        {
            HttpResponseMessage response;
            try
            {
                ProductoService service = (ProductoService)new ProductoService().setDatabase(db);
                Producto p = service.find(id);

               

                response = this.getSuccessResponse(p);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        //[ApiAuthenticationFilter(true)]
        public HttpResponseMessage Post([FromBody] Producto p)
        {
            HttpResponseMessage response;
            try
            {
                ProductoService service = (ProductoService)new ProductoService().setDatabase(db);
                p.FechaAlta = DateTime.Today;
                p.UsuarioAlta = 1;
                p.Estado = "A";
                p = service.saveOrUpdate(p);

                response = this.getSuccessResponse(p);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        //[ApiAuthenticationFilter(true)]
        public HttpResponseMessage Delete(long id)
        {
            HttpResponseMessage response;
            try
            {
                ProductoService service = (ProductoService)new ProductoService().setDatabase(db);
                Producto p = service.find(id);
                service.delete(id);

                if (p.Estado == "D") { 
                response = this.getSuccessResponse(p);
                }
                else {
                    response = this.getSuccessResponse("No se puede eliminar porque existe una compra o hay stock disponible");
                }
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }
    }
}