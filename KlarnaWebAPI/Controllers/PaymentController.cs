using KlarnaWebAPI.Config;
using KlarnaWebAPI.Errors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace KlarnaWebAPI.Controllers
{
    public class PaymentController : ApiController
    {
        private Lafiesta50Entities db = Lafiesta50Entities.Create(DbConnection.BuildConnectionString());

        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePayment(string id)
        {
            if (!CustomerExists(id))
            {                
                return NotFound();
            }
            else
            {                
                Klarna_Order order = db.Klarna_Orders.SingleOrDefault(o => o.KlarnaID == id);                
                if (order == null)
                {
                    return NotFound();
                }
                else
                {
                    order.Status_Payment = 1;                    
                    order.Status_Payment_Text = Constants.PAYMENT_COMPLETED + " " + DateTime.Now;
                    db.Entry(order).State = EntityState.Modified;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        LogHandler.LogException(e);
                        return NotFound();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CustomerExists(id))
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
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(string id)
        {
            try
            {
                return db.Klarna_Orders.Count(e => e.KlarnaID == id) > 0;
            } catch (EntityException e) {
                LogHandler.LogException("EntityException: " + e.Message);
                return false;
            }
        }

    }
        
}
