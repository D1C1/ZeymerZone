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

namespace ZeymerZoneWebService.Controllers
{
    public class KalendersController : ApiController
    {
        private ZZDBContext db = new ZZDBContext();

        // GET: api/Kalenders
        public IQueryable<Kalender> GetKalenders()
        {
            return db.Kalenders;
        }

        // GET: api/Kalenders/5
        [ResponseType(typeof(Kalender))]
        public IHttpActionResult GetKalender(int id)
        {
            Kalender kalender = db.Kalenders.Find(id);
            if (kalender == null)
            {
                return NotFound();
            }

            return Ok(kalender);
        }

        // PUT: api/Kalenders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKalender(int id, Kalender kalender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kalender.Kalender_Id)
            {
                return BadRequest();
            }

            db.Entry(kalender).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KalenderExists(id))
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

        // POST: api/Kalenders
        [ResponseType(typeof(Kalender))]
        public IHttpActionResult PostKalender(Kalender kalender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Kalenders.Add(kalender);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kalender.Kalender_Id }, kalender);
        }

        // DELETE: api/Kalenders/5
        [ResponseType(typeof(Kalender))]
        public IHttpActionResult DeleteKalender(int id)
        {
            Kalender kalender = db.Kalenders.Find(id);
            if (kalender == null)
            {
                return NotFound();
            }

            db.Kalenders.Remove(kalender);
            db.SaveChanges();

            return Ok(kalender);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KalenderExists(int id)
        {
            return db.Kalenders.Count(e => e.Kalender_Id == id) > 0;
        }
    }
}