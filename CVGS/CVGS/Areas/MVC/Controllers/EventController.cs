using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CVGS.Areas.MVC.Controllers
{
    public class EventController : Controller
    {       

        public readonly CVGSEntities _context;

        public EventController() { }

        public EventController(CVGSEntities context)
        {
            _context = context;
        }

        // GET: MVC/Event/Index
        public ActionResult Index(int? eventId)
        {
            var cvgsContext = _context.events
                .OrderBy(a => Convert.ToInt32(a.eventId));

            return View(cvgsContext.ToList());
        }

        // GET: MVC/Event/Details
        public ActionResult View(int id)
        {
            /*if (id == null)
            {
                return NotFound();
            }*/

            var events = _context.events
                .SingleOrDefault(a => a.eventId == id);

            /*if (events == null)
            {
                return NotFound();
            }*/

            return View(events);
        }

        // GET: MVC/Event/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: MVC/Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create(@event events)
        {
            if (ModelState.IsValid)
            {
                _context.events.Add(events);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(events);
        }

        //GET: MVC/CVGS/Edit/id
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var events = _context.events.SingleOrDefault(m => m.eventId == id);
            //if (events == null)
            //{
            //    return NotFound();
            //}
            return View(events);
        }

        //POST: MVC/Event/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Edit(int? id, @event events)
        {
            //if (id != events.eventId)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                //_context.events.Update(events);
                var oldEvent = _context.events.SingleOrDefault(a => a.eventId == id);
                _context.events.Remove(oldEvent);

                _context.events.Add(events);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(events);
        }

        //GET: MVC/Event/Delete/id
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var events = _context.events
                .SingleOrDefault(m => m.eventId == id);
            //if (busRoute == null)
            //{
            //    return NotFound();
            //}

            return View(events);
        }

        //POST: MVC/Event/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> DeleteConfirmed(int id)
        {
            var events = _context.events.SingleOrDefault(a => a.eventId == id);
            _context.events.Remove(events);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}