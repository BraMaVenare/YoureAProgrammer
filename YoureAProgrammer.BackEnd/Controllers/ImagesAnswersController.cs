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
    public class ImagesAnswersController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: ImagesAnswers
        public async Task<ActionResult> Index()
        {
            var imagesAnswer = db.ImagesAnswer.Include(i => i.Answers);
            return View(await imagesAnswer.ToListAsync());
        }

        // GET: ImagesAnswers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImagesAnswer imagesAnswer = await db.ImagesAnswer.FindAsync(id);
            if (imagesAnswer == null)
            {
                return HttpNotFound();
            }
            return View(imagesAnswer);
        }

        // GET: ImagesAnswers/Create
        public ActionResult Create()
        {
            ViewBag.AnswersId = new SelectList(db.Answers, "AnswersId", "Description");
            return View();
        }

        // POST: ImagesAnswers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ImageAnswerId,ImageFullPath,AnswersId")] ImagesAnswer imagesAnswer)
        {
            if (ModelState.IsValid)
            {
                db.ImagesAnswer.Add(imagesAnswer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AnswersId = new SelectList(db.Answers, "AnswersId", "Description", imagesAnswer.AnswersId);
            return View(imagesAnswer);
        }

        // GET: ImagesAnswers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImagesAnswer imagesAnswer = await db.ImagesAnswer.FindAsync(id);
            if (imagesAnswer == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnswersId = new SelectList(db.Answers, "AnswersId", "Description", imagesAnswer.AnswersId);
            return View(imagesAnswer);
        }

        // POST: ImagesAnswers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ImageAnswerId,ImageFullPath,AnswersId")] ImagesAnswer imagesAnswer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imagesAnswer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AnswersId = new SelectList(db.Answers, "AnswersId", "Description", imagesAnswer.AnswersId);
            return View(imagesAnswer);
        }

        // GET: ImagesAnswers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImagesAnswer imagesAnswer = await db.ImagesAnswer.FindAsync(id);
            if (imagesAnswer == null)
            {
                return HttpNotFound();
            }
            return View(imagesAnswer);
        }

        // POST: ImagesAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ImagesAnswer imagesAnswer = await db.ImagesAnswer.FindAsync(id);
            db.ImagesAnswer.Remove(imagesAnswer);
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
