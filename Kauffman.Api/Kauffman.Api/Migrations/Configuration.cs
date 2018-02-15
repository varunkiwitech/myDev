namespace Kauffman.Api.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SubscriptionAssessment.Models;
    using Kauffman.Api.SubscriptionAssessment;

    internal sealed class Configuration : DbMigrationsConfiguration<Kauffman.Api.SubscriptionAssessment.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Kauffman.Api.SubscriptionAssessment.ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Users"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Users" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "kauffman"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "kauffman@admin.com", Email = "kauffman@admin.com", FullName = "Kauffman Admin", PhoneNumberConfirmed = true, EmailConfirmed = true };

                manager.Create(user, "password@123");
                manager.AddToRole(user.Id, "Admin");
            }

            //AddSubscriptionTypesData(context);
            AddSubscriptionPlans(context);
        }

        private void AddSubscriptionPlans(ApplicationDbContext context)
        {
            if (!context.SubscriptionTypes.Any())
            {
                context.SubscriptionTypes.AddOrUpdate(
                    new SubscriptionType { Type = "Month", DurationMonths = 1 },
                    new SubscriptionType { Type = "6 Months", DurationMonths = 6 },
                    new SubscriptionType { Type = "1 Year", DurationMonths = 12 }
                    );
            }
            context.Commit();
        }

        private void AddSubscriptionTypesData(Kauffman.Api.SubscriptionAssessment.ApplicationDbContext context)
        {
            if (!context.Subscription.Any())
            {
                context.Subscription.AddOrUpdate(
                    new Subscription { SubscriptionName = "Subscription 1", Description = "This is Subscription 1", DiscountDescription = "" , Amount = 3.77 , DiscountDuration = 0},
                    new Subscription { SubscriptionName = "Subscription 2", Description = "This is Subscription 2", DiscountDescription = "1 Month Free", Amount = 20.77, DiscountDuration = 1 },
                    new Subscription { SubscriptionName = "Subscription 3", Description = "This is Subscription 3", DiscountDescription = "2 Months Free", Amount = 37.77, DiscountDuration = 2 }
                    );
            }

            context.Commit();
        }
    }
}
