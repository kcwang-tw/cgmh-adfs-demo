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
    public class UserRankTestApi : TestApiBase, IUserRankQuery
    {
        public UserRankTestApi(IHttpClientFactory clientFactory) 
            : base(clientFactory)
        {
        }

        public async Task<UserRank> GetUserRankAsync(string userId)
        {
            var response = await Client.GetAsync($"user-rank/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                var userRank = JsonConvert.DeserializeObject<REDUDEMORNK>(apiResponse);

                if (string.IsNullOrWhiteSpace(userRank.IDNO))
                {
                    return null;
                }

                return new UserRank
                {
                    UserId = userRank.IDNO,
                    Rank = userRank.RNK
                };
            }

            return null;
        }
    }
}
