using Iransabzcnc.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Iransabzcnc.WEB.DataContext
{
    public class SlideDbContext : DbContext
    {
        public SlideDbContext():base("DefaultConnection")
        {

        }

        public DbSet<Slide> Slides { get; set; }
        public DbSet<SlidePhoto> SlidePhotos { get; set; }
    }
}