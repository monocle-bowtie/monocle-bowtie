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
    public class CajaController : BaseApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CajaController));
        
        // GET api/Caja
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                CajaService service = (CajaService)new CajaService().setDatabase(db);
                List<Caja> caja = service.getAll();



                response = this.getSuccessResponse(caja);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // GET api/GetCaja/5
        public HttpResponseMessage GetCaja(long id)
        {
            HttpResponseMessage response;
            try
            {
                CajaService service = (CajaService)new CajaService().setDatabase(db);
                Caja c = service.find(id);



                response = this.getSuccessResponse(c);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        public HttpResponseMessage GetDate(DateTime p_desde, DateTime p_hasta)
        {
            HttpResponseMessage response;
            try
            {
                CajaService service = (CajaService)new CajaService().setDatabase(db);
                List<Caja> c = service.getFecha(p_desde, p_hasta);



                response = this.getSuccessResponse(c);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Caja c)
        {
            HttpResponseMessage response;
            try
            {
                CajaService service = (CajaService)new CajaService().setDatabase(db);
                c.FechaAlta = DateTime.Today;
                c.UsuarioAlta = 1;
                c.Estado = "A";
                c = service.saveOrUpdate(c);

                response = this.getSuccessResponse(c);
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
                CajaService service = (CajaService)new CajaService().setDatabase(db);
                Caja c = service.find(id);
                service.delete(id);

                if (c.Estado == "D")
                {
                    response = this.getSuccessResponse(c);
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