using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CVGS.Models;

namespace CVGS.Controllers
{
    public class EventController : Controller
    {

        private CVGSEntities _context = new CVGSEntities();

        public EventController(CVGSEntities context)
        {
            _context = context;
        }

        public EventController()
        {

        }

        // GET: Event/Index
        public ActionResult Index()
        {
            var events = _context.eventDatas;

            return View(events.ToList());
        }

        // GET: Event/Details
        public ActionResult View(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var events = _context.eventDatas
                .SingleOrDefault(a => a.eventId == id);

            if (events == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(events);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create(eventData eventDatas)
        {
            if (ModelState.IsValid)
            {
                _context.eventDatas.Add(eventDatas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventDatas);
        }

        //GET: CVGS/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var events = _context.eventDatas.SingleOrDefault(m => m.eventId == id);
            if (events == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(events);
        }

        //POST: Event/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Edit(int? id, eventData eventDatas)
        {
            if (id != eventDatas.eventId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                //_context.events.Update(events);
                var oldEvent = _context.eventDatas.SingleOrDefault(a => a.eventId == id);
                _context.eventDatas.Remove(oldEvent);

                _context.eventDatas.Add(eventDatas);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventDatas);
        }

        //GET: Event/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var events = _context.eventDatas
                .SingleOrDefault(m => m.eventId == id);
            if (events == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(events);
        }

        //POST: Event/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> DeleteConfirmed(int id)
        {
            var events = _context.eventDatas.SingleOrDefault(a => a.eventId == id);
            _context.eventDatas.Remove(events);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}