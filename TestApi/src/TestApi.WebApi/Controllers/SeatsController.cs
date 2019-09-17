using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using TestApi.WebApi.Models;
using TestApi.WebApi.Repositories;

namespace TestApi.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/seats")]
    public class SeatsController : ApiController
    {
        private readonly SeatsRepository _repo;

        public SeatsController()
        {
            _repo = new SeatsRepository();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var scopeClaim = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/scope");
            if (scopeClaim == null || (!scopeClaim.Value.Contains("user_impersonation")))
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.Unauthorized, ReasonPhrase = "The Scope claim does not contain 'user_impersonation' or scope claim not found" });
            }

            Claim subject = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Upn);

            try
            {
                var seatingPlan = await _repo.GetAllSeatsAsync();

                if (seatingPlan.Count() >= 1)
                {
                    return Ok(seatingPlan);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Add([FromBody]REDUDEMOSEAT seat)
        {
            try
            {
                var affectedRows = await _repo.CreateSeatAsync(seat);

                return Ok(new { affectedRows });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{idno}")]
        public async Task<IHttpActionResult> Delete(string idno)
        {
            try
            {
                var affectedRows = await _repo.DeleteSeatAsync(idno);

                return Ok(new { affectedRows });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
