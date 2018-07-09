using Iransabzcnc.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Iransabzcnc.WEB.DataContext
{
    public class ContactUsDbContext : DbContext
    {
        public ContactUsDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<ContactUs> ContactUses { get; set; }
    }
}