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
    public class ConceptoController : BaseApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ConceptoController));

        // GET api/Concepto
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                ConceptoService service = (ConceptoService)new ConceptoService().setDatabase(db);
                List<Concepto> conceptos = service.getAll();



                response = this.getSuccessResponse(conceptos);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // GET api/Concepto/5
        public HttpResponseMessage GetConcepto(long id)
        {
            HttpResponseMessage response;
            try
            {
                ConceptoService service = (ConceptoService)new ConceptoService().setDatabase(db);
                Concepto c = service.find(id);



                response = this.getSuccessResponse(c);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // POST api/Concepto
        public HttpResponseMessage Post([FromBody] Concepto c)
        {
            HttpResponseMessage response;
            try
            {
                ConceptoService service = (ConceptoService)new ConceptoService().setDatabase(db);
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
       // public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/Concepto/5
        public HttpResponseMessage Delete(long id)
        {
            HttpResponseMessage response;
            try
            {
                ConceptoService service = (ConceptoService)new ConceptoService().setDatabase(db);
                Concepto c = service.find(id);
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