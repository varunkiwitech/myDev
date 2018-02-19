using Kauffman.Api.SubscriptionAssessment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Kauffman.Api.SubscriptionAssessment
{
    public class UserManager
    {
        static ApplicationDbContext db = new ApplicationDbContext();

        public async  Task<UserInfo> RegisterUser(string email, string password, string fullName)
        {
            var user = new ApplicationUser { UserName = email, Email = email, EmailConfirmed = true, PhoneNumberConfirmed = true, FullName = fullName };
            try
            {
                Logger.Logger.Log("UserManager-RegisterUser()", Logger.Resources.LogMessages.RegisterStart + email);

                UserInfo regUserResponse = new UserInfo();
                regUserResponse.IsUserSubscribed = false;
                regUserResponse.HasUserTakenAssesment = false;

                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var result =  await UserManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    Logger.Logger.Log("UserManager-RegisterUser()", Logger.Resources.LogMessages.RegisterationSuccess + email);
                    regUserResponse.IsUserActive = result.Succeeded;
                    regUserResponse.UserId = user.Id;
                    regUserResponse.SubscriptionEndDate = null;
                    regUserResponse.Message = Resources.ApiResponse.RegisterationSuccess;
                    return regUserResponse;
                }
                else
                {
                    Logger.Logger.Log("UserManager-RegisterUser()", Logger.Resources.LogMessages.RegistrationFailed + email);
                    regUserResponse.IsUserActive = result.Succeeded;
                    regUserResponse.UserId = "";
                    regUserResponse.SubscriptionEndDate = null;
                    regUserResponse.Message = Resources.ApiResponse.RegistrationFailed;

                    if (result.Errors != null && !string.IsNullOrEmpty(result.Errors.FirstOrDefault()))
                        regUserResponse.Message = result.Errors.FirstOrDefault();

                    return regUserResponse;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public UserInfo GetUserStatus()
        {
            UserInfo userStatus = new UserInfo();
            try
            {
                Logger.Logger.Log("UserManager-GetUserStatus()", Logger.Resources.LogMessages.MethodStart);

                string userId = GetUserId();
                string emailId = GetUserMail();

                Logger.Logger.Log("UserManager-GetUserStatus()", Logger.Resources.LogMessages.LoggednInUser + emailId);

                userStatus.UserId = userId;
                userStatus.IsUserActive = true;
                DateTime subscriptionEndDate = DateTime.UtcNow;

                userStatus.IsUserSubscribed = SubscriptionManager.CheckUserSubscription(userId, out subscriptionEndDate);
                userStatus.SubscriptionEndDate = subscriptionEndDate;

                if (!userStatus.IsUserSubscribed)
                    userStatus.SubscriptionEndDate = null;

                userStatus.HasUserTakenAssesment = AssessmentManager.CheckUserAssessmentStatus(userId);

                if (!userStatus.IsUserSubscribed)
                    userStatus.Message = "User has not purchased subscription";
                if(userStatus.IsUserSubscribed && !userStatus.HasUserTakenAssesment)
                    userStatus.Message = "User is subscribed. Need to take Assesment";
                if (userStatus.IsUserSubscribed && userStatus.HasUserTakenAssesment)
                    userStatus.Message = "Active User";
                if (!userStatus.IsUserActive)
                    userStatus.Message = "Inactive User";
            }
            catch (Exception ex)
            {
                Logger.Logger.Log("UserManager-GetUserStatus()", Logger.Resources.LogMessages.MethodException + ex.Message);
                throw;
            }
            return userStatus;
        }


        #region PrivateMethods

        private static string GetUserId()
        {
            var identity = (System.Security.Claims.ClaimsPrincipal)System.Threading.Thread.CurrentPrincipal;
            var principal = System.Threading.Thread.CurrentPrincipal as System.Security.Claims.ClaimsPrincipal;
            var userId = identity.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
            return userId;
        }
        private static string GetUserName()
        {
            var identity = (System.Security.Claims.ClaimsPrincipal)System.Threading.Thread.CurrentPrincipal;
            var principal = System.Threading.Thread.CurrentPrincipal as System.Security.Claims.ClaimsPrincipal;
            var name = identity.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            return name;
        }
        private static string GetUserMail()
        {
            var identity = (System.Security.Claims.ClaimsPrincipal)System.Threading.Thread.CurrentPrincipal;
            var principal = System.Threading.Thread.CurrentPrincipal as System.Security.Claims.ClaimsPrincipal;
            var mail = identity.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Email).Select(c => c.Value).SingleOrDefault();
            return mail;
        }

        #endregion

    }

}