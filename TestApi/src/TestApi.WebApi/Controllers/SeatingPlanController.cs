using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TestApi.WebApi.Repositories;

namespace TestApi.WebApi.Controllers
{
    [RoutePrefix("api/seats")]
    public class SeatingPlanController : ApiController
    {
        private readonly SeatingPlanRepository _repo;

        public SeatingPlanController()
        {
            _repo = new SeatingPlanRepository();
        }

        [HttpGet]
        [Route]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var seatingPlan = await _repo.GetSeatingPlanAsync();

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
    }
}
