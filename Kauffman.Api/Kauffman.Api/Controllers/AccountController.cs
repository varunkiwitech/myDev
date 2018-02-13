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

namespace Kauffman.Api.Controllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        [HttpGet]
        [Route("api/TestMethod")]
        public string TestMethod()
        {
            return "Hello, C# Corner Member. ";
        }


        [HttpGet]
        [Route("user/UserStatus")]
        public string GetUserStatus(string userId)
        {
            return "Hello, C# Corner Member. ";
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <remarks>
        /// To Register New User
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("signup")]
        [ResponseType(typeof(RegisterResponse))]
        public async Task<HttpResponseMessage> Register([FromBody] RegisterViewModel model)
        {
            try
            {
                RegisterResponse regUserResponse = new RegisterResponse();
                regUserResponse.IsUserSubscribed = false;
                regUserResponse.HasUserTakenAssesment = false;

                ApplicationDbContext context = new ApplicationDbContext();
                var user = new ApplicationUser { UserName = model.UserEmail, Email = model.UserEmail };

                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    regUserResponse.IsUserCreated = result.Succeeded;
                    regUserResponse.UserId = user.Id;
                    return Request.CreateResponse(HttpStatusCode.OK, regUserResponse);
                }
                else
                {
                    regUserResponse.IsUserCreated = result.Succeeded;
                    regUserResponse.UserId = "";
                    return Request.CreateResponse(HttpStatusCode.OK, regUserResponse);
                }
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
