using SistAdmin.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;

namespace SistAdmin.App_Filters
{
	public class ApiAuthenticationFilter : GenericAuthenticationFilter
	{
        protected bowtieEntities db = new bowtieEntities();

		public ApiAuthenticationFilter()
		{
		}

		public ApiAuthenticationFilter(bool isActive)
			: base(isActive)
		{
		}

		protected override bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
		{
		//	Usuario usr = this.db.Usuario.Where(u => u. == username).FirstOrDefault();// cambiar a login
		//	string pwd = usr.password;
			//if (password == pwd) return true;
			/*var provider = actionContext.ControllerContext.Configuration
							   .DependencyResolver.GetService(typeof(IUserServices)) as IUserServices;
			if (provider != null)
			{
				var userId = provider.Authenticate(username, password);
				if (userId > 0)
				{
					var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
					if (basicAuthenticationIdentity != null)
						basicAuthenticationIdentity.UserId = userId;
					return true;
				}
			}*/
			return false;
		}
	}
}