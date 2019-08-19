using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PrimaryApi.DataAccess.FromTestApi.Applications
{
    public abstract class TestApiBase
    {
        protected HttpClient Client { get; private set; }

        public TestApiBase(IHttpClientFactory clientFactory)
        {
            Client = clientFactory.CreateClient("test-api");
        }
    }
}
