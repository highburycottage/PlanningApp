using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlanningApp.Models;

namespace PlanningApp.Controllers
{
    public class vendorHQTablesController : Controller
    {
        private MBCPlanningEntities db = new MBCPlanningEntities();

        // GET: vendorHQTables
        public ActionResult Index()
        {
            return View(db.vendorHQTables.ToList());
        }

        // GET: vendorHQTables/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vendorHQTable vendorHQTable = db.vendorHQTables.Find(id);
            if (vendorHQTable == null)
            {
                return HttpNotFound();
            }
            return View(vendorHQTable);
        }

        // GET: vendorHQTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: vendorHQTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "vendorID,vendorLocationID,vendorAddress1,vendorAddress2,vendorCity,vendorCounty,vendorPostCode,vendorPhoneNumber,vendorContact")] vendorHQTable vendorHQTable)
        {
            if (ModelState.IsValid)
            {
                db.vendorHQTables.Add(vendorHQTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vendorHQTable);
        }

        // GET: vendorHQTables/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vendorHQTable vendorHQTable = db.vendorHQTables.Find(id);
            if (vendorHQTable == null)
            {
                return HttpNotFound();
            }
            return View(vendorHQTable);
        }

        // POST: vendorHQTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "vendorID,vendorLocationID,vendorAddress1,vendorAddress2,vendorCity,vendorCounty,vendorPostCode,vendorPhoneNumber,vendorContact")] vendorHQTable vendorHQTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendorHQTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vendorHQTable);
        }

        // GET: vendorHQTables/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vendorHQTable vendorHQTable = db.vendorHQTables.Find(id);
            if (vendorHQTable == null)
            {
                return HttpNotFound();
            }
            return View(vendorHQTable);
        }

        // POST: vendorHQTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            vendorHQTable vendorHQTable = db.vendorHQTables.Find(id);
            db.vendorHQTables.Remove(vendorHQTable);
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
