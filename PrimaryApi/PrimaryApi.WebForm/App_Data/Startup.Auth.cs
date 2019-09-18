using System;
using System.Configuration;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

[assembly: OwinStartup(typeof(PrimaryApi.WebForm.Startup))]

namespace PrimaryApi.WebForm
{
    public partial class Startup
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
        /// ADFS 目錄探索位置 ex: https://adfs2016.southeastasia.cloudapp.azure.com/adfs/.well-known/openid-configuration
        /// </summary>
        private static string _metadataAddress = ConfigurationManager.AppSettings["ida:ADFSDiscoveryDoc"];

        /// <summary>
        /// 導向位置 ex: https://localhost:44398/
        /// </summary>
        private static string _redirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];

        /// <summary>
        /// Authority ex: https://adfs2016.southeastasia.cloudapp.azure.com/adfs/
        /// </summary>
        private static string _authority = ConfigurationManager.AppSettings["ida:Authority"];

        /// <summary>
        /// Resource Id ex: https://localhost:44326
        /// </summary>
        private static string _resourceId = ConfigurationManager.AppSettings["ida:ResourceId"];

        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = _clientId,
                    MetadataAddress = _metadataAddress,
                    PostLogoutRedirectUri = "https://localhost:44398/index.aspx",
                    RedirectUri = _redirectUri,
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        SaveSigninToken = true
                    },
                    Notifications = new OpenIdConnectAuthenticationNotifications
                    {
                        AuthorizationCodeReceived = async (AuthorizationCodeReceivedNotification context) =>
                        {
                            AuthenticationContext authContext = new AuthenticationContext(_authority, false);

                            // 導向位置，需和註冊之位置相同，這邊直接透過 HttpContext 取得，如出現 400 ，請檢查此值是否完全一樣
                            Uri uri = new Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path));

                            // ASP.NET Web Form 走 OIDC，故這邊加上 AuthorizationCode 流程，讓後續可以使用 OBO 呼叫 API
                            AuthenticationResult result = await authContext.AcquireTokenByAuthorizationCodeAsync(
                                context.Code,
                                uri,
                                new ClientCredential(_clientId, _appKey),
                                _resourceId);
                        },
                        SecurityTokenValidated = notification =>
                        {
                            // 設定 id_token ( Logout 需要使用 )
                            // 若是 asp.net core 可以直接使用 SaveToken 來處理
                            notification.AuthenticationTicket.Identity.AddClaim(new Claim("id_token", notification.ProtocolMessage.IdToken));
                            return Task.FromResult(0);
                        },
                        RedirectToIdentityProvider = n =>
                        {
                            // if signing out, add the id_token_hint
                            if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.Logout)
                            {
                                var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

                                if (idTokenHint != null)
                                {
                                    // 設定 id_token_hint ( 登出時必須使用 id_token )
                                    n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                                }

                            }

                            return Task.FromResult(0);
                        }
                        // 錯誤處理，這邊沒使用，註解提供給你們備查
                        //AuthenticationFailed =
                        //(AuthenticationFailedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> context) =>
                        //{
                        //    context.HandleResponse();
                        //    context.Response.Redirect("/Home/Error?message=" + context.Exception.Message);
                        //    return Task.FromResult(0);
                        //}

                    }
                });
        }
    }
}
