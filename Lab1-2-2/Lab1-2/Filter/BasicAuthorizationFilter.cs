using Lab1_2.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1_2.Models
{
    public class BasicAuthorizationFilter : IAuthorizationFilter
    {
        private const string USERNAME = "admin";
        private const string PASSWORD = "1234";
        private const string Realm = "App Realm";

        public static bool Validate(string username, string password)
        {
            return USERNAME.Equals(username) && PASSWORD.Equals(password);
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.Keys.Contains(HeaderNames.Authorization))
            {
                context.HttpContext.Response.Headers.Add("WWW-Authenticate",
                string.Format("Basic realm=\"{0}\"", Realm)); context.Result = new UnauthorizedResult(); return;
            }

            string authenticationToken = context.HttpContext.Request.Headers[HeaderNames.Authorization];
            authenticationToken = authenticationToken.Split(" ")[1].Trim();
            string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
            string[] usernamePasswordArray = decodedAuthenticationToken.Split(':'); string username = usernamePasswordArray[0];
            string password = usernamePasswordArray[1];

            if (Validate(username, password))
            {
                var identity = new GenericIdentity(username);
                IPrincipal principal = new GenericPrincipal(identity, null); Thread.CurrentPrincipal = principal;
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }
            if (context.Filters.OfType<DisableBasicAuthentication>().Any())
            {
                return;
            }
        }
    }
}
