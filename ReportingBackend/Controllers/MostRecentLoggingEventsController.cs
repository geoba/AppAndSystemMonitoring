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
    public class MostRecentLoggingEventsController : ApiController
    {
        private MonitoringEntities db = new MonitoringEntities();

        // GET: api/MostRecentLoggingEvents
        public IQueryable<MostRecentLoggingEvent> GetMostRecentLoggingEvents()
        {
            return db.MostRecentLoggingEvents;
        }

        // GET: api/MostRecentLoggingEvents/5
        [ResponseType(typeof(MostRecentLoggingEvent))]
        public IHttpActionResult GetMostRecentLoggingEvent(long id)
        {
            MostRecentLoggingEvent mostRecentLoggingEvent = db.MostRecentLoggingEvents.Find(id);
            if (mostRecentLoggingEvent == null)
            {
                return NotFound();
            }

            return Ok(mostRecentLoggingEvent);
        }

        // PUT: api/MostRecentLoggingEvents/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMostRecentLoggingEvent(long id, MostRecentLoggingEvent mostRecentLoggingEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mostRecentLoggingEvent.ID)
            {
                return BadRequest();
            }

            db.Entry(mostRecentLoggingEvent).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MostRecentLoggingEventExists(id))
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

        // POST: api/MostRecentLoggingEvents
        [ResponseType(typeof(MostRecentLoggingEvent))]
        public IHttpActionResult PostMostRecentLoggingEvent(MostRecentLoggingEvent mostRecentLoggingEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MostRecentLoggingEvents.Add(mostRecentLoggingEvent);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MostRecentLoggingEventExists(mostRecentLoggingEvent.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = mostRecentLoggingEvent.ID }, mostRecentLoggingEvent);
        }

        // DELETE: api/MostRecentLoggingEvents/5
        [ResponseType(typeof(MostRecentLoggingEvent))]
        public IHttpActionResult DeleteMostRecentLoggingEvent(long id)
        {
            MostRecentLoggingEvent mostRecentLoggingEvent = db.MostRecentLoggingEvents.Find(id);
            if (mostRecentLoggingEvent == null)
            {
                return NotFound();
            }

            db.MostRecentLoggingEvents.Remove(mostRecentLoggingEvent);
            db.SaveChanges();

            return Ok(mostRecentLoggingEvent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MostRecentLoggingEventExists(long id)
        {
            return db.MostRecentLoggingEvents.Count(e => e.ID == id) > 0;
        }
    }
}