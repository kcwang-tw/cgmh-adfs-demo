using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimaryApi.WebApi.Resources
{
    public class SeatAddResource
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Extension { get; set; }
        public string Phone { get; set; }
        public string Seat { get; set; }
    }
}
