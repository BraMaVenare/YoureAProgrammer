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
using YoureAProgrammer.BackEnd.Helpers;

namespace YoureAProgrammer.BackEnd.Controllers
{
    public class AnswersController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Answers
        public async Task<ActionResult> Index()
        {
            var answers = db.Answers.Include(a => a.Questions);
            return View(await answers.ToListAsync());
        }

        // GET: Answers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answers answers = await db.Answers.FindAsync(id);
            if (answers == null)
            {
                return HttpNotFound();
            }
            return View(answers);
        }

        // GET: Answers/Create
        public ActionResult Create()
        {
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Title");
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AnswerView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Answers";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }
                var answers = this.ToAnswer(view, pic);
                db.Answers.Add(answers);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Title", view.QuestionID);
            return View(view);
        }

        private Answers ToAnswer(AnswerView view, string pic)
        {
            return new Answers
            {
                AnswersId= view.AnswersId,
                Description= view.Description,
                ImagePath = pic,
                QuestionID = view.QuestionID
            };
        }

        // GET: Answers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answers answers = await db.Answers.FindAsync(id);
            if (answers == null)
            {
                return HttpNotFound();
            }
            var view = this.toView(answers);
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Title", answers.QuestionID);
            return View(view);
        }
        private AnswerView toView(Answers answer)
        {
            return new AnswerView
            {
                AnswersId = answer.AnswersId,
                Description = answer.Description,
                ImagePath = answer.ImagePath,
                QuestionID = answer.QuestionID,
            };
        }
        // POST: Answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AnswerView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.ImagePath;
                var folder = "~/Content/Answers";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }
                var answers = this.ToAnswer(view, pic);
                db.Entry(answers).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Title", view.QuestionID);
            return View(view);
        }

        // GET: Answers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answers answers = await db.Answers.FindAsync(id);
            if (answers == null)
            {
                return HttpNotFound();
            }
            return View(answers);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Answers answers = await db.Answers.FindAsync(id);
            db.Answers.Remove(answers);
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
