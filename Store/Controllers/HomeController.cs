using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Controllers
{
    public class HomeController : Controller
    {

        private AMotorsEntities db = new AMotorsEntities();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.outOfStockProductCount = db.products.Where(x => x.StockQuantity == 0).Count();
            ViewBag.BonutCount = db.products.Where(x => x.Product_Category.name == "Bonut").Count();
            ViewBag.FenderCount = db.products.Where(x => x.Product_Category.name == "Fender").Count();
            ViewBag.BumperCount = db.products.Where(x => x.Product_Category.name == "Bumper").Count();
            ViewBag.GrilleCount = db.products.Where(x => x.Product_Category.name == "Grille").Count();
            ViewBag.DoorMachineCount = db.products.Where(x => x.Product_Category.name == "Door Machine").Count();
            ViewBag.LightCaseCount = db.products.Where(x => x.Product_Category.name == "Light Case").Count();
            ViewBag.BonutLockCount = db.products.Where(x => x.Product_Category.name == "Bonut Lock").Count();
            ViewBag.RadiatorSupportCount = db.products.Where(x => x.Product_Category.name == "Radiator Support").Count();
            ViewBag.LampCount = db.products.Where(x => x.Product_Category.name == "Lamp").Count();
            ViewBag.TotalProductCount = db.products.Count();
            return View();
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
