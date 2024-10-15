using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThekwiniAdvanaced.Models;

namespace ThekwiniAdvanaced.Controllers
{
    public class ViolationsController : Controller
    {
        private AppDBContext db = new AppDBContext();

        // GET: Violations
        public ActionResult Index()
        {
            return View(db.violations.ToList());
        }

        // GET: Violations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Violation violation = db.violations.Find(id);
            if (violation == null)
            {
                return HttpNotFound();
            }
            return View(violation);
        }

        // GET: Violations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Violations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ViolationCode,ViolationName,ViolationCost")] Violation violation)
        {
            if (ModelState.IsValid)
            {
                db.violations.Add(violation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(violation);
        }

        // GET: Violations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Violation violation = db.violations.Find(id);
            if (violation == null)
            {
                return HttpNotFound();
            }
            return View(violation);
        }

        // POST: Violations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ViolationCode,ViolationName,ViolationCost")] Violation violation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(violation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(violation);
        }

        // GET: Violations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Violation violation = db.violations.Find(id);
            if (violation == null)
            {
                return HttpNotFound();
            }
            return View(violation);
        }

        // POST: Violations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Violation violation = db.violations.Find(id);
            db.violations.Remove(violation);
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
