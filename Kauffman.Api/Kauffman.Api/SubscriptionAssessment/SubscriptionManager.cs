using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kauffman.Api.SubscriptionAssessment
{
    public class SubscriptionManager
    {
        static ApplicationDbContext db = new ApplicationDbContext();


        public static bool CheckUserSubscription(string userId, out DateTime subscriptionEndDate)
        {
            subscriptionEndDate = DateTime.Now;
            //Insert Code for new subscription
            return false;
        }

        public static bool IsUserSubscriptionExist(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return false;

            return false;
        }

    }
}