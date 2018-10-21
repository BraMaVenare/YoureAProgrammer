using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YoureAProgrammer.BackEnd.Models;
using YoureAProgrammer.Common.Models;

namespace YoureAProgrammer.BackEnd.Controllers
{
    public class ReunionsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Reunions
        public async Task<ActionResult> Index()
        {
            var reunions = db.Reunions.Include(r => r.Skills);
            return View(await reunions.ToListAsync());
        }

        // GET: Reunions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reunions reunions = await db.Reunions.FindAsync(id);
            if (reunions == null)
            {
                return HttpNotFound();
            }
            return View(reunions);
        }

        // GET: Reunions/Create
        public ActionResult Create()
        {
            ViewBag.SkillId = new SelectList(db.Skills, "SkillId", "Name");
            return View();
        }

        // POST: Reunions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ReunionId,Description,Localizate,Date,SkillId")] Reunions reunions)
        {
            if (ModelState.IsValid)
            {
                db.Reunions.Add(reunions);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SkillId = new SelectList(db.Skills, "SkillId", "Name", reunions.SkillId);
            return View(reunions);
        }

        // GET: Reunions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reunions reunions = await db.Reunions.FindAsync(id);
            if (reunions == null)
            {
                return HttpNotFound();
            }
            ViewBag.SkillId = new SelectList(db.Skills, "SkillId", "Name", reunions.SkillId);
            return View(reunions);
        }

        // POST: Reunions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ReunionId,Description,Localizate,Date,SkillId")] Reunions reunions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reunions).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SkillId = new SelectList(db.Skills, "SkillId", "Name", reunions.SkillId);
            return View(reunions);
        }

        // GET: Reunions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reunions reunions = await db.Reunions.FindAsync(id);
            if (reunions == null)
            {
                return HttpNotFound();
            }
            return View(reunions);
        }

        // POST: Reunions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Reunions reunions = await db.Reunions.FindAsync(id);
            db.Reunions.Remove(reunions);
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
