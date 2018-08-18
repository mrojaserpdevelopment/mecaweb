using KlarnaWebAPIClient.Models;
using KlarnaWebAPIClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KlarnaWebAPIClient.Controllers
{
    public class KlarnaOrderController : Controller
    {
        KlarnaOrderClient client = new KlarnaOrderClient();

        public ActionResult Index()
        {            
            ViewBag.listKlarnaOrders = client.FindAll();
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public ActionResult Create(KlarnaOrderViewModel vm)
        {            
            client.Create(vm.klarnaOrder);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            client.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            KlarnaOrderViewModel vm = new KlarnaOrderViewModel();
            vm.klarnaOrder = client.Find(id);
            return View("Edit", vm);
        }
        [HttpPost]
        public ActionResult ProcessEdit(KlarnaOrderViewModel vm)
        {
            client.Edit(vm.klarnaOrder);
            return RedirectToAction("Index");
        }  
    }
}
