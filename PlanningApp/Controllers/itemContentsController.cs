using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PlanningApp.Models;

namespace PlanningApp.Controllers
{
    public class itemContentsController : Controller
    {
        private MBCPlanningEntities db = new MBCPlanningEntities();

        // GET: itemContents
        //public ActionResult Index()
        //{
        //    return View(db.itemContents.ToList());
        //}

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "description_nu" : "";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            ViewBag.CurrentFilter = searchString;


            var itemContents = from p in db.itemContents
                           select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                itemContents = itemContents.Where(p => p.description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "discription_nu":
                    itemContents = itemContents.OrderByDescending(p => p.itemCodeID);
                    break;
                default:
                    itemContents = itemContents.OrderBy(p => p.itemCodeID);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(itemContents.ToPagedList(pageNumber, pageSize));
        }

        // GET: itemContents/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            itemContent itemContent = db.itemContents.Find(id);
            if (itemContent == null)
            {
                return HttpNotFound();
            }
            return View(itemContent);
        }

        // GET: itemContents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: itemContents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "itemCodeID,description")] itemContent itemContent)
        {
            if (ModelState.IsValid)
            {
                db.itemContents.Add(itemContent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(itemContent);
        }

        // GET: itemContents/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            itemContent itemContent = db.itemContents.Find(id);
            if (itemContent == null)
            {
                return HttpNotFound();
            }
            return View(itemContent);
        }

        // POST: itemContents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "itemCodeID,description")] itemContent itemContent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemContent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(itemContent);
        }

        // GET: itemContents/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            itemContent itemContent = db.itemContents.Find(id);
            if (itemContent == null)
            {
                return HttpNotFound();
            }
            return View(itemContent);
        }

        // POST: itemContents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            itemContent itemContent = db.itemContents.Find(id);
            db.itemContents.Remove(itemContent);
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
