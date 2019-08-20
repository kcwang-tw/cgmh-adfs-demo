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
                var readItem = await response.Content.ReadAsAsync<REDUDEMORNK>();

                if (string.IsNullOrWhiteSpace(readItem.IDNO))
                {
                    return null;
                }

                return new UserRank
                {
                    UserId = readItem.IDNO,
                    Rank = readItem.RNK
                };
            }

            return null;
        }
    }
}
