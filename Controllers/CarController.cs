using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class CarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Car
        public ActionResult Index()
        {
            return View(db.Car.ToList());
        }

        public ActionResult Search(string searchTerm)
        {
            return View("Index", db.Car.Where(x => x.Name.Contains(searchTerm)).ToList());
        }

        // GET: Car/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car Car = db.Car.Find(id);
            if (Car == null)
            {
                return HttpNotFound();
            }
            
            return View(Car);
        }

        // GET: Car/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CarTypes = GetCarTypes();
            return View();
        }

        public List<SelectListItem> GetCarTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem() { Value = "Compact", Text = "Compact"},
                new SelectListItem() { Value = "Economy", Text = "Economy"},
                new SelectListItem() { Value = "Mid Size", Text = "Mid Size" },
                new SelectListItem() { Value = "Full Size", Text = "Full Size" },
                new SelectListItem() { Value = "Truck", Text = "Truck" },
                new SelectListItem() { Value = "Sports", Text = "Sports" }
            };
        }

        // POST: Car/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarId,Name,Type,ImageFile,FileAttach")] Car car)
        {   
            UpdateFileDetails(car);
            if (ModelState.IsValid)
            {
                db.Car.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(car);
        }

        // GET: Car/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car Car = db.Car.Find(id);
            if (Car == null)
            {
                return HttpNotFound();
            }

            ViewBag.CarTypes = GetCarTypes();
            return View(Car);
        }

        private void UpdateFileDetails(Car car)
        {
            HttpPostedFileBase postedFile = car.FileAttach;
            if (postedFile != null)
            {
                byte[] bytes;
                using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                {
                    bytes = br.ReadBytes(postedFile.ContentLength);
                }

                car.ImageFileName = Path.GetFileName(postedFile.FileName);
                car.ImageContentType = postedFile.ContentType;
                car.ImageFile = bytes;
            }
        }

        // POST: Car/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarId,Name,Type,ImageFile,FileAttach")] Car car)
        {
            UpdateFileDetails(car);
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Car/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car Car = db.Car.Find(id);
            if (Car == null)
            {
                return HttpNotFound();
            }
            return View(Car);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car Car = db.Car.Find(id);
            db.Car.Remove(Car);
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
