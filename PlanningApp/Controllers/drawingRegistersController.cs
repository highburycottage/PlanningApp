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
    public class drawingRegistersController : Controller
    {
        private MBCPlanningEntities db = new MBCPlanningEntities();

        // GET: drawingRegisters
        public ActionResult Index()
        {
            var drawingRegisters = db.drawingRegisters.Include(d => d.project);
            return View(drawingRegisters.ToList());
        }

        // GET: drawingRegisters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            drawingRegister drawingRegister = db.drawingRegisters.Find(id);
            if (drawingRegister == null)
            {
                return HttpNotFound();
            }
            return View(drawingRegister);
        }

        // GET: drawingRegisters/Create
        public ActionResult Create()
        {
            ViewBag.projectID = new SelectList(db.projects, "projectID", "projectID");
            return View();
        }

        // POST: drawingRegisters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "drawingNumber,description,revisionID,issued,comments,printSize,drawingValid,originatorID,SSMA_TimeStamp,projectID")] drawingRegister drawingRegister)
        {
            if (ModelState.IsValid)
            {
                db.drawingRegisters.Add(drawingRegister);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.projectID = new SelectList(db.projects, "projectID", "siteName", drawingRegister.projectID);
            return View(drawingRegister);
        }

        // GET: drawingRegisters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            drawingRegister drawingRegister = db.drawingRegisters.Find(id);
            if (drawingRegister == null)
            {
                return HttpNotFound();
            }
            ViewBag.projectID = new SelectList(db.projects, "projectID", "siteName", drawingRegister.projectID);
            return View(drawingRegister);
        }

        // POST: drawingRegisters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "drawingNumber,description,revisionID,issued,comments,printSize,drawingValid,originatorID,SSMA_TimeStamp,projectID")] drawingRegister drawingRegister)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drawingRegister).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.projectID = new SelectList(db.projects, "projectID", "siteName", drawingRegister.projectID);
            return View(drawingRegister);
        }

        // GET: drawingRegisters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            drawingRegister drawingRegister = db.drawingRegisters.Find(id);
            if (drawingRegister == null)
            {
                return HttpNotFound();
            }
            return View(drawingRegister);
        }

        // POST: drawingRegisters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            drawingRegister drawingRegister = db.drawingRegisters.Find(id);
            db.drawingRegisters.Remove(drawingRegister);
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
