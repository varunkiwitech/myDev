using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kauffman.Api.DbContext
{
    public class OwinAuthDbContext : IdentityDbContext
    {
        public OwinAuthDbContext()
       : base("KauffmanConnectionString")
        {
        }
    }
}