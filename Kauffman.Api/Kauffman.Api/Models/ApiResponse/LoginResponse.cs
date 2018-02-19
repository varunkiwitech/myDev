using Kauffman.Api.SubscriptionAssessment.Models;
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

    public class ApiMetadata
    {
        public int StatusCode { get; set; }

        public string StatusMessage { get; set; }
    }


    public class SubscriptionPlansResponse
    {
        public ApiMetadata Meta { get; set; }

        public List<Subscription> SubscriptionPlans { get; set; }
    }

    public class UserStatusResponse
    {
        public ApiMetadata Meta { get; set; }

        public UserInfo UserInfo { get; set; }
    }
}