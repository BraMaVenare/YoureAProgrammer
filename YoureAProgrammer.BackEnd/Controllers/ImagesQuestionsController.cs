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
    public class ImagesQuestionsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: ImagesQuestions
        public async Task<ActionResult> Index()
        {
            var imagesQuestion = db.ImagesQuestion.Include(i => i.Questions);
            return View(await imagesQuestion.ToListAsync());
        }

        // GET: ImagesQuestions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImagesQuestion imagesQuestion = await db.ImagesQuestion.FindAsync(id);
            if (imagesQuestion == null)
            {
                return HttpNotFound();
            }
            return View(imagesQuestion);
        }

        // GET: ImagesQuestions/Create
        public ActionResult Create()
        {
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Title");
            return View();
        }

        // POST: ImagesQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ImageQuestionID,ImageFullPath,QuestionID")] ImagesQuestion imagesQuestion)
        {
            if (ModelState.IsValid)
            {
                db.ImagesQuestion.Add(imagesQuestion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Title", imagesQuestion.QuestionID);
            return View(imagesQuestion);
        }

        // GET: ImagesQuestions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImagesQuestion imagesQuestion = await db.ImagesQuestion.FindAsync(id);
            if (imagesQuestion == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Title", imagesQuestion.QuestionID);
            return View(imagesQuestion);
        }

        // POST: ImagesQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ImageQuestionID,ImageFullPath,QuestionID")] ImagesQuestion imagesQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imagesQuestion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Title", imagesQuestion.QuestionID);
            return View(imagesQuestion);
        }

        // GET: ImagesQuestions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImagesQuestion imagesQuestion = await db.ImagesQuestion.FindAsync(id);
            if (imagesQuestion == null)
            {
                return HttpNotFound();
            }
            return View(imagesQuestion);
        }

        // POST: ImagesQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ImagesQuestion imagesQuestion = await db.ImagesQuestion.FindAsync(id);
            db.ImagesQuestion.Remove(imagesQuestion);
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
