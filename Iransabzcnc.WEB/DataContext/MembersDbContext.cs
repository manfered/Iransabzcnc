using Iransabzcnc.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Iransabzcnc.WEB.DataContext
{
    public class MembersDbContext : DbContext
    {
        public MembersDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<Member> Members { get; set; }
        public DbSet<MemberImage> MemberImages { get; set; }
    }
}