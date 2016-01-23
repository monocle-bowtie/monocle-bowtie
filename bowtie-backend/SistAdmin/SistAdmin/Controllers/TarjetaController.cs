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
    public class TarjetaController : BaseApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TarjetaController));

        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                TarjetaService service = (TarjetaService)new TarjetaService().setDatabase(db);
                List<Tarjeta> tarjetas = service.getAll();



                response = this.getSuccessResponse(tarjetas);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // GET api/<controller>/5
        public HttpResponseMessage GetTarjeta(long id)
        {
            HttpResponseMessage response;
            try
            {
                TarjetaService service = (TarjetaService)new CajaService().setDatabase(db);
                Tarjeta t = service.find(id);



                response = this.getSuccessResponse(t);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Tarjeta t)
        {
            HttpResponseMessage response;
            try
            {
                TarjetaService service = (TarjetaService)new TarjetaService().setDatabase(db);
                t.FechaAlta = DateTime.Today;
                t.UsuarioAlta = 1;
                t.Estado = "A";
                t = service.saveOrUpdate(t);

                response = this.getSuccessResponse(t);
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

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(long id)
        {
            HttpResponseMessage response;
            try
            {
                TarjetaService service = (TarjetaService)new TarjetaService().setDatabase(db);
                Tarjeta t = service.find(id);
                service.delete(id);

                if (t.Estado == "D")
                {
                    response = this.getSuccessResponse(t);
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