using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Interview.Models.Container;

namespace Interview.Controllers
{
    public class KeyValuesController : Controller
    {
        private HouseRulesEntities db = new HouseRulesEntities();

        // GET: KeyValues
        public ActionResult Index()
        {
            return View(db.KeyValue.ToList());
        }

        // GET: KeyValues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyValue keyValue = db.KeyValue.Find(id);
            if (keyValue == null)
            {
                return HttpNotFound();
            }
            return View(keyValue);
        }

        // GET: KeyValues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KeyValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KeyID,KeyName,Status,Description,CreateDate")] KeyValue keyValue)
        {
            if (ModelState.IsValid)
            {
                db.KeyValue.Add(keyValue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(keyValue);
        }

        // GET: KeyValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyValue keyValue = db.KeyValue.Find(id);
            if (keyValue == null)
            {
                return HttpNotFound();
            }
            return View(keyValue);
        }

        // POST: KeyValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KeyID,KeyName,Status,Description,CreateDate")] KeyValue keyValue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(keyValue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(keyValue);
        }

        // GET: KeyValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyValue keyValue = db.KeyValue.Find(id);
            if (keyValue == null)
            {
                return HttpNotFound();
            }
            return View(keyValue);
        }

        // POST: KeyValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KeyValue keyValue = db.KeyValue.Find(id);
            db.KeyValue.Remove(keyValue);
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
