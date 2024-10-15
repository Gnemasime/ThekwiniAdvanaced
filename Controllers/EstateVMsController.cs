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
    public class EstateVMsController : Controller
    {
        private AppDBContext db = new AppDBContext();

        // GET: EstateVMs
        public ActionResult Index()
        {
            // return View(db.estateVMs.ToList());
           // AppDBContext db = new AppDBContext();
            List<EstateVM> estates = new List<EstateVM>();
            var driverList = (from tn in db.owners
                              join pen in db.estatePenalties on tn.OwnerId equals pen.OwnerId
                              join are in db.areas on pen.AreaCode equals are.AreaCode
                              join vio in db.violations on pen.ViolationCode equals vio.ViolationCode
                              select new { tn.OwnerName, tn.OwnerPoints, tn.Status, vio.ViolationName, are.AreaName, pen.TotalPenaltyCost }).ToList();
            foreach( var driver in driverList)
            {
                EstateVM estate = new EstateVM();
                estate.Ownername = driver.OwnerName;
                estate.OwnerPoints = driver.OwnerPoints;
                estate.Status = driver.Status;
                estate.ViolationName = driver.ViolationName;
                estate.AreaName = driver.AreaName;
                estate.T_Cost = driver.TotalPenaltyCost;

                estates.Add(estate);
            }
            return View(estates);
        }

        // GET: EstateVMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstateVM estateVM = db.estateVMs.Find(id);
            if (estateVM == null)
            {
                return HttpNotFound();
            }
            return View(estateVM);
        }

        // GET: EstateVMs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstateVMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EstateVMId,Ownername,OwnerPoints,Status,ViolationName,AreaName,T_Cost")] EstateVM estateVM)
        {
            if (ModelState.IsValid)
            {
                db.estateVMs.Add(estateVM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estateVM);
        }

        // GET: EstateVMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstateVM estateVM = db.estateVMs.Find(id);
            if (estateVM == null)
            {
                return HttpNotFound();
            }
            return View(estateVM);
        }

        // POST: EstateVMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstateVMId,Ownername,OwnerPoints,Status,ViolationName,AreaName,T_Cost")] EstateVM estateVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estateVM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estateVM);
        }

        // GET: EstateVMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstateVM estateVM = db.estateVMs.Find(id);
            if (estateVM == null)
            {
                return HttpNotFound();
            }
            return View(estateVM);
        }

        // POST: EstateVMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstateVM estateVM = db.estateVMs.Find(id);
            db.estateVMs.Remove(estateVM);
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
