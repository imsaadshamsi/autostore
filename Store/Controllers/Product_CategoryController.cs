using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Store;
using System.IO;

namespace Store.Controllers
{
    public class Product_CategoryController : Controller
    {
        private AMotorsEntities db = new AMotorsEntities();

        // GET: Product_Category
        public ActionResult Index()
        {
            var product_Category = db.Product_Category;//.Include(p => p.model);
            return View(product_Category.ToList());
        }

        // GET: Product_Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Category product_Category = db.Product_Category.Find(id);
            if (product_Category == null)
            {
                return HttpNotFound();
            }
            return View(product_Category);
        }

        // GET: Product_Category/Create
        public ActionResult Create()
        {
            ViewBag.model_Id = new SelectList(db.models, "model_Id", "name");
            return View();
        }

        // POST: Product_Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productCategory_Id,model_Id,name,description,ThemeImage")] Product_Category product_Category, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string filename = upload.FileName;
                string contentType = upload.ContentType;
                using (Stream fs = upload.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        product_Category.ThemeImage = bytes;
                    }
                }

                        db.Product_Category.Add(product_Category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.model_Id = new SelectList(db.models, "model_Id", "name");//, product_Category.model_Id);
            return View(product_Category);
        }

        // GET: Product_Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Category product_Category = db.Product_Category.Find(id);
            if (product_Category == null)
            {
                return HttpNotFound();
            }
            ViewBag.model_Id = new SelectList(db.models, "model_Id", "name");//, product_Category.model_Id);
            return View(product_Category);
        }

        // POST: Product_Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productCategory_Id,model_Id,name,description,")] Product_Category product_Category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");       
            }
            ViewBag.model_Id = new SelectList(db.models, "model_Id", "name");//, product_Category.model_Id);
            return View(product_Category);
        }

        // GET: Product_Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Category product_Category = db.Product_Category.Find(id);
            if (product_Category == null)
            {
                return HttpNotFound();
            }
            return View(product_Category);
        }

        // POST: Product_Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_Category product_Category = db.Product_Category.Find(id);
            db.Product_Category.Remove(product_Category);
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
