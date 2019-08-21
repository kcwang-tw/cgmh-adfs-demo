using Newtonsoft.Json;
using PrimaryApi.Core.DomainModels;
using PrimaryApi.Core.Interfaces;
using PrimaryApi.DataAccess.FromTestApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryApi.DataAccess.FromTestApi.Applications
{
    public class SeatsTestApi : TestApiBase, ISeatsQuery, ISeatsCommand
    {
        public SeatsTestApi(IHttpClientFactory clientFactory) 
            : base(clientFactory)
        {
        }

        public async Task<IEnumerable<DepartmentSeat>> GetAllSeatsAsync()
        {
            var response = await Client.GetAsync("seats");
            var result = new List<DepartmentSeat>();

            if (response.IsSuccessStatusCode)
            {
                var readItems = await response.Content.ReadAsAsync<IEnumerable<REDUDEMOSEAT>>();

                foreach (var item in readItems)
                {
                    result.Add(new DepartmentSeat
                    {
                        UserId = item.IDNO,
                        UserName = item.CNM,
                        Extension = item.EXT,
                        Phone = item.PHONE,
                        Seat = item.SEAT
                    });
                }
            }

            return result;
        }

        public async Task<DepartmentSeat> GetSeatByUserIdAsync(string userId)
        {
            var allSeats = await GetAllSeatsAsync();
            return allSeats.FirstOrDefault(s => s.UserId == userId);
        }

        public async Task<int> AddSeatAsync(DepartmentSeat seat)
        {
            var postContent = new REDUDEMOSEAT
            {
                IDNO = seat.UserId,
                CNM = seat.UserName,
                EXT = seat.Extension,
                PHONE = seat.Phone,
                SEAT = seat.Seat
            };

            var convertContent = JsonConvert.SerializeObject(postContent);

            var response = await Client.PostAsync("seats", new StringContent(convertContent, Encoding.UTF8, "application/json"));

            var readResult = await response.Content.ReadAsAsync<ExecuteResult>();

            return readResult.AffectedRows;
        }

        public async Task<int> RemoveSeatAsync(string userId)
        {
            var response = await Client.DeleteAsync("seats/" + userId);

            var readResult = await response.Content.ReadAsAsync<ExecuteResult>();

            return readResult.AffectedRows;
        }
    }
}
