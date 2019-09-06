using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrimaryApi.WinForm
{
    public partial class Form1 : Form
    {
        private static string _authority = ConfigurationManager.AppSettings["ida:Authority"];
        private static string _clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private readonly Uri _redirectUri = new Uri(ConfigurationManager.AppSettings["ida:RedirectUri"]);

        private static string _resourceId = ConfigurationManager.AppSettings["primaryApi:ResourceId"];
        private static string _resourceBaseAddress = ConfigurationManager.AppSettings["primaryApi:BaseAddress"];

        private AuthenticationContext _authContext = null;

        public Form1()
        {
            InitializeComponent();
            _authContext = new AuthenticationContext(_authority, false);
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            _authContext.TokenCache.Clear();

            AuthenticationResult result = null;
            try
            {
                result = await _authContext.AcquireTokenAsync(
                    _resourceId,
                    //"https://localhost:44382/s2",
                    _clientId,
                    _redirectUri, new PlatformParameters(PromptBehavior.Always));
                MessageBox.Show("Token:" + result.AccessToken);
                MessageBox.Show(result.UserInfo.DisplayableId);

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", result.AccessToken);

                //https://localhost:44326/api/v1/seats

                var response = await httpClient.GetAsync(_resourceBaseAddress + "/api/v1/seats");
                //var response = await httpClient.GetAsync("https://localhost:44382/api/test");


                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("An error occurred : " + response.ReasonPhrase);

                    return;
                }

                MessageBox.Show(await response.Content.ReadAsStringAsync());

            }
            catch (AdalException ex)
            {
                if (ex.ErrorCode == "access_denied")
                {
                    MessageBox.Show("access_denied");
                }
                else
                {
                    string message = ex.Message;
                    if (ex.InnerException != null)
                    {
                        message += "Error Code: " + ex.ErrorCode + "Inner Exception : " + ex.InnerException.Message;
                    }

                    MessageBox.Show(message);
                }

                return;
            }
        }
    }
}
