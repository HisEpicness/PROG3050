using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CVGS.Controllers
{
    public class ReviewController : Controller
    {
        private CVGSEntities db = new CVGSEntities();
        Decimal gameId;
        // GET: Review

        [HttpGet]
        public ActionResult Index(Decimal id)
        {
            var reviewList = db.reviews
                .Where(c => c.gameId == id);
            return View(reviewList.ToList());
        }

        [HttpPost]
        public ActionResult Index(Decimal id, String txtAreaReview)
        {

            if (id != null && txtAreaReview != null)
            {
                review review = new review();
                var username = Session["User"].ToString();
                review.gameId = id;
                review.review1 = txtAreaReview;
                review.username = username;
                review p = db.reviews.OrderByDescending(c => c.id).FirstOrDefault();
                int newId = (null == p ? 0 : p.id) + 1;
                review.id = newId;
                db.reviews.Add(review);
                db.SaveChanges();
            }

            var reviewList = db.reviews
                .Where(c => c.gameId == id);
            return View(reviewList.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(int id, Decimal gameId, FormCollection form)
        {
            bool check = form["id"] != null;
            Decimal gId = Decimal.Parse(form["gameId"]);

            review review = db.reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            review.approve = check;
            db.SaveChanges();
            
            var reviewList = db.reviews
                .Where(c => c.gameId == gId);
            return View("Index", reviewList.ToList());
        }

    }
}