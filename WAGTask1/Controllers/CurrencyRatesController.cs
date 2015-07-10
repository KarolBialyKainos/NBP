using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WAGTask1.DAL;
using WAGTask1.Models;

namespace WAGTask1.Controllers
{
    public class CurrencyRatesController : Controller
    {
        private BankContext db = new BankContext();

        // GET: CurrencyRates
        public ActionResult Index()
        {
            var currencyRates = db.CurrencyRates.Include(c => c.Currency);
            return View(currencyRates.ToList());
        }

        // GET: CurrencyRates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyRate currencyRate = db.CurrencyRates.Find(id);
            if (currencyRate == null)
            {
                return HttpNotFound();
            }
            return View(currencyRate);
        }

        // GET: CurrencyRates/Create
        public ActionResult Create()
        {
            ViewBag.CurrencyID = new SelectList(db.Currencies, "ID", "Name");
            return View();
        }

        // POST: CurrencyRates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Date,ConversionFactor,Rate,CurrencyID")] CurrencyRate currencyRate)
        {
            if (ModelState.IsValid)
            {
                db.CurrencyRates.Add(currencyRate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CurrencyID = new SelectList(db.Currencies, "ID", "Name", currencyRate.CurrencyID);
            return View(currencyRate);
        }

        // GET: CurrencyRates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyRate currencyRate = db.CurrencyRates.Find(id);
            if (currencyRate == null)
            {
                return HttpNotFound();
            }
            ViewBag.CurrencyID = new SelectList(db.Currencies, "ID", "Name", currencyRate.CurrencyID);
            return View(currencyRate);
        }

        // POST: CurrencyRates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date,ConversionFactor,Rate,CurrencyID")] CurrencyRate currencyRate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(currencyRate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurrencyID = new SelectList(db.Currencies, "ID", "Name", currencyRate.CurrencyID);
            return View(currencyRate);
        }

        // GET: CurrencyRates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyRate currencyRate = db.CurrencyRates.Find(id);
            if (currencyRate == null)
            {
                return HttpNotFound();
            }
            return View(currencyRate);
        }

        // POST: CurrencyRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CurrencyRate currencyRate = db.CurrencyRates.Find(id);
            db.CurrencyRates.Remove(currencyRate);
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
