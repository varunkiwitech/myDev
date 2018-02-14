using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kauffman.Api.Models.ApiResponse
{
    public class UserStatus
    {
        public string UserId { get; set; }
        public bool IsUserAcvite { get; set; }
        public bool IsUserSubscribed { get; set; }
        public bool HasUserTakenAssesment { get; set; }
        public DateTime SubscriptionEndDate { get; set; }
    }
}