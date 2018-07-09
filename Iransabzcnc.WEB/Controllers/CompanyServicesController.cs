using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Iransabzcnc.Entities;
using Iransabzcnc.WEB.DataContext;
using Iransabzcnc.WEB.Models;

namespace Iransabzcnc.WEB.Controllers
{
    [Authorize]
    public class CompanyServicesController : Controller
    {
        private CompanyServiceDbContext db = new CompanyServiceDbContext();

        // GET: CompanyServices
        public async Task<ActionResult> Index()
        {
            var companyServices = db.CompanyServices.Include(c => c.ServiceIconPhoto);
            return View(await companyServices.ToListAsync());
        }

        // GET: CompanyServices/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyService companyService = await db.CompanyServices.FindAsync(id);
            if (companyService == null)
            {
                return HttpNotFound();
            }
            return View(companyService);
        }

        // GET: CompanyServices/Create
        public ActionResult Create()
        {
            ViewBag.CompanyServiceID = new SelectList(db.ServiceIconPhotos, "ServiceIconPhotoId", "FileName");
            return View();
        }

        // POST: CompanyServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CompanyServiceID,Title,BriefDescription,FullDescription")] CompanyService companyService)
        {
            if (ModelState.IsValid)
            {
                db.CompanyServices.Add(companyService);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyServiceID = new SelectList(db.ServiceIconPhotos, "ServiceIconPhotoId", "FileName", companyService.CompanyServiceID);
            return View(companyService);
        }

        // GET: CompanyServices/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyService companyService = await db.CompanyServices.FindAsync(id);
            if (companyService == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyServiceID = new SelectList(db.ServiceIconPhotos, "ServiceIconPhotoId", "FileName", companyService.CompanyServiceID);
            return View(companyService);
        }

        // POST: CompanyServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CompanyServiceID,Title,BriefDescription,FullDescription")] CompanyService companyService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyService).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyServiceID = new SelectList(db.ServiceIconPhotos, "ServiceIconPhotoId", "FileName", companyService.CompanyServiceID);
            return View(companyService);
        }

        // GET: CompanyServices/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyService companyService = await db.CompanyServices.FindAsync(id);
            if (companyService == null)
            {
                return HttpNotFound();
            }
            return View(companyService);
        }

        // POST: CompanyServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyService companyService = await db.CompanyServices.FindAsync(id);

            //delete service icon with image file first if existed
            if (companyService.ServiceIconPhoto != null)
            {
                PhotoFileManager manager = new PhotoFileManager(PhotoManagerType.ServiceTitle);
                manager.Delete(companyService.ServiceIconPhoto.FileName, false);

                db.ServiceIconPhotos.Remove(companyService.ServiceIconPhoto);
            }

            //delete service images if existed

            if (companyService.ServicePhotos != null)
            {
                List<ServicePhoto> servicePhotosList = companyService.ServicePhotos.ToList();
                PhotoFileManager manager = new PhotoFileManager(PhotoManagerType.Service);
                foreach (var item in servicePhotosList)
                {
                    manager.Delete(item.FileName, true);

                    db.ServicePhotos.Remove(item);
                }
            }

            db.CompanyServices.Remove(companyService);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
