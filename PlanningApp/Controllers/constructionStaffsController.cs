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
    public class constructionStaffsController : Controller
    {
        private MBCPlanningEntities db = new MBCPlanningEntities();

        // GET: constructionStaffs
        //public ActionResult Index()
        //{
        //    return View(db.constructionStaffs.ToList());
        //}
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                       
            if (searchString !=null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var staff = from s in db.constructionStaffs
                        select s;
            if (!string.IsNullOrEmpty(searchString))
            {
                staff = staff.Where(s => s.userName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    staff = staff.OrderByDescending(s => s.userName);
                    break;
                default:
                    staff = staff.OrderBy(s => s.userName);
                    break;
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(staff.ToPagedList(pageNumber, pageSize));
        }
        // GET: constructionStaffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            constructionStaff constructionStaff = db.constructionStaffs.Find(id);
            if (constructionStaff == null)
            {
                return HttpNotFound();
            }
            return View(constructionStaff);
        }

        // GET: constructionStaffs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: constructionStaffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "staffID,userName,emailAddress")] constructionStaff constructionStaff)
        {
            if (ModelState.IsValid)
            {
                db.constructionStaffs.Add(constructionStaff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(constructionStaff);
        }

        // GET: constructionStaffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            constructionStaff constructionStaff = db.constructionStaffs.Find(id);
            if (constructionStaff == null)
            {
                return HttpNotFound();
            }
            return View(constructionStaff);
        }

        // POST: constructionStaffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "staffID,userName,emailAddress")] constructionStaff constructionStaff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(constructionStaff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(constructionStaff);
        }

        // GET: constructionStaffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            constructionStaff constructionStaff = db.constructionStaffs.Find(id);
            if (constructionStaff == null)
            {
                return HttpNotFound();
            }
            return View(constructionStaff);
        }

        // POST: constructionStaffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            constructionStaff constructionStaff = db.constructionStaffs.Find(id);
            db.constructionStaffs.Remove(constructionStaff);
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
