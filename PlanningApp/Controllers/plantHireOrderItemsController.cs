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
    public class plantHireOrderItemsController : Controller
    {
        private MBCPlanningEntities db = new MBCPlanningEntities();

        // GET: plantHireOrderItems
        public ActionResult Index()
        {
            var plantHireOrderItems = db.plantHireOrderItems.Include(p => p.plantHireOrder);
            return View(plantHireOrderItems.ToList());
        }

        // GET: plantHireOrderItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plantHireOrderItem plantHireOrderItem = db.plantHireOrderItems.Find(id);
            if (plantHireOrderItem == null)
            {
                return HttpNotFound();
            }
            return View(plantHireOrderItem);
        }

        // GET: plantHireOrderItems/Create
        public ActionResult Create()
        {
            ViewBag.orderNumberID = new SelectList(db.plantHireOrders, "orderNumberID", "title");
            return View();
        }

        // POST: plantHireOrderItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "itemOrderID,orderNumberID,item,quantity,unit,description,weeklyrate,discount,hireCost,dateRequiredOnSite,duration,offHiredNo,offHireDate,SSMA_TimeStamp")] plantHireOrderItem plantHireOrderItem)
        {
            if (ModelState.IsValid)
            {
                db.plantHireOrderItems.Add(plantHireOrderItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.orderNumberID = new SelectList(db.plantHireOrders, "orderNumberID", "title", plantHireOrderItem.orderNumberID);
            return View(plantHireOrderItem);
        }

        // GET: plantHireOrderItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plantHireOrderItem plantHireOrderItem = db.plantHireOrderItems.Find(id);
            if (plantHireOrderItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.orderNumberID = new SelectList(db.plantHireOrders, "orderNumberID", "title", plantHireOrderItem.orderNumberID);
            return View(plantHireOrderItem);
        }

        // POST: plantHireOrderItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "itemOrderID,orderNumberID,item,quantity,unit,description,weeklyrate,discount,hireCost,dateRequiredOnSite,duration,offHiredNo,offHireDate,SSMA_TimeStamp")] plantHireOrderItem plantHireOrderItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plantHireOrderItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.orderNumberID = new SelectList(db.plantHireOrders, "orderNumberID", "title", plantHireOrderItem.orderNumberID);
            return View(plantHireOrderItem);
        }

        // GET: plantHireOrderItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plantHireOrderItem plantHireOrderItem = db.plantHireOrderItems.Find(id);
            if (plantHireOrderItem == null)
            {
                return HttpNotFound();
            }
            return View(plantHireOrderItem);
        }

        // POST: plantHireOrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            plantHireOrderItem plantHireOrderItem = db.plantHireOrderItems.Find(id);
            db.plantHireOrderItems.Remove(plantHireOrderItem);
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
