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
    public class PromocionController : BaseApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PromocionController));
        // GET api/Promocion
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                PromocionService service = (PromocionService)new PromocionService().setDatabase(db);
                List<Promocion> promociones = service.getAll();



                response = this.getSuccessResponse(promociones);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }



        // GET api/Promocion/5
        public HttpResponseMessage GetPromocion(long id)
        {
            HttpResponseMessage response;
            try
            {
                PromocionService service = (PromocionService)new PromocionService().setDatabase(db);
                Promocion p = service.find(id);



                response = this.getSuccessResponse(p);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }


        // GET api/Promocion/5
        public HttpResponseMessage GetPromoDetalle()
        {
            HttpResponseMessage response;
            try
            {
                PromocionService service = (PromocionService)new PromocionService().setDatabase(db);
                List<PromoDetalle> p = service.GetPromoDetalle();



                response = this.getSuccessResponse(p);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Promocion p)
        {
            HttpResponseMessage response;
            try
            {
                PromocionService service = (PromocionService)new PromocionService().setDatabase(db);
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

        // POST api/<controller>
        public HttpResponseMessage PostDetalle([FromBody] PromocionProducto p)
        {
            HttpResponseMessage response;
            try
            {
                PromocionService service = (PromocionService)new PromocionService().setDatabase(db);
                p.FechaAlta = DateTime.Today;
                p.UsuarioAlta = 1;
                p.Estado = "A";
                p = service.SaveDetalle(p);

                response = this.getSuccessResponse(p);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        public HttpResponseMessage PostDetallev2([FromBody] PromoDetalle p)
        {
            HttpResponseMessage response;
            try
            {
                PromocionService service = (PromocionService)new PromocionService().setDatabase(db);
                p.FechaAlta = DateTime.Today;
                p.UsuarioAlta = 1;
                p.Estado = "A";
                p = service.SaveDetalleV2(p);

                response = this.getSuccessResponse(p);
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
                PromocionService service = (PromocionService)new PromocionService().setDatabase(db);
                Promocion p = service.find(id);
                service.delete(id);

                if (p.Estado == "D")
                {
                    response = this.getSuccessResponse(p);
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