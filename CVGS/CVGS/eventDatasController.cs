using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CVGS
{
    public class eventDatasController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: eventDatas
        public ActionResult Index()
        {
            var eventDatas = db.eventDatas.Include(e => e.user);
            return View(eventDatas.ToList());
        }

        // GET: eventDatas/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eventData eventData = db.eventDatas.Find(id);
            if (eventData == null)
            {
                return HttpNotFound();
            }
            return View(eventData);
        }

        // GET: eventDatas/Create
        public ActionResult Create()
        {
            ViewBag.createdBy = new SelectList(db.users, "username", "firstName");
            return View();
        }

        // POST: eventDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "eventId,name,date,createdBy,description")] eventData eventData)
        {
            if (ModelState.IsValid)
            {
                db.eventDatas.Add(eventData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.createdBy = new SelectList(db.users, "username", "firstName", eventData.createdBy);
            return View(eventData);
        }

        // GET: eventDatas/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eventData eventData = db.eventDatas.Find(id);
            if (eventData == null)
            {
                return HttpNotFound();
            }
            ViewBag.createdBy = new SelectList(db.users, "username", "firstName", eventData.createdBy);
            return View(eventData);
        }

        // POST: eventDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "eventId,name,date,createdBy,description")] eventData eventData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.createdBy = new SelectList(db.users, "username", "firstName", eventData.createdBy);
            return View(eventData);
        }

        // GET: eventDatas/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            eventData eventData = db.eventDatas.Find(id);
            if (eventData == null)
            {
                return HttpNotFound();
            }
            return View(eventData);
        }

        // POST: eventDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            eventData eventData = db.eventDatas.Find(id);
            db.eventDatas.Remove(eventData);
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
