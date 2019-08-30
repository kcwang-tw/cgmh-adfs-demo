using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security.ActiveDirectory;
using Owin;


namespace TestApi.WebApi
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseActiveDirectoryFederationServicesBearerAuthentication(
            new ActiveDirectoryFederationServicesBearerAuthenticationOptions
            {
                MetadataEndpoint = "https://adfs2016.southeastasia.cloudapp.azure.com/federationmetadata/2007-06/federationmetadata.xml",
                //ConfigurationManager.AppSettings["ida:AdfsMetadataEndpoint"],
                TokenValidationParameters = new TokenValidationParameters()
                {
                    SaveSigninToken = true,
                    ValidAudience = @"https://localhost:44382"//ConfigurationManager.AppSettings["ida:Audience"]
                }

            });
        }
    }
}
