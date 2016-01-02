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
    public class GrupoController : BaseApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(GrupoController));

        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                GrupoService service = (GrupoService)new GrupoService().setDatabase(db);
                List<GrupoProducto> gruposproductos = service.getAll();



                response = this.getSuccessResponse(gruposproductos);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        public HttpResponseMessage GetDetalle()
        {
            HttpResponseMessage response;
            try
            {
                GrupoService service = (GrupoService)new GrupoService().setDatabase(db);
                List<GrupoXProducto1> gruposproductos = service.getDetalle();



                response = this.getSuccessResponse(gruposproductos);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // GET api/Grupo/5
        public HttpResponseMessage GetGrupos(long id)
        {
            HttpResponseMessage response;
            try
            {
                GrupoService service = (GrupoService)new GrupoService().setDatabase(db);
                GrupoProducto gp = service.find(id);



                response = this.getSuccessResponse(gp);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // POST api/grupo
        public HttpResponseMessage Post([FromBody] GrupoProducto gp)
        {
            HttpResponseMessage response;
            try
            {
                GrupoService service = (GrupoService)new GrupoService().setDatabase(db);
                gp.FechaAlta = DateTime.Today;
                gp.UsuarioAlta = 1;
                gp.Estado = "A";
                gp = service.saveOrUpdate(gp);

                response = this.getSuccessResponse(gp);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // POST api/grupo
        public HttpResponseMessage PostDetalle([FromBody] GrupoXProducto1 gp)
        {
            HttpResponseMessage response;
            try
            {
                GrupoService service = (GrupoService)new GrupoService().setDatabase(db);
                gp.FechaAlta = DateTime.Today;
                gp.UsuarioAlta = 1;
                gp.Estado = "A";
                gp = service.saveOrUpdate1(gp);

                response = this.getSuccessResponse(gp);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
       // {
        //}

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(long id)
        {
            HttpResponseMessage response;
            try
            {
                GrupoService service = (GrupoService)new GrupoService().setDatabase(db);
                GrupoProducto gp = service.find(id);
                service.delete(id);

                if (gp.Estado == "D")
                {
                    response = this.getSuccessResponse(gp);
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