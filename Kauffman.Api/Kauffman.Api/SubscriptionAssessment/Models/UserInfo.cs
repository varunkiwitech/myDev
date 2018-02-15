using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kauffman.Api.SubscriptionAssessment.Models
{
    public class UserInfo
    {
        public bool IsUserActive { get; set; }
        public string UserId { get; set; }
        public bool IsUserSubscribed { get; set; }

        public bool HasUserTakenAssesment { get; set; }

        public string Message { get; set; }

        public DateTime? SubscriptionEndDate { get; set; }

    }
}