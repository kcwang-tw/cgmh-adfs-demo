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
    [Route("api/v1/seats")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly ISeatsQuery _query;
        private readonly IMapper _mapper;

        public SeatsController(ISeatsQuery query, IMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSeats()
        {
            var seatingPlan = await _query.GetAllSeatsAsync();

            if (seatingPlan.Count() >= 1)
            {
                var resource = _mapper.Map<IEnumerable<SeatResource>>(seatingPlan);
                return Ok(new ApiResponse(InnerStatusCodes.GetSuccess2001)
                {
                    data = resource
                });
            }

            return NotFound();
        }
    }
}