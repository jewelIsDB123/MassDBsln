using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MassDB.Models;

namespace MassDB.Controllers
{
    public class MassMamberController : Controller
    {
        private MassDBEntities db = new MassDBEntities();

        // GET: MassMamber
        public ActionResult Index()
        {
            var massMambers = db.MassMambers.Include(m => m.District);
            return View(massMambers.ToList());
        }

        // GET: MassMamber/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MassMamber massMamber = db.MassMambers.Find(id);
            if (massMamber == null)
            {
                return HttpNotFound();
            }
            return View(massMamber);
        }

        // GET: MassMamber/Create
        public ActionResult Create()
        {
            ViewBag.DistrictId = new SelectList(db.Districts, "DistrictId", "DistrictName");
            return View();
        }

        // POST: MassMamber/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,PhoneNumber,EmailAddress,Age,JoinInDate,DistrictId")] MassMamber massMamber)
        {
            if (ModelState.IsValid)
            {
                db.MassMambers.Add(massMamber);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DistrictId = new SelectList(db.Districts, "DistrictId", "DistrictName", massMamber.DistrictId);
            return View(massMamber);
        }

        // GET: MassMamber/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MassMamber massMamber = db.MassMambers.Find(id);
            if (massMamber == null)
            {
                return HttpNotFound();
            }
            ViewBag.DistrictId = new SelectList(db.Districts, "DistrictId", "DistrictName", massMamber.DistrictId);
            return View(massMamber);
        }

        // POST: MassMamber/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PhoneNumber,EmailAddress,Age,JoinInDate,DistrictId")] MassMamber massMamber)
        {
            if (ModelState.IsValid)
            {
                db.Entry(massMamber).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DistrictId = new SelectList(db.Districts, "DistrictId", "DistrictName", massMamber.DistrictId);
            return View(massMamber);
        }

        // GET: MassMamber/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MassMamber massMamber = db.MassMambers.Find(id);
            if (massMamber == null)
            {
                return HttpNotFound();
            }
            return View(massMamber);
        }

        // POST: MassMamber/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MassMamber massMamber = db.MassMambers.Find(id);
            db.MassMambers.Remove(massMamber);
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
