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
    public class QuestionsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Questions
        public async Task<ActionResult> Index()
        {
            var questions = db.Questions.Include(q => q.Skills);
            return View(await questions.ToListAsync());
        }

        // GET: Questions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questions questions = await db.Questions.FindAsync(id);
            if (questions == null)
            {
                return HttpNotFound();
            }
            return View(questions);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            ViewBag.SkillId = new SelectList(db.Skills, "SkillId", "Name");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(QuestionView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Questions";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }
                var questions = this.ToQuestion(view, pic);
                db.Questions.Add(questions);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SkillId = new SelectList(db.Skills, "SkillId", "Name", view.SkillId);
            return View(view);
        }

        private Questions ToQuestion(QuestionView view, string pic)
        {
            return new Questions
            {
                QuestionID = view.QuestionID,
                Title = view.Title,
                Description = view.Description,
                ImagePath= pic,
                SkillId = view.SkillId,          
            };
        }

        // GET: Questions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questions questions = await db.Questions.FindAsync(id);
            if (questions == null)
            {
                return HttpNotFound();
            }
            var view = this.toView(questions);
            ViewBag.SkillId = new SelectList(db.Skills, "SkillId", "Name", view.SkillId);
            return View(view);
        }

        private QuestionView toView(Questions questions)
        {
            return new QuestionView
            {
                QuestionID = questions.QuestionID,
                Title = questions.Title,
                Description = questions.Description,
                ImagePath = questions.ImagePath,
                SkillId = questions.SkillId,
            };
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(QuestionView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.ImagePath;
                var folder = "~/Content/Questions";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }
                var questions = this.ToQuestion(view, pic);
                db.Entry(questions).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SkillId = new SelectList(db.Skills, "SkillId", "Name", view.SkillId);
            return View(view);
        }

        // GET: Questions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questions questions = await db.Questions.FindAsync(id);
            if (questions == null)
            {
                return HttpNotFound();
            }
            return View(questions);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Questions questions = await db.Questions.FindAsync(id);
            db.Questions.Remove(questions);
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
