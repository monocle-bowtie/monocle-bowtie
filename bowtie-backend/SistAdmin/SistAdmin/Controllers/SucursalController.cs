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
    public class SucursalController : BaseApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SucursalController));

        // GET api/Sucursal
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                SucursalService service = (SucursalService)new SucursalService().setDatabase(db);
                List<Sucursal> sucursales = service.getAll();



                response = this.getSuccessResponse(sucursales);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // GET api/Sucursal/5
        public HttpResponseMessage GetSucursal(long id)
        {
            HttpResponseMessage response;
            try
            {
                SucursalService service = (SucursalService)new SucursalService().setDatabase(db);
                Sucursal p = service.find(id);



                response = this.getSuccessResponse(p);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Sucursal s)
        {
            HttpResponseMessage response;
            try
            {
                SucursalService service = (SucursalService)new SucursalService().setDatabase(db);
                s.FechaAlta = DateTime.Today;
                s.UsuarioAlta = 1;
                s.Estado = "A";
                s = service.saveOrUpdate(s);

                response = this.getSuccessResponse(s);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // PUT api/<controller>/5
       // public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(long id)
        {
            HttpResponseMessage response;
            try
            {
                SucursalService service = (SucursalService)new SucursalService().setDatabase(db);
                Sucursal s = service.find(id);
                service.delete(id);

                if (s.Estado == "D")
                {
                    response = this.getSuccessResponse(s);
                }
                else
                {
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