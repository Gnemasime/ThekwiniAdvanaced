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
    public class EstatePenaltiesController : Controller
    {
        private AppDBContext db = new AppDBContext();

        // GET: EstatePenalties
        public ActionResult Index()
        {
            var estatePenalties = db.estatePenalties.Include(e => e.Area).Include(e => e.Owner).Include(e => e.Violation);
            return View(estatePenalties.ToList());
        }

        // GET: EstatePenalties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstatePenalty estatePenalty = db.estatePenalties.Find(id);
            if (estatePenalty == null)
            {
                return HttpNotFound();
            }
            return View(estatePenalty);
        }

        // GET: EstatePenalties/Create
        public ActionResult Create()
        {
            ViewBag.AreaCode = new SelectList(db.areas, "AreaCode", "AreaName");
            ViewBag.OwnerId = new SelectList(db.owners, "OwnerId", "OwnerName");
            ViewBag.ViolationCode = new SelectList(db.violations, "ViolationCode", "ViolationName");
            return View();
        }

        // POST: EstatePenalties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PenaltyId,OwnerId,AreaCode,ViolationCode,TotalPenaltyCost")] EstatePenalty estatePenalty)
        {
            if (ModelState.IsValid)
            {
                db.estatePenalties.Add(estatePenalty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AreaCode = new SelectList(db.areas, "AreaCode", "AreaName", estatePenalty.AreaCode);
            ViewBag.OwnerId = new SelectList(db.owners, "OwnerId", "OwnerName", estatePenalty.OwnerId);
            ViewBag.ViolationCode = new SelectList(db.violations, "ViolationCode", "ViolationName", estatePenalty.ViolationCode);
            return View(estatePenalty);
        }

        // GET: EstatePenalties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstatePenalty estatePenalty = db.estatePenalties.Find(id);
            if (estatePenalty == null)
            {
                return HttpNotFound();
            }
            ViewBag.AreaCode = new SelectList(db.areas, "AreaCode", "AreaName", estatePenalty.AreaCode);
            ViewBag.OwnerId = new SelectList(db.owners, "OwnerId", "OwnerName", estatePenalty.OwnerId);
            ViewBag.ViolationCode = new SelectList(db.violations, "ViolationCode", "ViolationName", estatePenalty.ViolationCode);
            return View(estatePenalty);
        }

        // POST: EstatePenalties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PenaltyId,OwnerId,AreaCode,ViolationCode,TotalPenaltyCost")] EstatePenalty estatePenalty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estatePenalty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AreaCode = new SelectList(db.areas, "AreaCode", "AreaName", estatePenalty.AreaCode);
            ViewBag.OwnerId = new SelectList(db.owners, "OwnerId", "OwnerName", estatePenalty.OwnerId);
            ViewBag.ViolationCode = new SelectList(db.violations, "ViolationCode", "ViolationName", estatePenalty.ViolationCode);
            return View(estatePenalty);
        }

        // GET: EstatePenalties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstatePenalty estatePenalty = db.estatePenalties.Find(id);
            if (estatePenalty == null)
            {
                return HttpNotFound();
            }
            return View(estatePenalty);
        }

        // POST: EstatePenalties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstatePenalty estatePenalty = db.estatePenalties.Find(id);
            db.estatePenalties.Remove(estatePenalty);
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
