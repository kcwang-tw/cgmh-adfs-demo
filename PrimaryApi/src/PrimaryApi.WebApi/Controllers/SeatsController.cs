using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using PrimaryApi.Core.DomainModels;
using PrimaryApi.Core.Interfaces;
using PrimaryApi.WebApi.Helpers;
using PrimaryApi.WebApi.Resources;

namespace PrimaryApi.WebApi.Controllers
{
    [Authorize]
    [Route("api/v1/seats")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly ISeatsQuery _query;
        private readonly ISeatsCommand _command;
        private readonly IMapper _mapper;

        public SeatsController(ISeatsQuery query, ISeatsCommand command, IMapper mapper)
        {
            _query = query;
            _command = command;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSeats()
        {

            var scopeClaim = HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/scope");
            if (scopeClaim == null || (!scopeClaim.Value.Contains("user_impersonation")))
            {
                // 錯誤處理先註解，可自行增加
                //return Unauthorized();
            }

            // 取得 UPN
            Claim subject = HttpContext.User.FindFirst(ClaimTypes.Upn);

            // 取得 BootstrapContext
            string userAccessToken = (User.Identity as ClaimsIdentity).BootstrapContext as string;
            var userAssertion = new UserAssertion(userAccessToken, "urn:ietf:params:oauth:grant-type:jwt-bearer", subject.Value);


            var seats = await _query.GetAllSeatsAsync(userAssertion);

            if (seats.Count() >= 1)
            {
                var resource = _mapper.Map<IEnumerable<SeatResource>>(seats);
                return Ok(new ApiResponse(InnerStatusCodes.GetSuccess2001)
                {
                    data = resource
                });
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddSeat([FromBody]SeatAddResource seatResource)
        {
            var newSeat = _mapper.Map<DepartmentSeat>(seatResource);
            var result = await _command.AddSeatAsync(newSeat);

            if (result == 1)
            {
                var createdSeat = await _query.GetSeatByUserIdAsync(newSeat.UserId);
                return Ok(new ApiResponse(InnerStatusCodes.CreateSuccess2002) { data = createdSeat });
            }

            return BadRequest();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> RemoveSeat(string userId)
        {
            var result = await _command.RemoveSeatAsync(userId);

            if (result == 1)
            {
                return Ok(new ApiResponse(InnerStatusCodes.RemoveSuccess2004) { data = new { affectedRows = result } });
            }

            return BadRequest();

        }
    }
}