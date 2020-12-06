using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class MaintenanceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Maintenance
        public ActionResult Index()
        {
            return View(db.Maintenance.ToList());
        }

        public ActionResult CarIndex(int? carId)
        {
            if (carId.HasValue)
            {
                ViewBag.CarName = db.Car.Where(x => x.CarId == carId).FirstOrDefault().Name;
                ViewBag.CarId = carId;
                return View("Index", db.Maintenance.Where(x => x.CarId == carId).ToList());
            }

            return View("Index");
        }

        // GET: Maintenance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maintenance Maintenance = db.Maintenance.Find(id);
            if (Maintenance == null)
            {
                return HttpNotFound();
            }
            return View(Maintenance);
        }

        // GET: Maintenance/Create
        [Authorize]
        public ActionResult Create(int? carId)
        {
            Maintenance model = new Maintenance();
            model.CarId = carId.Value;
            return View(model);
        }

        // POST: Maintenance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaintenanceId,CarId,Months,Description")] Maintenance Maintenance)
        {
            if (ModelState.IsValid)
            {
                db.Maintenance.Add(Maintenance);
                db.SaveChanges();
                return RedirectToAction("CarIndex", new { carId = Maintenance.CarId });
            }

            return View(Maintenance);
        }

        // GET: Maintenance/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maintenance Maintenance = db.Maintenance.Find(id);
            if (Maintenance == null)
            {
                return HttpNotFound();
            }
            return View(Maintenance);
        }

        // POST: Maintenance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaintenanceId,CarId,Months,Description")] Maintenance Maintenance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Maintenance).State = EntityState.Modified;
                db.SaveChanges();
                int carId = Maintenance.CarId;
                return RedirectToAction("CarIndex", new { carId = Maintenance.CarId });
            }

            return View(Maintenance);
        }

        // GET: Maintenance/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maintenance Maintenance = db.Maintenance.Find(id);
            if (Maintenance == null)
            {
                return HttpNotFound();
            }
            return View(Maintenance);
        }

        // POST: Maintenance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Maintenance Maintenance = db.Maintenance.Find(id);
            db.Maintenance.Remove(Maintenance);
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
