using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlanningApp.Models;
using PlanningApp.ViewModel;

namespace PlanningApp.Controllers
{
    public class projectsController : Controller
    {
        private MBCPlanningEntities db = new MBCPlanningEntities();

        // GET: projects
        public ActionResult Index()
        {
            return View(db.projects.ToList());
        }

        // GET: projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            project project = db.projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "projectID,siteName,siteAddress1,siteAddress2,siteAddress3,sitePostCode,deliveryRestrictions,SSMA_TimeStamp")] project project)
        {
            if (ModelState.IsValid)
            {
                db.projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var staffViewModel = new StaffViewModel
            {
                project = db.projects.Include(i => i.constructionStaffs).First(i => i.projectID == id),
            };

            if (staffViewModel.project == null)
                return HttpNotFound();

            var allProjectStaffList = db.constructionStaffs.ToList();
            staffViewModel.AllProjectStaff = allProjectStaffList.Select(o => new SelectListItem
            {
                Text = o.userName,
                Value = o.staffID.ToString()
            });

            ViewBag.projectID = new SelectList(db.projects, "staffID", "FullName", staffViewModel.project.projectID);
            return View(staffViewModel);
        }

        // POST: projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "projectID,siteName,siteAddress1,siteAddress2,siteAddress3,sitePostCode,deliveryRestrictions,SSMA_TimeStamp")] project project)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(project).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(project);
        //}
        public ActionResult Edit(StaffViewModel staffViewModel)
        {

            if (staffViewModel == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                var jobToUpdate = db.projects
                    .Include(i => i.constructionStaffs).First(i => i.projectID == staffViewModel.project.projectID);

                if (TryUpdateModel(jobToUpdate, "Project", new string[] { "siteName", "projectID" }))
                {
                    var newJobTags = db.constructionStaffs.Where(
                        m => staffViewModel.SelectedConstructionStaff.Contains(m.staffID)).ToList();
                    var updatedJobTags = new HashSet<int>(staffViewModel.SelectedConstructionStaff);
                    foreach (constructionStaff jobTag in db.constructionStaffs)
                    {
                        if (!updatedJobTags.Contains(jobTag.staffID))
                        {
                            jobToUpdate.constructionStaffs.Remove(jobTag);
                        }
                        else
                        {
                            jobToUpdate.constructionStaffs.Add((jobTag));
                        }
                    }

                    db.Entry(jobToUpdate).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            ViewBag.ProjectID = new SelectList(db.projects, "projectID", "siteName", staffViewModel.project.projectID);
            return View(staffViewModel);
        }




        // GET: projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            project project = db.projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            project project = db.projects.Find(id);
            db.projects.Remove(project);
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
