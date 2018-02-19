using Kauffman.Api.SubscriptionAssessment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Kauffman.Api.SubscriptionAssessment
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public string FullName { get; set; }
        //public string LastName { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("KauffmanConnectionString", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<IdentityUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");


            //modelBuilder.Entity<UserSubscription>().ToTable("UserSubscriptions");
            //modelBuilder.Entity<Subscription>().ToTable("Subscription");
            //modelBuilder.Entity<SubscriptionType>().ToTable("SubscriptionTypes");
            //modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            //modelBuilder.Entity<IdentityRole>().ToTable("Roles");

        }


        public System.Data.Entity.DbSet<UserSubscription> UserSubscriptions { get; set; }

        public System.Data.Entity.DbSet<Subscription> Subscription { get; set; }

        public System.Data.Entity.DbSet<SubscriptionType> SubscriptionTypes { get; set; }

        public System.Data.Entity.DbSet<UserAssessmentStatus> UserAssessmentStatus { get; set; }

        public System.Data.Entity.DbSet<UserAssessmentAnswer> UserAssessmentAnswers { get; set; }

        public System.Data.Entity.DbSet<AssessmentQuestion> AssessmentQuestions { get; set; }

        public System.Data.Entity.DbSet<QuestionType> QuestionTypes { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}