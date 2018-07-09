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
    public class ServicePhotoesController : Controller
    {
        private CompanyServiceDbContext db = new CompanyServiceDbContext();

        // GET: ServicePhotoes
        public async Task<ActionResult> Index(int id)
        {
            ViewBag.CompanyServiceID = id;
            return View(await db.ServicePhotos.Where(s => s.CompanyService.CompanyServiceID == id).ToListAsync());
        }

        // GET: ServicePhotoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePhoto servicePhoto = await db.ServicePhotos.FindAsync(id);
            if (servicePhoto == null)
            {
                return HttpNotFound();
            }
            return View(servicePhoto);
        }

        // GET: ServicePhotoes/Create
        public ActionResult Create(int id)
        {
            ViewBag.CompanyServiceID = id;
            ViewBag.ImageType = PhotoManagerType.Service;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.Service);
            return View();
        }

        // POST: ServicePhotoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ServicePhotoID,FileName,Title")] ServicePhoto servicePhoto, int CompanyServiceId)
        {
            if (ModelState.IsValid)
            {
                servicePhoto.CompanyService = db.CompanyServices.Find(CompanyServiceId);

                db.ServicePhotos.Add(servicePhoto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = CompanyServiceId });
            }

            return View(servicePhoto);
        }

        // GET: ServicePhotoes/Edit/5
        public async Task<ActionResult> Edit(int? id, int CompanyServiceID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePhoto servicePhoto = await db.ServicePhotos.FindAsync(id);
            if (servicePhoto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyServiceId = CompanyServiceID;
            ViewBag.ImageType = PhotoManagerType.Service;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.Service);
            return View(servicePhoto);
        }

        // POST: ServicePhotoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ServicePhotoID,FileName,Title")] ServicePhoto servicePhoto, int CompanyServiceId)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicePhoto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = CompanyServiceId });
            }
            return View(servicePhoto);
        }

        // GET: ServicePhotoes/Delete/5
        public async Task<ActionResult> Delete(int? id, int CompanyServiceID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePhoto servicePhoto = await db.ServicePhotos.FindAsync(id);
            if (servicePhoto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyServiceId = CompanyServiceID;
            ViewBag.ImageType = PhotoManagerType.Service;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.Service);
            return View(servicePhoto);
        }

        // POST: ServicePhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, int companyServiceId, string deleteFileName)
        {
            //delete image file first
            PhotoFileManager manager = new PhotoFileManager(PhotoManagerType.Service);
            manager.Delete(deleteFileName, true);

            ServicePhoto servicePhoto = await db.ServicePhotos.FindAsync(id);
            db.ServicePhotos.Remove(servicePhoto);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", new { id = companyServiceId });
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
