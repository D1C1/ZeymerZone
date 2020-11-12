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
    public class KostplansController : ApiController
    {
        private ZZDBContext db = new ZZDBContext();

        // GET: api/Kostplans
        public IQueryable<Kostplan> GetKostplans()
        {
            return db.Kostplans;
        }

        // GET: api/Kostplans/5
        [ResponseType(typeof(Kostplan))]
        public IHttpActionResult GetKostplan(int id)
        {
            Kostplan kostplan = db.Kostplans.Find(id);
            if (kostplan == null)
            {
                return NotFound();
            }

            return Ok(kostplan);
        }

        // PUT: api/Kostplans/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKostplan(int id, Kostplan kostplan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kostplan.Kostplan_Id)
            {
                return BadRequest();
            }

            db.Entry(kostplan).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KostplanExists(id))
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

        // POST: api/Kostplans
        [ResponseType(typeof(Kostplan))]
        public IHttpActionResult PostKostplan(Kostplan kostplan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Kostplans.Add(kostplan);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kostplan.Kostplan_Id }, kostplan);
        }

        // DELETE: api/Kostplans/5
        [ResponseType(typeof(Kostplan))]
        public IHttpActionResult DeleteKostplan(int id)
        {
            Kostplan kostplan = db.Kostplans.Find(id);
            if (kostplan == null)
            {
                return NotFound();
            }

            db.Kostplans.Remove(kostplan);
            db.SaveChanges();

            return Ok(kostplan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KostplanExists(int id)
        {
            return db.Kostplans.Count(e => e.Kostplan_Id == id) > 0;
        }
    }
}