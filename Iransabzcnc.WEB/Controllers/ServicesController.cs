using Iransabzcnc.WEB.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Iransabzcnc.WEB.Controllers
{
    public class ServicesController : Controller
    {
        private CompanyServiceDbContext db = new CompanyServiceDbContext();

        // GET: Services
        public async Task<ActionResult> Details(int id)
        {
            ViewBag.serviceId = id;
            var model = await db.CompanyServices.FindAsync(id);
            return View(model);
        }
    }
}