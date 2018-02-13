using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Kauffman.Api.SubscriptionAssessment
{
    public class SubscriptionDContext : System.Data.Entity.DbContext
    {
        public SubscriptionDContext()
      : base("KauffmanConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    public class AssessmentDbContext : System.Data.Entity.DbContext
    {
        public AssessmentDbContext()
      : base("KauffmanConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}