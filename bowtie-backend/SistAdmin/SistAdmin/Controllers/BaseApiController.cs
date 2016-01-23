using SistAdmin.Models;
using SistAdmin.TransferObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace SistAdmin.Controllers
{
	public abstract class BaseApiController : ApiController
	{
        protected bowtieEntities db = new bowtieEntities();

		protected HttpResponseMessage getSuccessResponse(Object data)
		{
			return Request.CreateResponse(HttpStatusCode.OK, data);
		}

		protected HttpResponseMessage getErrorResponse(Exception e)
		{
			string simplifiedExceptions = ConfigurationManager.AppSettings["SimplifiedExceptions"];
			if (simplifiedExceptions == "true")
			{
				SimplifiedExceptionTO to = new SimplifiedExceptionTO();
				to.Message = e.Message;
				to.ClassName = e.GetType().FullName;
				return Request.CreateResponse(HttpStatusCode.InternalServerError, to);
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
			}
		}
	}
}
