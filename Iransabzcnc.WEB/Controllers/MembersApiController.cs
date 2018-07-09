using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Iransabzcnc.Entities;
using Iransabzcnc.WEB.DataContext;

namespace Iransabzcnc.WEB.Controllers
{
    public class MembersApiController : ApiController
    {
        private MembersDbContext db = new MembersDbContext();

        public MembersApiController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/MembersApi
        public IQueryable<Member> GetMembers()
        {
            return db.Members.Include(m => m.MemberImage);
        }

        // GET: api/MembersApi/5
        [ResponseType(typeof(Member))]
        public async Task<IHttpActionResult> GetMember(int id)
        {
            Member member = await db.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            return Ok(member);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MemberExists(int id)
        {
            return db.Members.Count(e => e.MemberId == id) > 0;
        }
    }
}