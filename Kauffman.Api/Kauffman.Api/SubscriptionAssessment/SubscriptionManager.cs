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
            subscriptionEndDate = DateTime.UtcNow;

            bool isUserSubscriptionActive = false;
            var userSubscription = db.UserSubscriptions.Where(a => a.User.Id == userId).FirstOrDefault();

            if (userSubscription != null)
            {
                subscriptionEndDate = userSubscription.SubscriptionEndDate;
                DateTime dTCurrent = DateTime.UtcNow;
                int result = DateTime.Compare(dTCurrent, subscriptionEndDate);
                if (result > 0)
                    isUserSubscriptionActive = true;
            }

            return isUserSubscriptionActive;
        }


        public List<Models.Subscription> GetSubscriptionPlans()
        {
            return db.Subscription.ToList();
        }

        public static bool IsUserSubscriptionExist(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return false;

            return false;
        }

    }
}