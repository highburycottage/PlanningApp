using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlanningApp.Models;
using PagedList;

namespace PlanningApp.Controllers
{
    public class vendorLocationDetailsController : Controller
    {
        private MBCPlanningEntities db = new MBCPlanningEntities();

        // GET: vendorLocationDetails
        //public ActionResult Index()
        //{
        //    var vendorLocationDetails = db.vendorLocationDetails.Include(v => v.vendorHQTable);
        //    return View(vendorLocationDetails.ToList());
        //}
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString !=null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.currentFilter = searchString;

            var location = from l in db.vendorLocationDetails
                           select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                location = location.Where(l => l.vendorName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    location = location.OrderByDescending(l => l.vendorName);
                    break;
                default:
                    location = location.OrderBy(l => l.vendorName);
                    break;
                
            }
            //var vendorLocationDetails = db.vendorLocationDetails.Include(v => v.vendorHQTable);
            //return View(vendorLocationDetails.ToList());
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            //return View(location.ToList());
            return View(location.ToPagedList(pageNumber, pageSize));
        }
        // GET: vendorLocationDetails/Details/5
        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    vendorLocationDetail vendorLocationDetail = db.vendorLocationDetails.Find(id);
        //    if (vendorLocationDetail == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(vendorLocationDetail);
        //}

        public ActionResult Details(string vendorLocationID, string vendorNumber)
        {
            if (vendorLocationID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vendorLocationDetail vendorLocationDetail = db.vendorLocationDetails.Find(vendorLocationID, vendorNumber);
            if (vendorLocationDetail == null)
            {
                return HttpNotFound();
            }
            return View(vendorLocationDetail);
        }

        // GET: vendorLocationDetails/Create
        public ActionResult Create()
        {
            ViewBag.vendorNumber = new SelectList(db.vendorHQTables, "vendorID", "vendorLocationID");
            return View();
        }

        // POST: vendorLocationDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "vendorLocationID,vendorNumber,vendorName,vendorAddress,vendorAddress2,vendorCity,vendorCounty,vendorPostCode,vendorPhoneNumber,vendorContact,vendorEmail")] vendorLocationDetail vendorLocationDetail)
        {
            if (ModelState.IsValid)
            {
                db.vendorLocationDetails.Add(vendorLocationDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.vendorNumber = new SelectList(db.vendorHQTables, "vendorID", "vendorLocationID", vendorLocationDetail.vendorNumber);
            return View(vendorLocationDetail);
        }

        // GET: vendorLocationDetails/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    vendorLocationDetail vendorLocationDetail = db.vendorLocationDetails.Find(id);
        //    if (vendorLocationDetail == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.vendorNumber = new SelectList(db.vendorHQTables, "vendorID", "vendorLocationID", vendorLocationDetail.vendorNumber);
        //    return View(vendorLocationDetail);
        //}
        public ActionResult Edit(string vendorLocationID, string vendorNumber)
        {
            if (vendorLocationID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vendorLocationDetail vendorLocationDetail = db.vendorLocationDetails.Find(vendorLocationID, vendorNumber);
            if (vendorLocationDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.vendorNumber = new SelectList(db.vendorHQTables, "vendorID", "vendorLocationID", vendorLocationDetail.vendorNumber);
            return View(vendorLocationDetail);
        }
        // POST: vendorLocationDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "vendorLocationID,vendorNumber,vendorName,vendorAddress,vendorAddress2,vendorCity,vendorCounty,vendorPostCode,vendorPhoneNumber,vendorContact,vendorEmail")] vendorLocationDetail vendorLocationDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendorLocationDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.vendorNumber = new SelectList(db.vendorHQTables, "vendorID", "vendorLocationID", vendorLocationDetail.vendorNumber);
            return View(vendorLocationDetail);
        }

        // GET: vendorLocationDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vendorLocationDetail vendorLocationDetail = db.vendorLocationDetails.Find(id);
            if (vendorLocationDetail == null)
            {
                return HttpNotFound();
            }
            return View(vendorLocationDetail);
        }

        // POST: vendorLocationDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            vendorLocationDetail vendorLocationDetail = db.vendorLocationDetails.Find(id);
            db.vendorLocationDetails.Remove(vendorLocationDetail);
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
