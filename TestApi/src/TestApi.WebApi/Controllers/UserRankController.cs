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
    [RoutePrefix("api/user-rank")]
    public class UserRankController : ApiController
    {
        private readonly UserRankRepository _repo;

        public UserRankController()
        {
            _repo = new UserRankRepository();
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IHttpActionResult> Get(string userId)
        {
            try
            {
                var rank = await _repo.GetRankByUserIdAsync(userId);

                if (!string.IsNullOrWhiteSpace(rank.IDNO))
                {
                    return Ok(rank);
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
