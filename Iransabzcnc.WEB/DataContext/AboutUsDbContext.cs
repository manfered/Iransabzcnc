using Iransabzcnc.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Iransabzcnc.WEB.DataContext
{
    public class AboutUsDbContext : DbContext
    {
        public AboutUsDbContext() : base("DefaultConnection")
        {
                
        }

        public DbSet<AboutUs> AboutUses { get; set; }
    }
}