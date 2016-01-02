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
    public class ClienteController : BaseApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ClienteController));
        
        // GET api/Cliente
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                ClienteService service = (ClienteService)new ClienteService().setDatabase(db);
                List<Cliente> clientes = service.getAll();



                response = this.getSuccessResponse(clientes);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // GET api/Cliente/5
        public HttpResponseMessage GetCliente(long id)
        {
            HttpResponseMessage response;
            try
            {
                ClienteService service = (ClienteService)new ClienteService().setDatabase(db);
                Cliente c = service.find(id);



                response = this.getSuccessResponse(c);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Cliente c)
        {
            HttpResponseMessage response;
            try
            {
                ClienteService service = (ClienteService)new ClienteService().setDatabase(db);
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
                ClienteService service = (ClienteService)new ClienteService().setDatabase(db);
                Cliente c = service.find(id);
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