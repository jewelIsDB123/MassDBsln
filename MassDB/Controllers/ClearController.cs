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
    public class ClearController : Controller
    {
        private MassDBEntities db = new MassDBEntities();

        // GET: Clear
        public ActionResult Index()
        {
            var clears = db.Clears.Include(c => c.MassMamber);
            return View(clears.ToList());
        }

        // GET: Clear/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clear clear = db.Clears.Find(id);
            if (clear == null)
            {
                return HttpNotFound();
            }
            return View(clear);
        }

        // GET: Clear/Create
        public ActionResult Create()
        {
            ViewBag.MassMemberId = new SelectList(db.MassMambers, "Id", "Name");
            return View();
        }

        // POST: Clear/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClearId,WashRoomClearingDate,FilterClearingDate,Notes,MassMemberId")] Clear clear)
        {
            if (ModelState.IsValid)
            {
                db.Clears.Add(clear);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MassMemberId = new SelectList(db.MassMambers, "Id", "Name", clear.MassMemberId);
            return View(clear);
        }

        // GET: Clear/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clear clear = db.Clears.Find(id);
            if (clear == null)
            {
                return HttpNotFound();
            }
            ViewBag.MassMemberId = new SelectList(db.MassMambers, "Id", "Name", clear.MassMemberId);
            return View(clear);
        }

        // POST: Clear/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClearId,WashRoomClearingDate,FilterClearingDate,Notes,MassMemberId")] Clear clear)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clear).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MassMemberId = new SelectList(db.MassMambers, "Id", "Name", clear.MassMemberId);
            return View(clear);
        }

        // GET: Clear/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clear clear = db.Clears.Find(id);
            if (clear == null)
            {
                return HttpNotFound();
            }
            return View(clear);
        }

        // POST: Clear/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clear clear = db.Clears.Find(id);
            db.Clears.Remove(clear);
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
