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
        [ResponseType(typeof(UserStatusResponse))]
        public async Task<HttpResponseMessage> Register([FromBody] RegisterViewModel userSignUpData)
        {
            UserStatusResponse res = new UserStatusResponse();
            res.Meta = new ApiMetadata();
            res.UserInfo = null;
            try
            {
                if (string.IsNullOrEmpty(userSignUpData.FullName) || string.IsNullOrEmpty(userSignUpData.Password) || string.IsNullOrEmpty(userSignUpData.UserEmail))
                {
                    Logger.Logger.Log("API - Register()", Logger.Resources.LogMessages.InvalidSignUpInfo);
                    res.Meta.StatusCode = Convert.ToInt32(HttpStatusCode.PreconditionFailed);
                    res.Meta.StatusMessage = Resources.ApiResponse.InvalidSignUpInfo;
                    return Request.CreateResponse(HttpStatusCode.PreconditionFailed, res);
                }

                bool isEmailValid = Validator.IsEmailValid(userSignUpData.UserEmail);

                if (!isEmailValid)
                {
                    Logger.Logger.Log("API - Register()", Logger.Resources.LogMessages.IncorrectEmail);
                    res.Meta.StatusCode = Convert.ToInt32(HttpStatusCode.PreconditionFailed);
                    res.Meta.StatusMessage = Resources.ApiResponse.IncorrectEmail;
                    return Request.CreateResponse(HttpStatusCode.PreconditionFailed, res);
                }

                UserInfo regUserResponse = await manager.RegisterUser(userSignUpData.UserEmail, userSignUpData.Password, userSignUpData.FullName);

                if (regUserResponse != null)
                    res.UserInfo = regUserResponse;

                res.Meta.StatusMessage = "Sucess";

                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (ApplicationException bex)
            {
                Logger.Logger.Log("API - Register()", bex.Message);
                res.Meta.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
                res.Meta.StatusMessage = "Bad Request";
                return Request.CreateResponse(HttpStatusCode.BadRequest, res);
            }
            catch (Exception ex)
            {
                Logger.Logger.Log("API - Register()", ex.Message);
                res.Meta.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
                res.Meta.StatusMessage = Resources.ApiResponse.InternalServerError;
                return Request.CreateResponse(HttpStatusCode.BadRequest, res);
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
        [Route("user/userstatus")]
        [ResponseType(typeof(UserStatusResponse))]
        public HttpResponseMessage GetUserInfo()
        {
            UserStatusResponse res = new UserStatusResponse();
            res.Meta = new ApiMetadata();
            res.UserInfo = null;

            try
            {
                UserInfo userInfo = manager.GetUserStatus();

                if (userInfo != null)
                    res.UserInfo = userInfo;

                res.Meta.StatusMessage = "Sucess";

                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (ApplicationException bex)
            {
                Logger.Logger.Log("API - GetUserInfo()", bex.Message);
                res.Meta.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
                res.Meta.StatusMessage = Resources.ApiResponse.FethcUserInfoFailed;
                return Request.CreateResponse(HttpStatusCode.BadRequest, res);
            }
            catch (Exception ex)
            {
                Logger.Logger.Log("API - GetUserInfo()", ex.Message);
                res.Meta.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
                res.Meta.StatusMessage = Resources.ApiResponse.InternalServerError;
                return Request.CreateResponse(HttpStatusCode.BadRequest, res);
            }
        }

     

    }
}
