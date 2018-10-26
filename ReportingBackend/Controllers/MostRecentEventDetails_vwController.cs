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
using WebApplication4;

namespace WebApplication4.Controllers
{
    public class MostRecentEventDetails_vwController : ApiController
    {
        private MonitoringEntities db = new MonitoringEntities();

        // GET: api/MostRecentEventDetails_vw
        public IQueryable<MostRecentEventDetails_vw> GetMostRecentEventDetails_vw()
        {
            return db.MostRecentEventDetails_vw;
        }

        // GET: api/MostRecentEventDetails_vw/5
        [ResponseType(typeof(MostRecentEventDetails_vw))]
        public IHttpActionResult GetMostRecentEventDetails_vw(long id)
        {
            MostRecentEventDetails_vw mostRecentEventDetails_vw = db.MostRecentEventDetails_vw.Find(id);
            if (mostRecentEventDetails_vw == null)
            {
                return NotFound();
            }

            return Ok(mostRecentEventDetails_vw);
        }

        // PUT: api/MostRecentEventDetails_vw/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMostRecentEventDetails_vw(long id, MostRecentEventDetails_vw mostRecentEventDetails_vw)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mostRecentEventDetails_vw.EventId)
            {
                return BadRequest();
            }

            db.Entry(mostRecentEventDetails_vw).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MostRecentEventDetails_vwExists(id))
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

        // POST: api/MostRecentEventDetails_vw
        [ResponseType(typeof(MostRecentEventDetails_vw))]
        public IHttpActionResult PostMostRecentEventDetails_vw(MostRecentEventDetails_vw mostRecentEventDetails_vw)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MostRecentEventDetails_vw.Add(mostRecentEventDetails_vw);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MostRecentEventDetails_vwExists(mostRecentEventDetails_vw.EventId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = mostRecentEventDetails_vw.EventId }, mostRecentEventDetails_vw);
        }

        // DELETE: api/MostRecentEventDetails_vw/5
        [ResponseType(typeof(MostRecentEventDetails_vw))]
        public IHttpActionResult DeleteMostRecentEventDetails_vw(long id)
        {
            MostRecentEventDetails_vw mostRecentEventDetails_vw = db.MostRecentEventDetails_vw.Find(id);
            if (mostRecentEventDetails_vw == null)
            {
                return NotFound();
            }

            db.MostRecentEventDetails_vw.Remove(mostRecentEventDetails_vw);
            db.SaveChanges();

            return Ok(mostRecentEventDetails_vw);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MostRecentEventDetails_vwExists(long id)
        {
            return db.MostRecentEventDetails_vw.Count(e => e.EventId == id) > 0;
        }
    }
}