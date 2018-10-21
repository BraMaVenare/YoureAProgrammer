using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using YoureAProgrammer.Common.Models;
using YoureAProgrammer.Domain.Models;

namespace YoureAProgrammer.API.Controllers
{
    public class QuestionsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Questions
        public IQueryable<Questions> GetQuestions()
        {
            return db.Questions;
        }

        // GET: api/Questions/5
        [ResponseType(typeof(Questions))]
        public async Task<IHttpActionResult> GetQuestions(int id)
        {
            Questions questions = await db.Questions.FindAsync(id);
            if (questions == null)
            {
                return NotFound();
            }

            return Ok(questions);
        }

        // PUT: api/Questions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutQuestions(int id, Questions questions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != questions.QuestionID)
            {
                return BadRequest();
            }

            db.Entry(questions).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Questions
        [ResponseType(typeof(Questions))]
        public async Task<IHttpActionResult> PostQuestions(Questions questions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Questions.Add(questions);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = questions.QuestionID }, questions);
        }

        // DELETE: api/Questions/5
        [ResponseType(typeof(Questions))]
        public async Task<IHttpActionResult> DeleteQuestions(int id)
        {
            Questions questions = await db.Questions.FindAsync(id);
            if (questions == null)
            {
                return NotFound();
            }

            db.Questions.Remove(questions);
            await db.SaveChangesAsync();

            return Ok(questions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuestionsExists(int id)
        {
            return db.Questions.Count(e => e.QuestionID == id) > 0;
        }
    }
}