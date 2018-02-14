using Kauffman.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Kauffman.Api.Models.ApiResponse;
using Kauffman.Api.SubscriptionAssessment;

namespace Kauffman.Api.Controllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        [HttpGet]
        [Route("api/TestMethod")]
        public string TestMethod()
        {
           
            string uId = GetUserId();
            return "Hello, C# Corner Member. ";
        }



        /// <summary>
        /// Signup 
        /// </summary>
        /// <remarks>
        /// To SignUp as new user
        /// </remarks>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("signup")]
        [ResponseType(typeof(RegisterResponse))]
        public async Task<HttpResponseMessage> Register([FromBody] RegisterViewModel model)
        {
            try
            {
                Logger.Logger.Log("Register", "Start Register for User : " + model.UserEmail);

                RegisterResponse regUserResponse = new RegisterResponse();
                regUserResponse.IsUserSubscribed = false;
                regUserResponse.HasUserTakenAssesment = false;

                ApplicationDbContext context = new ApplicationDbContext();
                var user = new ApplicationUser { UserName = model.UserEmail, Email = model.UserEmail };

                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    Logger.Logger.Log("Register", "Register for User Successful: " + model.UserEmail);
                    regUserResponse.IsUserCreated = result.Succeeded;
                    regUserResponse.UserId = user.Id;
                    return Request.CreateResponse(HttpStatusCode.OK, regUserResponse);
                }
                else
                {
                    Logger.Logger.Log("Register", "Register for User Failed: " + model.UserEmail);
                    regUserResponse.IsUserCreated = result.Succeeded;
                    regUserResponse.UserId = "";
                    return Request.CreateResponse(HttpStatusCode.OK, regUserResponse);
                }
            }
            catch (ApplicationException bex)
            {
                Logger.Logger.Log("Register", "Error: Register for User Failed: " + bex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, bex.Message);
            }
            catch (Exception ex)
            {
                Logger.Logger.Log("Register", "Error: Register for User Failed: " + ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal Server Error.");
            }
        }


        /// <summary>
        /// Get User Status
        /// </summary>
        /// <remarks>
        /// To get status of existing user
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("user/UserStatus")]
        [ResponseType(typeof(UserStatus))]
        public HttpResponseMessage GetUserStatus()
        {
            try
            {
                string userId = GetUserId();

                //To check if user exists or not
                UserStatus userStatus = new UserStatus();
                userStatus.UserId = userId;
                userStatus.IsUserAcvite = true;
                DateTime subscriptionEndDate = DateTime.UtcNow;

                userStatus.IsUserSubscribed = SubscriptionManager.CheckUserSubscription(userId, out subscriptionEndDate);
                userStatus.SubscriptionEndDate = subscriptionEndDate;
                userStatus.HasUserTakenAssesment = AssessmentManager.CheckUserAssessmentStatus(userId);

                return Request.CreateResponse(HttpStatusCode.OK, userStatus);
            }
            catch (ApplicationException bex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, bex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal Server Error.");
            }
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
