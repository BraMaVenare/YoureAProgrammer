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
    public class SkillsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Skills
        public IQueryable<Skills> GetSkills()
        {
            return db.Skills;
        }

        // GET: api/Skills/5
        [ResponseType(typeof(Skills))]
        public async Task<IHttpActionResult> GetSkills(int id)
        {
            Skills skills = await db.Skills.FindAsync(id);
            if (skills == null)
            {
                return NotFound();
            }

            return Ok(skills);
        }

        // PUT: api/Skills/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSkills(int id, Skills skills)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != skills.SkillId)
            {
                return BadRequest();
            }

            db.Entry(skills).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillsExists(id))
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

        // POST: api/Skills
        [ResponseType(typeof(Skills))]
        public async Task<IHttpActionResult> PostSkills(Skills skills)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Skills.Add(skills);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = skills.SkillId }, skills);
        }

        // DELETE: api/Skills/5
        [ResponseType(typeof(Skills))]
        public async Task<IHttpActionResult> DeleteSkills(int id)
        {
            Skills skills = await db.Skills.FindAsync(id);
            if (skills == null)
            {
                return NotFound();
            }

            db.Skills.Remove(skills);
            await db.SaveChangesAsync();

            return Ok(skills);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SkillsExists(int id)
        {
            return db.Skills.Count(e => e.SkillId == id) > 0;
        }
    }
}