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
    public class SlidesApiController : ApiController
    {
        private SlideDbContext db = new SlideDbContext();

        public SlidesApiController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/SlidesApi
        public IQueryable<Slide> GetSlides()
        {
            return db.Slides.Include(s => s.SlidePhoto);
        }

        // GET: api/SlidesApi/5
        [ResponseType(typeof(Slide))]
        public async Task<IHttpActionResult> GetSlide(int id)
        {
            Slide slide = await db.Slides.FindAsync(id);
            if (slide == null)
            {
                return NotFound();
            }

            return Ok(slide);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SlideExists(int id)
        {
            return db.Slides.Count(e => e.SlideID == id) > 0;
        }
    }
}