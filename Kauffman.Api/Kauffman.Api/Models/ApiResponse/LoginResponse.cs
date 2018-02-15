using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kauffman.Api.Models.ApiResponse
{
    public class LoginResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public double expires_in { get; set; }
        public DateTime issued { get; set; }
        public DateTime expires { get; set; }

    }
}