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
    public class AnswersController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Answers
        public IQueryable<Answers> GetAnswers()
        {
            return db.Answers;
        }

        // GET: api/Answers/5
        [ResponseType(typeof(Answers))]
        public async Task<IHttpActionResult> GetAnswers(int id)
        {
            Answers answers = await db.Answers.FindAsync(id);
            if (answers == null)
            {
                return NotFound();
            }

            return Ok(answers);
        }

        // PUT: api/Answers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAnswers(int id, Answers answers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != answers.AnswersId)
            {
                return BadRequest();
            }

            db.Entry(answers).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswersExists(id))
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

        // POST: api/Answers
        [ResponseType(typeof(Answers))]
        public async Task<IHttpActionResult> PostAnswers(Answers answers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Answers.Add(answers);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = answers.AnswersId }, answers);
        }

        // DELETE: api/Answers/5
        [ResponseType(typeof(Answers))]
        public async Task<IHttpActionResult> DeleteAnswers(int id)
        {
            Answers answers = await db.Answers.FindAsync(id);
            if (answers == null)
            {
                return NotFound();
            }

            db.Answers.Remove(answers);
            await db.SaveChangesAsync();

            return Ok(answers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnswersExists(int id)
        {
            return db.Answers.Count(e => e.AnswersId == id) > 0;
        }
    }
}