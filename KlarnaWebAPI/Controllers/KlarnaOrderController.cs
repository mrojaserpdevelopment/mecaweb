using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using KlarnaWebAPI.Config;
using KlarnaWebAPI.Errors;


namespace KlarnaWebAPI.Controllers
{
    public class KlarnaOrderController : ApiController
    {
        private Lafiesta50Entities db = Lafiesta50Entities.Create(DbConnection.BuildConnectionString());

        // GET: api/klarnaorder  
        public IQueryable<Klarna_Order> Get()
        {
            return db.Klarna_Orders;
        }  

        // GET: api/klarnaorder/5  
        [ResponseType(typeof(Klarna_Order))]
        public IHttpActionResult Get(string id)
        {
            string orderId = id.ToString();
            Klarna_Order order = db.Klarna_Orders.SingleOrDefault(o => o.OrderNo_ == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        //PUT: api/klarnaorder/5  
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult PostOrder(string id, [FromBody] Klarna_Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!id.Equals(order.KlarnaID))
            {
                return BadRequest();
            }

            if (order.Status_Payment == 1)
            {
                order.Status_Payment_Text = order.Status_Payment_Text + " " + DateTime.Now;
            }
            db.Entry(order).State = EntityState.Modified;            

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                LogHandler.LogException(e);
                //EventLog eventLog = new EventLog("Application");
                //eventLog.Source = "Application";
                //eventLog.WriteEntry("Log message 1", EventLogEntryType.Information, 101, 1);
                //foreach (var eve in e.EntityValidationErrors)
                //{
                //    eventLog.WriteEntry("Log message 2", EventLogEntryType.Information, 101, 1);
                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        eventLog.WriteEntry("Property: " + ve.PropertyName + " - " + "Error: " + ve.ErrorMessage, EventLogEntryType.Information, 101, 1);
                //    }
                //}
                return NotFound();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(order.OrderNo_))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.OK);
        }

        // POST: api/klarnaorder  
        //[ResponseType(typeof(Klarna_Order))]
        //public IHttpActionResult PostCustomer(Klarna_Order order)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Klarna_Orders.Add(order);
        //    db.SaveChanges();

        //    //return CreatedAtRoute("DefaultApi", new { id = customer.CustomerId }, customer);
        //    return CreatedAtRoute("DefaultApi", new { id = order.OrderNo_ }, order);
        //}

        //// DELETE: api/Customers/5  
        //[ResponseType(typeof(Klarna_Order))]
        //public IHttpActionResult DeleteCustomer(int id)
        //{
        //    Klarna_Order order = db.Klarna_Orders.Find(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Klarna_Orders.Remove(order);
        //    db.SaveChanges();

        //    return Ok(order);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(string orderNo)
        {
            try {
                return db.Klarna_Orders.Count(e => e.OrderNo_ == orderNo) > 0;
            }
            catch (EntityException e)
            {
                LogHandler.LogException("EntityException: " + e.Message);
                return false;
            }
        }

    }
}
