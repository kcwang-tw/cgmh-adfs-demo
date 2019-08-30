using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using PrimaryApi.Core.DomainModels;
using PrimaryApi.Core.Interfaces;
using PrimaryApi.DataAccess.FromTestApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IHttpClientFactory = System.Net.Http.IHttpClientFactory;

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
            //resource=https%3a%2f%2flocalhost:44382
            //&client_id=0965501a-0e59-4fb4-8b37-509e50d85626
            //&client_secret=lsmZnF8GgeTd_wSiDYk2ST6ENWqq4SgJ4T3FBF-g
            //&grant_type=client_credentials

            var credential = new ClientCredential(
                "0965501a-0e59-4fb4-8b37-509e50d85626",
                "OgaagV7PkQLSrkiVtVpNP5VCoG6uK3T6r3th-xeu");
            var authContext = new AuthenticationContext(
                "https://adfs2016.southeastasia.cloudapp.azure.com/adfs/",
                false);

            authContext.TokenCache.Clear();

            //var authResult = await authContext.AcquireTokenSilentAsync(
            //    "https://localhost:44382", credential, UserIdentifier.AnyUser);


            var authResult = await authContext.AcquireTokenAsync("https://localhost:44382", credential);

            Client.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", authResult.AccessToken);

            var response = await Client.GetAsync("seats/");
            var result = new List<DepartmentSeat>();

            if(!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }


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
