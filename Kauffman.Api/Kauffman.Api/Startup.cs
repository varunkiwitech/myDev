﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Kauffman.Api.Models;
using Kauffman.Api.SubscriptionAssessment;
//using Kauffman.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(Kauffman.Api.Startup))]
namespace Kauffman.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
            //createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            if (!roleManager.RoleExists("SuperAdmin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "SuperAdmin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = System.Configuration.ConfigurationManager.AppSettings.Get("AdminUserName");//"support@p3datasys.com";
                user.Email = System.Configuration.ConfigurationManager.AppSettings.Get("AdminUserName");//"support@p3datasys.com";
                user.FullName = "Varun Jain";
                user.EmailConfirmed = true;

                string userPWD = System.Configuration.ConfigurationManager.AppSettings.Get("AdminuserPWD");// "Chr!%T2016";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "SuperAdmin");

                }
            }

            if (!roleManager.RoleExists("Users"))
            {
                var role = new IdentityRole();
                role.Name = "Users";
                roleManager.Create(role);

            }
        }
        private void ConfigureOAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<ApplicationDbContext>(() => new ApplicationDbContext());
            app.CreatePerOwinContext<UserManager<IdentityUser>>(CreateManager);

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/login"),
                Provider = new AuthorizationServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(10000),
                AllowInsecureHttp = true,

            });
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
        {
            public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
            {
                context.Validated();
            }

            #region[CreateProperties]
            public static AuthenticationProperties CreateProperties(string userName, DateTime subscriptionEndDate, bool HasUserTakenAssesment = false, bool IsUserSubscribed = false)
            {
                IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName },
                //{ "HasUserTakenAssesment", HasUserTakenAssesment.ToString()},
                //{ "SubscriptionEndDate", subscriptionEndDate.ToString("yyyyMMdd hh:mm:ss")},
                //{ "IsUserSubscribed", IsUserSubscribed.ToString()}

                //subscriptionEndDate
            };
                return new AuthenticationProperties(data);
            }
            #endregion

            #region[TokenEndpoint]
            public override Task TokenEndpoint(OAuthTokenEndpointContext context)
            {
                foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
                {
                    context.AdditionalResponseParameters.Add(property.Key, property.Value);
                }

                return Task.FromResult<object>(null);
            }
            #endregion

            public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
            {
                UserManager<IdentityUser> userManager = context.OwinContext.GetUserManager<UserManager<IdentityUser>>();
                IdentityUser user;
                try
                {
                    Logger.Logger.Log("Login-GrantResourceOwnerCredentials()", "Login for User : " + context.UserName);
                    user = await userManager.FindAsync(context.UserName, context.Password);

                }
                catch(Exception ex)
                {
                    // Could not retrieve the user due to error.
                    Logger.Logger.Log("Login-GrantResourceOwnerCredentials()", "Error: Login for User Failed : " + context.UserName);
                    Logger.Logger.Log("Login-GrantResourceOwnerCredentials()", "Error Message : " + ex.Message);
                    context.SetError("server_error");
                    context.Rejected();
                    return;
                }
                if (user != null)
                {
                    try
                    {
                        DateTime subscriptionEndDate = DateTime.UtcNow;
                        bool isUserSubscriptionActivated = SubscriptionManager.CheckUserSubscription(user.Id,out subscriptionEndDate);
                        bool HasUserTakenAssesment = AssessmentManager.CheckUserAssessmentStatus(user.Id);

                        ClaimsIdentity identity = await userManager.CreateIdentityAsync(
                                                                user,
                                                                DefaultAuthenticationTypes.ExternalBearer);
                        //var properties = CreateProperties(user.Email,subscriptionEndDate, HasUserTakenAssesment, isUserSubscriptionActivated);

                        //var ticket = new AuthenticationTicket(identity, properties);
                        context.Validated(identity);

                        Logger.Logger.Log("Login-GrantResourceOwnerCredentials()", "Login Successful for User : " + context.UserName);
                    }
                    catch(Exception ex)
                    {
                        Logger.Logger.Log("Login-GrantResourceOwnerCredentials()", "Error: Login for User Failed : " + context.UserName);
                        Logger.Logger.Log("Login-GrantResourceOwnerCredentials()", "Error Message : " + ex.Message);

                        context.SetError("server_error");
                        context.Rejected();
                        return;
                    }
                }
                else
                {
                    Logger.Logger.Log("Login-GrantResourceOwnerCredentials()", "Error:Invalid Login for User : " + context.UserName);
                    context.SetError("invalid_grant", "Invalid UserId or password'");
                    context.Rejected();
                }
            }
        }
        private static UserManager<IdentityUser> CreateManager(IdentityFactoryOptions<UserManager<IdentityUser>> options, IOwinContext context)
        {
            var userStore = new UserStore<IdentityUser>(context.Get<ApplicationDbContext>());
            var owinManager = new UserManager<IdentityUser>(userStore);
            return owinManager;
        }
    }
}