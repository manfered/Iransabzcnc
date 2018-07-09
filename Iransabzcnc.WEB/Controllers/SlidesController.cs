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
    public class SlidesController : Controller
    {
        private SlideDbContext db = new SlideDbContext();

        // GET: Slides
        public async Task<ActionResult> Index()
        {
            var slides = db.Slides.Include(s => s.SlidePhoto);
            return View(await slides.ToListAsync());
        }

        // GET: Slides/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = await db.Slides.FindAsync(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // GET: Slides/Create
        public ActionResult Create()
        {
            ViewBag.SlideID = new SelectList(db.SlidePhotos, "SlidePhotoID", "FileName");
            return View();
        }

        // POST: Slides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SlideID,Name")] Slide slide)
        {
            if (ModelState.IsValid)
            {
                db.Slides.Add(slide);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SlideID = new SelectList(db.SlidePhotos, "SlidePhotoID", "FileName", slide.SlideID);
            return View(slide);
        }

        // GET: Slides/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = await db.Slides.FindAsync(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            ViewBag.SlideID = new SelectList(db.SlidePhotos, "SlidePhotoID", "FileName", slide.SlideID);
            return View(slide);
        }

        // POST: Slides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SlideID,Name")] Slide slide)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slide).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SlideID = new SelectList(db.SlidePhotos, "SlidePhotoID", "FileName", slide.SlideID);
            return View(slide);
        }

        // GET: Slides/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = await db.Slides.FindAsync(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Slides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Slide slide = await db.Slides.FindAsync(id);

            //if slide has a image we need to delete image file + slideimage from db
            if(slide.SlidePhoto != null)
            {
                //remove image file
                PhotoFileManager manager = new PhotoFileManager(PhotoManagerType.Slide);
                manager.Delete(slide.SlidePhoto.FileName, false);

                //remove from db
                db.SlidePhotos.Remove(slide.SlidePhoto);
            }

            db.Slides.Remove(slide);
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
