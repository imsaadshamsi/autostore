using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Store;

namespace Store.Controllers
{
    public class car_manufacturerController : Controller
    {
        private AMotorsEntities db = new AMotorsEntities();

        // GET: car_manufacturer
        public ActionResult Index()
        {
            return View(db.car_manufacturer.ToList());
        }

        // GET: car_manufacturer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            car_manufacturer car_manufacturer = db.car_manufacturer.Find(id);
            if (car_manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(car_manufacturer);
        }

        // GET: car_manufacturer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: car_manufacturer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cm_Id,name")] car_manufacturer car_manufacturer)
        {
            if (ModelState.IsValid)
            {
                db.car_manufacturer.Add(car_manufacturer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(car_manufacturer);
        }

        // GET: car_manufacturer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            car_manufacturer car_manufacturer = db.car_manufacturer.Find(id);
            if (car_manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(car_manufacturer);
        }

        // POST: car_manufacturer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cm_Id,name")] car_manufacturer car_manufacturer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car_manufacturer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car_manufacturer);
        }

        // GET: car_manufacturer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            car_manufacturer car_manufacturer = db.car_manufacturer.Find(id);
            if (car_manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(car_manufacturer);
        }

        // POST: car_manufacturer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            car_manufacturer car_manufacturer = db.car_manufacturer.Find(id);
            db.car_manufacturer.Remove(car_manufacturer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
