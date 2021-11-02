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
    public class MarketController : Controller
    {
        private MassDBEntities db = new MassDBEntities();

        // GET: Market
        public ActionResult Index()
        {
            var markets = db.Markets.Include(m => m.MassMamber);
            return View(markets.ToList());
        }

        // GET: Market/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Market market = db.Markets.Find(id);
            if (market == null)
            {
                return HttpNotFound();
            }
            return View(market);
        }

        // GET: Market/Create
        public ActionResult Create()
        {
            ViewBag.MassMemberId = new SelectList(db.MassMambers, "Id", "Name");
            return View();
        }

        // POST: Market/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MarketId,MarketingOpeningDate,MarketingCloseingDate,Filter,Boil,MarketingTk,BigMarketingTk,TotalTk,MassMemberId")] Market market)
        {
            if (ModelState.IsValid)
            {
                db.Markets.Add(market);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MassMemberId = new SelectList(db.MassMambers, "Id", "Name", market.MassMemberId);
            return View(market);
        }

        // GET: Market/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Market market = db.Markets.Find(id);
            if (market == null)
            {
                return HttpNotFound();
            }
            ViewBag.MassMemberId = new SelectList(db.MassMambers, "Id", "Name", market.MassMemberId);
            return View(market);
        }

        // POST: Market/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MarketId,MarketingOpeningDate,MarketingCloseingDate,Filter,Boil,MarketingTk,BigMarketingTk,TotalTk,MassMemberId")] Market market)
        {
            if (ModelState.IsValid)
            {
                db.Entry(market).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MassMemberId = new SelectList(db.MassMambers, "Id", "Name", market.MassMemberId);
            return View(market);
        }

        // GET: Market/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Market market = db.Markets.Find(id);
            if (market == null)
            {
                return HttpNotFound();
            }
            return View(market);
        }

        // POST: Market/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Market market = db.Markets.Find(id);
            db.Markets.Remove(market);
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
