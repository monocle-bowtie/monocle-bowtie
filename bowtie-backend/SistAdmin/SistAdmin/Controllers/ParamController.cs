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
    public class ParamController : BaseApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ParamController));
        // GET api/Param
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                ParamService service = (ParamService)new ParamService().setDatabase(db);
                List<Parametrizacion> parametrizacion = service.getAll();



                response = this.getSuccessResponse(parametrizacion);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // GET api/param/5
        public HttpResponseMessage GetParametros(long id)
        {
            HttpResponseMessage response;
            try
            {
                ParamService service = (ParamService)new ParamService().setDatabase(db);
                Parametrizacion param = service.find(id);
                response = this.getSuccessResponse(param);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // POST api/Param
        public HttpResponseMessage Post([FromBody] Parametrizacion param)
        {
            HttpResponseMessage response;
            try
            {
                ParamService service = (ParamService)new ParamService().setDatabase(db);
                param.FechaAlta = DateTime.Today;
                param.UsuarioAlta = 1;
                param.Estado = "A";
                param = service.saveOrUpdate(param);

                response = this.getSuccessResponse(param);
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
                ParamService service = (ParamService)new ParamService().setDatabase(db);
                Parametrizacion param = service.find(id);
                service.delete(id);

                if (param.Estado == "D")
                {
                    response = this.getSuccessResponse(param);
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