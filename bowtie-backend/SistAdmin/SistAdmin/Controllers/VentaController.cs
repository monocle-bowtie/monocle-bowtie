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
    public class VentaController : BaseApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(VentaController));

        // GET api/<controller>
        public HttpResponseMessage GetPreVenta()
        {
            HttpResponseMessage response;
            try
            {
                VentaService service = (VentaService)new VentaService().setDatabase(db);
                List<Preventa> ventas = service.getPreVentas();



                response = this.getSuccessResponse(ventas);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] RegistrarVenta rv)
        {
            HttpResponseMessage response;
            try
            {
                VentaService service = (VentaService)new VentaService().setDatabase(db);

                service.RegistrarVenta(rv);


                response = this.getSuccessResponse(rv);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }


        public HttpResponseMessage PostPreVenta([FromBody] ConfirmarPreventa cp)
        {
            HttpResponseMessage response;
            try
            {
                VentaService service = (VentaService)new VentaService().setDatabase(db);

                service.RegistrarPreventa(cp);


                response = this.getSuccessResponse(cp);
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
        public void Delete(int id)
        {
        }
    }
}