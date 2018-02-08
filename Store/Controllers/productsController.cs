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
    public class productsController : Controller
    {
        private AMotorsEntities db = new AMotorsEntities();

        // GET: products
        public ActionResult Index()
        {
            var products = db.products.Include(p => p.Product_Category).Include(p=>p.model).OrderBy(x=>x.productCategory_Id);
           
            return View(products.ToList());
        }

        // GET: products/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: products/Create
        public ActionResult Create()
        {
            ViewBag.productCategory_Id = new SelectList(db.Product_Category, "productCategory_Id", "name");
            var models = db.models.Include(m => m.car).AsEnumerable().Select(s => new { model_Id = s.model_Id, description = string.Format("{0} {1} {2} {3}", s.car.car_manufacturer.name, s.car.name, s.name, s.description) }).ToList();
            ViewBag.model_Id = new SelectList(models, "model_Id", "description");

            //var models = db.models.Include(m => m.car).AsEnumerable().Select(s => new { model_Id = s.model_Id, description = string.Format("{0} {1} ", s.car.name, s.description) }).ToList();            
            //ViewBag.model_Id = new SelectList(models, "model_Id", "description");
            return View();
        }

        // POST: products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "product_Id,productCategory_Id,name,model_Id,ShortDescription,FullDescription,Price,OldPrice,SpecialPrice,StockQuantity")] product product)
        public ActionResult Create(product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productCategory_Id = new SelectList(db.Product_Category, "productCategory_Id", "name", product.productCategory_Id);
            ViewBag.model_Id = new SelectList(db.models, "model_Id", "name", product.model_Id);


            
            return View(product);
        }


        
        // GET: products/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.productCategory_Id = new SelectList(db.Product_Category, "productCategory_Id", "name", product.productCategory_Id);
            var models = db.models.Include(m => m.car).AsEnumerable().Select(s => new { model_Id = s.model_Id, description = string.Format("{0} {1} {2} {3}", s.car.car_manufacturer.name, s.car.name, s.name, s.description) }).ToList();
            //ViewBag.model_Id = new SelectList(db.models.Include(m => m.car).AsEnumerable().Select(s => new { model_Id = s.model_Id, description = string.Format("{0} {1} {2} {3}", s.car.car_manufacturer.name, s.car.name, s.name, s.description) }).ToList(), "model_Id", "description");

            ViewBag.model_Id = new SelectList(models, "model_Id", "description",product.model_Id);


            return View(product);
        }

        // POST: products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_Id,productCategory_Id,name,model_Id,ShortDescription,FullDescription,OldPrice,Price,SpecialPrice,StockQuantity,StockShopQTY")] product product)
        {
            if (ModelState.IsValid)
            {
                var productCurrent = db.products.AsNoTracking().Where(x => x.product_Id == product.product_Id).FirstOrDefault();//.Find(product.product_Id);
                product.OldPrice = productCurrent.Price;

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productCategory_Id = new SelectList(db.Product_Category, "productCategory_Id", "name", product.productCategory_Id);
            ViewBag.model_Id = new SelectList(db.models, "model_Id", "name", product.model_Id);
            return View(product);
        }

        // GET: products/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
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
