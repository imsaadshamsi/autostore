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
    public class itemsController : Controller
    {
        private AMotorsEntities db = new AMotorsEntities();

        // GET: items
        public ActionResult Index()
        {
            var items = db.items.Include(i => i.inventory).Include(i => i.product);
            return View(items.ToList());
        }

        // GET: items/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            item item = db.items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: items/Create
        public ActionResult Create()
        {
            ViewBag.item_Id = new SelectList(db.inventories, "item_Id", "item_Id");
            ViewBag.product_Id = new SelectList(db.products, "product_Id", "name");
            return View();
        }

        // POST: items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "item_Id,product_Id,name,listPrice,unitCost,status")] item item)
        {
            if (ModelState.IsValid)
            {
                db.items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.item_Id = new SelectList(db.inventories, "item_Id", "item_Id", item.item_Id);
            ViewBag.product_Id = new SelectList(db.products, "product_Id", "name", item.product_Id);
            return View(item);
        }

        // GET: items/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            item item = db.items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.item_Id = new SelectList(db.inventories, "item_Id", "item_Id", item.item_Id);
            ViewBag.product_Id = new SelectList(db.products, "product_Id", "name", item.product_Id);
            return View(item);
        }

        // POST: items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "item_Id,product_Id,name,listPrice,unitCost,status")] item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.item_Id = new SelectList(db.inventories, "item_Id", "item_Id", item.item_Id);
            ViewBag.product_Id = new SelectList(db.products, "product_Id", "name", item.product_Id);
            return View(item);
        }

        // GET: items/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            item item = db.items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            item item = db.items.Find(id);
            db.items.Remove(item);
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
