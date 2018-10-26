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
    public class TestDetailsController : ApiController
    {
        private MonitoringEntities db = new MonitoringEntities();

        // GET: api/TestDetails
        public IQueryable<TestDetail> GetTestDetails()
        {
            return db.TestDetails;
        }

        // GET: api/TestDetails/5
        [ResponseType(typeof(TestDetail))]
        public IHttpActionResult GetTestDetail(int id)
        {
            TestDetail testDetail = db.TestDetails.Find(id);
            if (testDetail == null)
            {
                return NotFound();
            }

            return Ok(testDetail);
        }

        // PUT: api/TestDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTestDetail(int id, TestDetail testDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != testDetail.ID)
            {
                return BadRequest();
            }

            db.Entry(testDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestDetailExists(id))
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

        // POST: api/TestDetails
        [ResponseType(typeof(TestDetail))]
        public IHttpActionResult PostTestDetail(TestDetail testDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TestDetails.Add(testDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = testDetail.ID }, testDetail);
        }

        // DELETE: api/TestDetails/5
        [ResponseType(typeof(TestDetail))]
        public IHttpActionResult DeleteTestDetail(int id)
        {
            TestDetail testDetail = db.TestDetails.Find(id);
            if (testDetail == null)
            {
                return NotFound();
            }

            db.TestDetails.Remove(testDetail);
            db.SaveChanges();

            return Ok(testDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TestDetailExists(int id)
        {
            return db.TestDetails.Count(e => e.ID == id) > 0;
        }
    }
}