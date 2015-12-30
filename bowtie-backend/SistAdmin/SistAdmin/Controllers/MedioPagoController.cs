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
    public class MedioPagoController : BaseApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MedioPagoController));
        // GET api/MedioPago
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                MedioPagoService service = (MedioPagoService)new MedioPagoService().setDatabase(db);
                List<MedioPago> mediospago = service.getAll();



                response = this.getSuccessResponse(mediospago);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // GET api/GetMedioPago/5
        public HttpResponseMessage GetMedioPago(long id)
        {
            HttpResponseMessage response;
            try
            {
                MedioPagoService service = (MedioPagoService)new MedioPagoService().setDatabase(db);
                MedioPago mp = service.find(id);



                response = this.getSuccessResponse(mp);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] MedioPago mp)
        {
            HttpResponseMessage response;
            try
            {
                MedioPagoService service = (MedioPagoService)new MedioPagoService().setDatabase(db);
                mp.FechaAlta = DateTime.Today;
                mp.UsuarioAlta = 1;
                mp.Estado = "A";
                mp = service.saveOrUpdate(mp);

                response = this.getSuccessResponse(mp);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(long id)
        {
            HttpResponseMessage response;
            try
            {
                MedioPagoService service = (MedioPagoService)new MedioPagoService().setDatabase(db);
                MedioPago mp = service.find(id);
                service.delete(id);

                if (mp.Estado == "D")
                {
                    response = this.getSuccessResponse(mp);
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