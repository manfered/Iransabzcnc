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
    public class SlidePhotoesController : Controller
    {
        private SlideDbContext db = new SlideDbContext();

        // GET: SlidePhotoes
        public async Task<ActionResult> Index()
        {
            var slidePhotos = db.SlidePhotos.Include(s => s.Slide);
            return View(await slidePhotos.ToListAsync());
        }

        // GET: SlidePhotoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlidePhoto slidePhoto = await db.SlidePhotos.FindAsync(id);
            if (slidePhoto == null)
            {
                return HttpNotFound();
            }
            return View(slidePhoto);
        }

        // GET: SlidePhotoes/Create
        public ActionResult Create(int id)
        {
            //ViewBag.SlidePhotoID = new SelectList(db.Slides, "SlideID", "Name");
            ViewBag.SlidePhotoID = id;
            ViewBag.ImageType = PhotoManagerType.Slide;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.Slide);
            return View();
        }

        // POST: SlidePhotoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SlidePhotoID,FileName,Title")] SlidePhoto slidePhoto)
        {
            if (ModelState.IsValid)
            {
                db.SlidePhotos.Add(slidePhoto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Slides");
            }

            ViewBag.SlidePhotoID = new SelectList(db.Slides, "SlideID", "Name", slidePhoto.SlidePhotoID);
            return View(slidePhoto);
        }

        // GET: SlidePhotoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlidePhoto slidePhoto = await db.SlidePhotos.FindAsync(id);
            if (slidePhoto == null)
            {
                return HttpNotFound();
            }
            ViewBag.SlidePhotoID = new SelectList(db.Slides, "SlideID", "Name", slidePhoto.SlidePhotoID);
            ViewBag.ImageType = PhotoManagerType.Slide;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.Slide);
            return View(slidePhoto);
        }

        // POST: SlidePhotoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SlidePhotoID,FileName,Title")] SlidePhoto slidePhoto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slidePhoto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Slides");
            }
            ViewBag.SlidePhotoID = new SelectList(db.Slides, "SlideID", "Name", slidePhoto.SlidePhotoID);
            return View(slidePhoto);
        }

        // GET: SlidePhotoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlidePhoto slidePhoto = await db.SlidePhotos.FindAsync(id);
            if (slidePhoto == null)
            {
                return HttpNotFound();
            }
            ViewBag.ImageType = PhotoManagerType.Slide;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.Slide);
            return View(slidePhoto);
        }

        // POST: SlidePhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, string deleteFileName)
        {
            //delete image file first
            PhotoFileManager manager = new PhotoFileManager(PhotoManagerType.Slide);
            manager.Delete(deleteFileName, false);

            SlidePhoto slidePhoto = await db.SlidePhotos.FindAsync(id);
            db.SlidePhotos.Remove(slidePhoto);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Slides");
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
