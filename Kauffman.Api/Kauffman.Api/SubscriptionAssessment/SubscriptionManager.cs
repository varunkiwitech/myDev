using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kauffman.Api.SubscriptionAssessment
{
    public class SubscriptionManager
    {
        static SubscriptionDContext db = new SubscriptionDContext();


        public static bool NewUserSubscriptionStatus()
        {
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