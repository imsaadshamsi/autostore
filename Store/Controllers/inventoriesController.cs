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
    public class inventoriesController : Controller
    {
        private AMotorsEntities db = new AMotorsEntities();

        // GET: inventories
        public ActionResult Index()
        {
            var inventories = db.inventories.Include(i => i.item);
            return View(inventories.ToList());
        }

        // GET: inventories/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inventory inventory = db.inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // GET: inventories/Create
        public ActionResult Create()
        {
            ViewBag.item_Id = new SelectList(db.items, "item_Id", "name");
            return View();
        }

        // POST: inventories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "item_Id,quantity")] inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.inventories.Add(inventory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.item_Id = new SelectList(db.items, "item_Id", "name", inventory.item_Id);
            return View(inventory);
        }

        // GET: inventories/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inventory inventory = db.inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            ViewBag.item_Id = new SelectList(db.items, "item_Id", "name", inventory.item_Id);
            return View(inventory);
        }

        // POST: inventories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "item_Id,quantity")] inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.item_Id = new SelectList(db.items, "item_Id", "name", inventory.item_Id);
            return View(inventory);
        }

        // GET: inventories/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inventory inventory = db.inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            inventory inventory = db.inventories.Find(id);
            db.inventories.Remove(inventory);
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
