using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using njmvcmusicapp.Models;

namespace njmvcmusicapp.Controllers
{
    public class SalesOrderDetailsController : Controller
    {
        private NitinDBSQLEntities db = new NitinDBSQLEntities();

        // GET: SalesOrderDetails
        public ActionResult Index()
        {
            var salesOrderDetails = db.SalesOrderDetails.Include(s => s.Product).Include(s => s.SalesOrderHeader);
            return View(salesOrderDetails.ToList());
        }

        // GET: SalesOrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrderDetail salesOrderDetail = db.SalesOrderDetails.Find(id);
            if (salesOrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(salesOrderDetail);
        }

        // GET: SalesOrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
            ViewBag.SalesOrderID = new SelectList(db.SalesOrderHeaders, "SalesOrderID", "SalesOrderNumber");
            return View();
        }

        // POST: SalesOrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalesOrderID,SalesOrderDetailID,OrderQty,ProductID,UnitPrice,UnitPriceDiscount,LineTotal,rowguid,ModifiedDate")] SalesOrderDetail salesOrderDetail)
        {
            if (ModelState.IsValid)
            {
                db.SalesOrderDetails.Add(salesOrderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", salesOrderDetail.ProductID);
            ViewBag.SalesOrderID = new SelectList(db.SalesOrderHeaders, "SalesOrderID", "SalesOrderNumber", salesOrderDetail.SalesOrderID);
            return View(salesOrderDetail);
        }

        // GET: SalesOrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrderDetail salesOrderDetail = db.SalesOrderDetails.Find(id);
            if (salesOrderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", salesOrderDetail.ProductID);
            ViewBag.SalesOrderID = new SelectList(db.SalesOrderHeaders, "SalesOrderID", "SalesOrderNumber", salesOrderDetail.SalesOrderID);
            return View(salesOrderDetail);
        }

        // POST: SalesOrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesOrderID,SalesOrderDetailID,OrderQty,ProductID,UnitPrice,UnitPriceDiscount,LineTotal,rowguid,ModifiedDate")] SalesOrderDetail salesOrderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesOrderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", salesOrderDetail.ProductID);
            ViewBag.SalesOrderID = new SelectList(db.SalesOrderHeaders, "SalesOrderID", "SalesOrderNumber", salesOrderDetail.SalesOrderID);
            return View(salesOrderDetail);
        }

        // GET: SalesOrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrderDetail salesOrderDetail = db.SalesOrderDetails.Find(id);
            if (salesOrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(salesOrderDetail);
        }

        // POST: SalesOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesOrderDetail salesOrderDetail = db.SalesOrderDetails.Find(id);
            db.SalesOrderDetails.Remove(salesOrderDetail);
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
