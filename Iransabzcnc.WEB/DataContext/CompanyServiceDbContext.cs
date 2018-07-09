using Iransabzcnc.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Iransabzcnc.WEB.DataContext
{
    public class CompanyServiceDbContext : DbContext
    {
        public CompanyServiceDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<CompanyService> CompanyServices { get; set; }
        public DbSet<ServiceIconPhoto> ServiceIconPhotos { get; set; }
        public DbSet<ServicePhoto> ServicePhotos { get; set; }
    }
}