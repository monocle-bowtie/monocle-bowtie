using SistAdmin.Models;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
namespace SistAdmin.App_Start
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        protected bowtieEntities db = new bowtieEntities();

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            string user = context.UserName;
            string password = context.Password;

            Usuario dbUser = this.db.Usuario.Where(u => u.login == context.UserName).FirstOrDefault();

            if (password != "password")
            {
                context.SetError("invalid_grant", "Contrseña incorrecta.");
                return;
            }
            if (dbUser == null)
            {
                context.SetError("invalid_grant", "Usuario inexistente.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));


            foreach (RolxUsuario ur in dbUser.RolxUsuario )
            {
                foreach (FuncionXRol rp in ur.Rol.FuncionXRol)
                {   
                    if (!identity.HasClaim(ClaimTypes.Role, rp.Funcion.Descripcion))
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, rp.Funcion.Descripcion));
                    }
                }
            }

            context.Validated(identity);

        }

    }
}