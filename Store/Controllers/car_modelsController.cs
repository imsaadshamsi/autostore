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
    public class car_modelsController : Controller
    {
        private AMotorsEntities db = new AMotorsEntities();

        // GET: car_models
        public ActionResult Index()
        {
            var models = db.models.Include(m => m.car).OrderBy(k=>k.car.name);
            return View(models.ToList());
        }

        // GET: car_models/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model model = db.models.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: car_models/Create
        public ActionResult Create()
        {
            ViewBag.car_Id = new SelectList(db.cars, "car_Id", "name");//db.cars, "car_Id", "name");
            return View();
        }

        // POST: car_models/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "model_Id,name,description,car_Id")] model model)
        {
            if (ModelState.IsValid)
            {
                db.models.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.car_Id = new SelectList(db.cars, "car_Id", "name", model.car_Id);
            return View(model);
        }

        // GET: car_models/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model model = db.models.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.car_Id = new SelectList(db.cars, "car_Id", "name", model.car_Id);
            return View(model);
        }

        // POST: car_models/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "model_Id,name,description,car_Id")] model model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.car_Id = new SelectList(db.cars, "car_Id", "name", model.car_Id);
            return View(model);
        }

        // GET: car_models/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model model = db.models.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: car_models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            model model = db.models.Find(id);
            db.models.Remove(model);
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
