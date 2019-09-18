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
        /// <summary>
        /// Client ID ex: 343c4d52-005c-4d2e-b7e7-ef98c0709373
        /// </summary>
        private static string _clientId = ConfigurationManager.AppSettings["ida:ClientId"];

        /// <summary>
        /// APP Key ex: YN1OvJOAfZD9Sj3FLOCewnOBbUq-VafedXXFXJuo
        /// </summary>
        private static string _appKey = ConfigurationManager.AppSettings["ida:AppKey"];

        /// <summary>
        /// Authority ex: https://adfs2016.southeastasia.cloudapp.azure.com/adfs/
        /// </summary>
        private static string _authority = ConfigurationManager.AppSettings["ida:Authority"];

        /// <summary>
        /// Resource Id ex: https://localhost:44326
        /// </summary>
        private static string _resourceId = ConfigurationManager.AppSettings["ida:ResourceId"];


        protected async void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                Response.Write(HttpContext.Current.User.Identity.Name);

                var authContext = new AuthenticationContext(_authority, false);

                var credential = new ClientCredential(_clientId, _appKey);

                var nameIdentifier = ClaimsPrincipal.Current.Claims.FirstOrDefault(
                    c => c.Type == ClaimTypes.NameIdentifier).Value;

                var result = await authContext.AcquireTokenSilentAsync(
                    _resourceId,
                    credential,
                    new UserIdentifier(nameIdentifier, UserIdentifierType.UniqueId));

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", result.AccessToken);

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
            // 取出所有驗證 Type
            IEnumerable<AuthenticationDescription> authTypes = 
                HttpContext.Current.GetOwinContext().Authentication.GetAuthenticationTypes();

            HttpContext.Current.GetOwinContext().Authentication.SignOut(
                new AuthenticationProperties { RedirectUri= "https://localhost:44398/index.aspx" },
                authTypes.Select(t => t.AuthenticationType).ToArray());
        }

        
        //public void EndSession()
        //{
        //    // If AAD sends a single sign-out message to the app, end the user's session, but don't redirect to AAD for sign out.
        //    HttpContext.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
        //}
    }
}