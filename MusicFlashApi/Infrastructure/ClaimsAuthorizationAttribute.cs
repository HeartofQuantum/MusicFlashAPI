using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MusicFlashApi.Infrastructure
{
    public class ClaimsAuthorizationAttribute : AuthorizationFilterAttribute 
    {

        public string ClaimType { get; set;}
        public string ClaimValue{ get; set; }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {
            var prinicipal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            if (!prinicipal.Identity.IsAuthenticated)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            if (!(prinicipal.HasClaim(x => x.Type == ClaimType && x.Value == ClaimValue)))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }

            //User is Authorizaed, complete execution
            return Task.FromResult<object>(null);
        }

    }
}