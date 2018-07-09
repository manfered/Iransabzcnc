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
    public class MemberImagesController : Controller
    {
        private MembersDbContext db = new MembersDbContext();

        // GET: MemberImages
        public async Task<ActionResult> Index()
        {
            var memberImages = db.MemberImages.Include(m => m.Member);
            return View(await memberImages.ToListAsync());
        }

        // GET: MemberImages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberImage memberImage = await db.MemberImages.FindAsync(id);
            if (memberImage == null)
            {
                return HttpNotFound();
            }
            return View(memberImage);
        }

        // GET: MemberImages/Create
        public ActionResult Create(int id)
        {
            //ViewBag.MemberImageId = new SelectList(db.Members, "MemberId", "Name");
            ViewBag.MemberId = id;
            ViewBag.ImageType = PhotoManagerType.Member;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.Member);
            return View();
        }

        // POST: MemberImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MemberImageId,FileName,Title")] MemberImage memberImage)
        {
            if (ModelState.IsValid)
            {
                db.MemberImages.Add(memberImage);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Members");
            }

            ViewBag.MemberImageId = new SelectList(db.Members, "MemberId", "Name", memberImage.MemberImageId);
            return View(memberImage);
        }

        // GET: MemberImages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberImage memberImage = await db.MemberImages.FindAsync(id);
            if (memberImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberImageId = new SelectList(db.Members, "MemberId", "Name", memberImage.MemberImageId);
            ViewBag.ImageType = PhotoManagerType.Member;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.Member);
            return View(memberImage);
        }

        // POST: MemberImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MemberImageId,FileName,Title")] MemberImage memberImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberImage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Members");
            }
            ViewBag.MemberImageId = new SelectList(db.Members, "MemberId", "Name", memberImage.MemberImageId);
            return View(memberImage);
        }

        // GET: MemberImages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberImage memberImage = await db.MemberImages.FindAsync(id);
            if (memberImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.ImageType = PhotoManagerType.Member;
            ViewBag.ImageDirectory = PhotoFileManager.StorageDirectoryDetection(PhotoManagerType.Member);
            return View(memberImage);
        }

        // POST: MemberImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, string deleteFileName)
        {
            //delete image file first
            PhotoFileManager manager = new PhotoFileManager(PhotoManagerType.Member);
            manager.Delete(deleteFileName, false);

            MemberImage memberImage = await db.MemberImages.FindAsync(id);
            db.MemberImages.Remove(memberImage);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Members");
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
