using SistAdmin.Models;
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
    public class ProveedorController : BaseApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ProveedorController));

        // GET api/Proveedor/Get
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                ProveedorService service = (ProveedorService)new ProveedorService().setDatabase(db);
                List<Proveedor> proveedores = service.getAll();



                response = this.getSuccessResponse(proveedores);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        //GET api/Proveedor/GetProducto/id
        public HttpResponseMessage GetProveedor(long id)
        {
            HttpResponseMessage response;
            try
            {
                ProveedorService service = (ProveedorService)new ProveedorService().setDatabase(db);
                Proveedor p = service.find(id);

                service.delete(id);

                response = this.getSuccessResponse(p);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // POST api/Proveedor
        public HttpResponseMessage Post([FromBody] Proveedor p)
        {
            HttpResponseMessage response;
            try
            {
                ProveedorService service = (ProveedorService)new ProveedorService().setDatabase(db);
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

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/Proveedor/5
        public HttpResponseMessage Delete(long id)
        {
            HttpResponseMessage response;
            try
            {
                ProveedorService service = (ProveedorService)new ProveedorService().setDatabase(db);
                Proveedor p = service.find(id);
                service.delete(id);

                response = this.getSuccessResponse(p);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }
    }
}