using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ZeymerZoneWebService;

namespace ZeymerZoneUWP.Controllers
{
    public class VejledersController : ApiController
    {
        private ZZDBContext db = new ZZDBContext();

        // GET: api/Vejleders
        public IQueryable<Vejleder> GetVejleders()
        {
            return db.Vejleders;
        }

        // GET: api/Vejleders/5
        [ResponseType(typeof(Vejleder))]
        public IHttpActionResult GetVejleder(int id)
        {
            Vejleder vejleder = db.Vejleders.Find(id);
            if (vejleder == null)
            {
                return NotFound();
            }

            return Ok(vejleder);
        }

        // PUT: api/Vejleders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVejleder(int id, Vejleder vejleder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vejleder.Vejleder_Id)
            {
                return BadRequest();
            }

            db.Entry(vejleder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VejlederExists(id))
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

        // POST: api/Vejleders
        [ResponseType(typeof(Vejleder))]
        public IHttpActionResult PostVejleder(Vejleder vejleder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vejleders.Add(vejleder);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vejleder.Vejleder_Id }, vejleder);
        }

        // DELETE: api/Vejleders/5
        [ResponseType(typeof(Vejleder))]
        public IHttpActionResult DeleteVejleder(int id)
        {
            Vejleder vejleder = db.Vejleders.Find(id);
            if (vejleder == null)
            {
                return NotFound();
            }

            db.Vejleders.Remove(vejleder);
            db.SaveChanges();

            return Ok(vejleder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VejlederExists(int id)
        {
            return db.Vejleders.Count(e => e.Vejleder_Id == id) > 0;
        }
    }
}