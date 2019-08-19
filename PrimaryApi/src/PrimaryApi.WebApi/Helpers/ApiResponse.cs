using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimaryApi.WebApi.Helpers
{
    public class ApiResponse
    {
        public ApiResponse(InnerStatusCodes status)
        {
            innerStatusCode = status;
        }

        public InnerStatusCodes innerStatusCode { get; private set; }
        public dynamic data { get; set; }
    }
}
