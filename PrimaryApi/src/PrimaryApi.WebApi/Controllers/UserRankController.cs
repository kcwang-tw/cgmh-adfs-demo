using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimaryApi.Core.Interfaces;
using PrimaryApi.WebApi.Helpers;
using PrimaryApi.WebApi.Resources;

namespace PrimaryApi.WebApi.Controllers
{
    [Route("api/v1/user-rank")]
    [ApiController]
    public class UserRankController : ControllerBase
    {
        private readonly IUserRankQuery _query;
        private readonly IMapper _mapper;

        public UserRankController(IUserRankQuery query, IMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetRankByUserId(string userId)
        {
            var userRank = await _query.GetUserRankAsync(userId);

            if (!string.IsNullOrWhiteSpace(userRank.UserId))
            {
                var resource = _mapper.Map<UserRankResource>(userRank);
                return Ok(
                    new ApiResponse(InnerStatusCodes.GetSuccess2001)
                    {
                        data = resource
                    });
            }

            return NotFound();
        }
    }
}