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
    public class ReunionsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Reunions
        public IQueryable<Reunions> GetReunions()
        {
            return db.Reunions;
        }

        // GET: api/Reunions/5
        [ResponseType(typeof(Reunions))]
        public async Task<IHttpActionResult> GetReunions(int id)
        {
            Reunions reunions = await db.Reunions.FindAsync(id);
            if (reunions == null)
            {
                return NotFound();
            }

            return Ok(reunions);
        }

        // PUT: api/Reunions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutReunions(int id, Reunions reunions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reunions.ReunionId)
            {
                return BadRequest();
            }

            db.Entry(reunions).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReunionsExists(id))
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

        // POST: api/Reunions
        [ResponseType(typeof(Reunions))]
        public async Task<IHttpActionResult> PostReunions(Reunions reunions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reunions.Add(reunions);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = reunions.ReunionId }, reunions);
        }

        // DELETE: api/Reunions/5
        [ResponseType(typeof(Reunions))]
        public async Task<IHttpActionResult> DeleteReunions(int id)
        {
            Reunions reunions = await db.Reunions.FindAsync(id);
            if (reunions == null)
            {
                return NotFound();
            }

            db.Reunions.Remove(reunions);
            await db.SaveChangesAsync();

            return Ok(reunions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReunionsExists(int id)
        {
            return db.Reunions.Count(e => e.ReunionId == id) > 0;
        }
    }
}