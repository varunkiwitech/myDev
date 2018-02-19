using Kauffman.Api.SubscriptionAssessment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Kauffman.Api.SubscriptionAssessment.Models;
using System.Web.Http.Description;
using Kauffman.Api.Models.ApiResponse;

namespace Kauffman.Api.Controllers
{
    [Authorize]
    public class SubscriptionController : ApiController
    {
        SubscriptionManager manager = new SubscriptionManager();
        /// <summary>
        /// Get User Status
        /// </summary>
        /// <remarks>
        /// To get status of existing user
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("subscriptions")]
        [ResponseType(typeof(SubscriptionPlansResponse))]
        public HttpResponseMessage GetSubscriptionPlans()
        {
            SubscriptionPlansResponse res = new SubscriptionPlansResponse();
            res.Meta = new ApiMetadata();
            res.SubscriptionPlans = null;
            try
            {
                res.Meta.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
                var plans = manager.GetSubscriptionPlans();
                if (plans == null || plans.Count == 0)
                {
                    res.Meta.StatusMessage = "No Subscription Plans";
                }
                else
                {
                    res.SubscriptionPlans = new List<Subscription>();
                    res.SubscriptionPlans = plans;
                    res.Meta.StatusMessage = plans.Count.ToString() + " Subscription Plans Found";
                }
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (ApplicationException bex)
            {
                Logger.Logger.Log("API - GetSubscriptionPlans()", bex.Message);
                res.Meta.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
                res.Meta.StatusMessage = "Bad Request";
                return Request.CreateResponse(HttpStatusCode.BadRequest, res);
            }
            catch (Exception ex)
            {
                Logger.Logger.Log("API - GetSubscriptionPlans()", ex.Message);
                res.Meta.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
                res.Meta.StatusMessage = Resources.ApiResponse.InternalServerError;
                return Request.CreateResponse(HttpStatusCode.BadRequest, res);
            }
        }
    }
}