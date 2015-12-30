using SistAdmin.Models;
using SistAdmin.Services;
using SistAdmin.TransferObjects;
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
    public class StockController : BaseApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(StockController));

        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                StockService service = (StockService)new StockService().setDatabase(db);
                List<StockDetalle> stocks = service.getAll();



                response = this.getSuccessResponse(stocks);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // GET api/<controller>/5
        public HttpResponseMessage GetStock(long id)
        {
            HttpResponseMessage response;
            try
            {
                StockService service = (StockService)new StockService().setDatabase(db);
                Stock s = service.find(id);



                response = this.getSuccessResponse(s);
            }
            catch (Exception e)
            {
                response = this.getErrorResponse(e);
            }
            return response;
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Stock s)
        {
            HttpResponseMessage response;
            try
            {
                StockService service = (StockService)new StockService().setDatabase(db);
                s.FechaAlta = DateTime.Today;
                s.UsuarioAlta = 1;
                s = service.saveOrUpdate(s);

                response = this.getSuccessResponse(s);
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
        public HttpResponseMessage Delete(long id)
        {
            HttpResponseMessage response;
            try
            {
                StockService service = (StockService)new StockService().setDatabase(db);
                Stock s = service.find(id);
                service.delete(id);

                if (s.Estado == "D")
                {
                    response = this.getSuccessResponse(s);
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