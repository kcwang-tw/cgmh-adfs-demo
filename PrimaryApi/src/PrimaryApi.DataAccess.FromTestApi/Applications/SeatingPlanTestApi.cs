using Newtonsoft.Json;
using PrimaryApi.Core.DomainModels;
using PrimaryApi.Core.Interfaces;
using PrimaryApi.DataAccess.FromTestApi.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryApi.DataAccess.FromTestApi.Applications
{
    public class SeatingPlanTestApi : TestApiBase, ISeatingPlanQuery
    {
        public SeatingPlanTestApi(IHttpClientFactory clientFactory) 
            : base(clientFactory)
        {
        }

        public async Task<IEnumerable<SeatingPlan>> GetAllSeatsAsync()
        {
            var response = await Client.GetAsync("seating-plan");
            var result = new List<SeatingPlan>();

            if (response.IsSuccessStatusCode)
            {
                var readItems = await response.Content.ReadAsAsync<IEnumerable<REDUDEMOSEATING>>();

                foreach (var item in readItems)
                {
                    result.Add(new SeatingPlan
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
    }
}
