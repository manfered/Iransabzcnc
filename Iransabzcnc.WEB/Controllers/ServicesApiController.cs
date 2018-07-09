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
    public class ServicesApiController : ApiController
    {
        private CompanyServiceDbContext db = new CompanyServiceDbContext();

        public ServicesApiController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/ServicesApi
        public IQueryable<CompanyService> GetCompanyServices()
        {
            //return db.CompanyServices.Include(s => s.ServiceIconPhoto)
            //                         .Include(s => s.ServicePhotos);

            return db.CompanyServices.Include(s => s.ServiceIconPhoto);
        }

        // GET: api/ServicesApi/5
        [ResponseType(typeof(CompanyService))]
        public async Task<IHttpActionResult> GetCompanyService(int id)
        {
            CompanyService companyService = await db.CompanyServices.Include(s => s.ServiceIconPhoto)
                                                                    .Include(s => s.ServicePhotos)
                                                                    .FirstOrDefaultAsync(e => e.CompanyServiceID == id);
            
            if (companyService == null)
            {
                return NotFound();
            }

            return Ok(companyService);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyServiceExists(int id)
        {
            return db.CompanyServices.Count(e => e.CompanyServiceID == id) > 0;
        }
    }
}