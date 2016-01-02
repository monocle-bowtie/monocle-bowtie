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
    public class CiudadController : BaseApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CiudadController));

        // GET api/Ciudad/Get
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                CiudadService service = (CiudadService)new CiudadService().setDatabase(db);
                List<Ciudad> ciudades = service.getAll();



                response = this.getSuccessResponse(ciudades);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // GET api/Ciudad/5
        public HttpResponseMessage GetCiudad(long id)
        {
            HttpResponseMessage response;
            try
            {
                CiudadService service = (CiudadService)new CiudadService().setDatabase(db);
                Ciudad c = service.find(id);

                service.delete(id);

                response = this.getSuccessResponse(c);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Ciudad c)
        {
            HttpResponseMessage response;
            try
            {
                CiudadService service = (CiudadService)new CiudadService().setDatabase(db);
                c.FechaAlta = DateTime.Today;
                c.UsuarioAlta = 1;
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
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}