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
    [Route("api/v1/seating-plan")]
    [ApiController]
    public class SeatingPlanController : ControllerBase
    {
        private readonly ISeatingPlanQuery _query;
        private readonly IMapper _mapper;

        public SeatingPlanController(ISeatingPlanQuery query, IMapper mapper)
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
                var resource = _mapper.Map<IEnumerable<SeatingPlanResource>>(seatingPlan);
                return Ok(new ApiResponse(InnerStatusCodes.GetSuccess2001)
                {
                    data = resource
                });
            }

            return NotFound();
        }
    }
}