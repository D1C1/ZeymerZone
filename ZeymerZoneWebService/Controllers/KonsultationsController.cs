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
    public class KonsultationsController : ApiController
    {
        private ZZDBContext db = new ZZDBContext();

        // GET: api/Konsultations
        public IQueryable<Konsultation> GetKonsultations()
        {
            return db.Konsultations;
        }

        // GET: api/Konsultations/5
        [ResponseType(typeof(Konsultation))]
        public IHttpActionResult GetKonsultation(int id)
        {
            Konsultation konsultation = db.Konsultations.Find(id);
            if (konsultation == null)
            {
                return NotFound();
            }

            return Ok(konsultation);
        }

        // PUT: api/Konsultations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKonsultation(int id, Konsultation konsultation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != konsultation.Konsultation_Id)
            {
                return BadRequest();
            }

            db.Entry(konsultation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KonsultationExists(id))
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

        // POST: api/Konsultations
        [ResponseType(typeof(Konsultation))]
        public IHttpActionResult PostKonsultation(Konsultation konsultation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Konsultations.Add(konsultation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = konsultation.Konsultation_Id }, konsultation);
        }

        // DELETE: api/Konsultations/5
        [ResponseType(typeof(Konsultation))]
        public IHttpActionResult DeleteKonsultation(int id)
        {
            Konsultation konsultation = db.Konsultations.Find(id);
            if (konsultation == null)
            {
                return NotFound();
            }

            db.Konsultations.Remove(konsultation);
            db.SaveChanges();

            return Ok(konsultation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KonsultationExists(int id)
        {
            return db.Konsultations.Count(e => e.Konsultation_Id == id) > 0;
        }
    }
}