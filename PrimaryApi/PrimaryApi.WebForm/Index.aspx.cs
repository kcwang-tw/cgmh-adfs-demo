using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services.Description;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Security.Claims;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;

namespace PrimaryApi.WebForm
{
    public partial class Index : System.Web.UI.Page
    {
        private static string _clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string _metadataAddress = ConfigurationManager.AppSettings["ida:ADFSDiscoveryDoc"];
        private static string _redirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                ClaimsPrincipal current = ClaimsPrincipal.Current;
                var bootstrapContext = current.Identities.First().BootstrapContext;

                Response.Write(HttpContext.Current.User.Identity.Name);
               
                var authContext = new AuthenticationContext(
                    "https://adfs2016.southeastasia.cloudapp.azure.com/adfs/", 
                    false
                    );

                //UserAssertion userAssertion = new UserAssertion(bootstrapContext.ToString(),
                //                             "urn:ietf:params:oauth:grant-type:jwt-bearer",
                //                                "Sky");


                var backendtokenTask = await authContext.AcquireTokenAsync(
                    "https://localhost:44326", // ResourceID
                    _clientId,
                    new Uri(_redirectUri),
                    new PlatformParameters(PromptBehavior.Auto));

                //var clientId = "39dd26f0-2a50-464f-b6a6-f6a9f6d60413";
                //var clientSecret = "b385U0bfgD3Jyxqg2TaKsDBipTPQey0RUJBKDuhh";
                //var credential = new ClientCredential(clientId, clientSecret);

                //var backendtokenTask = await authContext.AcquireTokenAsync(
                //    "https://localhost:44326", // ResourceID
                //    _clientId,
                //    userAssertion);


                Response.Write(backendtokenTask.AccessToken);

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", backendtokenTask.AccessToken);

                var response = await httpClient.GetAsync(@"https://localhost:44326/api/v1/seats");

                if (!response.IsSuccessStatusCode)
                {
                     Response.Write("An error occurred : " + response.ReasonPhrase);

                    return;
                }

                Response.Write(await response.Content.ReadAsStringAsync());

            }
        }

        // SignIn
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                HttpContext.Current.GetOwinContext()
                    .Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = "/index.aspx" }, 
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
        }

        // SignOut
        protected void Button2_Click(object sender, EventArgs e)
        {
            HttpContext.Current.GetOwinContext().Authentication.SignOut(
                OpenIdConnectAuthenticationDefaults.AuthenticationType, 
                CookieAuthenticationDefaults.AuthenticationType);
        }

        
        //public void EndSession()
        //{
        //    // If AAD sends a single sign-out message to the app, end the user's session, but don't redirect to AAD for sign out.
        //    HttpContext.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
        //}
    }
}