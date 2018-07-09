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
    public class ServiceIconPhotoesController : Controller
    {
        private CompanyServiceDbContext db = new CompanyServiceDbContext();

        // GET: ServiceIconPhotoes
        public async Task<ActionResult> Index(int id)
        {
            var serviceIconPhotos = db.ServiceIconPhotos.Include(s => s.CompanyService);
            return View(await serviceIconPhotos.ToListAsync());
        }

        // GET: ServiceIconPhotoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceIconPhoto serviceIconPhoto = await db.ServiceIconPhotos.FindAsync(id);
            if (serviceIconPhoto == null)
            {
                return HttpNotFound();
            }
            return View(serviceIconPhoto);
        }

        // GET: ServiceIconPhotoes/Create
        public ActionResult Create(int id)
        {
            //ViewBag.ServiceIconPhotoId = new SelectList(db.CompanyServices, "CompanyServiceID", "Title");
            ViewBag.CompanyServiceID = id;
            ViewBag.ImageType = PhotoManagerType.ServiceTitle;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.ServiceTitle);
            return View();
        }

        // POST: ServiceIconPhotoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ServiceIconPhotoId,FileName,Title")] ServiceIconPhoto serviceIconPhoto)
        {
            if (ModelState.IsValid)
            {
                db.ServiceIconPhotos.Add(serviceIconPhoto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index","CompanyServices");
            }

            ViewBag.ServiceIconPhotoId = new SelectList(db.CompanyServices, "CompanyServiceID", "Title", serviceIconPhoto.ServiceIconPhotoId);
            return View(serviceIconPhoto);
        }

        // GET: ServiceIconPhotoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceIconPhoto serviceIconPhoto = await db.ServiceIconPhotos.FindAsync(id);
            if (serviceIconPhoto == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceIconPhotoId = new SelectList(db.CompanyServices, "CompanyServiceID", "Title", serviceIconPhoto.ServiceIconPhotoId);
            ViewBag.ImageType = PhotoManagerType.ServiceTitle;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.ServiceTitle);
            return View(serviceIconPhoto);
        }

        // POST: ServiceIconPhotoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ServiceIconPhotoId,FileName,Title")] ServiceIconPhoto serviceIconPhoto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceIconPhoto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "CompanyServices");
            }
            ViewBag.ServiceIconPhotoId = new SelectList(db.CompanyServices, "CompanyServiceID", "Title", serviceIconPhoto.ServiceIconPhotoId);
            return View(serviceIconPhoto);
        }

        // GET: ServiceIconPhotoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceIconPhoto serviceIconPhoto = await db.ServiceIconPhotos.FindAsync(id);
            if (serviceIconPhoto == null)
            {
                return HttpNotFound();
            }
            ViewBag.ImageType = PhotoManagerType.ServiceTitle;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.ServiceTitle);
            return View(serviceIconPhoto);
        }

        // POST: ServiceIconPhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, string deleteFileName)
        {
            //delete service icon file image 
            PhotoFileManager manager = new PhotoFileManager(PhotoManagerType.ServiceTitle);
            manager.Delete(deleteFileName, false);

            ServiceIconPhoto serviceIconPhoto = await db.ServiceIconPhotos.FindAsync(id);
            db.ServiceIconPhotos.Remove(serviceIconPhoto);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "CompanyServices");
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
