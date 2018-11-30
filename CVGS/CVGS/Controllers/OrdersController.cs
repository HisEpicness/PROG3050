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
    public class OrdersController : Controller
    {
        private CVGSEntities db = new CVGSEntities();

        // GET: Orders
        public ActionResult Index()
        {
            string user = Session["User"].ToString();
            var orders = db.orders.Include(o => o.game).Include(o => o.orderStatu).Include(o => o.user);

            if (Session["Emp"] == null)
            {
                orders = db.orders.Include(o => o.game).Include(o => o.orderStatu)
                    .Include(o => o.user).Where(o => o.username == user);
            }
            else
            {
                orders = db.orders.Include(o => o.game).Include(o => o.orderStatu).Include(o => o.user);
            }
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.gameId = new SelectList(db.games, "id", "name");
            ViewBag.status = new SelectList(db.orderStatus, "status", "status");
            ViewBag.username = new SelectList(db.users, "username", "firstName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "username,orderId,gameId,orderDate,shipDate,status")] order order)
        {
            if (ModelState.IsValid)
            {
                db.orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.gameId = new SelectList(db.games, "id", "name", order.gameId);
            ViewBag.status = new SelectList(db.orderStatus, "status", "status", order.status);
            ViewBag.username = new SelectList(db.users, "username", "firstName", order.username);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.gameId = new SelectList(db.games, "id", "name", order.gameId);
            ViewBag.status = new SelectList(db.orderStatus, "status", "status", order.status);
            ViewBag.username = new SelectList(db.users, "username", "firstName", order.username);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "username,orderId,gameId,orderDate,shipDate,status")] order order)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.gameId = new SelectList(db.games, "id", "name", order.gameId);
            ViewBag.status = new SelectList(db.orderStatus, "status", "status", order.status);
            ViewBag.username = new SelectList(db.users, "username", "firstName", order.username);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            order order = db.orders.Find(id);
            db.orders.Remove(order);
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
