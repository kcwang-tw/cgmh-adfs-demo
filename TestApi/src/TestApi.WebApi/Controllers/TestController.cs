using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using TestApi.WebApi.Repositories;

namespace TestApi.WebApi.Controllers
{
    [Authorize]
    public class TestController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            var scopeClaim = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/scope");
            if (scopeClaim == null || (!scopeClaim.Value.Contains("user_impersonation")))
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.Unauthorized, ReasonPhrase = "The Scope claim does not contain 'user_impersonation' or scope claim not found" });
            }

            Claim subject = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier);

            return Ok("HelloWorld");
        }
    }
}
