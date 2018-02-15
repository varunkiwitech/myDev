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
using Kauffman.Utility;
using Kauffman.Api.SubscriptionAssessment.Models;


namespace Kauffman.Api.Controllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        UserManager manager = new UserManager();

        /// <summary>
        /// Please use [/login] as the API   
        /// </summary>
        /// <remarks>
        /// Please use [/login] as the API 
        /// </remarks>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("user/login")]
        [ResponseType(typeof(LoginResponse))]
        public string Login([FromBody] SignInViewModel userSignInData)
        {
            return "Api Url changed to [/login]";
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
        [ResponseType(typeof(UserInfo))]
        public async Task<HttpResponseMessage> Register([FromBody] RegisterViewModel userSignUpData)
        {
            try
            {
                if (string.IsNullOrEmpty(userSignUpData.FullName) || string.IsNullOrEmpty(userSignUpData.Password) || string.IsNullOrEmpty(userSignUpData.UserEmail))
                {
                    Logger.Logger.Log("API - Register()", Logger.Resources.LogMessages.InvalidSignUpInfo);
                    return Request.CreateErrorResponse(HttpStatusCode.PreconditionFailed, Resources.ApiResponse.InvalidSignUpInfo);
                }

                bool isEmailValid = Validator.IsEmailValid(userSignUpData.UserEmail);

                if (!isEmailValid)
                {
                    Logger.Logger.Log("API - Register()", Logger.Resources.LogMessages.IncorrectEmail);
                    return Request.CreateErrorResponse(HttpStatusCode.PreconditionFailed, Resources.ApiResponse.IncorrectEmail);
                }

                UserInfo regUserResponse = await manager.RegisterUser(userSignUpData.UserEmail, userSignUpData.Password, userSignUpData.FullName);

                return Request.CreateResponse(HttpStatusCode.OK, regUserResponse);
            }
            catch (ApplicationException bex)
            {
                Logger.Logger.Log("API - Register()", bex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.ApiResponse.RegistrationFailed);
            }
            catch (Exception ex)
            {
                Logger.Logger.Log("API - Register()", ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Resources.ApiResponse.InternalServerError);
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
        [ResponseType(typeof(UserInfo))]
        public HttpResponseMessage GetUserStatus()
        {
            try
            {

                //To check if user exists or not
              

                return Request.CreateResponse(HttpStatusCode.OK, "");
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

    }
}
