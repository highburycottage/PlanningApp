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
    public class plantHireOrdersController : Controller
    {
        private MBCPlanningEntities db = new MBCPlanningEntities();

        // GET: plantHireOrders1
        public ActionResult Index()
        {
            var plantHireOrders = db.plantHireOrders.Include(p => p.project);
            return View(plantHireOrders.ToList());
        }

        // GET: plantHireOrders1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plantHireOrder plantHireOrder = db.plantHireOrders.Find(id);
            if (plantHireOrder == null)
            {
                return HttpNotFound();
            }
            return View(plantHireOrder);
        }

        // GET: plantHireOrders1/Create
        public ActionResult Create()
        {
            ViewBag.projectID = new SelectList(db.projects, "projectID", "siteName");
            return View();
        }

        // POST: plantHireOrders1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "orderNumberID,projectID,vendorLocationID,item,quantity,unit,Description,weeklyRate,discount,hireCost,dateRequiredOnSite,duration,offHireNo,offHireDate,title,siteContact,originator")] plantHireOrder plantHireOrder)
        {
            if (ModelState.IsValid)
            {
                db.plantHireOrders.Add(plantHireOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.projectID = new SelectList(db.projects, "projectID", "siteName", plantHireOrder.projectID);
            return View(plantHireOrder);
        }

        // GET: plantHireOrders1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plantHireOrder plantHireOrder = db.plantHireOrders.Find(id);
            if (plantHireOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.projectID = new SelectList(db.projects, "projectID", "siteName", plantHireOrder.projectID);
            return View(plantHireOrder);
        }

        // POST: plantHireOrders1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "orderNumberID,projectID,vendorLocationID,item,quantity,unit,Description,weeklyRate,discount,hireCost,dateRequiredOnSite,duration,offHireNo,offHireDate,title,siteContact,originator")] plantHireOrder plantHireOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plantHireOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.projectID = new SelectList(db.projects, "projectID", "siteName", plantHireOrder.projectID);
            return View(plantHireOrder);
        }

        // GET: plantHireOrders1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plantHireOrder plantHireOrder = db.plantHireOrders.Find(id);
            if (plantHireOrder == null)
            {
                return HttpNotFound();
            }
            return View(plantHireOrder);
        }

        // POST: plantHireOrders1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            plantHireOrder plantHireOrder = db.plantHireOrders.Find(id);
            db.plantHireOrders.Remove(plantHireOrder);
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
