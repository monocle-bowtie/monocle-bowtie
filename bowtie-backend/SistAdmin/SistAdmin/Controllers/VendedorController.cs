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
    public class VendedorController : BaseApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(VendedorController));

        // GET api/Vendedor
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                VendedorService service = (VendedorService)new VendedorService().setDatabase(db);
                List<Vendedor> vendedores = service.getAll();



                response = this.getSuccessResponse(vendedores);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // GET api/Vendedor/5
        public HttpResponseMessage GetVendedor(long id)
        {
            HttpResponseMessage response;
            try
            {
                VendedorService service = (VendedorService)new VendedorService().setDatabase(db);
                Vendedor v = service.find(id);



                response = this.getSuccessResponse(v);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // POST api/Vendedor
        public HttpResponseMessage Post([FromBody] Vendedor v)
        {
            HttpResponseMessage response;
            try
            {
                VendedorService service = (VendedorService)new VendedorService().setDatabase(db);
                v.FechaAlta = DateTime.Today;
                v.UsuarioAlta = 1;
                v.Estado = "A";
                v = service.saveOrUpdate(v);

                response = this.getSuccessResponse(v);
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

        // DELETE api/Vendedor/5
        public HttpResponseMessage Delete(long id)
        {
            HttpResponseMessage response;
            try
            {
                VendedorService service = (VendedorService)new VendedorService().setDatabase(db);
                Vendedor v = service.find(id);
                service.delete(id);

                if (v.Estado == "D")
                {
                    response = this.getSuccessResponse(v);
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