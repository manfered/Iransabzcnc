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

namespace Iransabzcnc.WEB.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        private MembersDbContext db = new MembersDbContext();

        // GET: Members
        public async Task<ActionResult> Index()
        {
            var members = db.Members.Include(m => m.MemberImage);
            return View(await members.ToListAsync());
        }

        // GET: Members/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = await db.Members.FindAsync(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.MemberImages, "MemberImageId", "FileName");
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MemberId,Name,JobTitle")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.MemberImages, "MemberImageId", "FileName", member.MemberId);
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = await db.Members.FindAsync(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.MemberImages, "MemberImageId", "FileName", member.MemberId);
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MemberId,Name,JobTitle")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.MemberImages, "MemberImageId", "FileName", member.MemberId);
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = await db.Members.FindAsync(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Member member = await db.Members.FindAsync(id);
            db.Members.Remove(member);
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
